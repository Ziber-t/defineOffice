using Define.Common.Extensions;
using Define.Tokenizer.Model;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Define.Tokenizer.Extensions
{
    public static class ParagraphExtensions
    {
        public static IEnumerable<string> GetTableDefinitions(this Paragraph paragraph)
        {
            var definitions = new HashSet<string>();

            var columnText = paragraph.GetText().RemoveNonWordCharacters();

            var tableDefinitions = columnText.GetTableDefinitions();

            if (!tableDefinitions.Any())
                definitions.Add(columnText);

            foreach (var def in tableDefinitions)
                definitions.Add(def);

            return definitions;
        }

        public static IEnumerable<string> GetInlineDefinitions(this Paragraph paragraph)
        {
            var inlineDefinitions = new HashSet<string>();

            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var text = paragraph.GetText();
            var formattedText = paragraph.GetFormattedText();

            if (formattedText.IsAllTextInBold()) return inlineDefinitions;

            var quotedWithArticle = text.GetQuotedInlineDefinitions();
            var boldWithArticle = formattedText.RemoveItalicTags().GetBoldWithArticleInlineDefinitions();
            var italicWithArticle = formattedText.RemoveBoldTags().GetItalicWithArticleInlineDefinitions();
            var quotedAndFormattedAsBold = formattedText.RemoveItalicTags().GetQuotedAndFormattedInlineDefinitions();
            var quotedAndFormattedAsItalic = formattedText.RemoveBoldTags().GetQuotedAndFormattedInlineDefinitions();
            var boldInBrackets = formattedText.RemoveItalicTags().GetBoldInBracketsInlineDefinitions();

            var boldWithTextInBrackets = formattedText.RemoveItalicTags().GetFormattedWithTextInBracketsInlineDefinitions();
            var italicWithTextInBrackets = formattedText.RemoveBoldTags().GetFormattedWithTextInBracketsInlineDefinitions();
            var quotedInBrackets = text.GetQuotedInBracketsInlineDefinition();

            inlineDefinitions.UnionWith(quotedWithArticle);
            inlineDefinitions.UnionWith(boldWithArticle);
            inlineDefinitions.UnionWith(italicWithArticle);
            inlineDefinitions.UnionWith(quotedAndFormattedAsBold);
            inlineDefinitions.UnionWith(quotedAndFormattedAsItalic);
            inlineDefinitions.UnionWith(boldInBrackets);
            inlineDefinitions.UnionWith(boldWithTextInBrackets);
            inlineDefinitions.UnionWith(italicWithTextInBrackets);
            inlineDefinitions.UnionWith(quotedInBrackets);

            return inlineDefinitions;
        }

        public static string GetListedDefinition(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var text = paragraph.GetText();
            var formattedText = paragraph.GetFormattedText();

            var textBeforeKeyword = text.GetListedDefinition();

            if (string.IsNullOrEmpty(textBeforeKeyword)) return null;

            var boldText = formattedText.RemoveItalicTags().GetBoldText();
            var italicText = formattedText.RemoveBoldTags().GetItalicText();
            var quotedText = text.GetQuotedText();

            var boldDefinition = boldText.FirstOrDefault(b => textBeforeKeyword.Contains(b));
            if (!string.IsNullOrEmpty(boldDefinition)) return boldDefinition;

            var quotedDefinition = quotedText.FirstOrDefault(q => textBeforeKeyword.Contains(q));
            if (!string.IsNullOrEmpty(quotedDefinition)) return quotedDefinition;

            var italicDefinition = italicText.FirstOrDefault(i => textBeforeKeyword.Contains(i));

            if (!string.IsNullOrEmpty(italicDefinition) && !formattedText.GetReferences().Contains(italicDefinition))
            {
                return italicDefinition;
            }

            return null;
        }

        public static bool StartsWithKeyword(this Paragraph paragraph)
        {
            return paragraph.GetText().RemoveNonWordCharacters().StartsWithKeyword();
        }

        public static bool HasListedDefinition(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var text = paragraph.GetText().RemoveNonWordCharacters();

            if (string.IsNullOrEmpty(text)) return false;

            return text.HasListedDefinition();
        }

        public static bool HasBookmark(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var bookmarks = paragraph.Descendants<BookmarkStart>();

            return bookmarks.Any();
        }

        public static bool HasHeadingStyle(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var style = paragraph.GetStyle();

            if (string.IsNullOrEmpty(style)) return false;

            return style.Equals("Heading1") || style.Equals("Heading2");
        }

        private static string GetStyle(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var style = paragraph.Descendants<ParagraphStyleId>().FirstOrDefault();

            if (style == null) return null;

            return style.Val.ToString();
        }

        public static bool IsGood(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            return paragraph.GetTextInternal().IsAnyText();
        }

        public static bool IsInTableWithTwoColumns(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var tables = paragraph.Ancestors<Table>();

            if (tables.Any())
            {
                if (tables.Count() > 1) return false;

                var table = tables.First();

                var columns = table.Descendants<GridColumn>();

                if (columns.Count() == 2) return true;
            }

            return false;
        }

        public static bool IsInFirstColumn(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var cell = paragraph.Ancestors<TableCell>().FirstOrDefault();

            if (cell == null) return false;

            return cell.PreviousSibling<TableCell>() == null;
        }

        public static Paragraph GetLastParagraphFromSecondColumn(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var cell = paragraph.Ancestors<TableCell>().FirstOrDefault();

            if (cell == null) return null;

            var secondCell = cell.NextSibling<TableCell>();

            if (secondCell == null) return null;

            return secondCell.Descendants<Paragraph>().LastOrDefault(p => p.IsGood());
        }

        private static string GetTextInternal(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var justText = new StringBuilder();
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(paragraph.OuterXml);

            var runs = xmlDocument.GetElementsByTagName("w:r");

            foreach (XmlElement run in runs)
            {
                var text = run.GetElementsByTagName("w:t").Cast<XmlNode>().FirstOrDefault()?.InnerText;
                var delText = run.GetElementsByTagName("w:delText").Cast<XmlNode>().FirstOrDefault()?.InnerText;

                if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(delText)) continue;

                if (!string.IsNullOrEmpty(text))
                    justText.Append(text);

                if (!string.IsNullOrEmpty(delText))
                    justText.Append(delText);
            }

            return justText.ToString();
        }

        public static string GetText(this Paragraph paragraph)
        {
            return paragraph.GetTextInternal();
        }

        public static string GetFormattedText(this Paragraph paragraph)
        {
            if (paragraph == null) throw new ArgumentNullException(nameof(paragraph));

            var formattedText = new StringBuilder();

            var isPreviousBold = false;
            var isPreviousItalic = false;

            var runs = paragraph.Descendants<Run>();

            foreach (var run in runs)
            {
                var text = run.Elements<Text>().FirstOrDefault()?.Text;

                if (string.IsNullOrEmpty(text)) continue;

                var rProp = run.RunProperties;

                if (rProp?.Bold != null)
                {
                    if (!isPreviousBold)
                    {
                        isPreviousBold = true;
                        formattedText.Append("<b>");
                    }
                }

                if (rProp?.Italic != null)
                {
                    if (!isPreviousItalic)
                    {
                        isPreviousItalic = true;
                        formattedText.Append("<i>");
                    }
                }

                if (rProp?.Italic == null)
                {
                    if (isPreviousItalic)
                    {
                        isPreviousItalic = false;
                        formattedText.Append("</i>");
                    }
                }

                if (rProp?.Bold == null)
                {
                    if (isPreviousBold)
                    {
                        isPreviousBold = false;
                        formattedText.Append("</b>");
                    }
                }

                formattedText.Append(text);
            }


            if (isPreviousItalic)
                formattedText.Append("</i>");

            if (isPreviousBold)
                formattedText.Append("</b>");

            // make sure we replace cases such as "</b> <b>" or "</i> <i>" with single whitespace in result string
            return Regex.Replace(formattedText.ToString(), Constants.WhitespaceBetweenCloseAndOpenTags, " ");
        }

        public static bool HasFormattedText(this Paragraph paragraph)
        {
            var formattedText = paragraph.GetFormattedText();

            var isBold = formattedText.GetBoldText(false).Any();

            var isItalic = formattedText.GetItalicText(false).Any();

            return isBold || isItalic;
        }

        public static bool HasReference(this Paragraph paragraph)
        {
            var formattedText = paragraph.GetFormattedText();

            return formattedText.HasReference();
        }

        public static bool HasQuotedText(this Paragraph paragraph)
        {
            var text = paragraph.GetText();

            return text.GetQuotedText().Any();
        }

        public static bool IsStartOfSchedule(this Paragraph paragraph)
        {
            var text = paragraph.GetText();

            return text.IsSchedule();
        }

        public static bool IsStartOfAnnex(this Paragraph paragraph)
        {
            var text = paragraph.GetText();

            return text.IsAnnex();
        }

        public static bool IsStartOfAppendix(this Paragraph paragraph)
        {
            var text = paragraph.GetText();

            return text.IsAppendix();
        }

        public static bool IsStartOfSection(this Paragraph paragraph)
        {
            var text = paragraph.GetText();

            return text.IsSection();
        }
    }
}
