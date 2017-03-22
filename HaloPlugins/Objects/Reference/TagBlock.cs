using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO;

namespace HaloPlugins.Objects.Reference
{
    public class TagBlock : MetaNode
    {
        #region Fields

        private int blockCount = 0;
        /// <summary>
        /// Gets the number of blocks in this tag_block structure.
        /// </summary>
        public int BlockCount { get { return blockCount; } }

        private int address = 0;
        /// <summary>
        /// Gets a virtual address that points to this tag_block's data.
        /// </summary>
        public int Address { get { return address; } }

        private readonly int definitionSize;
        /// <summary>
        /// Gets the size of the underlying data structure this tag_block encapsulates.
        /// </summary>
        public int DefinitionSize { get { return definitionSize; } }

        private readonly int paddingInterval = 4;
        /// <summary>
        /// Gets the padding interval this tag_block's data must be aligned to.
        /// </summary>
        public int PaddingInterval { get { return paddingInterval; } }

        private readonly int maxBlockCount = -1;
        /// <summary>
        /// Gets the maximum number of blocks this tag_block can hold.
        /// </summary>
        public int MaxBlockCount { get { return maxBlockCount; } }

        public MetaNode[] definition;
        List<MetaNode[]> blocks = new List<MetaNode[]>();

        #endregion

        #region Constructor

        public TagBlock(string name, int definitionSize, int maxBlocks, MetaNode[] definition) 
            : base(name, 8, FieldType.TagBlock, EngineManager.HaloEngine.Halo2)
        {
            // Set fields to default values.
            this.definitionSize = definitionSize;
            this.maxBlockCount = maxBlocks;
            this.definition = definition;

#if (SANITY_CHECK)

            // Manually compute the definition size.
            int size = 0;
            for (int i = 0; i < definition.Length; i++)
                size += definition[i].FieldSize;

            // Check the computed size and runtime size match.
            if (size != definitionSize)
            {
                // Size mismatch error.
                Console.WriteLine("TagBlock \"{0}\" has definition size mismatch, expected {1} but computed {2}!",
                    base.Name, definitionSize, size);
            }

#endif
        }

        public TagBlock(string name, int definitionSize, int maxBlocks, EngineManager.HaloEngine engine, MetaNode[] definition)
            : base(name, (engine == EngineManager.HaloEngine.Halo2 ? 8 : 12), FieldType.TagBlock, engine)
        {
            // Set fields to default values.
            this.definitionSize = definitionSize;
            this.maxBlockCount = maxBlocks;
            this.definition = definition;

#if (SANITY_CHECK)

            // Manually compute the definition size.
            int size = 0;
            for (int i = 0; i < definition.Length; i++)
                size += definition[i].FieldSize;

            // Check the computed size and runtime size match.
            if (size != definitionSize)
            {
                // Size mismatch error.
                Console.WriteLine("TagBlock \"{0}\" has definition size mismatch, expected {1} but computed {2}!",
                    base.Name, definitionSize, size);
            }

#endif
        }

        public TagBlock(string name, int definitionSize, int maxBlocks, int paddingInterval, MetaNode[] definition)
            : base(name, 8, FieldType.TagBlock, EngineManager.HaloEngine.Halo2)
        {
            // Set fields to default values.
            this.definitionSize = definitionSize;
            this.maxBlockCount = maxBlocks;
            this.definition = definition;
            this.paddingInterval = paddingInterval;

#if (SANITY_CHECK)

            // Manually compute the definition size.
            int size = 0;
            for (int i = 0; i < definition.Length; i++)
                size += definition[i].FieldSize;

            // Check the computed size and runtime size match.
            if (size != definitionSize)
            {
                // Size mismatch error.
                Console.WriteLine("TagBlock \"{0}\" has definition size mismatch, expected {1} but computed {2}!",
                    base.Name, definitionSize, size);
            }

#endif
        }

        public TagBlock(string name, int definitionSize, int maxBlocks, int paddingInterval, EngineManager.HaloEngine engine, MetaNode[] definition)
            : base(name, (engine == EngineManager.HaloEngine.Halo2 ? 8 : 12), FieldType.TagBlock, engine)
        {
            // Set fields to default values.
            this.definitionSize = definitionSize;
            this.maxBlockCount = maxBlocks;
            this.definition = definition;
            this.paddingInterval = paddingInterval;

#if (SANITY_CHECK)

            // Manually compute the definition size.
            int size = 0;
            for (int i = 0; i < definition.Length; i++)
                size += definition[i].FieldSize;

            // Check the computed size and runtime size match.
            if (size != definitionSize)
            {
                // Size mismatch error.
                Console.WriteLine("TagBlock \"{0}\" has definition size mismatch, expected {1} but computed {2}!",
                    base.Name, definitionSize, size);
            }

#endif
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new empty definition instance and adds it to the block list.
        /// </summary>
        /// <returns>Index of the new block, -1 if the opperation did not complete successfully.</returns>
        public int AddBlock()
        {
            return AddBlock(false);
        }

        /// <summary>
        /// Creates a new empty definition instance and adds it to the block list.
        /// </summary>
        /// <param name="isInternal">Used to determine if the internal block count should be incremented.</param>
        /// <returns>Index of the new block, -1 if the opperation did not complete successfully.</returns>
        private int AddBlock(bool isInternal)
        {
            // Check to make sure we can add another block.
            if (isInternal == false && maxBlockCount != -1 && blockCount >= maxBlockCount)
                return -1;

            // Create a new definition array.
            MetaNode[] block = new MetaNode[definition.Length];

            // Clone all the fields in the definition.
            for (int i = 0; i < definition.Length; i++)
                block[i] = (MetaNode)definition[i].Clone();

            // Add it to the blocks list.
            blocks.Add(block);

            // Check if we should increment the internal block count.
            if (isInternal == false)
                blockCount++;

            // Return the new block index.
            return blocks.Count - 1;
        }

        /// <summary>
        /// Inserts a new definition instance into the block list at the specified index.
        /// </summary>
        /// <param name="blockIndex">Index to insert the new block at.</param>
        /// <returns>True if the block was successully inserted, false otherwise.</returns>
        public bool InsertBlock(int blockIndex)
        {
            // Check to make sure we can add another block.
            if (maxBlockCount != -1 && blockCount >= maxBlockCount)
                return false;

            // Make sure the index passed was valid.
            if (blockIndex < 0 || blockIndex >= blockCount)
                return false;

            // Create a new definition array.
            MetaNode[] block = new MetaNode[definition.Length];

            // Clone all the fields in the definition.
            for (int i = 0; i < definition.Length; i++)
                block[i] = (MetaNode)definition[i].Clone();

            // Insert it into the list at the specified position.
            blocks.Insert(blockIndex, block);

            // The block has been successully inserted.
            return true;
        }

        /// <summary>
        /// Removes a definition instance from the list at the specified index.
        /// </summary>
        /// <param name="blockIndex">Index to remove the block at.</param>
        /// <returns>True if the block was successfully removed, false otherwise.</returns>
        public bool RemoveBlock(int blockIndex)
        {
            // Make sure there are block to be removed.
            if (blockCount == 0)
                return false;

            // Make sure the index passed was valid.
            if (blockIndex < 0 || blockIndex >= blockCount)
                return false;

            // Remove the block from the list.
            blocks.RemoveAt(blockIndex);

            // Decrease the internal block count.
            blockCount--;

            // Done, return true.
            return true;
        }

        /// <summary>
        /// Removes all the blocks from this tag_block instance.
        /// </summary>
        public void RemoveAll()
        {
            // Remove all the blocks from the list.
            blocks.Clear();

            // Reset the internal block count.
            blockCount = 0;
        }

        #endregion

        #region MetaNode Members

        public override void Read(EndianReader br)
        {
            Read(br, 0);
        }

        public override void Read(EndianReader br, int Magic)
        {
            // Read tag_block header.
            blockCount = br.ReadInt32();
            address = br.ReadInt32() - Magic;
            if (Engine != EngineManager.HaloEngine.Halo2)
                br.BaseStream.Position += 4;

            // Save current stream position.
            long oldPos = br.BaseStream.Position;

            // Initialize the blocks list with the block count we read.
            blocks = new List<MetaNode[]>(blockCount);

            // Check if there are any blocks to read.
            if (blockCount == 0)
                return;

            // Seek to the tag_block address.
            br.BaseStream.Position = address;

            // Loop through the block count and read each block.
            for (int i = 0; i < blockCount; i++)
            {
                // Position the stream at the correct address for this block.
                // NOTE: If our definition sizes are correct we should not need this.
                //br.BaseStream.Position = address + (i * definitionSize);

                // Create a new definition instance and add it to the block array.
                AddBlock(true);

                // Read the definition fields from the stream.
                for (int x = 0; x < definition.Length; x++)
                    blocks[i][x].Read(br, Magic);
            }

            // Restore stream position.
            br.BaseStream.Position = oldPos;
        }

        public override void Write(EndianWriter bw)
        {
            // Check if there are any blocks in the block list.
            if (blockCount == 0)
            {
                // Write empty tag_block and return.
                bw.Write(new byte[base.FieldSize]);
                return;
            }

            // Save current stream position.
            long oldPos = bw.BaseStream.Position;

            // Write a dummy tag_block header.
            bw.Write(new byte[base.FieldSize]);

            // Seek to the end of the stream.
            bw.BaseStream.Position = bw.BaseStream.Length;

            // Write alignment padding.
            bw.AlignToBoundary(paddingInterval);

            // Save the tag_block data address.
            address = (int)bw.BaseStream.Position;

            // Write an empty buffer for the block list so all
            // child tag_block elements will be written correctly.
            bw.Write(new byte[blockCount * definitionSize]);

            // Seek back to the start of the tag_block data.
            bw.BaseStream.Position = address;

            // Loop through the blocks and write each one.
            for (int x = 0; x < blockCount; x++)
            {
                // Position the stream at the correct address for this block.
                // NOTE: If our definition sizes are correct we should not need this.
                //bw.BaseStream.Position = address + (x * definitionSize);

                // Write the block definition fields to the stream.
                for (int i = 0; i < definition.Length; i++)
                    blocks[x][i].Write(bw);
            }

            // Restore stream position.
            bw.BaseStream.Position = oldPos;

            // Write the tag_block header.
            bw.Write(blockCount);
            bw.Write(address);
            if (Engine != EngineManager.HaloEngine.Halo2)
                bw.Write(0);
        }

        public override void Write(EndianWriter bw, int magic)
        {
            // Note: This will not actually work for writing a tab_block to a compiled map.

            // Check if there are any blocks in the block list.
            if (blockCount == 0)
            {
                // Write empty tag_block and return.
                bw.Write(new byte[base.FieldSize]);
                return;
            }

            // Save current stream position.
            long oldPos = bw.BaseStream.Position;

            // Write a dummy tag_block header.
            bw.Write(new byte[base.FieldSize]);

            // Seek to the end of the stream.
            bw.BaseStream.Position = bw.BaseStream.Length;

            // Write alignment padding.
            bw.AlignToBoundary(paddingInterval);

            // Save the tag_block data address.
            address = (int)bw.BaseStream.Position;

            // Write an empty buffer for the block list so all
            // child tag_block elements will be written correctly.
            bw.Write(new byte[blockCount * definitionSize]);

            // Seek back to the start of the tag_block data.
            bw.BaseStream.Position = address;

            // Loop through the blocks and write each one.
            for (int x = 0; x < blockCount; x++)
            {
                // Position the stream at the correct address for this block.
                // NOTE: If our definition sizes are correct we should not need this.
                //bw.BaseStream.Position = address + (x * definitionSize);

                // Write the block definition fields to the stream.
                for (int i = 0; i < definition.Length; i++)
                    blocks[x][i].Write(bw, magic);
            }

            // Restore stream position.
            bw.BaseStream.Position = oldPos;

            // Write the tag_block header.
            bw.Write(blockCount);
            bw.Write(address + magic);
            if (Engine != EngineManager.HaloEngine.Halo2)
                bw.Write(0);
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
            // Initialize New Tag Block
            TagBlock NewBlock = new TagBlock(Name, definitionSize, maxBlockCount, paddingInterval, Engine, definition);

            // Copy Blocks
            for (int i = 0; i < blockCount; i++)
            {
                // Add New Block
                NewBlock.AddBlock();

                // Clone Fields
                for (int x = 0; x < definition.Length; x++)
                    NewBlock[i][x] = (MetaNode)blocks[i][x].Clone();
            }

            return NewBlock;
        }

        #endregion

        #region Indexers

        public MetaNode[] this[int blockIndex]
        {
            // Return Fields
            get 
            { 
                // Check to make sure the block index is valid.
                if (blockIndex < 0 || blockIndex >= blockCount)
                {
                    // Throw an exception.
                    throw new ArgumentOutOfRangeException(string.Format("blockIndex is out of range [{0}, {1})!", 0, blockCount));
                }

                // Return the block at the specified index.
                return blocks[blockIndex]; 
            }
            //set
            //{
            //    // Copy Fields
            //    for (int i = 0; i < definition.Length; i++)
            //        blocks[BlockIndex][i] = (MetaNode)value[i].Clone();
            //}
        }

        //public MetaNode this[int BlockIndex, int NodeIndex]
        //{
        //    // Return Field
        //    get { return blocks[BlockIndex][NodeIndex]; }

        //    // Set Field
        //    set { blocks[BlockIndex][NodeIndex] = (MetaNode)value.Clone(); }
        //}

        #endregion

        #region Object Members

        public override string ToString()
        {
            return "TagBlock: " + Name + ", BlockCount: " + blockCount.ToString() + ", BlockSize: " + definitionSize.ToString() + ", Alignment: " + paddingInterval.ToString() + ", MaxCount: " + maxBlockCount.ToString();
        }

        #endregion
    }
}
