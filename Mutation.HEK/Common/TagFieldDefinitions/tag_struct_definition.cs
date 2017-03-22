using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mutation.HEK.Guerilla;

namespace Mutation.HEK.Common.TagFieldDefinitions
{
    public class tag_struct_definition : tag_field
    {
        public int name_address;
        public int group_tag;
        public int display_name_address;
        public int block_definition_address;

        public override void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read the tag_field data.
            base.Read(h2LangLib, reader);

            // Seek to the definition address.
            reader.BaseStream.Position = this.definition_address - Guerilla.Guerilla.BaseAddress;

            // Read all the fields from the stream.
            this.name_address = reader.ReadInt32();
            this.group_tag = reader.ReadInt32();
            this.display_name_address = reader.ReadInt32();
            this.block_definition_address = reader.ReadInt32();
        }
    }
}
