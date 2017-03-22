using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class scen : TagDefinition
    {
       public scen() : base("scen", "scenery", 196)
       {
           // Obje
           Fields.AddRange(new obje().Fields.ToArray());

           // Scen
           Fields.AddRange(new MetaNode[] {
           new HaloPlugins.Objects.Data.Enum("Pathfinding Policy", new string[] { "Cut-Out", "Static", "Dynamic", "None" }, 16),
           new Flags("flags", new string[] { "Physically_Simulates" }, 16),
           new HaloPlugins.Objects.Data.Enum("Lightmapping Policy", new string[] { "Per-Vertex", "Per-Pixel_(Not_Implemented)", "Dynamic" }, 32),
           });
       }
    }
}
