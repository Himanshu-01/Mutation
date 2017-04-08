using Microsoft.CSharp;
using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public class MutationCodeCreator
    {
        #region Fields

        // Internal instance of the value type dictionary.
        private static Dictionary<field_type, Type> valueTypeDictionary = null;
        /// <summary>
        /// Dictionary containing all Guerilla field_type's and their corresponding .Net object type.
        /// </summary>
        public static Dictionary<field_type, Type> ValueTypeDictionary
        {
            get
            {
                // Check if the value type dictionary has been initialized.
                if (valueTypeDictionary == null)
                {
                    // Initialize and build the dictionary.
                    BuildValueTypeDictionary();
                }

                // Return it.
                return valueTypeDictionary;
            }
            private set
            {
                // Set the value.
                valueTypeDictionary = value;
            }
        }

        // CodeDOM objects used to create source files.
        private CodeCompileUnit codeUnit;

        /// <summary>
        /// Gets the CodeDOM object for this namespace.
        /// </summary>
        public CodeNamespace CodeNamespace { get; set; }

        /// <summary>
        /// Gets the CodeDOM class object for this class.
        /// </summary>
        public CodeTypeDeclaration CodeClass { get; private set; }

        // Default import directives for tag definition classes.
        private static readonly string[] DefaultNamespaces = new string[]
        {
            "System",
            "System.IO",
            "Mutation.Halo.TagGroups",
            "Mutation.Halo.TagGroups.Attributes",
            "Mutation.Halo.TagGroups.FieldTypes"
        };

        /// <summary>
        /// Namespaces used in the Mutation tag layouts.
        /// </summary>
        private static readonly string[] MutationNamespaces = new string[]
        {
            "Mutation.Halo.TagGroups",
            "Mutation.Halo.TagGroups.Attributes",
            "Mutation.Halo.TagGroups.FieldTypes"
        };

        /// <summary>
        /// Mutation namespace for the tag layouts.
        /// </summary>
        public const string MutationTagsNamespace = "Mutation.Halo.TagGroups.Tags";

        /// <summary>
        /// Name of the base object type for tag block definitions.
        /// </summary>
        private const string TagBlockDefinitionBaseType = "TagBlockDefinition";

        #endregion

        #region Constructor

        public MutationCodeCreator()
        {
            // Build the value type dictionary.
            BuildValueTypeDictionary();

            // Cache the binary reader and writer methods.
            //CacheBinaryReaderWriterMethods();

            // Create a new code namespace for the class.
            this.CodeNamespace = new CodeNamespace(MutationTagsNamespace);

            // Add the standard set of import directives to the namespace.
            foreach (string name in DefaultNamespaces)
            {
                // Add the import to the namespace.
                this.CodeNamespace.Imports.Add(new CodeNamespaceImport(name));
            }

            // Initialize a new code compile unit and add this namespace to it.
            this.codeUnit = new CodeCompileUnit();
            this.codeUnit.Namespaces.Add(this.CodeNamespace);
        }

        #endregion

        /// <summary>
        /// Creates a new class for the tag group and returns a MutationCodeCreator who's root namespace is the new class.
        /// </summary>
        /// <param name="className">Name of the class definition.</param>
        /// <param name="blockAttribute">Tag block definition attribute for the block.</param>
        /// <param name="baseType">Name of the base type if this class inherits another type.</param>
        /// <returns>A new MutationCodeCreator for the child namespace.</returns>
        public MutationCodeCreator CreateTagGroupClass(string className, CodeAttributeDeclaration blockAttribute, string baseType = TagBlockDefinitionBaseType)
        {
            // Create a new code creator instance.
            MutationCodeCreator codeCreator = new MutationCodeCreator();

            // Create the class code type declaration.
            codeCreator.CodeClass = new CodeTypeDeclaration(className);
            codeCreator.CodeClass.IsClass = true;
            codeCreator.CodeClass.BaseTypes.Add(new CodeTypeReference(baseType));

            // Add the block attribute to the class declaration.
            codeCreator.CodeClass.CustomAttributes.Add(blockAttribute);

            // Add the new tag group class to our namespace.
            this.CodeNamespace.Types.Add(codeCreator.CodeClass);

            // Return the new code creator.
            return codeCreator;
        }

        /// <summary>
        /// Creates a new class for the tag block and returns a MutationCodeCreator who's root namespace is the new class.
        /// </summary>
        /// <param name="blockName">Name of the block definition.</param>
        /// <param name="blockAttribute">Tag block definition attribute for the block.</param>
        /// <returns>A new MutationCodeCreator for the child namespace.</returns>
        public MutationCodeCreator CreateTagBlockClass(string blockName, CodeAttributeDeclaration attribute)
        {
            // This method is here in case I decide to change the functionality of tag block definitions.
            // For now they are created the same as a tag group.
            MutationCodeCreator codeCreator = CreateTagGroupClass(blockName, attribute);

            // Add a region directive to the tag block.
            codeCreator.CodeClass.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, blockName));
            codeCreator.CodeClass.EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));

            // Return the new code creator for the tag block.
            return codeCreator;
        }

        /// <summary>
        /// Creates a new field for the specified type and adds it to the current code object.
        /// </summary>
        /// <param name="fieldType">Guerilla field type of the field.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        public void AddField(field_type fieldType, string fieldName, CodeAttributeDeclarationCollection attributeCollection = null)
        {
            // Get the underlying type for this field.
            Type standardFieldType = ValueTypeDictionary[fieldType];

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(MutationCodeFormatter.CreateShortCodeTypeReference(standardFieldType, MutationNamespaces), fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.CodeClass.Members.Add(field);
        }

        /// <summary>
        /// Creates a new field for a custom type and adds it to the current code object.
        /// </summary>
        /// <param name="fieldType">Guerilla field type of the field.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldTypeName">Name of the custom type.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        public void AddCustomTypedField(field_type fieldType, string fieldName, string fieldTypeName, CodeAttributeDeclarationCollection attributeCollection = null)
        {
            // Get the underlying type for this field.
            Type standardFieldType = ValueTypeDictionary[fieldType];

            // If the field name is the same as the type name then we have to append an '@' character.
            if (fieldName.Equals(fieldTypeName) == true)
                fieldName.Insert(0, "@");

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(fieldTypeName, fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.CodeClass.Members.Add(field);
        }

        /// <summary>
        /// Creates a new enum or bitmask type definition in the current code object.
        /// </summary>
        /// <param name="codeScope">Code scope of the enum for ensuring each flag option is unique.</param>
        /// <param name="field">Enum definition from Guerilla.</param>
        public void AddEnumOrBitmask(MutationCodeScope codeScope, enum_definition field)
        {
            // Determine the underlying type for the enum based on the field type.
            Type fieldType = ValueTypeDictionary[field.type];

            // Create the enum field by creating a new CodeTypeDeclaration.
            CodeTypeDeclaration @enum = new CodeTypeDeclaration(codeScope.Namespace);
            @enum.IsEnum = true;
            @enum.BaseTypes.Add(fieldType);

            // Check if this field is a bitmask and if so add the Flags attribute.
            if (codeScope.Type == MutationCodeScopeType.Bitmask)
            {
                // Setup a code attribute declaration object for the Flags attribute.
                CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(FlagsAttribute).Name));
                @enum.CustomAttributes.Add(attribute);
            }

            // Loop through all of the enum options and add each one to the enum field.
            for (int i = 0; i < field.option_count; i++)
            {
                // Check if the option name is valid and if not skip it.
                if (field.options[i] == string.Empty)
                    continue;

                // Create a code safe field name for the enum option.
                string displayName, units, tooltip;
                EditorMarkUpFlags markupFlags;
                string optionName = codeScope.CreateCodeSafeFieldName(field_type._field_enum_option, field.options[i], out displayName, out units, out tooltip, out markupFlags);

                // Create a new CodeMemberField for the enum option.
                CodeMemberField option = new CodeMemberField
                {
                    Name = optionName,

                    // Set the flag value accordingly.
                    InitExpression = new CodeSnippetExpression(string.Format("0x{0}", 
                    (codeScope.Type == MutationCodeScopeType.Bitmask ? (1 << i) : i).ToString("x"))),
                };

                // Create a new UI markup attribute and add it to the enum option.
                option.CustomAttributes.Add(EditorMarkUpAttribute.CreateAttributeDeclaration(flags: markupFlags, displayName: displayName, unitsSpecifier: units, tooltipText: tooltip));

                // Add the option to the enum.
                @enum.Members.Add(option);
            }

            // Add the enum to the class definition.
            this.CodeClass.Members.Add(@enum);
        }

        /// <summary>
        /// Creates a new padding field and adds it to the current code object.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="paddingLength">Size of the padding field.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        public void AddPaddingField(string fieldName, int paddingLength, CodeAttributeDeclarationCollection attributeCollection = null)
        {
            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(typeof(byte[]), fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.CodeClass.Members.Add(field);
        }

        /// <summary>
        /// Creates a new Explanation field using the information provided.
        /// </summary>
        /// <param name="fieldName">Name of the explanation field.</param>
        /// <param name="blockName">Name of the explanation block.</param>
        /// <param name="explanation">Explanation for the block.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        public void AddExplanationField(string fieldName, string blockName = "", string explanation = "", CodeAttributeDeclarationCollection attributeCollection = null)
        {
            // Get the underlying type for this field.
            Type standardFieldType = ValueTypeDictionary[field_type._field_explanation];

            // Create a new code member field for the explanation block.
            CodeMemberField field = new CodeMemberField(MutationCodeFormatter.CreateShortCodeTypeReference(standardFieldType, MutationNamespaces), fieldName);
            field.Attributes = MemberAttributes.Public;

            // Create a list of parameters to give to the explanation constructor.
            List<CodeExpression> initializers = new List<CodeExpression>();

            // Check if the block name is present.
            if (blockName != string.Empty)
            {
                // Create a code expression for the block name initializer.
                initializers.Add(new CodeSnippetExpression(string.Format("name: \"{0}\"", blockName)));
            }

            // Check if the explanation is present.
            if (MutationCodeFormatter.IsValidFieldName(explanation) == true)
            {
                // Create a code expression for the explanation initializer.
                initializers.Add(new CodeSnippetExpression(string.Format("explanation: {0}", MutationCodeFormatter.CreateCodeSafeStringLiteral(explanation))));
            }

            // Create the init expression which will call the constructor of the Explanation object.
            field.InitExpression = new CodeObjectCreateExpression(MutationCodeFormatter.CreateShortCodeTypeReference(standardFieldType, MutationNamespaces), initializers.ToArray());

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.CodeClass.Members.Add(field);
        }

        /// <summary>
        /// Creates a new tag block field and adds it to the current code object.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="blockTypeName">Name of the underlying tag block definition type.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        public void AddTagBlockField(string fieldName, string blockTypeName, CodeAttributeDeclarationCollection attributeCollection = null)
        {
            // Create a new code type reference to reference the tag_block data type.
            CodeTypeReference tagBlockType = MutationCodeFormatter.CreateShortCodeTypeReference(ValueTypeDictionary[field_type._field_block], MutationNamespaces);
            tagBlockType.TypeArguments.Add(blockTypeName);

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(tagBlockType, fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.CodeClass.Members.Add(field);
        }

        /// <summary>
        /// Writes the current code scope to file.
        /// </summary>
        /// <param name="fileName">File name to write the code to.</param>
        public void WriteToFile(string fileName)
        {
            // Create a new CSharpCodeProvider to generate the code for us.
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            // Create a new stream writer to write the file.
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                // Create a new IndentedTextWriter to format the code is a nice mannor.
                IndentedTextWriter tw = new IndentedTextWriter(writer, "    ");

                // Setup the code generation options for style preferences.
                CodeGeneratorOptions options = new CodeGeneratorOptions();
                options.BlankLinesBetweenMembers = true;
                options.BracingStyle = "C";
                options.VerbatimOrder = false;

                // Generate the code using the code provider.
                codeProvider.GenerateCodeFromCompileUnit(this.codeUnit, tw, options);

                // Close the text writer.
                tw.Close();
            }

            // Dispose of the code provider.
            codeProvider.Dispose();
        }

        #region Caching Functions

        /// <summary>
        /// Builds the list of .Net types that encapsulate Guerilla field types.
        /// </summary>
        private static void BuildValueTypeDictionary()
        {
            // Add the basic field types to the dictionary.
            ValueTypeDictionary = new Dictionary<field_type, Type>((int)field_type._field_type_max);
            ValueTypeDictionary.Add(field_type._field_char_integer, typeof(byte));
            ValueTypeDictionary.Add(field_type._field_short_integer, typeof(short));
            ValueTypeDictionary.Add(field_type._field_long_integer, typeof(int));
            ValueTypeDictionary.Add(field_type._field_angle, typeof(float));
            ValueTypeDictionary.Add(field_type._field_real, typeof(float));
            ValueTypeDictionary.Add(field_type._field_real_fraction, typeof(float));
            ValueTypeDictionary.Add(field_type._field_char_enum, typeof(byte));
            ValueTypeDictionary.Add(field_type._field_enum, typeof(short));
            ValueTypeDictionary.Add(field_type._field_long_enum, typeof(int));
            ValueTypeDictionary.Add(field_type._field_byte_flags, typeof(byte));
            ValueTypeDictionary.Add(field_type._field_word_flags, typeof(short));
            ValueTypeDictionary.Add(field_type._field_long_flags, typeof(int));
            ValueTypeDictionary.Add(field_type._field_byte_block_flags, typeof(byte));
            ValueTypeDictionary.Add(field_type._field_word_block_flags, typeof(short));
            ValueTypeDictionary.Add(field_type._field_long_block_flags, typeof(int));
            ValueTypeDictionary.Add(field_type._field_char_block_index1, typeof(byte));
            ValueTypeDictionary.Add(field_type._field_short_block_index1, typeof(short));
            ValueTypeDictionary.Add(field_type._field_long_block_index1, typeof(int));
            ValueTypeDictionary.Add(field_type._field_char_block_index2, typeof(byte));
            ValueTypeDictionary.Add(field_type._field_short_block_index2, typeof(short));
            ValueTypeDictionary.Add(field_type._field_long_block_index2, typeof(int));

            // Generate a list of types from the Mutation.Halo assembly.
            Type[] assemblyTypes = Assembly.GetAssembly(typeof(GuerillaTypeAttribute)).GetTypes();

            // Find all the types that have a GuerillaTypeAttribute attached to them.
            var guerillaTypes = from type in assemblyTypes
                                where type.GetCustomAttributes(typeof(GuerillaTypeAttribute), false).Count() > 0
                                select type;

            // Add all the types to the value type dictionary.
            foreach (Type fieldType in guerillaTypes)
            {
                // Get the guerilla type attributes attached to the type.
                GuerillaTypeAttribute[] guerillaAttr = (GuerillaTypeAttribute[])fieldType.GetCustomAttributes(typeof(GuerillaTypeAttribute), false);
                foreach (GuerillaTypeAttribute singleAttr in guerillaAttr)
                {
                    // Add the field type to the value type dictionary.
                    ValueTypeDictionary.Add(singleAttr.FieldType, fieldType);
                }
            }
        }

        #endregion
    }
}
