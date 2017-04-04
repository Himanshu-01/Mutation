using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.Guerilla.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GuerillaPostProcessAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the block tha the associated method will postprocess.
        /// </summary>
        public string BlockName { get; private set; }

        /// <summary>
        /// Tags the method associated with this attribute as the post processing method for the block definition.
        /// </summary>
        /// <param name="blockName">Name of the block definition to be post processed.</param>
        public GuerillaPostProcessAttribute(string blockName)
        {
            // Initialize fields.
            this.BlockName = blockName;
        }
    }
}
