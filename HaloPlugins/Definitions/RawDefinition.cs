using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IO;

namespace HaloPlugins
{
    public class RawDefinition : IDisposable
    {
        // Parent
        public TagDefinition Owner;

        // Stream Info
        protected List<MemoryStream> Streams = new List<MemoryStream>();
        protected List<string> Names = new List<string>();

        // Disposable info
        bool IsDisposed = false;

        public RawDefinition() { }
        public RawDefinition(TagDefinition TagDef) 
        {
            this.Owner = TagDef;
        }

        ~RawDefinition()
        {
            this.Dispose(false);
        }

        public void AddStream(string StreamName)
        {
            // Add Name
            Names.Add(StreamName);

            // Add New Stream
            Streams.Add(new MemoryStream());
        }

        public virtual void Read(EndianReader br) 
        { 
            // Read Header
            int StreamCount = br.ReadInt32();
            for (int i = 0; i < StreamCount; i++)
            {
                // Init Stream
                Streams.Add(new MemoryStream(br.ReadInt32()));

                // Init Name
                Names.Add(new string(br.ReadChars(16)).Replace("\0", ""));
            }

            // Buffer Streams
            for (int i = 0; i < StreamCount; i++)
            {
                Streams[i].Write(br.ReadBytes((int)Streams[i].Capacity), 0, (int)Streams[i].Capacity);
            }
        }

        public virtual void Write(EndianWriter bw) 
        { 
            // Write Header
            bw.Write(Streams.Count);
            for (int i = 0; i < Streams.Count; i++)
            {
                // Write Stream Info
                bw.Write((int)Streams[i].Length);
                bw.Write(Names[i].ToCharArray());
                bw.Write(new byte[16 - Names[i].Length]);
            }

            // Write Stream Data
            for (int i = 0; i < Streams.Count; i++)
            {
                bw.Write(Streams[i].ToArray());
            }
        }

        public virtual void FlushStream(string StreamName, int Ptr) { }

        #region Indexers

        public MemoryStream this[string StreamName]
        {
            get
            {
                for (int i = 0; i < Streams.Count; i++)
                {
                    if (Names[i] == StreamName)
                        return Streams[i];
                }
                return null;
            }
            set
            {
                for (int i = 0; i < Streams.Count; i++)
                {
                    if (Names[i] == StreamName)
                        Streams[i] = value;
                }
            }
        }

        #endregion

        #region IDisposable Members

        protected virtual void Dispose(bool IsDisposing)
        {
            if (!IsDisposed)
            {
                // Dispose unmanaged resources
                for (int i = 0; i < Streams.Count; i++)
                {
                    Streams[i].Close();
                    Streams[i].Dispose();
                }

                // Dispose managed resources
                if (IsDisposing)
                {
                    Owner = null;
                    Names.Clear();
                    Streams.Clear();
                }

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
