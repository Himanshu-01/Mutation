using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace DDS
{
    public enum BitmapFormat
    {
        A8 = 0,
        Y8 = 1,
        AY8 = 2,
        A8Y8 = 3,
        R5G6B5 = 6,
        A1R5G5B5 = 8,
        A4R4G4B4 = 9,
        X8R8G8B8 = 10,
        A8R8G8B8 = 11,
        DXT1 = 14,
        DXT3 = 15,
        DXT5 = 16,
        P8 = 17,
        Lightmap = 18,
        U8V8 = 22,
    }

    public enum BitmapType
    {
        Texture_2D,
        Texture_3D,
        Cubemap,
        Sprite,
    }

    public class DDSImage
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Size { get; set; }
        public BitmapFormat Format { get; set; }
        public BitmapType Type { get; set; }
        public byte[] Buffer { get; set; }
        public bool Swizzled { get; set; }
        public int Depth { get; set; }
        public int PixelOffset { get; set; }
        public int MipMapCount { get; set; }

        public DDSImage(int Height, int Width, byte[] Buffer, BitmapFormat Format, BitmapType Type,
            bool Swizzled, int Depth, int PixelOffset, int MipMapCount)
        {
            this.Height = Height;
            this.Width = Width;
            this.Buffer = Buffer;
            this.Size = Buffer.Length;
            this.Format = Format;
            this.Type = Type;
            this.Swizzled = Swizzled;
            this.Depth = Depth;
            this.PixelOffset = PixelOffset;
            this.MipMapCount = MipMapCount;
        }

        public Bitmap GetImage()
        {
            #region Decode
            byte[] DecodedLOD = Buffer;
            byte[] RecodedLOD = new byte[0];
            PixelFormat pixelFormat = PixelFormat.Undefined;
            int LengthMultipier = 0;
            int HalfSourceDataLength;
            //if (Width % 8 != 0)
            //{
            //    Width = (ushort)(Width + (8 - (Width % 8)));
            //}

            // Decode
            switch (Type)
            {
                case BitmapType.Texture_2D:
                    {
                        switch (Format)
                        {
                            case BitmapFormat.DXT1://Working
                                DecodedLOD = DXT.DecodeDXT1(Height, Width, DecodedLOD);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.DXT3://Working
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 8);
                                DecodedLOD = DXT.DecodeDXT23(Height, Width, DecodedLOD);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.DXT5://Working
                                DecodedLOD = DXT.DecodeDXT45(Height, Width, DecodedLOD);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.A8R8G8B8://Working?
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, Width, Height, Depth, 32, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 32);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.X8R8G8B8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, Width, Height, Depth, 32, true);
                                //Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 32);
                                pixelFormat = PixelFormat.Format32bppRgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.A4R4G4B4://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 32);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                int QuaterSourceDataLength = DecodedLOD.Length;
                                RecodedLOD = new byte[DecodedLOD.Length * 2];
                                for (int i = 0; i < QuaterSourceDataLength; i += 2)
                                {
                                    RecodedLOD[(i * 2) + 3] = (byte)(DecodedLOD[i] & 0XF0);
                                    RecodedLOD[(i * 2) + 2] = (byte)(DecodedLOD[i] & 0X0F);
                                    RecodedLOD[(i * 2) + 1] = (byte)(DecodedLOD[i + 1] & 0XF0);
                                    RecodedLOD[(i * 2)] = (byte)(DecodedLOD[i + 1] & 0X0F);
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.U8V8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppRgb;
                                DecodedLOD = DXT.DecodeU8V8((float)Width, (float)Height, DecodedLOD, MipMapCount, 16F);
                                RecodedLOD = new byte[DecodedLOD.Length * 2];

                                for (int i = 0; i < DecodedLOD.Length; i += 2)
                                {

                                    int a = i * 2;
                                    RecodedLOD[a + 3] = 0;//unused
                                    RecodedLOD[a + 2] = DXT.CalculateU8V8Color(DecodedLOD[i]);// > 127 ? (byte)(256 - DecodedLOD[i]) : (byte)DecodedLOD[i];//red
                                    RecodedLOD[a + 1] = DXT.CalculateU8V8Color(DecodedLOD[i + 1]);// > 127 ? (byte)(256 - DecodedLOD[i + 1]) : (byte)DecodedLOD[i + 1]; ;//green
                                    RecodedLOD[a] = 255;//CalculateUVW(DecodedLOD[i + 1], DecodedLOD[i]);
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.A1R5G5B5://Working+
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                LengthMultipier = 2;
                                pixelFormat = PixelFormat.Format16bppArgb1555;
                                break;
                            case BitmapFormat.R5G6B5://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                LengthMultipier = 2;
                                pixelFormat = PixelFormat.Format16bppRgb565;
                                break;
                            case BitmapFormat.A8Y8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 16);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                RecodedLOD = new byte[DecodedLOD.Length * 2];
                                for (int i = 0; i < DecodedLOD.Length; i += 2)
                                {
                                    RecodedLOD[i * 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 1] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 3] = DecodedLOD[i];
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.P8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = Color.H2Palette[DecodedLOD[i]].r;
                                    RecodedLOD[a + 1] = Color.H2Palette[DecodedLOD[i]].g;
                                    RecodedLOD[a + 2] = Color.H2Palette[DecodedLOD[i]].b;
                                    RecodedLOD[a + 3] = Color.H2Palette[DecodedLOD[i]].a;
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.Y8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = DecodedLOD[i];
                                    RecodedLOD[a + 1] = DecodedLOD[i];
                                    RecodedLOD[a + 2] = DecodedLOD[i];
                                    RecodedLOD[a + 3] = 255;
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            //case BitmapFormat.Lightmap://Working
                            //    if (Swizzle)
                            //        DecodedLOD = BitmapFunctions.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                            //    LengthMultipier = 4;
                            //    pixelFormat = PixelFormat.Format32bppArgb;

                            //    RecodedLOD = new byte[DecodedLOD.Length * 4];
                            //    LightMap.Palette Palette = Map.Sbsp[SBSPIndex, Tags.SearchType.Index].LightMap.Palettes[PaletteIndex];
                            //    for (int i = 0; i < DecodedLOD.Length; i++)
                            //    {
                            //        int a = i * 4;
                            //        RecodedLOD[a + 0] = Palette.Colors[DecodedLOD[i]].R;
                            //        RecodedLOD[a + 1] = Palette.Colors[DecodedLOD[i]].G;
                            //        RecodedLOD[a + 2] = Palette.Colors[DecodedLOD[i]].B;
                            //        RecodedLOD[a + 3] = Palette.Colors[DecodedLOD[i]].A;
                            //    }
                            //    DecodedLOD = RecodedLOD;
                            //    break;
                            case BitmapFormat.AY8:
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 8);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = (byte)(DecodedLOD[i]);
                                    RecodedLOD[a + 1] = (byte)(DecodedLOD[i]);
                                    RecodedLOD[a + 2] = (byte)(DecodedLOD[i]);
                                    RecodedLOD[a + 3] = (byte)(DecodedLOD[i]);
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.A8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 8);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = 255;
                                    RecodedLOD[a + 1] = 255;
                                    RecodedLOD[a + 2] = 255;
                                    RecodedLOD[a + 3] = DecodedLOD[i];
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            default:
                                return new System.Drawing.Bitmap(1, 1);

                        }
                        break;
                    }
                case BitmapType.Texture_3D:
                    {
                        switch (Format)
                        {
                            case BitmapFormat.A8Y8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 16);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                RecodedLOD = new byte[DecodedLOD.Length * 2];
                                for (int i = 0; i < DecodedLOD.Length; i += 2)
                                {
                                    RecodedLOD[i * 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 1] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 3] = DecodedLOD[i];
                                }
                                //Height;
                                Width *= Depth;

                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.AY8:
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 16);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = DecodedLOD[i] == 0 ? (byte)255 : (byte)0;
                                    RecodedLOD[a + 1] = (byte)255;
                                    RecodedLOD[a + 2] = (byte)255;
                                    RecodedLOD[a + 3] = (byte)255;
                                }
                                Width *= Depth;
                                DecodedLOD = RecodedLOD;
                                break;
                        }
                        break;
                    }
                case BitmapType.Cubemap:
                    {
                        switch (Format)
                        {
                            case BitmapFormat.DXT1://Working
                                DecodedLOD = DXT.DecodeCubemap(Height, DecodedLOD, 4, MipMapCount, 86);
                                DecodedLOD = DXT.DecodeDXT1(Height, Width * 6, DecodedLOD);
                                Height *= 6;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;

                            case BitmapFormat.X8R8G8B8://Working
                                int Padding1 = DXT.CalculateCubemapPadding(Width, Height, MipMapCount, 32, Size);
                                DecodedLOD = DXT.DecodeCubemap(Height, DecodedLOD, 32, MipMapCount, Padding1);
                                Height *= 6;
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, Width, Height, Depth, 32, true);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                        }
                        break;
                    }
                case BitmapType.Sprite:
                    {
                        break;
                    }
            }
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Width, Height);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, pixelFormat);
            Marshal.Copy(DecodedLOD, 0, data.Scan0, Width * Height * LengthMultipier);
            //File.WriteAllBytes("C:\\Users\\Grimdoomer\\Desktop\\foo.bin", DecodedLOD);
            bitmap.UnlockBits(data);
            return bitmap;
            #endregion
        }

        public MemoryStream ToStream()
        {
            #region Decode
            byte[] DecodedLOD = Buffer;
            byte[] RecodedLOD = new byte[0];
            PixelFormat pixelFormat = PixelFormat.Undefined;
            int LengthMultipier = 0;
            int HalfSourceDataLength;
            //if (Width % 8 != 0)
            //{
            //    Width = (ushort)(Width + (8 - (Width % 8)));
            //}

            // Decode
            switch (Type)
            {
                case BitmapType.Texture_2D:
                    {
                        switch (Format)
                        {
                            case BitmapFormat.DXT1://Working
                                DecodedLOD = DXT.DecodeDXT1(Height, Width, DecodedLOD);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.DXT3://Working
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 8);
                                DecodedLOD = DXT.DecodeDXT23(Height, Width, DecodedLOD);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.DXT5://Working
                                DecodedLOD = DXT.DecodeDXT45(Height, Width, DecodedLOD);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.A8R8G8B8://Working?
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, Width, Height, Depth, 32, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 32);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.X8R8G8B8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, Width, Height, Depth, 32, true);
                                //Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 32);
                                pixelFormat = PixelFormat.Format32bppRgb;
                                LengthMultipier = 4;
                                break;
                            case BitmapFormat.A4R4G4B4://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 32);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                int QuaterSourceDataLength = DecodedLOD.Length;
                                RecodedLOD = new byte[DecodedLOD.Length * 2];
                                for (int i = 0; i < QuaterSourceDataLength; i += 2)
                                {
                                    RecodedLOD[(i * 2) + 3] = (byte)(DecodedLOD[i] & 0XF0);
                                    RecodedLOD[(i * 2) + 2] = (byte)(DecodedLOD[i] & 0X0F);
                                    RecodedLOD[(i * 2) + 1] = (byte)(DecodedLOD[i + 1] & 0XF0);
                                    RecodedLOD[(i * 2)] = (byte)(DecodedLOD[i + 1] & 0X0F);
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.U8V8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppRgb;
                                DecodedLOD = DXT.DecodeU8V8((float)Width, (float)Height, DecodedLOD, MipMapCount, 16F);
                                RecodedLOD = new byte[DecodedLOD.Length * 2];

                                for (int i = 0; i < DecodedLOD.Length; i += 2)
                                {

                                    int a = i * 2;
                                    RecodedLOD[a + 3] = 0;//unused
                                    RecodedLOD[a + 2] = DXT.CalculateU8V8Color(DecodedLOD[i]);// > 127 ? (byte)(256 - DecodedLOD[i]) : (byte)DecodedLOD[i];//red
                                    RecodedLOD[a + 1] = DXT.CalculateU8V8Color(DecodedLOD[i + 1]);// > 127 ? (byte)(256 - DecodedLOD[i + 1]) : (byte)DecodedLOD[i + 1]; ;//green
                                    RecodedLOD[a] = 255;//CalculateUVW(DecodedLOD[i + 1], DecodedLOD[i]);
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.A1R5G5B5://Working+
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                LengthMultipier = 2;
                                pixelFormat = PixelFormat.Format16bppArgb1555;
                                break;
                            case BitmapFormat.R5G6B5://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                LengthMultipier = 2;
                                pixelFormat = PixelFormat.Format16bppRgb565;
                                break;
                            case BitmapFormat.A8Y8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 16);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                RecodedLOD = new byte[DecodedLOD.Length * 2];
                                for (int i = 0; i < DecodedLOD.Length; i += 2)
                                {
                                    RecodedLOD[i * 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 1] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 3] = DecodedLOD[i];
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.P8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = Color.H2Palette[DecodedLOD[i]].r;
                                    RecodedLOD[a + 1] = Color.H2Palette[DecodedLOD[i]].g;
                                    RecodedLOD[a + 2] = Color.H2Palette[DecodedLOD[i]].b;
                                    RecodedLOD[a + 3] = Color.H2Palette[DecodedLOD[i]].a;
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.Y8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = DecodedLOD[i];
                                    RecodedLOD[a + 1] = DecodedLOD[i];
                                    RecodedLOD[a + 2] = DecodedLOD[i];
                                    RecodedLOD[a + 3] = 255;
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            //case BitmapFormat.Lightmap://Working
                            //    if (Swizzle)
                            //        DecodedLOD = BitmapFunctions.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                            //    LengthMultipier = 4;
                            //    pixelFormat = PixelFormat.Format32bppArgb;

                            //    RecodedLOD = new byte[DecodedLOD.Length * 4];
                            //    LightMap.Palette Palette = Map.Sbsp[SBSPIndex, Tags.SearchType.Index].LightMap.Palettes[PaletteIndex];
                            //    for (int i = 0; i < DecodedLOD.Length; i++)
                            //    {
                            //        int a = i * 4;
                            //        RecodedLOD[a + 0] = Palette.Colors[DecodedLOD[i]].R;
                            //        RecodedLOD[a + 1] = Palette.Colors[DecodedLOD[i]].G;
                            //        RecodedLOD[a + 2] = Palette.Colors[DecodedLOD[i]].B;
                            //        RecodedLOD[a + 3] = Palette.Colors[DecodedLOD[i]].A;
                            //    }
                            //    DecodedLOD = RecodedLOD;
                            //    break;
                            case BitmapFormat.AY8:
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 8);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = (byte)(DecodedLOD[i]);
                                    RecodedLOD[a + 1] = (byte)(DecodedLOD[i]);
                                    RecodedLOD[a + 2] = (byte)(DecodedLOD[i]);
                                    RecodedLOD[a + 3] = (byte)(DecodedLOD[i]);
                                }
                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.A8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 8);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = 255;
                                    RecodedLOD[a + 1] = 255;
                                    RecodedLOD[a + 2] = 255;
                                    RecodedLOD[a + 3] = DecodedLOD[i];
                                }
                                DecodedLOD = RecodedLOD;
                                break;

                        }
                        break;
                    }
                case BitmapType.Texture_3D:
                    {
                        switch (Format)
                        {
                            case BitmapFormat.A8Y8://Working
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 16, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 16);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                RecodedLOD = new byte[DecodedLOD.Length * 2];
                                for (int i = 0; i < DecodedLOD.Length; i += 2)
                                {
                                    RecodedLOD[i * 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 1] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 2] = DecodedLOD[i + 1];
                                    RecodedLOD[(i * 2) + 3] = DecodedLOD[i];
                                }
                                //Height;
                                Width *= Depth;

                                DecodedLOD = RecodedLOD;
                                break;
                            case BitmapFormat.AY8:
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, PixelOffset, Width, Height, Depth, 8, true);
                                Width = (ushort)DXT.CalculateTrueDimensions(Height, Width, Format, Size, 16);
                                LengthMultipier = 4;
                                pixelFormat = PixelFormat.Format32bppArgb;

                                RecodedLOD = new byte[DecodedLOD.Length * 4];
                                for (int i = 0; i < DecodedLOD.Length; i++)
                                {
                                    int a = i * 4;
                                    RecodedLOD[a] = DecodedLOD[i] == 0 ? (byte)255 : (byte)0;
                                    RecodedLOD[a + 1] = (byte)255;
                                    RecodedLOD[a + 2] = (byte)255;
                                    RecodedLOD[a + 3] = (byte)255;
                                }
                                Width *= Depth;
                                DecodedLOD = RecodedLOD;
                                break;
                        }
                        break;
                    }
                case BitmapType.Cubemap:
                    {
                        switch (Format)
                        {
                            case BitmapFormat.DXT1://Working
                                DecodedLOD = DXT.DecodeCubemap(Height, DecodedLOD, 4, MipMapCount, 86);
                                DecodedLOD = DXT.DecodeDXT1(Height, Width * 6, DecodedLOD);
                                Height *= 6;
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;

                            case BitmapFormat.X8R8G8B8://Working
                                int Padding1 = DXT.CalculateCubemapPadding(Width, Height, MipMapCount, 32, Size);
                                DecodedLOD = DXT.DecodeCubemap(Height, DecodedLOD, 32, MipMapCount, Padding1);
                                Height *= 6;
                                if (Swizzled == true)
                                    DecodedLOD = Swizzling.Swizzle(DecodedLOD, Width, Height, Depth, 32, true);
                                pixelFormat = PixelFormat.Format32bppArgb;
                                LengthMultipier = 4;
                                break;
                        }
                        break;
                    }
                case BitmapType.Sprite:
                    {
                        break;
                    }
            }
            MemoryStream ms = new MemoryStream();
            ms.Write(DecodedLOD, 0, DecodedLOD.Length);
            return ms;
            #endregion
        }
    }
}
