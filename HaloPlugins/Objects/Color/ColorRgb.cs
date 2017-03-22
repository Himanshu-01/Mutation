using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Color
{
    public class ColorRgb : MetaNode
    {
        public const int FieldSize = 4;

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public ColorRgb(string name) 
            : base(name, FieldSize, FieldType.ColorRgb, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to defualt values.
            R = 0;
            G = 0;
            B = 0;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            R = br.ReadByte();
            G = br.ReadByte();
            B = br.ReadByte();
            br.BaseStream.Position++;
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(new byte[] { R, G, B });
            bw.BaseStream.Position++;
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
            R = (byte)((object[])value)[0];
            G = (byte)((object[])value)[1];
            B = (byte)((object[])value)[2];
        }

        public override object Clone()
        {
            ColorRgb c = new ColorRgb(this.Name);
            c.R = this.R;
            c.G = this.G;
            c.B = this.B;
            return c;
        }

        #endregion

        #region Object Memebers

        public override string ToString()
        {
            return "ColorRgb: " + Name + ", R: " + R.ToString() + ", G: " + G.ToString() + ", B: " + B.ToString();
        }

        #endregion
    }
}
