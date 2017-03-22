using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DevExpress;
using DevExpress.XtraTab;
using System.Collections;
using DevExpress.XtraNavBar;
using Global;
using HaloPlugins;

namespace HaloControls
{
    public partial class TagBlock : UserControl
    {
        int OldIndex = 0;
        bool Expanded = true, Loading = true;
        
        protected HaloPlugins.Objects.Reference.TagBlock tagBlock;

        public TagBlock(HaloPlugins.Objects.Reference.TagBlock tagBlock)
        {
            // Initialize form controls.
            InitializeComponent();

            // Save our tabBlock object.
            this.tagBlock = tagBlock;

            // Setup the tag block header and footer controls.
            groupControl1.Text = this.tagBlock.Name;
            groupControl2.Text = string.Format("End of \'{0}\'", this.tagBlock.Name);
        }

        private void TagBlock_Load(object sender, EventArgs ea)
        {
            // Set default control size.
            this.Size = new Size(this.Size.Width, 34);

            // Collapse Panel
            button1_Click(null, null);

            // Load Controls
            //HaloPlugins.Objects.MetaNode[] Fields = (HaloPlugins.Objects.MetaNode[])tagblock.GetValue();
            //for (int i = Fields.Length - 1; i >= 0; i--)
            //{
            //    if (Fields[i].GetType() == typeof(HaloPlugins.Value))
            //    {
            //        Value v = new Value(((HaloPlugins.Value)Fields[i]).GetName(), ((HaloPlugins.Value)Fields[i]).GetValue(), Fields[i].GetSize(), i);
            //        v.Dock = DockStyle.Top;
            //        blockContentPanel.Controls.Add(v);
            //    }
            //    else if (Fields[i].GetType() == typeof(HaloPlugins.Flags))
            //    {
            //        Bitmask b = new Bitmask(((HaloPlugins.Flags)Fields[i]).GetName(), ((HaloPlugins.Flags)Fields[i]).GetOptions(), ((HaloPlugins.Flags)Fields[i]).GetValue(), ((HaloPlugins.Flags)Fields[i]).GetBitCount(), i);
            //        b.Dock = DockStyle.Top;
            //        blockContentPanel.Controls.Add(b);
            //    }
            //    else if (Fields[i].GetType() == typeof(HaloPlugins.Enum))
            //    {
            //        Enum b = new Enum(((HaloPlugins.Enum)Fields[i]).GetName(), ((HaloPlugins.Enum)Fields[i]).GetOptions(), ((HaloPlugins.Enum)Fields[i]).GetValue(), ((HaloPlugins.Enum)Fields[i]).GetBitCount(), i);
            //        b.Dock = DockStyle.Top;
            //        blockContentPanel.Controls.Add(b);
            //    }
            //    else if (Fields[i].GetType() == typeof(HaloPlugins.LongString)
            //        || Fields[i].GetType() == typeof(HaloPlugins.ShortString)
            //        || Fields[i].GetType() == typeof(HaloPlugins.Tag))
            //    {
            //        HaloControls.String s = new String(Fields[i].GetName(), Fields[i].GetValue(), Fields[i].GetSize(), i);
            //        s.Dock = DockStyle.Top;
            //        blockContentPanel.Controls.Add(s);
            //    }
            //    else if (Fields[i].GetType() == typeof(HaloPlugins.StringId))
            //    {
            //        HaloControls.String s = new String(Fields[i].GetName(), Fields[i].GetValue(), 128, i);
            //        s.Dock = DockStyle.Top;
            //        blockContentPanel.Controls.Add(s);
            //    }
            //    else if (Fields[i].GetType() == typeof(HaloPlugins.TagIndex)
            //        || Fields[i].GetType() == typeof(HaloPlugins.TagReference))
            //    {
            //        HaloControls.TagReference t = new TagReference(Fields[i].GetName(), (string)Fields[i].GetValue(), i);
            //        t.Dock = DockStyle.Top;
            //        blockContentPanel.Controls.Add(t);
            //    }
            //    else if (Fields[i].GetType() == typeof(HaloPlugins.TagBlock))
            //    {
            //        HaloControls.TagBlock r = new TagBlock(Fields[i].GetName(), (HaloPlugins.TagBlock)Fields[i], i);
            //        r.Dock = DockStyle.Top;
            //        blockContentPanel.Controls.Add(r);
            //    }
            //}

            // Setup combo box
            if (tagBlock.BlockCount > 0)
            {
                // Add block numbers
                for (int i = 0; i < tagBlock.BlockCount; i++)
                    comboBox1.Items.Add(i.ToString());

                comboBox1.SelectedIndex = 0;
            }
            else
            {
                blockContentPanel.Enabled = false;
            }

            // Done
            Loading = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Negate the expanded state
            Expanded = !Expanded;

            // Change the visible state of the block header and content panels
            // Make the header and content panels invisible
            blockHeaderPanel.Visible = Expanded;
            blockContentPanel.Visible = Expanded;
        }

        private void btnAddChunk_Click(object sender, EventArgs e)
        {
            // Add Chunk
            tagBlock.AddBlock();

            // Save values
            if (tagBlock.BlockCount > 1)
            {
                OldIndex = comboBox1.SelectedIndex;
                SaveValues(OldIndex);
            }

            // Update ComboBox
            comboBox1.Items.Add(comboBox1.Items.Count.ToString());
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;

            // Update controls
            UpdateValues();

            // Make sure the Panel Is Enabled
            blockContentPanel.Enabled = true;
            blockContentPanel.Visible = true;
            Expanded = true;
        }

        private void btnDeleteChunk_Click(object sender, EventArgs e)
        {
            // Check
            if (tagBlock.BlockCount > 0)
                return;

            // Remove Chunk
            OldIndex = comboBox1.SelectedIndex;
            tagBlock.RemoveBlock(comboBox1.SelectedIndex);

            // Update ComboBox
            comboBox1.Items.Clear();
            for (int i = 0; i < tagBlock.BlockCount; i++)
                comboBox1.Items.Add(i.ToString());

            // Selete Near Index
            if (OldIndex <= tagBlock.BlockCount - 1)
                comboBox1.SelectedIndex = OldIndex;
            else if (OldIndex - 1 <= tagBlock.BlockCount - 1)
                comboBox1.SelectedIndex = OldIndex - 1;
            else
                comboBox1.Text = "";

            // Double Check
            if (tagBlock.BlockCount == 0)
            {
                blockContentPanel.Enabled = false;
                blockContentPanel.Visible = false;
                Expanded = false;
            }
        }

        private void btnDeleteAllChunks_Click(object sender, EventArgs e)
        {
            // Update Form
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            blockContentPanel.Enabled = false;
            blockContentPanel.Visible = false;
            Expanded = false;
            OldIndex = 0;

            // Clear Reflexive
            tagBlock.RemoveAll();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Double Check
            if (Loading)
                return;

            // Save Old Values
            SaveValues(OldIndex);

            // Update
            OldIndex = comboBox1.SelectedIndex;
            UpdateValues();

            // Resume
            blockContentPanel.ResumeLayout();
        }

        public void Reload(object Value)
        {
            //tagblock = (HaloPlugins.TagBlock)Value;
            //if (tagblock.BlockCount > 0)
            //{
            //    //Set Loading
            //    Loading = true;

            //    //Change Index
            //    comboBox1.Items.Clear();
            //    for (int i = 0; i < tagblock.BlockCount; i++)
            //    {
            //        comboBox1.Items.Add(i.ToString());
            //    }
            //    comboBox1.SelectedIndex = 0;
            //    OldIndex = 0;

            //    // Update values
            //    UpdateValues();

            //    // Done
            //    Loading = false;
            //}
            //else
            //{
            //    comboBox1.Items.Clear();
            //    comboBox1.Text = "";
            //    blockContentPanel.Enabled = false;
            //    blockContentPanel.Visible = false;
            //    Expanded = false;
            //}
        }

        public object Save()
        {
            // Sanity Check
            if (tagBlock.BlockCount == 0)
                return tagBlock;

            // Save values
            SaveValues(comboBox1.SelectedIndex);

            // We return nothing
            return null;
        }

        void UpdateValues()
        {
            /*
            int Field = 0;
            for (int i = 0; i < blockContentPanel.Controls.Count; i++)
            {
                if (blockContentPanel.Controls[i].GetType() == typeof(Value))
                {
                    Field = ((Value)(blockContentPanel.Controls[i])).Index;
                    ((Value)(blockContentPanel.Controls[i])).Reload(tagblock[comboBox1.SelectedIndex][Field].GetValue());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(String))
                {
                    Field = ((String)(blockContentPanel.Controls[i])).Index;
                    ((String)(blockContentPanel.Controls[i])).Reload(tagblock[comboBox1.SelectedIndex][Field].GetValue());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(Enum))
                {
                    Field = ((Enum)(blockContentPanel.Controls[i])).Index;
                    ((Enum)(blockContentPanel.Controls[i])).Reload(tagblock[comboBox1.SelectedIndex][Field].GetValue());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(Bitmask))
                {
                    Field = ((Bitmask)(blockContentPanel.Controls[i])).Index;
                    ((Bitmask)(blockContentPanel.Controls[i])).Reload(tagblock[comboBox1.SelectedIndex][Field].GetValue());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(TagReference))
                {
                    Field = ((TagReference)(blockContentPanel.Controls[i])).Index;
                    ((TagReference)(blockContentPanel.Controls[i])).Reload(tagblock[comboBox1.SelectedIndex][Field].GetValue());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(TagBlock))
                {
                    Field = ((TagBlock)(blockContentPanel.Controls[i])).Index;
                    ((TagBlock)(blockContentPanel.Controls[i])).Reload(tagblock[comboBox1.SelectedIndex][Field]);
                }
            }
             * */
        }

        void SaveValues(int Index)
        {
            /*
            int Field = 0;
            for (int i = 0; i < blockContentPanel.Controls.Count; i++)
            {
                if (blockContentPanel.Controls[i].GetType() == typeof(Value))
                {
                    Field = ((Value)(blockContentPanel.Controls[i])).Index;
                    tagblock[Index][Field].SetValue(((Value)(blockContentPanel.Controls[i])).Save());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(String))
                {
                    Field = ((String)(blockContentPanel.Controls[i])).Index;
                    tagblock[Index][Field].SetValue(((String)(blockContentPanel.Controls[i])).Save());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(Enum))
                {
                    Field = ((Enum)(blockContentPanel.Controls[i])).Index;
                    tagblock[Index][Field].SetValue(((Enum)(blockContentPanel.Controls[i])).Save());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(Bitmask))
                {
                    Field = ((Bitmask)(blockContentPanel.Controls[i])).Index;
                    tagblock[Index][Field].SetValue(((Bitmask)(blockContentPanel.Controls[i])).Save());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(TagReference))
                {
                    Field = ((TagReference)(blockContentPanel.Controls[i])).Index;
                    tagblock[Index][Field].SetValue(((TagReference)(blockContentPanel.Controls[i])).Save());
                }
                else if (blockContentPanel.Controls[i].GetType() == typeof(TagBlock))
                {
                    Field = ((TagBlock)(blockContentPanel.Controls[i])).Index;
                    ((TagBlock)(blockContentPanel.Controls[i])).Save();
                }
            }
             */
        }

        private void btnInsertChunk_Click(object sender, EventArgs e)
        {
            // Get copy data
            CopyStruct copydata = Global.EventHandler.GetCopyData();
            //if (copydata.BlockName == tagblock.GetName())
            //{
            //    // Overwrite?
            //    if (copydata.TagBlockData.GetType() == typeof(HaloPlugins.TagBlock))
            //        Reload((HaloPlugins.TagBlock)copydata.TagBlockData);
            //    else
            //    {
            //        if (MessageBox.Show("Would you like to overwrite this block?", "Overwrite Block", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            this.tagblock[comboBox1.SelectedIndex] = (MetaNode[])copydata.TagBlockData;
            //            UpdateValues();
            //        }
            //        else
            //        {
            //            // Add block
            //            this.tagblock.AddBlock();
            //            this.tagblock[tagblock.BlockCount - 1] = (MetaNode[])copydata.TagBlockData;

            //            // Update
            //            comboBox1.Items.Add((tagblock.BlockCount - 1).ToString());
            //            comboBox1.SelectedIndex = tagblock.BlockCount - 1;
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("Source block does not match destination!");
        }

        private void btnCopyChunk_Click(object sender, EventArgs e)
        {
            //// Create a new copy struct
            //CopyStruct copydata = new CopyStruct();
            //copydata.Endian = IO.Endianness.Little;
            //copydata.ProjectDirectory = Global.Application.Instance.Project.Directory;
            //copydata.BlockName = tagblock.GetName();
            //copydata.TagBlockData = tagblock[comboBox1.SelectedIndex];

            //// Set data
            //Global.EventHandler.SetCopyData(copydata);
        }

        private void btnCopyAllChunks_Click(object sender, EventArgs e)
        {
            //// Create a new copy struct
            //CopyStruct copydata = new CopyStruct();
            //copydata.Endian = IO.Endianness.Little;
            //copydata.ProjectDirectory = Global.Application.Instance.Project.Directory;
            //copydata.BlockName = tagblock.GetName();
            //copydata.TagBlockData = tagblock;

            //// Set data
            //Global.EventHandler.SetCopyData(copydata);
        }
    }
}
