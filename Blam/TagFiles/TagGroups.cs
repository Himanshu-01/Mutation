using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blam.Objects.DataTypes;

namespace Blam.TagFiles
{
    public static class TagGroups
    {
        #region string_id's

        public static void _InitializeStringIdTables()
        {
            // Allocate the string_id index buffer.
            Engine.Engine.g_string_id_index_buffer = new int[Engine.Engine.k_maximum_string_ids];
            for (int i = 0; i < Engine.Engine.k_maximum_string_ids; i++) 
                Engine.Engine.g_string_id_index_buffer[i] = -1;

            // Allocate string_id storage buffer.
            Engine.Engine.g_string_id_storage = new char[Engine.Engine.k_maximum_string_id_storage];
            Array.Clear(Engine.Engine.g_string_id_storage, 0, Engine.Engine.k_maximum_string_id_storage);

            // Initialize the count and size.
            Engine.Engine.g_string_id_count = 0;
            Engine.Engine._g_string_id_storage_size = 0;

            // Initialize the string_id_globals hashtable.
            Engine.Engine.g_string_id_globals = new Blam.Memory.Hashtable<string, Objects.DataTypes.string_id>("string id globals",
                Objects.DataTypes.string_id.kSizeOf, 2 * Engine.Engine.k_maximum_string_ids, Engine.Engine.k_maximum_string_ids, 
                string_id_hashcode_proc, string_id_compare_proc);

            // Create the "null" string id.
            _CreateStringId("");

            // Loop through the default string_id's and create a handle for each one.
            for (int i = 0; i < Engine.Engine._k_number_of_default_string_ids; i++)
            {
                // Create the string handle.
                _CreateStringId(Engine.Engine._k_default_string_ids[i]);
            }
        }

        public static string_id _CreateStringId(string str_const)
        {
            // Create a local buffer and copy the string constant to it.
            char[] safe_buffer = new char[string_id.k_string_id_value_length + 1];
            Array.Copy(str_const.ToCharArray(), safe_buffer, Math.Min(str_const.Length, string_id.k_string_id_value_length));

            // Format the string constant to remove any bad characters.
            _FormatStringId(ref safe_buffer);

            // "cast" the string buffer to an actual string.
            str_const = new string(safe_buffer).Replace("\0", "");

            // Try to get the handle for this string constant from the string_id globals hashtable.
            string_id handle = _GetStringIdHandle(str_const);
            if (handle != string_id._string_id_invalid)
                return handle;

            // Check if the string_id table is full.
            if (Engine.Engine.g_string_id_count == Engine.Engine.k_maximum_string_ids)
                return string_id._string_id_invalid;

            // Get the length of the string constant.
            int length = str_const.Length;

            // Check if there is enough room in the g_string_id_storage buffer to hold the string constant.
            if (Engine.Engine._g_string_id_storage_size + length + 1 > Engine.Engine.k_maximum_string_id_storage)
                return string_id._string_id_invalid;

            // Store the address of the new string constant into the g_string_id_index_buffer array.
            Engine.Engine.g_string_id_index_buffer[Engine.Engine.g_string_id_count] = Engine.Engine._g_string_id_storage_size;

            // Increment g_string_id_count.
            Engine.Engine.g_string_id_count++;

            // Copy the string into the string_id storage buffer.
            Array.Copy(safe_buffer, 0, Engine.Engine.g_string_id_storage, Engine.Engine._g_string_id_storage_size, length);
            Engine.Engine._g_string_id_storage_size += length + 1;

            // Create the string_id handle from the index and length of the string constant.
            handle = new string_id((byte)length, (uint)Engine.Engine.g_string_id_count - 1);

            // Add the string constant to the string_id globals hashtable.
            Engine.Engine.g_string_id_globals.Add(str_const, handle);

            // Return the string_id handle we just created.
            return handle;
        }

        public static string_id _GetStringIdHandle(string str_const)
        {
            // Create a local buffer and copy the string constant to it.
            char[] safe_buffer = new char[string_id.k_string_id_value_length + 1];
            Array.Copy(str_const.ToCharArray(), safe_buffer, Math.Min(str_const.Length, string_id.k_string_id_value_length));

            // Format the string constant to remove any bad characters.
            _FormatStringId(ref safe_buffer);

            // "cast" the string buffer to an actual string.
            str_const = new string(safe_buffer).Replace("\0", "");

            // Check if the string_id globals hashtable has an entry for this string constant.
            string_id handle = string_id._string_id_invalid;
            Engine.Engine.g_string_id_globals.ContainsValue(str_const, ref handle);

            // Return the key that was or wasn't found.
            return handle;
        }

        public static string string_id_get_string_const(string_id handle)
        {
            // Check that the index is less than the number of string_id's currently created.
            if (handle.ToIndex() >= Engine.Engine.g_string_id_count)
                return string.Empty;

            // Get the pointer for the string constant from the string_id index buffer.
            int index = Engine.Engine.g_string_id_index_buffer[handle.ToIndex()];
            if (index == -1)
                return string.Empty;

            // Cast the buffer from g_string_id_storage to a string.
            return new string(Engine.Engine.g_string_id_storage, index, handle.Length);
        }

        private static void _FormatStringId(ref char[] str_const)
        {
            // Loop through the string until we hit a null terminating character.
            for (int i = 0; str_const[i] != 0; i++)
            {
                // Check if it is an upper case character, and if so make it lower case.
                if (str_const[i] >= 'A' && str_const[i] <= 'Z')
                    str_const[i] = (char)((byte)str_const[i] - 32);

                // Check for special characters.
                if (str_const[i] == ' ' || str_const[i] == '-')
                    str_const[i] = '_';
            }
        }

        public static bool string_id_compare_proc(object obj1, object obj2)
        {
            // Compare the two string id constants.
            return string.Equals(obj1, obj2);
        }

        public static int string_id_hashcode_proc(object obj)
        {
            byte[] hashbytes = { 3, 7, 0x0B, 0x0D, 0x11, 0x13, 0x17, 0x1D, 0x1F, 0x25, 0x29, 0x2B, 0x2F, 0x35, 0x3B };

            int hashcode = 0;

            // Loop through the string and compute the hashcode.
            string str_const = (string)obj;
            for (int i = 0; i < str_const.Length; i++)
            {
                hashcode += (byte)str_const[i] * hashbytes[i % 15];
            }

            // Return the hashcode.
            return hashcode;
        }

        #endregion

        //public static void TestTagLayouts()
        //{
        //    // Initialize the h2x engine poop.
        //    HaloPlugins.Halo2Xbox h2x = new HaloPlugins.Halo2Xbox();

        //    // Loop through all the tag layouts.
        //    for (int i = 0; i < h2x.TagDefinitions.Length; i++)
        //    {
        //        // Output to console.
        //        Console.WriteLine("\nprocessing {0}...", h2x.GroupTagNames[i]);

        //        // Create an instance of the tag layout.
        //        HaloPlugins.TagDefinition tagLayout = h2x.CreateInstance(h2x.GroupTagNames[i]);

        //        // Loop through all the tag fields.
        //        int headerSize = 0;
        //        for (int x = 0; x < tagLayout.Fields.Count; x++)
        //        {
        //            // Add the field size to the header size.
        //            headerSize += tagLayout.Fields[x].FieldSize;

        //            // Check if the field is a tag_block.
        //            if (tagLayout.Fields[x].FieldType == HaloPlugins.Objects.FieldType.TagBlock)
        //            {
        //                // Process the tag_block seperately.
        //                TestTagBlock((HaloPlugins.Objects.Reference.TagBlock)tagLayout.Fields[x]);
        //            }
        //        }

        //        // Check if the header size is correct.
        //        if (headerSize != tagLayout.HeaderSize)
        //        {
        //            // Write to console.
        //            Console.WriteLine("error processing {0} expected {1} but found {2}", 
        //                h2x.TagDefinitions[i], tagLayout.HeaderSize, headerSize);
        //        }
        //    }

        //    // Done.
        //    Console.WriteLine("done");
        //}

        //private static void TestTagBlock(HaloPlugins.Objects.Reference.TagBlock tagBlock)
        //{
        //    // Loop through all the fields in the tag_block definition and compute the size.
        //    int blockSize = 0;
        //    for (int i = 0; i < tagBlock.definition.Length; i++)
        //    {
        //        // Add the field size to the block size.
        //        blockSize += tagBlock.definition[i].FieldSize;

        //        // Check if the field is a tag_block.
        //        if (tagBlock.definition[i].FieldType == HaloPlugins.Objects.FieldType.TagBlock)
        //        {
        //            // Process the tag_block seperately.
        //            TestTagBlock((HaloPlugins.Objects.Reference.TagBlock)tagBlock.definition[i]);
        //        }
        //    }

        //    // Check if the block size is correct.
        //    if (blockSize != tagBlock.DefinitionSize)
        //    {
        //        // Write to console.
        //        Console.WriteLine("\terror processing tag_block: \"{0}\" expected {1} but found {2}", 
        //            tagBlock.Name, tagBlock.DefinitionSize, blockSize);
        //    }
        //}
    }
}
