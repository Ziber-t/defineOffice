namespace Define
{
    partial class StartRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public StartRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.ribbonButton = this.Factory.CreateRibbonGroup();
            this.showDefinitionBtn = this.Factory.CreateRibbonButton();
            this.ScanDocumentBtn = this.Factory.CreateRibbonButton();
            this.ReportBtn = this.Factory.CreateRibbonButton();
            this.showDefinitionOnSelectionCkb = this.Factory.CreateRibbonCheckBox();
            this.tab1.SuspendLayout();
            this.ribbonButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.ribbonButton);
            this.tab1.Label = "Define";
            this.tab1.Name = "tab1";
            // 
            // ribbonButton
            // 
            this.ribbonButton.Items.Add(this.showDefinitionBtn);
            this.ribbonButton.Items.Add(this.ScanDocumentBtn);
            this.ribbonButton.Items.Add(this.ReportBtn);
            this.ribbonButton.Items.Add(this.showDefinitionOnSelectionCkb);
            this.ribbonButton.Label = "Define";
            this.ribbonButton.Name = "ribbonButton";
            // 
            // showDefinitionBtn
            // 
            this.showDefinitionBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.showDefinitionBtn.Image = global::Define.Properties.Resources.logo2;
            this.showDefinitionBtn.Label = "Show Definition";
            this.showDefinitionBtn.Name = "showDefinitionBtn";
            this.showDefinitionBtn.ShowImage = true;
            this.showDefinitionBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ShowDefinitionBtn_Click);
            // 
            // ScanDocumentBtn
            // 
            this.ScanDocumentBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ScanDocumentBtn.Image = global::Define.Properties.Resources.reset;
            this.ScanDocumentBtn.Label = "Scan Document";
            this.ScanDocumentBtn.Name = "ScanDocumentBtn";
            this.ScanDocumentBtn.ShowImage = true;
            this.ScanDocumentBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ScanDocumentBtn_Click);
            // 
            // ReportBtn
            // 
            this.ReportBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ReportBtn.Image = global::Define.Properties.Resources.report;
            this.ReportBtn.Label = "Report";
            this.ReportBtn.Name = "ReportBtn";
            this.ReportBtn.ShowImage = true;
            this.ReportBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ReportBtn_Click);
            // 
            // showDefinitionOnSelectionCkb
            // 
            this.showDefinitionOnSelectionCkb.Label = "Show Definition after selection";
            this.showDefinitionOnSelectionCkb.Name = "showDefinitionOnSelectionCkb";
            this.showDefinitionOnSelectionCkb.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ShowDefinitionOnSelectionCkb_Click);
            // 
            // StartRibbon
            // 
            this.Name = "StartRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.StartRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.ribbonButton.ResumeLayout(false);
            this.ribbonButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup ribbonButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton showDefinitionBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ScanDocumentBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ReportBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox showDefinitionOnSelectionCkb;
    }

    partial class ThisRibbonCollection
    {
        internal StartRibbon StartRibbon
        {
            get { return this.GetRibbon<StartRibbon>(); }
        }
    }
}
