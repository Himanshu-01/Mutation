using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public enum MutationCodeScopeType
    {
        GlobalNamespace,
        TagGroup,
        TagBlock,
        Struct,
        Enum,
        Bitmask
    }

    public class MutationCodeScope
    {
        /// <summary>
        /// Full type name of the type this scope encapsulates.
        /// </summary>
        public string Namespace { get; private set; }

        /// <summary>
        /// Parent type/namespace for this scope, ie one level up.
        /// </summary>
        public string ParentNamespace { get; private set; }

        /// <summary>
        /// Type of code scope, ie namespace, class, struct, enum.
        /// </summary>
        public MutationCodeScopeType Type { get; private set; }

        /// <summary>
        /// List of fields that are present in this code scope, cannot contain duplicates.
        /// </summary>
        public HashSet<string> Fields { get; private set; }

        /// <summary>
        /// Dictionary of types in this code scope, and their corresponding MutationCodeScope.
        /// </summary>
        public Dictionary<string, MutationCodeScope> Types { get; private set; }

        /// <summary>
        /// Address of the Guerilla definition that corresponds to this code scope.
        /// </summary>
        public int DefinitionAddress { get; private set; }

        /// <summary>
        /// Number of no name fields in the current scope.
        /// </summary>
        private int noNameFieldCount = 0;

        /// <summary>
        /// Number of no name types in the current scope.
        /// </summary>
        private int noNameTypeCount = 0;

        /// <summary>
        /// Number of padding fields in the current scope.
        /// </summary>
        private int paddingFieldCount = 0;

        /// <summary>
        /// Number of explanation fields in the current scope.
        /// </summary>
        private int explanationFieldCount = 0;

        /// <summary>
        /// Creates a new MutationCodeScope using the scope information provided.
        /// </summary>
        /// <param name="@namespace">Full type name for the code scope</param>
        /// <param name="parentNamepace">Full type name of the parent type/namespace including the namespace</param>
        /// <param name="definitionAddress">Guerilla definition address that corresponds to this code scope</param>
        /// <param name="scopeType">Type of code scope</param>
        public MutationCodeScope(string @namespace, string parentNamepace, int definitionAddress, MutationCodeScopeType scopeType)
        {
            // Initialize fields.
            this.Namespace = @namespace;
            this.ParentNamespace = parentNamepace;
            this.DefinitionAddress = definitionAddress;
            this.Type = scopeType;

            // Initialize field/type lists.
            this.Fields = new HashSet<string>();
            this.Types = new Dictionary<string, MutationCodeScope>();
        }

        /// <summary>
        /// Creates a new code safe field name that is unique in the current scope and adds it to the fields list for this scope.
        /// </summary>
        /// <param name="fieldType">Type of field.</param>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="displayName">The UI mark up display name for the field.</param>
        /// <param name="units">String to receive the units specifier is one is present</param>
        /// <param name="tooltip">String to receive the tooltip text if it is present</param>
        /// <param name="markupFlags">The UI markup flags for this field.</param>
        /// <returns>The new code safe field name for the field</returns>
        public string CreateCodeSafeFieldName(field_type fieldType, string fieldName, out string displayName, out string units, out string tooltip, out EditorMarkUpFlags markupFlags)
        {
            string newFieldName = "";

            // Satisfy the compiler.
            displayName = string.Empty;
            units = string.Empty;
            tooltip = string.Empty;
            markupFlags = EditorMarkUpFlags.None;

            // Check if the field is a padding field.
            if (fieldType == field_type._field_pad || fieldType == field_type._field_skip || fieldType == field_type._field_useless_pad)
            {
                // Create a new padding field name.
                return AddPaddingField();
            }
            else if (fieldType == field_type._field_explanation)
            {
                // Create a new explanation field name.
                return AddExplanationField();
            }

            // Check if the name is invalid.
            if (MutationCodeFormatter.IsValidFieldName(fieldName) == false)
            {
                // Create a new no-name field name.
                return AddNoNameField();
            }

            // Convert the field name to a code safe representation.
            MutationCodeFormatter.ProcessFieldName(fieldName, out newFieldName, out displayName, out units, out tooltip, out markupFlags);
            if (newFieldName == "")
            {
                // Create a new no-name field name.
                return AddNoNameField();
            }

            // Make sure the new field name is unique in the code scope.
            if (this.Fields.Contains(newFieldName) == true)
            {
                string tempFieldName = "";

                // Loop and append an integer until the field name becomes unique.
                int uniqueInt = 1;
                do
                {
                    // Append an integer to the field name to try and make it unique.
                    tempFieldName = String.Format("{0}{1}", newFieldName, uniqueInt++);
                }
                while (this.Fields.Contains(tempFieldName) == true);

                // Save the new field name.
                newFieldName = tempFieldName;
            }

            // Add the new field name to the fields list.
            this.Fields.Add(newFieldName);

            // Return the new field name.
            return newFieldName;
        }

        /// <summary>
        /// Creates a new code safe flag name that is unique in the current code scope and adds it to the fields list.
        /// </summary>
        /// <param name="fieldName">Name of the flag.</param>
        /// <returns>The new code safe representation of the flag name.</returns>
        public string CreateCodeSafeFlagName(string fieldName)
        {
            // Convert the flag name to a code safe representation.
            string newFlagName = MutationCodeFormatter.CreateCodeSafeFieldName(fieldName);

            // Make sure the new flag name is unique in the code scope.
            if (this.Fields.Contains(newFlagName) == true)
            {
                string tempFlagName = "";

                // Loop and append an integer until the flag name becomes unique.
                int uniqueInt = 1;
                do
                {
                    // Append an integer to the flag name to try and make it unique.
                    tempFlagName = String.Format("{0}{1}", tempFlagName, uniqueInt++);
                }
                while (this.Fields.Contains(tempFlagName) == true);

                // Save the new field name.
                newFlagName = tempFlagName;
            }

            // Add the new flag name to the fields list.
            this.Fields.Add(newFlagName);

            // Return the new flag name.
            return newFlagName;
        }

        /// <summary>
        /// Creates a new code scope for a type is one does not already exist, else it turns the existing scope for the type.
        /// </summary>
        /// <param name="typeName">Name of the type</param>
        /// <param name="definitionAddress">Guerilla definition address for the type</param>
        /// <param name="scopeType">Type of code scope to be created</param>
        /// <returns>The code scope for the type.</returns>
        public MutationCodeScope CreateCodeScopeForType(string typeName, int definitionAddress, MutationCodeScopeType scopeType)
        {
            // Check if there is an entry in the Types list with the same definition address.
            MutationCodeScope codeScope = FindExistingCodeScope(definitionAddress);
            if (codeScope != null)
            {
                // There is an existing code scope for this type so just return that.
                return codeScope;
            }

            // Create a code safe type name for the new type.
            string newTypeName, displayName, units, tooltip;
            EditorMarkUpFlags markupFlags;
            MutationCodeFormatter.ProcessFieldName(typeName, out newTypeName, out displayName, out units, out tooltip, out markupFlags);
            if (newTypeName == "" || MutationCodeFormatter.IsValidFieldName(newTypeName) == false)
            {
                // For now we will create a no name type for it, and I will create a preprocessing function later on.
                newTypeName = this.CreateNoNameType();
            }

            // Check if the type is an enum or flags and append the corresponding character.
            if (scopeType == MutationCodeScopeType.Bitmask)
            {
                // Append 'b' for bitmask.
                newTypeName = newTypeName.Insert(0, char.IsUpper(newTypeName[0]) == true ? "b" : "b_");
            }
            else if (scopeType == MutationCodeScopeType.Enum)
            {
                // Append 'e' for enum.
                newTypeName = newTypeName.Insert(0, char.IsUpper(newTypeName[0]) == true ? "e" : "e_");
            }

            // Check if the type name is unique or if it already exists.
            if (this.Types.Keys.Contains(newTypeName) == true)
            {
                string tempTypeName = "";

                // This shouldn't really happen, but if it does loop until we have a valid type name.
                int uniqueInt = 1;
                do
                {
                    // Append an integer to the type name to try and make it unique.
                    tempTypeName = string.Format("{0}{1}", newTypeName, uniqueInt++);
                }
                while (this.Types.Keys.Contains(tempTypeName) == true);

                // Save the temp type name.
                newTypeName = tempTypeName;
            }

            // Create a new code scope for this type.
            codeScope = new MutationCodeScope(newTypeName, this.Namespace, definitionAddress, scopeType);

            // Add the new type to the types dictionary.
            this.Types.Add(newTypeName, codeScope);

            // Return the new code scope for the type.
            return codeScope;
        }

        /// <summary>
        /// Checks if there is an existing code scope for the definition address provided.
        /// </summary>
        /// <param name="definitionAddress">Address of the Guerilla definition to look for.</param>
        /// <returns>The existing code scope if one exists, null otherwise.</returns>
        public MutationCodeScope FindExistingCodeScope(int definitionAddress)
        {
            // Check if there is an entry in the Types list with the same definition address.
            if (definitionAddress != -1 && this.Types.Values.Count(type => type.DefinitionAddress == definitionAddress) > 0)
            {
                // There is already a registered type for this definition, just return the code scope instance for it.
                return this.Types.Values.First(type => type.DefinitionAddress == definitionAddress);
            }

            // No existing definition was found.
            return null;
        }

        /// <summary>
        /// Creates a code safe field name for a padding field that is unique in the code scope and adds it to the fields list.
        /// </summary>
        /// <returns>Name of the new padding field.</returns>
        private string AddPaddingField()
        {
            string newFieldName = "";

            // Loop until we have a valid field name.
            do
            {
                // Create a new field name for the padding field.
                newFieldName = string.Format("padding{0}", this.paddingFieldCount++);
            }
            while (this.Fields.Contains(newFieldName) == true);

            // Add the field name to the fields list.
            this.Fields.Add(newFieldName);

            // Return the new field name.
            return newFieldName;
        }

        /// <summary>
        /// Creates a code safe field name for a no-name field that is unique in the code scope and adds it to the fields list.
        /// </summary>
        /// <returns>Name of the new no-name field.</returns>
        private string AddNoNameField()
        {
            string newFieldName = "";

            // This is a no name field, loop until we have a valid field name for it.
            do
            {
                // Create a new field name for the no name field.
                newFieldName = string.Format("noNameField{0}", this.noNameFieldCount++);
            }
            while (this.Fields.Contains(newFieldName) == true);

            // Add the field name to the fields list.
            this.Fields.Add(newFieldName);

            // Return the new field name.
            return newFieldName;
        }

        /// <summary>
        /// Creates a code safe field name for an explanation field that is unique in the current scope and adds it to the fields list.
        /// </summary>
        /// <returns>Name of the new explanation field.</returns>
        private string AddExplanationField()
        {
            string newFieldName = "";

            // This is an explanation field, just loop until we have a valid field name for it.
            do
            {
                // Create a new field name for the explanation field.
                newFieldName = string.Format("explanationField{0}", this.explanationFieldCount++);
            }
            while (this.Fields.Contains(newFieldName) == true);

            // Add the field name to the fields list.
            this.Fields.Add(newFieldName);

            // Return the new field name.
            return newFieldName;
        }

        /// <summary>
        /// Creates a new type name for a no-name type that is unique in the code scope.
        /// </summary>
        /// <returns>Name of the new no-name type.</returns>
        private string CreateNoNameType()
        {
            string newTypeName = "";

            // This is a no name type, loop until we have a valid type name for it.
            do
            {
                // Create a new type name for the no name type.
                newTypeName = string.Format("NoNameType{0}", this.noNameTypeCount++);
            }
            while (this.Types.Keys.Contains(newTypeName) == true);

            // Return the new type name.
            return newTypeName;
        }
    }
}
