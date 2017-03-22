using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX.Direct3D9;
using SlimDX;

namespace Portal
{
    public class Geometry
    {
        public static void ComputeBoundingBox(DataStream points, int VertCount, VertexFormat Format, out Vector3 min, out Vector3 max)
        {
            // Initialize
            byte[] p = new byte[12];
            points.Position = 0;

            // Set up Vectors
            min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // Read Each Vector
            for (int i = 0; i < VertCount / 3; i++)
            {
                // Read out the Verts
                points.Read(p, i * 12, 12);
                float x = BitConverter.ToSingle(p, 0);
                float y = BitConverter.ToSingle(p, 4);
                float z = BitConverter.ToSingle(p, 8);

                // Set Minimums
                min.X = x > min.X ? min.X : x;
                min.Y = y > min.Y ? min.Y : y;
                min.Z = z > min.Z ? min.Z : z;

                // Set Maximums
                max.X = x < max.X ? max.X : x;
                max.Y = y < max.Y ? max.Y : y;
                max.Z = z < max.Z ? max.Z : z;
            }
        }
    }
}
