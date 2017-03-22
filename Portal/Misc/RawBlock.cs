using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;

namespace Portal
{
    public class Resource
    {
        public short PrimaryId, SecondaryId;
        public int BlockType, Offset, Size;
    }

    public class RawBlock
    {
        public int Offset, Size, HeaderSize, DataSize;
        public Resource[] Resources;

        public void Read(IMetaNode[] mode, int Index)
        {
            Resources = new Resource[((H2XTagBlock)mode[Index]).GetChunkCount()];
            for (int i = 0; i < Resources.Length; i++)
            {
                ((H2XTagBlock)mode[Index]).ChangeIndex(i);
                IMetaNode[] Fields = (IMetaNode[])mode[Index].GetValue();
                Resources[i] = new Resource();
                Resources[i].BlockType = (int)Fields[0].GetValue();
                Resources[i].PrimaryId = (short)Fields[1].GetValue();
                Resources[i].SecondaryId = (short)Fields[2].GetValue();
                Resources[i].Size = (int)Fields[3].GetValue();
                Resources[i].Offset = (int)Fields[4].GetValue();
            }
        }
    }
}
