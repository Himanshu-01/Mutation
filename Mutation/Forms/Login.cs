using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Web;
using System.Net;
using System.IO;
using Global;

namespace Mutation
{
    public partial class Login : Form
    {
        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
        public Login()
        {
            InitializeComponent();
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            string UserName = textBox1.Text;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] Password = sha.ComputeHash(encoding.GetBytes(textBox2.Text));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://files.remnantmods.com/grimdoomer/Mutation/auth.php?username=" + UserName + "&password=" + textBox2.Text);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader input = new StreamReader(response.GetResponseStream());
            //Global.Globals.Response = input.ReadLine();
            //Global.Globals.UserName = UserName;

            this.Close();
        }
    }
}
