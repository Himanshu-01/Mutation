using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class vehc : TagDefinition
    {
       public vehc() : base("vehc", "vehicle_collection", 12)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Vehicle Permutations", 16, 32, new MetaNode[] { 
               new Value("Weight", typeof(float)),
               new TagReference("Vehicle", "vehi"),
               new StringId("Variant Name"),
           }),
           new Value("Unused Spawn Time    (in seconds, 0 = default)", typeof(uint)),
           });
       }
    }
}
