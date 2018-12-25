using System;
using System.Text;

namespace Define.Common.Diagnostic
{
    public static class DiagnosticInformation
    {
        public static TimeSpan TotalScanTimeSpan { get; set; }
        public static TimeSpan WordIndexParagraphsTimeSpan { get; set; }
        public static TimeSpan WordRetrieveOpenXmlTimeSpan { get; set; }
        public static TimeSpan WordSaveFileTimeSpan { get; set; }
        public static TimeSpan OpenXmlTotalTime { get; set; }
        public static TimeSpan OpenXmlFilterParagraphsTimeSpan { get; set; }
        public static TimeSpan OpenXmlExtractParagraphTextTimeSpan { get; set; }
        public static TimeSpan OpenXmlProcessListedDefinitionsTimeSpan { get; set; }
        public static TimeSpan OpenXmlProcessInlineDefinitionsTimeSpan { get; set; }
        public static TimeSpan OpenXmlProcessTablesTimeSpan { get; set; }
        public static int DefinitionsCount { get; set; }
        public static int WordParagraphCount { get; set; }
        public static int OpenXmlParagraphCount { get; set; }

        public static void Reset()
        {
            TotalScanTimeSpan = default(TimeSpan);
            WordSaveFileTimeSpan = default(TimeSpan);
            WordIndexParagraphsTimeSpan = default(TimeSpan);
            WordRetrieveOpenXmlTimeSpan = default(TimeSpan);
            OpenXmlTotalTime = default(TimeSpan);
            OpenXmlExtractParagraphTextTimeSpan = default(TimeSpan);
            OpenXmlProcessListedDefinitionsTimeSpan = default(TimeSpan);
            OpenXmlProcessInlineDefinitionsTimeSpan = default(TimeSpan);
            OpenXmlFilterParagraphsTimeSpan = default(TimeSpan);
            OpenXmlProcessTablesTimeSpan = default(TimeSpan);
            WordParagraphCount = default(int);
            OpenXmlParagraphCount = default(int);
            DefinitionsCount = default(int);
        }

        public static string Dump()
        {
            var builder = new StringBuilder();

            builder.AppendLine("");
            builder.AppendLine("========= DIAGNOSTIC SUMMARY ==========");
            builder.AppendLine($"WORD: Total paragraphs: {WordParagraphCount}");
            builder.AppendLine($"OXML: Total paragraphs: {OpenXmlParagraphCount}");
            builder.AppendLine($"WORD: Indexing paragraphs: {WordIndexParagraphsTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"WORD: Saving file: {WordSaveFileTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"WORD: Converting to OpenXML: {WordRetrieveOpenXmlTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"OXML: Filtering paragraphs: {OpenXmlFilterParagraphsTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"OXML: Getting paragraph text: {OpenXmlExtractParagraphTextTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"OXML: Processing listed definitions: {OpenXmlProcessListedDefinitionsTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"OXML: Processing tables: {OpenXmlProcessTablesTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"OXML: Processing inline definitions: {OpenXmlProcessInlineDefinitionsTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"OXML: Total scan time: {OpenXmlTotalTime.TotalSeconds} sec");
            builder.AppendLine($"Total scan time: {TotalScanTimeSpan.TotalSeconds} sec");
            builder.AppendLine($"Definitions found: {DefinitionsCount}");
            builder.AppendLine("=======================================");
            builder.AppendLine("");

            return builder.ToString();
        }
    }
}
