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
    public class phys : TagDefinition
    {
       public phys() : base("phys", "physics", 116)
       {
           Fields.AddRange(new MetaNode[] {
           new Value("Radius", typeof(float)),
           new Value("Moment Scale", typeof(float)),
           new Value("Mass", typeof(float)),
           new Value("Center of mass x", typeof(float)),
           new Value("Center of mass y", typeof(float)),
           new Value("Center of mass z", typeof(float)),
           new Value("Density", typeof(float)),
           new Value("Gravity scale", typeof(float)),
           new Value("Ground friction", typeof(float)),
           new Value("Ground Depth", typeof(float)),
           new Value("Ground damp function", typeof(float)),
           new Value("Normal K1", typeof(float)),
           new Value("Normal K0", typeof(float)),
           new Padding(4),
           new Value("Water Friction", typeof(float)),
           new Value("Water Depth", typeof(float)),
           new Value("Water Density", typeof(float)),
           new Padding(4),
           new Value("Air friction", typeof(float)),
           new Padding(4),
           new Value("xx movement", typeof(float)),
           new Value("yy movement", typeof(float)),
           new Value("zz movement", typeof(float)),
           new TagBlock("Internal Matrix", 36, 2, new MetaNode[] { 
               new Value("yy+zz", typeof(float)),
               new Value("-xy", typeof(float)),
               new Value("-zx", typeof(float)),
               new Value("-xy", typeof(float)),
               new Value("zz+xx", typeof(float)),
               new Value("-yz", typeof(float)),
               new Value("-zx", typeof(float)),
               new Value("-yz", typeof(float)),
               new Value("xx+yy", typeof(float)),
           }),
           new TagBlock("PoweredMass Points", 60, 32, new MetaNode[] { 
               new ShortString("Name", StringType.Asci),
               new Flags("flags", new string[] { "ground_friction", "water_friction", "air_friction", "water_lift", "air_lift", "thrust", "antigrav", "Gets_Damage_from_region" }, 32),
               new Value("antigrav strength", typeof(float)),
               new Value("antigrav offset", typeof(float)),
               new Value("antigrav height", typeof(float)),
               new Value("antigrav damp fraction", typeof(float)),
               new Value("antigrav normal k1", typeof(float)),
               new Value("antigrav normal k0", typeof(float)),
           }),
           new TagBlock("Mass Points", 128, 32, new MetaNode[] { 
               new ShortString("Name", StringType.Asci),
               new Value("Powered Mass Point Index", typeof(short)),
               new Value("Model Node", typeof(short)),
               new Flags("flags", new string[] { "Metallic" }, 32),
               new Value("Relative Mass", typeof(float)),
               new Value("Mass", typeof(float)),
               new Value("Relative Density", typeof(float)),
               new Value("Density", typeof(float)),
               new Value("Pos X", typeof(float)),
               new Value("Pos Y", typeof(float)),
               new Value("Pos Z", typeof(float)),
               new Value("Forward i", typeof(float)),
               new Value("Forward j", typeof(float)),
               new Value("Forward k", typeof(float)),
               new Value("Up i", typeof(float)),
               new Value("Up j", typeof(float)),
               new Value("Up k", typeof(float)),
               new HaloPlugins.Objects.Data.Enum("friction type", new string[] { "point", "forward", "left", "up" }, 32),
               new Value("friction parallel scale", typeof(float)),
               new Value("friction perependicular scale", typeof(float)),
               new Value("radius", typeof(float)),
               new Padding(20),
           }),
           });
       }
    }
}
