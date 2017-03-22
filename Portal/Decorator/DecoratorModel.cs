using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using SlimDX;
using SlimDX.Direct3D9;
using Portal.Decorator;
using System.IO;

namespace Portal
{
    public struct DecoratorVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 Tuv;
        public static readonly VertexFormat Format = VertexFormat.PositionNormal | VertexFormat.Texture1;
    }

    public class DecoratorModel : RawBlock, IRenderable
    {
        public Shader[] Shaders;
        public float MinLightScale, MaxLightScale;
        public Class[] Classes;
        public Decorator.Model[] Models;

        public string BlockStart;
        public int Unknown;

        public short[] Indices;
        public Vector3[] Vertices;
        public Vector3[] Normals;
        public Vector3[] Tangents;
        public Vector3[] Binormals;
        public Vector2[] UVs;

        public Mesh mesh;

        public void Read(IMetaNode[] decr, string TagPath)
        {
            // Shaders
            Shaders = new Shader[((H2XTagBlock)decr[0]).GetChunkCount()];
            for (int i = 0; i < Shaders.Length; i++)
            {
                ((H2XTagBlock)decr[0]).ChangeIndex(i);
                IMetaNode[] nodes = (IMetaNode[])decr[0].GetValue();
                Shaders[i] = new Shader((string)nodes[0].GetValue());
            }

            // Lighting
            MinLightScale = (float)decr[1].GetValue();
            MaxLightScale = (float)decr[2].GetValue();

            // Classes
            Classes = new Class[((H2XTagBlock)decr[3]).GetChunkCount()];
            for (int i = 0; i < Classes.Length; i++)
            {
                ((H2XTagBlock)decr[3]).ChangeIndex(i);
                Classes[i] = new Class();
                Classes[i].Read((IMetaNode[])decr[3].GetValue());
            }

            // Models
            Models = new Portal.Decorator.Model[((H2XTagBlock)decr[4]).GetChunkCount()];
            for (int i = 0; i < Models.Length; i++)
            {
                ((H2XTagBlock)decr[4]).ChangeIndex(i);
                Models[i] = new Portal.Decorator.Model();
                Models[i].Read((IMetaNode[])decr[4].GetValue());
            }

            // Indices
            Indices = new short[((H2XTagBlock)decr[6]).GetChunkCount()];
            for (int i = 0; i < Indices.Length; i++)
            {
                ((H2XTagBlock)decr[6]).ChangeIndex(i);
                IMetaNode[] nodes = (IMetaNode[])decr[6].GetValue();
                Indices[i] = (short)nodes[0].GetValue();
            }

            // Raw Info
            Offset = (int)decr[8].GetValue();
            Size = (int)decr[9].GetValue();
            HeaderSize = (int)decr[10].GetValue();
            DataSize = (int)decr[11].GetValue();
            base.Read(decr, 12);

            // Open Raw - Internal Only for Now
            BinaryReader br = null;
            if (Offset < 0x2FFFFFFF)
                br = new BinaryReader(new FileStream(TagPath.Replace(".decorator", ".decoratorraw"), FileMode.Open, FileAccess.Read, FileShare.Read));

            // Header
            BlockStart = new string(br.ReadChars(4));
            br.BaseStream.Position += 4;
            Unknown = br.ReadInt32();

            // Render Data
            if (Resources.Length != 0)
            {
                br.BaseStream.Position = Offset + 8 + HeaderSize + Resources[0].Offset;
                int VertCount = Resources[0].Size / 32;
                Vertices = new Vector3[VertCount];
                Normals = new Vector3[VertCount];
                Tangents = new Vector3[VertCount];
                Binormals = new Vector3[VertCount];
                UVs = new Vector2[VertCount];
                for (int i = 0; i < VertCount; i++)
                {
                    Vertices[i] = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    Normals[i] = Decompress(br.ReadInt32());
                    Tangents[i] = Decompress(br.ReadInt32());
                    Binormals[i] = Decompress(br.ReadInt32());
                    UVs[i] = new Vector2(br.ReadSingle(), br.ReadSingle());
                }
            }
        }

        public void Initialize(Device device)
        {
            // Initialize the Mesh
            if (Resources.Length != 0)
            {
                mesh = new Mesh(device, Indices.Length / 3, Vertices.Length, MeshFlags.Managed, DecoratorVertex.Format);
                DataStream VertDS = mesh.LockVertexBuffer(LockFlags.None);
                for (int x = 0; x < Vertices.Length; x++)
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
                mesh.UnlockVertexBuffer();

                // Initialize the Texture
                Shaders[0].InitializeTextures(device);
            }
        }

        public void Draw(Device device)
        {
            // Check
            if (Resources.Length != 0)
            {
                // Initialize Scene
                device.SetTexture(0, Shaders[0].MainTexture);
                device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
                device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
                device.SetTextureStageState(0, TextureStage.ColorArg2, TextureArgument.Current);
                device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
                device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);

                // Draw
                device.VertexFormat = DecoratorVertex.Format;
                //device.RenderState.CullMode = Cull.None;
                //device.RenderState.FillMode = FillMode.Solid;
                device.Indices = mesh.IndexBuffer;
                device.SetStreamSource(0, mesh.VertexBuffer, 0, 32);
                //device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, Vertices.Length, 0, Vertices.Length);
                mesh.DrawSubset(0);
            }
        }

        public void Click(Device device, int x, int y)
        {
            
        }

        private float Decompress(short value, float Min, float Max)
        {
            double percent = (value + 32768f) / 65535f;
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
    }
}
