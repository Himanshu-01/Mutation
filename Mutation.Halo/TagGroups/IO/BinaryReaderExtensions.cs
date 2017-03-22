using Mutation.Halo.TagGroups.FieldTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo
{
    public static class BinaryReaderExtensions
    {
        public static ShortBounds ReadShortBounds(this BinaryReader reader)
        {
            // Read a ShortBounds object from the stream.
            ShortBounds bounds = new ShortBounds();
            bounds.lower = reader.ReadInt16();
            bounds.upper = reader.ReadInt16();

            return bounds;
        }

        public static AngleBounds ReadAngleBounds(this BinaryReader reader)
        {
            // Read an AngleBounds object from the stream.
            AngleBounds bounds = new AngleBounds();
            bounds.lower = reader.ReadSingle();
            bounds.upper = reader.ReadSingle();

            return bounds;
        }

        public static RealBounds ReadRealBounds(this BinaryReader reader)
        {
            // Read a RealBounds object from the stream.
            RealBounds bounds = new RealBounds();
            bounds.lower = reader.ReadSingle();
            bounds.upper = reader.ReadSingle();

            return bounds;
        }

        public static RealFractionBounds ReadRealFractionBounds(this BinaryReader reader)
        {
            // Read a RealFractionBounds object from the stream.
            RealFractionBounds bounds = new RealFractionBounds();
            bounds.lower = reader.ReadSingle();
            bounds.upper = reader.ReadSingle();

            return bounds;
        }

        public static ColorRgb ReadColorRgb(this BinaryReader reader)
        {
            // Read a ColorRgb object from the stream.
            ColorRgb color = new ColorRgb();
            reader.BaseStream.Position++;
            color.r = reader.ReadByte();
            color.g = reader.ReadByte();
            color.b = reader.ReadByte();

            return color;
        }

        public static ColorArgb ReadColorArgb(this BinaryReader reader)
        {
            // Read a ColorArgb object from the stream.
            ColorArgb color = new ColorArgb();
            color.a = reader.ReadByte();
            color.r = reader.ReadByte();
            color.g = reader.ReadByte();
            color.b = reader.ReadByte();

            return color;
        }

        public static RealColorRgb ReadRealColorRgb(this BinaryReader reader)
        {
            // Read a RealColorRgb object from the stream.
            RealColorRgb color = new RealColorRgb();
            color.r = reader.ReadSingle();
            color.g = reader.ReadSingle();
            color.b = reader.ReadSingle();

            return color;
        }

        public static RealColorArgb ReadRealColorArgb(this BinaryReader reader)
        {
            // Read a RealColorArgb object from the stream.
            RealColorArgb color = new RealColorArgb();
            color.a = reader.ReadSingle();
            color.r = reader.ReadSingle();
            color.g = reader.ReadSingle();
            color.b = reader.ReadSingle();

            return color;
        }

        public static RealColorHsv ReadRealColorHsv(this BinaryReader reader)
        {
            // Read a RealColorHsv object from the stream.
            RealColorHsv color = new RealColorHsv();
            color.hue = reader.ReadSingle();
            color.saturation = reader.ReadSingle();
            color.value = reader.ReadSingle();

            return color;
        }

        public static RealColorAhsv ReadRealColorAhsv(this BinaryReader reader)
        {
            // Read a RealColorAhsv object from the stream.
            RealColorAhsv color = new RealColorAhsv();
            color.alpha = reader.ReadSingle();
            color.hue = reader.ReadSingle();
            color.saturation = reader.ReadSingle();
            color.value = reader.ReadSingle();

            return color;
        }

        public static DatumIndex ReadDatumIndex(this BinaryReader reader)
        {
            // Read a DatumIndex object from the stream.
            DatumIndex index = new DatumIndex();
            index.handle = reader.ReadUInt32();

            return index;
        }

        public static string_id ReadStringID(this BinaryReader reader)
        {
            // Read a string_id object from the stream.
            string_id sid = new string_id();
            sid.handle = reader.ReadInt32();

            return sid;
        }

        public static String32 ReadString32(this BinaryReader reader)
        {
            // Read a String32 object from the stream.
            String32 @string = new String32();
            @string.value = reader.ReadChars(String32.kSizeOf);

            return @string;
        }

        public static String256 ReadString256(this BinaryReader reader)
        {
            // Read a String256 object from the stream.
            String256 @string = new String256();
            @string.value = reader.ReadChars(String256.kSizeOf);

            return @string;
        }

        public static GroupTag ReadGroupTag(this BinaryReader reader)
        {
            // Read a GroupTag object from the stream.
            GroupTag tag = new GroupTag();
            tag.value = reader.ReadChars(GroupTag.kSizeOf);

            return tag;
        }

        public static TagReference ReadTagReference(this BinaryReader reader)
        {
            // Read a TagReference object from the stream.
            TagReference reference = new TagReference();
            reference.groupTag = reader.ReadGroupTag();
            reference.datum = reader.ReadDatumIndex();

            return reference;
        }

        public static Point2d ReadPoint2d(this BinaryReader reader)
        {
            // Read a Point2d object from the stream.
            Point2d point = new Point2d();
            point.x = reader.ReadInt16();
            point.y = reader.ReadInt16();

            return point;
        }

        public static Rectangle2d ReadRectangle2d(this BinaryReader reader)
        {
            // Read a Rectangle2d object from the stream.
            Rectangle2d rect = new Rectangle2d();
            rect.top = reader.ReadInt16();
            rect.left = reader.ReadInt16();
            rect.bottom = reader.ReadInt16();
            rect.right = reader.ReadInt16();

            return rect;
        }

        public static RealPoint2d ReadRealPoint2d(this BinaryReader reader)
        {
            // Read a RealPoint2d object from the stream.
            RealPoint2d point = new RealPoint2d();
            point.x = reader.ReadSingle();
            point.y = reader.ReadSingle();

            return point;
        }

        public static RealPoint3d ReadRealPoint3d(this BinaryReader reader)
        {
            // Read a RealPoint3d object from the stream.
            RealPoint3d point = new RealPoint3d();
            point.x = reader.ReadSingle();
            point.y = reader.ReadSingle();
            point.z = reader.ReadSingle();

            return point;
        }

        public static RealVector2d ReadRealVector2d(this BinaryReader reader)
        {
            // Read a RealVector2d object from the stream.
            RealVector2d vect = new RealVector2d();
            vect.i = reader.ReadSingle();
            vect.j = reader.ReadSingle();

            return vect;
        }

        public static RealVector3d ReadRealVector3d(this BinaryReader reader)
        {
            // Read a RealVector3d object from the stream.
            RealVector3d vect = new RealVector3d();
            vect.i = reader.ReadSingle();
            vect.j = reader.ReadSingle();
            vect.k = reader.ReadSingle();

            return vect;
        }

        public static RealQuaternion ReadRealQuaternion(this BinaryReader reader)
        {
            // Read a RealQuaternion object from the stream.
            RealQuaternion quat = new RealQuaternion();
            quat.x = reader.ReadSingle();
            quat.y = reader.ReadSingle();
            quat.z = reader.ReadSingle();
            quat.w = reader.ReadSingle();

            return quat;
        }

        public static RealEulerAngle2d ReadRealEulerAngle2d(this BinaryReader reader)
        {
            // Read a RealEulerAngle2d object from the stream.
            RealEulerAngle2d angle = new RealEulerAngle2d();
            angle.yaw = reader.ReadSingle();
            angle.pitch = reader.ReadSingle();

            return angle;
        }

        public static RealEulerAngle3d ReadRealEulerAngle3d(this BinaryReader reader)
        {
            // Read a RealEulerAngle3d object from the stream.
            RealEulerAngle3d angle = new RealEulerAngle3d();
            angle.yaw = reader.ReadSingle();
            angle.pitch = reader.ReadSingle();
            angle.roll = reader.ReadSingle();

            return angle;
        }

        public static RealPlane2d ReadRealPlane2d(this BinaryReader reader)
        {
            // Read a RealPlane2d object from the stream.
            RealPlane2d plane = new RealPlane2d();
            plane.i = reader.ReadSingle();
            plane.j = reader.ReadSingle();
            plane.d = reader.ReadSingle();

            return plane;
        }

        public static RealPlane3d ReadRealPlane3d(this BinaryReader reader)
        {
            // Read a RealPlane3d object from the stream.
            RealPlane3d plane = new RealPlane3d();
            plane.i = reader.ReadSingle();
            plane.j = reader.ReadSingle();
            plane.k = reader.ReadSingle();
            plane.d = reader.ReadSingle();

            return plane;
        }
    }
}
