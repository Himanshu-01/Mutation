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
    #region _field_string

    [GuerillaType(field_type._field_string)]
    [StructLayout(LayoutKind.Explicit, Size = kSizeOf)]
    public struct String32
    {
        /// <summary>
        /// Size of the String32 struct.
        /// </summary>
        public const int kSizeOf = 32;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = kSizeOf)]
        [FieldOffset(0)]
        public char[] value;

        /// <summary>
        /// Initializes a new String32 struct using the string value provided as the initial value.
        /// </summary>
        /// <param name="value">Initial value string.</param>
        public String32(string value)
        {
            // Initialize the character buffer.
            this.value = new char[kSizeOf];

            // Copy at most 32 characters to the buffer.
            Array.Copy(value.ToCharArray(), this.value, Math.Min(value.Length, kSizeOf));
        }

        public static implicit operator string(String32 @string)
        {
            // Implicit cast from a String32 struct to a string.
            return new string(@string.value);
        }

        public static implicit operator String32(string value)
        {
            // Implicit cast from a string to a String32 struct.
            return new String32(value);
        }

        public static bool operator ==(String32 a, String32 b)
        {
            // Comparison for equality.
            return a.value.Equals(b.value);
        }

        public static bool operator !=(String32 a, String32 b)
        {
            // Comparison for inequality.
            return !a.value.Equals(b.value);
        }

        public override bool Equals(object obj)
        {
            // Check that the other object is a String32 struct.
            if (obj != null && obj.GetType() != typeof(String32))
                return false;

            // Cast the other object to a String32 struct and compare.
            return this == (String32)obj;
        }

        public override int GetHashCode()
        {
            // Return the hashcode of the character array.
            return this.value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Value={0}", new string(this.value));
        }
    }

    #endregion

    #region _field_long_string

    [GuerillaType(field_type._field_long_string)]
    [StructLayout(LayoutKind.Explicit, Size = kSizeOf)]
    public struct String256
    {
        /// <summary>
        /// Size of the String256 struct
        /// </summary>
        public const int kSizeOf = 256;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = kSizeOf)]
        [FieldOffset(0)]
        public char[] value;

        public String256(string value)
        {
            // Initialize the string buffer to an empty string.
            this.value = new char[kSizeOf];

            // Copy at most 256 characters.
            Array.Copy(value.ToCharArray(), this.value, Math.Min(value.Length, kSizeOf));
        }

        public static implicit operator string(String256 @string)
        {
            // Implicit cast from a String256 struct to a string.
            return new string(@string.value);
        }

        public static implicit operator String256(string value)
        {
            // Implicit cast from a string to a String256 struct.
            return new String256(value);
        }

        public static bool operator ==(String256 a, String256 b)
        {
            // Comparison for equality.
            return a.value.Equals(b.value);
        }

        public static bool operator !=(String256 a, String256 b)
        {
            // Comparison for inequality.
            return !a.value.Equals(b.value);
        }

        public override bool Equals(object obj)
        {
            // Check that the other object is a String256 struct.
            if (obj != null && obj.GetType() != typeof(String256))
                return false;

            // Cast the other object to a String32 struct and compare.
            return this == (String256)obj;
        }

        public override int GetHashCode()
        {
            // Return the hashcode of the character array.
            return this.value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Value={0}", new string(this.value));
        }
    }

    #endregion

    #region _field_tag

    [GuerillaType(field_type._field_tag)]
    [StructLayout(LayoutKind.Explicit, Size = kSizeOf)]
    public struct GroupTag
    {
        /// <summary>
        /// Represents a group tag for any tag type.
        /// </summary>
        public const string WildCard = "****";

        /// <summary>
        /// Represents a null group tag.
        /// </summary>
        public const int Null = unchecked((int)0xFFFFFFFF);

        /// <summary>
        /// Size of the tag struct.
        /// </summary>
        public const int kSizeOf = 4;

        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = kSizeOf)]
        public char[] value;

        public GroupTag(char[] tag)
        {
            // Initialize the tag array.
            this.value = new char[kSizeOf];
            Array.Copy(tag, this.value, Math.Min(tag.Length, kSizeOf));
        }

        public GroupTag(byte[] tag)
        {
            // Initialize the tag array.
            this.value = new char[kSizeOf];

            // Convert the byte array to a char array, copying at most kSizeOf characters.
            for (int i = 0; i < Math.Min(kSizeOf, tag.Length); i++)
                this.value[i] = (char)tag[i];
        }

        public GroupTag(int value)
        {
            // Initialize the tag array.
            this.value = new char[kSizeOf];

            // Convert the int to a byte array, and copy at most kSizeOf characters.
            byte[] bits = BitConverter.GetBytes(value);
            for (int i = 0; i < Math.Min(kSizeOf, bits.Length); i++)
                this.value[i] = (char)bits[i];
        }

        public GroupTag(string tag)
        {
            // Initialize the tag array.
            this.value = new char[kSizeOf];
            Array.Copy(tag.ToCharArray(), this.value, Math.Min(tag.Length, kSizeOf));
        }

        public string Reverse()
        {
            // Return the string in reverse order.
            return new string(new char[] { this.value[3], this.value[2], this.value[1], this.value[0] });
        }

        public static explicit operator string(GroupTag _tag)
        {
            // Implicit cast from a tag struct to a string.
            return new string(_tag.value);
        }

        public static implicit operator GroupTag(string value)
        {
            // Implicit cast from a string to a tag struct.
            return new GroupTag(value);
        }

        public static explicit operator byte[](GroupTag _tag)
        {
            // Implicit cast from a tag struct to a byte array.
            return new byte[] 
            { 
                (byte)_tag.value[0],
                (byte)_tag.value[1],
                (byte)_tag.value[2],
                (byte)_tag.value[3]
            };
        }

        public static implicit operator GroupTag(byte[] value)
        {
            // Implicit cast from a byte array to a tag struct.
            return new GroupTag(value);
        }

        public static explicit operator int(GroupTag _tag)
        {
            // Implicit cast from a tag struct to a 32-bit integer.
            byte[] bits = new byte[] 
            { 
                (byte)_tag.value[0],
                (byte)_tag.value[1],
                (byte)_tag.value[2],
                (byte)_tag.value[3]
            };

            // Convert the byte array to a 32-bit integer.
            return BitConverter.ToInt32(bits, 0);
        }

        public static implicit operator GroupTag(int value)
        {
            // Implicit cast from a 32-bit integer to a tag struct.
            byte[] bits = BitConverter.GetBytes(value);
            return new GroupTag(bits);
        }

        public static bool operator ==(GroupTag a, GroupTag b)
        {
            // Compare the two objects for equality.
            return a.value.Equals(b.value);
        }

        public static bool operator !=(GroupTag a, GroupTag b)
        {
            // Compare the two objects for inequality.
            return !a.value.Equals(b.value);
        }

        public override bool Equals(object obj)
        {
            // Check if the other object is not null and is a tag object.
            if (obj == null || obj.GetType() != typeof(GroupTag))
                return false;

            // Compare the two objects.
            return this == (GroupTag)obj;
        }

        public override int GetHashCode()
        {
            // Return the tag as a 32-bit integer for the hashcode.
            return (int)this;
        }

        public override string ToString()
        {
            return string.Format("Value={0}", new string(this.value));
        }
    }

    #endregion
}
