using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using System.Net;

namespace Mutation
{
    public partial class Feedback : Form
    {
        int FeedbackType = 0;

        public Feedback(int FeedbackType)
        {
            InitializeComponent();
            this.FeedbackType = FeedbackType;
        }

        private void Feedback_Load(object sender, EventArgs e)
        {
            // Add a Menu to the combobox
            DXPopupMenu menu = new DXPopupMenu();
            menu.Items.Add(new DXMenuItem("Comment", menu_click));
            menu.Items.Add(new DXMenuItem("Bug", menu_click));
            dropDownButton1.DropDownControl = menu;
            dropDownButton1.Text = menu.Items[FeedbackType].Caption;

            // Alias
            txtAlias.Text = Global.Application.Instance.UserAccount.UserName;
        }

        void menu_click(object sender, EventArgs e)
        {
            DXMenuItem item = (DXMenuItem)sender;
            dropDownButton1.Text = item.Caption;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string Username = txtAlias.Text;
            string Message = "%0D%0A" + DateTime.Now.ToString().Replace(" ", "%20") + "%20" + dropDownButton1.Text + ":%20" + memoEdit1.Text.Replace(" ", "%20");
            WebRequest request = WebRequest.Create("http://files.remnantmods.com/grimdoomer/Mutation/report.php?username=" + Username + "&content=" + Message);
            request.Timeout = 10000;
            try { request.GetResponse(); }
            catch
            {
                MessageBox.Show(dropDownButton1.Text + " report timed out! Please try again in a few minutes.");
                return;
            }

            MessageBox.Show(dropDownButton1.Text + " Submited!");
            this.Close();
        }
    }
}
