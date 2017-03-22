using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Strings
{
    public class LongString : MetaNode
    {
        public const int FieldSize = 256;

        public string Value { get; set; }

        private StringType type;
        public StringType Type { get { return type; } }

        public LongString(string name, StringType type) 
            : base(name, FieldSize, FieldType.LongString, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to defualt values.
            Value = "";
            this.type = type;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            if (Type == StringType.Asci)
                Value = new string(br.ReadChars(256));
            else
                Value = br.ReadUnicodeString(128);
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            if (Type == StringType.Asci)
            {
                bw.Write(Value.ToCharArray());
                bw.Write(new byte[256 - Value.Length]);
            }
            else
                bw.WriteUnicodeString(Value, 128);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return Value;
        }

        public override void SetValue(object value, params object[] parameters)
        {
            this.Value = (string)value;
        }

        public override object Clone()
        {
            LongString s = new LongString(this.Name, this.Type);
            s.Value = Value;
            return s;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "LongString: " + Name + ", Type: " + Type.ToString() + ", String: " + Value;
        }

        #endregion
    }
}
