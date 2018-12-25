using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Define.Common.Extensions;
using Define.Tokenizer.Model;

namespace Define.Tokenizer.Extensions
{
    public static class StringExtensions
    {
        public static bool StartsWithKeyword(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            if (Constants.ListedDefinitionKeywords.Any(kw => text.StartsWith(kw)))
                return true;

            return false;
        }

        public static bool HasListedDefinition(this string text)
        {
            return Regex.IsMatch(text.RemoveNonWordCharacters(), Constants.ListedDefinitionPattern);
        }

        public static bool IsAnyText(this string text)
        {
            return Regex.IsMatch(text.RemoveNonWordCharacters(), Constants.AnyTextPattern);
        }

        public static string RemoveNonWordCharacters(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var result = text;

            foreach (var c in Constants.ConstsToTrim)
            {
                result = result.Replace(c.ToString(), " ");
            }

            result = Regex.Replace(result, @"[ ]{2,}", " ");

            return result.Trim();
        }

        private static IEnumerable<string> GetTextByRegexPattern(this string text, string pattern, RegexOptions regexOptions = RegexOptions.None, bool IsCapitalizedOnly=true)
        {
            var results = new HashSet<string>();

            if (string.IsNullOrEmpty(text)) return results;

            var matches = Regex.Matches(text, pattern, regexOptions).Cast<Match>();
            
            foreach (var match in matches)
            {
                foreach(var group in match.Groups.Cast<Group>().Skip(1))
                {
                    var matchedText = group.Value.RemoveNonWordCharacters();

                    if (string.IsNullOrEmpty(matchedText) || 
                        (IsCapitalizedOnly && !matchedText.IsCapitalized())) continue;

                    results.Add(matchedText);
                }
            }

            return results;
        }

        public static IEnumerable<string> GetQuotedText(this string text, bool IsCapitalized=true)
        {
            return GetTextByRegexPattern(text, Constants.QuotedTextPattern, RegexOptions.None, IsCapitalized);
        }

        public static IEnumerable<string> GetItalicText(this string text, bool IsCapitalized = true)
        {
            return GetTextByRegexPattern(text, Constants.ItalicTextPattern, RegexOptions.None, IsCapitalized);
        }

        public static IEnumerable<string> GetBoldText(this string text, bool IsCapitalized = true)
        {
            return GetTextByRegexPattern(text, Constants.BoldTextPattern, RegexOptions.None, IsCapitalized);
        }

        public static string GetListedDefinition(this string text)
        {
            return GetTextByRegexPattern(text, Constants.ListedDefinitionPattern).FirstOrDefault();
        }

        public static IEnumerable<string> GetTableDefinitions(this string text)
        {
            return GetTextByRegexPattern(text, Constants.AlternativePattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetQuotedInlineDefinitions(this string text)
        {
            return GetTextByRegexPattern(text, Constants.QuotedWithArticleInlineDefinitionPattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetBoldWithArticleInlineDefinitions(this string text)
        {
            return GetTextByRegexPattern(text, Constants.BoldWithArticleInlineDefinitionPattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetItalicWithArticleInlineDefinitions(this string text)
        {
            return GetTextByRegexPattern(text, Constants.ItalicWithArticleInlineDefinitionPattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetQuotedAndFormattedInlineDefinitions(this string text)
        {
            return GetTextByRegexPattern(text, Constants.QuotedAndFormattedInlineDefinitionPattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetBoldInBracketsInlineDefinitions(this string text)
        {
            return GetTextByRegexPattern(text, Constants.BoldInBracketsInlineDefinitionPattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetFormattedWithTextInBracketsInlineDefinitions(this string text)
        {
            return GetTextByRegexPattern(text, Constants.FormattedWithTextInBracketsInlineDefinitionPattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetQuotedInBracketsInlineDefinition(this string text)
        {
            return GetTextByRegexPattern(text, Constants.QuotedInBracketsInlineDefinitionPattern, RegexOptions.IgnoreCase);
        }

        public static bool HasReference(this string text)
        {
            return Regex.IsMatch(text, Constants.ReferencePattern, RegexOptions.IgnoreCase);
        }

        public static IEnumerable<string> GetReferences(this string text)
        {
            return GetTextByRegexPattern(text, Constants.ReferencePattern, RegexOptions.IgnoreCase);
        }

        public static string RemoveItalicTags(this string text)
        {
            return text.Replace("<i>", "").Replace("</i>", "");
        }

        public static string RemoveBoldTags(this string text)
        {
            return text.Replace("<b>", "").Replace("</b>", "");
        }

        public static bool IsAllTextInBold(this string text)
        {
            return Regex.IsMatch(text, Constants.AllTextBoldPattern);
        }

        public static bool IsSchedule(this string text)
        {
            return Regex.IsMatch(text, Constants.SchedulePattern, RegexOptions.IgnoreCase);
        }

        public static bool IsAnnex(this string text)
        {
            return Regex.IsMatch(text, Constants.AnnexPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsAppendix(this string text)
        {
            return Regex.IsMatch(text, Constants.AppendixPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsSection(this string text)
        {
            return Regex.IsMatch(text, Constants.SectionPattern, RegexOptions.IgnoreCase);
        }


    }
}
