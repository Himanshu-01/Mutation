using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Reference
{
    public class InfoBlock : MetaNode
    {
        private string info;
        /// <summary>
        /// Information about upcoming fields.
        /// </summary>
        public string Info { get { return info; } }

        public InfoBlock(string info) 
            : base("InfoBlock", 0, FieldType.InfoBlock, EngineManager.HaloEngine.Neutral)
        {
            // Set fields to default values.
            this.info = info;
        }

        #region MetaNode Members

        public override void Read(EndianReader br) { }

        public override void Read(EndianReader br, int Magic) { }

        public override void Write(EndianWriter bw) { }

        public override void Write(EndianWriter bw, int magic) { }

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
            return new InfoBlock(this.Info);
        }

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "InfoBlock: " + Info;
        }

        #endregion
    }
}
