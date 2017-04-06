using Mutation.Halo.TagGroups.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.FieldTypes
{
#warning VertexBuffer is not fully implemented
    [GuerillaType(HEK.Common.field_type._field_vertex_buffer)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct VertexBuffer
    {
        /// <summary>
        /// Size of a VertexBuffer struct.
        /// </summary>
        public const int kSizeOf = 32;

        public byte TypeIndex;
        public byte StrideIndex;
    }
}
