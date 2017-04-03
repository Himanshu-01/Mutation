using Mutation.Halo.TagGroups.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.FieldTypes
{
    [GuerillaType(HEK.Common.field_type._field_explanation)]
    public class Explanation
    {
        /// <summary>
        /// Name of the explanation block.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The explanation.
        /// </summary>
        public string Explaination { get; private set; }

        /// <summary>
        /// Creates a new Explanation field using the info provided.
        /// </summary>
        /// <param name="name">Name of the explanation block.</param>
        /// <param name="explanation">Explanation details.</param>
        public Explanation(string name = "", string explanation = "")
        {
            // Initialize fields.
            this.Name = name;
            this.Explaination = explanation;
        }
    }
}
