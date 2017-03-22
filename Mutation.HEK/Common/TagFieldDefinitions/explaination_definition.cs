using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutation.HEK.Guerilla;

namespace Mutation.HEK.Common.TagFieldDefinitions
{
    public class explaination_definition : tag_field
    {
        /// <summary>
        /// Gets the explaination string for the next field(s).
        /// </summary>
        public string Explaination { get; private set; }

        public override void Read(IntPtr h2LangLib, System.IO.BinaryReader reader)
        {
            // Read the tag field data.
            base.Read(h2LangLib, reader);

            // Read the explaination string.
            this.Explaination = Guerilla.Guerilla.ReadString(h2LangLib, reader, this.definition_address);
        }
    }
}
