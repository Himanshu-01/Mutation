using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using HaloPlugins;
using IO;
using System.IO;
using Global;
using HaloControls.Controls;

namespace HaloControls
{
    public class TagEditorPage : XtraTabPage
    {
        //private bool saved = true;
        ///// <summary>
        ///// Gets the Saved Status of the Tab
        ///// </summary>
        //public bool Saved { get { return saved; } }

        private TagDefinition plugin;
        /// <summary>
        /// Gets the Plugin the Tab is using
        /// </summary>
        public TagDefinition Plugin { get { return plugin; } }

        private string path = "";
        /// <summary>
        /// Gets the Location of the Tag
        /// </summary>
        public string Path { get { return path; } }

        // Space in between each control.
        private const int CONTROL_SPACING = 5;

        private bool isPanelLoaded = false;

        public TagEditorPage()
        {
            // Set some basic properties.
            this.AutoScroll = true;
            this.DoubleBuffered = true;

            // Create a new Xtra PanelControl to be our content panel.
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.AutoSize = true;
            panel.AutoScroll = true;
            panel.BackColor = Color.FromArgb(255, 250, 250, 250);

            // Add to the tab page
            this.Controls.Add(panel);
        }

        //public TagEditorPage(string Class)
        //{
        //    //Suspend Layout
        //    this.SuspendLayout();

        //    // Set Fields
        //    saved = false;

        //    // Load tagdef
        //    plugin = EngineManager.Engines[Global.Application.Instance.Project.Engine].CreateInstance(Class);

        //    // Create Panel
        //    Panel panel = new Panel();
        //    panel.SuspendLayout();
        //    panel.Dock = DockStyle.Fill;
        //    panel.AutoScroll = true;
        //    panel.AutoSize = true;
        //    panel.BackColor = Color.FromKnownColor(KnownColor.Control);

        //    // Load into panel
        //    LoadIntoPanel();

        //    // Update Tab
        //    this.Text = "New_Tag." + plugin.CompiledClass + "*";
        //    panel.ResumeLayout();
        //    this.Controls.Add(panel);

        //    //Resume
        //    this.ResumeLayout();
        //}

        public void Open(string Location)
        {
            // Open the tag
            this.path = Location;
            EndianReader br = new EndianReader(Endianness.Little, new FileStream(Location, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite));

            // Create tag def
            string Ext = Location.Substring(Location.LastIndexOf(".") + 1);
            plugin = EngineManager.Engines[Global.Application.Instance.Project.Engine].CreateInstance(Ext);

            // Read
            plugin.Read(br);

            // Close
            br.Close();

            // Load into panel
            LoadIntoPanel();

            // Set the tab name.
            this.Text = Location.Substring(Location.LastIndexOf("\\") + 1);
        }

        void LoadIntoPanel()
        {
#if (GUI_BENCHMARK)

            // Create a stop watch so we can time the opperation.
            Stopwatch watch = new Stopwatch();
            watch.Start();

#endif

            // Suspend the layout of the tab page.
            this.Controls[0].Visible = false;
            this.SuspendLayout();

            // Compute the default width and coordinates for child controls.
            int width = (this.Width / 2);
            int x = (this.Width / 4);
            int y = 5;

            // Create a group meta panel for all non-tag_block controls.
            MetaGroupPanel panel = new MetaGroupPanel();

            // Create placement coords for the meta panel.
            int panelx = 1;
            int panely = 2;

            // Loop through all the fields in the tag layout.
            for (int i = 0; i < plugin.Fields.Count; i++)
            {
                // Create the control for the meta node based on the field type.
                MetaControl control = CreateControl(plugin[i]);
                if (control == null)
                    continue;

                // Check if we are adding a normal field or a tag_block.
                if (plugin[i].FieldType == HaloPlugins.Objects.FieldType.TagBlock)
                {
                    // Check to see if there are any child controls in the group panel.
                    if (panel.Controls.Count > 0)
                    {
                        // Add the group panel to the tab.
                        this.Controls[0].Controls.Add(panel);

                        // Set the position and default width of the meta panel.
                        panel.DefaultWidth = width;
                        panel.Location = new Point(x, y);

                        // Adjust the y coordinate to compensate for the new control.
                        y += CONTROL_SPACING + panel.Size.Height;

                        // Create a new group meta panel.
                        panel = new MetaGroupPanel();

                        // Reset placement coords.
                        panelx = 1;
                        panely = 2;
                    }

                    // Add the field to the controls list.
                    // NOTE: We need to do this before adjusting the size or the real
                    // height of the control will not be computed.
                    this.Controls[0].Controls.Add(control);

                    // Set the position and default width of the control.
                    control.DefaultWidth = width;
                    control.Location = new Point(x, y);

                    // Adjust the y coordinate to compensate for the new control.
                    y += CONTROL_SPACING + control.Size.Height;
                }
                else
                {
                    // Add the field to the controls list.
                    // NOTE: We need to do this before adjusting the size or the real
                    // height of the control will not be computed.
                    panel.Controls.Add(control);

                    // Set the position of the control.
                    control.Location = new Point(panelx, panely);

                    // Set the field to inherit it's DefaultWidth from the parent control.
                    control.InheritsDefaultWidthFromParent = true;

                    // Adjust the y coordinate to compensate for the new control.
                    panely += CONTROL_SPACING + control.Size.Height;
                }
            }

            // Check to see if the last group meta panel has 0 child controls.
            if (panel.Controls.Count > 0)
            {
                // Add the group panel to the tab.
                this.Controls[0].Controls.Add(panel);

                // Set the position and default width of the meta panel.
                panel.DefaultWidth = width;
                panel.Location = new Point(x, y);

                // Adjust the y coordinate to compensate for the new control.
                y += CONTROL_SPACING + panel.Size.Height;
            }

            // Resume the layout.
            this.ResumeLayout();
            this.Controls[0].Visible = true;

            // The panel is fully loaded.
            isPanelLoaded = true;

#if (GUI_BENCHMARK)

            // Stop the stop watch and display the time.
            watch.Stop();
            Console.WriteLine("TagEditorPage->LoadIntoPanel() {0}", watch.Elapsed.ToString());
#endif
        }

        private MetaControl CreateControl(HaloPlugins.Objects.MetaNode field)
        {
            // Check the field type and handle accordingly.
            switch (field.FieldType)
            {
                case HaloPlugins.Objects.FieldType.Flags:       return new Bitmask((HaloPlugins.Objects.Data.Flags)field);
                case HaloPlugins.Objects.FieldType.Enum:        return new Enum((HaloPlugins.Objects.Data.Enum)field);
                case HaloPlugins.Objects.FieldType.Value:       return new Value((HaloPlugins.Objects.Data.Value)field);

                case HaloPlugins.Objects.FieldType.LongString:
                case HaloPlugins.Objects.FieldType.ShortString: 
                case HaloPlugins.Objects.FieldType.StringId:
                case HaloPlugins.Objects.FieldType.Tag:         return new HaloControls.String(field);

                default:
                    {
                        // Display a warning to the console
                        Console.WriteLine("WARNING: Field type \"{0}\" in tag type \"{1}\" does not have a gui element!",
                            field.FieldType.ToString(), plugin.DefaultExtension);

                        // Return null.
                        return null;
                    }
            }
        }

        public void Save()
        {
            //// Double Check Path
            //if (!File.Exists(path))
            //{
            //    SaveFileDialog sfd = new SaveFileDialog();
            //    sfd.Filter = "Halo 2 Tags (*." + TagClass + "|*." + TagClass;
            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //        path = sfd.FileName;
            //        Globals.Tags.Add(path);
            //    }
            //}

            // Get new values
            //int Field = 0;
            //for (int i = 0; i < this.Controls[0].Controls.Count; i++)
            //{
            //    if (this.Controls[0].Controls[i].GetType() == typeof(Value))
            //    {
            //        Field = ((Value)this.Controls[0].Controls[i]).Index;
            //        plugin[Field].SetValue(((Value)this.Controls[0].Controls[i]).Save());
            //    }
            //    else if (this.Controls[0].Controls[i].GetType() == typeof(Enum))
            //    {
            //        Field = ((Enum)this.Controls[0].Controls[i]).Index;
            //        plugin[Field].SetValue(((Enum)this.Controls[0].Controls[i]).Save());
            //    }
            //    else if (this.Controls[0].Controls[i].GetType() == typeof(Bitmask))
            //    {
            //        Field = ((Bitmask)this.Controls[0].Controls[i]).Index;
            //        plugin[Field].SetValue(((Bitmask)this.Controls[0].Controls[i]).Save());
            //    }
            //    else if (this.Controls[0].Controls[i].GetType() == typeof(HaloControls.String))
            //    {
            //        Field = ((HaloControls.String)this.Controls[0].Controls[i]).Index;
            //        plugin[Field].SetValue(((HaloControls.String)this.Controls[0].Controls[i]).Save());
            //    }
            //    else if (this.Controls[0].Controls[i].GetType() == typeof(TagReference))
            //    {
            //        Field = ((HaloControls.TagReference)this.Controls[0].Controls[i]).Index;
            //        plugin[Field].SetValue(((HaloControls.TagReference)this.Controls[0].Controls[i]).Save());
            //    }
            //    else if (this.Controls[0].Controls[i].GetType() == typeof(TagBlock))
            //    {
            //        Field = ((TagBlock)this.Controls[0].Controls[i]).Index;
            //        ((TagBlock)this.Controls[0].Controls[i]).Save();
            //    }
            //}

            // Delete the file
            File.Delete(Path);

            // Reopen stream
            EndianWriter bw = new EndianWriter(Endianness.Little, new FileStream(Path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));

            // Write to tag
            plugin.Write(bw);

            // Close
            bw.Close();

            // Done
            MessageBox.Show("Done!");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            // Let the super class handle resizing.
            base.OnSizeChanged(e);

            // Check if the panel has been initialized.
            if (isPanelLoaded == false)
                return;

            // Get the panel control that has all the meta field controls.
            Panel panel = (Panel)this.Controls[0];

            // Compute the new x position for the panel children.
            int x = (this.Width / 4);

            // Loop through all the child controls in the panel.
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                // Check if the child control is of type MetaControl.
                Type type = panel.Controls[i].GetType().BaseType;
                if (type == typeof(MetaControl) || type.BaseType == typeof(MetaControl))
                {
                    // Change the default width, which will trigger OnDefaultWidthChanged.
                    ((MetaControl)panel.Controls[i]).DefaultWidth = (panel.Size.Width / 2);

                    // Suspend the layout of the control.
                    panel.Controls[i].SuspendLayout();

                    // Change the x position of the control to re-center it on the panel.
                    int y = panel.Controls[i].Location.Y;
                    ((MetaControl)panel.Controls[i]).Location = new Point(x, y);

                    // Resume the layout of the control and process any layout requests.
                    panel.Controls[i].ResumeLayout(true);
                }
            }
        }
    }
}
