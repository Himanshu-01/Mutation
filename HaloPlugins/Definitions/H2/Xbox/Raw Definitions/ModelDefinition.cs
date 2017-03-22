using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.H2.Xbox
{
    public class ModelDefinition : RawDefinition
    {
        public ModelDefinition() : base() { }
        public ModelDefinition(TagDefinition TagDef, EndianReader br) : base(TagDef) 
        { 
            // Add New Stream
            base.AddStream("ModelRaw");

            // Mesh Sections
            for (int i = 0; i < ((TagBlock)base.Owner[6]).BlockCount; i++)
            {
                // Get Values
                int Offset = (int)((TagBlock)base.Owner[6])[i][26].GetValue();
                int Size = (int)((TagBlock)base.Owner[6])[i][27].GetValue();

                // Double Check
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[6])[i][26].SetValue((int)base["ModelRaw"].Length);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["ModelRaw"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["ModelRaw"].Length % 512);
                    if (Padding != 512) base["ModelRaw"].Write(new byte[Padding], 0, Padding);
                }
            }

            // Lightmap Raw
            for (int i = 0; i < ((TagBlock)base.Owner[21]).BlockCount; i++)
            {
                // Get Values
                int Offset = (int)((TagBlock)base.Owner[21])[i][11].GetValue();
                int Size = (int)((TagBlock)base.Owner[21])[i][12].GetValue();

                // Double Check
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > 0 && Size > 0)
                {
                    // Write new Offset
                    Offset &= 0x3FFFFFFF;
                    ((TagBlock)base.Owner[21])[i][11].SetValue((int)base["ModelRaw"].Length);

                    // Write Raw
                    br.BaseStream.Position = Offset;
                    byte[] Buffer = br.ReadBytes(Size);
                    base["ModelRaw"].Write(Buffer, 0, Buffer.Length);

                    // Write Padding
                    int Padding = 512 - ((int)base["ModelRaw"].Length % 512);
                    if (Padding != 512) base["ModelRaw"].Write(new byte[Padding], 0, Padding);
                }
            }
        }

        public override void FlushStream(string StreamName, int Ptr)
        {
            // Mesh Sections
            for (int i = 0; i < ((TagBlock)base.Owner[6]).BlockCount; i++)
            {
                // Get Values
                int Offset = (int)((TagBlock)base.Owner[6])[i][26].GetValue();
                int Size = (int)((TagBlock)base.Owner[6])[i][27].GetValue();

                // Double Check
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                {
                    // Write new Offset
                    ((TagBlock)base.Owner[6])[i][26].SetValue(Offset + Ptr);
                }
            }

            // Lightmap Raw
            for (int i = 0; i < ((TagBlock)base.Owner[21]).BlockCount; i++)
            {
                // Get Values
                int Offset = (int)((TagBlock)base.Owner[21])[i][11].GetValue();
                int Size = (int)((TagBlock)base.Owner[21])[i][12].GetValue();

                // Double Check
                if ((Offset & 0xC0000000) == 0 && (Offset & 0x3FFFFFFF) > -1 && Size > 0)
                {
                    // Write new Offset
                    ((TagBlock)base.Owner[21])[i][11].SetValue(Offset + Ptr);
                }
            }
        }
    }
}
