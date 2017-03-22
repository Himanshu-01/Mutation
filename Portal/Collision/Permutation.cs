using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Collision
{
    public class Permutation
    {
        public string Name;
        public Bsp[] Bsps;

        public void Read(IMetaNode[] coll)
        {
            // Name
            Name = (string)coll[0].GetValue();
            
            // Bsps
            Bsps = new Bsp[((H2XTagBlock)coll[1]).GetChunkCount()];
            for (int i = 0; i < Bsps.Length; i++)
            {
                ((H2XTagBlock)coll[1]).ChangeIndex(i);
                Bsps[i] = new Bsp();
                Bsps[i].Read((IMetaNode[])coll[1].GetValue());
            }
        }

        public void Initialize(Device device)
        {
            // Bsps
            for (int i = 0; i < Bsps.Length; i++)
                Bsps[i].Initialize(device);
        }

        public void Draw(Device device)
        {
            // Bsps
            for (int i = 0; i < Bsps.Length; i++)
                Bsps[i].Draw(device);
        }
    }
}
