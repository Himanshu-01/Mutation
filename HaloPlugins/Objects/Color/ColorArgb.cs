using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Color
{
    public class ColorArgb : MetaNode
    {
        public const int FieldSize = 4;

        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public ColorArgb(string name) 
            : base(name, FieldSize, FieldType.ColorArgb, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to defualt values.
            A = 0;
            R = 0;
            G = 0;
            B = 0;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            A = br.ReadByte();
            R = br.ReadByte();
            G = br.ReadByte();
            B = br.ReadByte();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(new byte[] { A, R, G, B });
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
            A = (byte)((object[])value)[0];
            R = (byte)((object[])value)[1];
            G = (byte)((object[])value)[2];
            B = (byte)((object[])value)[3];
        }

        public override object Clone()
        {
            ColorArgb c = new ColorArgb(this.Name);
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
            return "ColorArgb: " + Name + ", A: " + A.ToString() + ", R: " + R.ToString() + ", G: " + G.ToString() + ", B: " + B.ToString();
        }

        #endregion
    }
}
