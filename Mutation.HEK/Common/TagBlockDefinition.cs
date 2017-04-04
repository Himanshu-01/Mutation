using Mutation.HEK.Common.TagFieldDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.HEK.Common
{
    public class TagBlockDefinition : ITagDefinition
    {
        /// <summary>
        /// Gets the tag_block_defintion struct this object encapsulates.
        /// </summary>
        public tag_block_definition s_tag_block_definition;// { get; set; }

        /// <summary>
        /// Gets the array of tag_field_set structs the tag_blocK_definition
        /// </summary>
        public tag_field_set[] TagFieldSets { get; set; }

        /// <summary>
        /// Gets a 2-dimensional array that contains sets of tag_field's for each tag_field_set in the tag_block_definition struct.
        /// </summary>
        public List<tag_field>[] TagFields { get; set; }

        /// <summary>
        /// Gets an index into the TagFieldSets array for the latest tag field set.
        /// </summary>
        public int TagFieldSetLatestIndex { get; set; }

        /// <summary>
        /// Gets a list of TagBlockDefinition's that reference this TagBlockDefinition instance.
        /// </summary>
        public List<TagBlockDefinition> References { get; set; }

        /// <summary>
        /// Gets a boolean indicating if this TagBlockDefinition is a tag group or not.
        /// </summary>
        public bool IsTagGroup { get; set; }

        public TagBlockDefinition()
        {
            // Initialize the tag_block_definition.
            this.s_tag_block_definition = new tag_block_definition();

            // Initialize fields.
            this.References = new List<TagBlockDefinition>();
            this.IsTagGroup = false;
        }

        public void Read(IntPtr h2LangLib, System.IO.BinaryReader reader)
        {

        }

        public void AddReference(TagBlockDefinition parent)
        {
            // Check if the referencing defintion is already in the references list.
            if (this.References.Contains(parent) == true)
            {
                // This shouldn't really happen.
            }

            // Add the owner reference to the references list.
            this.References.Add(parent);
        }

        public int GetFieldSetIndexClosestToH2Xbox()
        {
            // Check the name of the block and return the field_set index that most closely represents the layout for h2 xbox.
            int LatestFieldSet = 0;
            switch (this.s_tag_block_definition.Name)
            {
                case "materials_block":
                    LatestFieldSet = 0;
                    if (this.TagFields[LatestFieldSet].Count == 5)
                    {
                        this.s_tag_block_definition.Name = "phmo_materials_block";
                        //definitionName = "phmo_materials_block";
                    }
                    break;
                case "hud_globals_block":
                case "global_new_hud_globals_struct_block":
                case "sound_promotion_parameters_struct_block":
                case "sound_gestalt_promotions_block":
                case "sound_block":
                case "tag_block_index_struct_block":
                case "vertex_shader_classification_block":
                    LatestFieldSet = 0;
                    break;
                case "model_block":
                    LatestFieldSet = 1;
                    break;
                case "animation_pool_block":
                    LatestFieldSet = 4;
                    break;
                default:
                    //reader.BaseStream.Seek(field_set_latest_address, SeekOrigin.Begin);
                    //var latestFieldSet = new tag_field_set(reader,
                    //    Guerilla.PostprocessFunctions.Where(x => x.Key == definitionName)
                    //        .Select(x => x.Value)
                    //        .FirstOrDefault());
                    //LatestFieldSet = latestFieldSet;
                    break;
            }

            // Return the new field set index.
            return LatestFieldSet;
        }
    }
}
