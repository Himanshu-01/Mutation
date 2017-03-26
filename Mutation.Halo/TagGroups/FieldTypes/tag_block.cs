using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.FieldTypes
{
#warning tag_block not fully implemented
    [GuerillaType(field_type._field_block)]
    [GuerillaType(field_type._field_struct)]
    public class tag_block<T> : IList<T> where T : IMetaDefinition, ICloneable
    {
        /// <summary>
        /// Gets the size of the tag_block struct.
        /// </summary>
        public const int kSizeOf = 8;

        /// <summary>
        /// Gets the number of child blocks this tag_block currently has.
        /// </summary>
        public int count;
        /// <summary>
        /// Gets the address of the tag_block data.
        /// </summary>
        public int address;
        /// <summary>
        /// Gets the field type of the underlying tag block definition.
        /// </summary>
        public readonly Type definition = typeof(T);

        /// <summary>
        /// Gets an array of of blocks of type <see cref="definition"/>.
        /// </summary>
        public T[] blocks;

        /// <summary>
        /// Gets the size of the underlying tag block definition.
        /// </summary>
        public int DefinitionSize
        {
            get
            {
                // Check if there is a TagBlockDefinitionAttribute on this type.
                object[] attribute = this.definition.GetType().GetCustomAttributes(typeof(TagBlockDefinitionAttribute), false);
                return (attribute[0] as TagBlockDefinitionAttribute).SizeOf;
            }
        }

        /// <summary>
        /// Gets the alignment for the underlying tag block definition.
        /// </summary>
        public int Alignment
        {
            get
            {
                // Check if there is a TagBlockDefinitionAttribute on this type.
                object[] attribute = this.definition.GetType().GetCustomAttributes(typeof(TagBlockDefinitionAttribute), false);
                return (attribute[0] as TagBlockDefinitionAttribute).Alignment;
            }
        }

        /// <summary>
        /// Gets the maximum number of blocks this tag_block can have.
        /// </summary>
        public int MaxBlockCount
        {
            get
            {
                // Check if there is a TagBlockDefinitionAttribute on this type.
                object[] attribute = this.definition.GetType().GetCustomAttributes(typeof(TagBlockDefinitionAttribute), false);
                return (attribute[0] as TagBlockDefinitionAttribute).MaxBlockCount;
            }
        }

        /// <summary>
        /// Initializes a new tag_block struct using default values.
        /// </summary>
        //public tag_block()
        //{
        //    this.count = 0;
        //    this.address = 0;
        //    this.blocks = new T[this.count];
        //    this.definition = typeof(T);
        //}

        #region IList Members

        public int IndexOf(T item)
        {
            // Loop through the array and search for the element specified.
            for (int i = 0; i < this.count; i++)
            {
                // Check if the blocks are equal.
                if (this.blocks[i].Equals(item) == true)
                    return i;
            }

            // No matching block was found.
            return -1;
        }

        public void Insert(int index, T item)
        {
            // Check if the index is within the bounds of the array.
            if (index < 0 || index >= this.count)
                throw new ArgumentException("\"index\" is out of the bounds of the tag_block array!");

            // Allocate a new block array.
            T[] newBlocks = new T[this.count + 1];

            // Copy the old blocks and the new block into the new block array.
            Array.Copy(this.blocks, 0, newBlocks, 0, index);
            newBlocks[index] = item;
            Array.Copy(this.blocks, index, newBlocks, index + 1, count - index);

            // Set the block array to the new array we just made and adjust the count.
            this.count++;
            this.blocks = newBlocks;
        }

        public void RemoveAt(int index)
        {
            // Check if the index is within the bounds of the array.
            if (index < 0 || index >= this.count)
                throw new ArgumentException("\"index\" is out of the bounds of the tag_block array!");

            // Allocate a new block array.
            T[] newBlocks = new T[this.count - 1];

            // Loop through the block array and copy each block except for the one specified.
            for (int i = 0, x = 0; i < this.count && i != index; i++)
            {
                // Copy the block.
                newBlocks[x] = this.blocks[i];
            }

            // Set the block array to the new array we just made and adjust the count.
            this.count--;
            this.blocks = newBlocks;
        }

        public T this[int index]
        {
            get
            {
                // Check if the index is within the bounds of the array.
                if (index < 0 || index >= this.count)
                    throw new ArgumentException("\"index\" is out of the bounds of the tag_block array!");

                // Return the block instance.
                return this.blocks[index];
            }
            set
            {
                // Check if the index is within the bounds of the array.
                if (index < 0 || index >= this.count)
                    throw new ArgumentException("\"index\" is out of the bounds of the tag_block array!");

                // Set the block instance.
                this.blocks[index] = value;
            }
        }

        public void Add(T item)
        {
            // Check if we can add another block.
            if (this.count == this.MaxBlockCount)
                throw new Exception("tag_block has reached its maximum capacity!");

            // Resize the array.
            Array.Resize<T>(ref this.blocks, this.count + 1);

            // Save the new block.
            this.blocks[this.count++] = item;
        }

        public void Clear()
        {
            // Clear the block array.
            this.count = 0;
            this.blocks = new T[this.count];
        }

        public bool Contains(T item)
        {
            // Loop through each block and see if it's equal to the block specified.
            foreach (T block in this.blocks)
            {
                // Compare the blocks.
                if (block.Equals(item) == true)
                    return true;
            }

            // The specified block was not found in the array.
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            // Loop through the blocks and copy as many as we can.
            for (int i = arrayIndex, x = 0; i < array.Length && x < this.count; i++, x++)
            {
                // Copy the block.
                array[i] = (T)this.blocks[x].Clone();
            }
        }

        public int Count
        {
            get { return this.count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            // Check if the specified block exists in the array.
            int index = IndexOf(item);
            if (index == -1)
                return false;

            // Allocate a new block array.
            T[] newBlocks = new T[this.count - 1];

            // Loop through the block array and copy each block except for the one specified.
            for (int i = 0, x = 0; i < this.count && i != index; i++)
            {
                // Copy the block.
                newBlocks[x] = this.blocks[i];
            }

            // Set the block array to the new array we just made and adjust the count.
            this.count--;
            this.blocks = newBlocks;

            // Done, the item was successfully removed.
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this.blocks.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.blocks.GetEnumerator();
        }

        #endregion
    }
}
