using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blam.Objects.DataTypes
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = kSizeOf)]
    public struct char_enum
    {
        /// <summary>
        /// Size of a char_enum object.
        /// </summary>
        public const int kSizeOf = 1;

        /// <summary>
        /// Selected enum index.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public byte value;

        #region Constructor

        /// <summary>
        /// Initializes the char_enum struct with the value provided.
        /// </summary>
        /// <param name="value">Initial enum index.</param>
        public char_enum(byte value)
        {
            // Initialize fields.
            this.value = value;
        }

        public char_enum(int value)
        {
            // Initialize fields.
            this.value = (byte)value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two char_enum structs for equality.
        /// </summary>
        /// <param name="lhs">Left hand char_enum for comparison.</param>
        /// <param name="rhs">Right hand char_enum for comparison.</param>
        /// <returns>True if both char_enum structs are equal, false otherwise.</returns>
        public static bool operator ==(char_enum lhs, char_enum rhs)
        {
            return lhs.value == rhs.value;
        }

        /// <summary>
        /// Compares two char_enum structs for inequality.
        /// </summary>
        /// <param name="lhs">Left hand char_enum for comparison.</param>
        /// <param name="rhs">Right hand char_enum for comparison.</param>
        /// <returns>True if both char_enum structs are not equal, false otherwise.</returns>
        public static bool operator !=(char_enum lhs, char_enum rhs)
        {
            return lhs.value != rhs.value;
        }

        /// <summary>
        /// Implicit conversion from a char_enum to an 8-bit signed integer.
        /// </summary>
        /// <param name="enum_">char_enum struct to convert.</param>
        /// <returns>8-bit signed representation of <paramref name="enum_"/></returns>
        public static implicit operator byte(char_enum enum_)
        {
            return enum_.value;
        }

        /// <summary>
        /// Implicit conversion from an 8-bit signed integer to a char_enum object.
        /// </summary>
        /// <param name="value">Enum value used to initialize the char_enum object.</param>
        /// <returns>char_enum struct with the same value as <paramref name="value"/></returns>
        public static implicit operator char_enum(byte value)
        {
            return new char_enum(value);
        }

        /// <summary>
        /// Implicit conversion from a 32-bit signed integer to a char_enum object.
        /// </summary>
        /// <param name="value">Enum value used to initialize the char_enum object.</param>
        /// <returns>char_enum struct with the same value as <paramref name="value"/></returns>
        public static implicit operator char_enum(int value)
        {
            return new char_enum(value);
        }

        #endregion

        #region Object Members

        /// <summary>
        /// Compares this char_enum object to another object for equality.
        /// </summary>
        /// <param name="obj">Other object to compare to.</param>
        /// <returns>True if both objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // Check if the other object is a char_enum object.
            if (obj == null || obj.GetType() != typeof(char_enum))
                return false;

            // Cast the other object to a char_enum and compare.
            char_enum other = (char_enum)obj;
            return this == other;
        }

        /// <summary>
        /// Gets a hashcode for this char_enum object.
        /// </summary>
        /// <returns>Hashcode of this char_enum struct.</returns>
        public override int GetHashCode()
        {
            return this.value;
        }

        /// <summary>
        /// Gets a string representation of the char_enum object.
        /// </summary>
        /// <returns>String representation of the char_enum object.</returns>
        public override string ToString()
        {
            return string.Format("char_enum: {0}", this.value);
        }

        #endregion
    }
}
