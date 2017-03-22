using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IO;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.H2.Xbox
{
    public class BitmapDefinition : RawDefinition
    {
        public BitmapDefinition() : base() { }
        public BitmapDefinition(TagDefinition TagDef, EndianReader br) : base(TagDef)
        {
            // Create a new stream
            base.AddStream("BitmapRaw");

            // Bitmaps
            for (int i = 0; i < ((TagBlock)base.Owner[20]).BlockCount; i++)
            {
                // 3 LODS
                for (int x = 0; x < 3; x++)
                {
                    // Get Values
                    int Offset = (int)((TagBlock)base.Owner[20])[i][12 + x].GetValue();
                    int Size = (int)((TagBlock)base.Owner[20])[i][18 + x].GetValue();

                    // Double Check
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                    {
                        // Write new Offset
                        Offset &= 0x3FFFFFFF;
                        ((TagBlock)base.Owner[20])[i][12 + x].SetValue((int)base["BitmapRaw"].Length);

                        // Write Raw
                        br.BaseStream.Position = Offset;
                        byte[] Buffer = br.ReadBytes(Size);
                        base["BitmapRaw"].Write(Buffer, 0, Buffer.Length);

                        // Write Padding
                        int Padding = 512 - ((int)base["BitmapRaw"].Length % 512);
                        if (Padding != 512) base["BitmapRaw"].Write(new byte[Padding], 0, Padding);
                    }
                }
            }
        }

        public override void FlushStream(string StreamName, int Ptr)
        {
            // Bitmaps
            for (int i = 0; i < ((TagBlock)base.Owner[20]).BlockCount; i++)
            {
                // 3 LODS
                for (int x = 0; x < 3; x++)
                {
                    // Get Values
                    int Offset = (int)((TagBlock)base.Owner[20])[i][12 + x].GetValue();
                    int Size = (int)((TagBlock)base.Owner[20])[i][18 + x].GetValue();

                    // Double Check
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)base.Owner[20])[i][12 + x].SetValue(Offset + Ptr);
                    }
                }
            }
        }
    }
}
