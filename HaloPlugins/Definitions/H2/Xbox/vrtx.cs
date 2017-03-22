using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class vrtx : TagDefinition
    {
       public vrtx() : base("vrtx", "vertex_shader", 12)
       {
           Fields.AddRange(new MetaNode[] {
           new HaloPlugins.Objects.Data.Enum("Platform", new string[] { "PC", "Xbox" }, 32),
           new TagBlock("Geometry Classifications", 28, 12, new MetaNode[] { 
               new Padding(4),
               new TagBlock("Compiled Shader", 2, -1, new MetaNode[] { 
                   new Value("Data", typeof(short)),
               }),
               new TagBlock("Code", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Padding(8),
           }),
           });
       }
    }
}
