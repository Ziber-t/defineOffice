using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Define.Common.Helpers;

namespace Define
{
    public partial class ReportBox : Form
    {
        public ReportBox()
        {
            InitializeComponent();
        }

        private void ReportBox_Load(object sender, EventArgs e)
        {
            LoadDefinitionsIntoDataGrid();
        }

        private void LoadDefinitionsIntoDataGrid()
        {
            var i = 0;
            var document = Globals.ThisAddIn.Application.ActiveDocument;
            var definitions = Globals.ThisAddIn.AnalyzersByDocument[document.FullName].GetAllWordDefinitions();
            var documentText = Globals.ThisAddIn.Application.ActiveDocument.Content.Text;

            foreach (var definition in definitions)
            {
                var count = 0;

                try
                {
                    var singularPattern = definition.Singular.Replace("(", @"\(").Replace(")", @"\)");
                    var pluralPattern = definition.Plural.Replace("(", @"\(").Replace(")", @"\)");

                    count = Regex.Matches(documentText, singularPattern, RegexOptions.IgnoreCase).Count;
                    count += Regex.Matches(documentText, pluralPattern, RegexOptions.IgnoreCase).Count;
                }
                catch (ArgumentException e)
                {
                    LogHelper.Warn($"Failed to find definition: {definition.Singular}/{definition.Plural} in the document");
                }

                var row = (DataGridViewRow) DataGrid.Rows[0].Clone();

                if (row != null)
                {
                    row.Cells[0].Value = (i + 1);
                    row.Cells[1].Value = definition.Singular;
                    row.Cells[1].ToolTipText = "Double-Click to inspect the definition";

                    row.Cells[2].Value = definition.Locations[0].Text;
                    row.Cells[3].Value = count.ToString();

                    row.DefaultCellStyle.BackColor = count <= 0 ? Color.DarkRed : Color.White;
                    row.DefaultCellStyle.ForeColor = count <= 0 ? Color.White : Color.Black;

                    DataGrid.Rows.Add(row);
                }
                i++;
            }
        }

        private void DataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LogHelper.Info("Report Box: Definiton Clicked");

            if (e.ColumnIndex != 1) return;

            var selection = DataGrid.CurrentCell.Value.ToString();

            if (string.IsNullOrWhiteSpace(selection)) return;

            var document = Globals.ThisAddIn.Application.ActiveDocument;

            var definition = Globals.ThisAddIn.AnalyzersByDocument[document.FullName].GetWordDefinition(selection);

            if (definition == null)
            {
                MessageBox.Show(new Form() { TopMost = true }, @"Definition could not be found");
                return;
            }

            var definitionBox = new DefinitionBox {Definition = definition, SourceFile = document.FullName };

            definitionBox.Show();
        }
    }
}
