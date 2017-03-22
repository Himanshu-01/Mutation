using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    public class RealAngle2d : MetaNode
    {
        public const int FieldSize = 8;

        public float X { get; set; } // yaw
        public float Y { get; set; } // pitch

        public RealAngle2d(string name) 
            : base(name, FieldSize, FieldType.RealAngle2d, EngineManager.HaloEngine.Neutral)
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
            RealAngle2d a = new RealAngle2d(this.Name);
            a.X = this.X;
            a.Y = this.Y;
            return a;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "RealAngle2d: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString();
        }

        #endregion
    }
}
