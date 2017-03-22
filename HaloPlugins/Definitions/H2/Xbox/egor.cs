using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class egor : TagDefinition
    {
       public egor() : base("egor", "screen_effect", 144)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(64),
           new TagReference("Shader", "shad"),
           new Padding(64),
           new TagBlock("Pass References", 172, 8, new MetaNode[] { 
               new Padding(8),
               new Value("Layer Pass Index", typeof(short)),
               new Value("If Primary Equals 0 And Secondary Equals 0", typeof(short)),
               new Value("If Primary Greater Than 0 And Secondary Equals 0", typeof(short)),
               new Value("If Primary Equals 0 And Secondary Greater Than 0", typeof(short)),
               new Value("If Primary Greater Than 0 And Secondary Greater Than 0", typeof(short)),
               new Padding(62),
               new HaloPlugins.Objects.Data.Enum("Stage 0 Mode", new string[] { "Default", "Viewport_Normalized", "Viewport_Relative", "Frame_Buffer_Relative", "Zero" }, 16),
               new HaloPlugins.Objects.Data.Enum("Stage 1 Mode", new string[] { "Default", "Viewport_Normalized", "Viewport_Relative", "Frame_Buffer_Relative", "Zero" }, 16),
               new HaloPlugins.Objects.Data.Enum("Stage 2 Mode", new string[] { "Default", "Viewport_Normalized", "Viewport_Relative", "Frame_Buffer_Relative", "Zero" }, 16),
               new HaloPlugins.Objects.Data.Enum("Stage 3 Mode", new string[] { "Default", "Viewport_Normalized", "Viewport_Relative", "Frame_Buffer_Relative", "Zero" }, 16),
               new TagBlock("Advanced Control", 0, 1, new MetaNode[] { 
               }),
               new HaloPlugins.Objects.Data.Enum("Target", new string[] { "Frame_Buffer", "Texaccum", "Texaccum_Small", "Texaccum_Tiny", "Copy_FB_To_Texaccum" }, 32),
               new Padding(64),
               new TagBlock("Convolution", 92, 2, new MetaNode[] { 
                   new Padding(64),
                   new Flags("Flags", new string[] { "Only_When_Primary_Is_Active", "Only_When_Secondary_Is_Active", "Predator_Zoom" }, 32),
                   new Value("Convolution Amount", typeof(float)),
                   new Value("Filter Scale", typeof(float)),
                   new Value("Filter Box Factor", typeof(float)),
                   new Value("Zoom Falloff Radius", typeof(float)),
                   new Value("Zoom Cutoff Radius", typeof(float)),
                   new Value("Resolution Scale", typeof(float)),
               }),
           }),
           });
       }
    }
}
