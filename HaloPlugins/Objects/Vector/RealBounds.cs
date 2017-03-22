using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    public class RealBounds : MetaNode
    {
        public const int FieldSize = 8;

        public float To { get; set; }
        public float From { get; set; }

        public RealBounds(string name) 
            : base(name, FieldSize, FieldType.RealBounds, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            To = 0.0f;
            From = 0.0f;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            To = br.ReadSingle();
            From = br.ReadSingle();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(To);
            bw.Write(From);
        }

        public override void  Write(EndianWriter bw, int magic)
        {
 	         Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return new object[] { To, From };
        }

        public override void SetValue(object value, params object[] parameters)
        {
            To = (float)((object[])value)[0];
            From = (float)((object[])value)[1];
        }

        public override object Clone()
        {
            RealBounds s = new RealBounds(this.Name);
            s.To = this.To;
            s.From = this.From;
            return s;
        }

        #endregion

        #region Object Memory

        public override string ToString()
        {
            return "RealBounds: " + Name + ", To: " + To.ToString() + ", From: " + From.ToString();
        }

        #endregion
    }
}
