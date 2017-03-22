using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups.IO
{
    public class VirtualMemoryStream : MemoryStream
    {
        /// <summary>
        /// Gets the base memory address for the memory stream.
        /// </summary>
        public long BaseAddress { get; private set; }

        /// <summary>
        /// Gets or sets the current position in the stream.
        /// </summary>
        public override long Position
        {
            get
            {
                // Adjust the offset.
                return base.Position + BaseAddress;
            }
            set
            {
                // Adjust the offset.
                base.Position = value - BaseAddress;
            }
        }
    }
}
