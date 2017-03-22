using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Strings
{
    public enum StringType
    {
        Asci,
        Unicode
    }

    public class ShortString : MetaNode
    {
        public string Value { get; set; }

        private StringType type;
        public StringType Type { get { return type; } }

        public ShortString(string name, StringType type) 
            : base(name, (type == StringType.Asci ? 32 : 64), FieldType.ShortString, EngineManager.HaloEngine.Neutral)
        {
            // Initialize fields to default values.
            Value = "";
            this.type = type;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            if (Type == StringType.Asci)
                Value = new string(br.ReadChars(32));
            else
                Value = br.ReadUnicodeString(32);
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
                bw.Write(new byte[32 - Value.Length]);
            }
            else
                bw.WriteUnicodeString(Value, 32);
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
            ShortString s = new ShortString(this.Name, this.Type);
            s.Value = Value;
            return s;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "ShortString: " + ", Type: " + Type.ToString() + ", String: " + Value;
        }

        #endregion
    }
}
