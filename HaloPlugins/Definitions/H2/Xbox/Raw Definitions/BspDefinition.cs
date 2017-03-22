using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.H2.Xbox
{
    public class BspDefinition : RawDefinition
    {
        public BspDefinition() : base() { }
        public BspDefinition(TagDefinition TagDef, EndianReader br) : base(TagDef) 
        {
            // Create New Streams
            base.AddStream("Bsp1");
            base.AddStream("Bsp2");
            base.AddStream("Bsp3");
            base.AddStream("Bsp4");

            // Detail Objects
            for (int i = 0; i < ((TagBlock)base.Owner[26]).BlockCount; i++)
            {
                // Get Offset and Size
                int Offset = (int)((TagBlock)base.Owner[26])[i][19].GetValue();
                int Size = (int)((TagBlock)base.Owner[26])[i][20].GetValue();

                // Write Raw
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[26])[i][19].SetValue((int)base["Bsp1"].Length);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["Bsp1"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["Bsp1"].Length % 512);
                    if (Padding != 512) base["Bsp1"].Write(new byte[Padding], 0, Padding);
                }
            }

            // Permutations
            for (int i = 0; i < ((TagBlock)base.Owner[41]).BlockCount; i++)
            {
                // Get Offset and Size
                int Offset = (int)((TagBlock)base.Owner[41])[i][19].GetValue();
                int Size = (int)((TagBlock)base.Owner[41])[i][20].GetValue();

                // Write Raw
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[41])[i][19].SetValue((int)base["Bsp2"].Length);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["Bsp2"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["Bsp2"].Length % 512);
                    if (Padding != 512) base["Bsp2"].Write(new byte[Padding], 0, Padding);
                }
            }

            // Water Definitions
            for (int i = 0; i < ((TagBlock)base.Owner[63]).BlockCount; i++)
            {
                // Get Offset and Size
                int Offset = (int)((TagBlock)base.Owner[63])[i][2].GetValue();
                int Size = (int)((TagBlock)base.Owner[63])[i][3].GetValue();

                // Write Raw
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[63])[i][2].SetValue((int)base["Bsp3"].Length);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["Bsp3"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["Bsp3"].Length % 512);
                    if (Padding != 512) base["Bsp3"].Write(new byte[Padding], 0, Padding);
                }
            }

            // Decorators
            for (int i = 0; i < ((TagBlock)base.Owner[67]).BlockCount; i++)
            {
                // Groups
                for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[67])[i][4]).BlockCount; x++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)((TagBlock)base.Owner[67])[i][4])[x][0].GetValue();
                    int Size = (int)((TagBlock)((TagBlock)base.Owner[67])[i][4])[x][1].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                    {
                        // Write new Offset
                        Offset &= 0x3FFFFFFF;
                        ((TagBlock)((TagBlock)base.Owner[67])[i][4])[x][0].SetValue((int)base["Bsp4"].Length);

                        // Write Raw
                        br.BaseStream.Position = Offset;
                        byte[] Buffer = br.ReadBytes(Size);
                        base["Bsp4"].Write(Buffer, 0, Buffer.Length);

                        // Write Padding
                        int Padding = 512 - ((int)base["Bsp4"].Length % 512);
                        if (Padding != 512) base["Bsp4"].Write(new byte[Padding], 0, Padding);
                    }
                }
            }
        }

        public override void FlushStream(string StreamName, int Ptr)
        {
            if (StreamName == "Bsp1")
            {
                // Detail Objects
                for (int i = 0; i < ((TagBlock)base.Owner[26]).BlockCount; i++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)base.Owner[26])[i][19].GetValue();
                    int Size = (int)((TagBlock)base.Owner[26])[i][20].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset;
                        ((TagBlock)base.Owner[26])[i][19].SetValue(Offset + Ptr);
                    }
                }
            }
            else if (StreamName == "Bsp2")
            {
                // Permutations
                for (int i = 0; i < ((TagBlock)base.Owner[41]).BlockCount; i++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)base.Owner[41])[i][19].GetValue();
                    int Size = (int)((TagBlock)base.Owner[41])[i][20].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)base.Owner[41])[i][19].SetValue(Offset + Ptr);
                    }
                }
            }
            else if (StreamName == "Bsp3")
            {
                // Water Definitions
                for (int i = 0; i < ((TagBlock)base.Owner[63]).BlockCount; i++)
                {
                    // Get Offset and Size
                    int Offset = (int)((TagBlock)base.Owner[63])[i][2].GetValue();
                    int Size = (int)((TagBlock)base.Owner[63])[i][3].GetValue();

                    // Write Raw
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)base.Owner[63])[i][2].SetValue(Offset + Ptr);
                    }
                }
            }
            else if (StreamName == "Bsp4")
            {
                // Decorators
                for (int i = 0; i < ((TagBlock)base.Owner[67]).BlockCount; i++)
                {
                    // Groups
                    for (int x = 0; x < ((TagBlock)((TagBlock)base.Owner[67])[i][4]).BlockCount; x++)
                    {
                        // Get Offset and Size
                        int Offset = (int)((TagBlock)((TagBlock)base.Owner[67])[i][4])[x][0].GetValue();
                        int Size = (int)((TagBlock)((TagBlock)base.Owner[67])[i][4])[x][1].GetValue();

                        // Write Raw
                        if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                        {
                            // Write new Offset
                            ((TagBlock)((TagBlock)base.Owner[67])[i][4])[x][0].SetValue(Offset + Ptr);
                        }
                    }
                }
            }
        }
    }
}
