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
    #region _field_rgb_color

#warning ColorRgb not fully implemented
    [GuerillaType(field_type._field_rgb_color)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct ColorRgb
    {
        /// <summary>
        /// Size of the ColorRgb struct.
        /// </summary>
        public const int kSizeOf = 4;

        byte pad;
        public byte r;
        public byte g;
        public byte b;

        /// <summary>
        /// Initializes a new ColorRgb struct using the values provided.
        /// </summary>
        /// <param name="r">Red channel value.</param>
        /// <param name="g">Green channel value.</param>
        /// <param name="b">Blue channel value.</param>
        public ColorRgb(byte r, byte g, byte b)
        {
            this.pad = 0;
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

    #endregion

    #region _field_argb_color

#warning ColorArg struct not fully implemented
    [GuerillaType(field_type._field_argb_color)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct ColorArgb
    {
        /// <summary>
        /// Size of the ColorArgb struct.
        /// </summary>
        public const int kSizeOf = 4;

        public byte a;
        public byte r;
        public byte g;
        public byte b;

        /// <summary>
        /// Initializes the ColorArgb struct using the values provided.
        /// </summary>
        /// <param name="a">Alpha channel value.</param>
        /// <param name="r">Red channel value.</param>
        /// <param name="g">Green channel value.</param>
        /// <param name="b">Blue channel value.</param>
        public ColorArgb(byte a, byte r, byte g, byte b)
        {
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

    #endregion

    #region _field_real_rgb_color

#warning RealColorRgb not fully implemented
    [GuerillaType(field_type._field_real_rgb_color)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealColorRgb
    {
        /// <summary>
        /// Size of the RealColorRgb struct.
        /// </summary>
        public const int kSizeOf = 12;

        public float r;
        public float g;
        public float b;

        /// <summary>
        ///  Initializes a new RealColorRgb struct with the values provided.
        /// </summary>
        /// <param name="r">Red channel value.</param>
        /// <param name="g">Green channel value.</param>
        /// <param name="b">Blue channel value.</param>
        public RealColorRgb(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

    #endregion

    #region _field_real_argb_color

#warning RealColorArgb not fully implemented
    [GuerillaType(field_type._field_real_argb_color)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealColorArgb
    {
        /// <summary>
        /// Size of the RealColorArgb struct.
        /// </summary>
        public const int kSizeOf = 16;

        public float a;
        public float r;
        public float g;
        public float b;

        /// <summary>
        /// Initializes a new RealColorArgb struct.
        /// </summary>
        /// <param name="a">Alpha channel value.</param>
        /// <param name="r">Red channel value.</param>
        /// <param name="g">Green channel value.</param>
        /// <param name="b">Blue channel value.</param>
        public RealColorArgb(float a, float r, float g, float b)
        {
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

    #endregion

    #region _field_real_hsv_color

#warning RealColorHsv not fully implemented
    [GuerillaType(field_type._field_real_hsv_color)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealColorHsv
    {
        /// <summary>
        /// Size of the RealColorHsv struct.
        /// </summary>
        public const int kSizeOf = 12;

        public float hue;
        public float saturation;
        public float value;

        /// <summary>
        /// Initializes a new RealColorHsv struct using the values provided.
        /// </summary>
        /// <param name="hue">Initial hue.</param>
        /// <param name="saturation">Initial saturation.</param>
        /// <param name="value">Initial value.</param>
        public RealColorHsv(float hue, float saturation, float value)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.value = value;
        }
    }

    #endregion

    #region _field_real_ahsv_color

#warning RealColorAhsv not fully implemented
    [GuerillaType(field_type._field_real_ahsv_color)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealColorAhsv
    {
        /// <summary>
        /// Size of the RealColorAhsv struct.
        /// </summary>
        public const int kSizeOf = 16;

        public float alpha;
        public float hue;
        public float saturation;
        public float value;

        /// <summary>
        /// Initializes a new RealColorAhsv struct using the values provided.
        /// </summary>
        /// <param name="alpha">Initial alpha value.</param>
        /// <param name="hue">Initial hue.</param>
        /// <param name="saturation">Initial saturation.</param>
        /// <param name="value">Initial value.</param>
        public RealColorAhsv(float alpha, float hue, float saturation, float value)
        {
            this.alpha = alpha;
            this.hue = hue;
            this.saturation = saturation;
            this.value = value;
        }
    }

    #endregion
}
