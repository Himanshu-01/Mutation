using System;
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
    }
}
