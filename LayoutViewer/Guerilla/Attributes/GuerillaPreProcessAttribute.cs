using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayoutViewer.Guerilla.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GuerillaPreProcessAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the block definition that the associated method will preprocess.
        /// </summary>
        public string BlockName { get; private set; }

        /// <summary>
        /// Tags the method associated with this attribute as the preprocess method for the block definition <see cref="blockName"/>.
        /// </summary>
        /// <param name="blockName">Name of the block definition this method is associated with.</param>
        public GuerillaPreProcessAttribute(string blockName)
        {
            // Save fields.
            this.BlockName = blockName;
        }
    }
}
