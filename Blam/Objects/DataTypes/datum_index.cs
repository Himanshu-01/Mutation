using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blam.Objects.DataTypes
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = kSizeOf)]
    public struct datum_index : IEqualityComparer<datum_index>
    {
        public const uint NONE = 0xFFFFFFFF;

        #region Fields

        /// <summary>
        /// Size of a datum_index object.
        /// </summary>
        public const int kSizeOf = 4;

        /// <summary>
        /// The 32bit datum_index handle value.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public uint Handle;

        /// <summary>
        /// Absolute index of the datum_index handle.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public ushort Index;

        /// <summary>
        /// Salt value of the datum_index handle.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(2)]
        public short Salt;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new datum_index object using the specfied index and salt.
        /// </summary>
        /// <param name="index">Absolute index of the datum_index.</param>
        /// <param name="salt">Identifier of the datum_index.</param>
        public datum_index(ushort index, short salt)
        {
            // Satisfy the compiler.
            this.Handle = 0;

            // Assign the index and salt.
            this.Index = index;
            this.Salt = salt;
        }

        /// <summary>
        /// Creates a new datum_index object using the specified handle value.
        /// </summary>
        /// <param name="handle">Handle value of the datum_index.</param>
        public datum_index(uint handle)
        {
            // Satisfy the compiler.
            this.Index = 0;
            this.Salt = 0;

            // Assign the handle.
            this.Handle = handle;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the index field of the datum_index handle.
        /// </summary>
        /// <returns>Absolute index of the datum_index handle.</returns>
        public ushort ToIndex()
        {
            return Index;
        }

        /// <summary>
        /// Gets the identifier field of the datum_index handle.
        /// </summary>
        /// <returns>Unique identifier of the datum_index handle.</returns>
        public short ToIdentifier()
        {
            return Salt;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two datum_index objects for equality.
        /// </summary>
        /// <param name="lhs">Left hand datum_index for comparison.</param>
        /// <param name="rhs">Right hand datum_index for comparison.</param>
        /// <returns>True if the datum_indexes are equal, false otherwise.</returns>
        public static bool operator ==(datum_index lhs, datum_index rhs)
        {
            return lhs.Handle == rhs.Handle;
        }

        /// <summary>
        /// Compares two datum_index objects for inequality.
        /// </summary>
        /// <param name="lhs">Left hand datum_index for comparison.</param>
        /// <param name="rhs">Right hand datum_index for comparison.</param>
        /// <returns>True if hte datum_indexes are not equal, false otherwise.</returns>
        public static bool operator !=(datum_index lhs, datum_index rhs)
        {
            return lhs.Handle != rhs.Handle;
        }

        /// <summary>
        /// Implicit cast to a signed integer.
        /// </summary>
        /// <param name="datum">datum_index to cast.</param>
        /// <returns>Value of the datum_index as a signed integer.</returns>
        public static implicit operator int(datum_index datum)
        {
            return (int)((datum.Salt << 24) & 0xff000000) | ((datum.Salt << 8) & 0x00ff0000) |
                        ((datum.Index >> 8) & 0x0000ff00) | ((datum.Index >> 24) & 0x000000ff);
        }

        /// <summary>
        /// Explicit cast to an unsigned integer.
        /// </summary>
        /// <param name="datum">datum_index to cast.</param>
        /// <returns>Value of the datum_index as an unsigned integer.</returns>
        public static explicit operator uint(datum_index datum)
        {
            return datum.Handle;
        }

        #endregion

        #region Object Members

        /// <summary>
        /// Compares this datum_index to another object for equality.
        /// </summary>
        /// <param name="obj">Other object to compare to.</param>
        /// <returns>A boolean indicating that both objects are equal.</returns>
        public override bool Equals(object obj)
        {
            // Check that the other object is of the same type and is not null.
            if (obj == null || obj.GetType() != typeof(datum_index))
                return false;

            // Cast it to a datum_index and compare.
            datum_index other = (datum_index)obj;
            return this.Handle == other.Handle;
        }

        /// <summary>
        /// Gets a hashcode for this datum_index object.
        /// </summary>
        /// <returns>Hashcode of this datum_index.</returns>
        public override int GetHashCode()
        {
            return this;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region IEqualityComparer<datum_index> Members

        /// <summary>
        /// Compares two datum_indexes for equality.
        /// </summary>
        /// <param name="x">Source of the comparison.</param>
        /// <param name="y">Target of the comparison.</param>
        /// <returns></returns>
        public bool Equals(datum_index x, datum_index y)
        {
            return x.Handle == y.Handle;
        }

        /// <summary>
        /// Gets the hashcode of a datum_index object.
        /// </summary>
        /// <param name="obj">datum_index to get the hashcode for.</param>
        /// <returns>Hashcode of the datum_index.</returns>
        public int GetHashCode(datum_index obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
