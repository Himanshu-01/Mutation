using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    public enum HiddenFieldType
    {
        /// <summary>
        /// The field is hidden from the basic gui editor, but available for editing in the advanced view.
        /// </summary>
        AdvancedViewOnly,
        /// <summary>
        /// The field is not editable regardless of what mode the gui editor is in.
        /// </summary>
        NonEditable
    }

    public class EditorOptionsAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating if the field should be hidden from the editor gui.
        /// </summary>
        public HiddenFieldType Hidden { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating if the field will appear as read only in the editor gui.
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}
