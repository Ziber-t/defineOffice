using Define.Common.Diagnostic;
using Define.Common.Helpers;
using Define.Tokenizer.Extensions;
using Define.Tokenizer.Model;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Define.Tokenizer
{
    public class DefineTokenizer
    {
        private readonly TokenContext _tokens;

        public DefineTokenizer(TokenContext tokenContext)
        {
            _tokens = tokenContext ?? throw new ArgumentNullException(nameof(tokenContext));
        }
        
        /// <summary>
        /// Find definitions in docx file
        /// </summary>
        /// <param name="path">Path to docx file</param>
        public void TokenizeFile(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var wordDocument = WordprocessingDocument.Open(stream, false))
                {
                    ProcessDocument(wordDocument);
                }
            }
        }

        /// <summary>
        /// Find definitions in OpenXML(OPC) string
        /// </summary>
        /// <param name="openXml">OpenXML(OPC) string</param>
        public void TokenizeOpenXml(string openXml)
        {
            using (var wordDocument = WordprocessingDocument.FromFlatOpcString(openXml))
            {
                ProcessDocument(wordDocument);
            }
        }

        private void ProcessDocument(WordprocessingDocument document)
        {
            // get body from the document
            var body = document.MainDocumentPart.Document.Body;
            if (body == null)
            {
                LogHelper.Fatal("Failed to get the body of the document");
                return;
            }

            // remove all elements from the body which may prevent from matching paragraphs
            CleanUpBody(body);

            // process body of the document
            ProcessBody(body);
        }

        private void ProcessBody(Body body)
        {
            // get good paragraphs
            var filterStopwatch = Stopwatch.StartNew();
            var paragraphs = body.Descendants<Paragraph>().Where(p => p.IsGood()).ToList();
            filterStopwatch.Stop();
            DiagnosticInformation.OpenXmlFilterParagraphsTimeSpan = filterStopwatch.Elapsed;

            // dump paragraphs for diagnostic
            var paragraphsDump = new StringBuilder();
            paragraphs.ForEach(p => paragraphsDump.AppendLine(p.GetText()));
            FileHelper.SaveDiagnosticFileToTempFolder("openxml_paragraphs.txt", paragraphsDump.ToString());

            // find listed definitions
            var findListedDefinitionsStopwatch = Stopwatch.StartNew();
            TokenizeListedDefinitions(paragraphs);
            findListedDefinitionsStopwatch.Stop();
            DiagnosticInformation.OpenXmlProcessListedDefinitionsTimeSpan += findListedDefinitionsStopwatch.Elapsed;

            // find definitions in table
            var findDefinitionsInTableStopwatch = Stopwatch.StartNew();
            TokenizeTableDefinitions(paragraphs);
            findDefinitionsInTableStopwatch.Stop();
            DiagnosticInformation.OpenXmlProcessTablesTimeSpan += findDefinitionsInTableStopwatch.Elapsed;

            // find inline definitions
            var findInlineDefinitionsStopwatch = Stopwatch.StartNew();
            TokenizeInlineDefinitions(paragraphs);
            findInlineDefinitionsStopwatch.Stop();
            DiagnosticInformation.OpenXmlProcessInlineDefinitionsTimeSpan += findInlineDefinitionsStopwatch.Elapsed;


            // save paragraph count to diagnostic
            DiagnosticInformation.OpenXmlParagraphCount = paragraphs.Count;
        }

        private void CleanUpBody(Body body)
        {
            // remove all pictures from the document
            foreach (var picture in body.Descendants<Picture>())
            {
                picture.Remove();
            }

            // remove all alternate content from the document
            foreach (var alternateContent in body.Descendants<AlternateContent>())
            {
                alternateContent.Remove();
            }
        }

        private void TokenizeInlineDefinitions(IList<Paragraph> paragraphs)
        {
            for (var i = 0; i < paragraphs.Count(); i++)
            {
                var paragraph = paragraphs[i];

                if (paragraph.IsInTableWithTwoColumns()) continue;

                var inlineDefinitions = paragraph.GetInlineDefinitions();

                if (!inlineDefinitions.Any()) continue;

                foreach(var inlineDefinition in inlineDefinitions)
                {
                    _tokens.Add(inlineDefinition, i, i, TokenType.InlineDefinition);
                }
            }
        }

        private void TokenizeListedDefinitions(IList<Paragraph> paragraphs)
        {
            for(var i=0; i < paragraphs.Count(); i++)
            {
                var paragraph = paragraphs[i];

                if (paragraph.IsInTableWithTwoColumns()) continue;

                var startIndex = i;
                var stopIndex = i;
                string listedDefinition;

                // for the case when definition and its text located in different paragraphs
                // for example:
                // “Associated Person”
                // means in relation to a company, a person(including are employee, agent or Group).
                if (paragraph.StartsWithKeyword() && i > 0)
                {
                    var previousParagraph = paragraphs[i - 1];
                    listedDefinition = previousParagraph.GetText().RemoveNonWordCharacters();
                    startIndex--;
                }
                else
                {
                    // there can be only one listed definition per paragraph
                    listedDefinition = paragraph.GetListedDefinition();
                }
                
                if (string.IsNullOrEmpty(listedDefinition)) continue;

                while(++stopIndex < paragraphs.Count())
                {
                    var nextParagraph = paragraphs[stopIndex];

                    // for the case when definition and its text located in different paragraphs
                    // for example:
                    // “Associated Person”
                    // means in relation to a company, a person(including are employee, agent or Group).
                    if (nextParagraph.StartsWithKeyword())
                    {
                        stopIndex--;
                        break;
                    }

                    // stop if next paragraph has heading style
                    if (nextParagraph.HasHeadingStyle())
                        break;
                    
                    // stop if we find next listed definition
                    if (nextParagraph.HasListedDefinition())
                        break;

                    // TODO: add condition here to stop processing
                    // when new clause, section or schedule is found
                }

                _tokens.Add(listedDefinition, startIndex, --stopIndex, TokenType.ListedDefinition);

                i = stopIndex;
            }
        }

        private void TokenizeTableDefinitions(IList<Paragraph> paragraphs)
        {
            for (var i = 0; i < paragraphs.Count(); i++)
            {
                var startIndex = i;
                var paragraph = paragraphs[i];

                if (!paragraph.IsInTableWithTwoColumns()) continue;

                if (!paragraph.IsInFirstColumn()) continue;

                var lastParagraph = paragraph.GetLastParagraphFromSecondColumn();

                if (lastParagraph == null) continue;

                while (lastParagraph != paragraphs[++i]) { }

                var endIndex = i;

                var firstColumn = new List<Paragraph>();
                var secondColumn = new List<Paragraph>();

                // let's build the list of paragraphs in first column and in second
                for (var j = startIndex; j <= endIndex; j++)
                {
                    if (paragraphs[j].IsInFirstColumn())
                        firstColumn.Add(paragraphs[j]);
                    else
                        secondColumn.Add(paragraphs[j]);
                }

                if (secondColumn.First().StartsWithKeyword())
                {
                    var tableDefinitions = firstColumn.First().GetTableDefinitions();

                    foreach (var def in tableDefinitions)
                    {
                        _tokens.Add(def, startIndex, endIndex, TokenType.TableDefinition);
                    }
                }
                else
                {
                    var isFirstColumnFormatted = firstColumn.Any(p => p.HasFormattedText() && !p.HasReference());

                    var isSecondColumnFormatted = secondColumn.Any(p => p.HasFormattedText());

                    var isQuotedTextInFirstColumn = firstColumn.Any(p => p.HasQuotedText());

                    if ((isFirstColumnFormatted && isSecondColumnFormatted) || 
                        (!isFirstColumnFormatted && !isSecondColumnFormatted && !isQuotedTextInFirstColumn) ||
                        (!isFirstColumnFormatted && isSecondColumnFormatted))
                        continue;

                    var tableDefinitions = firstColumn.First().GetTableDefinitions();

                    foreach (var def in tableDefinitions)
                    {
                        _tokens.Add(def, startIndex, endIndex, TokenType.TableDefinition);
                    }

                }
            }
        }
    }
}

