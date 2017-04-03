using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    /// <summary>
    /// Flag values used to indicate how a field should be displayed in the editor.
    /// </summary>
    [Flags]
    public enum EditorMarkUpFlags : int
    {
        /// <summary>
        /// No flags set.
        /// </summary>
        None = 0,
        /// <summary>
        /// Forces the field to be read-only.
        /// </summary>
        ReadOnly = 0x1,
        /// <summary>
        /// Hides the field from the editor.
        /// </summary>
        Hidden = 0x2,
    }

    /// <summary>
    /// Customization for displaying tag fields in an editor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EditorMarkUpAttribute : Attribute
    {
        /// <summary>
        /// Flags that control how the field is displayed in the editor.
        /// </summary>
        public EditorMarkUpFlags Flags { get; private set; }

        /// <summary>
        /// UI display name for the field.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Units specifier for the field.
        /// </summary>
        public string UnitsSpecifier { get; private set; }

        /// <summary>
        /// Tooltip text for the field.
        /// </summary>
        public string ToolTipText { get; private set; }

        /// <summary>
        /// Initializes a new EditorMarkUpAttribute instance using the values provided.
        /// </summary>
        /// <param name="flags">Display flags for the field.</param>
        /// <param name="displayName">UI display name for the field.</param>
        /// <param name="unitsSpecifier">Units specifier for the field.</param>
        /// <param name="tooltipText">Tooltip text for the field.</param>
        public EditorMarkUpAttribute(EditorMarkUpFlags flags = EditorMarkUpFlags.None, string displayName = "", string unitsSpecifier = "", string tooltipText = "")
        {
            // Initialize fields.
            this.Flags = flags;
            this.DisplayName = displayName;
            this.UnitsSpecifier = unitsSpecifier;
            this.ToolTipText = tooltipText;
        }

        public static CodeAttributeDeclaration CreateAttributeDeclaration(EditorMarkUpFlags flags = EditorMarkUpFlags.None, string displayName = "", string unitsSpecifier = "", string tooltipText = "")
        {
            // Create the attribute declaration and initialize it with no parameters.
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(typeof(EditorMarkUpAttribute).Name);

            // Check if the display flags are set.
            if (flags != EditorMarkUpFlags.None)
            {
                // Add an argument for the display flags.
                attribute.Arguments.Add(new CodeAttributeArgument(new CodeSnippetExpression(string.Format("flags: {0}", MarkUpFlagsToString(flags)))));
            }

            // Check if the display name is present.
            if (displayName != string.Empty)
            {
                // Add an argument for the display name.
                attribute.Arguments.Add(new CodeAttributeArgument(new CodeSnippetExpression(string.Format("displayName: \"{0}\"", displayName))));
            }

            // Check if the units specifier is present.
            if (unitsSpecifier != string.Empty)
            {
                // Add an argument for the units specifier.
                attribute.Arguments.Add(new CodeAttributeArgument(new CodeSnippetExpression(string.Format("unitsSpecifier: \"{0}\"", unitsSpecifier))));
            }

            // Check if the tooltip text is present.
            if (tooltipText != string.Empty)
            {
                // Add an argument for the tooltip text.
                attribute.Arguments.Add(new CodeAttributeArgument(new CodeSnippetExpression(string.Format("tooltipText: \"{0}\"", tooltipText))));
            }

            // Return the new attribute declaration.
            return attribute;
        }

        /// <summary>
        /// Converts a EditorMarkUpFlags value into a string representation of the values that are set.
        /// </summary>
        /// <param name="flags">Flags to convert into a string.</param>
        /// <returns>String representation of the flag values set.</returns>
        private static string MarkUpFlagsToString(EditorMarkUpFlags flags)
        {
            string flagsStr = string.Empty;

            // Get a list of the enum values and names.
            string[] enumNames = Enum.GetNames(typeof(EditorMarkUpFlags));
            Array enumValues = Enum.GetValues(typeof(EditorMarkUpFlags));

            // Loop through all of the options in the markup flags enum and check for each one.
            for (int i = 1; i < enumNames.Length; i++)
            {
                // Check if the flags specified contain the current flag value.
                if ((flags & (EditorMarkUpFlags)enumValues.GetValue(i)) != EditorMarkUpFlags.None)
                {
                    // Add the flag value to the string.
                    if (flagsStr == string.Empty)
                        flagsStr += string.Format("EditorMarkUpFlags.{0}", enumNames[i]);
                    else
                        flagsStr += string.Format(" | EditorMarkUpFlags.{0}", enumNames[i]);
                }
            }

            // Return the string representation of the flag values.
            return flagsStr;
        }
    }
}
