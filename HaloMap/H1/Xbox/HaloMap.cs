using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IO;
using HaloPlugins;
using System.IO;
//using Xceed.Compression.Formats;
using Global;

namespace HaloMap.H1.Xbox
{
    #region Enums

    public enum MapType : int
    {
        Singleplayer = 0,
        Multiplayer = 1,
        Mainmenu = 2
    }

    #endregion

    #region Header Structure

    [StructLayout(LayoutKind.Explicit, Size = 2048)]
    public struct MapHeader
    {
        [FieldOffset(0)]
        public int Magic;
        [FieldOffset(4)]
        public int Version;
        [FieldOffset(8)]
        public int FileSize;
        [FieldOffset(12)]
        public int TagCount;
        [FieldOffset(16)]
        public int IndexOffset;
        [FieldOffset(20)]
        public int IndexSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        [FieldOffset(32)]
        public string MapName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        [FieldOffset(64)]
        public string MapBuild;
        [FieldOffset(96)]
        public MapType Type;
        [FieldOffset(100)]
        public int Checksum;
        [FieldOffset(1044)]
        public int Footer;
    }

    #endregion

    #region TagIndex Structure

    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct TagIndexHeader
    {
        [FieldOffset(0)]
        public int VirtualTagIndexAddress;
        [FieldOffset(4)]
        public int TagHierarchyCount;
        [FieldOffset(8)]
        public int VirtualTagStartAddress;
        [FieldOffset(12)]
        public int ScenarioId;
        [FieldOffset(16)]
        public int GlobalsId;
        [FieldOffset(20)]
        public int Unknown;
        [FieldOffset(24)]
        public int TagCount;
        [FieldOffset(28)]
        public int TagIndexMagic;
    }

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct TagEntry
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        [FieldOffset(0)]
        public char[] Class;
        [FieldOffset(4)]
        public int Id;
        [FieldOffset(8)]
        public int VirtualTagAddress;
        [FieldOffset(12)]
        public int Size;
    }

    #endregion

    public class HaloMap : IHaloMap
    {
        #region Fields

        // Reader/Writer
        MemoryStream ms;
        EndianReader br;
        EndianWriter bw;

        // Map Structure
        MapHeader Header;
        TagIndexHeader TagIndexHeader;
        TagEntry[] Tags;
        string[] StringIds;
        string[] TagPaths;

        // Virtual-To-Physical Magics
        int MemoryMagic, MetaMagic;

        // GC Handles, I'm a big boy now!
        GCHandle HeaderPtr, TagIndexHeaderPtr, TagsPtr;

        #endregion

        #region Constructor

        public HaloMap() { }

        public HaloMap(string MapPath)
        {
            // Intialize Memory Stream
            ms = new MemoryStream(File.ReadAllBytes(MapPath));

            // Initialize Reader/Writer
            br = new EndianReader(Endianness.Little, ms);
            bw = new EndianWriter(Endianness.Little, ms);
        }

        #endregion

        #region IHaloMap Members

        public void Initialize()
        {
            // Parse Header
            HeaderPtr = GCHandle.Alloc(br.ReadBytes(2048), GCHandleType.Pinned);
            Header = (MapHeader)Marshal.PtrToStructure(HeaderPtr.AddrOfPinnedObject(), typeof(MapHeader));

            // Read Compressed Data
            ms.Position = 2048;
            byte[] Src = new byte[(int)ms.Length - 2048];
            ms.Read(Src, 0, Src.Length);

            // Save Header
            ms.Position = 0;
            byte[] HeaderData = new byte[2048];
            ms.Read(HeaderData, 0, 2048);

            // Decompress
            //byte[] Dst = ZLibCompressedStream.Decompress(Src);

            //// Rebuild Memory Stream
            //ms = new MemoryStream(2048 + Dst.Length);
            //ms.Write(HeaderData, 0, 2048);
            //ms.Write(Dst, 0, Dst.Length);
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public string GetEngine()
        {
            return "Halo1Xbox";
        }

        public string GetName()
        {
            return Header.MapName;
        }

        public void Compile(Global.Data.Project project, System.ComponentModel.BackgroundWorker worker)
        {
            throw new NotImplementedException();
        }

        public void Decompile(Global.Data.Project project, System.ComponentModel.BackgroundWorker worker)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
