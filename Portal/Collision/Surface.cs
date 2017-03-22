using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Collision
{
    public class Surface
    {
        public short Plane, FirstEdge;

        public void Read(IMetaNode[] coll)
        {
            Plane = (short)coll[0].GetValue();
            FirstEdge = (short)coll[1].GetValue();
        }
    }
}
