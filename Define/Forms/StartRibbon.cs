using System;
using System.Windows.Forms;
using Define.Common.Extensions;
using Define.Common.Helpers;
using Define.Helpers;
using Microsoft.Office.Tools.Ribbon;

namespace Define
{
    public partial class StartRibbon
    {
        private void StartRibbon_Load(object sender, RibbonUIEventArgs e)
        {
        }

        private void ShowDefinitionBtn_Click(object sender, RibbonControlEventArgs e)
        {
            LogHelper.Info($"Showing definition");

            WordHelpers.RunActionWithWaitCursor(() =>
            {
                var activeDocument = Globals.ThisAddIn.Application.ActiveDocument;

                LogHelper.Debug($"Getting selected text...");
                var selectedText = Globals.ThisAddIn.Application.Selection.Text.RefineSelectionText();

                LogHelper.Debug($"Selected text: {selectedText}");

                if (string.IsNullOrWhiteSpace(selectedText)) return;

                var definition = Globals.ThisAddIn.AnalyzersByDocument[activeDocument.FullName].GetWordDefinition(selectedText);

                // TODO Leave here commented, we will re-implement this as a final fallback
                //if (string.IsNullOrWhiteSpace(definition.Text))
                //{
                //    definition = FindFirstInstanceOfTerm(selection);
                //}

                if (definition == null)
                {
                    MessageBox.Show(new Form() { TopMost = true }, $"Cannot find the definition '{selectedText}'");
                    return;
                }
                if (definition.Locations.Count > 1)
                {
                    var listOfDefinitions = new ListOfDefinitions(definition, Globals.ThisAddIn.Application.Selection.Range, activeDocument.FullName);
                    listOfDefinitions.Show();
                }
                else
                {
                    LogHelper.Info("Borders: start=" + definition.Locations[0].Start + " ; end=" + definition.Locations[0].End);
                    var definitionBox = new DefinitionBox { Definition = definition, SourceFile = activeDocument.FullName, _currentPosition = Globals.ThisAddIn.Application.Selection.Range };
                    definitionBox.Show();
                }
            });
        }

        private void ScanDocumentBtn_Click(object sender, RibbonControlEventArgs e)
        {
            LogHelper.Info("Scanning document...");

            WordHelpers.RunActionWithWaitCursor(() =>
                WordHelpers.RunActionWithDisablingScreenUpdating(() =>
                {
                    var activeDocument = Globals.ThisAddIn.Application.ActiveDocument;

                    var analyzer = Globals.ThisAddIn.AnalyzersByDocument[activeDocument.FullName];

                    try
                    {
                        analyzer.Analyze();

                    }
                    catch (Exception ex)
                    {
                        LogHelper.Fatal("Failed to analyze doc.", ex);
                    }
                })
            );
        }

        private void ReportBtn_Click(object sender, RibbonControlEventArgs e)
        {
            LogHelper.Info("Report generation requested");

            WordHelpers.RunActionWithWaitCursor(() => 
            {
                new ReportBox().Show();
            });

            LogHelper.Info("Report generation complete");
        }

        private void ShowDefinitionOnSelectionCkb_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.FlagOfSelection = ((RibbonCheckBox)sender).Checked;
        }
    }
}