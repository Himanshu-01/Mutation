using Mutation.Halo.TagGroups.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.FieldTypes
{
#warning TagData not fully implemented
    [GuerillaType(HEK.Common.field_type._field_data)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct TagData
    {
        /// <summary>
        /// Size of a TagData struct.
        /// </summary>
        public const int kSizeOf = 8;

        public int Size;
        public int Address;

        public byte[] Data;
    }
}
