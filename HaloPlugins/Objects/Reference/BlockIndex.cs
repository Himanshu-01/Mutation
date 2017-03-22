using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Reference
{
    public class BlockIndex : MetaNode
    {
        private string blockName;
        /// <summary>
        /// Name of the tag_block this BlockIndex indexes.
        /// </summary>
        public string BlockName { get { return blockName; } }

        private string fieldName;
        /// <summary>
        /// Name of the field in the tag_block this BlockIndex points to.
        /// </summary>
        public string FieldName { get { return fieldName; } }

        private Type type;
        /// <summary>
        /// Data type of this BlockIndex field.
        /// </summary>
        public Type DataType { get { return type; } }

        /// <summary>
        /// Index value of this BlockIndex field.
        /// </summary>
        public object Index { get; set; }

        public BlockIndex(string name, string blockName, string fieldName, Type type)
            : base(name, (type == typeof(byte) ? 1 : type == typeof(short) ? 2 : 4), FieldType.BlockIndex, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            this.blockName = blockName;
            this.fieldName = fieldName;
            this.type = type;
            Index = 0;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            if (DataType == typeof(byte))
                Index = br.ReadByte();
            else if (DataType == typeof(short))
                Index = br.ReadInt16();
            else if (DataType == typeof(int))
                Index = br.ReadInt32();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            if (DataType == typeof(byte))
                bw.Write((byte)Index);
            else if (DataType == typeof(short))
                bw.Write((short)Index);
            else if (DataType == typeof(int))
                bw.Write((int)Index);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            return Index;
        }

        public override void SetValue(object value, params object[] parameters)
        {
            Index = value;
        }

        public override object Clone()
        {
            BlockIndex b = new BlockIndex(this.Name, this.BlockName, this.FieldName, this.DataType);
            b.Index = this.Index;
            return b;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "BlockIndex: " + Name + ", BlockName: " + BlockName + ", FieldName: " + FieldName + ", BlockIndex: " + Index.ToString();
        }

        #endregion
    }
}
