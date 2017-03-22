using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.H2.Xbox
{
    public class SoundDefinition : RawDefinition
    {
        public SoundDefinition() : base() { }
        public SoundDefinition(TagDefinition TagDef, EndianReader br) : base(TagDef) 
        {
            // Add New Streams
            base.AddStream("SoundRaw");
            base.AddStream("ExtraInfo");

            // Sound Chunks
            for (int i = 0; i < ((TagBlock)base.Owner[8]).BlockCount; i++)
            {
                // Get Values
                int Offset = (int)((TagBlock)base.Owner[8])[i][0].GetValue();
                int Size = (int)((TagBlock)base.Owner[8])[i][1].GetValue() & 0x7FFFFFFF;

                // Double Check
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[8])[i][0].SetValue((int)base["SoundRaw"].Position);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["SoundRaw"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["SoundRaw"].Position % 512);
                    if (Padding != 512) base["SoundRaw"].Write(new byte[Padding], 0, Padding);
                }
            }

            // Extra Model Info
            for (int i = 0; i < ((TagBlock)base.Owner[10]).BlockCount; i++)
            {
                // Get Values
                int Offset = (int)((TagBlock)base.Owner[10])[i][1].GetValue();
                int Size = (int)((TagBlock)base.Owner[10])[i][2].GetValue();

                // Double Check
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[10])[i][1].SetValue((int)base["ExtraInfo"].Length);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["ExtraInfo"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["ExtraInfo"].Length % 512);
                    if (Padding != 512) base["ExtraInfo"].Write(new byte[Padding], 0, Padding);
                }
            }
        }

        public override void FlushStream(string StreamName, int Ptr)
        {
            if (StreamName == "SoundRaw")
            {
                // Sound Chunks
                for (int i = 0; i < ((TagBlock)base.Owner[8]).BlockCount; i++)
                {
                    // Get Values
                    int Offset = (int)((TagBlock)base.Owner[8])[i][0].GetValue();
                    int Size = (int)((TagBlock)base.Owner[8])[i][1].GetValue() & 0x7FFFFFFF;

                    // Double Check
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)base.Owner[8])[i][0].SetValue(Offset + Ptr);
                    }
                }
            }
            else if (StreamName == "ExtraInfo")
            {
                // Extra Model Info
                for (int i = 0; i < ((TagBlock)base.Owner[10]).BlockCount; i++)
                {
                    // Get Values
                    int Offset = (int)((TagBlock)base.Owner[10])[i][1].GetValue();
                    int Size = (int)((TagBlock)base.Owner[10])[i][2].GetValue();

                    // Double Check
                    if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                    {
                        // Write new Offset
                        ((TagBlock)base.Owner[10])[i][1].SetValue(Offset + Ptr);
                    }
                }
            }
        }
    }
}
