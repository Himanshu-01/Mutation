using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Global;
using System.Windows.Forms;
using IO;

namespace HaloPlugins.Objects.Reference
{
    public class TagIndex : MetaNode
    {
        public const int FieldSize = 4;

        string groupTag = "", Tag = "Null Reference";
        int Index = -1, Id = -1;

        public TagIndex(string name, string groupTag) 
            : base(name, FieldSize, FieldType.DatumIndex, EngineManager.HaloEngine.Neutral)
        {
            this.groupTag = groupTag;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            Tag = (Index = Id = br.ReadInt32()) != -1 ? ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Keys.ElementAt(Index) : "Null Reference";
        }

        public override void Read(EndianReader br, int Magic)
        {
            Index = Id = br.ReadInt32();

            // Add
            if (Index != -1)
            {
                Tag = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Keys.ElementAt(Id & 0xFFFF);
            }
        }

        public override void Write(EndianWriter bw)
        {
            // Add
            if (Tag != "Null Reference")
            {
                try { Index = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"])[Tag]; }
                catch
                {
                    Index = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Count;
                    ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Add(Tag, Index);
                }
            }
            else
                Index = -1;

            bw.Write(Index);
        }

        public override object  GetValue(params object[] parameters)
        {
            return Tag;
        }

        public override void SetValue(object value, params object[] parameters)
        {
            Tag = (string)parameters[0];
        }

        public int GetSize()
        {
            return 4;
        }

        public override void Write(EndianWriter bw, int Magic)
        {
            bw.Write(Index != -1 ? ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"])[Tag] : Index);
        }

        public string GetName()
        {
            return Name;
        }

        #endregion

        #region ICloneable Members

        public override object Clone()
        {
            TagIndex i = new TagIndex(this.Name, this.groupTag);
            i.Index = this.Index;
            i.Tag = this.Tag;
            i.Id = this.Id;
            return i;
        }

        #endregion

        #region TagIndex Members

        public int GetId()
        {
            return Id & 0xFFFF;
        }

        public string GetFileName()
        {
            return Tag;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return "TagIndex: " + Name + ", Class: " + groupTag + ", Index: " + Index.ToString() + ", Tag: " + Tag;
        }

        #endregion
    }
}
