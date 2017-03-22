using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Color
{
    public class RealColorArgb : MetaNode
    {
        public const int FieldSize = 16;

        public float A { get; set; }
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public RealColorArgb(string name) 
            : base(name, FieldSize, FieldType.RealColorArgb, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            A = 0.0f;
            R = 0.0f;
            G = 0.0f;
            B = 0.0f;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            A = br.ReadSingle();
            R = br.ReadSingle();
            G = br.ReadSingle();
            B = br.ReadSingle();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(A);
            bw.Write(R);
            bw.Write(G);
            bw.Write(B);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return new object[] { A, R, G, B };
        }

        public override void SetValue(object value, params object[] parameters)
        {
            A = (float)((object[])value)[0];
            R = (float)((object[])value)[1];
            G = (float)((object[])value)[2];
            B = (float)((object[])value)[3];
        }

        public override object Clone()
        {
            RealColorArgb c = new RealColorArgb(this.Name);
            c.A = this.A;
            c.R = this.R;
            c.G = this.G;
            c.B = this.B;
            return c;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "RealColorArgb: " + Name + ", A: " + A.ToString() + ", R: " + R.ToString() + ", G: " + G.ToString() + ", B: " + B.ToString();
        }

        #endregion
    }
}
