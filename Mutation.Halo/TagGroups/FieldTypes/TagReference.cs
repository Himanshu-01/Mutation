using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.FieldTypes
{
#warning TagReference not fully implemented
    [GuerillaType(field_type._field_tag_reference)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct TagReference
    {
        /// <summary>
        /// Size of the TagReference struct.
        /// </summary>
        public const int kSizeOf = 8;

        public GroupTag groupTag;
        public DatumIndex datum;

        /// <summary>
        /// Initializes a new TagReference object with a null reference.
        /// </summary>
        //public TagReference()
        //{
        //    this.groupTag = GroupTag.Null;
        //    this.datum = DatumIndex.NONE;
        //}

        /// <summary>
        /// Initializes a new TagReference using the values provided.
        /// </summary>
        /// <param name="groupTag">Group tag of the tag being referenced.</param>
        /// <param name="datum">Datum index of the tag being referenced.</param>
        public TagReference(GroupTag groupTag, DatumIndex datum)
        {
            this.groupTag = groupTag;
            this.datum = datum;
        }
    }
}
