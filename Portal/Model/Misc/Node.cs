using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloObjects;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D9;

namespace Portal.Model
{
    public class Node
    {
        public string Name;
        public short ParentNodeIndex, ChildNodeIndex, NextSiglingNodeIndex, ImportNodeIndex;
        public Vector3 DefaultTranslation, DefaultRotation;
        public Vector3 InverseFoward, InverseLeft, InverseUp, InversePosition;
        public float InverseScale, DistanceFromParent;

        public bool IsSelected = false;
        public Mesh mesh;
        public Vector3 Min, Max;
        public SlimDX.Direct3D9.Material m = new SlimDX.Direct3D9.Material();
        public SlimDX.Direct3D9.Material Clear = new SlimDX.Direct3D9.Material();
        public SlimDX.Direct3D9.Material Text = new SlimDX.Direct3D9.Material();
        public Mesh TextMesh;
        public System.Drawing.Font f;

        public void Read(IMetaNode[] mode)
        {
            Name = (string)mode[0].GetValue();
            ParentNodeIndex = (short)mode[1].GetValue();
            ChildNodeIndex = (short)mode[2].GetValue();
            NextSiglingNodeIndex = (short)mode[3].GetValue();
            ImportNodeIndex = (short)mode[4].GetValue();
            DefaultTranslation = new Vector3((float)mode[5].GetValue(), (float)mode[6].GetValue(), (float)mode[7].GetValue());
            DefaultRotation = new Vector3((float)mode[8].GetValue(), (float)mode[9].GetValue(), (float)mode[10].GetValue());
            InverseFoward = new Vector3((float)mode[11].GetValue(), (float)mode[12].GetValue(), (float)mode[13].GetValue());
            InverseLeft = new Vector3((float)mode[14].GetValue(), (float)mode[15].GetValue(), (float)mode[16].GetValue());
            InverseUp = new Vector3((float)mode[17].GetValue(), (float)mode[18].GetValue(), (float)mode[19].GetValue());
            InversePosition = new Vector3((float)mode[20].GetValue(), (float)mode[21].GetValue(), (float)mode[22].GetValue());
            InverseScale = (float)mode[23].GetValue();
            DistanceFromParent = (float)mode[24].GetValue();
        }

        public void Initialize(Device device)
        {
            // Create the Mesh
            mesh = Mesh.CreateSphere(device, 0.05f, 20, 20);
            DataStream ds = mesh.LockVertexBuffer(LockFlags.None);

            // Initialize the Font
            f = new System.Drawing.Font(FontFamily.GenericSerif, 0.5f);
            TextMesh = Mesh.CreateText(device, f, Name, 1.0f, 1.0f);
            Text.Ambient = Color.Red;

            // Calculate the bounding box values for Selection
            Min = new Vector3();
            Max = new Vector3();
            Geometry.ComputeBoundingBox(ds, mesh.VertexCount, mesh.VertexFormat, out Min, out Max);
            mesh.UnlockVertexBuffer();
        }

        public void Draw(Device device)
        {
            // Selection
            if (IsSelected)
                m.Emissive = System.Drawing.Color.FromArgb(150, 200, 100, 0);
            else
                m.Emissive = System.Drawing.Color.FromArgb(255, 0, 255, 0);

            // Draw
            device.Material = m;
            device.SetTransform(TransformState.World, Matrix.Translation(DefaultTranslation));
            mesh.DrawSubset(0);

            // Draw the Name
            //device.Material = Text;
            //device.Transform.World = Matrix.Translation(DefaultTranslation);
            //TextMesh.DrawSubset(0);
            device.Material = Clear;
        }

        public void Click(Device device, Vector3 Near, Vector3 Far)
        {
            // Set up out Ray
            Near = Vector3.Unproject(Near, device.Viewport, device.GetTransform(TransformState.Projection), device.GetTransform(TransformState.View), Matrix.Translation(DefaultTranslation));
            Far = Vector3.Unproject(Far, device.Viewport, device.GetTransform(TransformState.Projection), device.GetTransform(TransformState.View), Matrix.Translation(DefaultTranslation));
            Near = Vector3.Subtract(Near, Far);

            // Check if we have a collision
            //if (IsSelected && Geometry.BoxBoundProbe(Min, Max, Near, Far))
            //    IsSelected = false;
            //else
            //    IsSelected = Geometry.BoxBoundProbe(Min, Max, Near, Far);
        }
    }
}
