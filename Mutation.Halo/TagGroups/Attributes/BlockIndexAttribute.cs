using Mutation.Halo.TagGroups.FieldTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class BlockIndexAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the tag block definition this field indexes.
        /// </summary>
        public string TagBlockName { get; set; }

        /// <summary>
        /// Delegate function for getting a tag block definition instance for a block index field.
        /// </summary>
        /// <param name="tagIndex">Global tag index.</param>
        /// <returns>True if a block definition instance is successfully retreived, false otherwise.</returns>
        public delegate bool GetBlockProc(DatumIndex tagIndex);
        /// <summary>
        /// Gets or sets the <see cref="GetBlockProc"/> function for the block index field.
        /// </summary>
        public GetBlockProc GetBlockFunction { get; set; }

        /// <summary>
        /// Delegate function for checking if the selected tag block definition instance is valid.
        /// </summary>
        /// <param name="tagIndex">Global tag index.</param>
        /// <returns>True if the selected block instance is a valid source block for this block index field.</returns>
        public delegate bool IsValidSourceBlockProc(DatumIndex tagIndex);
        /// <summary>
        /// Gets or sets the <see cref="IsValidSourceBlockProc"/> function for the block index field.
        /// </summary>
        public IsValidSourceBlockProc IsValidSourceBlockFunction { get; set; }
    }
}
