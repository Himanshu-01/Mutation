using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Global;
using HaloObjects;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Model
{
    public class MarkerGroup
    {
        public string Name;
        public Marker[] Markers;

        public void Read(IMetaNode[] mode)
        {
            Name = (string)mode[0].GetValue();
            Markers = new Marker[((H2XTagBlock)mode[1]).GetChunkCount()];
            for (int i = 0; i < Markers.Length; i++)
            {
                ((H2XTagBlock)mode[1]).ChangeIndex(i);
                Markers[i] = new Marker();
                Markers[i].Read((IMetaNode[])mode[1].GetValue());
            }
        }

        public void Initialize(Device device)
        {
            for (int i = 0; i < Markers.Length; i++)
                Markers[i].Initialize(device);
        }

        public void Draw(Device device)
        {
            for (int i = 0; i < Markers.Length; i++)
                Markers[i].Draw(device);
        }
    }
}
