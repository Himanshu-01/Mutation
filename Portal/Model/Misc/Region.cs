using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Model
{
    public class Region
    {
        public string Name;
        public short NodeMapOffset, NodeMapSize;
        public Permutation[] Perms;

        public void Read(IMetaNode[] mode)
        {
            Name = (string)mode[0].GetValue();
            NodeMapOffset = (short)mode[1].GetValue();
            NodeMapSize = (short)mode[2].GetValue();

            Perms = new Permutation[((H2XTagBlock)mode[3]).GetChunkCount()];
            for (int i = 0; i < Perms.Length; i++)
            {
                ((H2XTagBlock)mode[3]).ChangeIndex(i);
                Perms[i] = new Permutation();
                Perms[i].Read((IMetaNode[])mode[3].GetValue());
            }
        }
    }
}
