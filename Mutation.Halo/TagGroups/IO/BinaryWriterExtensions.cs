using Mutation.Halo.TagGroups.FieldTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, ShortBounds bounds)
        {
            // Write a ShortBounds object to the stream.
            writer.Write(bounds.lower);
            writer.Write(bounds.upper);
        }

        public static void Write(this BinaryWriter writer, AngleBounds bounds)
        {
            // Write an AngleBounds object to the stream.
            writer.Write(bounds.lower);
            writer.Write(bounds.upper);
        }

        public static void Write(this BinaryWriter writer, RealBounds bounds)
        {
            // Write a RealBounds object to the stream.
            writer.Write(bounds.lower);
            writer.Write(bounds.upper);
        }

        public static void Write(this BinaryWriter writer, RealFractionBounds bounds)
        {
            // Write a RealFractionBounds object to the stream.
            writer.Write(bounds.lower);
            writer.Write(bounds.upper);
        }

        public static void Write(this BinaryWriter writer, ColorRgb color)
        {
            // Write a ColorRgb object to the stream.
            writer.Write((byte)0);
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
        }

        public static void Write(this BinaryWriter writer, ColorArgb color)
        {
            // Write a ColorArgb object to the stream.
            writer.Write(color.a);
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
        }

        public static void Write(this BinaryWriter writer, RealColorRgb color)
        {
            // Write a RealColorRgb object to the stream.
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
        }

        public static void Write(this BinaryWriter writer, RealColorArgb color)
        {
            // Write a RealColorArgb object to the stream.
            writer.Write(color.a);
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
        }

        public static void Write(this BinaryWriter writer, RealColorHsv color)
        {
            // Write a RealColorHsv object to the stream.
            writer.Write(color.hue);
            writer.Write(color.saturation);
            writer.Write(color.value);
        }

        public static void Write(this BinaryWriter writer, RealColorAhsv color)
        {
            // Write a RealColorAhsv object to the stream.
            writer.Write(color.alpha);
            writer.Write(color.hue);
            writer.Write(color.saturation);
            writer.Write(color.value);
        }

        public static void Write(this BinaryWriter writer, DatumIndex datum)
        {
            // Write a DatumIndex object to the stream.
            writer.Write(datum.handle);
        }

        public static void Write(this BinaryWriter writer, string_id sid)
        {
            // Write a string_id object to the stream.
            writer.Write(sid.handle);
        }

        public static void Write(this BinaryWriter writer, String32 @string)
        {
            // Write a String32 object to the stream.
            writer.Write(@string.value);
        }

        public static void Write(this BinaryWriter writer, String256 @string)
        {
            // Write a String256 object to the stream.
            writer.Write(@string.value);
        }

        public static void Write(this BinaryWriter writer, GroupTag tag)
        {
            // Write a GroupTag object to the stream.
            writer.Write(tag.value);
        }

        public static void Write(this BinaryWriter writer, TagReference tag)
        {
            // Write a TagReference object to the stream.
            writer.Write((byte[])tag.groupTag);
            writer.Write(tag.datum);
        }

        public static void Write(this BinaryWriter writer, Point2d point)
        {
            // Write a Point2d object to the stream.
            writer.Write(point.x);
            writer.Write(point.y);
        }

        public static void Write(this BinaryWriter writer, Rectangle2d rect)
        {
            // Write a Rectangle2d object to the stream.
            writer.Write(rect.top);
            writer.Write(rect.left);
            writer.Write(rect.bottom);
            writer.Write(rect.right);
        }

        public static void Write(this BinaryWriter writer, RealPoint2d point)
        {
            // Write a RealPoint2d object to the stream.
            writer.Write(point.x);
            writer.Write(point.y);
        }

        public static void Write(this BinaryWriter writer, RealPoint3d point)
        {
            // Write a RealPoint3d object to the stream.
            writer.Write(point.x);
            writer.Write(point.y);
            writer.Write(point.z);
        }

        public static void Write(this BinaryWriter writer, RealVector2d vect)
        {
            // Write a RealVector2d object to the stream.
            writer.Write(vect.i);
            writer.Write(vect.j);
        }

        public static void Write(this BinaryWriter writer, RealVector3d vect)
        {
            // Write a RealVector3d object to the stream.
            writer.Write(vect.i);
            writer.Write(vect.j);
            writer.Write(vect.k);
        }

        public static void Write(this BinaryWriter writer, RealQuaternion quat)
        {
            // Write a RealQuaternion object to the stream.
            writer.Write(quat.x);
            writer.Write(quat.y);
            writer.Write(quat.z);
            writer.Write(quat.w);
        }

        public static void Write(this BinaryWriter writer, RealEulerAngle2d angle)
        {
            // Write a RealEulerAngle2d object to the stream.
            writer.Write(angle.yaw);
            writer.Write(angle.pitch);
        }

        public static void Write(this BinaryWriter writer, RealEulerAngle3d angle)
        {
            // Write a RealEulerAngle3d object to the stream.
            writer.Write(angle.yaw);
            writer.Write(angle.pitch);
            writer.Write(angle.roll);
        }

        public static void Write(this BinaryWriter writer, RealPlane2d plane)
        {
            // Write a RealPlane2d object to the stream.
            writer.Write(plane.i);
            writer.Write(plane.j);
            writer.Write(plane.d);
        }

        public static void Write(this BinaryWriter writer, RealPlane3d plane)
        {
            // Write a RealPlane3d object to the stream.
            writer.Write(plane.i);
            writer.Write(plane.j);
            writer.Write(plane.k);
            writer.Write(plane.d);
        }
    }
}
