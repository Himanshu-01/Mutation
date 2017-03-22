using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mutation.HEK.Guerilla;

namespace Mutation.HEK.Common.TagFieldDefinitions
{
    public class enum_definition : tag_field
    {
        public int option_count;
        public int options_address;
        public int flags; //?

        public string[] options;

        public override void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read the tag_field data.
            base.Read(h2LangLib, reader);

            // Seek to the definition address.
            reader.BaseStream.Position = this.definition_address - Guerilla.Guerilla.BaseAddress;

            // Read all the fields from the stream.
            this.option_count = reader.ReadInt32();
            this.options_address = reader.ReadInt32();
            this.flags = reader.ReadInt32();

            // Loop through all of the options and read each one.
            this.options = new string[this.option_count];
            for (int i = 0; i < this.option_count; i++)
            {
                // Seek to the option address.
                reader.BaseStream.Position = this.options_address + (i * 4) - Guerilla.Guerilla.BaseAddress;

                // Read the option.
                int string_address = reader.ReadInt32();
                this.options[i] = string_address == 0 ? "" : Guerilla.Guerilla.ReadString(h2LangLib, reader, string_address);
            }
        }
    }
}
