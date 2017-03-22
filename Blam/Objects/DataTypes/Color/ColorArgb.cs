using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blam.Objects.DataTypes
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = kSizeOf)]
    public struct ColorArgb
    {
        /// <summary>
        /// Size of a ColorArgb object.
        /// </summary>
        public const int kSizeOf = 4;

        /// <summary>
        /// Alpha value of the Argb color.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public byte a;

        /// <summary>
        /// Red value of the Argb color.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(1)]
        public byte r;

        /// <summary>
        /// Green value of the Argb color.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(2)]
        public byte g;

        /// <summary>
        /// Blue value of the Argb color.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(3)]
        public byte b;

        #region Constructor

        /// <summary>
        /// Initializes the ColorArgb struct with the color values provided.
        /// </summary>
        /// <param name="a">Initial Alpha value.</param>
        /// <param name="r">Initial Red value.</param>
        /// <param name="g">Initial Green value.</param>
        /// <param name="b">Initial Blue value.</param>
        public ColorArgb(byte a, byte r, byte g, byte b)
        {
            // Initialize fields.
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        /// <summary>
        /// Initializes the ColorArgb struct with the 32-bit color value provided.
        /// </summary>
        /// <param name="color">Initial color value.</param>
        public ColorArgb(int color)
        {
            // Initialize fields.
            this.a = (byte)((color & 0xFF000000) >> 24);
            this.r = (byte)((color & 0xFF0000) >> 16);
            this.g = (byte)((color & 0xFF00) >> 8);
            this.b = (byte)(color & 0xFF);
        }

        /// <summary>
        /// Initializes the ColorArgb struct with the <see cref="System.Drawing.Color"/> object provided.
        /// </summary>
        /// <param name="color">Initial color value.</param>
        public ColorArgb(System.Drawing.Color color)
        {
            // Initialize fields.
            this.a = color.A;
            this.r = color.R;
            this.g = color.G;
            this.b = color.B;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the argb color to a System.Drawing.Color struct.
        /// </summary>
        /// <returns>The System.Drawing.Color struct representation of this argb color.</returns>
        public System.Drawing.Color ToColor()
        {
            return System.Drawing.Color.FromArgb(this.a, this.r, this.g, this.b);
        }

        /// <summary>
        /// Converts the argb color to a 32-bit signed integer.
        /// </summary>
        /// <returns>Integer representation of the argb color.</returns>
        public int ToInt32()
        {
#warning each color byte may need to be casted to an integer
            return (int)(this.a << 24 | this.r << 16 | this.g << 8 | this.b);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two ColorArgb structs for equality.
        /// </summary>
        /// <param name="lhs">Left hand ColorArgb struct for comparison.</param>
        /// <param name="rhs">Right hand ColorArgb struct for comparison.</param>
        /// <returns>True if both ColorArgb structs are equal, false otherwise.</returns>
        public static bool operator ==(ColorArgb lhs, ColorArgb rhs)
        {
            // Compare each color value.
            return (lhs.a == rhs.a && lhs.r == rhs.r && lhs.g == rhs.g && lhs.b == rhs.b);
        }

        /// <summary>
        /// Compares two ColorArgb structs for inequality.
        /// </summary>
        /// <param name="lhs">Left hand ColorArgb struct for comparison.</param>
        /// <param name="rhs">Right hand ColorArgb struct for comparison.</param>
        /// <returns>True if the ColorArgb structs are not equal, false otherwise.</returns>
        public static bool operator !=(ColorArgb lhs, ColorArgb rhs)
        {
            // Compare each color value.
            return !(lhs.a == rhs.a && lhs.r == rhs.r && lhs.g == rhs.g && lhs.b == rhs.b);
        }

        /// <summary>
        /// Implicit conversion from a ColorArgb struct to a 32-bit signed integer in ARGB format.
        /// </summary>
        /// <param name="color">ColorArgb struct to convert.</param>
        /// <returns>Integer representation of <paramref name="color"/> in ARGB format.</returns>
        public static implicit operator int(ColorArgb color)
        {
            return color.ToInt32();
        }

        /// <summary>
        /// Implicit conversion from a ColorArgb struct to a <see cref="System.Drawing.Color"/> struct.
        /// </summary>
        /// <param name="color">ColorArgb struct to convert.</param>
        /// <returns><see cref="System.Drawing.Color"/> representation of <paramref name="color"/>.</returns>
        public static implicit operator System.Drawing.Color(ColorArgb color)
        {
            return color.ToColor();
        }

        /// <summary>
        /// Implicit conversion from a 32-bit signed integer to a ColorArgb struct.
        /// </summary>
        /// <param name="color">32-bit signed integer in ARGB format to convert from.</param>
        /// <returns>ColorArgb struct with the same color value as <paramref name="color"/>.</returns>
        public static implicit operator ColorArgb(int color)
        {
            return new ColorArgb(color);
        }

        /// <summary>
        /// Implicit conversion from a <see cref="System.Drawing.Color"/> struct to a ColorArgb struct.
        /// </summary>
        /// <param name="color"><see cref="System.Drawing.Color"/> to convert from.</param>
        /// <returns>ColorArgb struct with the same color value as <paramref name="color"/>.</returns>
        public static implicit operator ColorArgb(System.Drawing.Color color)
        {
            return new ColorArgb(color);
        }

        #endregion

        #region Object Members

        /// <summary>
        /// Compares this ColorArgb struct to another object for equality.
        /// </summary>
        /// <param name="obj">Other object to compare to.</param>
        /// <returns>True if both objects are equal, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            // Check if the other object is a ColorArgb object.
            if (obj == null || obj.GetType() != typeof(ColorArgb))
                return false;

            // Cast the other object to a ColorArgb and compare.
            ColorArgb other = (ColorArgb)obj;
            return this == other;
        }

        /// <summary>
        /// Gets a hashcode for this ColorArgb object.
        /// </summary>
        /// <returns>Hashcode of this ColorArgb struct.</returns>
        public override int GetHashCode()
        {
            return this.ToInt32();
        }

        /// <summary>
        /// Gets a string representation of this ColorArgb object.
        /// </summary>
        /// <returns>String representation of this ColorArgb object.</returns>
        public override string ToString()
        {
            return string.Format("ColorArgb {{ a: {0} r: {1} g: {2} b: {3} }}",
                this.a, this.r, this.g, this.b);
        }

        #endregion
    }
}
