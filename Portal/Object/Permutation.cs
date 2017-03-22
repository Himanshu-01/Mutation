using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Object
{
    public class Permutation
    {
        public string Name;

        public void Read(IMetaNode[] hlmt)
        {
            Name = (string)hlmt[0].GetValue();
        }
    }
}
