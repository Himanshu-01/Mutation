using Mutation.HEK.Common;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TagBlockDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Gets the size of the tag block definition.
        /// </summary>
        public int SizeOf { get; private set; }

        /// <summary>
        /// Gets the alignment interval for the tag block definition.
        /// </summary>
        public int Alignment { get; private set; }

        /// <summary>
        /// Gets the maximum number of blocks the tag block definition can have.
        /// </summary>
        public int MaxBlockCount { get; private set; }

        /// <summary>
        /// Initializes a new TagBlockDefinitionAttribute using the values provided.
        /// </summary>
        /// <param name="sizeOf">Size of the tag block definition.</param>
        /// <param name="alignment">Alignment interval.</param>
        /// <param name="maxBlockCount">Maximum number of blocks.</param>
        public TagBlockDefinitionAttribute(int sizeOf, int alignment, int maxBlockCount)
        {
            // Initialize fields.
            this.SizeOf = sizeOf;
            this.Alignment = alignment;
            this.MaxBlockCount = maxBlockCount;
        }

        /// <summary>
        /// Creates a TagBlockDefinitionAttribute CodeDOM declaration.
        /// </summary>
        /// <param name="definition">Guerilla tag block definition.</param>
        /// <returns>A CodeDOM attribute declaration.</returns>
        public static CodeAttributeDeclaration CreateAttributeDeclaration(TagBlockDefinition definition)
        {
            // Get the latest field set from the guerilla definition.
            tag_field_set fieldSet = definition.TagFieldSets[definition.TagFieldSetLatestIndex];

            // Setup a TagBlockDefinitionAttribute attribute using the definition info.
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(typeof(TagBlockDefinitionAttribute).Name, new CodeAttributeArgument[] 
            {
                // CodeDOM doesn't seem to support named parameters so we are going to do some h4x here...
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("sizeOf: {0}", fieldSet.size))),
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("alignment: {0}", fieldSet.alignment_bit != 0 ? (1 << fieldSet.alignment_bit) : 4))),
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("maxBlockCount: {0}", definition.s_tag_block_definition.maximum_element_count)))
            });

            // Return the new attribute declaration.
            return attribute;
        }
    }
}
