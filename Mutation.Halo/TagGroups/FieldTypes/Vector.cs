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
    #region _field_point_2d

#warning Point2D not fully implemented
    [GuerillaType(field_type._field_point_2d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct Point2d
    {
        /// <summary>
        /// Size of the Point2d struct.
        /// </summary>
        public const int kSizeOf = 4;

        public short x;
        public short y;

        /// <summary>
        /// Initializes a new Point2d struct using the values provided.
        /// </summary>
        /// <param name="x">x vertex value.</param>
        /// <param name="y">y vertex value.</param>
        public Point2d(short x, short y)
        {
            // Initialize fields.
            this.x = x;
            this.y = y;
        }
    }

    #endregion

    #region _field_rectangle_2d

#warning Rectangle2d not full implemented
    [GuerillaType(field_type._field_rectangle_2d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct Rectangle2d
    {
        /// <summary>
        /// Size of the Rectangle2d struct.
        /// </summary>
        public const int kSizeOf = 8;

        public short top;
        public short left;
        public short bottom;
        public short right;

        /// <summary>
        /// Initializes a new Rectangle2s struct using the values provided as the initial values.
        /// </summary>
        /// <param name="top">Initial top value.</param>
        /// <param name="left">Initial left value.</param>
        /// <param name="bottom">Initial bottom value.</param>
        /// <param name="right">Initial right value.</param>
        public Rectangle2d(short top, short left, short bottom, short right)
        {
            // Initializes fields using the values provided.
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
        }
    }

    #endregion

    #region _field_real_point_2d

#warning RealPoint2d not fully implemented!
    [GuerillaType(field_type._field_real_point_2d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealPoint2d
    {
        /// <summary>
        /// Size of the RealPoint2d struct.
        /// </summary>
        public const int kSizeOf = 8;

        public float x;
        public float y;

        /// <summary>
        /// Initializes a new RealPoint2d struct using the values provided.
        /// </summary>
        /// <param name="x">x vertex value.</param>
        /// <param name="y">y vertex value.</param>
        public RealPoint2d(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    #endregion

    #region _field_real_point_3d

#warning RealPoint3d not fully implemented
    [GuerillaType(field_type._field_real_point_3d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealPoint3d
    {
        /// <summary>
        /// Size of the RealPoint3d struct.
        /// </summary>
        public const int kSizeOf = 12;

        public float x;
        public float y;
        public float z;

        /// <summary>
        /// Initializes a new RealPoint3d struct with the values provided.
        /// </summary>
        /// <param name="x">x vertex value.</param>
        /// <param name="y">y vertex value.</param>
        /// <param name="z">z vertex value.</param>
        public RealPoint3d(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    #endregion

    #region _field_real_vector_2d

#warning RealVector2d not fully implemented
    [GuerillaType(field_type._field_real_vector_2d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealVector2d
    {
        /// <summary>
        /// Size of the Vector2d struct.
        /// </summary>
        public const int kSizeOf = 8;

        public float i;
        public float j;

        /// <summary>
        /// Initializes a new RealVector2d struct using the values provided.
        /// </summary>
        /// <param name="i">i vertex value.</param>
        /// <param name="j">j vertex value.</param>
        public RealVector2d(float i, float j)
        {
            this.i = i;
            this.j = j;
        }
    }

    #endregion

    #region _field_real_vector_3d

#warning RealVector3d not fully implemented
    [GuerillaType(field_type._field_real_vector_3d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealVector3d
    {
        /// <summary>
        /// Size of the RealVector3d strcut.
        /// </summary>
        public const int kSizeOf = 12;

        public float i;
        public float j;
        public float k;

        /// <summary>
        /// Initializes a new RealVector3d using the values provided.
        /// </summary>
        /// <param name="i">i vertex value.</param>
        /// <param name="j">j vertex value.</param>
        /// <param name="k">k vertex value.</param>
        public RealVector3d(float i, float j, float k)
        {
            this.i = i;
            this.j = j;
            this.k = k;
        }
    }

    #endregion

    #region _field_real_quaternion

#warning RealQuaternion not fully implemented
    [GuerillaType(field_type._field_real_quaternion)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealQuaternion
    {
        /// <summary>
        /// Size of the RealQuaternion struct.
        /// </summary>
        public const int kSizeOf = 16;

        public float x;
        public float y;
        public float z;
        public float w;

        /// <summary>
        /// Initializes a new RealQuaternion struct with the values provided.
        /// </summary>
        /// <param name="x">x vertex value.</param>
        /// <param name="y">y vertex value.</param>
        /// <param name="z">z vertex value.</param>
        /// <param name="w">w vertex value.</param>
        public RealQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    #endregion

    #region _field_real_euler_angles_2d

#warning RealEulerAngle2d not fully implemented
    [GuerillaType(field_type._field_real_euler_angles_2d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealEulerAngle2d
    {
        /// <summary>
        /// Size of the RealEulerAngle2d struct.
        /// </summary>
        public const int kSizeOf = 8;

        public float yaw;
        public float pitch;

        /// <summary>
        /// Initializes a new RealEulerAngle2d struct using the values provided.
        /// </summary>
        /// <param name="yaw">yaw rotation value.</param>
        /// <param name="pitch">pitch rotation value.</param>
        public RealEulerAngle2d(float yaw, float pitch)
        {
            this.yaw = yaw;
            this.pitch = pitch;
        }
    }

    #endregion

    #region _field_real_euler_angles_3d

#warning RealEulerAngle3d not fully implemented
    [GuerillaType(field_type._field_real_euler_angles_3d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealEulerAngle3d
    {
        /// <summary>
        /// Size of the RealEulerAngle3d struct.
        /// </summary>
        public const int kSizeOf = 12;

        public float yaw;
        public float pitch;
        public float roll;

        /// <summary>
        /// Initializes a new RealEulerAngle3d struct using the values provided.
        /// </summary>
        /// <param name="yaw">yaw rotation value.</param>
        /// <param name="pitch">pitch rotation value.</param>
        /// <param name="roll">roll rotation value.</param>
        public RealEulerAngle3d(float yaw, float pitch, float roll)
        {
            this.yaw = yaw;
            this.pitch = pitch;
            this.roll = roll;
        }
    }

    #endregion

    #region _field_real_plane_2d

#warning RealPlane2d not fully implemented
    [GuerillaType(field_type._field_real_plane_2d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealPlane2d
    {
        /// <summary>
        /// Size of a the RealPlane2d struct.
        /// </summary>
        public const int kSizeOf = 12;

        public float i;
        public float j;
        public float d;

        /// <summary>
        /// Initializes a new RealPlane2d struct using the values provided.
        /// </summary>
        /// <param name="i">X-component of the plane's normal.</param>
        /// <param name="j">Y-component of the plane's normal.</param>
        /// <param name="d">Distance the plane is from the origin.</param>
        public RealPlane2d(float i, float j, float d)
        {
            this.i = i;
            this.j = j;
            this.d = d;
        }
    }

    #endregion

    #region _field_real_plane_3d

#warning RealPlane3d not fully implemented
    [GuerillaType(field_type._field_real_plane_3d)]
    [StructLayout(LayoutKind.Sequential, Size = kSizeOf)]
    public struct RealPlane3d
    {
        /// <summary>
        /// Size of a the RealPlane3d struct.
        /// </summary>
        public const int kSizeOf = 16;

        public float i;
        public float j;
        public float k;
        public float d;

        /// <summary>
        /// Initializes a new RealPlane3d struct using the values provided.
        /// </summary>
        /// <param name="i">X-component of the plane's normal.</param>
        /// <param name="j">Y-component of the plane's normal.</param>
        /// <param name="k">Z-component of the plane's normal.</param>
        /// <param name="d">Distance the plane is from the origin.</param>
        public RealPlane3d(float i, float j, float k, float d)
        {
            this.i = i;
            this.j = j;
            this.k = k;
            this.d = d;
        }
    }

    #endregion
}
