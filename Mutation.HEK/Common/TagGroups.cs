using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.HEK.Common
{
    #region field_type Enum

    public enum field_type : short
    {
        _field_string,
        _field_long_string,
        _field_string_id,
        _field_old_string_id,
        _field_char_integer,
        _field_short_integer,
        _field_long_integer,
        _field_angle,
        _field_tag,
        _field_char_enum,
        _field_enum,
        _field_long_enum,
        _field_long_flags,
        _field_word_flags,
        _field_byte_flags,
        _field_point_2d,
        _field_rectangle_2d,
        _field_rgb_color,
        _field_argb_color,
        _field_real,
        _field_real_fraction,
        _field_real_point_2d,
        _field_real_point_3d,
        _field_real_vector_2d,
        _field_real_vector_3d,
        _field_real_quaternion,
        _field_real_euler_angles_2d,
        _field_real_euler_angles_3d,
        _field_real_plane_2d,
        _field_real_plane_3d,
        _field_real_rgb_color,
        _field_real_argb_color,
        _field_real_hsv_color,
        _field_real_ahsv_color,
        _field_short_bounds,
        _field_angle_bounds,
        _field_real_bounds,
        _field_real_fraction_bounds,
        _field_tag_reference,
        _field_block,
        _field_long_block_flags,
        _field_word_block_flags,
        _field_byte_block_flags,
        _field_char_block_index1,
        _field_char_block_index2,
        _field_short_block_index1,
        _field_short_block_index2,
        _field_long_block_index1,
        _field_long_block_index2,
        _field_data,
        _field_vertex_buffer,
        _field_array_start,
        _field_array_end,
        _field_pad,
        _field_useless_pad,
        _field_skip,
        _field_explanation,
        _field_custom,
        _field_struct,
        _field_terminator,

        // I added this field type to the enum to handle lone IDs since they are a cache only thing.
        _field_datum_index,
        _field_enum_option,

        _field_type_max,
    }

    #endregion

    #region s_tag_field_set_version

    public struct s_tag_field_set_version
    {
        public int fields_address;
        public int index;
        public int upgrade_proc;
        //public int i1;
        public int size_of;

        public void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read all the fields from the stream.
            this.fields_address = reader.ReadInt32();
            this.index = reader.ReadInt32();
            this.upgrade_proc = reader.ReadInt32();
            reader.BaseStream.Position += 4;
            this.size_of = reader.ReadInt32();
        }
    }

    #endregion

    #region tag_field_set

    public struct tag_field_set
    {
        public s_tag_field_set_version version;
        public int size;
        public int alignment_bit;
        public int parent_version_index;
        public int fields_address;
        public int size_string_address;
        //byteswap definition
        //runtime data

        public int address;

        public string size_string;
        /// <summary>
        /// Gets the sizeof string for this tag_field_set.
        /// </summary>
        public string SizeString { get { return this.size_string; } }

        public void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read all the fields from the stream.
            this.address = (int)reader.BaseStream.Position + Guerilla.Guerilla.BaseAddress;
            this.version = new s_tag_field_set_version();
            this.version.Read(h2LangLib, reader);
            this.size = reader.ReadInt32();
            this.alignment_bit = reader.ReadInt32();
            this.parent_version_index = reader.ReadInt32();
            this.fields_address = reader.ReadInt32();
            this.size_string_address = reader.ReadInt32();

            // Read the size_of string.
            this.size_string = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.size_string_address);
        }
    }

    #endregion

    #region tag_block_definition

    public struct tag_block_definition
    {
        public int address;
        public int display_name_address;
        public int name_address;
        public int flags;
        public int maximum_element_count;
        public int maximum_element_count_string_address;
        public int field_sets_address;
        public int field_set_count;
        public int field_set_latest_address;
        //public int i1;
        public int postprocess_proc;
        public int format_proc;
        public int generate_default_proc;
        public int dispose_element_proc;

        private string display_name;
        /// <summary>
        /// Gets the display name of the tag_block_definition.
        /// </summary>
        public string DisplayName { get { return this.display_name; } }

        private string name;
        /// <summary>
        /// Gets the name of the tag_block_definition.
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        private string maximum_element_count_str;
        /// <summary>
        /// Gets the maximum number of elements for this tag_block_definition.
        /// </summary>
        public string MaximumElementCount { get { return this.maximum_element_count_str; } }

        public void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read all the fields from the stream.
            //this.address = (int)reader.BaseStream.Position;
            this.display_name_address = reader.ReadInt32();
            this.name_address = reader.ReadInt32();
            this.flags = reader.ReadInt32();
            this.maximum_element_count = reader.ReadInt32();
            this.maximum_element_count_string_address = reader.ReadInt32();
            this.field_sets_address = reader.ReadInt32();
            this.field_set_count = reader.ReadInt32();
            this.field_set_latest_address = reader.ReadInt32();
            reader.BaseStream.Position += 4;
            this.postprocess_proc = reader.ReadInt32();
            this.format_proc = reader.ReadInt32();
            this.generate_default_proc = reader.ReadInt32();
            this.dispose_element_proc = reader.ReadInt32();

            // Read the display name and name strings.
            this.display_name = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.display_name_address);
            this.name = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.name_address);
            this.maximum_element_count_str = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.maximum_element_count_string_address);
        }
    }

    #endregion

    #region tag_group

    public struct tag_group
    {
        public int name_address;
        public int flags;
        public int group_tag;
        public int parent_group_tag;
        public short version;
        public byte initialized;
        //public byte b1;
        public int postprocess_proc;
        public int save_postprocess_proc;
        public int postprocess_for_sync_proc;
        //public int i1;
        public int definition_address;
        public int[] child_group_tags;
        public short childs_count;
        //public short s1;
        public int default_tag_path_address;

        private string name;
        /// <summary>
        /// Gets the name of the tag_group.
        /// </summary>
        public string Name { get { return this.name; } }

        /// <summary>
        /// Gets the string representation of the group_tag;
        /// </summary>
        public string GroupTag { get { return Guerilla.Guerilla.GroupTagToString(this.group_tag); } }

        /// <summary>
        /// Gets the string representation of the parent_group_tag.
        /// </summary>
        public string ParentGroupTag { get { return Guerilla.Guerilla.GroupTagToString(this.parent_group_tag); } }

        /// <summary>
        /// Gets the tag block definition this tag goup points to.
        /// </summary>
        public TagBlockDefinition Definition { get; set; }

        public void Read(IntPtr h2LangLib, BinaryReader reader)
        {
            // Read all the fields from the stream.
            this.name_address = reader.ReadInt32();
            this.flags = reader.ReadInt32();
            this.group_tag = reader.ReadInt32();
            this.parent_group_tag = reader.ReadInt32();
            this.version = reader.ReadInt16();
            this.initialized = reader.ReadByte();
            reader.BaseStream.Position++;
            this.postprocess_proc = reader.ReadInt32();
            this.save_postprocess_proc = reader.ReadInt32();
            this.postprocess_for_sync_proc = reader.ReadInt32();
            reader.BaseStream.Position += 4;
            this.definition_address = reader.ReadInt32();
            this.child_group_tags = new int[16];
            for (int i = 0; i < 16; i++)
                this.child_group_tags[i] = reader.ReadInt32();
            this.childs_count = reader.ReadInt16();
            reader.BaseStream.Position += 2;
            this.default_tag_path_address = reader.ReadInt32();

            // Read the tag_group name.
            this.name = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.name_address);
        }
    }

    #endregion
}
