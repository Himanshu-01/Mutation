using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class fx : TagDefinition
    {
       public fx() : base("<fx>", "sound_effect_template", 28)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(8),
           new Padding(4),
           new TagBlock("Template Collection", 20, -1, new MetaNode[] { 
               new StringId("DSP Effect"),
               new TagBlock("Explanation", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
           }),
           new TagBlock("Template Collection", 12, 128, new MetaNode[] { 
               new TagBlock("Parameters", 24, 128, new MetaNode[] { 
                   new StringId("Name"),
                   new Value("Unknown", typeof(int)),
                   new Flags("Flags", new string[] { "Expose_as_Function" }, 16),
                   new Value("Hardware Offset", typeof(short)),
                   new Value("Default Enum Integer Value", typeof(short)),
                   new Value("Unknown", typeof(short)),
                   new TagBlock("Default Function", 16, -1, new MetaNode[] { 
                       new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Integer", "Real", "Filter_Type" }, 32),
                       new Value("Default Scalar Value", typeof(float)),
                       new Value("Minimum Scalar Value", typeof(float)),
                       new Value("Maximum Scalar Value", typeof(float)),
                   }),
               }),
               new StringId("Input Effect Name"),
           }),
           });
       }
    }
}
