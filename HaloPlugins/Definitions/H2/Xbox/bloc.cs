using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class bloc : TagDefinition
    {
       public bloc() : base("bloc", "crate", 192)
       {
           // Obje
           Fields.AddRange(new obje().Fields.ToArray());

           // Bloc
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Does_Not_Block_AOE" }, 32),
           });
       }
    }
}
