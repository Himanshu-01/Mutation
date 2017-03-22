using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Data
{
    public class Value : MetaNode
    {
        /// <summary>
        /// Gets or sets the data value of this Value field.
        /// </summary>
        public object DataValue { get; set; }

        private Type dataType;
        /// <summary>
        /// Gets the data type of this Value field.
        /// </summary>
        public Type DataType { get { return dataType; } }

        public Value(string name, Type type) : base(name, 0, FieldType.Value, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            this.dataType = type;
            DataValue = 0;

            // Set field size.
            if (dataType == typeof(byte))
                base.fieldSize = 1;
            else if (dataType == typeof(short) || dataType == typeof(ushort))
                base.fieldSize = 2;
            else if (dataType == typeof(int) || dataType == typeof(uint) || dataType == typeof(float))
                base.fieldSize = 4;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            if (dataType == typeof(byte))
                DataValue = br.ReadByte();
            else if (dataType == typeof(short))
                DataValue = br.ReadInt16();
            else if (dataType == typeof(ushort))
                DataValue = br.ReadUInt16();
            else if (dataType == typeof(int))
                DataValue = br.ReadInt32();
            else if (dataType == typeof(uint))
                DataValue = br.ReadUInt32();
            else if (dataType == typeof(float))
                DataValue = br.ReadInt32(); // ReadSingle();
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            if (dataType == typeof(byte))
                bw.Write(byte.Parse(DataValue.ToString()));
            else if (dataType == typeof(short))
                bw.Write(short.Parse(DataValue.ToString()));
            else if (dataType == typeof(ushort))
                bw.Write(ushort.Parse(DataValue.ToString()));
            else if (dataType == typeof(int))
                bw.Write(int.Parse(DataValue.ToString()));
            else if (dataType == typeof(uint))
                bw.Write(uint.Parse(DataValue.ToString()));
            else if (dataType == typeof(float))
                bw.Write(int.Parse(DataValue.ToString()));  //ms.bw.Write(float.Parse(Val.ToString()));
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            //if (dataType == typeof(byte))
            //    return byte.Parse(DataValue.ToString());
            //else if (dataType == typeof(short))
            //    return short.Parse(DataValue.ToString());
            //else if (dataType == typeof(ushort))
            //    return ushort.Parse(DataValue.ToString());
            //else if (dataType == typeof(int))
            //    return int.Parse(DataValue.ToString());
            //else if (dataType == typeof(uint))
            //    return uint.Parse(DataValue.ToString());
            //else if (dataType == typeof(float))
            //    return int.Parse(DataValue.ToString()); //return float.Parse(Val.ToString());
            //else
            //    return null;
            return DataValue;
        }

        public override void SetValue(object value, params object[] parameters)
        {
            this.DataValue = value;
        }

        public override object Clone()
        {
            Value v = new Value(this.Name, this.dataType);
            v.DataValue = this.DataValue;
            return v;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "Value: " + Name.ToString() + ", Type: " + dataType.ToString() + ", Value: " + DataValue.ToString();
        }

        #endregion
    }
}
