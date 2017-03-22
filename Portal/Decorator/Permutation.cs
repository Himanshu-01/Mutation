using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Decorator
{
    public class Permutation
    {
        public string Name;
        public short ShaderIndex;
        public byte FadeDistance;

        public void Read(IMetaNode[] decr)
        {
            Name = (string)decr[0].GetValue();
            ShaderIndex = (short)decr[1].GetValue();
            FadeDistance = Convert.ToByte(decr[3].GetValue().ToString());
        }
    }
}
