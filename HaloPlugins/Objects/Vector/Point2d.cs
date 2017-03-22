using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    public class Point2d : MetaNode
    {
        public const int FieldSize = 4;

        public short X { get; set; }
        public short Y { get; set; }

        public Point2d(string name) 
            : base(name, FieldSize, FieldType.Point2d, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to defualt values.
            X = 0;
            Y = 0;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            X = br.ReadInt16();
            Y = br.ReadInt16();
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
            X = (short)((object[])value)[0];
            Y = (short)((object[])value)[1];
        }

        public override object Clone()
        {
            Point2d p = new Point2d(this.Name);
            p.X = this.X;
            p.Y = this.Y;
            return p;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "Point2d: " + Name + ", X: " + X.ToString() + ", Y: " + Y.ToString();
        }

        #endregion
    }
}
