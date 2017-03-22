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
    public partial class Value : MetaControl
    {
        protected HaloPlugins.Objects.Data.Value value;

        public Value(HaloPlugins.Objects.Data.Value value)
        {
            // Initialize the form.
            InitializeComponent();

            // Set a custom OnDefaultWidthChanged handler.
            this.DefaultWidthChanged += new DefaultWidthChangedHandler(Value_DefaultWidthChanged);

            // Save the value object.
            this.value = value;

            // Setup the tootip.
            this.lblName.Text = this.value.Name;
            this.toolTipController1.SetTitle(lblName, this.value.Name);

            // Set the correct data type for the tooltip.
            string type = "";
            switch (this.value.DataType.ToString())
            {
                case "System.Int16":    type = "Short";     break;
                case "System.UInt16":   type = "UShort";    break;
                case "System.Int32":    type = "Int";       break;
                case "System.UInt32":   type = "UInt";      break;
                case "System.Single":   type = "Float";     break;
                default:
                    {
                        // Unknown data type.
                        throw new Exception(string.Format("Unknown data type {0} passed to Value control!", 
                            this.value.DataType.ToString()));
                        break;
                    }
            }

            // Format the string.
            this.toolTipController1.SetToolTip(this.lblName, string.Format("Type: {0}", type));

            // Set the textbox text to the data value.
            this.txtValue.Text = this.value.DataValue.ToString();
        }

        void Value_DefaultWidthChanged()
        {
            // Position the text box.
            int thirdWidth = (this.DefaultWidth / 3);
            this.txtValue.Location = new Point(thirdWidth, 5);

            // Set the text box size according to the byte size of the value type.
            this.txtValue.Size = new Size(this.value.FieldSize * 30, 20);
        }

        public override void Update(HaloPlugins.Objects.MetaNode node)
        {
            // Check to see if the new node is of the correct type.
            if (node.FieldType != HaloPlugins.Objects.FieldType.Value)
                throw new Exception("Tried to bind an incompatible MetaNode object to Value control!");

            // Bind to the new Value object.
            this.value = (HaloPlugins.Objects.Data.Value)node;

            // Update the value in the textbox.
            this.txtValue.Text = this.value.DataValue.ToString();
        }

        public override bool SaveChanges()
        {
            // Update the data value of the Value obeject.
            this.value.SetValue(this.txtValue.Text, null);

            // Changes saved successfully.
            return true;
        }
    }
}
