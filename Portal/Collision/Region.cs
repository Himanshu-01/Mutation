using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Collision
{
    public class Region
    {
        public string Name;
        public Permutation[] Perms;

        public void Read(IMetaNode[] coll)
        {
            // Name
            Name = (string)coll[0].GetValue();

            // Permutations
            Perms = new Permutation[((H2XTagBlock)coll[1]).GetChunkCount()];
            for (int i = 0; i < Perms.Length; i++)
            {
                ((H2XTagBlock)coll[1]).ChangeIndex(i);
                Perms[i] = new Permutation();
                Perms[i].Read((IMetaNode[])coll[1].GetValue());
            }
        }

        public void Initialize(Device device)
        {
            // Permutations
            for (int i = 0; i < Perms.Length; i++)
                Perms[i].Initialize(device);
        }

        public void Draw(Device device)
        {
            // Permutations
            for (int i = 0; i < Perms.Length; i++)
                Perms[i].Draw(device);
        }
    }
}
