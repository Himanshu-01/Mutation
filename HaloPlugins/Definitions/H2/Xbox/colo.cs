using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using HaloPlugins.Objects.Strings;

namespace HaloPlugins.Xbox
{
    public class colo : TagDefinition
    {
       public colo() : base("colo", "color_table", 8)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Colors", 48, 512, new MetaNode[] { 
               new ShortString("Name", StringType.Asci),
               new Value("Alpha", typeof(float)),
               new Value("Red", typeof(float)),
               new Value("Green", typeof(float)),
               new Value("Blue", typeof(float)),
           }),
           });
       }
    }
}
