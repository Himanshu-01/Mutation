using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Model
{
    public enum Format
    {
        Rigid = 0,
        RigidBoned = 1,
        Skinned = 2
    }

    public struct ModelVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public float Tu1, Tv1;
        //public float Tu2, Tv2;
        public static readonly VertexFormat Format = VertexFormat.Position | VertexFormat.Normal | VertexFormat.Texture1;
    }

    public class Section : RawBlock
    {
        // Meta Values
        public short VertCount, FaceCount;
        public Format Format;

        // Raw Data
        public string BlockStart;
        public int PartCount;
        public int IndiceCount;
        public int BoneCount;

        public PartInfo[] Parts;
        public short[] Indices;

        public Vector3[] Vertices;
        public short[] PrimaryBones, SecondaryBones;
        public byte[] PrimaryWeight, SecondaryWeight;

        public Vector2[] UVs;
        public Vector3[] Normals, Bitangents, Tangents;

        public byte[] Bones;

        public Mesh Mesh;
        public VertexBuffer VertexBuffer;
        public IndexBuffer IndexBuffer;

        public Vector3 Min = new Vector3();
        public Vector3 Max = new Vector3();
        public bool Selected = false;
        public SlimDX.Direct3D9.Material m = new SlimDX.Direct3D9.Material();

        public void Read(IMetaNode[] mode, string TagPath, CompressionInfo Info)
        {
            // Read Meta
            Format = (Format)(int)mode[0].GetValue();
            VertCount = (short)mode[1].GetValue();
            FaceCount = (short)mode[2].GetValue();
            Offset = (int)mode[26].GetValue();
            Size = (int)mode[27].GetValue();
            HeaderSize = (int)mode[28].GetValue();
            DataSize = (int)mode[29].GetValue();
            base.Read(mode, 30);

            // Open Raw
            BinaryReader br = null;
            if (Offset < 0x2FFFFFFF && Offset >= 0)
                br = new BinaryReader(new FileStream(TagPath.Replace(".render_model", ".model1raw"), FileMode.Open, FileAccess.Read, FileShare.Read));
            else
                br = RawData.OpenMap(ref Offset);

            // Header
            br.BaseStream.Position = Offset;
            BlockStart = new string(br.ReadChars(4));
            br.BaseStream.Position += 4;
            PartCount = br.ReadInt32();
            br.BaseStream.Position = 40;
            //IndiceCount = br.ReadInt32();
            br.BaseStream.Position = 108;
            BoneCount = br.ReadInt32();
            
            // Read all the Resource Blocks
            for (int i = 0; i < Resources.Length; i++)
            {
                switch (Resources[i].PrimaryId)
                {
                    #region Part Info

                    case 0:
                        {
                            Parts = new PartInfo[PartCount];
                            br.BaseStream.Position = Offset + 8 + HeaderSize + Resources[i].Offset;
                            for (int x = 0; x < Parts.Length; x++)
                            {
                                Parts[x] = new PartInfo();
                                Parts[x].Read(br);
                                IndiceCount += Parts[x].IndiceCount;
                            }
                            break;
                        }

                    #endregion

                    #region Bone Map

                    case 1:
                        {
                            Bones = new byte[BoneCount];
                            if (BoneCount > 1)
                            {
                                br.BaseStream.Position = Offset + 8 + HeaderSize + Resources[i].Offset;
                                for (int x = 0; x < BoneCount; x++)
                                    Bones[x] = br.ReadByte();
                            }
                            break;
                        }

                    #endregion

                    #region Indices

                    case 32:
                        {
                            Indices = new short[IndiceCount];
                            br.BaseStream.Position = Offset + 8 + HeaderSize + Resources[i].Offset;
                            for (int x = 0; x < Indices.Length; x++)
                                Indices[x] = br.ReadInt16();

                            if (Indices.Length != 0 && FaceCount * 3 != IndiceCount)
                                Indices = DecompressIndices(Indices, 0, Indices.Length);
                            break;
                        }

                    #endregion

                    #region Mesh Data

                    case 56:
                        {
                            switch (Resources[i].SecondaryId)
                            {
                                #region Vertices

                                case 0:
                                    {
                                        // Verticies
                                        Vertices = new Vector3[VertCount];
                                        br.BaseStream.Position = Offset + 8 + HeaderSize + Resources[i].Offset;

                                        // Initialize other Arrays
                                        if (BoneCount > 1)
                                        {
                                            if (Format == Format.RigidBoned)
                                                PrimaryBones = new short[VertCount];
                                            else if (Format == Format.Skinned)
                                            {
                                                PrimaryBones = new short[VertCount];
                                                SecondaryBones = new short[VertCount];
                                                PrimaryWeight = new byte[VertCount];
                                                SecondaryWeight = new byte[VertCount];
                                            }
                                        }

                                        // Read
                                        for (int ii = 0; ii < VertCount; ii++)
                                        {
                                            // Decompress Vert
                                            float x = Decompress(br.ReadInt16(), Info.XMin, Info.XMax);
                                            float y = Decompress(br.ReadInt16(), Info.YMin, Info.YMax);
                                            float z = Decompress(br.ReadInt16(), Info.ZMin, Info.ZMax);
                                            Vertices[ii] = new Vector3(x, y, z);

                                            // Bone map
                                            if (BoneCount > 1)
                                            {
                                                if (Format == Format.RigidBoned)
                                                    PrimaryBones[ii] = br.ReadInt16();
                                                else if (Format == Format.Skinned)
                                                {
                                                    PrimaryBones[ii] = br.ReadInt16();
                                                    SecondaryBones[ii] = br.ReadInt16();
                                                    PrimaryWeight[ii] = br.ReadByte();
                                                    SecondaryWeight[ii] = br.ReadByte();
                                                }
                                            }
                                        }
                                        break;
                                    }

                                #endregion

                                #region UVs

                                case 1:
                                    {
                                        UVs = new Vector2[VertCount];
                                        br.BaseStream.Position = Offset + 8 + HeaderSize + Resources[i].Offset;
                                        for (int x = 0; x < VertCount; x++)
                                        {
                                            float u = Decompress(br.ReadInt16(), Info.UMin, Info.UMax);
                                            float v = Decompress(br.ReadInt16(), Info.VMin, Info.VMax);
                                            UVs[x] = new Vector2(u, v);
                                        }
                                        break;
                                    }

                                #endregion

                                #region Normals, Bitangents, and Tangents

                                case 2:
                                    {
                                        Normals = new Vector3[VertCount];
                                        Bitangents = new Vector3[VertCount];
                                        Tangents = new Vector3[VertCount];
                                        br.BaseStream.Position = Offset + 8 + HeaderSize + Resources[i].Offset;
                                        for (int x = 0; x < VertCount; x++)
                                        {
                                            Normals[x] = Decompress(br.ReadInt32());
                                            Bitangents[x] = Decompress(br.ReadInt32());
                                            Tangents[x] = Decompress(br.ReadInt32());
                                        }
                                        break;
                                    }

                                #endregion
                            }
                            break;
                        }

                    #endregion
                }
            }
        }

        public void Initialize(Device device, Material[] Materials)
        {
            // Initialize Each Shader Group
            for (int i = 0; i < PartCount; i++)
            {
                // Calculate Info
                Parts[i].VertStart = Parts[i].IndiceStart / 3;
                Parts[i].VertCount = Parts[i].IndiceCount / 3;

                // Initialize VertexBuffer
                VertexBuffer = new VertexBuffer(device, VertCount * 32, Usage.WriteOnly, ModelVertex.Format, Pool.Managed);
                DataStream VertDS = VertexBuffer.Lock(0, VertCount * 32, LockFlags.None);
                for (int x = 0; x < VertCount; x++)
                {
                    // Position
                    VertDS.Write(BitConverter.GetBytes(Vertices[x].X), 0, 4);
                    VertDS.Write(BitConverter.GetBytes(Vertices[x].Y), 0, 4);
                    VertDS.Write(BitConverter.GetBytes(Vertices[x].Z), 0, 4);

                    // Normal
                    VertDS.Write(BitConverter.GetBytes(Normals[x].X), 0, 4);
                    VertDS.Write(BitConverter.GetBytes(Normals[x].Y), 0, 4);
                    VertDS.Write(BitConverter.GetBytes(Normals[x].Z), 0, 4);

                    // Textcord
                    VertDS.Write(BitConverter.GetBytes(UVs[x].X), 0, 4);
                    VertDS.Write(BitConverter.GetBytes(UVs[x].Y), 0, 4);
                }
                VertexBuffer.Unlock();

                // Initialize IndexBuffer
                IndexBuffer = new IndexBuffer(device, Indices.Length * 2, Usage.WriteOnly, Pool.Managed, true);
                DataStream IndexDS = IndexBuffer.Lock(0, Indices.Length * 2, LockFlags.None);
                for (int x = 0; x < Indices.Length; x++)
                {
                    IndexDS.Write(Indices[x]);
                }
                IndexBuffer.Unlock();

                // Initialize Texture
                //Materials[Parts[i].ShaderIndex].Shader.InitializeTextures(device);
            }
        }

        public void Draw(Device device, Material[] Materials)
        {
            // Texture Options
            //device.RenderState.FillMode = FillMode.Solid;
            //device.RenderState.CullMode = Cull.Clockwise;

            device.SetStreamSource(0, VertexBuffer, 0, 32);
            device.VertexFormat = ModelVertex.Format;
            device.Indices = IndexBuffer;

            // Material
            //if (Selected)
            //    m.Emissive = Color.FromArgb(255, 50, 0, 0);
            //else
            //    m.Emissive = Color.FromArgb(0);

            // Draw each Part
            for (int i = 0; i < PartCount; i++)
            {
                // Set Alpha Blending
                if (Materials[Parts[i].ShaderIndex].Shader.Alpha == AlphaType.AlphaBlend)
                {
                    device.SetRenderState(RenderState.AlphaBlendEnable, true);
                    device.SetRenderState(RenderState.AlphaTestEnable, false);
                }
                else if (Materials[Parts[i].ShaderIndex].Shader.Alpha == AlphaType.AlphaTest)
                {
                    device.SetRenderState(RenderState.AlphaBlendEnable, false);
                    device.SetRenderState(RenderState.AlphaTestEnable, true);
                }
                else
                {
                    device.SetRenderState(RenderState.AlphaBlendEnable, false);
                    device.SetRenderState(RenderState.AlphaTestEnable, false);
                }

                // Set Texture
                //device.SetTexture(0, Materials[Parts[i].ShaderIndex].Shader.MainTexture);
                device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
                device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
                device.SetTextureStageState(0, TextureStage.ColorArg2, TextureArgument.Current);
                device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
                device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
                device.SetRenderState(RenderState.FillMode, FillMode.Solid);

                // Draw
                if (FaceCount * 3 != IndiceCount)
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, VertCount, Parts[i].IndiceStart, Parts[i].IndiceCount);
                else
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, VertCount, Parts[i].IndiceStart, Parts[i].IndiceCount / 3);
            }
        }

        public void Click(Device device, Vector3 Near, Vector3 Far)
        {
            //for (int i = 0; i < PartCount; i++)
            //{
            //    // Set up out Ray
            //    Near.Unproject(device.Viewport, device.Transform.Projection, device.Transform.View, Matrix.Translation(0, 0, 0));
            //    Far.Unproject(device.Viewport, device.Transform.Projection, device.Transform.View, Matrix.Translation(0, 0, 0));
            //    Near.Subtract(Far);

            //    // Check if we have a collision
            //    if (Selected && Geometry.BoxBoundProbe(Min, Max, Near, Far))
            //        Selected = false;
            //    else
            //        Selected = Geometry.BoxBoundProbe(Min, Max, Near, Far);
            //}
        }

        private float Decompress(short value, float Min, float Max)
        {
            double percent = (value + 32768F) / 65535F;
            double result = (percent * ((double)Max - (double)Min)) + (double)Min;
            return (float)result;
        }

        private Vector3 Decompress(int Value)
        {
            byte xSign = (byte)((Value >> 10) & 0x1);
            byte ySign = (byte)((Value >> 21) & 0x1);
            byte zSign = (byte)((Value >> 31) & 0x1);
            float x = (int)(Value & 0x3FF);
            x /= (float)0x3FF;
            float y = (float)((Value >> 11) & 0x3FF);
            y /= (float)0x3FF;
            float z = (float)((Value >> 22) & 0x1FF);
            z /= (float)0x1FF;
            z = zSign == 1 ? (z - 1) : z;
            y = ySign == 1 ? (y - 1) : y;
            x = xSign == 1 ? (x - 1) : x;
            return new Vector3(x, y, z);
        }

        private short[] FixIndices()
        {
            List<short> Fixed = new List<short>();
            bool CullingFlip = false;
            int i = 0;
            while (i < IndiceCount)
            {
                // Pull out the Indices
                short v1 = Indices[i];
                short v2 = Indices[i + 1];
                short v3 = Indices[i + 2];

                // If non are the same, then we have a face!
                if (v1 != v2 && v2 != v3 && v3 != v1 && !CullingFlip)
                {
                    Fixed.Add(v1);
                    Fixed.Add(v2);
                    Fixed.Add(v3);
                    CullingFlip = true;
                    i += 1;
                }
                else if (v1 != v2 && v2 != v3 && v3 != v1 && CullingFlip)
                {
                    Fixed.Add(v1);
                    Fixed.Add(v3);
                    Fixed.Add(v2);
                    CullingFlip = false;
                    i += 1;
                }
                else
                {
                    i += 1;
                    CullingFlip = CullingFlip ? false : true;
                }
            }
            return Fixed.ToArray();
        }

        public static short[] DecompressIndices(short[] indices, int start, int count)
        {
            bool dir = false;
            short tempx;
            short tempy;
            short tempz;
            short[] shite = new short[50000];
            int m = start;
            int s = 0;
            do
            {


                tempx = indices[m];
                tempy = indices[m + 1];
                tempz = indices[m + 2];


                if (tempx != tempy && tempx != tempz && tempy != tempz)
                {
                    if (dir == false)
                    {
                        shite[s] = tempx;
                        shite[s + 1] = tempy;
                        shite[s + 2] = tempz;
                        s += 3;

                        dir = true;
                    }
                    else
                    {
                        shite[s] = tempx;
                        shite[s + 1] = tempz;
                        shite[s + 2] = tempy;
                        s += 3;
                        dir = false;
                    }
                    m += 1;
                }
                else
                {
                    if (dir == true) { dir = false; }
                    else { dir = true; }

                    m += 1;
                }
            }
            while (m < start + count - 2);
            short[] uncompressedindices = new short[s];
            Array.Copy(shite, uncompressedindices, s);
            return uncompressedindices;
        }
    }
}
