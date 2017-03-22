using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.H2.Xbox
{
    public class LightmapDefinition : RawDefinition
    {
        public LightmapDefinition() : base() { }
        public LightmapDefinition(TagDefinition TagDef, EndianReader br) : base(TagDef) 
        {
            // Create New Stream
            base.AddStream("LightmapRaw");

            // Groups
            for (int i = 0; i < ((TagBlock)base.Owner[15]).BlockCount; i++)
            {
                // Clustors
                for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[15])[i][6]).BlockCount; x++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)((TagBlock)base.Owner[15])[i][6])[x][18].GetValue();
                    int Size = (int)((TagBlock)((TagBlock)base.Owner[15])[i][6])[x][19].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                    {
                        // Write new Offset
                        Offset &= 0x3FFFFFFF;
                        ((TagBlock)((TagBlock)base.Owner[15])[i][6])[x][18].SetValue((int)base["LightmapRaw"].Length);

                        // Write Raw
                        br.BaseStream.Position = Offset;
                        byte[] Buffer = br.ReadBytes(Size);
                        base["LightmapRaw"].Write(Buffer, 0, Buffer.Length);

                        // Write Padding
                        int Padding = 512 - ((int)base["LightmapRaw"].Length % 512);
                        if (Padding != 512) base["LightmapRaw"].Write(new byte[Padding], 0, Padding);
                    }
                }

                // Poop Definitions
                for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[15])[i][8]).BlockCount; x++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)((TagBlock)base.Owner[15])[i][8])[x][18].GetValue();
                    int Size = (int)((TagBlock)((TagBlock)base.Owner[15])[i][8])[x][19].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                    {
                        // Write new Offset
                        Offset &= 0x3FFFFFFF;
                        ((TagBlock)((TagBlock)base.Owner[15])[i][8])[x][18].SetValue((int)base["LightmapRaw"].Length);

                        // Write Raw
                        br.BaseStream.Position = Offset;
                        byte[] Buffer = br.ReadBytes(Size);
                        base["LightmapRaw"].Write(Buffer, 0, Buffer.Length);

                        // Write Padding
                        int Padding = 512 - ((int)base["LightmapRaw"].Length % 512);
                        if (Padding != 512) base["LightmapRaw"].Write(new byte[Padding], 0, Padding);
                    }
                }

                // Geometry Buckets
                for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[15])[i][10]).BlockCount; x++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)((TagBlock)base.Owner[15])[i][10])[x][2].GetValue();
                    int Size = (int)((TagBlock)((TagBlock)base.Owner[15])[i][10])[x][3].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                    {
                        // Write new Offset
                        Offset &= 0x3FFFFFFF;
                        ((TagBlock)((TagBlock)base.Owner[15])[i][10])[x][2].SetValue((int)base["LightmapRaw"].Length);

                        // Write Raw
                        br.BaseStream.Position = Offset;
                        byte[] Buffer = br.ReadBytes(Size);
                        base["LightmapRaw"].Write(Buffer, 0, Buffer.Length);

                        // Write Padding
                        int Padding = 512 - ((int)base["LightmapRaw"].Length % 512);
                        if (Padding != 512) base["LightmapRaw"].Write(new byte[Padding], 0, Padding);
                    }
                }
            }
        }

        public override void FlushStream(string StreamName, int Ptr)
        {
            // Groups
            for (int i = 0; i < ((TagBlock)base.Owner[15]).BlockCount; i++)
            {
                // Clustors
                for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[15])[i][6]).BlockCount; x++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)((TagBlock)base.Owner[15])[i][6])[x][18].GetValue();
                    int Size = (int)((TagBlock)((TagBlock)base.Owner[15])[i][6])[x][19].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)((TagBlock)base.Owner[15])[i][6])[x][18].SetValue(Offset + Ptr);
                    }
                }

                // Poop Definitions
                for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[15])[i][8]).BlockCount; x++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)((TagBlock)base.Owner[15])[i][8])[x][18].GetValue();
                    int Size = (int)((TagBlock)((TagBlock)base.Owner[15])[i][8])[x][19].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)((TagBlock)base.Owner[15])[i][8])[x][18].SetValue(Offset + Ptr);
                    }
                }

                // Geometry Buckets
                for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[15])[i][10]).BlockCount; x++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)((TagBlock)base.Owner[15])[i][10])[x][2].GetValue();
                    int Size = (int)((TagBlock)((TagBlock)base.Owner[15])[i][10])[x][3].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)((TagBlock)base.Owner[15])[i][10])[x][2].SetValue(Offset + Ptr);
                    }
                }
            }
        }
    }
}
