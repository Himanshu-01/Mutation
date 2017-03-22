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
    public partial class Bitmask : MetaControl
    {
        protected HaloPlugins.Objects.Data.Flags bitmask = null;

        public Bitmask(HaloPlugins.Objects.Data.Flags bitmask)
        {
            // Initialize form controls.
            InitializeComponent();

            // Save our bitmask object.
            this.bitmask = bitmask;

            // Suspend the layout while we setup the controls.
            this.SuspendLayout();

            // Setup the name label.
            this.label1.Text = this.bitmask.Name;

            // Force the checklist box to autosize so it fits all the options.
            this.checkedListBox1.AutoSize = true;

            // Suspend the checklist box and add all the bitmask options to it.
            this.checkedListBox1.BeginUpdate();
            for (int i = 0; i < this.bitmask.Options.Length; i++)
            {
                // Check if the bit is unused.
                if (this.bitmask.Options[i] != "")
                    this.checkedListBox1.Items.Add(this.bitmask.Options[i].Replace("_", " "), this.bitmask[i]);
            }
            this.checkedListBox1.EndUpdate();

            // Resume the layout.
            this.ResumeLayout();
        }

        public override void Update(HaloPlugins.Objects.MetaNode node)
        {
            // Check to see if the new node is of the correct type.
            if (node.FieldType != HaloPlugins.Objects.FieldType.Flags)
                throw new Exception("Tried to bind an incompatible MetaNode object to Bitmask control!");

            // Bind to the new MetaNode object.
            this.bitmask = (HaloPlugins.Objects.Data.Flags)node;

            // Loop through the options for the bitmask, but keep a secondary
            // counter because the list options are different than the bitmask options.
            for (int i = 0, x = 0; i < this.bitmask.Options.Length; i++)
            {
                // Check if the bit is unused.
                if (this.bitmask.Options[i] != "")
                {
                    // Update the checked state of the option.
                    this.checkedListBox1.SetItemChecked(x, this.bitmask[i]);
                    x++;
                }
            }
        }

        public override bool SaveChanges()
        {
            // Loop through the options for the bitmask, but keep a secondary
            // counter because the list options are different than the bitmask options.
            for (int i = 0, x = 0; i < this.bitmask.Options.Length; i++)
            {
                // Check if the bit is unused.
                if (this.bitmask.Options[i] != "")
                {
                    // Update the state of the option.
                    this.bitmask[i] = this.checkedListBox1.GetItemChecked(x);
                    x++;
                }
            }

            // Changes saved successfully.
            return true;
        }
    }
}
