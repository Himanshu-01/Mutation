using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blam.Memory
{
    public class HashtableItem<T, K>
    {
        /// <summary>
        /// Gets or sets the T object value for this entry.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the hashcode of the item.
        /// </summary>
        public int Hashcode { get; set; }

        /// <summary>
        /// Gets or sets the next item in the linked list chain.
        /// </summary>
        public HashtableItem<T, K> Next { get; set; }

        /// <summary>
        /// Gets or sets the unique key for the item.
        /// </summary>
        public K Key { get; set; }

        /// <summary>
        /// Initializes a new HashtableItem instance using defualt values.
        /// </summary>
        public HashtableItem()
        {
            // Initialize the fields using default values.
            this.Value = default(T);
            this.Key = default(K);
            this.Hashcode = 0;
            this.Next = null;
        }

        /// <summary>
        /// Initializes a new HashtableItem instance using the provided parameters.
        /// </summary>
        /// <param name="value">The T object value.</param>
        /// <param name="hashcode">Hashcode of the T object value.</param>
        public HashtableItem(T value, K key, int hashcode)
        {
            // Save the parameters.
            this.Value = value;
            this.Key = key;
            this.Hashcode = hashcode;

            // Initialize the next pointer.
            this.Next = null;
        }
    }

    public class Hashtable<T, K>
    {
        /// <summary>
        /// Prototype to compare two objects in the hashtable.
        /// </summary>
        /// <param name="obj1">The source object.</param>
        /// <param name="obj2">The target object.</param>
        /// <returns>True if the objects are equal in value, false otherwise.</returns>
        public delegate bool HashtableCompareProc(object obj1, object obj2);

        /// <summary>
        /// Prototype to compute the hashcode of an object in the hashtable.
        /// </summary>
        /// <param name="obj">Object to compute the hashcode for.</param>
        /// <returns>Hashcode of the object provided.</returns>
        public delegate int HashtableHashcodeProc(object obj);


        private string name = "";
        /// <summary>
        /// Gets the name of the hashtable.
        /// </summary>
        public string Name { get { return this.name; } }

        private int key_size = 0;
        /// <summary>
        /// Gets the size of the key object for this hashtable.
        /// </summary>
        public int KeySize { get { return this.key_size; } }

        private int indice_count = 0;
        /// <summary>
        /// Gets the number of indices used to index the hashtable.
        /// </summary>
        public int IndiceCount { get { return this.indice_count; } }

        private int capacity = 0;
        /// <summary>
        /// Gets the maximum capacity of the hashtable.
        /// </summary>
        public int Capacity { get { return this.capacity; } }

        private HashtableHashcodeProc hashcode_proc = null;
        /// <summary>
        /// Gets the hashcode function used to compute the hashcodes for the objects in the hashtable.
        /// </summary>
        public HashtableHashcodeProc HashcodeProc { get { return this.hashcode_proc; } }

        private HashtableCompareProc compare_proc = null;
        /// <summary>
        /// Gets the compare function used to compare two objects in the hashtable.
        /// </summary>
        public HashtableCompareProc CompareProc { get { return this.compare_proc; } }

        /// <summary>
        /// Array used to index the HashtableItem structs based on the hashcode.
        /// </summary>
        private int[] indices;

        /// <summary>
        /// Internal varaible used to keep track of the nodes in the linked list since we are
        /// not allocating one continuous block of memory for this entire data structure.
        /// </summary>
        private HashtableItem<T, K>[] nodes;
        /// <summary>
        /// Head node of the linked list of HashtableItem structures.
        /// </summary>
        private HashtableItem<T, K> items = null;

        /// <summary>
        /// Constructs a new Hashtable with the provided parameters.
        /// </summary>
        /// <param name="name">Name of the hashtable.</param>
        /// <param name="key_size">Size of the key object.</param>
        /// <param name="indice_count">Number of indices used to index the hashtable.</param>
        /// <param name="capacity">Maximum capacity of the hashtable.</param>
        /// <param name="hashcode_proc">Function used to compute hashcodes for the objects in the hashtable.</param>
        /// <param name="compare_proc">Function used to compare objects in the hashtable.</param>
        public Hashtable(string name, int key_size, int indice_count, int capacity, HashtableHashcodeProc hashcode_proc, HashtableCompareProc compare_proc)
        {
            // Save the parameters.
            this.name = name;
            this.key_size = key_size;
            this.indice_count = indice_count;
            this.capacity = capacity;
            this.hashcode_proc = hashcode_proc;
            this.compare_proc = compare_proc;

            // Allocate the indice table.
            this.indices = new int[this.indice_count];

            // Allocate the list of hashtable items.
            this.nodes = new HashtableItem<T, K>[this.capacity];

            // Clear and initialize the hashtable.
            Clear();
        }

        /// <summary>
        /// Adds a value-key pair to the hashtable.
        /// </summary>
        /// <param name="value">Object to add to the hashtable.</param>
        /// <param name="key">Unique key object that corresponds with <paramref name="value"/></param>
        /// <returns>True if the item was added, false otherwise.</returns>
        public bool Add(T value, K key)
        {
            // Check if there are any empty items in the hashtable left.
            if (this.items == null)
                return false;

            // Compute the hashcode of the item.
            int hashcode = this.hashcode_proc(value);

            // Get the next unused HashtableItem structure.
            HashtableItem<T, K> item = this.items;
            item.Value = value;
            item.Key = key;
            item.Hashcode = hashcode;

            // Update the head node pointer.
            this.items = item.Next;

            // Compute the index into the indice table.
            int index = hashcode % this.indice_count;

            // Update the next pointer for the new HashtableItem.
            item.Next = this.indices[index] == -1 ? null : this.nodes[this.indices[index]];

            // Update the indice table to point to our new HashtableItem.
            this.indices[index] = Array.IndexOf(this.nodes, item);

            // The item was successfully added.
            return true;
        }

        /// <summary>
        /// Clears all the items from the hashtable.
        /// </summary>
        public void Clear()
        {
            // Clear the indice array.
            for (int i = 0; i < this.indice_count; i++)
                this.indices[i] = -1;

            // Set the head node pointer to null.
            this.items = null;

            // Initialize the linked list of HashtableItem nodes.
            for (int i = 0; i < this.Capacity; i++)
            {
                // Initialize the node.
                this.nodes[i] = new HashtableItem<T, K>();
                this.nodes[i].Value = default(T);
                this.nodes[i].Key = default(K);
                this.nodes[i].Hashcode = 0;
                this.nodes[i].Next = this.items;

                // Set the new head node to this HashtableItem structure.
                this.items = this.nodes[i];
            }
        }

        public T ContainsValue(T value, ref K key)
        {
            // Try to find a HashtableItem with matching value.
            HashtableItem<T, K> item = GetEntry(value);
            if (item == null)
                return default(T);

            // Copy the key object.
            key = item.Key;

            // Return the item value.
            return item.Value;
        }

        private HashtableItem<T, K> GetEntry(T value)
        {
            // Compute the hashcode of the object.
            int hashcode = this.hashcode_proc(value);

            // Compute the index into the indice table.
            int index = hashcode % this.indice_count;

            // Check if there is a HashtableItem at this index.
            if (this.indices[index] == -1)
                return null;

            // Loop through the bucket and search for an entry with matching value.
            for (HashtableItem<T, K> item = this.nodes[this.indices[index]]; 
                item != null; item = item.Next)
            {
                // Check if the hashcode matches.
                if (item.Hashcode != hashcode)
                    continue;

                // Check if the objects matching using the compare proc.
                if (this.compare_proc(value, item.Value) == true)
                    return item;
            }

            // An entry with matching value was not found, return null;
            return null;
        }

        public bool GetKey(T value, ref K key)
        {
            // Satisfy the compiler.
            //key = default(K);

            // Try to find a HashtableItem with matching value.
            HashtableItem<T, K> item = GetEntry(value);
            if (item == null)
                return false;

            // Copy the key object and return true.
            key = item.Key;
            return true;
        }
    }
}
