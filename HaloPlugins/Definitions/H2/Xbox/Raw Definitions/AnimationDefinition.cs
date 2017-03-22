using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.H2.Xbox
{
    public class AnimationDefinition : RawDefinition
    {
        public AnimationDefinition() : base() { }
        public AnimationDefinition(TagDefinition TagDef, EndianReader br) : base(TagDef) 
        { 
            // Create New Stream
            base.AddStream("AnimationRaw");

            // Animations
            for (int i = 0; i < ((TagBlock)base.Owner[21]).BlockCount; i++)
            {
                // Get Offset and Size
                int Size = (int)((TagBlock)base.Owner[21])[i][1].GetValue();
                int Offset = (int)((TagBlock)base.Owner[21])[i][2].GetValue();

                // Write Raw
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[21])[i][2].SetValue((int)base["AnimationRaw"].Length);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["AnimationRaw"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["AnimationRaw"].Length % 512);
                    if (Padding != 512) base["AnimationRaw"].Write(new byte[Padding], 0, Padding);
                }
            }
        }

        public override void FlushStream(string StreamName, int Ptr)
        {
            // Animations
            for (int i = 0; i < ((TagBlock)base.Owner[21]).BlockCount; i++)
            {
                // Get Offset and Size
                int Size = (int)((TagBlock)base.Owner[21])[i][1].GetValue();
                int Offset = (int)((TagBlock)base.Owner[21])[i][2].GetValue();

                // Write Raw
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                {
                    // Write new Offset
                    ((TagBlock)base.Owner[21])[i][2].SetValue(Offset + Ptr);
                }
            }
        }
    }
}
