using Mutation.HEK.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.HEK.Guerilla
{
    public static class Guerilla
    {
        /// <summary>
        /// The image load address used for translating virtual addresses to physical addresses.
        /// </summary>
        public const int BaseAddress = 0x400000;

        /// <summary>
        /// Virtual address of the tag layout table.
        /// </summary>
        public const int TagLayoutTableAddress = 0x00901B90;

        /// <summary>
        /// The number of tag layouts in the tag layout table.
        /// </summary>
        public const int NumberOfTagLayouts = 120;

        /// <summary>
        /// Name of the h2 language library used for localizing user interface strings.
        /// </summary>
        public const string H2LanguageLibrary = "h2alang.dll";

        #region Imports

        [DllImport("kernel32")]
        public extern static IntPtr LoadLibrary(string librayName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        #endregion

        public static void DumpTagLayouts(string exePath, string folder)
        {
            // Open the guerilla executable.
            BinaryReader reader = new BinaryReader(new FileStream(exePath, FileMode.Open, FileAccess.Read, FileShare.Read));

            // Load the h2 language library.
            IntPtr h2LangLib = LoadLibrary(H2LanguageLibrary);

            // Loop through all the tag layouts and extract each one.
            for (int i = 0; i < NumberOfTagLayouts; i++)
            {
                // Go to the tag layout table.
                reader.BaseStream.Position = TagLayoutTableAddress - BaseAddress + (i * 4);

                // Read the tag layout pointer.
                int layoutAddress = reader.ReadInt32();

                // Go to the tag layout and read it.
                reader.BaseStream.Position = layoutAddress - BaseAddress;
                tag_group tag = new tag_group();
                tag.Read(h2LangLib, reader);

                // Print the name of the layout.
                Console.WriteLine("found tag_group: {0}", tag.Name);

                // Create the tag layout file.
                StreamWriter writer = new StreamWriter(string.Format("{0}\\{1}.h", folder, tag.Name));

                // Write a header for the layout.
                writer.WriteLine("/*********************************************************************");
                writer.WriteLine("* name: {0}", tag.Name);
                writer.WriteLine("* flags: {0}", tag.flags);
                writer.WriteLine("* group_tag: {0}", tag.GroupTag);
                writer.WriteLine("* parent group_tag: {0}", tag.ParentGroupTag);
                writer.WriteLine("* version: {0}", tag.version);
                writer.WriteLine("*");
                writer.WriteLine("* postprocess_proc:\t\t0x{0}", tag.postprocess_proc.ToString("x08"));
                writer.WriteLine("* save_postprocess_proc:\t0x{0}", tag.save_postprocess_proc.ToString("x08"));
                writer.WriteLine("* postprocess_for_sync_proc:\t0x{0}", tag.postprocess_for_sync_proc.ToString("x08"));
                writer.WriteLine("**********************************************************************/");

                // Process the tag_group definition.
                //ProcessTagBlockDefinition(h2LangLib, reader, writer, tag.definition_address, tag.GroupTag, new tag_field(), "");

                // Close the tag layout writer.
                writer.Close();
            }

            // Close the reader stream.
            reader.Close();
        }

        /*
        public static void ProcessTagBlockDefinition(IntPtr h2LangLib, BinaryReader reader, StreamWriter writer, int address, string group_tag, tag_field parent, string tab)
        {
            // Seek to the tag_block_definition address.
            reader.BaseStream.Position = address - BaseAddress;

            // Read the tag_block_definition struct from the stream.
            tag_block_definition definition = new tag_block_definition();
            definition.Read(h2LangLib, reader);

            // Check how many field sets there are for this definition.
            if (definition.field_set_count > 1)
            {
                // Print a message to the console.
                Console.WriteLine("found {0} field sets for tag_block_definition {1}!", definition.field_set_count, definition.DisplayName);
            }

            // Special case tags may need the field set addresses adjusted.
            tag_field_set field_set = new tag_field_set();
            if (group_tag == "snd!")
            {
                definition.field_sets_address = 0x957870;
                //definition.field_set_latest_address = 0x906178;
                field_set.version.fields_address = 0x906178;
                field_set.version.index = 0;
                field_set.version.upgrade_proc = 0;
                field_set.version.size_of = -1;
                field_set.size = 172;
                field_set.alignment_bit = 0;
                field_set.parent_version_index = -1;
                field_set.fields_address = 0x906178;
                field_set.size_string_address = 0x00795330;
                field_set.size_string = "sizeof(sound_definition)";
            }
            else
            {
                // We are going to use the latest tag_field_set for right now.
                reader.BaseStream.Position = definition.field_set_latest_address - BaseAddress;
                field_set.Read(h2LangLib, reader);
            }

            // Create the main struct for this tag field set in the output file.
            string structName = field_set.SizeString.Replace("sizeof(struct ", "").Replace("sizeof(", "").Replace(")", "");
            writer.WriteLine();
            writer.WriteLine(tab + "/****************************************");
            writer.WriteLine(tab + "* size:\t{0}", field_set.size);
            writer.WriteLine(tab + "* alignment:\t{0}", field_set.alignment_bit != 0 ? (1 << field_set.alignment_bit) : 0);
            if (IsNumeric(definition.MaximumElementCount) == true)
                writer.WriteLine(tab + "* max_count:\t{0}", definition.maximum_element_count);
            else
                writer.WriteLine(tab + "* max_count:\t{0} = {1}", definition.MaximumElementCount, definition.maximum_element_count);
            writer.WriteLine(tab + "*");

            // Print the proc addresses only if they exist.
            if (definition.postprocess_proc != 0) writer.WriteLine(tab + "* postprocess_proc: 0x{0}", definition.postprocess_proc.ToString("x08"));
            if (definition.format_proc != 0) writer.WriteLine(tab + "* format_proc: 0x{0}", definition.format_proc.ToString("x08"));
            if (definition.generate_default_proc != 0) writer.WriteLine(tab + "* generate_default_proc: 0x{0}", definition.generate_default_proc.ToString("x08"));
            if (definition.dispose_element_proc != 0) writer.WriteLine(tab + "* dispose_element_proc: 0x{0}", definition.dispose_element_proc.ToString("x08"));
            writer.WriteLine(tab + "****************************************"); // /

            // Check if this is the root of the tag_group.
            if (tab == "")
            {
                // Create the struct header.
                writer.WriteLine(tab + "TAG_GROUP({0}, \"{1}\", {2})", definition.DisplayName, group_tag, field_set.SizeString);
                writer.WriteLine(tab + "{");
            }
            else
            {
                // Write the field info to the output file.
                writer.WriteLine(tab + "{{{0}, \"{1}\", &{2}}},", parent.type.ToString(), parent.Name, definition.Name);

                // Create the struct header.
                writer.WriteLine(tab + "TAG_BLOCK({0}, {1}, {2})", definition.Name, definition.MaximumElementCount, field_set.SizeString);
                writer.WriteLine(tab + "{");
            }

            // Increase tab.
            tab += "\t";

            // Seek to the field set address.
            reader.BaseStream.Position = field_set.fields_address - BaseAddress;

            // Read all the fields for the field_set.
            tag_field field = new tag_field();
            do
            {
                // Save the current address.
                long currentAddress = reader.BaseStream.Position;

                // Read the field from the stream.
                field.Read(h2LangLib, reader);

                // Check the field type.
                switch (field.type)
                {
                    case field_type._field_tag_reference:
                        {
                            // Seek to the definition address and read the tag_reference_definition struct.
                            reader.BaseStream.Position = field.definition - BaseAddress;
                            tag_reference_definition tag_ref = new tag_reference_definition();
                            tag_ref.Read(h2LangLib, reader);

                            // Write the field info to the ouput file.
                            writer.WriteLine(tab + "{{{0}, \"{1}\", \"{2}\"}},", field.type.ToString(), field.Name, GroupTagToString(tag_ref.group_tag));
                            break;
                        }
                    case field_type._field_block:
                        {
                            // Process the tag_block_definition structure.
                            ProcessTagBlockDefinition(h2LangLib, reader, writer, field.definition, "", field, tab);
                            break;
                        }
                    case field_type._field_struct:
                        {
                            // Seek to the definition address and read the tag_struct_definition struct.
                            reader.BaseStream.Position = field.definition - BaseAddress;
                            tag_struct_definition tag_struct = new tag_struct_definition();
                            tag_struct.Read(h2LangLib, reader);

                            // Process the tag_block_definition structure.
                            ProcessTagBlockDefinition(h2LangLib, reader, writer, tag_struct.block_definition_address, "", field, tab);
                            break;
                        }
                    case field_type._field_data:
                        {
                            // Seek to the definition address and read the tag_data_definition struct.
                            reader.BaseStream.Position = field.definition - BaseAddress;
                            tag_data_definition data_def = new tag_data_definition();
                            data_def.Read(h2LangLib, reader);

                            // Write the field header to the output file.
                            writer.WriteLine();
                            writer.WriteLine(tab + "/****************************************");
                            if (IsNumeric(data_def.MaximumSize) == true)
                                writer.WriteLine(tab + "* size:\t{0}", data_def.maximum_size);
                            else
                                writer.WriteLine(tab + "* size:\t{0} = {1}", data_def.MaximumSize, data_def.maximum_size);
                            writer.WriteLine(tab + "* alignment:\t{0}", data_def.alignment_bit != 0 ? (1 << data_def.alignment_bit) : 0);

                            // Print the proc addresses only if they exist.
                            if (data_def.byteswap_proc != 0 || data_def.copy_proc != 0) writer.WriteLine(tab + "*");
                            if (data_def.byteswap_proc != 0) writer.WriteLine(tab + "* byteswap_proc: 0x{0}", data_def.byteswap_proc.ToString("x08"));
                            if (data_def.copy_proc != 0) writer.WriteLine(tab + "* copy_proc: 0x{0}", data_def.copy_proc.ToString("x08"));
                            writer.WriteLine(tab + "****************************************"); // /

                            // Write the field to the output file.
                            writer.WriteLine(tab + "{{{0}, \"{1}\", &{2}}},", field.type.ToString(), field.Name, data_def.Name);
                            writer.WriteLine(tab + "FIELD_DATA({0}, {1}),", data_def.Name, data_def.MaximumSize);
                            writer.WriteLine();
                            break;
                        }
                    case field_type._field_explanation:
                        {
                            // Check if there is sub-text for this explaination.
                            string subtext = "";
                            if (field.definition != 0)
                                subtext = ReadString(h2LangLib, reader, field.definition);

                            // Write the field info to the output file.
                            writer.WriteLine(tab + "FIELD_EXPLAINATION(\"{0}\", \"{1}\"),", field.Name, subtext.Replace("\n", "<lb>"));
                            break;
                        }
                    case field_type._field_byte_flags:
                    case field_type._field_long_flags:
                    case field_type._field_word_flags:
                    case field_type._field_char_enum:
                    case field_type._field_enum:
                    case field_type._field_long_enum:
                        {
                            // Seek to the definition address and read the enum_definition struct.
                            reader.BaseStream.Position = field.definition - BaseAddress;
                            enum_definition enum_def = new enum_definition();
                            enum_def.Read(h2LangLib, reader);

                            // Read all the options for the enum.
                            string options = "";
                            for (int i = 0; i < enum_def.option_count; i++)
                            {
                                // Seek to the next option name address.
                                reader.BaseStream.Position = enum_def.options_address - BaseAddress + (i * 4);

                                // Read the string from the stream.
                                int string_address = reader.ReadInt32();
                                options += string.Format(i == 0 ? "{0}" : ":{0}", ReadString(h2LangLib, reader, string_address));
                            }

                            // Write the field info to the output file.
                            writer.WriteLine(tab + "{{{0}, \"{1}\", \"{2}\"}},", field.type.ToString(), field.Name, options);
                            break;
                        }
                    case field_type._field_byte_block_flags:
                    case field_type._field_word_block_flags:
                    case field_type._field_long_block_flags:
                        {
                            // Seek to the definition address and read the tag_block_definition struct.
                            reader.BaseStream.Position = field.definition - BaseAddress;
                            tag_block_definition block_def = new tag_block_definition();
                            block_def.Read(h2LangLib, reader);

                            // Write the field info to the output file.
                            writer.WriteLine(tab + "{{{0}, \"{1}\", &{2}}},", field.type.ToString(), field.Name, block_def.Name);
                            break;
                        }
                    case field_type._field_char_block_index1:
                    case field_type._field_short_block_index1:
                    case field_type._field_long_block_index1:
                        {
                            // Check if the field is even used.
                            if (field.Name.Equals("EMPTY STRING") == true)
                            {
                                // Just write the field to the output file.
                                writer.WriteLine(tab + "{{{0}, \"{1}\"}},", field.type.ToString(), field.Name);
                            }
                            else
                            {
                                // Read the tag_block_definition struct from the stream.
                                reader.BaseStream.Position = field.definition - BaseAddress;
                                tag_block_definition block_def = new tag_block_definition();
                                block_def.Read(h2LangLib, reader);

                                // Write the field info to the output file.
                                writer.WriteLine(tab + "{{{0}, \"{1}\", &{2}}},", field.type.ToString(), field.Name, block_def.Name);
                            }
                            break;
                        }
                    case field_type._field_char_block_index2:
                    case field_type._field_short_block_index2:
                    case field_type._field_long_block_index2:
                        {
                            // Seek to the definition address and read the block_index_custom_search_definition struct.
                            reader.BaseStream.Position = field.definition - BaseAddress;
                            block_index_custom_search_definition block_def = new block_index_custom_search_definition();
                            block_def.Read(h2LangLib, reader);

                            // Write the field info to the stream.
                            writer.WriteLine(tab + "{{{0}, \"{1}\", 0x{2}, 0x{3}}},", field.type.ToString(), field.Name,
                                block_def.get_block_proc.ToString("x08"), block_def.is_valid_source_block_proc.ToString("x08"));
                            break;
                        }
                    case field_type._field_array_start:
                        {
                            // Write the field info to the output file.
                            writer.WriteLine();
                            writer.WriteLine(tab + "{{{0}, \"{1}\", {2}}},", field.type.ToString(), field.Name, field.definition);
                            break;
                        }
                    case field_type._field_array_end:
                        {
                            // Just write the field name to the output file.
                            writer.WriteLine(tab + "{{{0}}},", field.type.ToString());
                            writer.WriteLine();
                            break;
                        }
                    case field_type._field_char_integer:
                    case field_type._field_short_integer:
                    case field_type._field_long_integer:
                        {
                            // Check if the definition address is null.
                            if (field.definition != 0)
                            {
                                // Seek to the definition address and read the tag_block_definition struct.
                                reader.BaseStream.Position = field.definition - BaseAddress;
                                tag_block_definition block_def = new tag_block_definition();
                                block_def.Read(h2LangLib, reader);

                                // Write the field info to the output file.
                                writer.WriteLine(tab + "{{{0}, \"{1}\", &{2}}},", field.type.ToString(), field.Name, block_def.Name);
                            }
                            else
                            {
                                // Just write the field to the output file.
                                writer.WriteLine(tab + "{{{0}, \"{1}\"}},", field.type.ToString(), field.Name);
                            }
                            break;
                        }
                    case field_type._field_useless_pad:
                        {
                            // Write the field info to the output file.
                            writer.WriteLine(tab + "{{{0}, {1}}},", field.type.ToString(), field.definition);
                            break;
                        }
                    case field_type._field_pad:
                        {
                            // Write the field info to the output file.
                            writer.WriteLine(tab + "FIELD_PAD({0}),", field.definition);
                            break;
                        }
                    case field_type._field_skip:
                        {
                            // Write the field info to the output file.
                            writer.WriteLine(tab + "{{{0}, {1}}},", field.type.ToString(), field.definition);
                            break;
                        }
                    case field_type._field_terminator:
                        {
                            // Write the field with no extra info to the output file.
                            writer.WriteLine(tab + "{{{0}}}", field.type.ToString());
                            break;
                        }
                    default:
                        {
                            //if (field.definition != 0 && field.type != field_type._field_string_id && field.type != field_type._field_custom)
                            //{
                            // custom, string_id, real
                            //}

                            // Just write the field to the output file.
                            writer.WriteLine(tab + "{{{0}, \"{1}\"}},", field.type.ToString(), field.Name);
                            break;
                        }
                }

                // Seek to the next tag_field.
                reader.BaseStream.Position = currentAddress + 16;// sizeof(tag_field);
            }
            while (field.type != field_type._field_terminator);

            // Decrease tab.
            tab = tab.Remove(tab.Length - 1);

            // Finish the tag_field_set struct.
            writer.WriteLine(tab + "};");
            writer.WriteLine();
        }
*/

        public static string ReadString(IntPtr h2LangLib, BinaryReader reader, int address)
        {
            string str = "";

            // Check if address is smaller than the base address of the executable.
            if (address < BaseAddress)
            {
                // The string is stored in the h2 language library.
                StringBuilder sb = new StringBuilder(0x1000);
                LoadString(h2LangLib, (uint)address, sb, sb.Capacity);
                str = sb.ToString();
            }
            else if (address > BaseAddress && (address - BaseAddress) < (int)reader.BaseStream.Length)
            {
                // Seek to the string address.
                reader.BaseStream.Position = address - BaseAddress;

                // Read the string from the executable.
                char b;
                while ((b = reader.ReadChar()) != '\0')
                    str += b;
            }

            // Return the string buffer.
            return str;
        }

        public static string GroupTagToString(int group_tag)
        {
            // Check if the group_tag is null.
            if (group_tag == -1)
                return string.Empty;

            // Convert the group_tag to a byte array.
            byte[] bytes = BitConverter.GetBytes(group_tag);

            // Convert each byte to a char.
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
                str += (char)bytes[i];

            // Return the string buffer.
            return Microsoft.VisualBasic.Strings.StrReverse(str);
        }

        public static bool IsNumeric(string str)
        {
            // Loop through the string and check each character.
            for (int i = 0; i < str.Length; i++)
            {
                // Check if the character is numeric.
                if (char.IsDigit(str[i]) == false)
                    return false;
            }

            // Every character is numeric, return true.
            return true;
        }
    }
}
