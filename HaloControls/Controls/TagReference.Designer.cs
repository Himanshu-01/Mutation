namespace HaloControls
{
    partial class TagReference
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
            this.label1 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnGoto = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "Null Reference";
            this.textEdit1.Location = new System.Drawing.Point(46, 5);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.textEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.textEdit1.Size = new System.Drawing.Size(230, 20);
            this.textEdit1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Image = global::HaloControls.Properties.Resources.Add;
            this.button1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.button1.Location = new System.Drawing.Point(282, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 24);
            this.button1.TabIndex = 6;
            this.button1.ToolTip = "Browse for a tag to link to";
            this.button1.ToolTipTitle = "Link Tag";
            this.button1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // btnGoto
            // 
            this.btnGoto.Image = global::HaloControls.Properties.Resources.Blow_Arrow_Right;
            this.btnGoto.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnGoto.Location = new System.Drawing.Point(316, 3);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(28, 24);
            this.btnGoto.TabIndex = 7;
            this.btnGoto.ToolTip = "Jumps to the tag in a new tag editor page";
            this.btnGoto.ToolTipTitle = "Jump To";
            this.btnGoto.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // TagReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGoto);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.label1);
            this.Name = "TagReference";
            this.Size = new System.Drawing.Size(500, 30);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraEditors.SimpleButton btnGoto;
    }
}
