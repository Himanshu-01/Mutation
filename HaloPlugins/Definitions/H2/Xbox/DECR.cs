using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.H2.Xbox;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class DECR : TagDefinition
    {
       public DECR() : base("DECR", "decorator", 108, new DecoratorDefinition())
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Shaders", 8, 8, new MetaNode[] { 
               new TagReference("Shader", "shad"),
           }),
           new Value("Lighting Min Scale", typeof(float)),
           new Value("Lighting Max Scale", typeof(float)),
           new TagBlock("Classes", 20, 8, new MetaNode[] { 
               new StringId("Name"),
               new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Model", "Floating_Decal", "Projected_Decal", "Screen_Facing_Quad", "Axis_Rotating_Quad", "Cross_Quad" }, 32),
               new Value("Scale", typeof(float)),
               new TagBlock("Permutations", 40, 64, new MetaNode[] { 
                   new StringId("Name"),
                   new Value("Shader #", typeof(short)),
                   new Flags("Flags", new string[] { "Align_To_Normal", "Only_On_Ground", "Upright" }, 8),
                   new HaloPlugins.Objects.Data.Enum("Fade Distance", new string[] { "Close", "Medium", "Far" }, 8),
                   new Value("Index", typeof(short)),
                   new Value("Distribution Weight", typeof(short)),
                   new Value("Scale", typeof(float)),
                   new Value("...To", typeof(float)),
                   new Value("Tint 1 R", typeof(byte)),
                   new Value("Tint 1 G", typeof(byte)),
                   new Value("Tint 1 B", typeof(byte)),
                   new Padding(1),
                   new Value("Tint 2 R", typeof(byte)),
                   new Value("Tint 2 G", typeof(byte)),
                   new Value("Tint 2 B", typeof(byte)),
                   new Padding(1),
                   new Value("Base Map Tint Percentage", typeof(float)),
                   new Value("Lightmap Tint Percentage", typeof(float)),
                   new Value("Wind Scale", typeof(float)),
               }),
           }),
           new TagBlock("Models", 8, 256, new MetaNode[] { 
               new StringId("Model Name"),
               new Value("Index Start", typeof(short)),
               new Value("Index Count", typeof(short)),
           }),
           new TagBlock("Raw Vertices", 56, 32768, new MetaNode[] { 
               new Value("X", typeof(float)),
               new Value("Y", typeof(float)),
               new Value("Z", typeof(float)),
               new Value("Normal i", typeof(float)),
               new Value("Normal j", typeof(float)),
               new Value("Normal k", typeof(float)),
               new Value("Tangent i", typeof(float)),
               new Value("Tangent j", typeof(float)),
               new Value("Tangent k", typeof(float)),
               new Value("Binormal i", typeof(float)),
               new Value("Binormal j", typeof(float)),
               new Value("Binormal k", typeof(float)),
               new Value("U", typeof(float)),
               new Value("V", typeof(float)),
           }),
           new TagBlock("Indices", 2, 32768, new MetaNode[] { 
               new Value("Index", typeof(short)),
           }),
           new TagBlock("Cached Data", 0, 1, new MetaNode[] { 
           }),
           new Value("Offset", typeof(int)),
           new Value("Size", typeof(int)),
           new Value("Header Size", typeof(int)),
           new Value("Data Size", typeof(int)),
           new TagBlock("Resources", 16, -1, new MetaNode[] { 
               new HaloPlugins.Objects.Data.Enum("Type", new string[] {  }, 32),
               new Value("Primary Locator", typeof(short)),
               new Value("Secondary Locator", typeof(short)),
               new Value("Data Size", typeof(int)),
               new Value("Data Offset", typeof(int)),
           }),
           new TagIndex("Owner Tag Section Offset", "DECR"),
           new Value("Unused", typeof(int)),
           new Padding(20),
           });
       }
    }
}
