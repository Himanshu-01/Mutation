using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class itmc : TagDefinition
    {
       public itmc() : base("itmc", "item_collection", 12)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Item Permutations", 16, 32, new MetaNode[] { 
               new Value("Weight", typeof(float)),
               new TagReference("Item", "item"),
               new StringId("Variant Name"),
           }),
           new Value("Unused Spawn Time   (in seconds, 0 = default)", typeof(uint)),
           });
       }
    }
}
