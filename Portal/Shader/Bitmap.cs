using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DDS;

namespace Portal
{
    public class Bitmap
    {
        public short Width, Height, Depth, Type, Format, MipMapCount, PixelOffset;
        public int[] LOD = new int[3];
        public int[] Size = new int[3];
        public bool Swizzled;
        public string Name;
        DDSImage DDS;
        public MemoryStream Stream;
        //public System.Drawing.Bitmap Bitmaps;

        public Bitmap(string TagPath)
        {
            // Open
            this.Name = TagPath;
            BinaryReader br = new BinaryReader(new FileStream(TagPath, FileMode.Open, FileAccess.Read, FileShare.Read));
            br.BaseStream.Position = 512 + 68;
            int ChunkCount = br.ReadInt32();
            int Translation = br.ReadInt32();
            if (ChunkCount > 0)
            {

                // Read Info
                br.BaseStream.Position = Translation + 4;
                Width = br.ReadInt16();
                Height = br.ReadInt16();
                Depth = br.ReadInt16();
                Type = br.ReadInt16();
                Format = br.ReadInt16();
                Swizzled = (br.ReadInt16() & 0x8) == 0x8;
                br.BaseStream.Position += 4;
                MipMapCount = br.ReadInt16();
                PixelOffset = br.ReadInt16();
                br.BaseStream.Position += 4;
                LOD[0] = br.ReadInt32();
                br.BaseStream.Position += 20;
                Size[0] = br.ReadInt32();

                // Open
                if (LOD[0] < 0x2FFFFFFF && LOD[0] >= 0)
                    br = new BinaryReader(new FileStream(TagPath.Replace(".bitmap", ".bitmapraw"), FileMode.Open, FileAccess.Read, FileShare.Read));
                else
                    br = RawData.OpenMap(ref LOD[0]);

                // Read
                br.BaseStream.Position = LOD[0];
                DDS = new DDSImage(Height, Width, br.ReadBytes(Size[0]), (BitmapFormat)Format,
                    (BitmapType)Type, Swizzled, Depth, PixelOffset, MipMapCount);
                //Bitmaps = DDS.GetImage();
                Stream = DDS.ToStream();
            }
            br.Close();
        }
    }
}
