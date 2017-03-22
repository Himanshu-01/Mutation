using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaloControls
{
    public abstract class MetaControl : UserControl
    {
        #region Constructor

        public MetaControl()
        {
            // Set the control to be double buffered.
            this.DoubleBuffered = true;

            // Set the back color to transparent.
            this.BackColor = System.Drawing.Color.Transparent;

            // Initialize the index for this element.
            this.ElementIndex = -1;
        }

        #endregion

        #region DefaultWidth

        // The default width of the control.
        private int defaultWidth;

        // Event handler that gets raised when defaultWidth is changed.
        public delegate void DefaultWidthChangedHandler();
        public event DefaultWidthChangedHandler DefaultWidthChanged;

        /// <summary>
        /// Gets or sets the defualt width of the control. Used for 
        /// Meta editing views.
        /// </summary>
        public int DefaultWidth
        {
            get { return defaultWidth; }
            set 
            { 
                // Set the new value.
                defaultWidth = value;

                // Raise the event handler.
                OnDefaultWidthChanged();
            }
        }

        public bool InheritsDefaultWidthFromParent { get; set; }

        public void OnDefaultWidthChanged()
        {
            // Suspend any layout processing.
            this.SuspendLayout();

            // Adjust the width of the control.
            this.Size = new System.Drawing.Size(defaultWidth, this.Size.Height);

            // Loop through all child controls to see if they need updating.
            for (int i = 0; i < this.Controls.Count; i++)
            {
                // Check if they inherit MetaControl.
                if (this.Controls[i].GetType().BaseType == typeof(MetaControl))
                {
                    // Change their default width, which will trigger OnDefaultWidthChanged.
                    ((MetaControl)this.Controls[i]).DefaultWidth = this.defaultWidth;
                }
            }

            // Resume layout processing, and process any layout requests.
            this.ResumeLayout(true);

            // Check if the event has a valid handler.
            if (DefaultWidthChanged != null)
                DefaultWidthChanged();
        }

        #endregion

        #region ElementIndex

        /// <summary>
        /// Gets or sets the index of the MetaNode this control represents.
        /// </summary>
        public int ElementIndex { get; set; }

        #endregion

        #region Meta Functions

        /// <summary>
        /// Gives the control a new MetaNode object to bind to.
        /// </summary>
        /// <param name="node">The new MetaNode object to bind to.</param>
        public abstract void Update(HaloPlugins.Objects.MetaNode node);

        /// <summary>
        /// Saves any changes made to the control's data to the MetaNode object the
        /// control is binded to.
        /// </summary>
        /// <returns>True if the new data is in the correct format. False otherwise.</returns>
        public abstract bool SaveChanges();

        #endregion
    }
}
