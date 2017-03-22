using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.H2.Xbox
{
    public class ParticleDefinition : RawDefinition
    {
        public ParticleDefinition() : base() { }
        public ParticleDefinition(TagDefinition TagDef, EndianReader br) : base(TagDef) 
        {
            // Create New Stream
            base.AddStream("ParticleRaw");

            // Get Values
            int Offset = (int)base.Owner[24].GetValue();
            int Size = (int)base.Owner[25].GetValue();

            // Double Check
            if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
            {
                // Write new Offset
                Offset &= 0x3FFFFFFF;
                base.Owner[24].SetValue((int)base["ParticleRaw"].Length);

                // Write Raw
                br.BaseStream.Position = Offset;
                byte[] Buffer = br.ReadBytes(Size);
                base["ParticleRaw"].Write(Buffer, 0, Buffer.Length);

                // Write Padding
                int Padding = 512 - ((int)base["ParticleRaw"].Length % 512);
                if (Padding != 512) base["ParticleRaw"].Write(new byte[Padding], 0, Padding);
            }
        }

        public override void FlushStream(string StreamName, int Ptr)
        {
            // Get Values
            int Offset = (int)base.Owner[24].GetValue();
            int Size = (int)base.Owner[25].GetValue();

            // Double Check
            if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
            {
                // Write new Offset
                base.Owner[24].SetValue(Offset + Ptr);
            }
        }
    }
}
