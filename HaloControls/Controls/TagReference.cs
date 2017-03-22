using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress;
using DevExpress.XtraTab;
using System.Reflection;
using System.IO;
using Global;
using HaloPlugins;
using IO;

namespace HaloControls
{
    public partial class TagReference : MetaControl
    {
        string Reference;
        public int Index;

        public TagReference(string Description, string Reference, int Index)
        {
            InitializeComponent();
            label1.Text = Description;
            this.Index = Index;
            this.Reference = Reference;

            // Set a custom OnDefaultWidthChanged handler.
            this.DefaultWidthChanged += new DefaultWidthChangedHandler(TagReference_DefaultWidthChanged);

            // Set textbox values
            textEdit1.Text = Reference;
            textEdit1.ForeColor = Reference != "Null Reference" ? Color.RoyalBlue : Color.Red;
        }

        void TagReference_DefaultWidthChanged()
        {
            // Position the goto button.
            btnGoto.Location = new Point(this.DefaultWidth - (btnGoto.Size.Width + 5), 3);

            // Position the change reference button.
            button1.Location = new Point(this.btnGoto.Location.X - (button1.Size.Width + 5), 3);

            // Size up and position the reference box.
            int thirdWidth = (this.DefaultWidth / 3);
            int width = (this.button1.Location.X - 5) - thirdWidth;
            textEdit1.Size = new Size(width, 20);
            textEdit1.Location = new Point(thirdWidth, 5);
        }

        public override void Update(HaloPlugins.Objects.MetaNode node)
        {
            throw new NotImplementedException();
        }

        public override bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Reload(object Value)
        {
            Reference = (string)Value;
            textEdit1.Text = Reference;
            textEdit1.ForeColor = Reference != "Null Reference" ? Color.RoyalBlue : Color.Red;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Halo 2 Tags (*.*)|*.*";
            if (Global.Application.Instance.IsProjectOpen != false)
                ofd.InitialDirectory = Global.Application.Instance.Project.TagFolder;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Open tag
                EndianReader br = new EndianReader(IO.Endianness.Little, new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read));

                // Get Ext
                string ext = ofd.FileName.Substring(ofd.FileName.LastIndexOf(".") + 1);
                TagDefinition tagdef = EngineManager.Engines[Global.Application.Instance.Project.Engine].CreateInstance(ext);

                // Read
                tagdef.Read(br);

                // Close
                br.Close();

                // See if tag belongs to this project
                if (!Global.Application.Instance.Project.Tags.Contains(tagdef.AbsolutePath))
                {
                    // Ask to add
                    if (MessageBox.Show("Mutation has detected this tag does not belong to the opened project.\r\nWould you like to add it to the project?", "Add tag to project", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Add
                        MessageBox.Show("This feature has not yet been enabled in Mutation.\r\nPlease wait for a new build.", "Future feature");
                        return;
                    }
                }
                else
                {
                    Reference = tagdef.AbsolutePath;
                    this.textEdit1.Text = Reference;
                    textEdit1.ForeColor = Color.RoyalBlue;
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            // Check if it Exists
            if (Global.Application.Instance.IsProjectOpen != false)
            {
                string Path = Global.Application.Instance.Project.TagFolder + textEdit1.Text;
                if (File.Exists(Path))
                    Global.EventHandler.OpenTag(Path);
            }
        }

        public object Save()
        {
            return textEdit1.Text;
        }
    }
}
