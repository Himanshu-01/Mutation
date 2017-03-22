using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class pphy : TagDefinition
    {
       public pphy() : base("pphy", "point_physics", 64)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "unused", "Collides_With_Structures", "Collides_With_Water_Surface", "Uses_Simple_Wind", "Uses_Dampened_Wind", "No_Gravity" }, 32),
           new Value("Density", typeof(float)),
           new Value("Air Friction", typeof(float)),
           new Value("Water Friction", typeof(float)),
           new Padding(16),
           new Value("Surface Friction", typeof(float)),
           new Value("Elasticity", typeof(float)),
           new Padding(24),
           });
       }
    }
}
