using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    /// <summary>
    /// Declares the field a tag struct.
    /// </summary>
    public class TagStructAttribute : Attribute
    {
        /// <summary>
        /// Creates a TagStructAttribute CodeDOM declaration.
        /// </summary>
        /// <returns>A CodeDOM attribute declaration.</returns>
        public static CodeAttributeDeclaration CreateAttributeDeclaration()
        {
            // Create a TagStructAttribute attribute for this field.
            CodeAttributeDeclaration attribute = new CodeAttributeDeclaration(typeof(TagStructAttribute).Name);

            // Return the new attribute declaration.
            return attribute;
        }
    }
}
