using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Collision
{
    public struct CollisionVertex
    {
        public Vector3 Position;
        //public int Diffuse;
        public static readonly VertexFormat Format = VertexFormat.Position;// | VertexFormat.Specular;
    }

    public class Bsp
    {
        public int RootNode;
        public Plane[] Planes;
        public Surface[] Surfaces;
        public Edge[] Edges;
        public Vector3[] Vertices;

        public short[] Indices;
        public VertexBuffer vb;
        public IndexBuffer ib;

        public void Read(IMetaNode[] coll)
        {
            // RootNode
            RootNode = (int)coll[0].GetValue();

            // Planes
            Planes = new Plane[((H2XTagBlock)coll[2]).GetChunkCount()];
            for (int i = 0; i < Planes.Length; i++)
            {
                ((H2XTagBlock)coll[2]).ChangeIndex(i);
                IMetaNode[] nodes = (IMetaNode[])coll[2].GetValue();
                Planes[i] = new Plane((float)nodes[0].GetValue(), (float)nodes[1].GetValue(), (float)nodes[2].GetValue(), (float)nodes[3].GetValue());
            }

            // Surfaces
            Surfaces = new Surface[((H2XTagBlock)coll[6]).GetChunkCount()];
            for (int i = 0; i < Surfaces.Length; i++)
            {
                ((H2XTagBlock)coll[6]).ChangeIndex(i);
                Surfaces[i] = new Surface();
                Surfaces[i].Read((IMetaNode[])coll[6].GetValue());
            }

            // Edges
            Edges = new Edge[((H2XTagBlock)coll[7]).GetChunkCount()];
            for (int i = 0; i < Edges.Length; i++)
            {
                ((H2XTagBlock)coll[7]).ChangeIndex(i);
                Edges[i] = new Edge();
                Edges[i].Read((IMetaNode[])coll[7].GetValue());
            }

            // Vertices
            Vertices = new Vector3[((H2XTagBlock)coll[8]).GetChunkCount()];
            for (int i = 0; i < Vertices.Length; i++)
            {
                ((H2XTagBlock)coll[8]).ChangeIndex(i);
                IMetaNode[] nodes = (IMetaNode[])coll[8].GetValue();
                Vertices[i] = new Vector3((float)nodes[0].GetValue(), (float)nodes[1].GetValue(), (float)nodes[2].GetValue());
            }
        }

        public void Initialize(Device device)
        {
            // Initialize VertexBuffer
            vb = new VertexBuffer(device, Vertices.Length * 12, Usage.WriteOnly, CollisionVertex.Format, Pool.Managed);
            DataStream VertDS = vb.Lock(0, Vertices.Length * 12, LockFlags.None);
            for (int x = 0; x < Vertices.Length; x++)
            {
                // Position
                VertDS.Write(BitConverter.GetBytes(Vertices[x].X), 0, 4);
                VertDS.Write(BitConverter.GetBytes(Vertices[x].Y), 0, 4);
                VertDS.Write(BitConverter.GetBytes(Vertices[x].Z), 0, 4);
            }
            vb.Unlock();

            // Initialize IndexBuffer
            Indices = new short[Edges.Length * 3];
            for (int i = 0; i < Edges.Length; i++)
            {
                Indices[i * 3] = Edges[i].StartVert;
                Indices[(i * 3) + 1] = Edges[i].EndVert;
                Indices[(i * 3) + 2] = Edges[i].EndVert;
            }

            ib = new IndexBuffer(device, Indices.Length, Usage.WriteOnly, Pool.Managed, true);
            DataStream IndexDS = ib.Lock(0, Indices.Length * 2, LockFlags.None);
            for (int x = 0; x < Indices.Length; x++)
            {
                IndexDS.Write(Indices[x]);
            }
            ib.Unlock();
        }

        public void Draw(Device device)
        {
            // Initialize Scene
            device.VertexFormat = CollisionVertex.Format;
            device.SetRenderState(RenderState.CullMode, Cull.None);
            device.SetRenderState(RenderState.FillMode, FillMode.Wireframe);

            // Draw
            device.SetStreamSource(0, vb, 0, 12);
            device.Indices = ib;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Vertices.Length, 0, Indices.Length);
        }
    }
}
