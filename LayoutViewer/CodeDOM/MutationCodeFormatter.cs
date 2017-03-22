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

        public static void ProcessFieldName(string fieldText, out string name, out string comment)
        {
            // Satisfy the compiler.
            name = string.Empty;
            comment = string.Empty;

            // Loop through the field text and process.
            char lastChar = ' ';
            bool doComment = false;
            for (int i = 0; i < fieldText.Length; i++)
            {
                // Check if we are doing the comment or not.
                if (doComment == false)
                {
                    // Check the current character.
                    if (char.IsLetterOrDigit(fieldText[i]) == true)
                    {
                        // Check if this is the first character.
                        if (i == 0)
                        {
                            // Just add the character
                            name += char.ToLower(fieldText[i]);
                        }
                        else
                        {
                            // Check the last character.
                            if (lastChar == ' ' || lastChar == ':')
                            {
                                // Capitalize the character and add it to the name.
                                name += char.ToUpper(fieldText[i]);
                            }
                            else
                                name += fieldText[i];
                        }
                    }
                    else if (fieldText[i] == '#')
                    {
                        // Switch to comment.
                        doComment = true;
                    }

                    // Save the current char as the last char.
                    lastChar = fieldText[i];
                }
                else
                {
                    // Just add the character to the comment.
                    comment += fieldText[i];
                }
            }
        }

        public static string ProcessMemberName(string memberName)
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
                        if (lastChar == ' ' || lastChar == ':' || lastChar == '_')
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
