using Mutation.Halo.TagGroups.Attributes;
using Mutation.Halo.TagGroups.FieldTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.CacheMap
{
    public class MapHeader
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

        #endregion

        #region Constants

        public const int kSizeOf = 2048;
        public const int MapHeaderSignature = 0x64616568; // 'daeh'
        public const int MapFooterSignature = 0x746F6F66; // 'toof'

        #endregion

        public int mapHeaderSignature;
        public int engineVersion;
        public int fileSize;
        public int scenarioPointer;
        public int indexOffset;
        public int indexSize;
        public int metaTableSize;
        public int nonRawSize;

        [Padding(PaddingType.Padding, 256)]
        private byte[] padding1;

        public String32 buildDate;
        public MapType mapType;

        [Padding(PaddingType.Padding, 20)]
        private byte[] padding2;

        public int crazyOffset;
        public int crazySize;
        public int stringIdPaddedTableOffset;
        public int stringIdCount;
        public int stringIdTableSize;
        public int stringIdIndexOffset;
        public int stringIdTableOffset;

        [Padding(PaddingType.Padding, 36)]
        private byte[] padding3;

        public String32 mapName;
        private int padding4;
        public String256 mapScenario;
        private int padding5;
        public int tagCount;
        public int fileTableOffset;
        public int fileTableSize;
        public int fileTableIndexOffset;
        public int checksum;

        [Padding(PaddingType.Padding, 1320)]
        private byte[] padding6;

        public int mapFooterSignature;

        public void Read(BinaryReader reader)
        {
            this.mapHeaderSignature = reader.ReadInt32();
            this.engineVersion = reader.ReadInt32();
            this.fileSize = reader.ReadInt32();
            this.scenarioPointer = reader.ReadInt32();
            this.indexOffset = reader.ReadInt32();
            this.indexSize = reader.ReadInt32();
            this.metaTableSize = reader.ReadInt32();
            this.nonRawSize = reader.ReadInt32();
            this.padding1 = reader.ReadBytes(PaddingAttribute.GetPaddingFieldSize(this.GetType(), "padding1"));
            this.buildDate = reader.ReadString32();
            this.mapType = (MapType)reader.ReadInt32();
            this.padding2 = reader.ReadBytes(PaddingAttribute.GetPaddingFieldSize(this.GetType(), "padding2"));
            this.crazyOffset = reader.ReadInt32();
            this.crazySize = reader.ReadInt32();
            this.stringIdPaddedTableOffset = reader.ReadInt32();
            this.stringIdCount = reader.ReadInt32();
            this.stringIdTableSize = reader.ReadInt32();
            this.stringIdIndexOffset = reader.ReadInt32();
            this.stringIdTableOffset = reader.ReadInt32();
            this.padding3 = reader.ReadBytes(PaddingAttribute.GetPaddingFieldSize(this.GetType(), "padding3"));
            this.mapName = reader.ReadString32();
            this.padding4 = reader.ReadInt32();
            this.mapScenario = reader.ReadString256();
            this.padding5 = reader.ReadInt32();
            this.tagCount = reader.ReadInt32();
            this.fileTableOffset = reader.ReadInt32();
            this.fileTableSize = reader.ReadInt32();
            this.fileTableIndexOffset = reader.ReadInt32();
            this.checksum = reader.ReadInt32();
            this.padding6 = reader.ReadBytes(PaddingAttribute.GetPaddingFieldSize(this.GetType(), "padding6"));
            this.mapFooterSignature = reader.ReadInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.mapHeaderSignature);
            writer.Write(this.engineVersion);
            writer.Write(this.fileSize);
            writer.Write(this.scenarioPointer);
            writer.Write(this.indexOffset);
            writer.Write(this.indexSize);
            writer.Write(this.metaTableSize);
            writer.Write(this.nonRawSize);
            writer.Write(this.padding1);
            writer.Write(this.buildDate);
            writer.Write((int)this.mapType);
            writer.Write(this.padding2);
            writer.Write(this.crazyOffset);
            writer.Write(this.crazySize);
            writer.Write(this.stringIdPaddedTableOffset);
            writer.Write(this.stringIdCount);
            writer.Write(this.stringIdTableSize);
            writer.Write(this.stringIdIndexOffset);
            writer.Write(this.stringIdTableOffset);
            writer.Write(this.padding3);
            writer.Write(this.mapName);
            writer.Write(this.padding4);
            writer.Write(this.mapScenario);
            writer.Write(this.padding5);
            writer.Write(this.tagCount);
            writer.Write(this.fileTableOffset);
            writer.Write(this.fileTableSize);
            writer.Write(this.fileTableIndexOffset);
            writer.Write(this.checksum);
            writer.Write(this.padding6);
            writer.Write(this.mapFooterSignature);
        }
    }
}
