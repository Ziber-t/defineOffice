using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Define.Helpers
{
    public static class WordHelpers
    {
        public static IEnumerable<Document> GetOpenDocuments()
        {
            return Globals.ThisAddIn.Application.Documents.Cast<Document>();
        }

        public static void RunActionWithWaitCursor(Action action)
        {
            Globals.ThisAddIn.Application.System.Cursor = WdCursorType.wdCursorWait;

            try
            {
                action();
            }
            finally
            {
                Globals.ThisAddIn.Application.System.Cursor = WdCursorType.wdCursorNormal;
            }
        }

        public static void RunActionWithDisablingScreenUpdating(Action action)
        {
            Globals.ThisAddIn.Application.ScreenUpdating = false;

            try
            {
                action();
            }
            finally
            {
                Globals.ThisAddIn.Application.ScreenUpdating = true;
            }
        }
    }
}
