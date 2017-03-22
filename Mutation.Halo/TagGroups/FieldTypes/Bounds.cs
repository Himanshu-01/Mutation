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
    #region _field_short_bounds

#warning ShortBounds not fully implemented
    [GuerillaType(field_type._field_short_bounds)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct ShortBounds
    {
        /// <summary>
        /// Size of the ShortBounds struct.
        /// </summary>
        public const int kSizeOf = 4;

        public short lower;
        public short upper;

        /// <summary>
        /// Initializes a new ShortBounds struct using the values provided.
        /// </summary>
        /// <param name="lower">Lower bounds.</param>
        /// <param name="upper">Upper bounds.</param>
        public ShortBounds(short lower, short upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }

    #endregion

    #region _field_angle_bounds

#warning AngleBounds not fully implemented
    [GuerillaType(field_type._field_angle_bounds)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct AngleBounds
    {
        /// <summary>
        /// Size of the AngleBounds struct.
        /// </summary>
        public const int kSizeOf = 8;

        public float lower;
        public float upper;

        /// <summary>
        /// Initializes a new AngleBounds struct using the values provided.
        /// </summary>
        /// <param name="lower">Lower bounds.</param>
        /// <param name="upper">Upper bounds.</param>
        public AngleBounds(float lower, float upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }

    #endregion

    #region _field_real_bounds

#warning RealBounds not fully implemented
    [GuerillaType(field_type._field_real_bounds)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealBounds
    {
        /// <summary>
        /// Size of the RealBounds struct.
        /// </summary>
        public const int kSizeOf = 8;

        public float lower;
        public float upper;

        /// <summary>
        /// Initializes a new RealBounds struct using the values provided.
        /// </summary>
        /// <param name="lower">Lower bounds.</param>
        /// <param name="upper">Upper bounds.</param>
        public RealBounds(float lower, float upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }

    #endregion

    #region _field_real_fraction_bounds

#warning RealFractionBounds not fully implemented
    [GuerillaType(field_type._field_real_fraction_bounds)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealFractionBounds
    {
        /// <summary>
        /// Size of the RealFractionBounds struct.
        /// </summary>
        public const int kSizeOf = 8;

        public float lower;
        public float upper;

        /// <summary>
        /// Initializes a new RealFractionBounds struct using the values provided.
        /// </summary>
        /// <param name="lower">Lower bounds.</param>
        /// <param name="upper">Upper bounds.</param>
        public RealFractionBounds(float lower, float upper)
        {
            this.lower = lower;
            this.upper = upper;
        }
    }

    #endregion
}
