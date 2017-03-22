using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class clwd : TagDefinition
    {
       public clwd() : base("clwd", "cloth", 108)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Doesn't_Use_Wind", "Uses_Grid_Attach_Top" }, 32),
           new StringId("Marker Attachment Name"),
           new TagReference("Shader", "shad"),
           new Value("Grid X Dimension", typeof(short)),
           new Value("Grid Y Dimension", typeof(short)),
           new Value("Grid Spacing X", typeof(float)),
           new Value("Grid Spacing Y", typeof(float)),
           new Value("Integration Type", typeof(short)),
           new Value("Number Iterations", typeof(short)),
           new Value("Weight", typeof(float)),
           new Value("Drag", typeof(float)),
           new Value("Wind Scale", typeof(float)),
           new Value("Wind Flappiness Scale", typeof(float)),
           new Value("Longest Rod", typeof(float)),
           new Padding(24),
           new TagBlock("Vertices", 20, 128, new MetaNode[] { 
               new Value("Initial Position X", typeof(float)),
               new Value("Initial Position Y", typeof(float)),
               new Value("Initial Position Z", typeof(float)),
               new Value("UV i", typeof(float)),
               new Value("UV j", typeof(float)),
           }),
           new TagBlock("Indices", 2, 768, new MetaNode[] { 
               new Value("Index", typeof(short)),
           }),
           new TagBlock("Strip Indices", 2, 768, new MetaNode[] { 
               new Value("Index", typeof(short)),
           }),
           new TagBlock("Links", 16, 640, new MetaNode[] { 
               new Value("Attachment Bits", typeof(float)),
               new Value("Index 1", typeof(short)),
               new Value("Index 2", typeof(short)),
               new Value("Default Distance", typeof(float)),
               new Value("Damping Multiplier", typeof(float)),
           }),
           });
       }
    }
}
