using Mutation.Halo.TagGroups.FieldTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.CacheMap
{
    #region s_TagIndexHeader

    public struct s_TagIndexHeader
    {
        #region Constants

        public const int kSizeOf = 32;
        public const int TagIndexSignature = 0x73676174; // 'sgat'

        #endregion

        public uint tagIndexAddress;
        public int tagHierarchyCount;
        public uint tagStartAddress;
        public DatumIndex scenarioDatum;
        public DatumIndex globalsDatum;
        public int unknown;
        public int tagCount;
        public int tagIndexSignature;

        public void Read(BinaryReader reader)
        {
            this.tagIndexAddress = reader.ReadUInt32();
            this.tagHierarchyCount = reader.ReadInt32();
            this.tagStartAddress = reader.ReadUInt32();
            this.scenarioDatum = reader.ReadDatumIndex();
            this.globalsDatum = reader.ReadDatumIndex();
            this.unknown = reader.ReadInt32();
            this.tagCount = reader.ReadInt32();
            this.tagIndexSignature = reader.ReadInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.tagIndexAddress);
            writer.Write(this.tagHierarchyCount);
            writer.Write(this.tagStartAddress);
            writer.Write(this.scenarioDatum);
            writer.Write(this.globalsDatum);
            writer.Write(this.unknown);
            writer.Write(this.tagCount);
            writer.Write(this.tagIndexSignature);
        }
    }

    #endregion

    #region s_TagGenealogy

    public struct s_TagGenealogy
    {
        public const int kSizeOf = 12;

        public GroupTag childClass;
        public GroupTag parentClass;
        public GroupTag grandparentClass;

        public void Read(BinaryReader reader)
        {
            this.childClass = reader.ReadGroupTag();
            this.parentClass = reader.ReadGroupTag();
            this.grandparentClass = reader.ReadGroupTag();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.childClass);
            writer.Write(this.parentClass);
            writer.Write(this.grandparentClass);
        }
    }

    #endregion

    #region s_TagIndexEntry

    public struct s_TagIndexEntry
    {
        public const int kSizeOf = 16;

        public GroupTag tagClass;
        public DatumIndex tagDatum;
        public uint tagAddress;
        public int tagSize;

        public void Read(BinaryReader reader)
        {
            this.tagClass = reader.ReadGroupTag();
            this.tagDatum = reader.ReadDatumIndex();
            this.tagAddress = reader.ReadUInt32();
            this.tagSize = reader.ReadInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.tagClass);
            writer.Write(this.tagDatum);
            writer.Write(this.tagAddress);
            writer.Write(this.tagSize);
        }
    }

    #endregion

    #region s_ScenarioStructureBSPBlock

    public struct s_ScenarioStructureBSPBlock
    {
        public const int kSizeOf = 68;

        public int bspOffset;
        public int bspSize;
        public uint bspAddress;
        public DatumIndex bspDatum;
        public DatumIndex lightmapDatum;

        public void Read(BinaryReader reader)
        {
            this.bspOffset = reader.ReadInt32();
            this.bspSize = reader.ReadInt32();
            this.bspAddress = reader.ReadUInt32();
            reader.BaseStream.Position += 4;
            this.bspDatum = reader.ReadDatumIndex();
            this.lightmapDatum = reader.ReadDatumIndex();
        }
    }

    #endregion

    public class TagIndex
    {
        public const int kMaximumScenarioBSPs = 8;

        /// <summary>
        /// Tag index header fields.
        /// </summary>
        public s_TagIndexHeader tagIndexHeader;

        /// <summary>
        /// Tag index genealogy table.
        /// </summary>
        public s_TagGenealogy[] tagGenealogy;

        /// <summary>
        /// Tag index entries.
        /// </summary>
        public s_TagIndexEntry[] tagIndexEntries;

        /// <summary>
        /// Structure BSP blocks in the scenario tag.
        /// </summary>
        public s_ScenarioStructureBSPBlock[] scenarioBspBlocks;

        public void QuickRead(BinaryReader reader, int indexOffset, int indexSize)
        {
            // Initialize and read the tag index header.
            this.tagIndexHeader = new s_TagIndexHeader();
            this.tagIndexHeader.Read(reader);

            // Verify the tag index header signature.
            if (this.tagIndexHeader.tagIndexSignature != s_TagIndexHeader.TagIndexSignature)
            {
                // The data might be corrupt.
                Console.WriteLine("TagIndex::QuickRead(): tag index header has invalid signature!");

                // TODO: Error handling.
            }

            // Compute the virtual to physical address modifier ('primary magic') value.
            int addressModifier = (int)(this.tagIndexHeader.tagIndexAddress - (indexOffset + s_TagIndexHeader.kSizeOf));

            // Seek to the genealogy table.
            reader.BaseStream.Position = this.tagIndexHeader.tagIndexAddress - addressModifier;

            // Initialize the genealogy table and read each tag hierarchy.
            this.tagGenealogy = new s_TagGenealogy[this.tagIndexHeader.tagHierarchyCount];
            for (int i = 0; i < this.tagIndexHeader.tagHierarchyCount; i++)
            {
                // Initialize and read the entry.
                this.tagGenealogy[i] = new s_TagGenealogy();
                this.tagGenealogy[i].Read(reader);
            }

            // Seek to the tag entry table.
            reader.BaseStream.Position = this.tagIndexHeader.tagStartAddress - addressModifier;

            // Initialize the tag entry table and read each tag entry.
            this.tagIndexEntries = new s_TagIndexEntry[this.tagIndexHeader.tagCount];
            for (int i = 0; i < this.tagIndexHeader.tagCount; i++)
            {
                // Initialize and read the entry.
                this.tagIndexEntries[i] = new s_TagIndexEntry();
                this.tagIndexEntries[i].Read(reader);
            }

            // Compute the virtual to physical address modifier for tag files ('seconday magic').
            addressModifier = (int)(this.tagIndexEntries[0].tagAddress - (indexOffset + indexSize));
        }
    }
}
