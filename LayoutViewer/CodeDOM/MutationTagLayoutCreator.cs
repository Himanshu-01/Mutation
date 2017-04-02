using Mutation.HEK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public class MutationTagLayoutCreator
    {
        /// <summary>
        /// Gets the tag block definition his layout creator is creating a layout for.
        /// </summary>
        public TagBlockDefinition TagBlockDefinition { get; set; }

        /// <summary>
        /// Gets the field name as found in guerilla.
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// Gets a code safe type name for tag block definition.
        /// </summary>
        public string CodeTypeName { get; private set; }

        public MutationTagLayoutCreator(TagBlockDefinition tagBlockDefinition)
        {
            // Initialie fields.
            this.TagBlockDefinition = tagBlockDefinition;
        }

        public void CreateTagLayout()
        {

        }

        public void WriteToFile(string folder)
        {

        }
    }
}
