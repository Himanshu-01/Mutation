namespace Mutation
{
    partial class NewTag
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "(<fx>) Sound Effect Template",
            "(adlg) AI Dialogue Globals",
            "(ant!) Antenna",
            "(bipd) Biped",
            "(bitm) Bitmap",
            "(bloc) Crate",
            "(bsdt) Breakable Surface",
            "(char) Character",
            "(clwd) Cloth",
            "(coll) Collision Model",
            "(colo) Color Table",
            "(cont) Contrail",
            "(crea) Creature",
            "(ctrl) Control",
            "(deca) Decal",
            "(DECR) Decorator",
            "(effe) Effect",
            "(egor) Screen Effect",
            "(eqip) Equipment",
            "(fog) Fog",
            "(foot) Material Effect",
            "(fpch) Fog Path",
            "(garb) Garbage",
            "(gldf) Global Lighting",
            "(goof) Multiplayer Variant Settings",
            "(hlmt) Object Properties",
            "(hudg) HUD Globals",
            "(itmc) Item Collection",
            "(jmad) Model Animation Graph",
            "(jpt!) Damage Effect",
            "(lens) Lens Flare",
            "(ligh) Light",
            "(lsnd) Looping Sound",
            "(ltmp) Lightmap",
            "(mach) Machine",
            "(matg) Match Globals",
            "(mdlg) AI Mission Dialogue",
            "(MGS2) Light Volume",
            "(mode) Render Model",
            "(mulg) Multiplayer Globals",
            "(nhdt) HUD Interface",
            "(phmo) Physics Model",
            "(phys) Physics",
            "(pmov) Particle Physics",
            "(pphy) Point Physics",
            "(proj) Projectile",
            "(prt3) Particle",
            "(PRTM) Particle Model",
            "(sbsp) Scenario Structure Bsp",
            "(scen) Scenery",
            "(scnr) Scenario",
            "(sfx+) Sound Effect Collection",
            "(shad) Shader",
            "(sily) UI Option",
            "(skin) User Interface List Skin Definition",
            "(sky) Sky",
            "(sncl) Sound Class",
            "(snd!) Sound",
            "(snde) Sound Environment",
            "(snmx) Sound Mixture",
            "(spas) Shader Pass",
            "(spk!) Speak",
            "(ssce) Sound Scenery",
            "(stem) Shader Template",
            "(styl) Style",
            "(tdlt) Beam Trail",
            "(trak) Camera Track",
            "(udlg) Unit Dialog",
            "(ugh!) Sound Diagnostics",
            "(unic) Unicode String List",
            "(vehc) Vehicle Collection",
            "(vehi) Vehicle",
            "(vrtx) Vertex Shader",
            "(weap) Weapon",
            "(weat) Weather System",
            "(wgit) User Interface Screen Widget Definition",
            "(wgtz) User Interface Globals Definition",
            "(wigl) User Interface Shared Globals Definition"});
            this.comboBox1.Location = new System.Drawing.Point(56, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(255, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.comboBox1);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(350, 85);
            this.panelControl1.TabIndex = 5;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(89, 49);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.button1_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(191, 49);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "Ok";
            this.simpleButton1.Click += new System.EventHandler(this.button2_Click);
            // 
            // NewTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 85);
            this.Controls.Add(this.panelControl1);
            this.Name = "NewTag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Tag Wizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewTag_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}