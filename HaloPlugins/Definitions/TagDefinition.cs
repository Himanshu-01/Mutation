using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IO;
using HaloPlugins.Objects;
using Blam.Objects.DataTypes;

namespace HaloPlugins
{
    public class TagDefinition : MetaNode, IDisposable
    {
        // Tag Definition Fields
        public List<MetaNode> Fields = new List<MetaNode>();
        public string Engine = "Halo2Xbox", DefaultExtension = "";

        private string groupTag = "";
        /// <summary>
        /// Gets the group tag id of the tag layout.
        /// </summary>
        public string GroupTag { get { return groupTag; } }

        private int headerSize = 0;
        /// <summary>
        /// Gets the header size of the tag layout.
        /// </summary>
        public int HeaderSize { get { return headerSize; } }

        private string absolutePath = "";
        /// <summary>
        /// Gets the file path of the tag file relative to the project directory.
        /// </summary>
        public string AbsolutePath { get { return absolutePath; } }

        private datum_index datum = new datum_index(datum_index.NONE);
        /// <summary>
        /// Gets the unique datum handle for this tag instance.
        /// </summary>
        public datum_index Datum { get { return datum; } }

        // Raw Data Fields
        public RawDefinition RawDef = null;

        // Disposable fields
        bool IsDisposed = false;

        public TagDefinition(string CompiledClass, string DefaultExtension, int HeaderSize) 
            : base(DefaultExtension, HeaderSize, FieldType.None, EngineManager.HaloEngine.Halo2)
        {
            this.groupTag = CompiledClass;
            this.DefaultExtension = DefaultExtension;
            this.headerSize = HeaderSize;
        }

        public TagDefinition(string CompiledClass, string DefaultExtension, int HeaderSize, RawDefinition RawDefinition)
            : base(DefaultExtension, HeaderSize, FieldType.None, EngineManager.HaloEngine.Halo2)
        {
            this.groupTag = CompiledClass;
            this.DefaultExtension = DefaultExtension;
            this.headerSize = HeaderSize;
            this.RawDef = RawDefinition;
        }

        ~TagDefinition()
        {
            this.Dispose(false);
        }

        public void AssignToInstance(datum_index datum, string absolutePath)
        {
            // Save the datum index and absolute path to bind this tag definition
            // to a tag in memory.
            this.datum = datum;
            this.absolutePath = absolutePath;
        }

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            // Header Values
            this.datum = new datum_index(br.ReadUInt32());
            int TagCount = br.ReadInt32();
            //int StringCount = br.ReadInt32();
            int DataSize = br.ReadInt32();
            int RawOffset = br.ReadInt32();
            this.absolutePath = br.ReadString();

            // Init lists
            EngineManager.Engines["Halo2Xbox"]["TagIds"] = new Dictionary<string, int>(TagCount);
            //EngineManager.Engines["Halo2Xbox"]["StringIds"] = new List<string>(StringCount);

            // Read Strings
            br.BaseStream.Position = DataSize;
            for (int i = 0; i < TagCount; i++)
                ((Dictionary<string, int>)EngineManager.Engines["Halo2Xbox"]["TagIds"]).Add(br.ReadNullTerminatingString(), i);

            // StringIds
            //for (int i = 0; i < StringCount; i++)
            //    ((List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"]).Add(br.ReadNullTerminatingString());

            // Fields
            br.BaseStream.Position = 512;
            for (int i = 0; i < Fields.Count; i++)
                Fields[i].Read(br);

            // Read Raw
            br.BaseStream.Position = RawOffset;
            if (RawDef != null)
            {
                RawDef = (RawDefinition)Activator.CreateInstance(RawDef.GetType(), new object[] { });
                RawDef.Owner = this;
                RawDef.Read(br);
            }
        }

        public override void Read(EndianReader br, int Magic)
        {
            // Fields
            for (int i = 0; i < Fields.Count; i++)
                Fields[i].Read(br, Magic);

            // Read Raw
            if (RawDef != null) RawDef = (RawDefinition)Activator.CreateInstance(RawDef.GetType(), new object[] { this, br });
        }

        public override void Write(EndianWriter bw)
        {
            // Header
            bw.Write(new byte[512]);
            bw.Write(new byte[HeaderSize]);
            bw.BaseStream.Position = 512;

            // Init lists
            EngineManager.Engines["Halo2Xbox"]["TagIds"] = new Dictionary<string, int>();
            //EngineManager.Engines["Halo2Xbox"]["StringIds"] = new List<string>();

            // Fields
            for (int i = 0; i < Fields.Count; i++)
                Fields[i].Write(bw);

            // Update Header
            bw.BaseStream.Position = 0;
            bw.Write((uint)this.datum);
            bw.Write(((Dictionary<string, int>)EngineManager.Engines["Halo2Xbox"]["TagIds"]).Count);
            //bw.Write(((List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"]).Count);
            bw.Write((int)bw.BaseStream.Length);
            bw.BaseStream.Position = bw.BaseStream.Length;

            // Write Tag Reference Names
            for (int i = 0; i < ((Dictionary<string, int>)EngineManager.Engines["Halo2Xbox"]["TagIds"]).Count; i++)
                bw.WriteNullTerminatingString(((Dictionary<string, int>)EngineManager.Engines["Halo2Xbox"]["TagIds"]).Keys.ElementAt(i));

            // Write String Reference Names
            //for (int i = 0; i < ((List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"]).Count; i++)
            //    bw.WriteNullTerminatingString(((List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"])[i]);

            // Finish Header
            bw.BaseStream.Position = 16;
            bw.Write((int)bw.BaseStream.Length);
            bw.Write(AbsolutePath);

            // Write Raw
            bw.BaseStream.Position = bw.BaseStream.Length;
            if (RawDef != null) RawDef.Write(bw);
        }

        public override object GetValue(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object value, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override void Write(EndianWriter bw, int Magic)
        {
            // Write Header
            int Offset = (int)bw.BaseStream.Position;
            bw.Write(new byte[HeaderSize]);
            bw.BaseStream.Position = Offset;

            // Compile Fields
            for (int i = 0; i < Fields.Count; i++)
                Fields[i].Write(bw, Magic);
        }

        #endregion

        #region ICloneable Members

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Indexers

        public MetaNode this[int Index]
        {
            get 
            { 
#if (SANITY_CHECK)
                // Output warning
                System.Diagnostics.StackTrace stack = new System.Diagnostics.StackTrace();
                Console.WriteLine("[HaloPlugins.TagDefinition.this[int] Depricated accessor called from " 
                    + stack.GetFrame(1).GetMethod().Name);
#endif

                // Return field
                return Fields[Index]; 
            }
            set 
            {
#if (SANITY_CHECK)
                // Output warning
                System.Diagnostics.StackTrace stack = new System.Diagnostics.StackTrace();
                Console.WriteLine("[HaloPlugins.TagDefinition.this[int] Depricated accessor called from "
                    + stack.GetFrame(1).GetMethod().Name);
#endif

                // Set new value
                Fields[Index] = value; 
            }
        }

        public MetaNode this[string Name]
        {
            get
            {
                // Loop through the fields list
                bool found = false;
                MetaNode node = null;
                for (int i = 0; i < Fields.Count; i++)
                {
#if (PLUGIN_SANITY_CHECK)
                    // Check field name
                    if (Fields[i].GetName().Equals(Name))
                    {
                        // Check if found is true
                        if (found == true)
                            Console.WriteLine("[HaloPlugins.TagDefinition.this[string] Meta object " +
                        this.GetName() + " has duplicate child nodes with name \"" + Name + "\"");
                    }
                    else
                    {
                        // Save meta node
                        node = Fields[i];
                        found = true;
                    }
#else
                    // Check field name
                    if (Fields[i].Name.Equals(Name))
                        return Fields[i];
#endif
                }

                // Done
                return node;
            }
            set
            {
                // Loop through the fields list
                bool found = false;
                for (int i = 0; i < Fields.Count; i++)
                {
#if (PLUGIN_SANITY_CHECK)
                    // Check field name
                    if (Fields[i].GetName().Equals(Name))
                    {
                        // Check if found is true
                        if (found == true)
                            Console.WriteLine("[HaloPlugins.TagDefinition.this[string] Meta object " +
                        this.GetName() + " has duplicate child nodes with name \"" + Name + "\"");
                    }
                    else
                    {
                        // Set new value
                        Fields[i] = value;
                        found = true;
                    }
#else
                    // Check field name
                    if (Fields[i].Name.Equals(Name))
                        Fields[i] = value;
#endif
                }
            }
        }

        #endregion

        #region IDisposable Members

        protected virtual void Dispose(bool IsDisposing)
        {
            if (!IsDisposed)
            {
                // Dispose managed resources
                if (IsDisposing)
                {
                    RawDef.Dispose();
                    Fields.Clear();
                }

                // Dispose unmanaged resources
                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
