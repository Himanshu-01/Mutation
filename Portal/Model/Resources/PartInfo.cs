using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Model
{
    public class PartInfo
    {
        public short Unknown1, Unknown2, ShaderIndex, IndiceStart;
        public int VertStart, VertCount;
        public int IndiceCount;
        public int[] Unknown3 = new int[9];
        public float XMin, XMax;
        public float YMin, YMax;
        public float ZMin, ZMax;

        public void Read(BinaryReader br)
        {
            Unknown1 = br.ReadInt16();
            Unknown2 = br.ReadInt16();
            ShaderIndex = br.ReadInt16();
            IndiceStart = br.ReadInt16();
            IndiceCount = br.ReadInt16();
            br.BaseStream.Position += 2;
            for (int i = 0; i < 9; i++)
                Unknown3[i] = br.ReadInt32();
            XMin = br.ReadSingle();
            XMax = br.ReadSingle();
            YMin = br.ReadSingle();
            YMax = br.ReadSingle();
            ZMin = br.ReadSingle();
            ZMax = br.ReadSingle();
        }
    }
}
