namespace Mutation
{
    partial class NewProjectDialog
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
            this.wizardControl1 = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.label11 = new System.Windows.Forms.Label();
            this.ProjectTypePage = new DevExpress.XtraWizard.WizardPage();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage1 = new DevExpress.XtraWizard.WizardPage();
            this.ProjectSettings = new DevExpress.XtraWizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtMapImage1 = new DevExpress.XtraEditors.TextEdit();
            this.txtProjectName1 = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtProjectDir1 = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.checkButton2 = new DevExpress.XtraEditors.CheckButton();
            this.checkButton1 = new DevExpress.XtraEditors.CheckButton();
            this.DecompilingSettings = new DevExpress.XtraWizard.WizardPage();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMapLocation = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.checkButton3 = new DevExpress.XtraEditors.CheckButton();
            this.label5 = new System.Windows.Forms.Label();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.txtMapImage2 = new DevExpress.XtraEditors.TextEdit();
            this.txtMapName2 = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.txtProjectDir2 = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardControl1.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            this.completionWizardPage1.SuspendLayout();
            this.ProjectTypePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.ProjectSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapImage1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectDir1.Properties)).BeginInit();
            this.DecompilingSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapImage2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectDir2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Controls.Add(this.welcomeWizardPage1);
            this.wizardControl1.Controls.Add(this.completionWizardPage1);
            this.wizardControl1.Controls.Add(this.ProjectTypePage);
            this.wizardControl1.Controls.Add(this.wizardPage1);
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.ProjectTypePage,
            this.wizardPage1,
            this.completionWizardPage1});
            this.wizardControl1.Size = new System.Drawing.Size(539, 389);
            this.wizardControl1.Text = "New Project Wizard";
            this.wizardControl1.WizardStyle = DevExpress.XtraWizard.WizardStyle.WizardAero;
            this.wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_FinishClick);
            this.wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControl1_NextClick);
            this.wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(this.wizardControl1_CancelClick);
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.label10);
            this.welcomeWizardPage1.Controls.Add(this.label9);
            this.welcomeWizardPage1.IntroductionText = "This Wizard will guide you through creating a new map project.\r\n";
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(479, 226);
            this.welcomeWizardPage1.Text = "New Project Wizard";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(14, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "To continue, click Next";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(14, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(302, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "This Wizard will guide you through creating a new map project.";
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.Controls.Add(this.label11);
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.Size = new System.Drawing.Size(479, 226);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(16, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(204, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "To finish creating your project, click finish.";
            // 
            // ProjectTypePage
            // 
            this.ProjectTypePage.Controls.Add(this.radioGroup1);
            this.ProjectTypePage.Controls.Add(this.label1);
            this.ProjectTypePage.Name = "ProjectTypePage";
            this.ProjectTypePage.Size = new System.Drawing.Size(479, 226);
            this.ProjectTypePage.Text = "Project Type";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(13, 42);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Resource Map (Not Supported)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Empty Project (Not Supported)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Starter Project (Not Supported)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Decompile")});
            this.radioGroup1.Size = new System.Drawing.Size(180, 96);
            this.radioGroup1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "The Project Type determins how the project will be created.";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(479, 226);
            // 
            // ProjectSettings
            // 
            this.ProjectSettings.Controls.Add(this.label4);
            this.ProjectSettings.Controls.Add(this.simpleButton2);
            this.ProjectSettings.Controls.Add(this.txtMapImage1);
            this.ProjectSettings.Controls.Add(this.txtProjectName1);
            this.ProjectSettings.Controls.Add(this.label3);
            this.ProjectSettings.Controls.Add(this.simpleButton1);
            this.ProjectSettings.Controls.Add(this.txtProjectDir1);
            this.ProjectSettings.Controls.Add(this.label2);
            this.ProjectSettings.Controls.Add(this.checkButton2);
            this.ProjectSettings.Controls.Add(this.checkButton1);
            this.ProjectSettings.Name = "ProjectSettings";
            this.ProjectSettings.Size = new System.Drawing.Size(479, 227);
            this.ProjectSettings.Text = "Project Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Map Image:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(408, 132);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(59, 23);
            this.simpleButton2.TabIndex = 8;
            this.simpleButton2.Text = "Browse";
            // 
            // txtMapImage1
            // 
            this.txtMapImage1.Location = new System.Drawing.Point(81, 135);
            this.txtMapImage1.Name = "txtMapImage1";
            this.txtMapImage1.Size = new System.Drawing.Size(321, 20);
            this.txtMapImage1.TabIndex = 7;
            // 
            // txtProjectName1
            // 
            this.txtProjectName1.EditValue = "My Project";
            this.txtProjectName1.Location = new System.Drawing.Point(92, 161);
            this.txtProjectName1.Name = "txtProjectName1";
            this.txtProjectName1.Size = new System.Drawing.Size(310, 20);
            this.txtProjectName1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(12, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Project Name:";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(408, 106);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(59, 23);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Browse";
            // 
            // txtProjectDir1
            // 
            this.txtProjectDir1.Location = new System.Drawing.Point(106, 109);
            this.txtProjectDir1.Name = "txtProjectDir1";
            this.txtProjectDir1.Size = new System.Drawing.Size(296, 20);
            this.txtProjectDir1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Project Directory:";
            // 
            // checkButton2
            // 
            this.checkButton2.Checked = true;
            this.checkButton2.Location = new System.Drawing.Point(15, 45);
            this.checkButton2.Name = "checkButton2";
            this.checkButton2.Size = new System.Drawing.Size(133, 23);
            this.checkButton2.TabIndex = 1;
            this.checkButton2.Text = "Create Scenario";
            // 
            // checkButton1
            // 
            this.checkButton1.Checked = true;
            this.checkButton1.Location = new System.Drawing.Point(15, 16);
            this.checkButton1.Name = "checkButton1";
            this.checkButton1.Size = new System.Drawing.Size(133, 23);
            this.checkButton1.TabIndex = 0;
            this.checkButton1.Text = "Create Globals";
            // 
            // DecompilingSettings
            // 
            this.DecompilingSettings.Controls.Add(this.label8);
            this.DecompilingSettings.Controls.Add(this.txtMapLocation);
            this.DecompilingSettings.Controls.Add(this.simpleButton5);
            this.DecompilingSettings.Controls.Add(this.checkButton3);
            this.DecompilingSettings.Controls.Add(this.label5);
            this.DecompilingSettings.Controls.Add(this.simpleButton3);
            this.DecompilingSettings.Controls.Add(this.txtMapImage2);
            this.DecompilingSettings.Controls.Add(this.txtMapName2);
            this.DecompilingSettings.Controls.Add(this.label6);
            this.DecompilingSettings.Controls.Add(this.simpleButton4);
            this.DecompilingSettings.Controls.Add(this.txtProjectDir2);
            this.DecompilingSettings.Controls.Add(this.label7);
            this.DecompilingSettings.Name = "DecompilingSettings";
            this.DecompilingSettings.Size = new System.Drawing.Size(479, 227);
            this.DecompilingSettings.Text = "Decompiling Settings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(13, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Map Location:";
            // 
            // txtMapLocation
            // 
            this.txtMapLocation.Location = new System.Drawing.Point(94, 110);
            this.txtMapLocation.Name = "txtMapLocation";
            this.txtMapLocation.Size = new System.Drawing.Size(308, 20);
            this.txtMapLocation.TabIndex = 32;
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(408, 107);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(59, 23);
            this.simpleButton5.TabIndex = 31;
            this.simpleButton5.Text = "Browse";
            // 
            // checkButton3
            // 
            this.checkButton3.Location = new System.Drawing.Point(16, 18);
            this.checkButton3.Name = "checkButton3";
            this.checkButton3.Size = new System.Drawing.Size(113, 23);
            this.checkButton3.TabIndex = 30;
            this.checkButton3.Text = "Add to Library";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Map Image:";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(408, 159);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(59, 23);
            this.simpleButton3.TabIndex = 28;
            this.simpleButton3.Text = "Browse";
            // 
            // txtMapImage2
            // 
            this.txtMapImage2.Location = new System.Drawing.Point(81, 162);
            this.txtMapImage2.Name = "txtMapImage2";
            this.txtMapImage2.Size = new System.Drawing.Size(321, 20);
            this.txtMapImage2.TabIndex = 27;
            // 
            // txtMapName2
            // 
            this.txtMapName2.Location = new System.Drawing.Point(92, 188);
            this.txtMapName2.Name = "txtMapName2";
            this.txtMapName2.Size = new System.Drawing.Size(310, 20);
            this.txtMapName2.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(12, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Project Name:";
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(408, 133);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(59, 23);
            this.simpleButton4.TabIndex = 24;
            this.simpleButton4.Text = "Browse";
            // 
            // txtProjectDir2
            // 
            this.txtProjectDir2.Location = new System.Drawing.Point(106, 136);
            this.txtProjectDir2.Name = "txtProjectDir2";
            this.txtProjectDir2.Size = new System.Drawing.Size(296, 20);
            this.txtProjectDir2.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(12, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Project Directory:";
            // 
            // NewProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 389);
            this.Controls.Add(this.wizardControl1);
            this.Name = "NewProjectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewProjectDialog";
            this.Load += new System.EventHandler(this.NewProjectDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardControl1.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            this.welcomeWizardPage1.PerformLayout();
            this.completionWizardPage1.ResumeLayout(false);
            this.completionWizardPage1.PerformLayout();
            this.ProjectTypePage.ResumeLayout(false);
            this.ProjectTypePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ProjectSettings.ResumeLayout(false);
            this.ProjectSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapImage1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectDir1.Properties)).EndInit();
            this.DecompilingSettings.ResumeLayout(false);
            this.DecompilingSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapImage2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMapName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectDir2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControl1;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraWizard.WizardPage ProjectTypePage;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraWizard.WizardPage ProjectSettings;
        private DevExpress.XtraEditors.CheckButton checkButton2;
        private DevExpress.XtraEditors.CheckButton checkButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtProjectDir1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TextEdit txtMapImage1;
        private DevExpress.XtraEditors.TextEdit txtProjectName1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraWizard.WizardPage DecompilingSettings;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtMapLocation;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.CheckButton checkButton3;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.TextEdit txtMapImage2;
        private DevExpress.XtraEditors.TextEdit txtMapName2;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.TextEdit txtProjectDir2;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraWizard.WizardPage wizardPage1;
    }
}