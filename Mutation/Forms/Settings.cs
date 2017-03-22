using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress;
using DevExpress.XtraBars.Ribbon;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Docking;
using System.Xml;
using System.IO;

namespace Mutation
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\Settings.txt");
                txtAlias.Text = sr.ReadLine();
                textEdit1.Text = sr.ReadLine();
                textEdit2.Text = sr.ReadLine();
                textEdit3.Text = sr.ReadLine();
                sr.Close();
            }
            catch { }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Settings.txt");
            sw.WriteLine(txtAlias.Text);
            sw.WriteLine(textEdit1.Text);
            sw.WriteLine(textEdit2.Text);
            sw.WriteLine(textEdit3.Text);
            sw.Close();

            //Global.User.Alias = txtAlias.Text;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "mainmenu.map (mainmenu.map)|mainmenu.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textEdit1.Text = ofd.FileName;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "shared.map (shared.map)|shared.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textEdit2.Text = ofd.FileName;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "single_player_shared.map (single_player_shared.map)|single_player_shared.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textEdit3.Text = ofd.FileName;
            }
        }
    }
}
