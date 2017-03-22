using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IO;
using System.IO;
using HaloPlugins;
using Global;
using HaloPlugins.Objects.Reference;
using Blam.Objects.DataTypes;

namespace HaloMap.H2.Xbox
{
    #region Enums

    public enum MapType : int
    {
        Singleplayer = 0,
        Multiplayer = 1,
        Mainmenu = 2,
        Shared = 3,
        SingleplayerShared = 4
    }

    public enum RawLocation : long
    {
        Internal = 0x00000000,
        Mainmenu = 0xC0000000,
        Shared = 0x80000000,
        SingleplayerShared = 0x40000000
    }

    #endregion

    #region Header Structure

    public struct MapHeader
    {
        public int Magic;
        public int Version;
        public int FileSize;
        public int ScenarioPtr;
        public int IndexOffset;
        public int IndexSize;
        public int MetaTableSize;

        public int NonRawSize;

        public string MapBuild;

        public MapType Type;

        public int CrazyOffset;
        public int CrazySize;
        public int StringIdPaddedTableOffset;
        public int StringIdCount;
        public int StringIdTableSize;
        public int StringIdIndexOffset;
        public int StringIdTableOffset;

        public string MapName;
        public string MapScenario;

        public int TagCount;
        public int FileTableOffset;
        public int FileTableSize;
        public int FileTableIndexOffset;
        public int MapChecksum;
    }

    #endregion

    #region TagIndex Structure

    public struct TagIndexHeader
    {
        public int VirtualTagIndexAddress;
        public int TagHierarchyCount;
        public int VirtualTagStartAddress;
        public int ScenarioId;
        public int GlobalsId;
        public int Unknown;
        public int TagCount;
        public int TagIndexMagic;
    }

    public struct TagEntry
    {
        public char[] Class;
        public int Id;
        public int VirtualTagAddress;
        public int Size;
    }

    #endregion

    #region Unicode Structure

    public struct UnicodeString
    {
        public int StringId;
        public int Offset;
    }

    public struct UnicodeTable
    {
        public int StringCount;
        public int TableSize;
        public int IndexOffset;
        public int TableOffset;
        public UnicodeString[] Strings;
    }

    #endregion

    #region Bsp Structure

    public struct BspBlock
    {
        public int BspIndex;
        public int LightmapIndex;
        public int Magic;
    }

    #endregion

    public class HaloMap : IHaloMap
    {
        #region Fields

        // Map Constants
#warning Explicitly defined engine constants
        const int Constant = -2147086304;
        const int FirstIdent = -512491520;
        const ushort SaltStart = 0xE174;

        // Reader/Writer
        FileStream fs;
        EndianReader br;
        EndianWriter bw;

        // Map Structure
        MapHeader Header;
        TagIndexHeader TagIndexHeader;
        TagEntry[] Tags;
        UnicodeTable[] UnicTables = new UnicodeTable[8];
        BspBlock[] Bsps;
        string[] TagPaths;

        // Virtual-To-Physical Magics
        int MemoryMagic, MetaMagic;

        #endregion

        #region Constructor

        public HaloMap() { }

        public HaloMap(string MapPath)
        {
            // Initialize Reader/Writer
            fs = new FileStream(MapPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            br = new EndianReader(Endianness.Little, fs);
            bw = new EndianWriter(Endianness.Little, fs);

            // Parse the header structure.
            br.BaseStream.Position = 0;
            this.Header.Magic = br.ReadInt32();
            this.Header.Version = br.ReadInt32();
            this.Header.FileSize = br.ReadInt32();
            this.Header.ScenarioPtr = br.ReadInt32();
            this.Header.IndexOffset = br.ReadInt32();
            this.Header.IndexSize = br.ReadInt32();
            this.Header.MetaTableSize = br.ReadInt32();
            this.Header.NonRawSize = br.ReadInt32();

            br.BaseStream.Position = 288;
            this.Header.MapBuild = new string(br.ReadChars(32)).Replace("\0", "");
            this.Header.Type = (MapType)br.ReadInt32();

            br.BaseStream.Position = 344;
            this.Header.CrazyOffset = br.ReadInt32();
            this.Header.CrazySize = br.ReadInt32();
            this.Header.StringIdPaddedTableOffset = br.ReadInt32();
            this.Header.StringIdCount = br.ReadInt32();
            this.Header.StringIdTableSize = br.ReadInt32();
            this.Header.StringIdIndexOffset = br.ReadInt32();
            this.Header.StringIdTableOffset = br.ReadInt32();

            br.BaseStream.Position = 408;
            this.Header.MapName = new string(br.ReadChars(36)).Replace("\0", "");
            this.Header.MapScenario = new string(br.ReadChars(256)).Replace("\0", "");

            br.BaseStream.Position = 704;
            this.Header.TagCount = br.ReadInt32();
            this.Header.FileTableOffset = br.ReadInt32();
            this.Header.FileTableSize = br.ReadInt32();
            this.Header.FileTableIndexOffset = br.ReadInt32();
            this.Header.MapChecksum = br.ReadInt32();
        }

        #endregion

        #region IHaloMap Members

        public void Initialize()
        {
             // Parse Index Header
            br.BaseStream.Position = Header.IndexOffset;
            this.TagIndexHeader.VirtualTagIndexAddress = br.ReadInt32();
            this.TagIndexHeader.TagHierarchyCount = br.ReadInt32();
            this.TagIndexHeader.VirtualTagStartAddress = br.ReadInt32();
            this.TagIndexHeader.ScenarioId = br.ReadInt32();
            this.TagIndexHeader.GlobalsId = br.ReadInt32();
            this.TagIndexHeader.Unknown = br.ReadInt32();
            this.TagIndexHeader.TagCount = br.ReadInt32();
            this.TagIndexHeader.TagIndexMagic = br.ReadInt32();

            // Initialize the string_id globals table.
            Blam.TagFiles.TagGroups._InitializeStringIdTables();

            // Read StringIds
            br.BaseStream.Position = Header.StringIdTableOffset;
            //EngineManager.Engines["Halo2Xbox"]["StringIds"] = new List<string>(Header.StringIdCount);
            //List<string> StringIds = (List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"];
            //for (int i = 0; i < Header.StringIdCount; i++)
            //    StringIds.Add(br.ReadNullTerminatingString());
            for (int i = 0; i < Header.StringIdCount; i++)
                Blam.TagFiles.TagGroups._CreateStringId(br.ReadNullTerminatingString());

            // Read TagPaths
            br.BaseStream.Position = Header.FileTableOffset;
            TagPaths = new string[Header.TagCount];
            for (int i = 0; i < Header.TagCount; i++)
                TagPaths[i] = br.ReadNullTerminatingString();

            // Parse Tags
            br.BaseStream.Position = Header.IndexOffset + 32 + (TagIndexHeader.TagHierarchyCount * 12);
            Tags = new TagEntry[TagIndexHeader.TagCount];
            EngineManager.Engines["Halo2Xbox"]["TagIds"] = new Dictionary<string, int>(TagIndexHeader.TagCount);
            for (int i = 0; i < TagIndexHeader.TagCount; i++)
            {
                Tags[i].Class = br.ReadChars(4);
                Tags[i].Id = br.ReadInt32();
                Tags[i].VirtualTagAddress = br.ReadInt32();
                Tags[i].Size = br.ReadInt32();

                TagPaths[i] += "." + EngineManager.Engines["Halo2Xbox"]["Extension", Utils.StrReverse(new string(Tags[i].Class))];
                ((Dictionary<string, int>)EngineManager.Engines["Halo2Xbox"]["TagIds"]).Add(TagPaths[i], Tags[i].Id);
            }

            // Compute Magics
            MemoryMagic = TagIndexHeader.VirtualTagIndexAddress - (Header.IndexOffset + 32);
            MetaMagic = Tags[0].VirtualTagAddress - (Header.IndexOffset + Header.IndexSize);

            // Load Matg Definition
            TagDefinition matg = EngineManager.Engines["Halo2Xbox"].CreateInstance("matg");
            br.BaseStream.Position = Tags[0].VirtualTagAddress - MetaMagic;
            matg.Read(br, MetaMagic);

            // Read Out Unicode Table
            for (int i = 0; i < 8; i++)
            {
                // Read Headers
                UnicTables[i].StringCount = (int)matg[30 + (i * 6)].GetValue();
                UnicTables[i].TableSize = (int)matg[31 + (i * 6)].GetValue();
                UnicTables[i].IndexOffset = (int)matg[32 + (i * 6)].GetValue();
                UnicTables[i].TableOffset = (int)matg[33 + (i * 6)].GetValue();
                UnicTables[i].Strings = new UnicodeString[UnicTables[i].StringCount];

                // Read Index
                br.BaseStream.Position = UnicTables[i].IndexOffset;
                for (int x = 0; x < UnicTables[i].StringCount; x++)
                {
                    UnicTables[i].Strings[x].StringId = br.ReadInt32();
                    UnicTables[i].Strings[x].Offset = br.ReadInt32();
                }
            }

            // Read Scenario
            TagDefinition scnr = EngineManager.Engines["Halo2Xbox"].CreateInstance("scnr");
            br.BaseStream.Position = Tags[3].VirtualTagAddress - MetaMagic;
            scnr.Read(br, MetaMagic);

            // Read Structure Bsps
            Bsps = new BspBlock[((TagBlock)scnr[61]).BlockCount];
            for (int i = 0; i < ((TagBlock)scnr[61]).BlockCount; i++)
            {
                // Bsp Info
                int BspIndex = ((TagReference)((TagBlock)scnr[61])[i][4]).GetTagIndex();
                int BspOffset = (int)((TagBlock)scnr[61])[i][0].GetValue();
                Tags[BspIndex].VirtualTagAddress = (int)((TagBlock)scnr[61])[i][2].GetValue();
                Tags[BspIndex].Size = (int)((TagBlock)scnr[61])[i][1].GetValue();
                int Magic = Tags[BspIndex].VirtualTagAddress - BspOffset;

                // Read Bsp Header
                br.BaseStream.Position = BspOffset + 8;
                int LightmapVirtOffset = br.ReadInt32();

                // Lightmap Info
                int LightmapIndex = ((TagReference)((TagBlock)scnr[61])[i][5]).GetTagIndex();
                Tags[LightmapIndex].VirtualTagAddress = LightmapVirtOffset;
                Tags[LightmapIndex].Size = (BspOffset + Tags[BspIndex].Size) - (Tags[LightmapIndex].VirtualTagAddress - Magic);

                // Build Bsp Block
                Bsps[i].BspIndex = BspIndex;
                Bsps[i].LightmapIndex = LightmapIndex;
                Bsps[i].Magic = Magic;

                // Write the BSP information to the console.
                Console.WriteLine("BSP {0}: {1}", i, ((TagReference)((TagBlock)scnr[61])[i][4]).GetFileName());
                Console.WriteLine("\tBSP ID: 0x{0}", Tags[BspIndex].Id.ToString("X8"));
                Console.WriteLine("\tLightmap ID: 0x{0}", Tags[LightmapIndex].Id.ToString("X8"));
                Console.WriteLine("\tVirtual Address: 0x{0} - 0x{1}", Tags[BspIndex].VirtualTagAddress.ToString("X8"), 
                    (Tags[BspIndex].VirtualTagAddress + Tags[BspIndex].Size).ToString("X8"));
                Console.WriteLine("\tPhysical Offset: 0x{0}", Tags[BspIndex].Size);
                Console.WriteLine("\tLightmap Address: 0x{0}", Tags[LightmapIndex].VirtualTagAddress.ToString("X8"));
                Console.WriteLine();

#if (false)
                // Prompt the user for a filename
                Console.Write("please enter a filename to save bsp \"{0}\"\n>", TagPaths[BspIndex]);
                string line = Console.ReadLine();
                if (line != "")
                {
                    string fileName = Global.Events.Debugging.SplitCommandLine(line)[0];
                    if (fileName != "")
                    {
                        // Debug dump bsp to file
                        Console.WriteLine("dumping bsp {0} to file {1}", TagPaths[BspIndex], fileName);
                        br.BaseStream.Position = BspOffset;
                        File.WriteAllBytes(fileName, br.ReadBytes(Tags[BspIndex].Size - Tags[LightmapIndex].Size));
                    }
                }
#endif
            }

#if (false)
            // Prompt the user for a filename
            Console.Write("please enter a filename to save tag ids to\n>");
            string fileName = Global.Events.Debugging.SplitCommandLine(Console.ReadLine())[0];
            if (fileName != "")
            {
                // Open file
                Console.WriteLine("dumping tag ids to file {0}", fileName);
                EndianWriter writer = new EndianWriter(Endianness.Little, new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write));

                // Write all tag ids
                for (int i = 0; i < Tags.Length; i++)
                {
                    writer.Write(Tags[i].Id);
                }

                // Close
                writer.Close();
            }
#endif
        }

        public void Destroy()
        {
        }

        public string GetEngine()
        {
            return "Halo2Xbox";
        }

        public string GetName()
        {
            return Header.MapName;
        }

        public void Compile(Global.Data.Project project, System.ComponentModel.BackgroundWorker worker)
        {
            #region Initialize

            // Initialize our Stream
            fs = new FileStream(project.BinFolder + project.Name + ".map", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            br = new EndianReader(Endianness.Little, fs);
            bw = new EndianWriter(Endianness.Little, fs);

            // Header
            bw.Write(new byte[2048]);

            // Streams
            EndianReader mr;

            // Global Tag List
            TagDefinition[] Metas = new TagDefinition[project.Tags.Count];

            // Indices
            List<int> Models = new List<int>();
            List<int> Bsps = new List<int>();
            List<int> Lightmaps = new List<int>();
            List<int> Weather = new List<int>();
            List<int> Decorators = new List<int>();
            List<int> Particles = new List<int>();
            List<int> Animations = new List<int>();
            List<int> Bitmaps = new List<int>();

            // Init lists
            Dictionary<string, int> TagIds = new Dictionary<string, int>();
            //List<string> StringIds = new List<string>(((string[])EngineManager.Engines["Halo2Xbox"]["EngineStringIds"]).Length);
            //StringIds.AddRange((string[])EngineManager.Engines["Halo2Xbox"]["EngineStringIds"]);

            // Re-initialize the string_id globals table.
            Blam.TagFiles.TagGroups._InitializeStringIdTables();

            // Prompt the user for a filename
            BinaryReader reader = null;
#if (false)
            Console.Write("please enter a filename to read tag ids from\n>");
            string line = Console.ReadLine();
            if (line != "")
            {
                string fileName = Global.Events.Debugging.SplitCommandLine(line)[0];
                if (fileName != "")
                    reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read));
            }
#endif

            // Load Metas
            worker.ReportProgress(0, new object[] { "Processing: ", "", project.Tags.Count, 1 });
            for (int i = 0; i < project.Tags.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object [] { project.Tags[i] });

                // Initialize Steams
                mr = new EndianReader(Endianness.Little, new FileStream(project.TagFolder + project.Tags[i], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite));

                // Read Meta
                Metas[i] = EngineManager.Engines["Halo2Xbox"].CreateInstance(project.Tags[i].Substring(project.Tags[i].IndexOf(".") + 1));
                Metas[i].Read(mr);

                // Add Raw Holding Tags
                switch (project.Tags[i].Substring(project.Tags[i].IndexOf(".") + 1))
                {
                    case "render_model": Models.Add(i); break;
                    case "weather": Weather.Add(i); break;
                    case "decorator": Decorators.Add(i); break;
                    case "particle_model": Particles.Add(i); break;
                    case "model_animation_graph": Animations.Add(i); break;
                    case "bitmap": Bitmaps.Add(i); break;
                }

                // Add string ids
                //int Count = ((List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"]).Count;
                //for (int x = 0; x < Count; x++)
                //{
                //    if (!StringIds.Contains(((List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"])[x]))
                //        StringIds.Add(((List<string>)EngineManager.Engines["Halo2Xbox"]["StringIds"])[x]);
                //}

                // Add tag lookup info
                if (reader == null)
                    TagIds.Add(project.Tags[i], (int)CreateIdent(i));
                else
                    TagIds.Add(project.Tags[i], reader.ReadInt32());

                // Close Streams
                mr.Close();
            }

            // Close reader
            if (reader != null)
                reader.Close();

            // Set engine lists
            EngineManager.Engines["Halo2Xbox"]["TagIds"] = TagIds;
            //EngineManager.Engines["Halo2Xbox"]["StringIds"] = StringIds;

            // Initialize Streams to Sound Diagnostics
            mr = new EndianReader(Endianness.Little, new FileStream(project.TagFolder + project.Tags[project.Tags.Count - 1], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite));

            #endregion

            #region Sound Raw

            // UI
            worker.ReportProgress(0, new object[] { "Writing Internal Sound Raw...", "", 0, 0 });

            // Write Raw
            bw.BaseStream.Position = bw.BaseStream.Length;
            Metas[Metas.Length - 1].RawDef.FlushStream("SoundRaw", (int)bw.BaseStream.Length);
            bw.Write(Metas[Metas.Length - 1].RawDef["SoundRaw"].ToArray());

            #endregion

            #region Model Raw

            worker.ReportProgress(0, new object[] { "Writing Model Raw: ", "", Models.Count, 1 });
            for (int i = 0; i < Models.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object[] { project.Tags[Models[i]] });

                // Raw
                bw.BaseStream.Position = bw.BaseStream.Length;
                Metas[Models[i]].RawDef.FlushStream("ModelRaw", (int)bw.BaseStream.Length);
                bw.Write(Metas[Models[i]].RawDef["ModelRaw"].ToArray());
            }

            #endregion

            #region BSP and Lightmap Raw

            // Get Scenario
            TagDefinition scnr = Metas[3];

            // Compile all the Raw for evey BSP and Lightmap
            TagBlock bsps = (TagBlock)scnr[61];
            for (int i = 0; i < bsps.BlockCount; i++)
            {
                // Bsp Info
                int BspIndex = Array.IndexOf(project.Tags.ToArray(), ((TagReference)bsps[i][4]).GetFileName());
                int LightmapIndex = Array.IndexOf(project.Tags.ToArray(), ((TagReference)bsps[i][5]).GetFileName());
                bool HasLightmap = LightmapIndex != -1;

                // Block Counts
                int Bsp1 = ((TagBlock)Metas[BspIndex][26]).BlockCount;
                int Bsp2 = ((TagBlock)Metas[BspIndex][41]).BlockCount;
                int Bsp3 = ((TagBlock)Metas[BspIndex][63]).BlockCount;
                int Bsp4 = ((TagBlock)Metas[BspIndex][67]).BlockCount;
                int Total = Bsp1 + Bsp2 + Bsp3 + Bsp4;

                // UI
                worker.ReportProgress(0, new object[] { "Writing Bsp Raw: ", project.Tags[BspIndex], Total, 1 });
                bw.BaseStream.Position = bw.BaseStream.Length;

                // Detail Objects
                Metas[BspIndex].RawDef.FlushStream("Bsp1", (int)bw.BaseStream.Length);
                bw.Write(Metas[BspIndex].RawDef["Bsp1"].ToArray());

                // Permutations
                Metas[BspIndex].RawDef.FlushStream("Bsp2", (int)bw.BaseStream.Length);
                bw.Write(Metas[BspIndex].RawDef["Bsp2"].ToArray());

                #region Lightmap Raw

                // Check Lightmap Exists
                if (HasLightmap)
                {
                    // Write
                    Metas[LightmapIndex].RawDef.FlushStream("LightmapRaw", (int)bw.BaseStream.Length);
                    bw.Write(Metas[LightmapIndex].RawDef["LightmapRaw"].ToArray());
                }

                #endregion

                // Decorators
                Metas[BspIndex].RawDef.FlushStream("Bsp4", (int)bw.BaseStream.Length);
                bw.Write(Metas[BspIndex].RawDef["Bsp4"].ToArray());

                // Water Definitions
                Metas[BspIndex].RawDef.FlushStream("Bsp3", (int)bw.BaseStream.Length);
                bw.Write(Metas[BspIndex].RawDef["Bsp3"].ToArray());
            }

            #endregion

            #region Weather Raw

            worker.ReportProgress(0, new object[] { "Writing Weather Raw: ", "", Weather.Count, 1 });
            for (int i = 0; i < Weather.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object[] { project.Tags[Weather[i]] });

                // Raw
                bw.BaseStream.Position = bw.BaseStream.Length;
                Metas[Weather[i]].RawDef.FlushStream("WeatherRaw", (int)bw.BaseStream.Length);
                bw.Write(Metas[Weather[i]].RawDef["WeatherRaw"].ToArray());
            }

            #endregion

            #region Decorator Raw

            worker.ReportProgress(0, new object[] { "Writing Decorator Raw: ", "", Decorators.Count, 1 });
            for (int i = 0; i < Decorators.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object[] { project.Tags[Decorators[i]] });

                // Raw
                bw.BaseStream.Position = bw.BaseStream.Length;
                Metas[Decorators[i]].RawDef.FlushStream("DecoratorRaw", (int)bw.BaseStream.Length);
                bw.Write(Metas[Decorators[i]].RawDef["DecoratorRaw"].ToArray());
            }

            #endregion

            #region Particle Raw

            worker.ReportProgress(0, new object[] { "Writing Particle Raw: ", "", Particles.Count, 1 });
            for (int i = 0; i < Particles.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object[] { project.Tags[Particles[i]] });

                // Raw
                bw.BaseStream.Position = bw.BaseStream.Length;
                Metas[Particles[i]].RawDef.FlushStream("ParticleRaw", (int)bw.BaseStream.Length);
                bw.Write(Metas[Particles[i]].RawDef["ParticleRaw"].ToArray());
            }

            #endregion

            #region Coconut Model Raw

            // UI
            worker.ReportProgress(0, new object[] { "Writing Coconut Model Raw...", "", ((TagBlock)Metas[Metas.Length - 1][10]).BlockCount, 1 });

            // Extra Model Info
            bw.BaseStream.Position = bw.BaseStream.Length;
            Metas[Metas.Length - 1].RawDef.FlushStream("ExtraInfo", (int)bw.BaseStream.Length);
            bw.Write(Metas[Metas.Length - 1].RawDef["ExtraInfo"].ToArray());

            #endregion

            #region Animation Raw

            worker.ReportProgress(0, new object[] { "Writing Animation Raw: ", "", Animations.Count, 1 });
            for (int i = 0; i < Animations.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object[] { project.Tags[Animations[i]] });

                // Raw
                bw.BaseStream.Position = bw.BaseStream.Length;
                Metas[Animations[i]].RawDef.FlushStream("AnimationRaw", (int)bw.BaseStream.Length);
                bw.Write(Metas[Animations[i]].RawDef["AnimationRaw"].ToArray());
            }

            #endregion

            #region Compile BSP and Lightmap Meta

            // UI
            worker.ReportProgress(0, new object[] { "Writing Bsp Meta: ", "", bsps.BlockCount, 1 });

            for (int i = 0; i < bsps.BlockCount; i++)
            {
                // Block Info
                int BspIndex = GetTagIndex(project, (string)bsps[i][4].GetValue());
                int LightmapIndex = GetTagIndex(project, (string)bsps[i][5].GetValue());
                bool HasLightmap = LightmapIndex != -1;

                // Bsp Info
                int BspOffset = 0;
                int BspSize = 0;

                // Lightmap Info
                int LightmapOffset = 0;
                int LightmapSize = 0;

                // UI
                worker.ReportProgress(0, new object[] { project.Tags[BspIndex] });

                // Compute Index Size
                int IndexSize = (1448 + (project.Tags.Count * 16));
                IndexSize += Padding(512, IndexSize);

                // Calculate Virtual To Physical Modifier
                BspOffset = (int)bw.BaseStream.Position;
                int VirtualTagAddress = Constant - 32 + IndexSize;
                int Magic = VirtualTagAddress - BspOffset;

                // Update Structure Bsp Block
                bsps[i][0].SetValue(BspOffset);
                bsps[i][2].SetValue(VirtualTagAddress);

                // Compile BSP Meta
                Metas[BspIndex].Write(bw, Magic);

                // Compile Lightmap Meta
                if (HasLightmap)
                {
                    // Pad sbsp
                    bw.BaseStream.Position = bw.BaseStream.Length;
                    bw.Write(new byte[Padding(4, (int)bw.BaseStream.Position)]);

                    // Update Info
                    LightmapOffset = (int)bw.BaseStream.Position;

                    // Compile Lightmap
                    Metas[LightmapIndex].Write(bw, Magic);

                    // Update Info
                    LightmapSize = (int)bw.BaseStream.Length - LightmapOffset;
                }

                // Bsp Block Padding
                bw.BaseStream.Position = bw.BaseStream.Length;
                bw.Write(new byte[Padding(4096, (int)bw.BaseStream.Position)]);
                BspSize = (int)bw.BaseStream.Length - BspOffset;

                // Update Structure Bsps Block
                bsps[i][1].SetValue(BspSize);

                // Write BSP Header
                bw.BaseStream.Position = BspOffset;
                bw.Write(BspSize);
                bw.Write(BspOffset + 16 + Magic);
                bw.Write(LightmapOffset + Magic);
                bw.Write("psbs".ToCharArray());
                bw.Write(0);
                bw.BaseStream.Position = bw.BaseStream.Length;
            }

            #endregion

            #region Compile StringId Tables

            // UI
            worker.ReportProgress(0, new object[] { "Writing String Id Tables...", "", 3, 1 });

            // String128
            Header.StringIdPaddedTableOffset = (int)bw.BaseStream.Length;
            Header.StringIdCount = Blam.Engine.Engine.g_string_id_count;
            for (int i = 0; i < Blam.Engine.Engine.g_string_id_count; i++)
            {
                // Loop for 128 characters.
                for (int x = 0; x < 127; x++)
                {
                    // Write the string constant buffer until we reach the null terminating character, then pad the
                    // string to 128 bytes in length.
                    if (Blam.Engine.Engine.g_string_id_storage[Blam.Engine.Engine.g_string_id_index_buffer[i] + x] != '\0')
                        bw.Write(Blam.Engine.Engine.g_string_id_storage[Blam.Engine.Engine.g_string_id_index_buffer[i] + x]);
                    else
                    {
                        // Pad the string and break the loop.
                        bw.Write(new byte[127 - x]);
                        break;
                    }
                }

                // Write the null terminating character.
                bw.Write((byte)0);
            }

            // Padding
            bw.Write(new byte[Padding(512, (int)bw.BaseStream.Length)]);

            // Step
            worker.ReportProgress(0);

            // StringId Index
            Header.StringIdIndexOffset = (int)bw.BaseStream.Length;
            for (int i = 0; i < Blam.Engine.Engine.g_string_id_count; i++)
                bw.Write(Blam.Engine.Engine.g_string_id_index_buffer[i]);

            // Padding
            bw.Write(new byte[Padding(512, (int)bw.BaseStream.Length)]);

            // Step
            worker.ReportProgress(0);

            // StringId Table
            Header.StringIdTableOffset = (int)bw.BaseStream.Length;
            bw.Write(Blam.Engine.Engine.g_string_id_storage, 0, Blam.Engine.Engine._g_string_id_storage_size);
            Header.StringIdTableSize = Blam.Engine.Engine._g_string_id_storage_size;

            // Padding
            bw.Write(new byte[Padding(512, (int)bw.BaseStream.Length)]);

            // Step
            worker.ReportProgress(0);

            #endregion

            #region Compile File Table

            // UI
            worker.ReportProgress(0, new object[] { "Writing File Table...", "", project.Tags.Count * 2, 1 });

            // File Table
            Header.FileTableOffset = (int)bw.BaseStream.Length;
            Header.TagCount = project.Tags.Count;
            for (int i = 0; i < project.Tags.Count; i++)
            {
                // UI
                worker.ReportProgress(0);

                // Write
                bw.Write(project.Tags[i].Remove(project.Tags[i].LastIndexOf(".")).ToCharArray());
                bw.Write((byte)0);
            }
            Header.FileTableSize = (int)bw.BaseStream.Length - Header.FileTableOffset;

            // Header
            bw.Write(new byte[Padding(512, (int)bw.BaseStream.Length)]);

            // File Table Index
            int TagOffset = 0;
            Header.FileTableIndexOffset = (int)bw.BaseStream.Length;
            for (int i = 0; i < project.Tags.Count; i++)
            {
                // UI
                worker.ReportProgress(0);

                // Write
                bw.Write(TagOffset);
                if (i != project.Tags.Count - 1)
                    TagOffset += project.Tags[i].Remove(project.Tags[i].LastIndexOf(".")).Length + 1;
            }

            // Padding
            bw.Write(new byte[Padding(512, (int)bw.BaseStream.Length)]);

            #endregion

            #region Compile Unicode Tables

            // UI
            worker.ReportProgress(0, new object[] { "Writing Unicode Tables...", "", 8, 1 });

            // Open
            mr = new EndianReader(Endianness.Little, new FileStream(project.Directory + project.ProjectFiles["UnicodeTables"], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite));

            // Read Header
            int[] StringCount = new int[8];
            int[] IndexOffset = new int[8];
            int[] TableOffset = new int[8];
            for (int i = 0; i < 8; i++)
            {
                StringCount[i] = mr.ReadInt32();
                IndexOffset[i] = mr.ReadInt32() + 104;
                TableOffset[i] = mr.ReadInt32() + 104;
            }
            int StringIdsCount = mr.ReadInt32();
            int StringIdsOffset = mr.ReadInt32() + 104;

            // Read StringIds
            List<string> Links = new List<string>();
            mr.BaseStream.Position = StringIdsOffset;
            for (int i = 0; i < StringIdsCount; i++)
                Links.Add(mr.ReadNullTerminatingString());

            // Build Unicode Tables
            for (int i = 0; i < 8; i++)
            {
                // UI
                worker.ReportProgress(0);

                // Update Globals
                Metas[0][30 + (i * 6)].SetValue(StringCount[i]);
                Metas[0][32 + (i * 6)].SetValue((int)bw.BaseStream.Position);

                // Write Index
                mr.BaseStream.Position = IndexOffset[i];
                for (int x = 0; x < StringCount[i]; x++)
                {
                    // Read and Relink SID
                    int SID = mr.ReadInt32();
                    int Offset = mr.ReadInt32();

                    // Write
                    Blam.Objects.DataTypes.string_id handle = Blam.TagFiles.TagGroups._CreateStringId(Links[SID]);
                    //SID = StringIds.IndexOf(Links[SID]);
                    //if (SID == -1)
                    if (handle == Blam.Objects.DataTypes.string_id._string_id_invalid)
                        bw.Write((int)0);
                    else
                    {
                        bw.Write((uint)handle);
                        //bw.Write((short)SID);
                        //bw.Write(new byte[] { (byte)0, (byte)StringIds[SID].Length });
                    }
                    bw.Write(Offset);
                }

                // Padding
                bw.Write(new byte[Padding(512, (int)bw.BaseStream.Position)]);

                // Write Strings
                mr.BaseStream.Position = TableOffset[i];
                Metas[0][33 + (i * 6)].SetValue((int)bw.BaseStream.Position);
                for (int x = 0; x < StringCount[i]; x++)
                {
                    byte b;
                    while ((b = mr.ReadByte()) != 0)
                        bw.Write(b);
                    bw.Write((byte)0);
                }
                Metas[0][31 + (i * 6)].SetValue((int)bw.BaseStream.Position - (int)Metas[0][33 + (i * 6)].GetValue());

                // Padding
                bw.Write(new byte[Padding(512, (int)bw.BaseStream.Position)]);
            }
            mr.Close();

            #endregion

            #region Write Crazy Block

            // UI
            worker.ReportProgress(0, new object[] { "Writing Crazy Block...", "", 0, 0 });

            // Update Header
            Header.CrazyOffset = (int)bw.BaseStream.Length;

            // Write Block
            bw.Write(File.ReadAllBytes(project.Directory + project.ProjectFiles["CrazyBlock"]));

            // Padding
            bw.Write(new byte[Padding(512, (int)bw.BaseStream.Length)]);

            // Update Header
            Header.CrazySize = (int)bw.BaseStream.Length - Header.CrazyOffset;

            #endregion

            #region Bitmap Raw

            worker.ReportProgress(0, new object[] { "Writing Bitmap Raw: ", "", Bitmaps.Count, 1 });
            for (int i = 0; i < Bitmaps.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object[] { project.Tags[Bitmaps[i]] });

                // Raw
                bw.BaseStream.Position = bw.BaseStream.Length;
                Metas[Bitmaps[i]].RawDef.FlushStream("BitmapRaw", (int)bw.BaseStream.Length);
                bw.Write(Metas[Bitmaps[i]].RawDef["BitmapRaw"].ToArray());
            }

            #endregion

            #region Compile Index

            // UI
            worker.ReportProgress(0, new object[] { "Writing Index...", "", 0, 0 });

            #region Compile Index Header

            Header.IndexOffset = (int)bw.BaseStream.Length;
            MemoryMagic = Constant - Header.IndexOffset - 32;
            bw.Write(Constant);
            bw.Write(118);
            TagIndexHeader.VirtualTagStartAddress = (Header.IndexOffset + 32 + 1416) + MemoryMagic;
            bw.Write(TagIndexHeader.VirtualTagStartAddress);
            bw.Write(FirstIdent + (65537 * 3));
            bw.Write(FirstIdent);
            bw.Write(0);
            bw.Write(project.Tags.Count);
            bw.Write("sgat".ToCharArray());

            #endregion

            #region Compile Tag List

            for (int i = 0; i < ((string[])EngineManager.Engines["Halo2Xbox"]["Classes"]).Length; i++)
            {
                // Write Class
                bw.Write(((string[])EngineManager.Engines["Halo2Xbox"]["Classes"])[i].ToCharArray());

                // Find Parents
                string Parent = FindParent(((string[])EngineManager.Engines["Halo2Xbox"]["Classes"])[i]);
                if (Parent != "")
                {
                    bw.Write(Parent.ToCharArray());
                    Parent = FindParent(Parent);
                    if (Parent != "")
                        bw.Write(Parent.ToCharArray());
                    else
                        bw.Write(-1);
                }
                else
                {
                    bw.Write(-1);
                    bw.Write(-1);
                }
            }

            #endregion

            #region Object Index

            MetaMagic = MemoryMagic + (int)bsps[0][1].GetValue(); // This could be a problem if another bsp is bigger than bsp[0]!!!
            int TagStart = (int)bw.BaseStream.Length;
            bw.Write(new byte[project.Tags.Count * 16]);
            bw.Write(new byte[Padding(512, (int)bw.BaseStream.Length)]);
            Header.IndexSize = (int)bw.BaseStream.Length - Header.IndexOffset;

            #endregion

            #endregion

            #region Compile Metas

            // UI
            worker.ReportProgress(0, new object[] { "Compiling: ", "", project.Tags.Count, 1 });

            // Compile all the tags
            Tags = new TagEntry[project.Tags.Count];
            for (int i = 0; i < project.Tags.Count; i++)
            {
                // UI
                worker.ReportProgress(0, new object[] { project.Tags[i] });

                // Goto Offset
                bw.BaseStream.Position = bw.BaseStream.Length;
                int Offset = (int)bw.BaseStream.Position;

                // Create Tag Entry
                bool IsBsp = (project.Tags[i].EndsWith(".scenario_structure_bsp") || project.Tags[i].EndsWith(".lightmap"));
                TagEntry t = new TagEntry();
                t.Class = Metas[i].GroupTag.ToCharArray();
                t.VirtualTagAddress = IsBsp ? 0 : Offset + MetaMagic;
                t.Id = ((Dictionary<string, int>)EngineManager.Engines["Halo2Xbox"]["TagIds"])[project.Tags[i]];

                // Compile with Padding
                if (!IsBsp)
                {
                    Metas[i].Write(bw, MetaMagic);
                    bw.BaseStream.Position = bw.BaseStream.Length;
                    bw.Write(new byte[Padding(4, (int)bw.BaseStream.Position)]);
                }
                t.Size = IsBsp ? 0 : (int)bw.BaseStream.Length - Offset;
                Tags[i] = t;
            }

            // Padding
            int Pad = Padding(4096, (int)bw.BaseStream.Length);
            for (int i = 0; i < Pad; i++)
                bw.Write((byte)0xCD);

            Header.MetaTableSize = (int)bw.BaseStream.Length - (Header.IndexOffset + Header.IndexSize);

            #endregion

            #region Finish Object Index

            // UI
            worker.ReportProgress(0, new object[] { "Writing Object Index...", "", Tags.Length, 1 });

            // Write
            bw.BaseStream.Position = TagStart;
            for (int i = 0; i < Tags.Length; i++)
            {
                // UI
                worker.ReportProgress(0);

                // Data
                bw.Write(Microsoft.VisualBasic.Strings.StrReverse(new string(Tags[i].Class)).ToCharArray());
                bw.Write(Tags[i].Id);
                bw.Write(Tags[i].VirtualTagAddress);
                bw.Write(Tags[i].Size);
            }

            #endregion

            #region Write Header

            // UI
            worker.ReportProgress(0, new object[] { "Writing Header...", "", 0, 0 });

            // Write
            bw.BaseStream.Position = 0;
            bw.Write("daeh".ToCharArray());
            bw.Write(8);
            bw.Write((int)bw.BaseStream.Length);
            bw.BaseStream.Position += 4;
            bw.Write(Header.IndexOffset);
            bw.Write(Header.IndexSize);
            bw.Write(Header.MetaTableSize);
            int metasize = (int)(br.BaseStream.Length - (Header.IndexOffset + Header.IndexSize));
            Header.NonRawSize = metasize + Header.IndexSize + (int)bsps[0][1].GetValue();
            bw.Write(Header.NonRawSize);
            bw.BaseStream.Position = 288;
            bw.Write("02.09.27.09809".ToCharArray());
            bw.Write(new byte[18]);
            bw.Write(1);
            bw.BaseStream.Position += 20;
            bw.Write(Header.CrazyOffset);
            bw.Write(Header.CrazySize);
            bw.Write(Header.StringIdPaddedTableOffset);
            bw.Write(Header.StringIdCount);
            bw.Write(Header.StringIdTableSize);
            bw.Write(Header.StringIdIndexOffset);
            bw.Write(Header.StringIdTableOffset);
            bw.BaseStream.Position = 408;
            bw.Write(project.Name.ToCharArray());
            bw.Write(new byte[36 - project.Name.Length]);
            string scenario = project.Tags[3].Replace(".scenario", "");
            bw.Write(scenario.ToCharArray());
            bw.Write(new byte[256 - scenario.Length]);
            bw.BaseStream.Position += 4;
            bw.Write(Header.TagCount);
            bw.Write(Header.FileTableOffset);
            bw.Write(Header.FileTableSize);
            bw.Write(Header.FileTableIndexOffset);
            bw.BaseStream.Position = 2044;
            bw.Write("toof".ToCharArray());

            // Sign
            br.BaseStream.Position = 2048;
            int times = ((int)br.BaseStream.Length - 2048) / 4;
            for (int i = 0; i < times; i++)
            {
                Header.MapChecksum ^= br.ReadInt32();
            }

            // Finish
            bw.BaseStream.Position = 720;
            bw.Write(Header.MapChecksum);

            #endregion

            #region Finish

            // Close
            fs.Close();
            br.Close();
            bw.Close();

            // Yee
            System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
            myPlayer.SoundLocation = System.Windows.Forms.Application.StartupPath + "\\Media\\Success.wav";
            //myPlayer.Play();

            // GC
            GC.Collect();

            #endregion
        }

        public void Decompile(Global.Data.Project project, System.ComponentModel.BackgroundWorker worker)
        {
            // Create Updater
            worker.ReportProgress(0, new object[] { "Processing...", "", Tags.Length, 1 });

            // Save Crazy
            br.BaseStream.Position = Header.CrazyOffset;
            project.ProjectFiles.Add("CrazyBlock", "Crazy.bin");
            File.WriteAllBytes(project.Directory + project.ProjectFiles["CrazyBlock"], br.ReadBytes(Header.CrazySize));

            // Write Unicode
            project.ProjectFiles.Add("UnicodeTables", "Unicode.uls");
            BinaryWriter uw = new BinaryWriter(new FileStream(project.Directory + project.ProjectFiles["UnicodeTables"], FileMode.Create, FileAccess.Write, FileShare.Write));

            // Get the string id list
            //List<string> StringIds = (List<string>)(EngineManager.Engines["Halo2Xbox"]["StringIds"]);

            // Compile String Ids
            List<string> Strings = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                /*
                 * Fuck all of this crap. We need to explode the unicode tags.
                 */

                for (int x = 0; x < UnicTables[i].StringCount; x++)
                {
                    Blam.Objects.DataTypes.string_id handle = (uint)UnicTables[i].Strings[x].StringId;
                    int Index = Strings.IndexOf(Blam.TagFiles.TagGroups.string_id_get_string_const(handle));
                    if (Index == -1)
                    {
                        Strings.Add(Blam.TagFiles.TagGroups.string_id_get_string_const(handle));
                        UnicTables[i].Strings[x].StringId = Strings.Count - 1;
                    }
                    else
                        UnicTables[i].Strings[x].StringId = Index;
                }
            }

            // Write
            MemoryStream ms = new MemoryStream();
            for (int i = 0; i < 8; i++)
            {
                // Update Header
                uw.Write(UnicTables[i].StringCount);
                uw.Write((int)ms.Length);

                // Write Index
                for (int x = 0; x < UnicTables[i].StringCount; x++)
                {
                    ms.Write(BitConverter.GetBytes(UnicTables[i].Strings[x].StringId), 0, 4);
                    ms.Write(BitConverter.GetBytes(UnicTables[i].Strings[x].Offset), 0, 4);
                }

                // Padding
                byte[] Padding = new byte[512 - ((int)ms.Position % 512)];
                ms.Write(Padding, 0, Padding.Length);

                // Update Header
                uw.Write((int)ms.Length);

                // Write Table
                br.BaseStream.Position = UnicTables[i].TableOffset;
                byte[] Table = br.ReadBytes(UnicTables[i].TableSize);
                Padding = new byte[512 - ((int)ms.Position % 512)];
                ms.Write(Table, 0, Table.Length);
                ms.Write(Padding, 0, Padding.Length);
            }

            // Update Header
            uw.Write(Strings.Count);
            uw.Write((int)ms.Length);

            // Write Data
            uw.Write(ms.ToArray());

            // Write Strings
            for (int i = 0; i < Strings.Count; i++)
            {
                uw.Write(Strings[i].ToCharArray());
                uw.Write((byte)0);
            }            
            uw.Close();

            // Read All Tags
            TagDefinition[] Metas = new TagDefinition[Tags.Length];
            for (int i = 0; i < Tags.Length; i++)
            {
                // Get tag
                int Magic = (new string(Tags[i].Class) == "psbs" || new string(Tags[i].Class) == "pmtl") ? GetBspMagic(Tags[i].Id) : MetaMagic;
                Metas[i] = EngineManager.Engines["Halo2Xbox"].CreateInstance(Utils.StrReverse(new string(Tags[i].Class)));

                // Assign the tag instance to the tag's datum index and absolute path.
                Metas[i].AssignToInstance(new datum_index((uint)Tags[i].Id), TagPaths[i]);

                // Read
                br.BaseStream.Position = Tags[i].VirtualTagAddress - Magic;
                Metas[i].Read(br, Magic);
            }

            // Write All Tags
            worker.ReportProgress(0, new object[] { "Decompiling:", "", Tags.Length, 1 });
            for (int i = 0; i < Tags.Length; i++)
            {
                // Create Sub-Directorys
                int Pointer = TagPaths[i].LastIndexOf("\\") + 1;
                string SubPath = TagPaths[i].Substring(0, Pointer);
                string TagName = TagPaths[i].Substring(Pointer, TagPaths[i].Length - Pointer);
                Directory.CreateDirectory(project.TagFolder + SubPath);

                // Update UI
                worker.ReportProgress(0, new object[] { TagPaths[i] });

                // Decompile
                string FullPath = project.TagFolder + SubPath + TagName;
                int Magic = (new string(Tags[i].Class) == "psbs" || new string(Tags[i].Class) == "pmtl") ? GetBspMagic(Tags[i].Id) : MetaMagic;
                bw = new EndianWriter(Endianness.Little, new FileStream(FullPath, FileMode.Create, FileAccess.Write, FileShare.Write));
                Metas[i].Write(bw);
                bw.Close();

                // Add To Project
                project.Tags.Add(SubPath + TagName);
            }

            // Finish
            project.Save(null);

            // GC
            GC.Collect();
        }

        #endregion

        #region Helper Functions

        public int GetBspMagic(int Id)
        {
            for (int i = 0; i < Bsps.Length; i++)
            {
                if (Bsps[i].BspIndex == (Id & 0xFFFF) || Bsps[i].LightmapIndex == (Id & 0xFFFF))
                    return Bsps[i].Magic;
            }

            return -1;
        }

        public int Padding(int Size, int Offset)
        {
            int pad = Size - (Offset % Size);
            return (pad != Size ? pad : 0);
        }

        public uint CreateIdent(int i)
        {
            uint index = (uint)i;
            uint salt = (uint)(SaltStart + (ushort)i);
            uint id = index | salt << 16;

            // Byte Flip
            byte[] bytes = BitConverter.GetBytes(id);
            byte[] newbytes = { bytes[0], bytes[1], bytes[2], bytes[3] };

            return BitConverter.ToUInt32(newbytes, 0);
        }

        public int GetTagIndex(Global.Data.Project project, string TagName)
        {
            for (int i = 0; i < project.Tags.Count; i++)
            {
                if (project.Tags[i] == TagName)
                    return i;
            }
            return -1;
        }

        public string FindParent(string Class)
        {
            string ret = "";
            switch (Microsoft.VisualBasic.Strings.StrReverse(Class))
            {
                case "bipd":
                case "vehi":
                    ret = "unit";
                    break;
                case "bloc":
                case "crea":
                case "devi":
                case "item":
                case "proj":
                case "scen":
                case "ssce":
                case "unit":
                    ret = "obje";
                    break;
                case "ctrl":
                case "lifi":
                case "mach":
                    ret = "devi";
                    break;
                case "eqip":
                case "garb":
                case "weap":
                    ret = "item";
                    break;
            }
            return Microsoft.VisualBasic.Strings.StrReverse(ret);
        }

        #endregion
    }
}
