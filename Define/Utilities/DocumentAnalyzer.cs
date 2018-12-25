using Define.Models;
using Define.Tokenizer;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Define.Common.Diagnostic;
using Define.Common.Helpers;
using System.Threading.Tasks;
using Define.Tokenizer.Extensions;
using System.Text;

namespace Define.Utilities
{
    public class DocumentAnalyzer
    {
        private readonly Document _document;
        private readonly DefineTokenizer _definitionFinder;
        private readonly TokenContext _tokenContext;
        private readonly List<Range> _ranges;

        public DocumentAnalyzer(Document document)
        {
            _document = document;
            _tokenContext = new TokenContext();
            _definitionFinder = new DefineTokenizer(_tokenContext);
            _ranges = new List<Range>();
        }

        public void Analyze()
        {
            var stopwatch = Stopwatch.StartNew();

            InitAnalyzer();

            var filename = _document.FullName;
            
            var search = new TaskFactory().StartNew(() => SearchDefinitions(filename));

            StoreDocumentRangesForNavigation();

            search.Wait();

            stopwatch.Stop();

            DiagnosticInformation.TotalScanTimeSpan = stopwatch.Elapsed;

            LogHelper.Debug(DiagnosticInformation.Dump());
        }

        private void InitAnalyzer()
        {
            DiagnosticInformation.Reset();

            var stopwatch = Stopwatch.StartNew();
            _document.Save();
            stopwatch.Stop();
            DiagnosticInformation.TotalScanTimeSpan = stopwatch.Elapsed;

            _tokenContext.Clear();
            _ranges.Clear();
        }

        private void SearchDefinitions(string file)
        {
            LogHelper.Debug($"Searching definitions in {_document.FullName}...");
            LogHelper.Debug("Clearing results from the previous scan...");
            LogHelper.Debug("Checking if the file has the docx extension...");

            if (file.EndsWith("docx"))
            {
                var stopwatch = Stopwatch.StartNew();
                _definitionFinder.TokenizeFile(file);
                stopwatch.Stop();
                DiagnosticInformation.OpenXmlTotalTime = stopwatch.Elapsed;
            }
            else
            {
                var stopwatch = Stopwatch.StartNew();
                var openXml = _document.Content.WordOpenXML;
                stopwatch.Stop();
                DiagnosticInformation.WordRetrieveOpenXmlTimeSpan = stopwatch.Elapsed;

                stopwatch.Restart();
                _definitionFinder.TokenizeOpenXml(openXml);
                stopwatch.Stop();
                DiagnosticInformation.OpenXmlTotalTime = stopwatch.Elapsed;
            }

            DiagnosticInformation.DefinitionsCount = _tokenContext.GetAll().Count();
        }

        private void StoreDocumentRangesForNavigation()
        {
            var paragraphsDump = new StringBuilder();
            var stopwatch = Stopwatch.StartNew();

            var ranges = _document
                .StoryRanges
                .Cast<Range>()
                .SingleOrDefault(r => r.StoryType == WdStoryType.wdMainTextStory)
                .Paragraphs
                .Cast<Paragraph>()
                .Where(p => Regex.IsMatch(p.Range.Text, @"[A-Za-z]+"))
                .Select(p => p.Range);

            _ranges.AddRange(ranges);

            _ranges.ForEach(r => paragraphsDump.AppendLine(r.Text.RemoveNonWordCharacters()));

            stopwatch.Stop();

            FileHelper.SaveDiagnosticFileToTempFolder("interop_paragraphs.txt", paragraphsDump.ToString());

            DiagnosticInformation.WordIndexParagraphsTimeSpan = stopwatch.Elapsed;
            DiagnosticInformation.WordParagraphCount = _ranges.Count();
        }

        public WordDefinition GetWordDefinition(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;

            var definition = _tokenContext.Get(text);

            if (definition == null) return null;

            return new WordDefinition
            {
                Name = definition.Name,
                Locations = definition.TokenInfos.Select(l =>
                {
                    var startRange = _ranges[l.StartParagraphIndex];
                    var endRange = _ranges[l.EndParagraphIndex];
                    var fullRange = _document.Range(startRange.Start, endRange.End);
                    return fullRange;
                }).ToList()
            };
        }

        public IEnumerable<WordDefinition> GetAllWordDefinitions()
        {
            return _tokenContext.GetAll().Select(d =>
                new WordDefinition
                {
                    Name = d.Name,
                    Locations = d.TokenInfos.Select(l =>
                    {
                        var startRange = _ranges[l.StartParagraphIndex];
                        var endRange = _ranges[l.EndParagraphIndex];
                        var fullRange = _document.Range(startRange.Start, endRange.End);
                        return fullRange;
                    }).ToList()
                });
        }
    }
}
