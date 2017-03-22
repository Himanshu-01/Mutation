using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Data
{
    public class Enum : MetaNode
    {
        /// <summary>
        /// Gets or sets the selected enum index.
        /// </summary>
        public int Value { get; set; }

        private int bitCount;
        /// <summary>
        /// Gets the size of the enum in bits.
        /// </summary>
        public int BitCount { get { return bitCount; } }

        private string[] options;
        /// <summary>
        /// Gets the enum options list.
        /// </summary>
        public string[] Options { get { return options; } }

        public Enum(string name, string[] options, int bitCount) 
            : base(name, (bitCount / 8), FieldType.Enum, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to defualt values.
            Value = 0;
            this.options = options;
            this.bitCount = bitCount;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            if (BitCount == 8)
                Value = br.ReadByte();
            else if (BitCount == 16)
                Value = br.ReadInt16();
            else if (BitCount == 32)
                Value = br.ReadInt32();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            if (BitCount == 8)
                bw.Write((byte)Value);
            else if (BitCount == 16)
                bw.Write((short)Value);
            else if (BitCount == 32)
                bw.Write(Value);
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
            this.Value = (int)value;
        }

        public override object Clone()
        {
            Enum e = new Enum(this.Name, this.Options, this.BitCount);
            e.Value = Value;
            return e;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "Enum: " + Name + ", BitCount: " + BitCount.ToString();
        }

        #endregion
    }
}
