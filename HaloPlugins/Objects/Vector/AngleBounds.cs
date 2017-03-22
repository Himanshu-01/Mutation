using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Vector
{
    public class AngleBounds : MetaNode
    {
        public const int FieldSize = 8;

        public float To { get; set; }
        public float From { get; set; }

        public AngleBounds(string name) 
            : base(name, FieldSize, FieldType.AngleBounds, EngineManager.HaloEngine.Neutral)
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

        public override void Write(EndianWriter bw, int magic)
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
            AngleBounds s = new AngleBounds(this.Name);
            s.To = this.To;
            s.From = this.From;
            return s;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "AngleBounds: " + Name + ", To: " + To.ToString() + ", From: " + From.ToString();
        }

        #endregion
    }
}
