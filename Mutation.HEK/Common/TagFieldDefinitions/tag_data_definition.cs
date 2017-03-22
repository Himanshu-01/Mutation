using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mutation.HEK.Guerilla;

namespace Mutation.HEK.Common.TagFieldDefinitions
{
    public class tag_data_definition : tag_field
    {
        public int definition_name_address;
        public int flags;
        public int alignment_bit;
        public int maximum_size;
        public int maximum_size_string_address;
        public int byteswap_proc;
        public int copy_proc;

        private string definition_name;
        /// <summary>
        /// Gets the name of the data definition.
        /// </summary>
        public string DefinitionName { get { return this.definition_name; } }

        private string maximum_size_string;
        /// <summary>
        /// Gets the string representation of the maximum size of the tag data definition.
        /// </summary>
        public string MaximumSize { get { return this.maximum_size_string; } }

        public override void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read the tag_field data.
            base.Read(h2LangLib, reader);

            // Seek to the definition address.
            reader.BaseStream.Position = this.definition_address - Guerilla.Guerilla.BaseAddress;

            // Read all the fields from the stream.
            this.definition_name_address = reader.ReadInt32();
            this.flags = reader.ReadInt32();
            this.alignment_bit = reader.ReadInt32();
            this.maximum_size = reader.ReadInt32();
            this.maximum_size_string_address = reader.ReadInt32();
            this.byteswap_proc = reader.ReadInt32();
            this.copy_proc = reader.ReadInt32();

            // Read the strings.
            this.definition_name = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.definition_name_address);
            this.maximum_size_string = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.maximum_size_string_address);
        }
    }
}
