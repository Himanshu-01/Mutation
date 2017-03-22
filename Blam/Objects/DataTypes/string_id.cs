using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blam.Objects.DataTypes
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = kSizeOf)]
    public struct string_id
    {
        /// <summary>
        /// Size of a string_id object.
        /// </summary>
        public const int kSizeOf = 4;

        public const int k_string_id_value_length = 127;

        public const uint _string_id_invalid = 0xFFFFFFFF;
        public const uint _string_id_empty = 0;

        /// <summary>
        /// Length of the string_id constant.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public byte Length;

        /// <summary>
        /// Unique 32-bit string_id handle.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public uint Handle;

        #region Constructor

        public string_id(byte Length, uint Index)
        {
            // Satisfy the compiler.
            this.Length = 0;
            this.Handle = _string_id_empty;

            // Create the string_id handle.
            this.Handle = CreateHandle(Length, Index);
        }

        public string_id(uint Handle)
        {
            // Satisfy the compiler.
            this.Length = 0;

            // Save the string handle.
            this.Handle = Handle;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the absolute index from the string_id handle.
        /// </summary>
        /// <returns>Absolute index of the string_id object.</returns>
        public int ToIndex()
        {
            return (int)(Handle & 0x00FFFFFF);
        }

        /// <summary>
        /// Creates a 32-bit string_id handle.
        /// </summary>
        /// <param name="Length">Length of the string_id constant.</param>
        /// <param name="Index">Absolute index of the string_id constant.</param>
        /// <returns>A 32-bit string_id handle.</returns>
        public static uint CreateHandle(byte Length, uint Index)
        {
            return (uint)(Length << 24 | (Index & 0x00FFFFFF));
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two string_id objects for equality.
        /// </summary>
        /// <param name="lhs">Left hand string_id for comparison.</param>
        /// <param name="rhs">Right hand string_id for comparison.</param>
        /// <returns>True if both string_id handles are equal, false otherwise.</returns>
        public static bool operator ==(string_id lhs, string_id rhs)
        {
            return lhs.Handle == rhs.Handle;
        }

        /// <summary>
        /// Compares two string_id objects for inequality.
        /// </summary>
        /// <param name="lhs">Left hand string_id for comparison.</param>
        /// <param name="rhs">Right hand string_id for comparison.</param>
        /// <returns>True if the string_id handles are not equal, false otherwise.</returns>
        public static bool operator !=(string_id lhs, string_id rhs)
        {
            return lhs.Handle != rhs.Handle;
        }

        /// <summary>
        /// Implicit cast to a signed integer.
        /// </summary>
        /// <param name="id">string_id to cast.</param>
        /// <returns>Handle value of the string_id object as a signed integer.</returns>
        public static implicit operator int(string_id id)
        {
            return (int)id.Handle;
        }

        /// <summary>
        /// Implicit cast to an unsigned integer.
        /// </summary>
        /// <param name="id">string_id to cast.</param>
        /// <returns>Handle value of the string_id object as an unsigned integer.</returns>
        public static implicit operator uint(string_id id)
        {
            return id.Handle;
        }

        /// <summary>
        /// Implicit conversion from an unsigned integer to a string_id.
        /// </summary>
        /// <param name="handle">String handle to convert.</param>
        /// <returns>A string_id object with the same handle value.</returns>
        public static implicit operator string_id(uint handle)
        {
            return new string_id(handle);
        }

        #endregion

        #region Object Members

        /// <summary>
        /// Compares this string_id to another object for equality.
        /// </summary>
        /// <param name="obj">The other object to compare to.</param>
        /// <returns>A boolean indicating both objects are equal.</returns>
        public override bool Equals(object obj)
        {
            // Check if the other object is a string_id object.
            if (obj == null || obj.GetType() != typeof(string_id))
                return false;

            // Cast to a string_id object and compare.
            string_id other = (string_id)obj;
            return other.Handle == this.Handle;
        }

        /// <summary>
        /// Gets a hashcode for this string_id object.
        /// </summary>
        /// <returns>Hashcode of this string_id.</returns>
        public override int GetHashCode()
        {
            return (int)this.Handle;
        }

        /// <summary>
        /// Gets a string representation of this string_id object.
        /// </summary>
        /// <returns>String representation of this string_id object.</returns>
        public override string ToString()
        {
            return string.Format("string_id { handle: {0} length: {1} index: {2} }",
                this.Handle.ToString("X"), this.Length, this.ToIndex());
        }

        #endregion
    }
}
