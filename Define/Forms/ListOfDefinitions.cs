using System;
using System.Windows.Forms;
using Define.Models;
using Define.Utilities;
using Microsoft.Office.Interop.Word;

namespace Define
{
    public partial class ListOfDefinitions : Form
    {
        private readonly Range _callPlace;
        private readonly WordDefinition _definition;
        private readonly string _sourceFile;

        public ListOfDefinitions(WordDefinition definition, Range callPlace, string sourceFile)
        {
            _callPlace = callPlace;
            _definition = definition;
            _sourceFile = sourceFile;

            InitializeComponent();

            FormClosing += ListOfDefinitions_FormClosing;
        }

        private void ListOfDefinitions_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_callPlace != null)
            {
                Navigate.ToRange(_callPlace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenDefinitionBox();
        }

        private void refresh_SizeChanged(object sender, EventArgs e)
        {
           multiLineListBox1.Refresh();
        }

        private void OpenDefinitionBox() {
            var selectedIndex = multiLineListBox1.SelectedIndex;
            var definitionBox = new DefinitionBox { Definition = _definition, SourceFile = _sourceFile, NumberOfShownDefinition = selectedIndex, _currentPosition = _callPlace };
            definitionBox.Show();

        }

        private void OnDoubleClickItem(object sender, EventArgs e)
        {
            OpenDefinitionBox();
        }

        private void ListOfDefinitions_Load(object sender, EventArgs e)
        {
            foreach (var elem in _definition.Locations)
            {
                var definitionText = elem.Text;
                if (definitionText.Length > 100)
                {
                    definitionText = definitionText.Substring(0, 100);
                    definitionText += "...";
                }
                multiLineListBox1.Items.Add($"Document: {_sourceFile}\n{definitionText}");
            }
            multiLineListBox1.SelectedIndex = 0;
            label1.Text = _definition.Singular;


            ThisAddIn.CheckDialogDimensions(this.Width, this.Height);
            SetDesktopLocation(ThisAddIn.xCoordDialog, ThisAddIn.yCoordDialog);
            ThisAddIn.IncreaseDialogCoords();
        }

        private void ListOfDefinitions_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThisAddIn.DecreaseDialogCoords();
        }

        /*private void OnFormClosed(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //OpenDefinitionBox();
        }*/
    }
}
