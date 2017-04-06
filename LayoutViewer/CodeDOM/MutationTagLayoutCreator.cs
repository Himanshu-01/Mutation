using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using Mutation.HEK.Guerilla;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public class MutationTagLayoutCreator
    {
        /// <summary>
        /// Gets the tag block definition his layout creator is creating a layout for.
        /// </summary>
        public TagBlockDefinition TagBlockDefinition { get; set; }

        /// <summary>
        /// Gets the field name as found in guerilla.
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// Gets a code safe type name for tag block definition.
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// Code scope for the layout.
        /// </summary>
        public MutationCodeScope CodeScope { get; private set; }

        /// <summary>
        /// Code creator for the the tag layout.
        /// </summary>
        public MutationCodeCreator CodeCreator { get; set; }

        public MutationTagLayoutCreator(TagBlockDefinition tagBlockDefinition)
        {
            // Initialize fields.
            this.TagBlockDefinition = tagBlockDefinition;
            this.CodeCreator = new MutationCodeCreator();
        }

        public void CreateCodeScope(MutationCodeScope parentScope)
        {
            // Create a new code scope for this type.
            this.CodeScope = parentScope.CreateCodeScopeForType(this.TagBlockDefinition.s_tag_block_definition.Name, 
                this.TagBlockDefinition.s_tag_block_definition.address,
                (this.TagBlockDefinition.IsTagGroup == true ? MutationCodeScopeType.TagGroup : MutationCodeScopeType.TagBlock));

            // Set the type name and field name based on the code scope that was just created.
            this.FieldName = this.TagBlockDefinition.s_tag_block_definition.Name;
            this.TypeName = this.CodeScope.Namespace;
        }

        public void CreateTagLayout(GuerillaReader reader, MutationCodeScope parentScope)
        {
            // Our child code creator for the tag group/block.
            MutationCodeCreator childCodeCreator = null;

            // Check if this tag block is a tag group.
            if (this.TagBlockDefinition.IsTagGroup == true)
            {
                string baseType = "";

                // Get the tag group that points to this tag block definition.
                tag_group tagGroup = reader.TagGroups.First(tag => tag.definition_address == this.TagBlockDefinition.s_tag_block_definition.address);

                // Check if the tag group has a parent tag group it inherits from.
                if (tagGroup.ParentGroupTag != string.Empty)
                {
                    // Get the parent tag group object.
                    tag_group parentTagGroup = reader.TagGroups.First(tag => tag.GroupTag.Equals(tagGroup.ParentGroupTag) == true);

                    // Now find the code scope for the parent tag layout.
                    MutationCodeScope parentTagScope = parentScope.Types.Values.First(scope => scope.DefinitionAddress == parentTagGroup.definition_address);

                    // Save the base type name.
                    baseType = parentTagScope.Namespace;
                }

                // Create a new tag group class.
                childCodeCreator = this.CodeCreator.CreateTagGroupClass(this.TypeName, baseType);
            }
            else
            {
                // Create a new tag block class.
                childCodeCreator = this.CodeCreator.CreateTagBlockClass(this.TypeName);
            }

            // Process the tag block definition.
            ProcessTagBlockDefinition(reader, this.TagBlockDefinition, childCodeCreator, this.CodeScope, parentScope);
        }

        public void WriteToFile(string folder)
        {
            // Check if the definition is a tag group or tag block and write it to the correct location.
            if (this.TagBlockDefinition.IsTagGroup == true)
            {
                // Write the tag group to file.
                this.CodeCreator.WriteToFile(string.Format("{0}\\{1}.cs", folder,
                    MutationCodeFormatter.CreateCodeSafeFieldName(this.TagBlockDefinition.s_tag_block_definition.Name)));
            }
            else
            {
                // Write the block definition to file.
                this.CodeCreator.WriteToFile(string.Format("{0}\\BlockDefinitions\\{1}.cs", folder,
                    MutationCodeFormatter.CreateCodeSafeFieldName(this.TagBlockDefinition.s_tag_block_definition.Name)));
            }
        }

        private void ProcessTagBlockDefinition(GuerillaReader reader, TagBlockDefinition blockDefinition, MutationCodeCreator blockCodeCreator, 
            MutationCodeScope blockCodeScope, MutationCodeScope parentScope)
        {
            // Get the field set index that most closely resembles halo 2 xbox layout.
            int fieldSetIndex = blockDefinition.GetFieldSetIndexClosestToH2Xbox();

            // Process all of the fields in the field_set.
            ProcessFields(blockDefinition.TagFields[fieldSetIndex], reader, blockCodeCreator, blockCodeScope, parentScope);
        }

        private void ProcessFields(List<tag_field> fields, GuerillaReader reader, MutationCodeCreator blockCodeCreator,
            MutationCodeScope blockCodeScope, MutationCodeScope parentScope)
        {
            // Loop through all of the fields in the collection.
            for (int i = 0; i < fields.Count; i++)
            {
                // Create a new field and add it to the scope for this block.
                string displayName, units, tooltip;
                string fieldName = blockCodeScope.CreateCodeSafeFieldName(fields[i].type, fields[i].Name, out displayName, out units, out tooltip);

                // Create an attribute collection for any attributes we might add to the field.
                CodeAttributeDeclarationCollection attributeCollection = new CodeAttributeDeclarationCollection();

                // Make sure at least one of the required fields for a UI markup attribute is valid.
                if (fields[i].Name != string.Empty || units != string.Empty || tooltip != string.Empty)
                {
                    // Create the UI markup attribute if the units or tooltip strings are valid.
                    attributeCollection.Add(EditorMarkUpAttribute.CreateAttributeDeclaration(displayName: displayName, unitsSpecifier: units, tooltipText: tooltip));
                }

                // Handle each field accordingly.
                switch (fields[i].type)
                {
                    case field_type._field_char_enum:
                    case field_type._field_enum:
                    case field_type._field_long_enum:
                        {
                            // Cast the field to a enum_definition struct.
                            enum_definition enumDefinition = (enum_definition)fields[i];

                            // Check if there is an existing code scope for this enum type.
                            MutationCodeScope enumScope = blockCodeScope.FindExistingCodeScope(enumDefinition.definition_address);
                            if (enumScope == null)
                            {
                                // Create a new code scope for the enum type.
                                enumScope = blockCodeScope.CreateCodeScopeForType(enumDefinition.Name, enumDefinition.definition_address, MutationCodeScopeType.Enum);

                                // Create a new enum in the block definition for this field.
                                blockCodeCreator.AddEnumOrBitmask(enumScope, enumDefinition);
                            }

                            // Add a field to the block definition for this enum type.
                            blockCodeCreator.AddCustomTypedField(fields[i].type, fieldName, enumScope.Namespace, attributeCollection);
                            break;
                        }
                    case field_type._field_byte_flags:
                    case field_type._field_word_flags:
                    case field_type._field_long_flags:
                        {
                            // Cast the field to a enum_definition struct.
                            enum_definition enumDefinition = (enum_definition)fields[i];

                            // Check if there is an existing code scope for this bitmask type.
                            MutationCodeScope bitmaskScope = blockCodeScope.FindExistingCodeScope(enumDefinition.definition_address);
                            if (bitmaskScope == null)
                            {
                                // Create a new code scope for the bitmask type.
                                bitmaskScope = blockCodeScope.CreateCodeScopeForType(enumDefinition.Name, enumDefinition.definition_address, MutationCodeScopeType.Bitmask);

                                // Create a new bitmask in the block definition for this field.
                                blockCodeCreator.AddEnumOrBitmask(bitmaskScope, enumDefinition);
                            }

                            // Add a field to the block definition for this bitmask type.
                            blockCodeCreator.AddCustomTypedField(fields[i].type, fieldName, bitmaskScope.Namespace, attributeCollection);
                            break;
                        }
                    case field_type._field_block:
                        {
                            // Get the definition struct from the field address.
                            TagBlockDefinition tagBlockDefinition = reader.TagBlockDefinitions[fields[i].definition_address];

                            // Check if the tag block definition already exists in the parent code scope.
                            MutationCodeScope tagBlockScope = parentScope.FindExistingCodeScope(tagBlockDefinition.s_tag_block_definition.address);
                            if (tagBlockScope == null)
                            {
                                // Create a new code scope for the tag block definition.
                                tagBlockScope = parentScope.CreateCodeScopeForType(tagBlockDefinition.s_tag_block_definition.Name,
                                    tagBlockDefinition.s_tag_block_definition.address, MutationCodeScopeType.TagBlock);

                                // Create a new class for the tag block definition.
                                MutationCodeCreator childBlockCodeCreator = this.CodeCreator.CreateTagBlockClass(tagBlockScope.Namespace);

                                // Process the tag block definition.
                                ProcessTagBlockDefinition(reader, tagBlockDefinition, childBlockCodeCreator, tagBlockScope, parentScope);
                            }

                            // Build the tag block definition attribute for the field.
                            attributeCollection.Add(TagBlockDefinitionAttribute.CreateAttributeDeclaration(tagBlockDefinition));

                            // Create a field for the tag block.
                            blockCodeCreator.AddTagBlockField(fieldName, tagBlockScope.Namespace, attributeCollection);
                            break;
                        }
                    case field_type._field_struct:
                        {
                            // Cast the field to a tag_struct_definition.
                            tag_struct_definition tagStruct = (tag_struct_definition)fields[i];

                            // Get the definition struct from the field address.
                            TagBlockDefinition tagBlockDefinition = reader.TagBlockDefinitions[tagStruct.block_definition_address];

                            // Check if the tag block definition already exists in the parent code scope.
                            MutationCodeScope tagBlockScope = parentScope.FindExistingCodeScope(tagBlockDefinition.s_tag_block_definition.address);
                            if (tagBlockScope == null)
                            {
                                // Create a new code scope for the tag block definition.
                                tagBlockScope = parentScope.CreateCodeScopeForType(tagBlockDefinition.s_tag_block_definition.Name,
                                    tagBlockDefinition.s_tag_block_definition.address, MutationCodeScopeType.TagBlock);

                                // Create a new class for the tag block definition.
                                MutationCodeCreator childBlockCodeCreator = this.CodeCreator.CreateTagBlockClass(tagBlockScope.Namespace);

                                // Process the tag block definition.
                                ProcessTagBlockDefinition(reader, tagBlockDefinition, childBlockCodeCreator, tagBlockScope, parentScope);
                            }

                            // Build the tag block definition attribute for the field.
                            attributeCollection.Add(TagBlockDefinitionAttribute.CreateAttributeDeclaration(tagBlockDefinition));

                            // Create a field for the tag block.
                            blockCodeCreator.AddTagBlockField(fieldName, tagBlockScope.Namespace, attributeCollection);
                            break;
                        }
                    case field_type._field_tag_reference:
                        {
                            // Cast the field to a tag_reference_definition definition.
                            tag_reference_definition tagRegDefinition = (tag_reference_definition)fields[i];

                            // Build the tag reference attribute for the field.
                            attributeCollection.Add(TagReferenceAttribute.CreateAttributeDeclaration(tagRegDefinition));

                            // Add the field with the group tag filter attribute.
                            blockCodeCreator.AddField(fields[i].type, fieldName, attributeCollection);
                            break;
                        }
                    case field_type._field_pad:
                    case field_type._field_skip:
                    case field_type._field_useless_pad:
                        {
                            // Build the padding attribute for the field.
                            attributeCollection.Add(PaddingAttribute.CreateAttributeDeclaration(fields[i].type, fields[i].definition_address));

                            // Add the field with the padding attribute.
                            blockCodeCreator.AddPaddingField(fieldName, fields[i].definition_address, attributeCollection);
                            break;
                        }
                    case field_type._field_explanation:
                        {
                            // Cast the field to a explanation_definition.
                            explaination_definition explanation = (explaination_definition)fields[i];

                            // Create a field for the explanation block.
                            blockCodeCreator.AddExplanationField(fieldName, explanation.Name, explanation.Explaination);
                            break;
                        }
                    case field_type._field_array_start:
                        {
                            // Build a list of fields inside of the array.
                            List<tag_field> arrayFields = CreateArrayFieldList(fields, i);

                            // Loop for the length of the array and process the fields.
                            for (int x = 0; x < fields[i].definition_address; x++)
                            {
                                // Process the array fields.
                                ProcessFields(arrayFields, reader, blockCodeCreator, blockCodeScope, parentScope);
                            }

                            // Skip the fields we just processed and the array terminator.
                            i += arrayFields.Count + 1;
                            break;
                        }
                    case field_type._field_data: break;     // I don't really know what to do about this type just yet...
                    case field_type._field_custom: break;
                    case field_type._field_terminator: break;
                    default:
                        {
                            // Check if the value type dictionary contains this field type.
                            if (blockCodeCreator.ValueTypeDictionary.Keys.Contains(fields[i].type) == false)
                                break;

                            // Add the field to the collection.
                            blockCodeCreator.AddField(fields[i].type, fieldName, attributeCollection);
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Creates a list of fields that belong to an array_start/array_end field set. 
        /// </summary>
        /// <param name="fields">Fields that belong to the array.</param>
        /// <param name="startIndex">Index of the array_start field.</param>
        /// <returns>List of fields inside of the array.</returns>
        private List<tag_field> CreateArrayFieldList(List<tag_field> fields, int startIndex)
        {
            // Create a new list to hold all of the fields in the array.
            List<tag_field> arrayFields = new List<tag_field>();

            // Loop through the fields and copy the array fields into their own list.
            int arrayStartCounter = 0;
            for (int i = startIndex + 1; i < fields.Count; i++)
            {
                // Check if the current field is an array_end field and it belongs to the original array_start field.
                if (fields[i].type == field_type._field_array_end)
                {
                    // Check if it belongs to the original array_start field.
                    if (arrayStartCounter == 0)
                    {
                        // This is the end of the array fields list.
                        return arrayFields;
                    }
                    else
                    {
                        // Decrement the array start counter.
                        arrayStartCounter--;
                    }
                }
                else if (fields[i].type == field_type._field_array_start)
                {
                    // Increment the array start counter for this child array.
                    arrayStartCounter++;
                }

                // Check for no-name fields.
                if (arrayFields.Count == 0 && fields[i].Name == string.Empty)
                    fields[i].Name = fields[startIndex].Name;

                // Add the field to the array fields list.
                arrayFields.Add(fields[i]);
            }

            // Return the array fields.
            return arrayFields;
        }
    }
}
