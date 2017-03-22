using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation.Halo.TagGroups
{
    public interface IMetaDefinition
    {
        void Read(BinaryReader reader);

        void Write(BinaryWriter writer);
    }
}
