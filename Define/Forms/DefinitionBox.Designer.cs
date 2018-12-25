namespace Define
{
    partial class DefinitionBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NavigateToDefinition = new System.Windows.Forms.Button();
            this.BackToPlaceButton = new System.Windows.Forms.Button();
            this.CountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HighlightAllUsages = new System.Windows.Forms.CheckBox();
            this.ShowSubDefinition = new System.Windows.Forms.Button();
            this.PreviousDefiniton = new System.Windows.Forms.Button();
            this.NextDefinition = new System.Windows.Forms.Button();
            this.SourceFileName = new System.Windows.Forms.Label();
            this.HighlightIncorrectUsages = new System.Windows.Forms.CheckBox();
            this.DefinitionTerm = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.buttonShowOccurence = new System.Windows.Forms.Button();
            this.SaveAmendedDefinition = new System.Windows.Forms.Button();
            this.AmendButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBoxEx1 = new Define.Controls.RichTextBoxEx();
            this.mainPanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // NavigateToDefinition
            // 
            this.NavigateToDefinition.Location = new System.Drawing.Point(169, 9);
            this.NavigateToDefinition.Margin = new System.Windows.Forms.Padding(2);
            this.NavigateToDefinition.Name = "NavigateToDefinition";
            this.NavigateToDefinition.Size = new System.Drawing.Size(85, 34);
            this.NavigateToDefinition.TabIndex = 3;
            this.NavigateToDefinition.Text = "Navigate To Definition";
            this.NavigateToDefinition.UseVisualStyleBackColor = true;
            this.NavigateToDefinition.Click += new System.EventHandler(this.NavigateToDefinition_Click);
            // 
            // BackToPlaceButton
            // 
            this.BackToPlaceButton.Location = new System.Drawing.Point(31, 11);
            this.BackToPlaceButton.Margin = new System.Windows.Forms.Padding(2);
            this.BackToPlaceButton.Name = "BackToPlaceButton";
            this.BackToPlaceButton.Size = new System.Drawing.Size(46, 33);
            this.BackToPlaceButton.TabIndex = 4;
            this.BackToPlaceButton.Text = "Done";
            this.BackToPlaceButton.UseVisualStyleBackColor = true;
            this.BackToPlaceButton.Click += new System.EventHandler(this.BackToPlaceButton_Click);
            // 
            // CountLabel
            // 
            this.CountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CountLabel.Location = new System.Drawing.Point(506, 8);
            this.CountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.CountLabel.Size = new System.Drawing.Size(113, 15);
            this.CountLabel.TabIndex = 5;
            this.CountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(590, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 6;
            // 
            // HighlightAllUsages
            // 
            this.HighlightAllUsages.AutoSize = true;
            this.HighlightAllUsages.Enabled = false;
            this.HighlightAllUsages.Location = new System.Drawing.Point(140, 11);
            this.HighlightAllUsages.Margin = new System.Windows.Forms.Padding(2);
            this.HighlightAllUsages.Name = "HighlightAllUsages";
            this.HighlightAllUsages.Size = new System.Drawing.Size(143, 17);
            this.HighlightAllUsages.TabIndex = 7;
            this.HighlightAllUsages.Text = "Highlight Correct Usages";
            this.HighlightAllUsages.UseVisualStyleBackColor = true;
            this.HighlightAllUsages.CheckedChanged += new System.EventHandler(this.HighlightUsages_CheckedChanged);
            // 
            // ShowSubDefinition
            // 
            this.ShowSubDefinition.Enabled = false;
            this.ShowSubDefinition.Location = new System.Drawing.Point(76, 9);
            this.ShowSubDefinition.Margin = new System.Windows.Forms.Padding(2);
            this.ShowSubDefinition.Name = "ShowSubDefinition";
            this.ShowSubDefinition.Size = new System.Drawing.Size(92, 34);
            this.ShowSubDefinition.TabIndex = 8;
            this.ShowSubDefinition.Text = "Show Nested Definition";
            this.ShowSubDefinition.UseVisualStyleBackColor = true;
            this.ShowSubDefinition.Click += new System.EventHandler(this.ShowSubDefinition_Click);
            // 
            // PreviousDefiniton
            // 
            this.PreviousDefiniton.Enabled = false;
            this.PreviousDefiniton.Location = new System.Drawing.Point(2, 11);
            this.PreviousDefiniton.Margin = new System.Windows.Forms.Padding(2);
            this.PreviousDefiniton.Name = "PreviousDefiniton";
            this.PreviousDefiniton.Size = new System.Drawing.Size(29, 33);
            this.PreviousDefiniton.TabIndex = 9;
            this.PreviousDefiniton.Text = "<<";
            this.PreviousDefiniton.UseVisualStyleBackColor = true;
            this.PreviousDefiniton.Click += new System.EventHandler(this.PreviousDefiniton_Click);
            // 
            // NextDefinition
            // 
            this.NextDefinition.Enabled = false;
            this.NextDefinition.Location = new System.Drawing.Point(255, 9);
            this.NextDefinition.Margin = new System.Windows.Forms.Padding(2);
            this.NextDefinition.Name = "NextDefinition";
            this.NextDefinition.Size = new System.Drawing.Size(29, 34);
            this.NextDefinition.TabIndex = 10;
            this.NextDefinition.Text = ">>";
            this.NextDefinition.UseVisualStyleBackColor = true;
            this.NextDefinition.Click += new System.EventHandler(this.NextDefinition_Click);
            // 
            // SourceFileName
            // 
            this.SourceFileName.AutoSize = true;
            this.SourceFileName.Location = new System.Drawing.Point(2, 9);
            this.SourceFileName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SourceFileName.Name = "SourceFileName";
            this.SourceFileName.Size = new System.Drawing.Size(91, 13);
            this.SourceFileName.TabIndex = 11;
            this.SourceFileName.Text = "Source File Name";
            // 
            // HighlightIncorrectUsages
            // 
            this.HighlightIncorrectUsages.AutoSize = true;
            this.HighlightIncorrectUsages.Enabled = false;
            this.HighlightIncorrectUsages.Location = new System.Drawing.Point(140, 28);
            this.HighlightIncorrectUsages.Margin = new System.Windows.Forms.Padding(2);
            this.HighlightIncorrectUsages.Name = "HighlightIncorrectUsages";
            this.HighlightIncorrectUsages.Size = new System.Drawing.Size(151, 17);
            this.HighlightIncorrectUsages.TabIndex = 12;
            this.HighlightIncorrectUsages.Text = "Highlight Incorrect Usages";
            this.HighlightIncorrectUsages.UseVisualStyleBackColor = true;
            this.HighlightIncorrectUsages.CheckedChanged += new System.EventHandler(this.HighlightIncorrectUsages_CheckedChanged);
            // 
            // DefinitionTerm
            // 
            this.DefinitionTerm.AutoSize = true;
            this.DefinitionTerm.Enabled = false;
            this.DefinitionTerm.Location = new System.Drawing.Point(273, 9);
            this.DefinitionTerm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DefinitionTerm.Name = "DefinitionTerm";
            this.DefinitionTerm.Size = new System.Drawing.Size(35, 13);
            this.DefinitionTerm.TabIndex = 13;
            this.DefinitionTerm.Text = "label1";
            this.DefinitionTerm.Visible = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.leftPanel);
            this.mainPanel.Controls.Add(this.SaveAmendedDefinition);
            this.mainPanel.Controls.Add(this.AmendButton);
            this.mainPanel.Controls.Add(this.PreviousDefiniton);
            this.mainPanel.Controls.Add(this.HighlightIncorrectUsages);
            this.mainPanel.Controls.Add(this.BackToPlaceButton);
            this.mainPanel.Controls.Add(this.HighlightAllUsages);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainPanel.Location = new System.Drawing.Point(0, 203);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(619, 55);
            this.mainPanel.TabIndex = 14;
            this.mainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPanel_Paint);
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.buttonShowOccurence);
            this.leftPanel.Controls.Add(this.ShowSubDefinition);
            this.leftPanel.Controls.Add(this.NextDefinition);
            this.leftPanel.Controls.Add(this.NavigateToDefinition);
            this.leftPanel.Location = new System.Drawing.Point(332, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(285, 55);
            this.leftPanel.TabIndex = 18;
            this.leftPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.leftPanel_Paint);
            // 
            // buttonShowOccurence
            // 
            this.buttonShowOccurence.Location = new System.Drawing.Point(3, 9);
            this.buttonShowOccurence.Name = "buttonShowOccurence";
            this.buttonShowOccurence.Size = new System.Drawing.Size(71, 34);
            this.buttonShowOccurence.TabIndex = 13;
            this.buttonShowOccurence.Text = "Show Occurrences";
            this.buttonShowOccurence.UseVisualStyleBackColor = true;
            this.buttonShowOccurence.Click += new System.EventHandler(this.ShowOccurancesButton_Click);
            // 
            // SaveAmendedDefinition
            // 
            this.SaveAmendedDefinition.Location = new System.Drawing.Point(5, 11);
            this.SaveAmendedDefinition.Name = "SaveAmendedDefinition";
            this.SaveAmendedDefinition.Size = new System.Drawing.Size(167, 34);
            this.SaveAmendedDefinition.TabIndex = 15;
            this.SaveAmendedDefinition.Text = "Save";
            this.SaveAmendedDefinition.UseVisualStyleBackColor = true;
            this.SaveAmendedDefinition.Visible = false;
            this.SaveAmendedDefinition.Click += new System.EventHandler(this.SaveAmendedDefinition_Click);
            // 
            // AmendButton
            // 
            this.AmendButton.Location = new System.Drawing.Point(77, 11);
            this.AmendButton.Name = "AmendButton";
            this.AmendButton.Size = new System.Drawing.Size(59, 34);
            this.AmendButton.TabIndex = 14;
            this.AmendButton.Text = "Amend";
            this.AmendButton.UseVisualStyleBackColor = true;
            this.AmendButton.Click += new System.EventHandler(this.amendButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CountLabel);
            this.panel2.Controls.Add(this.SourceFileName);
            this.panel2.Controls.Add(this.DefinitionTerm);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(619, 33);
            this.panel2.TabIndex = 15;
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEx1.Location = new System.Drawing.Point(0, 33);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.Size = new System.Drawing.Size(619, 170);
            this.richTextBoxEx1.TabIndex = 17;
            this.richTextBoxEx1.Text = "";
            // 
            // DefinitionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 258);
            this.Controls.Add(this.richTextBoxEx1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(308, 280);
            this.Name = "DefinitionBox";
            this.ShowIcon = false;
            this.Text = "Legal Analysis: Definition";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DefinitionBox_FormClosing);
            this.Load += new System.EventHandler(this.DefinitionBox_Load);
            this.Resize += new System.EventHandler(this.FormResize_Event);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.leftPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button NavigateToDefinition;
        private System.Windows.Forms.Button BackToPlaceButton;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox HighlightAllUsages;
        private System.Windows.Forms.Button ShowSubDefinition;
        private System.Windows.Forms.Button PreviousDefiniton;
        private System.Windows.Forms.Button NextDefinition;
        private System.Windows.Forms.Label SourceFileName;
        private System.Windows.Forms.CheckBox HighlightIncorrectUsages;
        private System.Windows.Forms.Label DefinitionTerm;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel panel2;
        private Controls.RichTextBoxEx richTextBoxEx1;
        private System.Windows.Forms.Button buttonShowOccurence;
        private System.Windows.Forms.Button AmendButton;
        private System.Windows.Forms.Button SaveAmendedDefinition;
        private System.Windows.Forms.Panel leftPanel;
    }
}