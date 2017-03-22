using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaloMap;
using System.Security.Cryptography;

namespace CrossReference
{
    public partial class Form1 : Form
    {
        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
        string BasePath, SourcePath;
        Map Base, Source;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Halo 2 Maps (*.map)|*.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                BasePath = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Halo 2 Maps (*.map)|*.map";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
                SourcePath = ofd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Read out Maps
            Base = new Map(BasePath);
            Base.Read(null);
            Source = new Map(SourcePath);
            Source.Read(null);

            // Compare Ugh
            if (Base.Coconuts.SoundChunks.Count != Source.Coconuts.SoundChunks.Count)
            {
                MessageBox.Show("Sound Chunks Count Mismatch!");
                return;
            }

            #region Sound Raw

            for (int i = 0; i < Base.Coconuts.SoundChunks.Count; i++)
            {
                if (Base.Coconuts.SoundChunks[i].RawLocation == RawLocation.Internal)
                {
                    if (Source.Coconuts.SoundChunks[i].RawLocation != RawLocation.Internal)
                    {
                        MessageBox.Show("Raw Location Mismatch At: " + i.ToString());
                    }
                    else
                    {
                        if (Base.Coconuts.SoundChunks[i].Offset != Source.Coconuts.SoundChunks[i].Offset)
                        {
                            ListViewItem item = new ListViewItem("Sound Raw");
                            item.SubItems.Add(Base.Coconuts.SoundChunks[i].Offset.ToString());
                            item.SubItems.Add(Source.Coconuts.SoundChunks[i].Offset.ToString());

                            // Goto and Run a SHA1 over it
                            Base.br.BaseStream.Position = Base.Coconuts.SoundChunks[i].Offset;
                            byte[] BaseHash = sha.ComputeHash(Base.br.ReadBytes(Base.Coconuts.SoundChunks[i].Size));

                            Source.br.BaseStream.Position = Source.Coconuts.SoundChunks[i].Offset;
                            byte[] SourceHash = sha.ComputeHash(Source.br.ReadBytes(Source.Coconuts.SoundChunks[i].Size));

                            // Compare
                            bool Match = true;
                            for (int x = 0; x < 20; x++)
                            {
                                if (BaseHash[x] != SourceHash[x])
                                    Match = false;
                            }

                            item.SubItems.Add(Match == true ? "Yes" : "No");
                            item.SubItems.Add(i.ToString());
                            listView1.Items.Add(item);
                        }
                    }
                }
            }

            #endregion

            // Close Maps
            Base.Close();
            Source.Close();
            MessageBox.Show("Done!");
        }
    }
}
