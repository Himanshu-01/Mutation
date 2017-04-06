using Mutation.HEK.Common.TagFieldDefinitions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    public class TagDataAttribute : Attribute
    {
        /// <summary>
        /// Maximum size of the tag data field.
        /// </summary>
        public int MaxSize { get; private set; }

        /// <summary>
        /// Gets the alignment constraint of the tag data.
        /// </summary>
        public int Alignment { get; private set; }

        /// <summary>
        /// String representation of the maximum size constant.
        /// </summary>
        public string MaxSizeString { get; private set; }

        /// <summary>
        /// Specifies the maximum size of a tag data field.
        /// </summary>
        /// <param name="maxSize">Maximum size of the tag data field.</param>
        /// <param name="alignment">Alignment constraint for the tag data.</param>
        /// <param name="maxSizeString">String representation of the maximum size constant.</param>
        public TagDataAttribute(int maxSize, int alignment, string maxSizeString = "")
        {
            // Initialize fields.
            this.MaxSize = maxSize;
            this.Alignment = alignment;
            this.MaxSizeString = maxSizeString;
        }

        /// <summary>
        /// Creates a TagDataAttribute CodeDOM declaration.
        /// </summary>
        /// <param name="definition">Guerilla tag data definition.</param>
        /// <returns>A CodeDOM attribute declaration.</returns>
        public static CodeAttributeDeclaration CreateAttributeDeclaration(tag_data_definition definition)
        {
            // Create a TagDataAttribute attribute for this field using the max size.
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(typeof(TagDataAttribute).Name,
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("maxSize: {0}", definition.maximum_size))),
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("alignment: {0}", definition.alignment_bit != 0 ? (1 << definition.alignment_bit) : 4)))
                );

            // Check if we should add the max size string argument.
            if (definition.MaximumSize != string.Empty)
            {
                // Add the max size string to the attribute arguments.
                attribute.Arguments.Add(new CodeAttributeArgument(new CodeSnippetExpression(string.Format("maxSizeString: \"{0}\"", definition.MaximumSize))));
            }

            // Return the new attribute declaration.
            return attribute;
        }
    }
}
