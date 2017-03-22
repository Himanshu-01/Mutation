using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaloMap;
using HaloPlugins;
using HaloControls;
using Controls;
using HaloObjects;

namespace StructureTool
{
    public partial class Form1 : Form
    {
        Map map;
        Tag tag;
        int Last = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Halo 2 Xbox Map (*.map)|*.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                map = new Map(ofd.FileName);
                map.Read(treeView1);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Select Tag
            if (treeView1.SelectedNode.Text.Length != 4)
                tag = map.Tags[(int)treeView1.SelectedNode.Tag];
            else
                return;

            // Get Plugin
            Handler.GetPlugin(tag.Class);
            HaloControls.Plugin p = new Plugin(Handler.DefaultName, null);
            if (p.Layout == null)
                return;

            // Read
            map.br.BaseStream.Position = tag.Offset;
            if (tag.Class == "sbsp" || tag.Class == "ltmp")
                p.Layout.Read(map.br, map.BSPs[0].Magic);
            else
                p.Layout.Read(map.br, map.Index.SecondaryMagic);

            // Put into Structure Viewer
            fileBlockGraphPanel1.BlockOffsets = new BlockDataCollection();
            listView1.Items.Clear();
            fileBlockGraphPanel1.FileLength = tag.Size;
            fileBlockGraphPanel1.BlockOffsets.Add(new Controls.BlockData(0, DataType.Header));

            // Add Reflexives
            Last = tag.Offset + p.Layout.HeaderSize;
            for (int i = 0; i < p.Layout.Fields.Length; i++)
            {
                if (p.Layout.Fields[i].GetType() == typeof(HaloObjects.TagBlock))
                {
                    // Check ChunkCount
                    if (((HaloObjects.TagBlock)p.Layout.Fields[i]).ChunkCount != 0)
                    {
                        // Calculate Padding
                        int Interval = ((HaloObjects.TagBlock)p.Layout.Fields[i]).Padding;
                        int Padding = Interval - (Last % Interval);
                        Padding = Padding != Interval ? Padding : 0;
                        int Trans = ((HaloObjects.TagBlock)p.Layout.Fields[i]).Translation;// -tag.Offset;

                        // Check Padding
                        if (Last + Padding == Trans)
                            fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last, DataType.Padding));
                        else if (Last + Padding > Trans)
                        {
                            fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last, DataType.Undefined));
                            listView1.Items.Add(new ListViewItem("Overlap at: " + ((HaloObjects.TagBlock)p.Layout.Fields[i]).GetName() + " - " + ((Last + Padding) - Trans).ToString()));
                        }
                        else if (Last + Padding < Trans)
                        {
                            fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last, DataType.Padding));
                            fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last + Padding, DataType.Undefined));
                            listView1.Items.Add(new ListViewItem("Hole at: " + ((HaloObjects.TagBlock)p.Layout.Fields[i]).GetName() + " - " + (Trans - (Last + Padding)).ToString()));
                        }

                        // Meta
                        fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Trans, DataType.Meta));

                        // Check Sub Reflexives
                        Last = Trans + (((HaloObjects.TagBlock)p.Layout.Fields[i]).ChunkCount * ((HaloObjects.TagBlock)p.Layout.Fields[i]).ChunkSize) + Padding;
                        CheckReflexive(((HaloObjects.TagBlock)p.Layout.Fields[i]));
                    }
                }
            }
        }

        void CheckReflexive(HaloObjects.TagBlock Reflexive)
        {
            if (Reflexive.ChunkCount != 0)
            {
                for (int i = 0; i < Reflexive.ChunkCount; i++)
                {
                    Reflexive.ChangeIndex(i);
                    MetaNode[] Nodes = (MetaNode[])Reflexive.GetValue();
                    for (int x = 0; x < Nodes.Length; x++)
                    {
                        if (Nodes[x].GetType() == typeof(HaloObjects.TagBlock))
                        {
                            // Check ChunkCount
                            if (((HaloObjects.TagBlock)Nodes[x]).ChunkCount != 0)
                            {
                                // Calculate Padding
                                int Interval = ((HaloObjects.TagBlock)Nodes[x]).Padding;
                                int Padding = Interval - (Last % Interval);
                                Padding = Padding != Interval ? Padding : 0;
                                int Trans = ((HaloObjects.TagBlock)Nodes[x]).Translation;// -tag.Offset;

                                // Check Padding
                                if (Last + Padding == Trans)
                                    fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last, DataType.Padding));
                                else if (Last + Padding > Trans)
                                {
                                    fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last, DataType.Undefined));
                                    listView1.Items.Add(new ListViewItem("Overlap at: " + ((HaloObjects.TagBlock)Nodes[x]).GetName() + "[" + i.ToString() + "]" + " - " + ((Last + Padding) - Trans).ToString()));
                                }
                                else if (Last + Padding < Trans)
                                {
                                    fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last, DataType.Padding));
                                    fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Last + Padding, DataType.Undefined));
                                    listView1.Items.Add(new ListViewItem("Hole at: " + ((HaloObjects.TagBlock)Nodes[x]).GetName() + " - " + (Trans - (Last + Padding)).ToString()));
                                }

                                // Meta
                                fileBlockGraphPanel1.BlockOffsets.Add(new BlockData(Trans, DataType.Meta));

                                // Check Sub Reflexives
                                Last = Trans + (((HaloObjects.TagBlock)Nodes[x]).ChunkCount * ((HaloObjects.TagBlock)Nodes[x]).ChunkSize) + Padding;
                                CheckReflexive(((HaloObjects.TagBlock)Nodes[x]));
                            }
                        }
                    }
                }
            }
        }
    }
}
