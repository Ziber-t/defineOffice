using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;

namespace Define.Helpers
{
    public static class DocumentHelpers
    {
        public static void HightlightInstancesOfTerm(string term, List<Range> list, bool shouldMatchCase, WdColor color)
        {
            foreach (var find in list.Select(range => range.Find))
            {
                find.ClearFormatting();
                find.HitHighlight(
                    FindText: term,
                    MatchCase: shouldMatchCase,
                    IgnorePunct: true,
                    HighlightColor: color);
            }
        }
       
        public static void ClearHightlightOnAllInstancesOfTerm(List<Range> list)
        {
            foreach (var find in list.Select(range => range.Find))
            {
                find.ClearHitHighlight();   
            }
        }

        public static void ClearHighlighting()
        {
            var find = Globals.ThisAddIn.Application.ActiveDocument.Content.Find;
            find.ClearHitHighlight();
        }

        public static IEnumerable<Range> GetSentences()
        {
            return Globals.ThisAddIn.Application.ActiveDocument.Sentences.Cast<Range>();
        }

        public static Range GetRangeFromDoc(int startPos, int endPos)
        {
            return Globals.ThisAddIn.Application.ActiveDocument.Range(startPos, endPos);
        }

        public static IEnumerable<Paragraph> GetParagraphs()
        {
            return Globals.ThisAddIn.Application.ActiveDocument.Paragraphs.Cast<Paragraph>();
        }

        public static IEnumerable<Table> GetTables()
        {
            return Globals.ThisAddIn.Application.ActiveDocument.Tables.Cast<Table>();
        }

        public static string FixSpaces(this string str)
        {
            var cleanedStr = new Regex("[ ]{2,}", RegexOptions.None).Replace(str, " ");
            return Regex.Replace(cleanedStr, "\u00A0", " ");
        }
    }
}
