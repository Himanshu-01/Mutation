using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mutation.HEK.Guerilla;

namespace Mutation.HEK.Common.TagFieldDefinitions
{
    public class block_index_custom_search_definition : tag_field
    {
        public int get_block_proc;
        public int is_valid_source_block_proc;

        public override void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read the tag_field data.
            base.Read(h2LangLib, reader);

            // Seek to the definition address.
            reader.BaseStream.Position = this.definition_address - Guerilla.Guerilla.BaseAddress;

            // Read the fields from the stream.
            this.get_block_proc = reader.ReadInt32();
            this.is_valid_source_block_proc = reader.ReadInt32();
        }
    }
}
