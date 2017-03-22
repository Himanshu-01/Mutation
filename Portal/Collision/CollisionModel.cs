using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using Portal.Collision;
using Xapien;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal
{
    public class CollisionModel : IRenderable
    {
        public Region[] Regions;

        public void Read(IMetaNode[] coll, string TagPath)
        {
            // Regions
            Regions = new Region[((H2XTagBlock)coll[2]).GetChunkCount()];
            for (int i = 0; i < Regions.Length; i++)
            {
                ((H2XTagBlock)coll[2]).ChangeIndex(i);
                Regions[i] = new Region();
                Regions[i].Read((IMetaNode[])coll[2].GetValue());
            }
        }

        public void Initialize(Device device)
        {
            // Regions
            for (int i = 0; i < Regions.Length; i++)
                Regions[i].Initialize(device);
        }

        public void Draw(Device device)
        {
            // Regions
            for (int i = 0; i < Regions.Length; i++)
                Regions[i].Draw(device);
        }

        public void Click(Device device, int x, int y)
        {
            
        }
    }
}
