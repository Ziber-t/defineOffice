using Microsoft.Office.Interop.Word;

namespace Define.Utilities
{
    public static class Navigate
    {
        public static void ToRange(Range range)
        {
            Globals.ThisAddIn.ShouldIgnoreNextSelection = Globals.ThisAddIn.FlagOfSelection;
            range.Select();
        }
    }
}
