namespace Define.Tokenizer.Model
{
    internal static class Constants
    {
        public static readonly string[] ListedDefinitionKeywords = new string[] { "means", "mean", "shall mean", "shall has the meaning", "shall have the meaning", "has the meaning", "shall bear the meaning" };
        
        // The pattern below can be tested here https://regex101.com/r/E8lqry/5
        public static readonly string ListedDefinitionPattern = @"^(.+?)(means?|shall mean|shall has the meaning|shall have the meaning|has the meaning|shall bear the meaning)(\s|\p{P}|$)";

        public static readonly string QuotedWithArticleInlineDefinitionPattern = @"(the|a|an|as)\s[""“]{1}(.*?)[""”]{1}";

        public static readonly string BoldWithArticleInlineDefinitionPattern = @"(the|a|an)\s?<b>(.*?)<\/b>";

        public static readonly string ItalicWithArticleInlineDefinitionPattern = @"(the|a|an|as)\s?<i>(.*?)<\/i";

        public static readonly string QuotedAndFormattedInlineDefinitionPattern = @"[""“]{1}<[bi]>(.*?)<\/[bi]>[""”]{1}";

        public static readonly string BoldInBracketsInlineDefinitionPattern = @"\(<[b]>(.*?)<\/[b]>\)";

        public static readonly string FormattedWithTextInBracketsInlineDefinitionPattern = @"\([^\(\)]+<[bi]>(.*?)<\/[bi]>\)";

        public static readonly string QuotedInBracketsInlineDefinitionPattern = @"\([^\(\)]*[""“]{1}(.*?)[""”]{1}\)";

        public static readonly string ReferencePattern = @"(Clause|Schedule|Annex)[^\(\)]*\(<i>(.*?)<\/i>\)";

        public static readonly string ItalicTextPattern = @"<i>(.*?)<\/i>";

        public static readonly string BoldTextPattern = @"<b>(.*?)<\/b>";

        //// https://regex101.com/r/9ZzBaP/2
        public static readonly string WhitespaceBetweenCloseAndOpenTags = @"<\/b>\s<b>|<\/i>\s<i>|<\/b><\/i>\s<i><b>|<\/i><\/b>\s<b><i>|<i><\/b>\s<b><\/i>|<b><\/i>\s<i><\/b>";

        public static readonly string QuotedTextPattern = @"[""“]{1}(.+?)[""”]{1}";

        public static readonly string AnyTextPattern = @"[A-Za-z]+";

        public static readonly string AlternativePattern = @"^(.*?)\sor\s(.*?)$";

        public static readonly string SchedulePattern = @"^Schedule";

        public static readonly string AnnexPattern = @"^Annex";

        public static readonly string SectionPattern = @"^Section";

        public static readonly string AppendixPattern = @"^Appendix";

        public static readonly string AllTextBoldPattern = @"^<b>.*?<\/b>$";

        public static readonly char[] ConstsToTrim = { '"', '”', '“', '\a', '\r', '\t', '\n', '[', ']', (char)0xA0};

    }
}
