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
    [GuerillaType(field_type._field_string_id)]
    [GuerillaType(field_type._field_old_string_id)]
    [StructLayout(LayoutKind.Explicit, Size = kSizeOf)]
    public struct string_id
    {
        /// <summary>
        /// Size of the string_id struct.
        /// </summary>
        public const int kSizeOf = 4;

        public const int _string_id_invalid = -1;
        public const int _string_id_empty_string = 0;

        [FieldOffset(0)]
        public int handle;

        /// <summary>
        /// Gets the length of the string constant.
        /// </summary>
        public byte Length { get { return (byte)((this.handle & 0xFF000000) >> 24); } }

        /// <summary>
        /// Gets the 24-bit string identifier index.
        /// </summary>
        public int Index { get { return this.handle & 0xFFFFFF; } }

        public string_id(int identifier)
        {
            // Initialize fields.
            this.handle = identifier;
        }

        public string_id(byte length, int index)
        {
            // Rebuild the handle from the parameters provided.
            this.handle = (int)(index | (((int)length << 24) & 0xFF000000));
        }

        public static implicit operator int(string_id value)
        {
            // Implicit cast from a string_id to a 32-bit handle value.
            return value.handle;
        }

        public static implicit operator string_id(int handle)
        {
            // Implicit cast from a 32-bit handle value to a string_id.
            return new string_id(handle);
        }

        public static bool operator ==(string_id a, string_id b)
        {
            // Comparison for equality.
            return a.handle == b.handle;
        }

        public static bool operator !=(string_id a, string_id b)
        {
            // Comparison for inequality.
            return a.handle != b.handle;
        }

        public override bool Equals(object obj)
        {
            // Check if the other object is null and of type string_id.
            if (obj == null || obj.GetType() != typeof(string_id))
                return false;

            // Compar the handles of both objects.
            string_id other = (string_id)obj;
            return this.handle == other.handle;
        }

        public override int GetHashCode()
        {
            // Return the handle as the hashcode.
            return this.handle;
        }

        public override string ToString()
        {
            return string.Format("Value=0x{0}", this.handle.ToString("X"));
        }
    }
}
