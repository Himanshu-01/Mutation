using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class MGS2 : TagDefinition
    {
       public MGS2() : base("MGS2", "light_volume", 16)
       {
           Fields.AddRange(new MetaNode[] {
           new Value("Falloff Distance From Camera", typeof(float)),
           new Value("Cutoff Distance From Camera", typeof(float)),
           new TagBlock("Volumes", 152, 16, new MetaNode[] { 
               new Flags("Flags", new string[] { "Force_Linear_Radius_Function", "Force_Linear_Offset", "Force_Differential_Evaluation", "Fuzzy", "Not_Scaled_By_Event_Duration", "Scaled_By_Marker" }, 32),
               new TagReference("Bitmap", "bitm"),
               new Value("Sprite Count", typeof(int)),
               new TagBlock("Offset", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Radius", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Brightness", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Color", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Facing", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Aspect", 28, 1, new MetaNode[] { 
                   new TagBlock("Along-Axis Scale", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new TagBlock("Away-From-Axis Scale", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new Value("Parallel Scale", typeof(float)),
                   new Value("Parallel Threshold Angle", typeof(float)),
                   new Value("Parallel Exponent", typeof(float)),
               }),
               new Value("Radius Frac Min", typeof(float)),
               new Value("DEPRECATED X-Step Exponent", typeof(float)),
               new Value("DEPRECATED X-Buffer Length", typeof(int)),
               new Value("X-Buffer Spacing", typeof(int)),
               new Value("X-Buffer Min Iterations", typeof(int)),
               new Value("X-Buffer Max Iterations", typeof(int)),
               new Value("X-Delta Max Error", typeof(float)),
               new Padding(4),
               new TagBlock("", 8, 256, new MetaNode[] { 
                   new Padding(8),
               }),
               new Padding(48),
           }),
           });
       }
    }
}
