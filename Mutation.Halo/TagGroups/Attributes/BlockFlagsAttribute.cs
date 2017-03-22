using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class BlockFlagsAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the tag block definition this flags apply to.
        /// </summary>
        public string TagBlockName { get; private set; }

        /// <summary>
        /// Specifies the tag block definition this block flags field applies to.
        /// </summary>
        /// <param name="tagBlockName">Name of the tag block the field applies to.</param>
        public BlockFlagsAttribute(string tagBlockName)
        {
            // Initialize fields.
            this.TagBlockName = tagBlockName;
        }
    }
}
