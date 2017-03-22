using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaloControls;
using HaloPlugins;
using HaloObjects;
using HaloMap;

namespace MapFucker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.map)|*.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                Fuck();
            else
                Search();
        }

        void Fuck()
        {
            // Open Map
            Map map = new Map(textBox1.Text);
            map.Read(null);

            for (int i = 0; i < map.Tags.Count; i++)
            {
                Handler.GetPlugin(map.Tags[i].Class);
                if (Handler.DefaultName != "null")
                {
                    map.br.BaseStream.Position = map.Tags[i].Offset;
                    Plugin p = new Plugin(Handler.DefaultName, null);
                    
                    if (p.Layout != null)
                    {
                        if (map.Tags[i].Class != "sbsp" && map.Tags[i].Class != "ltmp")
                        {
                            for (int x = 0; x < p.Layout.Fields.Length; x++)
                            {
                                if (p.Layout.Fields[x].GetType() == typeof(HaloObjects.Reflexive))
                                {
                                    FuckReflexive(map, (HaloObjects.Reflexive)p.Layout.Fields[x]);
                                }
                                else if (p.Layout.Fields[x].GetType() == typeof(HaloObjects.TagReference))
                                {
                                    map.bw.BaseStream.Position = map.br.BaseStream.Position + 4;
                                    map.bw.Write((int)-1);
                                }
                                else if (p.Layout.Fields[x].GetType() == typeof(HaloObjects.Ident))
                                {
                                    map.bw.BaseStream.Position = map.br.BaseStream.Position;
                                    map.bw.Write((int)-1);
                                }
                                else if (p.Layout.Fields[x].GetType() == typeof(HaloObjects.StringID))
                                {
                                    map.bw.BaseStream.Position = map.br.BaseStream.Position;
                                    map.bw.Write((int)0);
                                }
                                else
                                    map.br.BaseStream.Position += p.Layout.Fields[x].GetSize();
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Done!");
        }

        void FuckReflexive(Map map, HaloObjects.Reflexive r)
        {
            int ChunkCount = map.br.ReadInt32();
            int Trans = map.br.ReadInt32() - map.Index.SecondaryMagic;
            int OldPos = (int)map.br.BaseStream.Position;

            MetaNode[] nodes = (MetaNode[])r.GetValue();

            for (int i = 0; i < ChunkCount; i++)
            {
                map.br.BaseStream.Position = Trans + (i * r.ChunkSize);
                for (int x = 0; x < nodes.Length; x++)
                {
                    if (nodes[x].GetType() == typeof(HaloObjects.Reflexive))
                    {
                        FuckReflexive(map, (HaloObjects.Reflexive)nodes[x]);
                    }
                    else if (nodes[x].GetType() == typeof(HaloObjects.TagReference))
                    {
                        map.bw.BaseStream.Position = map.br.BaseStream.Position + 4;
                        map.bw.Write((int)-1);
                    }
                    else if (nodes[x].GetType() == typeof(HaloObjects.Ident))
                    {
                        map.bw.BaseStream.Position = map.br.BaseStream.Position;
                        map.bw.Write((int)-1);
                    }
                    else if (nodes[x].GetType() == typeof(HaloObjects.StringID))
                    {
                        map.bw.BaseStream.Position = map.br.BaseStream.Position;
                        map.bw.Write((int)0);
                    }
                    else
                        map.br.BaseStream.Position += nodes[x].GetSize();
                }
            }

            map.br.BaseStream.Position = OldPos;
        }

        void Search()
        {
            Map map = new Map(textBox1.Text);
            map.Header.Read(map);
            map.Index.Read(map);
            map.TagIndex.Read(map, null);
            map.StringId.Read(map);

            map.br.BaseStream.Position = map.Header.MetaTableOffset;
            while (map.br.BaseStream.Position < map.br.BaseStream.Length)
            {
                // Save Pos
                int Pos = (int)map.br.BaseStream.Position;

                // Test
                int Val1 = map.br.ReadInt32();
                int Val2 = map.br.ReadInt32();

                // Check
                if ((short)Val1 < map.Header.ScriptCount && (short)Val1 >= 1)
                {
                    short Index = (short)Val1;
                    byte Length = (byte)((Val1 & 0xFF00) << 16);

                    if (map.SIDs[Index].Length == Length)
                    {
                        int Tag = 0;
                        for (int i = 0; i < map.Tags.Count; i++)
                        {
                            if (Pos >= map.Tags[i].Offset && Pos < map.Tags[i].Offset + map.Tags[i].Size)
                            {
                                Tag = i;
                                break;
                            }
                        }

                        listView1.Items.Add(map.Tags[Tag].Class);
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add((Pos - map.Tags[Tag].Offset).ToString());
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add("String Id");
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(map.Tags[Tag].Path);
                    }
                }
                else if (Val2 - map.Index.SecondaryMagic >= map.Header.MetaTableOffset && Val2 - map.Index.SecondaryMagic < (int)map.br.BaseStream.Length && Val1 > 0)
                {
                    // Reflexive
                    int Tag = 0;
                    for (int i = 0; i < map.Tags.Count; i++)
                    {
                        if (Pos >= map.Tags[i].Offset && Pos < map.Tags[i].Offset + map.Tags[i].Size)
                        {
                            Tag = i;
                            break;
                        }
                    }

                    listView1.Items.Add(map.Tags[Tag].Class);
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add((Pos - map.Tags[Tag].Offset).ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add("Reflexive");
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(map.Tags[Tag].Path);
                    Pos += 4;
                }
                else
                {
                    // Check Tag Reference
                    bool Found = false;
                    bool Ident = false;
                    int Tag = 0;
                    for (int i = 0; i < map.Tags.Count; i++)
                    {
                        if (Val1 == map.Tags[i].Ident || Val2 == map.Tags[i].Ident)
                        {
                            Ident = Val1 == map.Tags[i].Ident ? true : false;
                            Found = true;
                        }

                        if (Pos >= map.Tags[i].Offset && Pos < map.Tags[i].Offset + map.Tags[i].Size)
                        {
                            Tag = i;
                        }
                    }

                    if (Found)
                    {
                        listView1.Items.Add(map.Tags[Tag].Class);
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add((Pos - map.Tags[Tag].Offset).ToString());
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(Ident == true ? "Ident" : "Tag Reference");
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(map.Tags[Tag].Path);
                        Pos += Ident == true ? 0 : 4;
                    }
                }

                // Reset Pos
                map.br.BaseStream.Position = Pos + 4;
            }

            MessageBox.Show("Done!");
        }
    }
}
