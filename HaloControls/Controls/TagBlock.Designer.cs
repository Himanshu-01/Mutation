namespace HaloControls
{
    partial class TagBlock
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.blockHeaderPanel = new DevExpress.XtraEditors.PanelControl();
            this.btnDeleteAllChunks = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteChunk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopyAllChunks = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopyChunk = new DevExpress.XtraEditors.SimpleButton();
            this.btnInsertChunk = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddChunk = new DevExpress.XtraEditors.SimpleButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.blockContentPanel = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.blockHeaderPanel)).BeginInit();
            this.blockHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockContentPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // blockHeaderPanel
            // 
            this.blockHeaderPanel.Controls.Add(this.btnDeleteAllChunks);
            this.blockHeaderPanel.Controls.Add(this.btnDeleteChunk);
            this.blockHeaderPanel.Controls.Add(this.btnCopyAllChunks);
            this.blockHeaderPanel.Controls.Add(this.btnCopyChunk);
            this.blockHeaderPanel.Controls.Add(this.btnInsertChunk);
            this.blockHeaderPanel.Controls.Add(this.btnAddChunk);
            this.blockHeaderPanel.Controls.Add(this.comboBox1);
            this.blockHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.blockHeaderPanel.Location = new System.Drawing.Point(2, 21);
            this.blockHeaderPanel.Name = "blockHeaderPanel";
            this.blockHeaderPanel.Size = new System.Drawing.Size(604, 34);
            this.blockHeaderPanel.TabIndex = 9;
            // 
            // btnDeleteAllChunks
            // 
            this.btnDeleteAllChunks.Image = global::HaloControls.Properties.Resources.Database_RemoveAll;
            this.btnDeleteAllChunks.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDeleteAllChunks.Location = new System.Drawing.Point(368, 5);
            this.btnDeleteAllChunks.Name = "btnDeleteAllChunks";
            this.btnDeleteAllChunks.Size = new System.Drawing.Size(28, 23);
            this.btnDeleteAllChunks.TabIndex = 15;
            this.btnDeleteAllChunks.ToolTip = "Deletes all of the blocks";
            this.btnDeleteAllChunks.ToolTipTitle = "Remove All Blocks";
            this.btnDeleteAllChunks.Click += new System.EventHandler(this.btnDeleteAllChunks_Click);
            // 
            // btnDeleteChunk
            // 
            this.btnDeleteChunk.Image = global::HaloControls.Properties.Resources.Database_Remove;
            this.btnDeleteChunk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDeleteChunk.Location = new System.Drawing.Point(334, 5);
            this.btnDeleteChunk.Name = "btnDeleteChunk";
            this.btnDeleteChunk.Size = new System.Drawing.Size(28, 23);
            this.btnDeleteChunk.TabIndex = 14;
            this.btnDeleteChunk.ToolTip = "Deletes the selected block";
            this.btnDeleteChunk.ToolTipTitle = "Remove Selected Block";
            this.btnDeleteChunk.Click += new System.EventHandler(this.btnDeleteChunk_Click);
            // 
            // btnCopyAllChunks
            // 
            this.btnCopyAllChunks.Image = global::HaloControls.Properties.Resources.Database_CopyAll;
            this.btnCopyAllChunks.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCopyAllChunks.Location = new System.Drawing.Point(300, 5);
            this.btnCopyAllChunks.Name = "btnCopyAllChunks";
            this.btnCopyAllChunks.Size = new System.Drawing.Size(28, 23);
            this.btnCopyAllChunks.TabIndex = 13;
            this.btnCopyAllChunks.ToolTip = "Copies all of the blocks";
            this.btnCopyAllChunks.ToolTipTitle = "Copy All Blocks";
            this.btnCopyAllChunks.Click += new System.EventHandler(this.btnCopyAllChunks_Click);
            // 
            // btnCopyChunk
            // 
            this.btnCopyChunk.Image = global::HaloControls.Properties.Resources.Database_Copy;
            this.btnCopyChunk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCopyChunk.Location = new System.Drawing.Point(266, 5);
            this.btnCopyChunk.Name = "btnCopyChunk";
            this.btnCopyChunk.Size = new System.Drawing.Size(28, 23);
            this.btnCopyChunk.TabIndex = 12;
            this.btnCopyChunk.ToolTip = "Copies the selected block";
            this.btnCopyChunk.ToolTipTitle = "Copy Selected Block";
            this.btnCopyChunk.Click += new System.EventHandler(this.btnCopyChunk_Click);
            // 
            // btnInsertChunk
            // 
            this.btnInsertChunk.Image = global::HaloControls.Properties.Resources.Database_Insert;
            this.btnInsertChunk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnInsertChunk.Location = new System.Drawing.Point(232, 6);
            this.btnInsertChunk.Name = "btnInsertChunk";
            this.btnInsertChunk.Size = new System.Drawing.Size(28, 23);
            this.btnInsertChunk.TabIndex = 11;
            this.btnInsertChunk.ToolTip = "Inserts copied block(s)";
            this.btnInsertChunk.ToolTipTitle = "Insert Block(s)";
            this.btnInsertChunk.Click += new System.EventHandler(this.btnInsertChunk_Click);
            // 
            // btnAddChunk
            // 
            this.btnAddChunk.Image = global::HaloControls.Properties.Resources.Database_Add;
            this.btnAddChunk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAddChunk.Location = new System.Drawing.Point(198, 5);
            this.btnAddChunk.Name = "btnAddChunk";
            this.btnAddChunk.Size = new System.Drawing.Size(28, 23);
            this.btnAddChunk.TabIndex = 10;
            this.btnAddChunk.ToolTip = "Adds a new block";
            this.btnAddChunk.ToolTipTitle = "Add New Block";
            this.btnAddChunk.Click += new System.EventHandler(this.btnAddChunk_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(187, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnExpand
            // 
            this.btnExpand.Image = global::HaloControls.Properties.Resources.Export;
            this.btnExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnExpand.Location = new System.Drawing.Point(582, 3);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(18, 12);
            this.btnExpand.TabIndex = 16;
            this.btnExpand.ToolTip = "Changes the expanded state of the tag block";
            this.btnExpand.ToolTipTitle = "Expand Tag Block";
            this.btnExpand.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.AutoSize = true;
            this.groupControl1.Controls.Add(this.btnExpand);
            this.groupControl1.Controls.Add(this.blockContentPanel);
            this.groupControl1.Controls.Add(this.blockHeaderPanel);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(608, 61);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "groupControl1";
            // 
            // blockContentPanel
            // 
            this.blockContentPanel.AutoSize = true;
            this.blockContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockContentPanel.Location = new System.Drawing.Point(2, 55);
            this.blockContentPanel.Name = "blockContentPanel";
            this.blockContentPanel.Size = new System.Drawing.Size(604, 4);
            this.blockContentPanel.TabIndex = 10;
            // 
            // groupControl2
            // 
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(2, 63);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(608, 20);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "groupControl2";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(616, 89);
            this.panelControl1.TabIndex = 12;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl2);
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(612, 85);
            this.panelControl2.TabIndex = 12;
            // 
            // TagBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panelControl1);
            this.Name = "TagBlock";
            this.Size = new System.Drawing.Size(616, 89);
            this.Load += new System.EventHandler(this.TagBlock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.blockHeaderPanel)).EndInit();
            this.blockHeaderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockContentPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl blockHeaderPanel;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.SimpleButton btnAddChunk;
        private DevExpress.XtraEditors.SimpleButton btnCopyChunk;
        private DevExpress.XtraEditors.SimpleButton btnInsertChunk;
        private DevExpress.XtraEditors.SimpleButton btnDeleteChunk;
        private DevExpress.XtraEditors.SimpleButton btnCopyAllChunks;
        private DevExpress.XtraEditors.SimpleButton btnDeleteAllChunks;
        private DevExpress.XtraEditors.SimpleButton btnExpand;
        private DevExpress.XtraEditors.PanelControl blockContentPanel;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}
