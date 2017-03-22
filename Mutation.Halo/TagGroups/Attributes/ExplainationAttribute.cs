using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ExplainationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the title for the explaination block.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Explaination string for the explaination block.
        /// </summary>
        public string Explaination { get; set; }
    }
}
