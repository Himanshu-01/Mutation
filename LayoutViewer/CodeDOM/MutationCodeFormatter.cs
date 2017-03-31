using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public static class MutationCodeFormatter
    {
        // Constants for parsing field names.
        private const char FieldNameUnitsSeparator = ':';
        private const char FieldNameToolTipSeparator = '#';

        // List of generally invalid characters in field names.
        private static readonly char[] InvalidCharacters = { ' ', '(', ')', '-', '^', '.', '@', '*', '<', '>', '/', '\'', ',', '[', ']' };

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
        /// <param name="name">The code safe field name.</param>
        /// <param name="units">The units associated with the field (UI markup data).</param>
        /// <param name="tooltip">The tooltip associated with the field (UI markup data).</param>
        public static void ProcessFieldName(string fieldText, out string name, out string units, out string tooltip)
        {
            // Satisfy the compiler.
            name = string.Empty;
            units = string.Empty;
            tooltip = string.Empty;

            // Split the string using the markup delimiters.
            string[] pieces = fieldText.Split(new char[] { FieldNameUnitsSeparator, FieldNameToolTipSeparator });

            // Create our indcies for splicing.
            int lastIndex = fieldText.Length;
            int unitsIndex = fieldText.IndexOf(FieldNameUnitsSeparator);
            int toolTipIndex = fieldText.IndexOf(FieldNameToolTipSeparator);

            // Check if the tooltip string exists.
            if (toolTipIndex != -1)
            {
                // Determine which piece the tooltip string is.
                tooltip = (toolTipIndex > unitsIndex ? pieces[pieces.Length - 1] : pieces[pieces.Length - 2]);

                // Set the last index so we can continue parsing.
                if (toolTipIndex < lastIndex)
                    lastIndex = toolTipIndex;
            }

            // Check if the units string exists.
            if (unitsIndex != -1)
            {
                // Determine which piece the units string is.
                units = (unitsIndex > toolTipIndex ? pieces[pieces.Length - 1] : pieces[pieces.Length - 2]);

                // Set the last index so we can continue parsing.
                if (unitsIndex < lastIndex)
                    lastIndex = unitsIndex;
            }

            // Split out the field name and sanitize it.
            name = CreateCodeSafeFieldName(fieldText.Substring(0, lastIndex));

            // If the name ends with a safe character just remove it.
            if (name.EndsWith("_") == true)
                name = name.Remove(name.Length - 1);
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
                    if (i == 0)
                    {
                        // Just add the character
                        name += char.ToUpper(memberName[i]);
                    }
                    else
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

            // Check if the first character of the flag name is a digit, and if so make it safe.
            if (name.Length > 0 && char.IsDigit(name[0]) == true)
            {
                // Append and '_' character to the flag name.
                name.Insert(0, "_");
            }

            // Return the processed name
            return name;
        }

        public static bool IsValidFieldName(string value)
        {
            string[] invalidNames = new[] 
            { 
                "EMPTY_STRING",
                "EMPTYSTRING", 
                "", 
                "YOUR MOM"
            };
            return !invalidNames.Any(x => value.Equals(x));
        }
    }
}
