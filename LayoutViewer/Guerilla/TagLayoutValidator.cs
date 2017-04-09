using LayoutViewer.CodeDOM;
using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using Mutation.HEK.Guerilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.Guerilla
{
    public class TagLayoutValidator
    {
        #region Fields

        private static Dictionary<field_type, int> mutationFieldSizes = null;
        /// <summary>
        /// Gets a dictionary of all Mutation fields that have the GuerillaTypeAttribute
        /// where the key is the Guerilla field_type and the value is the size of the Mutation field.
        /// </summary>
        public static Dictionary<field_type, int> MutationFieldSizes
        {
            get
            {
                // Check if the dictionary has already been initialized.
                if (mutationFieldSizes == null)
                {
                    // Initialize the singleton instance.
                    CacheMutationFieldSizes();
                }

                // Return the singleton instance.
                return mutationFieldSizes;
            }
            private set { mutationFieldSizes = value; }
        }

        private static Dictionary<field_type, int> guerillaFieldSizes = null;
        /// <summary>
        /// Gets a dictionary of all Guerilla fields where the key is the Guerilla field_type and the value
        /// is the size of the field.
        /// </summary>
        public static Dictionary<field_type, int> GuerillaFieldSizes
        {
            get
            {
                // Check if the dictionary has already been initialized.
                if (guerillaFieldSizes == null)
                {
                    // Initialize the singleton instance.
                    CacheGuerillaFieldSizes();
                }

                // Return the singleton instance.
                return guerillaFieldSizes;
            }
            private set { guerillaFieldSizes = value; }
        }

        #endregion

        /// <summary>
        /// Computes the size of a tag layout definition using the Mutation field type sizes.
        /// </summary>
        /// <param name="reader">Guerilla reader instance.</param>
        /// <param name="fields">Fields in the tag layout definition.</param>
        /// <returns>Size of the fields in terms of Mutation field type sizes.</returns>
        public static int ComputeMutationDefinitionSize(GuerillaReader reader, List<tag_field> fields)
        {
            int definitionSize = 0;

            // Loop through all of the fields and compute the size of the definition.
            for (int i = 0; i < fields.Count; i++)
            {
                // Handle the field size accordingly.
                switch (fields[i].type)
                {
                    case field_type._field_struct:
                        {
                            // Cast the field to a tag_struct_definition.
                            tag_struct_definition tagStruct = (tag_struct_definition)fields[i];

                            // Get the definition struct from the field address.
                            TagBlockDefinition tagBlockDefinition = reader.TagBlockDefinitions[tagStruct.block_definition_address];

                            // Get the size of the struct definition from the field_set most close to h2x.
                            definitionSize += ComputeMutationDefinitionSize(reader, tagBlockDefinition.TagFields[tagBlockDefinition.GetFieldSetIndexClosestToH2Xbox()]);
                            break;
                        }
                    case field_type._field_array_start:
                        {
                            // Get a list of fields that are in the array.
                            List<tag_field> arrayFields = MutationTagLayoutCreator.CreateArrayFieldList(fields, i);

                            // Get the size of the array fields and multiply it by the length of the array.
                            definitionSize += fields[i].definition_address * ComputeMutationDefinitionSize(reader, arrayFields);

                            // Skip the array fields and the array terminator.
                            i += arrayFields.Count + 1;
                            break;
                        }
                    case field_type._field_pad:
                    case field_type._field_skip:
                        {
                            // Field size is the length of the padding field.
                            definitionSize += fields[i].definition_address;
                            break;
                        }
                    default:
                        {
                            // Make sure the type array contains the field type.
                            if (MutationFieldSizes.Keys.Contains(fields[i].type) == false)
                                break;

                            // Get the field size from the type array.
                            definitionSize += MutationFieldSizes[fields[i].type];
                            break;
                        }
                }
            }

            // Return the definition size.
            return definitionSize;
        }

        /// <summary>
        /// Computes the size of a tag layout definition using Guerilla field type sizes.
        /// </summary>
        /// <param name="reader">Guerilla reader instance</param>
        /// <param name="fields">Fields in the tag layout definition.</param>
        /// <returns>Size of the fields in terms of Guerilla field type sizes.</returns>
        public static int ComputeGuerillaDefinitionSize(GuerillaReader reader, List<tag_field> fields)
        {
            int definitionSize = 0;

            // Loop through all of the fields and compute the size of the definition.
            for (int i = 0; i < fields.Count; i++)
            {
                // Handle the field size accordingly.
                switch (fields[i].type)
                {
                    case field_type._field_struct:
                        {
                            // Cast the field to a tag_struct_definition.
                            tag_struct_definition tagStruct = (tag_struct_definition)fields[i];

                            // Get the definition struct from the field address.
                            TagBlockDefinition tagBlockDefinition = reader.TagBlockDefinitions[tagStruct.block_definition_address];

                            // Get the size of the struct definition from the field_set most close to h2x.
                            definitionSize += ComputeGuerillaDefinitionSize(reader, tagBlockDefinition.TagFields[tagBlockDefinition.GetFieldSetIndexClosestToH2Xbox()]);
                            break;
                        }
                    case field_type._field_array_start:
                        {
                            // Get a list of fields that are in the array.
                            List<tag_field> arrayFields = MutationTagLayoutCreator.CreateArrayFieldList(fields, i);

                            // Get the size of the array fields and multiply it by the length of the array.
                            definitionSize += fields[i].definition_address * ComputeGuerillaDefinitionSize(reader, arrayFields);

                            // Skip the array fields and the array terminator.
                            i += arrayFields.Count + 1;
                            break;
                        }
                    case field_type._field_pad:
                    case field_type._field_skip:
                        {
                            // Field size is the length of the padding field.
                            definitionSize += fields[i].definition_address;
                            break;
                        }
                    default:
                        {
                            // Make sure the type array contains the field type.
                            if (GuerillaFieldSizes.Keys.Contains(fields[i].type) == false)
                                break;

                            // Get the field size from the type array.
                            definitionSize += GuerillaFieldSizes[fields[i].type];
                            break;
                        }
                }
            }

            // Return the definition size.
            return definitionSize;
        }

        #region Caching Functions

        /// <summary>
        /// Builds a dictionary of Guerilla field types and their corresponding size in terms of Mutation field types.
        /// </summary>
        private static void CacheMutationFieldSizes()
        {
            // Add the basic field types to the dictionary.
            MutationFieldSizes = new Dictionary<field_type, int>((int)field_type._field_type_max);
            MutationFieldSizes.Add(field_type._field_char_integer, sizeof(byte));
            MutationFieldSizes.Add(field_type._field_short_integer, sizeof(short));
            MutationFieldSizes.Add(field_type._field_long_integer, sizeof(int));
            MutationFieldSizes.Add(field_type._field_angle, sizeof(float));
            MutationFieldSizes.Add(field_type._field_real, sizeof(float));
            MutationFieldSizes.Add(field_type._field_real_fraction, sizeof(float));
            MutationFieldSizes.Add(field_type._field_char_enum, sizeof(byte));
            MutationFieldSizes.Add(field_type._field_enum, sizeof(short));
            MutationFieldSizes.Add(field_type._field_long_enum, sizeof(int));
            MutationFieldSizes.Add(field_type._field_byte_flags, sizeof(byte));
            MutationFieldSizes.Add(field_type._field_word_flags, sizeof(short));
            MutationFieldSizes.Add(field_type._field_long_flags, sizeof(int));
            MutationFieldSizes.Add(field_type._field_byte_block_flags, sizeof(byte));
            MutationFieldSizes.Add(field_type._field_word_block_flags, sizeof(short));
            MutationFieldSizes.Add(field_type._field_long_block_flags, sizeof(int));
            MutationFieldSizes.Add(field_type._field_char_block_index1, sizeof(byte));
            MutationFieldSizes.Add(field_type._field_short_block_index1, sizeof(short));
            MutationFieldSizes.Add(field_type._field_long_block_index1, sizeof(int));
            MutationFieldSizes.Add(field_type._field_char_block_index2, sizeof(byte));
            MutationFieldSizes.Add(field_type._field_short_block_index2, sizeof(short));
            MutationFieldSizes.Add(field_type._field_long_block_index2, sizeof(int));

            // Miscellaneous fields.
            MutationFieldSizes.Add(field_type._field_array_start, 0);
            MutationFieldSizes.Add(field_type._field_array_end, 0);
            MutationFieldSizes.Add(field_type._field_custom, 0);
            MutationFieldSizes.Add(field_type._field_terminator, 0);
            MutationFieldSizes.Add(field_type._field_useless_pad, 0);

            // Generate a list of types from the Mutation.Halo assembly.
            Type[] assemblyTypes = Assembly.GetAssembly(typeof(GuerillaTypeAttribute)).GetTypes();

            // Find all the types that have a GuerillaTypeAttribute attached to them.
            var guerillaTypes = from type in assemblyTypes
                                where type.GetCustomAttributes(typeof(GuerillaTypeAttribute), false).Count() > 0
                                select type;

            // Add all the types to the value size dictionary.
            foreach (Type fieldType in guerillaTypes)
            {
                // Get the guerilla type attributes attached to the type.
                GuerillaTypeAttribute[] guerillaAttr = (GuerillaTypeAttribute[])fieldType.GetCustomAttributes(typeof(GuerillaTypeAttribute), false);
                foreach (GuerillaTypeAttribute singleAttr in guerillaAttr)
                {
                    // Check for the special case tag_block class.
                    if (singleAttr.FieldType == field_type._field_block)
                    {
                        // Add the field size to the value size dictionary.
                        MutationFieldSizes.Add(singleAttr.FieldType, 8);
                    }
                    else
                    {
                        // Add the field size to the value size dictionary using the StructLayout attributue.
                        MutationFieldSizes.Add(singleAttr.FieldType, fieldType.StructLayoutAttribute.Size);
                    }
                }
            }
        }

        /// <summary>
        /// Builds a dictionary of Guerilla field types and their corresponding size in terms of Guerilla field types.
        /// </summary>
        private static void CacheGuerillaFieldSizes()
        {
            // Initialize the field size dictionary.
            guerillaFieldSizes = new Dictionary<field_type, int>((int)field_type._field_type_max);

            // Loop through all of the guerilla field types and add each one to the size list.
            for (int i = 0; i < (int)field_type._field_type_max; i++)
            {
                // Get the current field type.
                field_type fieldType = (field_type)i;

                // Get the size accordingle.
                switch (fieldType)
                {
                    case field_type._field_byte_block_flags:
                    case field_type._field_byte_flags:
                    case field_type._field_char_block_index1:
                    case field_type._field_char_block_index2:
                    case field_type._field_char_enum:
                    case field_type._field_char_integer:
                        {
                            // 1-byte fields.
                            guerillaFieldSizes.Add(fieldType, 1);
                            break;
                        }
                    case field_type._field_enum:
                    case field_type._field_short_block_index1:
                    case field_type._field_short_block_index2:
                    case field_type._field_short_integer:
                    case field_type._field_word_block_flags:
                    case field_type._field_word_flags:
                        {
                            // 2-byte fields.
                            guerillaFieldSizes.Add(fieldType, 2);
                            break;
                        }
                    case field_type._field_angle:
                    case field_type._field_datum_index:
                    case field_type._field_long_block_flags:
                    case field_type._field_long_block_index1:
                    case field_type._field_long_block_index2:
                    case field_type._field_long_enum:
                    case field_type._field_long_flags:
                    case field_type._field_long_integer:
                    case field_type._field_real:
                    case field_type._field_real_fraction:
                    case field_type._field_tag:
                    case field_type._field_argb_color:
                    case field_type._field_rgb_color:
                    case field_type._field_short_bounds:
                    case field_type._field_string_id:
                    case field_type._field_old_string_id:
                    case field_type._field_point_2d:
                        {
                            // 4-byte fields.
                            guerillaFieldSizes.Add(fieldType, 4);
                            break;
                        }
                    case field_type._field_angle_bounds:
                    case field_type._field_real_bounds:
                    case field_type._field_real_fraction_bounds:
                    case field_type._field_real_euler_angles_2d:
                    case field_type._field_real_point_2d:
                    case field_type._field_rectangle_2d:
                    case field_type._field_real_vector_2d:
                        {
                            // 8-byte fields.
                            guerillaFieldSizes.Add(fieldType, 8);
                            break;
                        }
                    case field_type._field_block:
                    //case field_type._field_string_id:
                    case field_type._field_real_euler_angles_3d:
                    case field_type._field_real_hsv_color:
                    case field_type._field_real_plane_2d:
                    case field_type._field_real_point_3d:
                    case field_type._field_real_rgb_color:
                    case field_type._field_real_vector_3d:
                        {
                            // 12-byte fields.
                            guerillaFieldSizes.Add(fieldType, 12);
                            break;
                        }
                    case field_type._field_tag_reference:
                    case field_type._field_real_argb_color:
                    case field_type._field_real_ahsv_color:
                    case field_type._field_real_plane_3d:
                    case field_type._field_real_quaternion:
                        {
                            // 16-byte fields.
                            guerillaFieldSizes.Add(fieldType, 16);
                            break;
                        }
                    case field_type._field_data:
                        {
                            // 20 byte field.
                            guerillaFieldSizes.Add(fieldType, 20);
                            break;
                        }
                    case field_type._field_string:
                    case field_type._field_vertex_buffer:
                    //case field_type._field_old_string_id:
                        {
                            // 32-byte field.
                            guerillaFieldSizes.Add(fieldType, 32);
                            break;
                        }
                    case field_type._field_long_string:
                        {
                            // 256-byte field.
                            guerillaFieldSizes.Add(fieldType, 256);
                            break;
                        }
                    case field_type._field_array_start:
                    case field_type._field_array_end:
                    case field_type._field_custom:
                    case field_type._field_explanation:
                    case field_type._field_useless_pad:
                    default:
                        {
                            // It's either some type of markup field or idfk.
                            guerillaFieldSizes.Add(fieldType, 0);
                            break;
                        }
                }
            }
        }

        #endregion
    }
}
