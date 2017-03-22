using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class gldf : TagDefinition
    {
       public gldf() : base("gldf", "global_lighting", 8)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Light Variables", 144, 13, new MetaNode[] { 
               new Flags("Objects Affected", new string[] { "All", "Biped", "Vehicle", "Weapon", "Equipment", "Garbage", "Projectile", "Scenery", "Machine", "Control", "Light_Fixture", "Sound_Scenery", "Crate", "Creature" }, 32),
               new Value("Lightmap Brightness Offset", typeof(float)),
               new Value("Primary Min Lightmap Color R", typeof(float)),
               new Value("Primary Min Lightmap Color G", typeof(float)),
               new Value("Primary Min Lightmap Color B", typeof(float)),
               new Value("Primary Max Lightmap Color R", typeof(float)),
               new Value("Primary Max Lightmap Color G", typeof(float)),
               new Value("Primary Max Lightmap Color B", typeof(float)),
               new Value("Exclusion Angle From Up", typeof(float)),
               new TagBlock("Primary Light Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Value("Secondary Min Lightmap Color R", typeof(float)),
               new Value("Secondary Min Lightmap Color G", typeof(float)),
               new Value("Secondary Min Lightmap Color B", typeof(float)),
               new Value("Secondary Max Lightmap Color R", typeof(float)),
               new Value("Secondary Max Lightmap Color G", typeof(float)),
               new Value("Secondary Max Lightmap Color B", typeof(float)),
               new Value("Secondary Min Diffuse Sample R", typeof(float)),
               new Value("Secondary Min Diffuse Sample G", typeof(float)),
               new Value("Secondary Min Diffuse Sample B", typeof(float)),
               new Value("Secondary Max Diffuse Sample R", typeof(float)),
               new Value("Secondary Max Diffuse Sample G", typeof(float)),
               new Value("Secondary Max Diffuse Sample B", typeof(float)),
               new Value("Secondary Z-Axsis Rotation", typeof(float)),
               new TagBlock("Secondary Light Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Value("Ambient Min Lightmap Sample R", typeof(float)),
               new Value("Ambient Min Lightmap Sample G", typeof(float)),
               new Value("Ambient Min Lightmap Sample B", typeof(float)),
               new Value("Ambient Max Lightmap Sample R", typeof(float)),
               new Value("Ambient Max Lightmap Sample G", typeof(float)),
               new Value("Ambient Max Lightmap Sample B", typeof(float)),
               new TagBlock("Ambient Light Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Lightmap Shadows", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
           }),
           });
       }
    }
}
