using LayoutViewer.Guerilla.Attributes;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.Guerilla.Tags
{
    public class bitmap_block
    {
        [GuerillaPreProcess("bitmap_block")]
        public static void PreProcess(TagBlockDefinition tagBlock)
        {
            // Get the correct field set that most closely matches h2x.
            List<tag_field> fields = tagBlock.TagFields[tagBlock.TagFieldSetLatestIndex];

            // Remove the WDP fields that were added for vista.
            fields.RemoveRange(31, 5);
        }
    }
}
