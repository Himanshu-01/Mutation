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

        // Number of padding fields added to the class definition.
        private int paddingFieldCount;

        // Default import directives for tag definition classes.
        private readonly string[] namespaces = new string[]
        {
            "System",
            "System.IO",
            "Mutation.Halo.TagGroups",
            "Mutation.Halo.TagGroups.Attributes",
            "Mutation.Halo.TagGroups.FieldTypes"
        };

        public static readonly string[] MutationNamespaces = new string[]
        {
            "Mutation.Halo.TagGroups",
            "Mutation.Halo.TagGroups.Attributes",
            "Mutation.Halo.TagGroups.FieldTypes"
        };

        #endregion

        #region Constructor

        public MutationCodeCreator()
        {
            // Build the value type dictionary.
            BuildValueTypeDictionary();

            // Cache the binary reader and writer methods.
            CacheBinaryReaderWriterMethods();
        }

        #endregion

        /// <summary>
        /// Initialize a new CodeCompileUnit with a class definition based on the fields provided.
        /// </summary>
        /// <param name="className">Name of the class definition.</param>
        /// <param name="namespaceName">Namespace the class will be created in.</param>
        public void CreateTagDefinitionClass(string className, string namespaceName)
        {
            // Create a new code namespace for the class.
            this.codeNamespace = new CodeNamespace(namespaceName);

            // Add the standard set of import directives to the namespace.
            foreach (string name in this.namespaces)
            {
                // Add the import to the namespace.
                this.codeNamespace.Imports.Add(new CodeNamespaceImport(name));
            }

            // Create the class code type declaration.
            this.codeClass = new CodeTypeDeclaration(className);
            this.codeClass.IsClass = true;
            this.codeClass.BaseTypes.Add(new CodeTypeReference("IMetaDefinition"));

            // Initialize the reading method for the class.
            this.readMethod = new CodeMemberMethod();
            this.readMethod.Name = "ReadDefinition";
            this.readMethod.Attributes = MemberAttributes.Public;
            this.readMethod.ReturnType = new CodeTypeReference("System.Void");
            this.readMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryReader", "reader"));

            // Initialize the writing method for the class.
            this.writeMethod = new CodeMemberMethod();
            this.writeMethod.Name = "WriteDefinition";
            this.writeMethod.Attributes = MemberAttributes.Public;
            this.writeMethod.ReturnType = new CodeTypeReference("System.Void");
            this.writeMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryWriter", "writer"));

            // Initialize the preprocess method for the class.
            this.preProcessMethod = new CodeMemberMethod();
            this.preProcessMethod.Name = "PreProcessDefinition";
            this.preProcessMethod.Attributes = MemberAttributes.Public;
            this.preProcessMethod.ReturnType = new CodeTypeReference("System.Void");

            // Initialize the postprocess method for the class.
            this.postProcessMethod = new CodeMemberMethod();
            this.postProcessMethod.Name = "PostProcessDefinition";
            this.postProcessMethod.Attributes = MemberAttributes.Public;
            this.postProcessMethod.ReturnType = new CodeTypeReference("System.Void");

            // Add the reading/writing and pre/postprocess methods to the class definition.
            this.codeClass.Members.Add(this.readMethod);
            this.codeClass.Members.Add(this.writeMethod);
            this.codeClass.Members.Add(this.preProcessMethod);
            this.codeClass.Members.Add(this.postProcessMethod);

            // Add the class type to the namespace.
            this.codeNamespace.Types.Add(this.codeClass);

            // Initialize a new code compile unit and add this namespace to it.
            this.codeUnit = new CodeCompileUnit();
            this.codeUnit.Namespaces.Add(this.codeNamespace);

            // Reset the padding field count.
            this.paddingFieldCount = 0;
        }

        public MutationCodeCreator CreateTagBlockClass(string blockName)
        {
            // Create a new MutationCodeCreator instance that links to this code creator for the tag block.
            MutationCodeCreator tagBlockCodeCreator = new MutationCodeCreator();

            // Create a new code namespace for the class.
            tagBlockCodeCreator.codeNamespace = this.codeNamespace;

            // Create the class code type declaration.
            tagBlockCodeCreator.codeClass = new CodeTypeDeclaration(blockName);
            tagBlockCodeCreator.codeClass.IsClass = true;
            tagBlockCodeCreator.codeClass.BaseTypes.Add(new CodeTypeReference("IMetaDefinition"));

            // Initialize the reading method for the class.
            tagBlockCodeCreator.readMethod = new CodeMemberMethod();
            tagBlockCodeCreator.readMethod.Name = "ReadDefinition";
            tagBlockCodeCreator.readMethod.Attributes = MemberAttributes.Public;
            tagBlockCodeCreator.readMethod.ReturnType = new CodeTypeReference("System.Void");
            tagBlockCodeCreator.readMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryReader", "reader"));

            // Initialize the writing method for the class.
            tagBlockCodeCreator.writeMethod = new CodeMemberMethod();
            tagBlockCodeCreator.writeMethod.Name = "WriteDefinition";
            tagBlockCodeCreator.writeMethod.Attributes = MemberAttributes.Public;
            tagBlockCodeCreator.writeMethod.ReturnType = new CodeTypeReference("System.Void");
            tagBlockCodeCreator.writeMethod.Parameters.Add(new CodeParameterDeclarationExpression("BinaryWriter", "writer"));

            // Initialize the preprocess method for the class.
            tagBlockCodeCreator.preProcessMethod = new CodeMemberMethod();
            tagBlockCodeCreator.preProcessMethod.Name = "PreProcessDefinition";
            tagBlockCodeCreator.preProcessMethod.Attributes = MemberAttributes.Public;
            tagBlockCodeCreator.preProcessMethod.ReturnType = new CodeTypeReference("System.Void");

            // Initialize the postprocess method for the class.
            tagBlockCodeCreator.postProcessMethod = new CodeMemberMethod();
            tagBlockCodeCreator.postProcessMethod.Name = "PostProcessDefinition";
            tagBlockCodeCreator.postProcessMethod.Attributes = MemberAttributes.Public;
            tagBlockCodeCreator.postProcessMethod.ReturnType = new CodeTypeReference("System.Void");

            // Add the reading/writing and pre/postprocess methods to the class definition.
            tagBlockCodeCreator.codeClass.Members.Add(tagBlockCodeCreator.readMethod);
            tagBlockCodeCreator.codeClass.Members.Add(tagBlockCodeCreator.writeMethod);
            tagBlockCodeCreator.codeClass.Members.Add(tagBlockCodeCreator.preProcessMethod);
            tagBlockCodeCreator.codeClass.Members.Add(tagBlockCodeCreator.postProcessMethod);

            // Add the class type to the namespace.
            tagBlockCodeCreator.codeNamespace.Types.Add(tagBlockCodeCreator.codeClass);

            // Reset the padding field count.
            tagBlockCodeCreator.paddingFieldCount = 0;

            // Return the new code creator for the tag block.
            return tagBlockCodeCreator;
        }

        public void AddField(field_type type, string name, CodeCommentStatementCollection comments = null, CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
        {
            // Get the proper field type for this field.
            Type fieldType = this.ValueTypeDictionary[type];

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(MutationCodeFormatter.CreateShortCodeTypeReference(fieldType, MutationNamespaces), name);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Check if there is a comment for this field and if so add it to the comments collection.
            if (comments != null)
                field.Comments.AddRange(comments);

            // Add the field to the class definition.
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), this.BinaryReaderMethods[fieldType]);

                // Add the field to the read method.
                CodeAssignStatement assign = new CodeAssignStatement(new CodeVariableReferenceExpression(name), invoke);
                this.readMethod.Statements.Add(assign);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"), 
                    this.BinaryWriterMethods[fieldType], new CodeExpression[] { new CodeVariableReferenceExpression(name) });

                // Add the field to the write method.
                this.writeMethod.Statements.Add(invoke);
            }
        }

        public void AddField(field_type type, string typeName, string name, CodeCommentStatementCollection comments = null, CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
        {
            // Get the proper field type for this field.
            Type fieldType = this.ValueTypeDictionary[type];

            // If the field name is the same as the type name then we have to append an '@' character.
            if (name.Equals(typeName) == true)
                name.Insert(0, "@");

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(typeName, name);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Check if there is a comment for this field and if so add it to the comments collection.
            if (comments != null)
                field.Comments.AddRange(comments);

            // Add the field to the class definition.
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), this.BinaryReaderMethods[fieldType]);

                // Create the explicit cast expression to cast from the primitive type to the custom field type.
                CodeCastExpression cast = new CodeCastExpression(typeName, invoke);

                // Add the field to the read method.
                CodeAssignStatement assign = new CodeAssignStatement(new CodeVariableReferenceExpression(name), cast);
                this.readMethod.Statements.Add(assign);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the explicit cast expression to cast the custom field type to a primitive type.
                CodeCastExpression cast = new CodeCastExpression(fieldType, new CodeVariableReferenceExpression(name));

                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"),
                    this.BinaryWriterMethods[fieldType], new CodeExpression[] { cast });

                // Add the field to the write method.
                this.writeMethod.Statements.Add(invoke);
            }
        }

        public string AddEnum(enum_definition field, string name, bool isBitmask)
        {
            // Format the enum name to be a proper type.
            string enumName = string.Format("{0}{1}", char.ToUpper(name[0]), name.Substring(1));

            // Determine the underlying type for the enum based on the field type.
            Type fieldType = this.ValueTypeDictionary[field.type];

            // Create the enum field by creating a new CodeTypeDeclaration.
            CodeTypeDeclaration @enum = new CodeTypeDeclaration(enumName);
            @enum.IsEnum = true;
            @enum.BaseTypes.Add(fieldType);

            // Check if this field is a bitmask.
            if (isBitmask == true)
            {
                // Setup a code attribute declaration object for the Flags attribute.
                CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(new CodeTypeReference(typeof(FlagsAttribute).Name));
                @enum.CustomAttributes.Add(attribute);
            }

            // Loop through all of the enum options and add each one to the enum field.
            for (int i = 0; i < field.option_count; i++)
            {
                // Check if the option name is blank.
                if (field.options[i] == string.Empty)
                    continue;

                // Create a new CodeMemberField for the enum option.
                CodeMemberField option = new CodeMemberField
                {
                    Name = MutationCodeFormatter.CreateCodeSafeFlagName(field.options[i]),

                    // I really want this to be a hex value, but CodeDOM doesn't seem to be able to do this.
                    //InitExpression = new CodePrimitiveExpression(isBitmask == true ? (1 << i) : i),
                    InitExpression = new CodeSnippetExpression(string.Format("0x{0}", (isBitmask == true ? (1 << i) : i).ToString("x"))),
                };

                // Add the option to the enum.
                @enum.Members.Add(option);
            }

            // Add the enum to the class definition.
            this.codeClass.Members.Add(@enum);

            // Return the enum type name.
            return enumName;
        }

        public void AddPaddingField(int size, CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
        {
            // Create the field name.
            string fieldName = string.Format("padding{0}", ++this.paddingFieldCount);
            Type fieldType = typeof(byte[]);

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(fieldType, fieldName);
            field.Attributes = MemberAttributes.Public;

            // Add any attributes for this field.
            field.CustomAttributes = attributeCollection;

            // Add the field to the class definition.
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("reader"), "ReadBytes", new CodePrimitiveExpression(size));

                // Add the field to the read method.
                CodeAssignStatement assign = new CodeAssignStatement(new CodeVariableReferenceExpression(fieldName), invoke);
                this.readMethod.Statements.Add(assign);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("writer"),
                    "Write", new CodeExpression[] { new CodeVariableReferenceExpression(fieldName) });

                // Add the field to the write method.
                this.writeMethod.Statements.Add(invoke);
            }
        }

        public void AddTagBlock(TagBlockDefinition definition, string typeName, string name, CodeCommentStatementCollection comments = null, CodeAttributeDeclarationCollection attributeCollection = null, bool addToRead = true, bool addToWrite = true)
        {
            // Create a new code type reference to reference the tag_block data type.
            CodeTypeReference tagBlockType = MutationCodeFormatter.CreateShortCodeTypeReference(ValueTypeDictionary[field_type._field_block], MutationNamespaces);
            tagBlockType.TypeArguments.Add(typeName);

            // Create a new code member field for the tag field.
            CodeMemberField field = new CodeMemberField(tagBlockType, name);
            field.Attributes = MemberAttributes.Public;

            // Setup a code type reference for the attribute type.
            CodeTypeReference attType = MutationCodeFormatter.CreateShortCodeTypeReference(typeof(TagBlockDefinitionAttribute), MutationNamespaces);

            // Setup a TagBlockDefinitionAttribute attribute for this tag block using the definition info.
            tag_field_set fieldSet = definition.TagFieldSets[definition.TagFieldSetLatestIndex];
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(attType, new CodeAttributeArgument[] {
                // CodeDOM doesn't seem to support named parameters so we are going to do some h4x here...
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("sizeOf: {0}", fieldSet.size))),
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("alignment: {0}", fieldSet.alignment_bit != 0 ? (1 << fieldSet.alignment_bit) : 4))),
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("maxBlockCount: {0}", definition.s_tag_block_definition.maximum_element_count)))
            });

            // Add it to the attributes list.
            field.CustomAttributes.Add(attribute);

            // Add any attributes for this field.
            if (attributeCollection != null)
                field.CustomAttributes.AddRange(attributeCollection);

            // Check if there is a comment for this field and if so add it to the comments collection.
            if (comments != null)
                field.Comments.AddRange(comments);

            // Add the field to the class definition.
            this.codeClass.Members.Add(field);

            // Check if we should add the field to the read method.
            if (addToRead == true)
            {
                // Create the binary reader invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(name), "ReadDefinition");
                invoke.Parameters.Add(new CodeVariableReferenceExpression("reader"));
                this.readMethod.Statements.Add(invoke);
            }

            // Check if we should add the field to the write method.
            if (addToWrite == true)
            {
                // Create the binary writer invoke statement.
                CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(name), "WriteDefinition");
                invoke.Parameters.Add(new CodeVariableReferenceExpression("writer"));
                this.writeMethod.Statements.Add(invoke);
            }
        }

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

        private void BuildValueTypeDictionary()
        {
            // Add the basic field types to the dictionary.
            this.ValueTypeDictionary = new Dictionary<field_type, Type>((int)field_type._field_type_max);
            this.ValueTypeDictionary.Add(field_type._field_char_integer, typeof(byte));
            this.ValueTypeDictionary.Add(field_type._field_short_integer, typeof(short));
            this.ValueTypeDictionary.Add(field_type._field_long_integer, typeof(int));
            this.ValueTypeDictionary.Add(field_type._field_angle, typeof(float));
            this.ValueTypeDictionary.Add(field_type._field_real, typeof(float));
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
