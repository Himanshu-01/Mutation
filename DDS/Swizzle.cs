using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDS
{
    internal class Swizzling
    {
        public static byte[] Swizzle(byte[] raw, int pixOffset, int Width, int Height, int depth, int bitCount, bool deswizzle)
        {
            bitCount /= 8;
            int a = 0;
            int b = 0;
            byte[] dataArray = new byte[raw.Length]; //Bitmap.Width * Height * bitCount;

            MaskSet masks = new MaskSet(Width, Height, depth);
            pixOffset = 0;
            for (int y = 0; y < Height * depth; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (deswizzle)
                    {
                        a = ((y * Width) + x) * bitCount;
                        b = (Swizzle(x, y, depth, masks)) * bitCount;
                    }
                    else
                    {
                        b = ((y * Width) + x) * bitCount;
                        a = (Swizzle(x, y, depth, masks)) * bitCount;
                    }

                    if (a < dataArray.Length && b < raw.Length)
                    {
                        for (int i = pixOffset; i < bitCount + pixOffset; i++)
                            dataArray[a + i] = raw[b + i];
                    }
                    else return null;
                }
            }

            //for (int u = 0; u < pixOffset; u++)
            //    raw[u] = raw[u];
            //for (int v = pixOffset + (Height * Width * depth * bitCount); v < raw.Length; v++)
            //    raw[v] = raw[v];

            return dataArray;
        }

        public static byte[] Swizzle(byte[] raw, int Width, int Height, int depth, int bitCount, bool deswizzle)
        {
            return Swizzle(raw, 0, Width, Height, depth, bitCount, deswizzle);
        }

        public static int Swizzle(int x, int y, int z, MaskSet masks)
        {
            return SwizzleAxis(x, masks.x) | SwizzleAxis(y, masks.y) | (z == -1 ? 0 : SwizzleAxis(z, masks.z));
        }

        public static int SwizzleAxis(int val, int mask)
        {
            int bit = 1;
            int result = 0;

            while (bit <= mask)
            {
                int tmp = mask & bit;

                if (tmp != 0) result |= (val & bit);
                else val <<= 1;

                bit <<= 1;
            }

            return result;
        }
    }

    class MaskSet
    {
        public int x = 0;
        public int y = 0;
        public int z = 0;

        public MaskSet(int w, int h, int d)
        {
            int bit = 1;
            int index = 1;

            while (bit < w || bit < h || bit < d)
            {
                //if (bit == 0) break;
                if (bit < w)
                {
                    x |= index;
                    index <<= 1;
                }
                if (bit < h)
                {
                    y |= index;
                    index <<= 1;
                }
                if (bit < d)
                {
                    z |= index;
                    index <<= 1;
                }
                bit <<= 1;
            }
        }
    }
}
