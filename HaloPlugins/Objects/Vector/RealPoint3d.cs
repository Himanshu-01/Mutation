using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    public class RealPoint3d : MetaNode
    {
        public const int FieldSize = 12;

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public RealPoint3d(string name) 
            : base(name, FieldSize, FieldType.RealPoint3d, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
            Z = br.ReadSingle();
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
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return new object[] { X, Y, Z };
        }

        public override void SetValue(object value, params object[] parameters)
        {
            X = (float)((object[])value)[0];
            Y = (float)((object[])value)[1];
            Z = (float)((object[])value)[2];
        }

        public override object Clone()
        {
            RealPoint3d p = new RealPoint3d(this.Name);
            p.X = this.X;
            p.Y = this.Y;
            p.Z = this.Z;
            return p;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "RealPoint3d: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString() + ", Z: " + Z.ToString();
        }

        #endregion
    }
}
