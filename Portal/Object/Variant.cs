using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Object
{
    public class Variant
    {
        public string Name;
        public Region[] Regions;

        public void Read(IMetaNode[] hlmt)
        {
            Name = (string)hlmt[0].GetValue();
            Regions = new Region[((H2XTagBlock)hlmt[2]).GetChunkCount()];
            for (int i = 0; i < Regions.Length; i++)
            {
                ((H2XTagBlock)hlmt[2]).ChangeIndex(i);
                Regions[i] = new Region();
                Regions[i].Read((IMetaNode[])hlmt[2].GetValue());
            }
        }
    }
}
