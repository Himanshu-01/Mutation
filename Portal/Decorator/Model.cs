using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Decorator
{
    public class Model
    {
        public string Name;
        public short IndiceStart, IndiceCount;

        public void Read(IMetaNode[] decr)
        {
            Name = (string)decr[0].GetValue();
            IndiceStart = (short)decr[1].GetValue();
            IndiceCount = (short)decr[2].GetValue();
        }
    }
}
