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
        void PreProcessDefinition();

        void PostProcessDefinition();

        void ReadDefinition(BinaryReader reader);

        void WriteDefinition(BinaryWriter writer);
    }
}
