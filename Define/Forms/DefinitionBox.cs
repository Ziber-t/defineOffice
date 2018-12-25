using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Define.Common.Extensions;
using Define.Common.Helpers;
using Define.Helpers;
using Define.Models;
using Define.Tokenizer;
using Define.Utilities;
using Microsoft.Office.Interop.Word;

namespace Define
{
    public partial class DefinitionBox : Form
    {
        private bool isEditMode = false;

        // Use the URL to test pattern: https://regex101.com/r/TvxpyD/4
        private const string ReferencePattern = @"(Clause|Schedule|Annex)\s(.*?)\s\((.*?)\)";

        public Range _currentPosition;
        public Range _copyCurrentRange;
        public WordDefinition Definition;
        public string SourceFile;
        public int NumberOfShownDefinition = 0;

        public ParagraphFormat _editingParagraphFormat;
        public ParagraphFormat _editingParagraphListFormat;

        private List<Range> _definitionOccurences;
        private List<Range> _corectDefinitionOccurences;
        private List<Range> _incorrectDefinitionOccurences;

        private int _viewPosition = -1;
        private int _incorrectDefinitionViewPosition = -1;
        private int _correctDefinitionViewPosition = -1;

        public DefinitionBox()
        {
            InitializeComponent();
            Text = @"Define - Definition";
            CenterToScreen();
        }
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
       
        #region onLoad Settings
        private void DefinitionBox_Load(object sender, EventArgs e)
        {
            ThisAddIn.CheckDialogDimensions(this.Width, this.Height);
            SetDesktopLocation(ThisAddIn.xCoordDialog, ThisAddIn.yCoordDialog);
            SetDefinitionBoxFieldsText();
            Init();

            ThisAddIn.IncreaseDialogCoords();
        }
        private void SetDefinitionOccurences()
        {
            _definitionOccurences = new List<Range>();
            _corectDefinitionOccurences = new List<Range>();
            _incorrectDefinitionOccurences = new List<Range>();
            GetAllSentencesWithDefinitions(Definition, _corectDefinitionOccurences, _incorrectDefinitionOccurences, _definitionOccurences);
        }
        private void SetDefinitionBoxFieldsText()
        {
            this.Text += $" of \"{Definition.Name}\"";
            DefinitionTerm.Text = Definition.Name;
            richTextBoxEx1.LinkClicked += richTextBoxEx1_LinkClicked;
            GetTextWithLinksFromRange(Definition.Locations[NumberOfShownDefinition]);
            SourceFileName.Text = SourceFile;
        }
        private void Init()
        {
            NextDefinition.Enabled = false;
            PreviousDefiniton.Enabled = false;
            ShowSubDefinition.Enabled = true;
        }
        #endregion

        private void NavigateToDefinition_Click(object sender, EventArgs e)
        {
            LogHelper.Info("Definition: Navigated to the definition");
            Navigate.ToRange(Definition.Locations[NumberOfShownDefinition]);
        }

        private void BackToPlaceButton_Click(object sender, EventArgs e)
        {
            Close();
            LogHelper.Info("Navigated back to position.");
        }
        
        public void GetTextWithLinksFromRange(Range range)
        {
            range.Copy();

            _editingParagraphFormat = range.ParagraphFormat.Duplicate;
            if (range.ListFormat.ListString.Length > 0)
            {
                _editingParagraphListFormat = range.Paragraphs.First.Format.Duplicate;
            }
            else
            {
                _editingParagraphListFormat = null;
            }

            richTextBoxEx1.Clear();
            richTextBoxEx1.Paste();
            var text = range.Text;

            foreach (Match match in Regex.Matches(text, ReferencePattern))
            {
                var link = match.Groups[0].Value;

                var pos = richTextBoxEx1.Find(link);
                if (pos > 0)
                {
                    richTextBoxEx1.Select(pos, link.Length);
                    richTextBoxEx1.InsertLink($"{link}");
                }
            }

            richTextBoxEx1.ReadOnly = true;
        }

        private void richTextBoxEx1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            WordHelpers.RunActionWithWaitCursor(() =>
            {
                var link = e.LinkText;
                var text = link;

                var match = Regex.Match(text, ReferencePattern);

                if (match.Groups.Count < 4)
                {
                    MessageBox.Show(new Form() { TopMost = true }, @"Such Clause or Schedule was not found");
                    return;
                }

                var title = match.Groups[3].Value;
                var listFormatValue = match.Groups[2].Value.Trim();
                listFormatValue = Regex.Replace(listFormatValue, @"\p{C}+", string.Empty);

                Document document = Globals.ThisAddIn.Application.ActiveDocument;
                Range rng = document.Content;

                rng.Find.ClearFormatting();
                rng.Find.Forward = true;
                rng.Find.Text = title;
                rng.Find.Execute();

                while (rng.Find.Found)
                {
                    var listFormatFromWord = rng.ListFormat.ListString.Trim('.');
                    listFormatFromWord = Regex.Replace(listFormatFromWord, @"\p{C}+", string.Empty);
                
                    if (listFormatFromWord.Equals(listFormatValue))
                    {
                        Navigate.ToRange(rng);
                        return;
                    }
                    rng.Find.Execute();
                }

                MessageBox.Show(new Form() { TopMost = true }, @"Such Clause or Schedule was not found");
            });
        }

        private void ShowSubDefinition_Click(object sender, EventArgs e)
        {
            WordHelpers.RunActionWithWaitCursor(() => 
            {
                var selection = StringExtensions.RefineSelectionText(richTextBoxEx1.SelectedText.Trim());
                var newDefinition = Globals.ThisAddIn.AnalyzersByDocument[Globals.ThisAddIn.Application.ActiveDocument.FullName].GetWordDefinition(selection);

                if (newDefinition == null)
                {
                    MessageBox.Show(new Form() { TopMost = true }, @"Definition could not be found");
                    return;
                }
                if (newDefinition.Locations.Count > 1)
                {
                    var listOfDefinitions = new ListOfDefinitions(newDefinition, Globals.ThisAddIn.Application.Selection.Range, Globals.ThisAddIn.Application.ActiveDocument.FullName);
                    listOfDefinitions.Show();
                }
                else
                {
                    var definitionBox = new DefinitionBox { Definition = newDefinition, SourceFile = Globals.ThisAddIn.Application.ActiveDocument.FullName, _currentPosition = Globals.ThisAddIn.Application.Selection.Range };
                    definitionBox.Show();
                }
            });

            LogHelper.Info("Nested definiton shown.");
        }

        #region NextDefenition

        private void NextDefinition_Click(object sender, EventArgs e)
        {
            if (HighlightIncorrectUsages.Checked && HighlightAllUsages.Checked)
            {
                GoToNextOccurencePosition(ref _viewPosition, _definitionOccurences);
            }
            else if (HighlightIncorrectUsages.Checked)
            {
                GoToNextOccurencePosition(ref _incorrectDefinitionViewPosition, _incorrectDefinitionOccurences);
            }
            else if (HighlightAllUsages.Checked)
            {
                GoToNextOccurencePosition(ref _correctDefinitionViewPosition, _corectDefinitionOccurences);
            }
        }

        private void GoToNextOccurencePosition(ref int position, List<Range> sentences)
        {
            LogHelper.Info("Navigated to the next occurence of definition.");

            if (position == 0)
            {
                PreviousDefiniton.Enabled = true;
            }
            if (position == sentences.Count - 2)
            {
                NextDefinition.Enabled = false;
            }

            position++;
            Navigate.ToRange(sentences.ElementAt(position));
        }

        #endregion

        #region PreviousDefenition
        private void PreviousDefiniton_Click(object sender, EventArgs e)
        {
            if (HighlightIncorrectUsages.Checked && HighlightAllUsages.Checked)
            {
                GoToPreviousOccurencePosition(ref _viewPosition, _definitionOccurences);
            }
            else if (HighlightIncorrectUsages.Checked)
            {
                GoToPreviousOccurencePosition(ref _incorrectDefinitionViewPosition, _incorrectDefinitionOccurences);
            }
            else if (HighlightAllUsages.Checked)
            {
                GoToPreviousOccurencePosition(ref _correctDefinitionViewPosition, _corectDefinitionOccurences);
            }
        }
        private void GoToPreviousOccurencePosition(ref int position, List<Range> sentences)
        {
            LogHelper.Info("Navigated to the next occurence of definition.");

            if (position == sentences.Count - 1)
            {
                NextDefinition.Enabled = true;
            }
            if (position == 1)
            {
                PreviousDefiniton.Enabled = false;
            }

            position--;
            Navigate.ToRange(sentences.ElementAt(position));
        }
        #endregion

        #region CheckBoxes
        private void HighlightIncorrectUsages_CheckedChanged(object sender, EventArgs e)
        {
            ResetTheCounters();
            var isFieldChecked = HighlightIncorrectUsages.Checked;

            if (isFieldChecked)
            {
                LogHelper.Info("Highlight incorrect usages of definition.");
                if (_incorrectDefinitionOccurences.Count != 0)
                {
                    HighLightIncorrectUsages();
                    NextDefinition.Enabled = true;
                }
                else
                {
                    if (HighlightAllUsages.Checked && _corectDefinitionOccurences.Count != 0)
                    {
                        NextDefinition.Enabled = true;
                    }
                }
                return;
            }

            if (!HighlightAllUsages.Checked)
            {
                NextDefinition.Enabled = false;
                PreviousDefiniton.Enabled = false;
            }
            
            LogHelper.Info("Clear highlights.");
            DocumentHelpers.ClearHightlightOnAllInstancesOfTerm(_incorrectDefinitionOccurences);
        }

        private void HighlightUsages_CheckedChanged(object sender, EventArgs e)
        {
            WordHelpers.RunActionWithWaitCursor(() =>
            {
                ResetTheCounters();
                var isFieldChecked = HighlightAllUsages.Checked;

                if (isFieldChecked)
                {
                    if (_corectDefinitionOccurences.Count != 0)
                    {
                        HighLightCorrectUsages();
                        NextDefinition.Enabled = true;
                    }
                    else
                    {
                        if (HighlightIncorrectUsages.Checked && _incorrectDefinitionOccurences.Count != 0)
                        {
                            NextDefinition.Enabled = true;
                        }
                    }
                    return;
                }

                if (!HighlightIncorrectUsages.Checked)
                {
                    NextDefinition.Enabled = false;
                    PreviousDefiniton.Enabled = false;
                }
                DocumentHelpers.ClearHightlightOnAllInstancesOfTerm(_corectDefinitionOccurences);
            });
        }
        #endregion

        private void ResetTheCounters()
        {
            _viewPosition = -1;
            _correctDefinitionViewPosition = -1;
            _incorrectDefinitionViewPosition = -1;
            PreviousDefiniton.Enabled = false;
        }
        private void HighLightCorrectUsages()
        {
            DocumentHelpers.HightlightInstancesOfTerm(DefinitionTerm.Text, _corectDefinitionOccurences, true, WdColor.wdColorAqua);
            DocumentHelpers.HightlightInstancesOfTerm(Definition.Plural, _corectDefinitionOccurences, true, WdColor.wdColorAqua);
        }

        private void HighLightIncorrectUsages()
        {
            DocumentHelpers.HightlightInstancesOfTerm(Definition.Singular, _incorrectDefinitionOccurences, false, WdColor.wdColorRed);
            DocumentHelpers.HightlightInstancesOfTerm(Definition.Plural, _incorrectDefinitionOccurences, false, WdColor.wdColorRed);
        }

        private void DefinitionBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            ThisAddIn.DecreaseDialogCoords();

            WordHelpers.RunActionWithWaitCursor(() =>
            {
                if (HighlightAllUsages.Checked)
                {
                    if (_corectDefinitionOccurences.Count != 0)
                        DocumentHelpers.ClearHightlightOnAllInstancesOfTerm(_corectDefinitionOccurences);
                }
                if (HighlightIncorrectUsages.Checked)
                {
                    if (_incorrectDefinitionOccurences.Count != 0)
                        DocumentHelpers.ClearHightlightOnAllInstancesOfTerm(_incorrectDefinitionOccurences);
                }
                if (_currentPosition != null)
                {
                    Navigate.ToRange(_currentPosition);
                }
            });
        }

        private void ShowOccurancesButton_Click(object sender, EventArgs e)
        {
            WordHelpers.RunActionWithWaitCursor(() => 
            {
                SetDefinitionOccurences();
                var amountOfCorrectOccurences = _corectDefinitionOccurences.Count;
                var amountOfIncorrectOccurences = _incorrectDefinitionOccurences.Count;
                if (amountOfCorrectOccurences > 0)
                {
                    HighlightAllUsages.Enabled = true;
                    HighlightAllUsages.Text = $"Highlight Correct Usages ({amountOfCorrectOccurences})";
                }
                if (amountOfIncorrectOccurences > 0)
                {
                    HighlightIncorrectUsages.Enabled = true;
                    HighlightIncorrectUsages.Text = $"Highlight Incorrect Usages ({amountOfIncorrectOccurences})";
                }
           
                CountLabel.Text = $"Occurrences: {_definitionOccurences.Count}";
            });
        }

        private void amendButton_Click(object sender, EventArgs e)
        {
            //int r = Definition.ListOfDefinition[NumberOfShownDefinition].Fields.Locked;
            //Definition.ListOfDefinition[NumberOfShownDefinition].Fields.Locked = 1;
            SwitchButtonsStatus(false);
        }

        private void SwitchButtonsStatus(bool status)
        {
            PreviousDefiniton.Visible = status;
            NextDefinition.Visible = status;
            BackToPlaceButton.Visible = status;
            HighlightAllUsages.Visible = status;
            HighlightIncorrectUsages.Visible = status;
            buttonShowOccurence.Visible = status;
            ShowSubDefinition.Visible = status;
            NavigateToDefinition.Visible = status;
            AmendButton.Visible = status;
            SaveAmendedDefinition.Visible = !status;
            isEditMode = !status;
            richTextBoxEx1.Clear();
            if (!status)
            {
                richTextBoxEx1.ReadOnly = false;

                Definition.Locations[NumberOfShownDefinition].Copy();

                try
                {
                    richTextBoxEx1.Paste();
                    var cells = Definition.Locations[NumberOfShownDefinition].Cells;
                    richTextBoxEx1.Paste();
                }
                catch (Exception e2) {
                }

                try {
                    var text = Definition.Locations[NumberOfShownDefinition].Text;
                    var forDelete = richTextBoxEx1.Find(text.Substring(0, 1));
                    richTextBoxEx1.Select(0, forDelete);
                    richTextBoxEx1.InsertLink("");
                } catch (Exception e) {
                }
                
            }
            else {
                GetTextWithLinksFromRange(Definition.Locations[NumberOfShownDefinition]);
                richTextBoxEx1.ReadOnly = true;
            }
            mainPanel.Height = 55;
        }

        private void SaveAmendedDefinition_Click(object sender, EventArgs e)
        {
            var newDefinition = Definition.Locations[NumberOfShownDefinition];
            richTextBoxEx1.SelectAll();
            richTextBoxEx1.Copy();
            newDefinition.Paste();

            newDefinition.ParagraphFormat = _editingParagraphFormat;
            if(_editingParagraphListFormat != null)
                newDefinition.Paragraphs.First.Format = _editingParagraphListFormat;
            
            //Globals.ThisAddIn.ApplicationDefinitionFinder.ActiveDocument = Globals.ThisAddIn.Application.ActiveDocument;
            SwitchButtonsStatus(true);
            DisplayAdoptivePanel();
        }

        private void FormResize_Event(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                mainPanel.Height = 55;
                return;
            }
            DisplayAdoptivePanel();
        }

        private void DisplayAdoptivePanel()
        {
            if (this.Width < 595)
            {
                mainPanel.Height = 110;
                var newLoc = Location;
                newLoc.X = 0;
                newLoc.Y = 56;
                leftPanel.Location = newLoc;
            }
            else
            {
                mainPanel.Height = 55;
                var newLoc = Location;
                newLoc.X = 332;
                newLoc.Y = 0;
                leftPanel.Location = newLoc;
            }
        }

        private void leftPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private static void GetAllSentencesWithDefinitions(WordDefinition definition, List<Range> listWithCorrectUsages, List<Range> listWithIncorrectUsages, List<Range> commonList)
        {
            foreach (var sentence in DocumentHelpers.GetSentences().ToArray())
            {
                var text = sentence.Text;

                if (text.Contains(definition.Singular, StringComparison.Ordinal) || text.Contains(definition.Plural, StringComparison.Ordinal))
                {
                    listWithCorrectUsages.Add(sentence);
                    commonList.Add(sentence);
                }
                else if (text.Contains(definition.Singular, StringComparison.OrdinalIgnoreCase) || text.Contains(definition.Plural, StringComparison.OrdinalIgnoreCase))
                {
                    listWithIncorrectUsages.Add(sentence);
                    commonList.Add(sentence);
                }
            }
        }
    }
}
