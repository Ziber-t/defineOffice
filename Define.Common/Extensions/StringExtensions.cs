using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using Pluralize.NET;

namespace Define.Common.Extensions
{
    public static class StringExtensions
    {
        private static readonly string[] Articles = { "the ", "an ", "a " };

        public static bool IsCapitalized(this string word)
        {
            return char.IsUpper(word[0]) || char.IsNumber(word[0]);
        }

        public static bool IsCapitalized(this char symbol)
        {
            return char.IsUpper(symbol);
        }

        public static string RefineSelectionText(this string selection)
        {
            if (string.IsNullOrWhiteSpace(selection)) return string.Empty;

            for (int i = 0; i < Articles.Length; ++i) {
                if (selection.StartsWith(Articles[i], StringComparison.OrdinalIgnoreCase)) {
                    selection = selection.Remove(0, Articles[i].Length);
                    break;
                }
            }

            selection = selection.Trim();
            if (selection.StartsWith("\"") || selection.StartsWith("“")) 
                selection = selection.Remove(0, 1);

            if(selection.EndsWith("\"") || selection.EndsWith("”"))
                selection = selection.Remove(selection.Length - 1, 1);

            return selection;
        }

        public static bool Contains(this string src, string toCheck, StringComparison comp)
        {
            return src.IndexOf(toCheck, comp) >= 0;
        }

        public static string ToSingularForm(this string word)
        {
            if (word.IsPlural())
            {
                return new Pluralizer().Singularize(word);
            }

            return word;
        }

        public static string ToPluralForm(this string word)
        {
            if (word.IsSingular())
            {
                return new Pluralizer().Pluralize(word);
            }

            return word;
        }

        public static bool IsPlural(this string word)
        {
            var pluralService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-GB"));

            return pluralService.IsPlural(word);
        }

        public static bool IsSingular(this string word)
        {
            var pluralService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-GB"));

            return pluralService.IsSingular(word);
        }
    }
}
