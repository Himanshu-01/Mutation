using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Model
{
    public class Permutation
    {
        public string Name;
        public short Lowest, Low, MediumLow, MediumHigh, High, Highest;

        public void Read(IMetaNode[] mode)
        {
            Name = (string)mode[0].GetValue();
            Lowest = (short)mode[1].GetValue();
            Low = (short)mode[2].GetValue();
            MediumLow = (short)mode[3].GetValue();
            MediumHigh = (short)mode[4].GetValue();
            High = (short)mode[5].GetValue();
            Highest = (short)mode[6].GetValue();
        }
    }
}
