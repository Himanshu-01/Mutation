using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace HaloControls
{
    public partial class String : MetaControl
    {
        protected HaloPlugins.Objects.MetaNode node;

        public String(HaloPlugins.Objects.MetaNode node)
        {
            // Initialize form controls.
            InitializeComponent();

            // Set a custom OnDefaultWidthChanged handler.
            this.DefaultWidthChanged += new DefaultWidthChangedHandler(String_DefaultWidthChanged);

            // Save the MetaNode object.
            this.node = node;

            // Setup the name label.
            label1.Text = this.node.Name;

            // Check what type of string object this to set the max length for the textbox.
            if (this.node.FieldType == HaloPlugins.Objects.FieldType.ShortString)
                textBox1.MaxLength = 32;
            else if (this.node.FieldType == HaloPlugins.Objects.FieldType.LongString ||
                this.node.FieldType == HaloPlugins.Objects.FieldType.StringId)
                textBox1.MaxLength = 128;
            else if (this.node.FieldType == HaloPlugins.Objects.FieldType.Tag)
                textBox1.MaxLength = 4;
            else
            {
                // Not sure what type of string this is.
                throw new Exception(string.Format("Maximum length not set for unknown string type {0}!",
                    this.node.FieldType.ToString()));
            }

            // Setup the value of the textbox.
            textBox1.Text = this.node.GetValue(null).ToString();
        }

        void String_DefaultWidthChanged()
        {
            // Position and size the text box.
            int thirdWidth = (this.DefaultWidth / 3);
            textBox1.Location = new Point(thirdWidth, 5);
            textBox1.Size = new Size(this.DefaultWidth - (thirdWidth + 5), 20);
        }

        public override void Update(HaloPlugins.Objects.MetaNode node)
        {
            // Check to see if the new node is of the correct type.
            if (this.node.FieldType != HaloPlugins.Objects.FieldType.ShortString &&
                this.node.FieldType != HaloPlugins.Objects.FieldType.LongString &&
                this.node.FieldType != HaloPlugins.Objects.FieldType.StringId &&
                this.node.FieldType != HaloPlugins.Objects.FieldType.Tag)
                throw new Exception("Tried to bind an incompatible MetaNode object to String control!");

            // Bind to the new string object.
            this.node = node;

            // Update the value of the textbox.
            textBox1.Text = this.node.GetValue(null).ToString();
        }

        public override bool SaveChanges()
        {
            // Save the textbox value to the string node.
            this.node.SetValue(textBox1.Text, null);

            // Changes saved successfully.
            return true;
        }
    }
}
