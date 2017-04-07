using Mutation.HEK.Common;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    public class TagGroupDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Gets the size of the tag group definition.
        /// </summary>
        public int SizeOf { get; private set; }

        /// <summary>
        /// Gets the version of the tag group definition.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Gets the tag group's group tag.
        /// </summary>
        public string GroupTag { get; private set; }

        /// <summary>
        /// Initializes a new TagGroupDefinitionAttribute using the values provided.
        /// </summary>
        /// <param name="sizeOf">Size of the tag group definition.</param>
        /// <param name="version">Definition version.</param>
        /// <param name="groupTag">Group tag for the tag group.</param>
        public TagGroupDefinitionAttribute(int sizeOf, int version, string groupTag)
        {
            // Initialize fields.
            this.SizeOf = sizeOf;
            this.Version = version;
            this.GroupTag = groupTag;
        }

        /// <summary>
        /// Creates a TagGroupDefinitionAttribute CodeDOM declaration.
        /// </summary>
        /// <param name="tagGroup">Guerill tag group definition.</param>
        /// <returns>A CodeDOM attribute declaration.</returns>
        public static CodeAttributeDeclaration CreateAttributeDeclaration(tag_group tagGroup)
        {
            // Get the latest field set from the guerilla definition.
            tag_field_set fieldSet = tagGroup.Definition.TagFieldSets[tagGroup.Definition.GetFieldSetIndexClosestToH2Xbox()];

            // Setup a TagGroupDefinitionAttribute attribute using the definition info.
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(typeof(TagGroupDefinitionAttribute).Name, new CodeAttributeArgument[]
            {
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("sizeOf: {0}", fieldSet.size))),
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("version: {0}", tagGroup.version))),
                new CodeAttributeArgument(new CodeSnippetExpression(string.Format("groupTag: \"{0}\"", tagGroup.GroupTag)))
            });

            // Return the new attribute declaration.
            return attribute;
        }
    }
}
