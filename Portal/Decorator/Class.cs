using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Decorator
{
    public class Class
    {
        public string Name;
        public Permutation[] Permutations;

        public void Read(IMetaNode[] decr)
        {
            Name = (string)decr[0].GetValue();
            Permutations = new Permutation[((H2XTagBlock)decr[3]).GetChunkCount()];
            for (int i = 0; i < Permutations.Length; i++)
            {
                ((H2XTagBlock)decr[3]).ChangeIndex(i);
                Permutations[i] = new Permutation();
                Permutations[i].Read((IMetaNode[])decr[3].GetValue());
            }
        }
    }
}
