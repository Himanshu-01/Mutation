using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HaloObjects;

namespace Portal.Model
{
    public class CompressionInfo
    {
        public float XMin, XMax;
        public float YMin, YMax;
        public float ZMin, ZMax;
        public float UMin, UMax;
        public float VMin, VMax;

        public void Read(IMetaNode[] mode)
        {
            XMin = (float)mode[0].GetValue();
            XMax = (float)mode[1].GetValue();
            YMin = (float)mode[2].GetValue();
            YMax = (float)mode[3].GetValue();
            ZMin = (float)mode[4].GetValue();
            ZMax = (float)mode[5].GetValue();
            UMin = (float)mode[6].GetValue();
            UMax = (float)mode[7].GetValue();
            VMin = (float)mode[8].GetValue();
            VMax = (float)mode[9].GetValue();
        }
    }
}
