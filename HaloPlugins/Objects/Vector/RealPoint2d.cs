using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    public class RealPoint2d : MetaNode
    {
        public const int FieldSize = 8;

        public float X { get; set; }
        public float Y { get; set; }

        public RealPoint2d(string name) 
            : base(name, FieldSize, FieldType.RealPoint2d, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            X = 0.0f;
            Y = 0.0f;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            X = br.ReadSingle();
            Y = br.ReadSingle();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return new object[] { X, Y };
        }

        public override void SetValue(object value, params object[] parameters)
        {
            X = (float)((object[])value)[0];
            Y = (float)((object[])value)[1];
        }

        public override object Clone()
        {
            RealPoint2d p = new RealPoint2d(this.Name);
            p.X = this.X;
            p.Y = this.Y;
            return p;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "RealPoint2d: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString();
        }

        #endregion
    }
}
