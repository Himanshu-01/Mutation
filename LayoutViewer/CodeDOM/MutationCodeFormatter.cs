using Mutation.Halo.TagGroups.Attributes;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public static class MutationCodeFormatter
    {
        /// <summary>
        /// Denotes the start of the units specifier string.
        /// </summary>
        public const char FieldNameUnitsCharacter = ':';
        /// <summary>
        /// Denotes the start of the tooltip text string.
        /// </summary>
        public const char FieldNameToolTipCharacter = '#';
        /// <summary>
        /// Declares the field as read-only.
        /// </summary>
        public const char FieldNameReadOnlyCharacter = '*';
        /// <summary>
        /// Declares the field an advanced field, and will only be displayed in the advanced view.
        /// </summary>
        public const char FieldNameAdvancedCharacter = '!';
        /// <summary>
        /// Declares the field the naming field for blocks in a tag_block field.
        /// </summary>
        public const char FieldNameBlockNameCharacter = '^';

        // List of generally invalid characters in field names.
        private static readonly char[] InvalidCharacters = { ' ', '(', ')', '-', '^', '.', '@', '*', '!', '#', '$', '&', '=', '+', '<', '>', '/', '\'', ',', '[', ']' };

        public static CodeTypeReference CreateShortCodeTypeReference(Type fieldType, string[] imports)
        {
            // Create a new CodeTypeReference for the field type.
            CodeTypeReference typeReference = new CodeTypeReference(fieldType);

            // Process the field type.
            return CreateShortCodeTypeReference(typeReference, fieldType, imports);
        }

        // Copied from: http://stackoverflow.com/questions/27892258/how-to-generate-code-with-short-type-names-instead-of-full-type-names
        private static CodeTypeReference CreateShortCodeTypeReference(CodeTypeReference typeReference, Type fieldType, string[] imports)
        {
            // Check some underlying type thing I don't totally understand.
            if (typeReference.ArrayRank > 0)
            {
                // Recursively process the type name.
                return CreateShortCodeTypeReference(typeReference.ArrayElementType, fieldType, imports);
            }

            // Check if the imports collection contains the namespace for the field type.
            if (fieldType.Namespace != null && imports.Any(@namespace => @namespace == fieldType.Namespace))
            {
                // Get the namespace prefix for the field type.
                string namespacePrefix = fieldType.Namespace + '.';

                // Remove the namespace prefix from the field's basetype if it exists.
                if (typeReference.BaseType != null && typeReference.BaseType.Contains(namespacePrefix) == true)
                    typeReference.BaseType = typeReference.BaseType.Substring(namespacePrefix.Length);
            }

            // Return the modified (or not) type reference object.
            return typeReference;
        }

        public static string FormatDefinitionName(string name)
        {
            string newName = "";

            // Split the name using the '_' character as the delimiter.
            string[] names = name.Split(new char[] { '_' });

            // Loop through all the parts and add each one to the new name.
            foreach (string part in names)
            {
                // Capitalize the first character of the name.
                newName += char.ToUpper(part[0]);
                newName += part.Substring(1);
            }

            // Return the newly formatted name.
            return newName;
        }

        /// <summary>
        /// Processes a Guerilla UI field name back into a code safe field name with UI markup.
        /// </summary>
        /// <param name="fieldText">The Guerilla field name to process.</param>
        /// <param name="fieldName">The code safe field name.</param>
        /// <param name="displayName">The UI mark up display name for the field.</param>
        /// <param name="units">The units associated with the field (UI markup data).</param>
        /// <param name="tooltip">The tooltip associated with the field (UI markup data).</param>
        public static void ProcessFieldName(string fieldText, out string fieldName, out string displayName, out string units, out string tooltip)
        {
            // Satisfy the compiler.
            fieldName = string.Empty;
            displayName = string.Empty;
            units = string.Empty;
            tooltip = string.Empty;

            // Split the string using the markup delimiters.
            string[] pieces = fieldText.Split(new char[] { FieldNameUnitsCharacter, FieldNameToolTipCharacter });

            // Create our indcies for splicing.
            int lastIndex = fieldText.Length;
            int unitsIndex = fieldText.IndexOf(FieldNameUnitsCharacter);
            int toolTipIndex = fieldText.IndexOf(FieldNameToolTipCharacter);

            // Check if the tooltip string exists.
            if (toolTipIndex != -1)
            {
                // Determine which piece the tooltip string is.
                tooltip = CreateCodeSafeStringLiteral(toolTipIndex > unitsIndex ? pieces[pieces.Length - 1] : pieces[pieces.Length - 2]);

                // Set the last index so we can continue parsing.
                if (toolTipIndex < lastIndex)
                    lastIndex = toolTipIndex;
            }

            // Check if the units string exists.
            if (unitsIndex != -1)
            {
                // Determine which piece the units string is.
                units = CreateCodeSafeStringLiteral(unitsIndex > toolTipIndex ? pieces[pieces.Length - 1] : pieces[pieces.Length - 2]);

                // Set the last index so we can continue parsing.
                if (unitsIndex < lastIndex)
                    lastIndex = unitsIndex;
            }

            // Split out the field name and sanitize it.
            displayName = CreateCodeSafeStringLiteral(pieces[0]);
            fieldName = CreateCodeSafeFieldName(pieces[0]);

            // If the name ends with a safe character just remove it.
            if (fieldName.EndsWith("_") == true)
                fieldName = fieldName.Remove(fieldName.Length - 1);
        }

        /// <summary>
        /// Sanitizes the <see cref="fieldName"/> into a code safe field name.
        /// </summary>
        /// <param name="fieldName">The unsafe field name to be processed</param>
        /// <returns>The code safe equivalent of <see cref="fieldName"/></returns>
        public static string CreateCodeSafeFieldName(string fieldName)
        {
            string newFieldName = "";

            // Loop through the entire string and check for any invalid field name characters.
            for (int i = 0; i < fieldName.Length; i++)
            {
                // Process the current character and add it to the output string.
                newFieldName += (InvalidCharacters.Contains(fieldName[i]) == true ? '_' : fieldName[i]);
            }

            // Check if the new field name is valid.
            if (newFieldName.Length == 0 || newFieldName == "_")
                return string.Empty;

            // Remove any trailing "_" characters.
            newFieldName = newFieldName.TrimEnd(new char[] { '_' });

            // Check if the first character is a digit and if so append a safe character.
            if (newFieldName.Length > 0 && char.IsDigit(newFieldName[0]) == true)
                newFieldName = newFieldName.Insert(0, "_");

            // Return the newly formatted field name.
            return newFieldName;
        }

        /// <summary>
        /// Sanitizes <see cref="memberName"/> into a code safe enum/flag value name.
        /// </summary>
        /// <param name="memberName">The unsafe field name to be processed</param>
        /// <returns>The code safe equivalent of <see cref="memberName"/></returns>
        public static string CreateCodeSafeFlagName(string memberName)
        {
            // Create a temporary string.
            string name = "";

            // Loop through the name and process each character.
            char lastChar = ' ';
            for (int i = 0; i < memberName.Length; i++)
            {
                // Check the current character.
                if (char.IsLetterOrDigit(memberName[i]) == true)
                {
                    // Check if this is the first character.
                    if (i > 0)
                    {
                        // Check the last character.
                        if (InvalidCharacters.Contains(lastChar) == true)
                        {
                            // Capitalize the character and add it to the name.
                            name += char.ToUpper(memberName[i]);
                        }
                        else
                            name += memberName[i];
                    }
                }

                // Save the current char as the last char.
                lastChar = memberName[i];
            }

            // Remove any trailing "_" characters.
            memberName = memberName.TrimEnd(new char[] { '_' });

            // Check if the first character of the flag name is a digit, and if so make it safe.
            if (name.Length > 0 && char.IsDigit(name[0]) == true)
            {
                // Append and '_' character to the flag name.
                name = name.Insert(0, "_");
            }

            // Return the processed name
            return name;
        }

        /// <summary>
        /// Sanatizes <see cref="literal"/> into a code safe string literal.
        /// </summary>
        /// <param name="literal">The unsafe string literal to sanatize.</param>
        /// <returns>The code safe equivalent of <see cref="literal"/></returns>
        /// <see cref="http://stackoverflow.com/questions/323640/can-i-convert-a-c-sharp-string-value-to-an-escaped-string-literal"/>
        public static string CreateCodeSafeStringLiteral(string literal)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    // Let CodeDOM do the sanatizing, it will handle everything we need to do.
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(literal), writer, null);
                    return writer.ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static EditorMarkUpFlags MarkupFlagsFromFieldName(string fieldName)
        {
            EditorMarkUpFlags flags = EditorMarkUpFlags.None;

            // Check the field name for the read-only character.
            flags |= (fieldName.Contains(FieldNameReadOnlyCharacter) == true ? EditorMarkUpFlags.ReadOnly : EditorMarkUpFlags.None);

            // Check the field name for the advanced view character.
            flags |= (fieldName.Contains(FieldNameAdvancedCharacter) == true ? EditorMarkUpFlags.Advanced : EditorMarkUpFlags.None);

            // Check the field name for the block naming character.
            flags |= (fieldName.Contains(FieldNameBlockNameCharacter) == true ? EditorMarkUpFlags.BlockName : EditorMarkUpFlags.None);

            // Return the markup flags.
            return flags;
        }

        /// <summary>
        /// Determines if <see cref="value"/> is a valid Guerilla field name.
        /// </summary>
        /// <param name="value">String to check.</param>
        /// <returns>True if <see cref="value"/> is a valid Guerilla field name.</returns>
        public static bool IsValidFieldName(string value)
        {
            string[] invalidNames = new[] 
            { 
                "EMPTY_STRING",
                "EMPTY STRING",
                "EMPTYSTRING", 
                "", 
                "YOUR MOM"
            };
            return !invalidNames.Any(x => value.Equals(x));
        }
    }
}
