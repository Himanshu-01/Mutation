using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Data
{
    public class Flags : MetaNode
    {
        /// <summary>
        /// Gets or sets the selected bitmask flags.
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

        public Flags(string name, string[] options, int bitCount) 
            : base(name, (bitCount / 8), FieldType.Flags, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
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

        public override void  SetValue(object value, params object[] parameters)
        {
            if (BitCount == 8)
                this.Value = (byte)value;
            else if (BitCount == 16)
                this.Value = (short)value;
            else if (BitCount == 32)
                this.Value = (int)value;
        }

        public override object Clone()
        {
            Flags b = new Flags(this.Name, this.Options, this.BitCount);
            b.Value = Value;
            return b;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets or sets the boolean value of the bit at the specified index.
        /// </summary>
        /// <param name="index">Index of the bit to get or set.</param>
        /// <returns>Value of the bit at index.</returns>
        public bool this[int index]
        {
            get { return ((Value >> index) & 1) == 1; }
            set { Value &= (int)(0xFFFFFFFF ^ (1 << index)); }
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "Flags: " + Name + ", BitCount: " + BitCount.ToString();
        }

        #endregion
    }
}
