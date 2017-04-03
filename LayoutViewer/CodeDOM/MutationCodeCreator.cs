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

        /// <summary>
        /// Dictionary containing all Guerilla field_type's and their corresponding .Net object type.
        /// </summary>
        public Dictionary<field_type, Type> ValueTypeDictionary { get; private set; }

        /// <summary>
        /// Gets a list of all the cached BinaryReader methods.
        /// </summary>
        public Dictionary<Type, string> BinaryReaderMethods { get; private set; }

        /// <summary>
        /// Gets a list of all the cached BinaryWriter methods.
        /// </summary>
        public Dictionary<Type, string> BinaryWriterMethods { get; private set; }

        // CodeDOM objects used to create source files.
        private CodeCompileUnit codeUnit;
        private CodeNamespace codeNamespace;
        private CodeTypeDeclaration codeClass;

        // Reading and writing functions for the code class.
        private CodeMemberMethod readMethod;
        private CodeMemberMethod writeMethod;
        private CodeMemberMethod preProcessMethod;
        private CodeMemberMethod postProcessMethod;

        // Default import directives for tag definition classes.
        private readonly string[] namespaces = new string[]
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
        public static readonly string[] MutationNamespaces = new string[]
        {
            "Mutation.Halo.TagGroups",
            "Mutation.Halo.TagGroups.Attributes",
            "Mutation.Halo.TagGroups.FieldTypes"
        };

        /// <summary>
        /// Mutation namespace for the tag layouts.
        /// </summary>
        public const string MutationTagsNamespace = "Mutation.Halo.TagGroups.Tags";

        #endregion

        #region Constructor

        public MutationCodeCreator()
        {
            // Build the value type dictionary.
            BuildValueTypeDictionary();

            // Cache the binary reader and writer methods.
            CacheBinaryReaderWriterMethods();

            // Create a new code namespace for the class.
            this.codeNamespace = new CodeNamespace(MutationTagsNamespace);

            // Add the standard set of import directives to the namespace.
            foreach (string name in this.namespaces)
            {
                // Add the import to the namespace.
                this.codeNamespace.Imports.Add(new CodeNamespaceImport(name));
            }

            // Initialize a new code compile unit and add this namespace to it.
            this.codeUnit = new CodeCompileUnit();
            this.codeUnit.Namespaces.Add(this.codeNamespace);
        }

        #endregion

        /// <summary>
        /// Creates a new class for the tag group and returns a MutationCodeCreator who's root namespace is the new class.
        /// </summary>
        /// <param name="className">Name of the class definition.</param>
        /// <param name="baseType">Name of the base type if this class inherits another type.</param>
        /// <returns>A new MutationCodeCreator for the child namespace.</returns>
        public MutationCodeCreator CreateTagGroupClass(string className, string baseType = "")
        {
            // Create a new code creator instance.
            MutationCodeCreator codeCreator = new MutationCodeCreator();

            // Create the class code type declaration.
            codeCreator.codeClass = new CodeTypeDeclaration(className);
            codeCreator.codeClass.IsClass = true;
            codeCreator.codeClass.BaseTypes.Add(new CodeTypeReference("IMetaDefinition"));

            // Check if the base type was provided.
            if (baseType != string.Empty)
            {
                // Add the base type to the base types list.
                codeCreator.codeClass.BaseTypes.Add(new CodeTypeReference(baseType));
            }

            // Initialize the reading method for the class.
            codeCreator.readMethod = new CodeMemberMethod();
            codeCreator.readMethod.Name = "ReadDefinition";
            codeCreator.readMethod.Attributes = MemberAttributes.Public;
            codeCreator.readMethod.ReturnType = new CodeTypeReference("System.Void");
            codeCreator.readMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryReader", "reader"));

            // Initialize the writing method for the class.
            codeCreator.writeMethod = new CodeMemberMethod();
            codeCreator.writeMethod.Name = "WriteDefinition";
            codeCreator.writeMethod.Attributes = MemberAttributes.Public;
            codeCreator.writeMethod.ReturnType = new CodeTypeReference("System.Void");
            codeCreator.writeMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryWriter", "writer"));

            // Initialize the preprocess method for the class.
            codeCreator.preProcessMethod = new CodeMemberMethod();
            codeCreator.preProcessMethod.Name = "PreProcessDefinition";
            codeCreator.preProcessMethod.Attributes = MemberAttributes.Public;
            codeCreator.preProcessMethod.ReturnType = new CodeTypeReference("System.Void");

            // Initialize the postprocess method for the class.
            codeCreator.postProcessMethod = new CodeMemberMethod();
            codeCreator.postProcessMethod.Name = "PostProcessDefinition";
            codeCreator.postProcessMethod.Attributes = MemberAttributes.Public;
            codeCreator.postProcessMethod.ReturnType = new CodeTypeReference("System.Void");

            // Add the reading/writing and pre/postprocess methods to the class definition.
            codeCreator.codeClass.Members.Add(codeCreator.readMethod);
            codeCreator.codeClass.Members.Add(codeCreator.writeMethod);
            codeCreator.codeClass.Members.Add(codeCreator.preProcessMethod);
            codeCreator.codeClass.Members.Add(codeCreator.postProcessMethod);

            // Add the new tag group class to our namespace.
            this.codeNamespace.Types.Add(codeCreator.codeClass);

            // Return the new code creator.
            return codeCreator;
        }

        /// <summary>
        /// Creates a new class for the tag block and returns a MutationCodeCreator who's root namespace is the new class.
        /// </summary>
        /// <param name="blockName">Name of the block definition.</param>
        /// <returns>A new MutationCodeCreator for the child namespace.</returns>
        public MutationCodeCreator CreateTagBlockClass(string blockName)
        {
            // This method is here in case I decide to change the functionality of tag block definitions.
            // For now they are created the same as a tag group.
            MutationCodeCreator codeCreator = CreateTagGroupClass(blockName);

            // Add a region directive to the tag block.
            //codeCreator.codeClass.StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, blockName));

            // Return the new code creator for the tag block.
            return codeCreator;
        }

        /// <summary>
        /// Creates a new field for the specified type and adds it to the current code object.
        /// </summary>
        /// <param name="fieldType">Guerilla field type of the field.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        /// <param name="addToRead">Boolean indicating if the padding field should be added to the read method for the current structure.</param>
        /// <param name="addToWrite">Boolean indicating if the padding field should be added to the write method for the current structure.</param>
        public void AddField(field_type fieldType, string fieldName, 
            CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
        {
            // Get the underlying type for this field.
            Type standardFieldType = this.ValueTypeDictionary[fieldType];

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(MutationCodeFormatter.CreateShortCodeTypeReference(standardFieldType, MutationNamespaces), fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), 
                    this.BinaryReaderMethods[standardFieldType]);

                // Add the field to the read method.
                CodeAssignStatement assign = new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), invoke);
                this.readMethod.Statements.Add(assign);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), 
                    this.BinaryWriterMethods[standardFieldType], new CodeExpression[] 
                    {
                        // this.<fieldName>
                        new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)
                    });

                // Add the field to the write method.
                this.writeMethod.Statements.Add(invoke);
            }
        }

        /// <summary>
        /// Creates a new field for a custom type and adds it to the current code object.
        /// </summary>
        /// <param name="fieldType">Guerilla field type of the field.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="fieldTypeName">Name of the custom type.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        /// <param name="addToRead">Boolean indicating if the padding field should be added to the read method for the current structure.</param>
        /// <param name="addToWrite">Boolean indicating if the padding field should be added to the write method for the current structure.</param>
        public void AddCustomTypedField(field_type fieldType, string fieldName, string fieldTypeName, 
            CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
        {
            // Get the underlying type for this field.
            Type standardFieldType = this.ValueTypeDictionary[fieldType];

            // If the field name is the same as the type name then we have to append an '@' character.
            if (fieldName.Equals(fieldTypeName) == true)
                fieldName.Insert(0, "@");

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(fieldTypeName, fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression("reader"), 
                    this.BinaryReaderMethods[standardFieldType]);

                // Create the explicit cast expression to cast from the primitive type to the custom field type.
                CodeCastExpression cast = new CodeCastExpression(fieldTypeName, invoke);

                // Add the field to the read method.
                CodeAssignStatement assign = new CodeAssignStatement(
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), cast);
                this.readMethod.Statements.Add(assign);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the explicit cast expression to cast the custom field type to a primitive type.
                CodeCastExpression cast = new CodeCastExpression(standardFieldType, 
                    new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName));

                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"),
                    this.BinaryWriterMethods[standardFieldType], new CodeExpression[] { cast });

                // Add the field to the write method.
                this.writeMethod.Statements.Add(invoke);
            }
        }

        /// <summary>
        /// Creates a new enum or bitmask type definition in the current code object.
        /// </summary>
        /// <param name="codeScope">Code scope of the enum for ensuring each flag option is unique.</param>
        /// <param name="field">Enum definition from Guerilla.</param>
        public void AddEnumOrBitmask(MutationCodeScope codeScope, enum_definition field)
        {
            // Determine the underlying type for the enum based on the field type.
            Type fieldType = this.ValueTypeDictionary[field.type];

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
                string units, tooltip;
                string optionName = codeScope.CreateCodeSafeFieldName(field.options[i], out units, out tooltip);

                // Create a new CodeMemberField for the enum option.
                CodeMemberField option = new CodeMemberField
                {
                    Name = optionName,

                    // Set the flag value accordingly.
                    InitExpression = new CodeSnippetExpression(string.Format("0x{0}", 
                    (codeScope.Type == MutationCodeScopeType.Bitmask ? (1 << i) : i).ToString("x"))),
                };

                // Create a new UI markup attribute and add it to the enum option.
                option.CustomAttributes.Add(EditorMarkUpAttribute.CreateAttributeDeclaration(displayName: field.options[i], unitsSpecifier: units, tooltipText: tooltip));

                // Add the option to the enum.
                @enum.Members.Add(option);
            }

            // Add the enum to the class definition.
            this.codeClass.Members.Add(@enum);
        }

        /// <summary>
        /// Creates a new padding field and adds it to the current code object.
        /// </summary>
        /// <param name="codeScope">Code scope the padding field will be added to.</param>
        /// <param name="paddingLength">Size of the padding field.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        /// <param name="addToRead">Boolean indicating if the padding field should be added to the read method for the current structure.</param>
        /// <param name="addToWrite">Boolean indicating if the padding field should be added to the write method for the current structure.</param>
        public void AddPaddingField(MutationCodeScope codeScope, int paddingLength, 
            CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
        {
            // Create a code safe field name for the padding field.
            string units, tooltip;
            string fieldName = codeScope.CreateCodeSafeFieldName("", out units, out tooltip, true);

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(typeof(byte[]), fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression("reader"), "ReadBytes", new CodePrimitiveExpression(paddingLength));

                // Add the field to the read method.
                CodeAssignStatement assign = new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), invoke);
                this.readMethod.Statements.Add(assign);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"),
                    "Write", new CodeExpression[] { new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName) });

                // Add the field to the write method.
                this.writeMethod.Statements.Add(invoke);
            }
        }

        /// <summary>
        /// Creates a new tag block field and adds it to the current code object.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="blockTypeName">Name of the underlying tag block definition type.</param>
        /// <param name="attributeCollection">Collection of attributes to be put on the field.</param>
        /// <param name="addToRead">Boolean indicating if the padding field should be added to the read method for the current structure.</param>
        /// <param name="addToWrite">Boolean indicating if the padding field should be added to the write method for the current structure.</param>
        public void AddTagBlockField(string fieldName, string blockTypeName, CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
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
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), "ReadDefinition");
                invoke.Parameters.Add(new CodeVariableReferenceExpression("reader"));
                this.readMethod.Statements.Add(invoke);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), "WriteDefinition");
                invoke.Parameters.Add(new CodeVariableReferenceExpression("writer"));
                this.writeMethod.Statements.Add(invoke);
            }
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
                options.BlankLinesBetweenMembers = false;
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

        private void BuildValueTypeDictionary()
        {
            // Add the basic field types to the dictionary.
            this.ValueTypeDictionary = new Dictionary<field_type, Type>((int)field_type._field_type_max);
            this.ValueTypeDictionary.Add(field_type._field_char_integer, typeof(byte));
            this.ValueTypeDictionary.Add(field_type._field_short_integer, typeof(short));
            this.ValueTypeDictionary.Add(field_type._field_long_integer, typeof(int));
            this.ValueTypeDictionary.Add(field_type._field_angle, typeof(float));
            this.ValueTypeDictionary.Add(field_type._field_real, typeof(float));
            this.ValueTypeDictionary.Add(field_type._field_real_fraction, typeof(float));
            this.ValueTypeDictionary.Add(field_type._field_char_enum, typeof(byte));
            this.ValueTypeDictionary.Add(field_type._field_enum, typeof(short));
            this.ValueTypeDictionary.Add(field_type._field_long_enum, typeof(int));
            this.ValueTypeDictionary.Add(field_type._field_byte_flags, typeof(byte));
            this.ValueTypeDictionary.Add(field_type._field_word_flags, typeof(short));
            this.ValueTypeDictionary.Add(field_type._field_long_flags, typeof(int));
            this.ValueTypeDictionary.Add(field_type._field_byte_block_flags, typeof(byte));
            this.ValueTypeDictionary.Add(field_type._field_word_block_flags, typeof(short));
            this.ValueTypeDictionary.Add(field_type._field_long_block_flags, typeof(int));
            this.ValueTypeDictionary.Add(field_type._field_char_block_index1, typeof(byte));
            this.ValueTypeDictionary.Add(field_type._field_short_block_index1, typeof(short));
            this.ValueTypeDictionary.Add(field_type._field_long_block_index1, typeof(int));
            this.ValueTypeDictionary.Add(field_type._field_char_block_index2, typeof(byte));
            this.ValueTypeDictionary.Add(field_type._field_short_block_index2, typeof(short));
            this.ValueTypeDictionary.Add(field_type._field_long_block_index2, typeof(int));

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
                    this.ValueTypeDictionary.Add(singleAttr.FieldType, fieldType);
                }
            }
        }

        private void CacheBinaryReaderWriterMethods()
        {
            // Initialize the method cache lists.
            this.BinaryReaderMethods = new Dictionary<Type, string>();
            this.BinaryWriterMethods = new Dictionary<Type, string>();

            // Build the list of all binary reader methods we are interested in.
            List<MethodInfo> readerMethods = FindAllReadMethods();

            // Add all the binary reader methods to the cache list.
            foreach (var method in readerMethods)
                this.BinaryReaderMethods.Add(method.ReturnType, method.Name);

            // Build the list of all binary writer methods we are interested in.
            List<MethodInfo> writerMethods = FindAllWriteMethods();

            // Add all of the binary writer methods to the cache list.
            foreach (var method in writerMethods)
                this.BinaryWriterMethods.Add(method.GetParameters().Count() > 1 ? method.GetParameters()[1].ParameterType : method.GetParameters()[0].ParameterType, method.Name);
        }

        private List<MethodInfo> FindAllReadMethods()
        {
            // Generate a list of types from the Mutation.Halo assembly.
            Type[] assemblyTypes = Assembly.GetAssembly(typeof(GuerillaTypeAttribute)).GetTypes();

            // Build a list of binary reader extension methods by searching the Mutation.Halo namespace.
            List<MethodInfo> extensionMethods = (from type in assemblyTypes
                                    where type.IsSealed && !type.IsGenericType && !type.IsNested
                                    from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                    where method.IsDefined(typeof(ExtensionAttribute), false)
                                    where method.GetParameters()[0].ParameterType == typeof(BinaryReader)
                                    select method).ToList();

            // Trim the list down to only one method per return type.
            extensionMethods = (from method in extensionMethods
                                group method by method.ReturnType
                                    into g
                                    select g.First()).ToList();

            // Build a list of native binary reader methods.
            List<MethodInfo> methods = (from method in typeof(BinaryReader).GetMethods()
                           where method.ReturnType != typeof(void)
                           select method).Where(x =>
                               {
                                   // Verify the name of the method contains the return type.
                                   string returnType = x.ReturnType.ToString().Split('.').Last();
                                   return x.Name.Contains(returnType);
                               }).ToList();

            // Trim the list down to one method per return type.
            methods = (from method in methods
                       group method by method.ReturnType
                           into g
                           select g.First()).ToList();

            // Combine the two lists and return the final collection.
            return (List<MethodInfo>)methods.Union(extensionMethods).ToList();
        }

        private List<MethodInfo> FindAllWriteMethods()
        {
            // Generate a list of types from the Mutation.Halo assembly.
            Type[] assemblyTypes = Assembly.GetAssembly(typeof(GuerillaTypeAttribute)).GetTypes();

            // Build a list of binary writer extension methods by searching the Mutation.Halo namespace.
            List<MethodInfo> extensionMethods = (from type in assemblyTypes
                                                 where type.IsSealed && !type.IsGenericType && !type.IsNested
                                                 from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                                 where method.IsDefined(typeof(ExtensionAttribute), false)
                                                 where method.ReturnType == typeof(void)
                                                 where method.Name == "Write"
                                                 where method.GetParameters().Count() > 1 && method.GetParameters()[0].ParameterType == typeof(BinaryWriter)
                                                 select method).ToList();

            // Trim the list down to only one method per parameter type.
            extensionMethods = (from method in extensionMethods
                                group method by method.GetParameters()[1].ParameterType
                                    into g
                                    select g.First()).ToList();

            // Build a list of native binary writer methods.
            List<MethodInfo> methods = (from method in typeof(BinaryWriter).GetMethods()
                                        where method.ReturnType == typeof(void)
                                        where method.Name == "Write"
                                        where method.GetParameters().Count() > 0
                                        select method).ToList();

            // Trim the list down to one method per parameter type.
            methods = (from method in methods
                       group method by method.GetParameters()[0].ParameterType
                           into g
                           select g.First()).ToList();

            // Combine the two lists and return the final collection.
            return (List<MethodInfo>)methods.Union(extensionMethods).ToList();
        }

        #endregion
    }
}
