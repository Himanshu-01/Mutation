using System;
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
    }
}
