using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class fog : TagDefinition
    {
       public fog() : base("fog ", "fog", 96)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Render_Only_Submerged_Geometry", "Extend_Infinitely_While_Visible", "Don't_Flood_Fill", "Aggressive_Flood_Fill", "Do_Not_Render", "Do_Not_Render_Unless_Submerged" }, 16),
           new Value("Priority", typeof(short)),
           new StringId("Global Material Name"),
           new Padding(4),
           new Value("Maximum Density", typeof(float)),
           new Value("Opaque Distance", typeof(float)),
           new Value("Opaque Depth", typeof(float)),
           new Value("Atmospheric Planar Depth Lower", typeof(float)),
           new Value("Atmospheric Planar Depth Upper", typeof(float)),
           new Value("Eye Offset Scale", typeof(float)),
           new Value("Color R", typeof(float)),
           new Value("Color G", typeof(float)),
           new Value("Color B", typeof(float)),
           new TagBlock("Patchy Fog", 52, 1, new MetaNode[] { 
               new Value("Color R", typeof(float)),
               new Value("Color G", typeof(float)),
               new Value("Color B", typeof(float)),
               new Padding(12),
               new Value("Density Lower", typeof(float)),
               new Value("Density Upper", typeof(float)),
               new Value("Distance Lower", typeof(float)),
               new Value("Distance Upper", typeof(float)),
               new Value("Min Depth Fraction", typeof(float)),
               new TagReference("Patchy_Fog", "fpch"),
           }),
           new TagReference("Background_Sound", "lsnd"),
           new TagReference("Sound_Enviroment", "snde"),
           new Value("Environment Damping Factor", typeof(float)),
           new Value("Background Sound Gain", typeof(float)),
           new TagReference("Enter_Sound", "snd!"),
           new TagReference("Exit_Sound", "snd!"),
           });
       }
    }
}
