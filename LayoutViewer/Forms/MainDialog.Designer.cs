namespace LayoutViewer.Forms
{
    partial class MainDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTagGroupBasic = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTagGroupAdvanced = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvTagGroups = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tvTagDefs = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tvMutation = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtGuerillaCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.txtMutationCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGuerillaCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMutationCode)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1080, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(104, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagGroupToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // tagGroupToolStripMenuItem
            // 
            this.tagGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTagGroupBasic,
            this.btnTagGroupAdvanced});
            this.tagGroupToolStripMenuItem.Name = "tagGroupToolStripMenuItem";
            this.tagGroupToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.tagGroupToolStripMenuItem.Text = "Tag Groups";
            // 
            // btnTagGroupBasic
            // 
            this.btnTagGroupBasic.Checked = true;
            this.btnTagGroupBasic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnTagGroupBasic.Name = "btnTagGroupBasic";
            this.btnTagGroupBasic.Size = new System.Drawing.Size(127, 22);
            this.btnTagGroupBasic.Text = "Basic";
            this.btnTagGroupBasic.Click += new System.EventHandler(this.btnTagGroupBasic_Click);
            // 
            // btnTagGroupAdvanced
            // 
            this.btnTagGroupAdvanced.Name = "btnTagGroupAdvanced";
            this.btnTagGroupAdvanced.Size = new System.Drawing.Size(127, 22);
            this.btnTagGroupAdvanced.Text = "Advanced";
            this.btnTagGroupAdvanced.Click += new System.EventHandler(this.btnTagGroupAdvanced_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1080, 639);
            this.splitContainer1.SplitterDistance = 324;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(324, 639);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvTagGroups);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(316, 613);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tag Groups";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvTagGroups
            // 
            this.tvTagGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvTagGroups.HideSelection = false;
            this.tvTagGroups.Location = new System.Drawing.Point(-4, 0);
            this.tvTagGroups.Name = "tvTagGroups";
            this.tvTagGroups.Size = new System.Drawing.Size(320, 592);
            this.tvTagGroups.TabIndex = 0;
            this.tvTagGroups.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTagGroups_AfterSelect);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tvTagDefs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(316, 613);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tag Block Definitions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tvTagDefs
            // 
            this.tvTagDefs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvTagDefs.HideSelection = false;
            this.tvTagDefs.Location = new System.Drawing.Point(-4, 0);
            this.tvTagDefs.Name = "tvTagDefs";
            this.tvTagDefs.Size = new System.Drawing.Size(320, 592);
            this.tvTagDefs.TabIndex = 0;
            this.tvTagDefs.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTagDefs_AfterSelect);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tvMutation);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(316, 613);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Mutation";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tvMutation
            // 
            this.tvMutation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvMutation.HideSelection = false;
            this.tvMutation.Location = new System.Drawing.Point(-4, 0);
            this.tvMutation.Name = "tvMutation";
            this.tvMutation.Size = new System.Drawing.Size(320, 592);
            this.tvMutation.TabIndex = 1;
            this.tvMutation.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMutation_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtGuerillaCode);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtMutationCode);
            this.splitContainer2.Size = new System.Drawing.Size(752, 639);
            this.splitContainer2.SplitterDistance = 372;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtGuerillaCode
            // 
            this.txtGuerillaCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGuerillaCode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtGuerillaCode.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.txtGuerillaCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtGuerillaCode.BackBrush = null;
            this.txtGuerillaCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.txtGuerillaCode.CharHeight = 14;
            this.txtGuerillaCode.CharWidth = 8;
            this.txtGuerillaCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGuerillaCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtGuerillaCode.IsReplaceMode = false;
            this.txtGuerillaCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtGuerillaCode.LeftBracket = '(';
            this.txtGuerillaCode.LeftBracket2 = '{';
            this.txtGuerillaCode.Location = new System.Drawing.Point(3, 3);
            this.txtGuerillaCode.Name = "txtGuerillaCode";
            this.txtGuerillaCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtGuerillaCode.ReadOnly = true;
            this.txtGuerillaCode.RightBracket = ')';
            this.txtGuerillaCode.RightBracket2 = '}';
            this.txtGuerillaCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtGuerillaCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtGuerillaCode.ServiceColors")));
            this.txtGuerillaCode.Size = new System.Drawing.Size(366, 611);
            this.txtGuerillaCode.TabIndex = 0;
            this.txtGuerillaCode.Zoom = 100;
            // 
            // txtMutationCode
            // 
            this.txtMutationCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMutationCode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtMutationCode.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.txtMutationCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtMutationCode.BackBrush = null;
            this.txtMutationCode.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.txtMutationCode.CharHeight = 14;
            this.txtMutationCode.CharWidth = 8;
            this.txtMutationCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMutationCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtMutationCode.IsReplaceMode = false;
            this.txtMutationCode.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtMutationCode.LeftBracket = '(';
            this.txtMutationCode.LeftBracket2 = '{';
            this.txtMutationCode.Location = new System.Drawing.Point(3, 3);
            this.txtMutationCode.Name = "txtMutationCode";
            this.txtMutationCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtMutationCode.ReadOnly = true;
            this.txtMutationCode.RightBracket = ')';
            this.txtMutationCode.RightBracket2 = '}';
            this.txtMutationCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtMutationCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtMutationCode.ServiceColors")));
            this.txtMutationCode.Size = new System.Drawing.Size(366, 611);
            this.txtMutationCode.TabIndex = 1;
            this.txtMutationCode.Zoom = 100;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 641);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1080, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(26, 17);
            this.lblStatus.Text = "Idle";
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 663);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Layout Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainDialog_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGuerillaCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMutationCode)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvTagGroups;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView tvTagDefs;
        private FastColoredTextBoxNS.FastColoredTextBox txtGuerillaCode;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnTagGroupBasic;
        private System.Windows.Forms.ToolStripMenuItem btnTagGroupAdvanced;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView tvMutation;
        private FastColoredTextBoxNS.FastColoredTextBox txtMutationCode;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}