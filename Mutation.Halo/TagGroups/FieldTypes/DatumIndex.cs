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
    [GuerillaType(field_type._field_datum_index)]
    [StructLayout(LayoutKind.Explicit, Size = kSizeOf)]
    public struct DatumIndex
    {
        public const uint NONE = 0xFFFFFFFF;

        /// <summary>
        /// Size of the DatumIndex struct.
        /// </summary>
        public const int kSizeOf = 4;

        /// <summary>
        /// The 32bit DatumIndex handle value.
        /// </summary>
        [FieldOffset(0)]
        public uint handle;

        /// <summary>
        /// Absolute index of the DatumIndex handle.
        /// </summary>
        [FieldOffset(0)]
        public ushort index;

        /// <summary>
        /// Salt value of the DatumIndex handle.
        /// </summary>
        [FieldOffset(2)]
        public short salt;

        /// <summary>
        /// Creates a new DatumIndex object using the specfied index and salt.
        /// </summary>
        /// <param name="index">Absolute index of the DatumIndex.</param>
        /// <param name="salt">Identifier of the DatumIndex.</param>
        public DatumIndex(ushort index, short salt)
        {
            // Satisfy the compiler.
            this.handle = 0;

            // Assign the index and salt.
            this.index = index;
            this.salt = salt;
        }

        /// <summary>
        /// Creates a new DatumIndex object using the specified handle value.
        /// </summary>
        /// <param name="handle">Handle value of the DatumIndex.</param>
        public DatumIndex(uint handle)
        {
            // Satisfy the compiler.
            this.index = 0;
            this.salt = 0;

            // Assign the handle.
            this.handle = handle;
        }

        /// <summary>
        /// Gets the index field of the DatumIndex handle.
        /// </summary>
        /// <returns>Absolute index of the DatumIndex handle.</returns>
        public ushort ToIndex()
        {
            return index;
        }

        /// <summary>
        /// Gets the identifier field of the DatumIndex handle.
        /// </summary>
        /// <returns>Unique identifier of the DatumIndex handle.</returns>
        public short ToIdentifier()
        {
            return salt;
        }

        /// <summary>
        /// Compares two DatumIndex objects for equality.
        /// </summary>
        /// <param name="lhs">Left hand DatumIndex for comparison.</param>
        /// <param name="rhs">Right hand DatumIndex for comparison.</param>
        /// <returns>True if the DatumIndexes are equal, false otherwise.</returns>
        public static bool operator ==(DatumIndex lhs, DatumIndex rhs)
        {
            return lhs.handle == rhs.handle;
        }

        /// <summary>
        /// Compares two DatumIndex objects for inequality.
        /// </summary>
        /// <param name="lhs">Left hand DatumIndex for comparison.</param>
        /// <param name="rhs">Right hand DatumIndex for comparison.</param>
        /// <returns>True if the DatumIndexes are not equal, false otherwise.</returns>
        public static bool operator !=(DatumIndex lhs, DatumIndex rhs)
        {
            return lhs.handle != rhs.handle;
        }

        /// <summary>
        /// Implicit cast to a signed integer.
        /// </summary>
        /// <param name="datum">DatumIndex to cast.</param>
        /// <returns>Value of the DatumIndex as a signed integer.</returns>
        public static implicit operator int(DatumIndex datum)
        {
            return (int)((datum.salt << 24) & 0xff000000) | ((datum.salt << 8) & 0x00ff0000) |
                        ((datum.index >> 8) & 0x0000ff00) | ((datum.index >> 24) & 0x000000ff);
        }

        /// <summary>
        /// Explicit cast to an unsigned integer.
        /// </summary>
        /// <param name="datum">DatumIndex to cast.</param>
        /// <returns>Value of the DatumIndex as an unsigned integer.</returns>
        public static explicit operator uint(DatumIndex datum)
        {
            return datum.handle;
        }

        /// <summary>
        /// Implicit cast from a unsigned integer to a DatumIndex object.
        /// </summary>
        /// <param name="handle">Handle value of the DatumIndex.</param>
        /// <returns>A DatumIndex object initialized with the handle value provided.</returns>
        public static implicit operator DatumIndex(uint handle)
        {
            return new DatumIndex(handle);
        }

        /// <summary>
        /// Compares this DatumIndex to another object for equality.
        /// </summary>
        /// <param name="obj">Other object to compare to.</param>
        /// <returns>A boolean indicating that both objects are equal.</returns>
        public override bool Equals(object obj)
        {
            // Check that the other object is of the same type and is not null.
            if (obj == null || obj.GetType() != typeof(DatumIndex))
                return false;

            // Cast it to a DatumIndex and compare.
            DatumIndex other = (DatumIndex)obj;
            return this.handle == other.handle;
        }

        /// <summary>
        /// Gets a hashcode for this DatumIndex object.
        /// </summary>
        /// <returns>Hashcode of this DatumIndex.</returns>
        public override int GetHashCode()
        {
            return this;
        }

        public override string ToString()
        {
            return string.Format("Datum=0x{0}", this.handle.ToString("X"));
        }
    }
}
