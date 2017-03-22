using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mutation.HEK.Guerilla;

namespace Mutation.HEK.Common.TagFieldDefinitions
{
    public class tag_reference_definition : tag_field
    {
        public int flags;
        public int group_tag;
        public int group_tags_address;

        public override void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read the tag_field data.
            base.Read(h2LangLib, reader);

            // Seek to the definition address.
            reader.BaseStream.Position = this.definition_address - Guerilla.Guerilla.BaseAddress;

            // Read all the fields from the stream.
            this.flags = reader.ReadInt32();
            this.group_tag = reader.ReadInt32();
            this.group_tags_address = reader.ReadInt32();
        }
    }
}
