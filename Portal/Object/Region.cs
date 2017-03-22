using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Object
{
    public class Region
    {
        public string Name;
        public short ParentIndex;
        public Permutation[] Perms;

        public void Read(IMetaNode[] hlmt)
        {
            Name = (string)hlmt[0].GetValue();
            ParentIndex = (short)hlmt[1].GetValue();

            // Permutations
            Perms = new Permutation[((H2XTagBlock)hlmt[3]).GetChunkCount()];
            for (int i = 0; i < Perms.Length; i++)
            {
                ((H2XTagBlock)hlmt[3]).ChangeIndex(i);
                Perms[i] = new Permutation();
                Perms[i].Read((IMetaNode[])hlmt[3].GetValue());
            }
        }
    }
}
