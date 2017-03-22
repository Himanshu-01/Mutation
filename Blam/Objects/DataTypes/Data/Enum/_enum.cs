using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blam.Objects.DataTypes
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = kSizeOf)]
    public struct _enum
    {
        /// <summary>
        /// Size of a _enum object.
        /// </summary>
        public const int kSizeOf = 2;

        /// <summary>
        /// Selected enum index.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public short value;

        #region Constructor

        /// <summary>
        /// Initializes the _enum struct with the value provided.
        /// </summary>
        /// <param name="value">Initial enum index.</param>
        public _enum(short value)
        {
            // Initialize fields.
            this.value = value;
        }

        /// <summary>
        /// Initializes the _enum struct with the value provided.
        /// </summary>
        /// <param name="value">Initial enum index.</param>
        public _enum(int value)
        {
            // Initialize fields.
            this.value = (short)value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two _enum structs for equality.
        /// </summary>
        /// <param name="lhs">Left hand _enum for comparison.</param>
        /// <param name="rhs">Right hand _enum for comparison.</param>
        /// <returns>True if both _enum structs are equal, false otherwise.</returns>
        public static bool operator ==(_enum lhs, _enum rhs)
        {
            return lhs.value == rhs.value;
        }

        /// <summary>
        /// Compares two _enum structs for inequality.
        /// </summary>
        /// <param name="lhs">Left hand _enum for comparison.</param>
        /// <param name="rhs">Right hand _enum for comparison.</param>
        /// <returns>True if both _enum structs are not equal, false otherwise.</returns>
        public static bool operator !=(_enum lhs, _enum rhs)
        {
            return lhs.value != rhs.value;
        }

        /// <summary>
        /// Implicit conversion from a _enum to an 16-bit signed integer.
        /// </summary>
        /// <param name="enum_">_enum struct to convert.</param>
        /// <returns>16-bit signed representation of <paramref name="enum_"/></returns>
        public static implicit operator short(_enum enum_)
        {
            return enum_.value;
        }

        /// <summary>
        /// Implicit conversion from an 16-bit signed integer to a _enum object.
        /// </summary>
        /// <param name="value">Enum value used to initialize the _enum object.</param>
        /// <returns>_enum struct with the same value as <paramref name="value"/></returns>
        public static implicit operator _enum(short value)
        {
            return new _enum(value);
        }

        /// <summary>
        /// Implicit conversion from a 32-bit signed integer to a _enum object.
        /// </summary>
        /// <param name="value">Enum value used to initialize the char_enum object.</param>
        /// <returns>_enum struct with the same value as <paramref name="value"/></returns>
        public static implicit operator _enum(int value)
        {
            return new _enum(value);
        }

        #endregion

        #region Object Members

        /// <summary>
        /// Compares this _enum object to another object for equality.
        /// </summary>
        /// <param name="obj">Other object to compare to.</param>
        /// <returns>True if both objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // Check if the other object is a _enum object.
            if (obj == null || obj.GetType() != typeof(_enum))
                return false;

            // Cast the other object to a _enum and compare.
            _enum other = (_enum)obj;
            return this == other;
        }

        /// <summary>
        /// Gets a hashcode for this _enum object.
        /// </summary>
        /// <returns>Hashcode of this _enum struct.</returns>
        public override int GetHashCode()
        {
            return this.value;
        }

        /// <summary>
        /// Gets a string representation of the _enum object.
        /// </summary>
        /// <returns>String representation of the _enum object.</returns>
        public override string ToString()
        {
            return string.Format("_enum: {0}", this.value);
        }

        #endregion
    }
}
