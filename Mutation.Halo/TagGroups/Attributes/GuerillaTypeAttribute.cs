using Mutation.HEK.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class GuerillaTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets the corresponding guerilla <see cref="field_type"/> this field encapsulates.
        /// </summary>
        public field_type FieldType { get; private set; }

        public GuerillaTypeAttribute(field_type fieldType)
        {
            // Initialize fields.
            this.FieldType = fieldType;
        }
    }
}
