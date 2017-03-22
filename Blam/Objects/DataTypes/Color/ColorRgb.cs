using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blam.Objects.DataTypes
{
    /*
     * See the hud_globals tag definition to test the rgb_color size theory.
     */

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = kSizeOf)]
    public struct ColorRgb
    {
        /// <summary>
        /// Size of a ColorRgb object.
        /// </summary>
        public const int kSizeOf = 4;

        /// <summary>
        /// Red value of the Rgb color.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(0)]
        public byte r;

        /// <summary>
        /// Green value of the Rgb color.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(1)]
        public byte g;

        /// <summary>
        /// Blue value of the Rgb color.
        /// </summary>
        [System.Runtime.InteropServices.FieldOffset(2)]
        public byte b;

        #region Constructor

        /// <summary>
        /// Initializes the ColorRgb struct with the color values provided.
        /// </summary>
        /// <param name="r">Initial Red value.</param>
        /// <param name="g">Initial Green value.</param>
        /// <param name="b">Initial Blue value.</param>
        public ColorRgb(byte r, byte g, byte b)
        {
            // Initialize fields.
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public ColorRgb(int color)
        {
            // Initialize fields.
            this.r = (byte)((color & 0xFF0000) >> 24);
            this.g = (byte)((color & 0xFF00) >> 26);
            this.b = (byte)((color & 0xFF) >> 8);
        }

        #endregion
    }
}
