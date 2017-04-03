using Mutation.HEK.Common.TagFieldDefinitions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TagReferenceAttribute : Attribute
    {
        /// <summary>
        /// Gets the group tag filter for the field.
        /// </summary>
        public string GroupTag { get; private set; }

        /// <summary>
        /// Specifies the group tag filter for a TagReference field.
        /// </summary>
        /// <param name="groupTag">Group tag filter.</param>
        public TagReferenceAttribute(string groupTag)
        {
            // Initialize fields.
            this.GroupTag = groupTag;
        }

        /// <summary>
        /// Creates a TagReferenceAttribute CodeDOM declaration.
        /// </summary>
        /// <param name="definition">Guerilla tag reference definition.</param>
        /// <returns>A CodeDOM attribute declaration.</returns>
        public static CodeAttributeDeclaration CreateAttributeDeclaration(tag_reference_definition definition)
        {
            // Create a TagReferenceAttribute attribute for this field with the group tag filter.
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(typeof(TagReferenceAttribute).Name,
                new CodeAttributeArgument(new CodePrimitiveExpression(Mutation.HEK.Guerilla.Guerilla.GroupTagToString(definition.group_tag))));

            // Return the attribute declaration.
            return attribute;
        }
    }
}
