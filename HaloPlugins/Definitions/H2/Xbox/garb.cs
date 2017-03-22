using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class garb : TagDefinition
    {
       public garb() : base("garb", "garbage", 468)
       {
           // Item
           Fields.AddRange(new item().Fields.ToArray());

           // Garb
           Fields.AddRange(new MetaNode[] {
           new Padding(168),
           });
       }
    }
}
