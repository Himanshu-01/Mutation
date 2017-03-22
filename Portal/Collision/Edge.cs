using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal.Collision
{
    public class Edge
    {
        public short StartVert, EndVert, FowardEdge, ReverseEdge, LeftSurface, RightSurface;

        public void Read(IMetaNode[] coll)
        {
            StartVert = (short)coll[0].GetValue();
            EndVert = (short)coll[1].GetValue();
            FowardEdge = (short)coll[2].GetValue();
            ReverseEdge = (short)coll[3].GetValue();
            LeftSurface = (short)coll[4].GetValue();
            RightSurface = (short)coll[5].GetValue();
        }
    }
}
