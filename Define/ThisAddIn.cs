using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using Define.Helpers;
using Define.Utilities;
using System.Threading;
using System.Collections.Generic;
using Define.Common.Helpers;
using Define.Common.Extensions;
using log4net.Config;

namespace Define
{
    public partial class ThisAddIn
    {
        public IDictionary<string, DocumentAnalyzer> AnalyzersByDocument { get; } = new Dictionary<string, DocumentAnalyzer>();

        public bool FlagOfSelection;

        public bool ShouldIgnoreNextSelection;

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion

        #region Event handlers

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            XmlConfigurator.Configure();

            LogHelper.Info("Starting Add-In...");
            LogHelper.Info($"Add-In version: {VersionHelper.GetAddInVersion()}");
            LogHelper.Info($"Word version: {VersionHelper.GetWordVersion()}");

            // setup event handlers
            Application.WindowSelectionChange += ApplicationOnWindowSelectionChange;
            Application.DocumentChange += ApplicationOnDocumentChange;
            Application.DocumentBeforeClose += Application_DocumentBeforeClose;
            System.Windows.Forms.Application.ThreadException += Application_ThreadExceptionHandler;
            AppDomain.CurrentDomain.UnhandledException += Application_UnhandledException;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            LogHelper.Debug("Shutting down Add-In...");

            Application.WindowSelectionChange -= ApplicationOnWindowSelectionChange;
            Application.DocumentChange -= ApplicationOnDocumentChange;
            Application.DocumentBeforeClose -= Application_DocumentBeforeClose;
            System.Windows.Forms.Application.ThreadException -= Application_ThreadExceptionHandler;
            AppDomain.CurrentDomain.UnhandledException -= Application_UnhandledException;
        }

        private void Application_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogHelper.Error("Unhandled exception:", (Exception)e.ExceptionObject);
        }

        private void Application_ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            LogHelper.Error("Thread exception handler:", e.Exception);
        }

        private void ApplicationOnWindowSelectionChange(Selection sel)
        {
            if (Globals.ThisAddIn.ShouldIgnoreNextSelection)
            {
                Globals.ThisAddIn.ShouldIgnoreNextSelection = false;
                return;
            }
            if (Globals.ThisAddIn.FlagOfSelection)
            {
                WordHelpers.RunActionWithWaitCursor(() =>
                {
                    var selectionRange = Globals.ThisAddIn.Application.Selection.Range;
                    var selection = selectionRange.Text.RefineSelectionText();
                    LogHelper.Info(string.Format("Find Definition Request for '{0}'", selection));

                    if (string.IsNullOrWhiteSpace(selection))
                    {
                        return;
                    }
                    var start = DateTime.Now;
                    var activeDocument = Globals.ThisAddIn.Application.ActiveDocument;
                    var definition = AnalyzersByDocument[activeDocument.FullName].GetWordDefinition(selection);

                    if (definition == null)
                    {
                        MessageBox.Show(new Form() { TopMost = true }, @"Definition could not be found");
                        return;
                    }
                    var finish = (DateTime.Now - start);
                    LogHelper.Info($"Word \"{selection}\" was found in {finish.TotalSeconds} seconds");
                    if (definition.Locations.Count > 1)
                    {
                        var listOfDefinitions = new ListOfDefinitions(definition, selectionRange, activeDocument.FullName);
                        listOfDefinitions.Show();
                    }
                    else
                    {
                        var definitionBox = new DefinitionBox { Definition = definition, SourceFile = activeDocument.FullName, _currentPosition = selectionRange };
                        definitionBox.Show();
                    }
                });
            }
        }

        private void ApplicationOnDocumentChange()
        {
            try
            {
                if (Application.Documents.Count==0) return;

                LogHelper.Info($"Active document changed to '{Application.ActiveDocument.Name}'");

                if (!AnalyzersByDocument.ContainsKey(Application.ActiveDocument.FullName))
                    AnalyzersByDocument[Application.ActiveDocument.FullName] = new DocumentAnalyzer(Application.ActiveDocument);
            }
            catch (COMException e)
            {
                LogHelper.Error("Failed to handle document change event", e);
            }
        }

        private void Application_DocumentBeforeClose(Document document, ref bool cancel)
        {
            LogHelper.Info($"Closing document {document.FullName}");
            
            if (AnalyzersByDocument.ContainsKey(document.FullName))
            {
                AnalyzersByDocument.Remove(document.FullName);
            }
        }

        #endregion

        #region Dialog position adjustments

        public static int xCoordDialog = 100;
        public static int yCoordDialog = 80;

        private static int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        private static int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

        public static void IncreaseDialogCoords()
        {
            xCoordDialog += 100;
            yCoordDialog += 80;
        }

        public static void DecreaseDialogCoords()
        {
            xCoordDialog -= 100;
            yCoordDialog -= 80;

            if (xCoordDialog < 100)
                xCoordDialog = 100;
            if (yCoordDialog < 80)
                yCoordDialog = 80;
        }

        public static void CheckDialogDimensions(int width, int height)
        {
            if (xCoordDialog + width > ScreenWidth)
            {
                xCoordDialog = 100;
            }
            if (yCoordDialog + height > ScreenHeight)
            {
                yCoordDialog = 80;
            }
        }

        #endregion
    }
}
