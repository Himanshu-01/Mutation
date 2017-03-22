using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Strings
{
    public class Tag : MetaNode
    {
        public const int FieldSize = 4;

        public string TagType { get; set; }

        public Tag(string name) 
            : base(name, FieldSize, FieldType.Tag, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            TagType = "";
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            TagType = Microsoft.VisualBasic.Strings.StrReverse(new string(br.ReadChars(4)));
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(Microsoft.VisualBasic.Strings.StrReverse(TagType).ToCharArray());
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return TagType;
        }

        public override void SetValue(object value, params object[] parameters)
        {
            TagType = (string)value;
        }

        public override object Clone()
        {
            Tag t = new Tag(this.Name);
            t.TagType = this.TagType;
            return t;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "Tag: " + Name + ", Tag: " + TagType;
        }

        #endregion
    }
}
