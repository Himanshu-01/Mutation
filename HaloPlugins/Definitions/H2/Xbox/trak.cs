using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class trak : TagDefinition
    {
       public trak() : base("trak", "camera_track", 12)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(4),
           new TagBlock("Control Points", 28, 16, new MetaNode[] { 
               new Value("Pos i", typeof(float)),
               new Value("Pos j", typeof(float)),
               new Value("Pos k", typeof(float)),
               new Value("Orientation i", typeof(float)),
               new Value("Orientation j", typeof(float)),
               new Value("Orientation k", typeof(float)),
               new Value("Orientation w", typeof(float)),
           }),
           });
       }
    }
}
