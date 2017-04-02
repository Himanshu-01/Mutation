using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.CodeDOM
{
    public class ProcessedFieldType
    {
        /// <summary>
        /// Gets or sets the guerilla field name for this object.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the code type name for this object.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Definition address of the guerilla field this type was created from.
        /// </summary>
        public int DefinitionAddress { get; set; }

        /// <summary>
        /// Gets or sets the code DOM object for this object.
        /// </summary>
        public CodeTypeDeclaration CodeDOMObject { get; set; }

        public ProcessedFieldType(string fieldName, string typeName, int definitionAddress, CodeTypeDeclaration domObject)
        {
            // Initialize fields.
            this.FieldName = fieldName;
            this.TypeName = typeName;
            this.DefinitionAddress = definitionAddress;
            this.CodeDOMObject = domObject;
        }
    }
}
