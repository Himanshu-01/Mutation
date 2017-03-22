using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Global;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using IO;

namespace HaloPlugins.Objects.Reference
{
    public class TagReference : MetaNode
    {
        int Index = -1, Id = -1;
        string groupTag = "", Tag = "Null Reference";
        public string GroupTag { get { return this.groupTag; } }

        public TagReference(string name, string groupTag) 
            : base(name, 8, FieldType.TagReference, EngineManager.HaloEngine.Halo2)
        {
            this.groupTag = groupTag;
        }

        public TagReference(string name, string groupTag, EngineManager.HaloEngine engine)
            : base(name, (engine == EngineManager.HaloEngine.Halo2 ? 8 : 16), FieldType.TagReference, engine)
        {
            this.groupTag = groupTag;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            groupTag = Microsoft.VisualBasic.Strings.StrReverse(new string(Encoding.ASCII.GetChars(br.ReadBytes(4))));
            if (Engine != EngineManager.HaloEngine.Halo2)
                br.BaseStream.Position += 8;
            Index = Id = br.ReadInt32();
            if (Index != -1)
                Tag = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Keys.ElementAt(Index);
        }

        public override void Read(EndianReader br, int Magic)
        {
            groupTag = Microsoft.VisualBasic.Strings.StrReverse(new string(Encoding.ASCII.GetChars(br.ReadBytes(4))));
            if (Engine != EngineManager.HaloEngine.Halo2)
                br.BaseStream.Position += 8;
            Index = Id = br.ReadInt32();

            // Add
            if (Index != -1)
            {
                Tag = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Keys.ElementAt(Index & 0xFFFF);
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

                // Get class
                groupTag = EngineManager.Engines[Global.Application.Instance.Project.Engine]["Class", Tag.Substring(Tag.LastIndexOf(".") + 1)];
            }
            else
                Index = -1;

            if (groupTag.Length != 4)
                throw new Exception("this is bad");

            bw.Write(Microsoft.VisualBasic.Strings.StrReverse(groupTag).ToCharArray());
            if (Engine != EngineManager.HaloEngine.Halo2)
                bw.Write((long)0);
            bw.Write(Index);
        }

        public override object GetValue(params object[] parameters)
        {
            return Tag;
        }

        public override void SetValue(object value, params object[] parameters)
        {
            Tag = (string)parameters[0];
        }

        public int GetSize()
        {
            return Engine == EngineManager.HaloEngine.Halo2 ? 8 : 16;
        }

        public override void Write(EndianWriter bw, int Magic)
        {
            if (Tag != "Null Reference")
                Id = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"])[Tag];
            else
                Id = -1;

            if (groupTag.Length != 4)
                throw new Exception("this is bad");

            bw.Write(Microsoft.VisualBasic.Strings.StrReverse(groupTag).ToCharArray());
            if (Engine != EngineManager.HaloEngine.Halo2)
                bw.Write((long)0);
            bw.Write(Id);
        }

        //public override void Decompile(EndianWriter bw, int Magic)
        //{
        //    // Add
        //    if (Tag != "Null Reference")
        //    {
        //        try { Index = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"])[Tag]; }
        //        catch
        //        {
        //            Index = ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Count;
        //            ((Dictionary<string, int>)EngineManager.Engines[Global.Application.Instance.Project.Engine]["TagIds"]).Add(Tag, Index);
        //        }

        //        // Get class
        //        groupTag = EngineManager.Engines[Global.Application.Instance.Project.Engine]["Class", Tag.Substring(Tag.LastIndexOf(".") + 1)];
        //    }
        //    else
        //        Index = -1;

        //    if (groupTag.Length != 4)
        //        throw new Exception("this is bad");

        //    bw.Write(Microsoft.VisualBasic.Strings.StrReverse(groupTag).ToCharArray());
        //    if (Engine != EngineManager.HaloEngine.Halo2)
        //        bw.Write((long)0);
        //    bw.Write(Index);
        //}

        public string GetName()
        {
            return Name;
        }

        #endregion

        #region ICloneable Members

        public override object Clone()
        {
            TagReference t = new TagReference(this.Name, this.groupTag, this.Engine);
            t.Index = Index;
            t.Tag = Tag;
            t.Id = Id;
            return t;
        }

        #endregion

        #region TagReferences Members

        public int GetTagIndex()
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
            return "TagReference: " + Name + ", Class: " + groupTag + ", Index: " + Index.ToString() + ", Tag: " + Tag;
        }

        #endregion
    }
}
