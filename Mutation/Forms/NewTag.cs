using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mutation
{
    public partial class NewTag : Form
    {
        string ret = "NONE";
        bool exit = false;

        public NewTag()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ret = comboBox1.Text;
            exit = true;
        }

        public string GetTag()
        {
            while (!exit)
            {
                Application.DoEvents();
                if (ret != "NONE")
                {
                    break;
                }
            }
            return ret;
        }

        private void NewTag_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
        }
    }
}
