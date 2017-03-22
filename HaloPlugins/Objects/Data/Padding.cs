using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Data
{
    public class Padding : MetaNode
    {
        private int size;
        /// <summary>
        /// Length of the padding field.
        /// </summary>
        public int Size { get { return size; } }

        /// <summary>
        /// Gets or sets the padding data.
        /// </summary>
        public byte[] Data { get; set; }

        public Padding(int size) 
            : base("Padding", size, FieldType.Padding, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            this.size = size;
            Data = new byte[Size];
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            Data = br.ReadBytes(Size);
        }

        public override void Read(EndianReader br, int Magic)
        {
            Read(br);
        }

        public override void Write(EndianWriter bw)
        {
            bw.Write(Data);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object value, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            Padding u = new Padding(Size);
            Array.Copy(Data, u.Data, Size);
            return u;
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return string.Format("Padding: Size={0} Data={{1}}", Size, Data);
        }

        #endregion
    }
}
