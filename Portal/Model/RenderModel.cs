using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using SlimDX;
using SlimDX.Direct3D9;
using Portal.Model;

namespace Portal
{
    public class RenderModel : IRenderable
    {
        // Object Properties
        ObjectProperties hlmt;

        // Model Info
        public CompressionInfo CompressionInfo = new CompressionInfo();
        public Region[] Regions;
        public Section[] Sections;
        public Node[] Nodes;
        public MarkerGroup[] Markers;
        public Portal.Model.Material[] Materials;

        public void Read(IMetaNode[] mode, string TagPath)
        {
            // Read the Compression Info
            IMetaNode[] Fields = (IMetaNode[])mode[4].GetValue();
            CompressionInfo.Read(Fields);

            // Regions
            Regions = new Region[((H2XTagBlock)mode[5]).GetChunkCount()];
            for (int i = 0; i < Regions.Length; i++)
            {
                ((H2XTagBlock)mode[5]).ChangeIndex(i);
                Fields = (IMetaNode[])mode[5].GetValue();
                Regions[i] = new Region();
                Regions[i].Read(Fields);
            }

            // Sections
            Sections = new Section[((H2XTagBlock)mode[6]).GetChunkCount()];
            for (int i = 0; i < Sections.Length; i++)
            {
                ((H2XTagBlock)mode[6]).ChangeIndex(i);
                Fields = (IMetaNode[])mode[6].GetValue();
                Sections[i] = new Section();
                Sections[i].Read(Fields, TagPath, CompressionInfo);
            }

            // Nodes
            Nodes = new Node[((H2XTagBlock)mode[15]).GetChunkCount()];
            for (int i = 0; i < Nodes.Length; i++)
            {
                ((H2XTagBlock)mode[15]).ChangeIndex(i);
                Fields = (IMetaNode[])mode[15].GetValue();
                Nodes[i] = new Node();
                Nodes[i].Read(Fields);
            }

            // Markers
            Markers = new MarkerGroup[((H2XTagBlock)mode[17]).GetChunkCount()];
            for (int i = 0; i < Markers.Length; i++)
            {
                ((H2XTagBlock)mode[17]).ChangeIndex(i);
                Fields = (IMetaNode[])mode[17].GetValue();
                Markers[i] = new MarkerGroup();
                Markers[i].Read(Fields);
            }

            // Materials
            Materials = new Portal.Model.Material[((H2XTagBlock)mode[18]).GetChunkCount()];
            for (int i = 0; i < Materials.Length; i++)
            {
                ((H2XTagBlock)mode[18]).ChangeIndex(i);
                Fields = (IMetaNode[])mode[18].GetValue();
                Materials[i] = new Portal.Model.Material();
                Materials[i].Read(Fields);
            }

            // Object Properties
            //hlmt = new ObjectProperties(TagPath);
        }

        public void Initialize(Device device)
        {
            // Sections
            for (int i = 0; i < Sections.Length; i++)
                Sections[i].Initialize(device, Materials);

            // Nodes
            for (int i = 0; i < Nodes.Length; i++)
                Nodes[i].Initialize(device);

            // Markers
            for (int i = 0; i < Markers.Length; i++)
                Markers[i].Initialize(device);
        }

        public void Draw(Device device)
        {
            // Sections
            //for (int i = 0; i < Sections.Length; i++)
            //    Sections[i].Draw(device, Materials);
            for (int i = 0; i < Regions.Length; i++)
            {
                for (int x = 0; x < Regions[i].Perms.Length; x++)
                {
                    Sections[Regions[i].Perms[x].High].Draw(device, Materials);
                }
            }

            // Nodes
            //for (int i = 0; i < Nodes.Length; i++)
            //    Nodes[i].Draw(device);

            // Markers
            //for (int i = 0; i < Markers.Length; i++)
            //    Markers[i].Draw(device);
        }

        public void Click(Device device, int x, int y)
        {
            Vector3 Near = new Vector3(x, y, 0);
            Vector3 Far = new Vector3(x, y, 1);

            // Sections
            for (int i = 0; i < Sections.Length; i++)
                Sections[i].Click(device, Near, Far);

            // Nodes
            for (int i = 0; i < Nodes.Length; i++)
                Nodes[i].Click(device, Near, Far);
        }
    }
}
