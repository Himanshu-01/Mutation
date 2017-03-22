using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    public class RealPlane3d : MetaNode
    {
        public const int FieldSize = 16;

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }

        public RealPlane3d(string name) 
            : base(name, FieldSize, FieldType.RealPlane3d, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
            W = 0.0f;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
            W = br.ReadSingle();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
            bw.Write(Z);
            bw.Write(W);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return new object[] { X, Y, Z, W };
        }

        public override void SetValue(object value, params object[] parameters)
        {
            X = (float)((object[])value)[0];
            Y = (float)((object[])value)[1];
            Z = (float)((object[])value)[2];
            W = (float)((object[])value)[3];
        }

        public override object Clone()
        {
            RealPlane3d v = new RealPlane3d(this.Name);
            v.X = this.X;
            v.Y = this.Y;
            v.Z = this.Z;
            v.W = this.W;
            return v;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "RealPlane3d: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString() + ", Z: " + Z.ToString() + ", W: " + W.ToString();
        }

        #endregion
    }
}
