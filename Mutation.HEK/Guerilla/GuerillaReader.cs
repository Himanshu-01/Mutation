using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.HEK.Guerilla
{
    public class GuerillaReader
    {
        /// <summary>
        /// Gets the file path for the guerilla executable.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Gets an array of tag_group's that were found in the guerilla editor.
        /// </summary>
        public tag_group[] TagGroups { get; private set; }

        /// <summary>
        /// Gets a dictionary that can be used to access tag_block_definition structures via their address.
        /// </summary>
        public Dictionary<int, TagBlockDefinition> TagBlockDefinitions { get; private set; }

        // Binary reader for reading the executable file.
        BinaryReader reader;

        // Win32 handle to the h2langlib.dll used for loading field names.
        IntPtr h2LangLib = IntPtr.Zero;

        public GuerillaReader(string filePath)
        {
            // Save fields.
            this.FilePath = filePath;

            // Initialize the tag block dictionary.
            this.TagBlockDefinitions = new Dictionary<int, TagBlockDefinition>();
        }

        public bool Read()
        {
            // Try to load the h2langlib dll.
            this.h2LangLib = Guerilla.LoadLibrary(Guerilla.H2LanguageLibrary);
            if (this.h2LangLib == IntPtr.Zero)
            {
                // Failed to load the h2 language library.
                //MessageBox.Show("Failed to load the halo 2 language library!", "Guerilla Error", MessageBoxButtons.OK);
                return false;
            }

            // Open the guerilla executable.
            this.reader = new BinaryReader(new FileStream(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read));

            // Read each tag_group from the executable.
            this.TagGroups = new tag_group[Guerilla.NumberOfTagLayouts];
            for (int i = 0; i < Guerilla.NumberOfTagLayouts; i++)
            {
                // Initialize the current tag definition.
                this.TagGroups[i] = new tag_group();

                // Seek to the entry in the tag layout table.
                this.reader.BaseStream.Position = Guerilla.TagLayoutTableAddress - Guerilla.BaseAddress + (i * 4);

                // Read the tag layout address.
                int layoutAddress = this.reader.ReadInt32();

                // Seek to the tag layout and read it.
                this.reader.BaseStream.Position = layoutAddress - Guerilla.BaseAddress;
                this.TagGroups[i].Read(this.h2LangLib, this.reader);

                // Read the tag block definition.
                ReadTagBlockDefinition(this.TagGroups[i].definition_address, this.TagGroups[i].GroupTag.Equals("snd!"), null);

                // Mark the tag definition as a tag group.
                this.TagBlockDefinitions[this.TagGroups[i].definition_address].IsTagGroup = true;
            }

            // Close the reader.
            this.reader.Close();

            // Done.
            return true;
        }

        private void ReadTagBlockDefinition(int address, bool isSound, TagBlockDefinition parent)
        {
            // Check if this tag_block_definition has already been read.
            if (this.TagBlockDefinitions.ContainsKey(address) == true)
            {
                //// Check if this definition has a parent and if so add a reference to it.
                //if (parent != null)
                //{
                //    // Add the parent defintion as a reference.
                //    this.TagBlockDefinitions[address].AddReference(parent);
                //}

                // Don't process the tag block.
                return;
            }

            // Seek to the definition address.
            this.reader.BaseStream.Position = address - Guerilla.BaseAddress;

            // Initialize and read the tag_block_definition struct.
            TagBlockDefinition tagBlockDef = new TagBlockDefinition();
            tagBlockDef.s_tag_block_definition.Read(h2LangLib, reader);

            // Special case tags may need the field set addresses adjusted.
            if (isSound == true)
            {
                tagBlockDef.s_tag_block_definition.field_sets_address = 0;// 0x906178;
                tagBlockDef.s_tag_block_definition.field_set_count = 6;
                tagBlockDef.s_tag_block_definition.field_set_latest_address = 0x906178;
            }

            // Initialize the tag field set arrays.
            tagBlockDef.TagFieldSets = new tag_field_set[tagBlockDef.s_tag_block_definition.field_set_count];
            tagBlockDef.TagFields = new tag_field[tagBlockDef.s_tag_block_definition.field_set_count][];

            //// Check if this definition has a parent and if so add a reference to it.
            //if (parent != null)
            //{
            //    // Add the parent defintion as a reference.
            //    tagBlockDef.AddReference(parent);
            //}

            // Add the tag block definition to the dictionary.
            this.TagBlockDefinitions.Add(address, tagBlockDef);

            // Loop through all the tag_field_set's and read each one.
            for (int i = 0; i < tagBlockDef.s_tag_block_definition.field_set_count; i++)
            {
                // Special case tags may need the field set addresses adjusted.
                if (isSound == true)
                {
                    // Initialize the field set.
                    tagBlockDef.TagFieldSets[i] = new tag_field_set();

                    // Manuall fill in the information.
                    if (i == 0)
                    {
                        // Seek to the tag_field_set definition address.
                        reader.BaseStream.Position = 0x957A60 - Guerilla.BaseAddress;
                        tagBlockDef.TagFieldSets[i].Read(h2LangLib, reader);
                    }
                    else if (i == 1 || i == 2 || i == 3) // 2 & 3
                    {
                        // Seek to the tag_field_set definition address.
                        reader.BaseStream.Position = 0x957448 - Guerilla.BaseAddress;
                        tagBlockDef.TagFieldSets[i].Read(h2LangLib, reader);
                    }
                    else if (i == 4) // 4
                    {
                        tagBlockDef.TagFieldSets[i].version.fields_address = 0x906078;
                        tagBlockDef.TagFieldSets[i].version.index = 0;
                        tagBlockDef.TagFieldSets[i].version.upgrade_proc = 0x49F700;
                        tagBlockDef.TagFieldSets[i].version.size_of = -1;
                        tagBlockDef.TagFieldSets[i].size = 176;
                        tagBlockDef.TagFieldSets[i].alignment_bit = 0;
                        tagBlockDef.TagFieldSets[i].parent_version_index = -1;
                        tagBlockDef.TagFieldSets[i].fields_address = 0x906078;
                        tagBlockDef.TagFieldSets[i].size_string = "sizeof(struct sound_definition_v1)";
                    }
                    else if (i == 5) // 5
                    {
                        tagBlockDef.TagFieldSets[i].version.fields_address = 0x906178;
                        tagBlockDef.TagFieldSets[i].version.index = 0;
                        tagBlockDef.TagFieldSets[i].version.upgrade_proc = 0;
                        tagBlockDef.TagFieldSets[i].version.size_of = -1;
                        tagBlockDef.TagFieldSets[i].size = 172;
                        tagBlockDef.TagFieldSets[i].alignment_bit = 0;
                        tagBlockDef.TagFieldSets[i].parent_version_index = -1;
                        tagBlockDef.TagFieldSets[i].fields_address = 0x906178;
                        tagBlockDef.TagFieldSets[i].size_string = "sizeof(sound_definition)";
                    }
                }
                else
                {
                    // Seek to the tag_field_set definition address.
                    reader.BaseStream.Position = tagBlockDef.s_tag_block_definition.field_sets_address + (i * 76) - Guerilla.BaseAddress;

                    // Initialize and read the tag_field_set struct.
                    tagBlockDef.TagFieldSets[i] = new tag_field_set();
                    tagBlockDef.TagFieldSets[i].Read(h2LangLib, reader);
                }

                // Check if this is the latest tag field set.
                if (tagBlockDef.s_tag_block_definition.field_set_latest_address == tagBlockDef.TagFieldSets[i].fields_address)
                    tagBlockDef.TagFieldSetLatestIndex = i;

                // Create a temporary list of tag_fields.
                List<tag_field> tagFields = new List<tag_field>();

                // Seek to the tag field set address.
                this.reader.BaseStream.Position = tagBlockDef.TagFieldSets[i].fields_address - Guerilla.BaseAddress;

                // Loop through all the tag fields and read each one.
                tag_field field = new tag_field();
                do
                {
                    // Save the current address.
                    long currentAddress = this.reader.BaseStream.Position;

                    // Read the field type from the stream.
                    field_type fieldType = (field_type)this.reader.ReadInt16();

                    // Create the field type accordingly.
                    switch (fieldType)
                    {
                        case field_type._field_tag_reference: field = new tag_reference_definition(); break;
                        case field_type._field_struct: field = new tag_struct_definition(); break;
                        case field_type._field_data: field = new tag_data_definition(); break;
                        case field_type._field_byte_flags:
                        case field_type._field_long_flags:
                        case field_type._field_word_flags:
                        case field_type._field_char_enum:
                        case field_type._field_enum:
                        case field_type._field_long_enum: field = new enum_definition(); break;
                        case field_type._field_char_block_index2:
                        case field_type._field_short_block_index2:
                        case field_type._field_long_block_index2: field = new block_index_custom_search_definition(); break;
                        case field_type._field_explanation: field = new explaination_definition(); break;
                        default: field = new tag_field(); break;
                    }

                    // Read the tag field from the stream.
                    this.reader.BaseStream.Position = currentAddress;
                    field.Read(h2LangLib, reader);

                    // Add the field to the list.
                    tagFields.Add(field);

                    // Read any tag_block definitions.
                    switch (field.type)
                    {
                        case field_type._field_byte_block_flags:
                        case field_type._field_word_block_flags:
                        case field_type._field_long_block_flags:
                        case field_type._field_char_integer:
                        case field_type._field_short_integer:
                        case field_type._field_long_integer:
                        case field_type._field_block:
                            {
                                // Check if the definition address is valid.
                                if (field.definition_address != 0)
                                    ReadTagBlockDefinition(field.definition_address, false, tagBlockDef);
                                break;
                            }
                        case field_type._field_struct:
                            {
                                ReadTagBlockDefinition(((tag_struct_definition)field).block_definition_address, false, tagBlockDef);
                                break;
                            }
                    }

                    // Seek to the next tag_field.
                    this.reader.BaseStream.Position = currentAddress + 16; //sizeof(tag_field)
                }
                while (field.type != field_type._field_terminator);

                // Add the list of tag_fields to the array.
                tagBlockDef.TagFields[i] = tagFields.ToArray();
            }
        }
    }
}
