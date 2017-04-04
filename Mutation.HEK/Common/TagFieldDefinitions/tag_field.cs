using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mutation.HEK.Guerilla;

namespace Mutation.HEK.Common.TagFieldDefinitions
{
    /// <summary>
    /// Interface that provides that functions needed for any class that represents the
    /// definition of a tag_field.
    /// </summary>
    public interface ITagDefinition
    {
        /// <summary>
        /// Reads the tag_field definition from the stream.
        /// </summary>
        /// <param name="h2LangLib"></param>
        /// <param name="reader"></param>
        void Read(IntPtr h2LangLib, BinaryReader reader);
    }

    /// <summary>
    /// Acts as an empty definition for tag_field's that do not have a definition.
    /// </summary>
    public class NullDefinition : tag_field
    {
        public override void Read(IntPtr h2LangLib, BinaryReader reader)
        {
        }
    }

    public class tag_field : ITagDefinition
    {
        public field_type type;
        //PAD16
        public int name_address;
        public int definition_address;
        public int group_tag;

        private string name;
        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        public virtual void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read all the fields from the stream.
            this.type = (field_type)reader.ReadInt16();
            reader.BaseStream.Position += 2;
            this.name_address = reader.ReadInt32();
            this.definition_address = reader.ReadInt32();
            this.group_tag = reader.ReadInt32();

            // Read the name string.
            this.name = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.name_address);
        }

        public override string ToString()
        {
            return string.Format("Type: {0} Name: {1} DefinitionAddress: {2} GroupTag: {3}",
                this.type.ToString(), this.name, "0x" + this.definition_address.ToString("x"), Guerilla.Guerilla.GroupTagToString(this.group_tag));
        }
    }
}
