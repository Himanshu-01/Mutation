using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal
{
    public struct ClothVertex
    {
        public Vector3 Position;
        public Vector2 Tuv;
        public static readonly VertexFormat Format = VertexFormat.Position | VertexFormat.Texture1;
    }

    public class Cloth : IRenderable
    {
        public string ShaderPath;
        public Shader Shader;
        public Vector3[] Vertices;
        public Vector2[] UVs;
        public short[] Indices;

        public Mesh mesh;

        public void Read(IMetaNode[] clwd, string TagPath)
        {
            // Shader
            ShaderPath = (string)clwd[2].GetValue();
            Shader = new Shader(ShaderPath);

            // Vertices
            Vertices = new Vector3[((H2XTagBlock)clwd[15]).GetChunkCount()];
            UVs = new Vector2[Vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
            {
                ((H2XTagBlock)clwd[15]).ChangeIndex(i);
                IMetaNode[] nodes = (IMetaNode[])clwd[15].GetValue();
                Vertices[i] = new Vector3((float)nodes[0].GetValue(), (float)nodes[1].GetValue(), (float)nodes[2].GetValue());
                UVs[i] = new Vector2((float)nodes[3].GetValue(), (float)nodes[4].GetValue());
            }

            // Indices
            Indices = new short[((H2XTagBlock)clwd[16]).GetChunkCount()];
            for (int i = 0; i < Indices.Length; i++)
            {
                ((H2XTagBlock)clwd[16]).ChangeIndex(i);
                IMetaNode[] node = (IMetaNode[])clwd[16].GetValue();
                Indices[i] = (short)node[0].GetValue();
            }
        }

        public void Initialize(Device device)
        {
            // Initialize Mesh
            mesh = new Mesh(device, Vertices.Length * 3, Vertices.Length, MeshFlags.Managed, ClothVertex.Format);
            ClothVertex[] verts = new ClothVertex[Vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
            {
                verts[i].Position = Vertices[i];
                verts[i].Tuv = UVs[i];
            }

            // Initialize VertexBuffer
            DataStream VertDS = mesh.LockVertexBuffer(LockFlags.None);
            for (int x = 0; x < Vertices.Length; x++)
            {
                // Position
                VertDS.Write(BitConverter.GetBytes(Vertices[x].X), 0, 4);
                VertDS.Write(BitConverter.GetBytes(Vertices[x].Y), 0, 4);
                VertDS.Write(BitConverter.GetBytes(Vertices[x].Z), 0, 4);

                // Textcord
                VertDS.Write(BitConverter.GetBytes(UVs[x].X), 0, 4);
                VertDS.Write(BitConverter.GetBytes(UVs[x].Y), 0, 4);
            }
            mesh.UnlockVertexBuffer();

            // Initialize IndexBuffer
            DataStream IndexDS = mesh.LockIndexBuffer(LockFlags.None);
            for (int x = 0; x < Indices.Length; x++)
            {
                IndexDS.Write(Indices[x]);
            }
            mesh.UnlockIndexBuffer();

            // Initialize Texture
            Shader.InitializeTextures(device);
        }

        public void Draw(Device device)
        {
            // Texture Options
            device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
            device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
            device.SetTextureStageState(0, TextureStage.ColorArg2, TextureArgument.Current);
            device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
            device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
            device.SetRenderState(RenderState.FillMode, FillMode.Solid);
            //device.RenderState.CullMode = Cull.None;

            // Draw
            device.SetTexture(0, Shader.BumpTexture);
            //mesh.DrawSubset(0);
            device.VertexFormat = ClothVertex.Format;
            device.Indices = mesh.IndexBuffer;
            device.SetStreamSource(0, mesh.VertexBuffer, 0, 20);
            device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, Vertices.Length, 0, Indices.Length);
        }

        public void Click(Device device, int x, int y)
        {
        }
    }
}
