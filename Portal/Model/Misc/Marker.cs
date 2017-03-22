using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HaloObjects;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Model
{
    public class Marker
    {
        public byte RegionIndex, PermutationIndex;
        public short NodeIndex;
        public Vector3 Translation;
        public Vector4 Rotation;
        public float Scale;

        public Mesh mesh;
        public SlimDX.Direct3D9.Material m = new SlimDX.Direct3D9.Material();

        public void Read(IMetaNode[] mode)
        {
            RegionIndex = (byte)mode[0].GetValue();
            PermutationIndex = (byte)mode[1].GetValue();
            NodeIndex = (short)mode[2].GetValue();
            Translation = new Vector3((float)mode[3].GetValue(), (float)mode[4].GetValue(), (float)mode[5].GetValue());
            Rotation = new Vector4((float)mode[6].GetValue(), (float)mode[7].GetValue(), (float)mode[8].GetValue(), (float)mode[9].GetValue());
            Scale = (float)mode[10].GetValue();
        }

        public void Initialize(Device device)
        {
            mesh = Mesh.CreateSphere(device, 0.5f, 5, 5);
            m.Ambient = System.Drawing.Color.Red;
        }

        public void Draw(Device device)
        {
            device.Material = m;
            device.SetTransform(TransformState.World, Matrix.Translation(Translation));
            mesh.DrawSubset(0);
        }
    }
}
