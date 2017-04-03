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
        private MutationCodeCreator codeCreator = new MutationCodeCreator();

        public MutationTagLayoutCreator(TagBlockDefinition tagBlockDefinition)
        {
            // Initialie fields.
            this.TagBlockDefinition = tagBlockDefinition;
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
                childCodeCreator = this.codeCreator.CreateTagGroupClass(this.TypeName, baseType);
            }
            else
            {
                // Create a new tag block class.
                childCodeCreator = this.codeCreator.CreateTagBlockClass(this.TypeName);
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
                this.codeCreator.WriteToFile(string.Format("{0}\\{1}.cs", folder,
                    MutationCodeFormatter.CreateCodeSafeFieldName(this.TagBlockDefinition.s_tag_block_definition.Name)));
            }
            else
            {
                // Write the block definition to file.
                this.codeCreator.WriteToFile(string.Format("{0}\\BlockDefinitions\\{1}.cs", folder,
                    MutationCodeFormatter.CreateCodeSafeFieldName(this.TagBlockDefinition.s_tag_block_definition.Name)));
            }
        }

        private void ProcessTagBlockDefinition(GuerillaReader reader, TagBlockDefinition blockDefinition, MutationCodeCreator blockCodeCreator, 
            MutationCodeScope blockCodeScope, MutationCodeScope parentScope)
        {
            // Loop through all of the fields and process each one.
            foreach (tag_field field in blockDefinition.TagFields[blockDefinition.TagFieldSetLatestIndex])
            {
                // Check if this field is a padding field.
                bool bIsPadding = (field.type == field_type._field_pad || field.type == field_type._field_skip || field.type == field_type._field_useless_pad);

                // Create a new field and add it to the scope for this block.
                string units, tooltip;
                string fieldName = blockCodeScope.CreateCodeSafeFieldName(field.Name, out units, out tooltip, bIsPadding);

                // Create an attribute collection for any attributes we might add to the field.
                CodeAttributeDeclarationCollection attributeCollection = new CodeAttributeDeclarationCollection();

                // Create the UI markup attribute if the units or tooltip strings are valid.
                attributeCollection.Add(EditorMarkUpAttribute.CreateAttributeDeclaration(displayName: fieldName, unitsSpecifier: units, tooltipText: tooltip));

                // Handle each field accordingly.
                switch (field.type)
                {
                    case field_type._field_char_enum:
                    case field_type._field_enum:
                    case field_type._field_long_enum:
                        {
                            // Cast the field to a enum_definition struct.
                            enum_definition enumDefinition = (enum_definition)field;

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
                            blockCodeCreator.AddCustomTypedField(field.type, fieldName, enumScope.Namespace, attributeCollection);
                            break;
                        }
                    case field_type._field_byte_flags:
                    case field_type._field_word_flags:
                    case field_type._field_long_flags:
                        {
                            // Cast the field to a enum_definition struct.
                            enum_definition enumDefinition = (enum_definition)field;

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
                            blockCodeCreator.AddCustomTypedField(field.type, fieldName, bitmaskScope.Namespace, attributeCollection);
                            break;
                        }
                    case field_type._field_block:
                        {
                            // Get the definition struct from the field address.
                            TagBlockDefinition tagBlockDefinition = reader.TagBlockDefinitions[field.definition_address];

                            // Check if the tag block definition already exists in the parent code scope.
                            MutationCodeScope tagBlockScope = parentScope.FindExistingCodeScope(tagBlockDefinition.s_tag_block_definition.address);
                            if (tagBlockScope == null)
                            {
                                // Create a new code scope for the tag block definition.
                                tagBlockScope = parentScope.CreateCodeScopeForType(tagBlockDefinition.s_tag_block_definition.Name,
                                    tagBlockDefinition.s_tag_block_definition.address, MutationCodeScopeType.TagBlock);

                                // Create a new class for the tag block definition.
                                MutationCodeCreator childBlockCodeCreator = this.codeCreator.CreateTagBlockClass(tagBlockScope.Namespace);

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
                            tag_struct_definition tagStruct = (tag_struct_definition)field;

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
                                MutationCodeCreator childBlockCodeCreator = this.codeCreator.CreateTagBlockClass(tagBlockScope.Namespace);

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
                            tag_reference_definition tagRegDefinition = (tag_reference_definition)field;

                            // Build the tag reference attribute for the field.
                            attributeCollection.Add(TagReferenceAttribute.CreateAttributeDeclaration(tagRegDefinition));

                            // Add the field with the group tag filter attribute.
                            blockCodeCreator.AddField(field.type, fieldName, attributeCollection);
                            break;
                        }
                    case field_type._field_pad:
                    case field_type._field_skip:
                    case field_type._field_useless_pad:
                        {
                            // Build the padding attribute for the field.
                            attributeCollection.Add(PaddingAttribute.CreateAttributeDeclaration(field.type, field.definition_address));

                            // Add the field with the group tag filter attribute.
                            blockCodeCreator.AddPaddingField(blockCodeScope, field.definition_address, attributeCollection);
                            break;
                        }
                    default:
                        {
                            // Check if the value type dictionary contains this field type.
                            if (blockCodeCreator.ValueTypeDictionary.Keys.Contains(field.type) == false)
                                continue;

                            // Add the field to the collection.
                            blockCodeCreator.AddField(field.type, fieldName, attributeCollection);
                            break;
                        }
                }
            }

            // Check if the current block is a tag group or not.
            if (blockDefinition.IsTagGroup == false)
            {
                // Add the region end directive.
            }
        }
    }
}
