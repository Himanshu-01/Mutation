using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Color
{
    public class RealColorRgb : MetaNode
    {
        public const int FieldSize = 12;

        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public RealColorRgb(string name) 
            : base(name, FieldSize, FieldType.RealColorRgb, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            R = 0.0f;
            G = 0.0f;
            B = 0.0f;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
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
            return new object[] { R, G, B };
        }

        public override void SetValue(object value, params object[] parameters)
        {
            R = (float)((object[])value)[0];
            G = (float)((object[])value)[1];
            B = (float)((object[])value)[2];
        }

        public override object Clone()
        {
            RealColorRgb c = new RealColorRgb(this.Name);
            c.R = this.R;
            c.G = this.G;
            c.B = this.B;
            return c;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "RealColorRgb: " + Name + ", R: " + R.ToString() + ", G: " + G.ToString() + ", B: " + B.ToString();
        }

        #endregion
    }
}
