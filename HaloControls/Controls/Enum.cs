using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using System.Reflection;

namespace HaloControls
{
    public partial class Enum : MetaControl
    {
        protected HaloPlugins.Objects.Data.Enum enumerator = null;

        public Enum(HaloPlugins.Objects.Data.Enum enumerator)
        {
            // Initialize form controls.
            InitializeComponent();

            // Set a custom OnDefaultWidthChanged handler.
            this.DefaultWidthChanged += new DefaultWidthChangedHandler(Enum_DefaultWidthChanged);

            // Save our enum object.
            this.enumerator = enumerator;

            // Suspend the layout while we setup the controls.
            this.SuspendLayout();

            // Setup the name label.
            label1.Text = this.enumerator.Name;

            /*
             * We need to create a custom drop down menu, that
             * handles scrolling and looks nice and shit.
             */

            // Create a new drop down menu for the enum options list.
            DXPopupMenu menu = new DXPopupMenu();            

            // Loop through the enum options and add them all to the drop down menu.
            menu.Items.BeginUpdate();
            for (int i = 0; i < this.enumerator.Options.Length; i++)
            {
                // Check if this option is unused.
                if (this.enumerator.Options[i] != "")
                {
                    // Note: We need to make sure itemIndex is actually the index of the menu item, and not some other bs value.
                    int itemIndex = menu.Items.Add(new DXMenuItem(this.enumerator.Options[i].Replace("_", " ").Replace("OP", ""), Enum_Click));
                    menu.Items[itemIndex].Tag = i;
                }
            }
            menu.Items.EndUpdate();

            // Set the combo box to use the drop down menu we just created.
            dropDownButton1.DropDownControl = menu;

            // Check to see if the select enum option is within range of the options list.
            if (this.enumerator.Value < this.enumerator.Options.Length)
            {
                // Set the text of the drop down button to the select option name.
                this.dropDownButton1.Text = this.enumerator.Options[this.enumerator.Value].Replace("_", " ").Replace("OP", "");
                this.dropDownButton1.Tag = this.enumerator.Value;
            }
            else
            {
                // Set the drop down button selection to default values.
                this.dropDownButton1.Text = "";
                this.dropDownButton1.Tag = this.enumerator.Value;
            }
        }

        void Enum_DefaultWidthChanged()
        {
            // Position and size the drop down box.
            int thirdWidth = (this.DefaultWidth / 3);
            dropDownButton1.Location = new Point(thirdWidth, 5);
        }

        void Enum_Click(object sender, EventArgs e)
        {
            // Get the drop down menu from the sender object.
            DXMenuItem item = (DXMenuItem)sender;

            // Update the combo box text with the option name.
            this.dropDownButton1.Text = item.Caption;
            this.dropDownButton1.Tag = item.Tag;
        }

        public override void Update(HaloPlugins.Objects.MetaNode node)
        {
            // Check to see if the new node is of the correct type.
            if (node.FieldType != HaloPlugins.Objects.FieldType.Enum)
                throw new Exception("Tried to bind an incompatible MetaNode object to Enum control!");

            // Bind to the new MetaNode object.
            this.enumerator = (HaloPlugins.Objects.Data.Enum)node;

            // Get the drop down menu control from the combo box button.
            DXPopupMenu menu = (DXPopupMenu)dropDownButton1.DropDownControl;

            // Check to see if the select enum option is within range of the options list.
            if (this.enumerator.Value < this.enumerator.Options.Length)
            {
                // Set the text of the drop down button to the select option name.
                this.dropDownButton1.Text = this.enumerator.Options[this.enumerator.Value].Replace("_", " ").Replace("OP", "");
                this.dropDownButton1.Tag = this.enumerator.Value;
            }
            else
            {
                // Set the drop down button selection to default values.
                this.dropDownButton1.Text = "";
                this.dropDownButton1.Tag = this.enumerator.Value;
            }
        }

        public override bool SaveChanges()
        {
            // Get the drop down menu control from the combo box button.
            DXPopupMenu menu = (DXPopupMenu)dropDownButton1.DropDownControl;

            // Save the selected index to the enum object.
            this.enumerator.Value = (int)this.dropDownButton1.Tag;

            // Changes saved successfully.
            return true;
        }
    }
}
