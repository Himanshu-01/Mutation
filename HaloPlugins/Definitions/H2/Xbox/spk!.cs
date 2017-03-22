using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class spk : TagDefinition
    {
       public spk() : base("spk!", "speak", 40)
       {
           Fields.AddRange(new MetaNode[] {
           new Value("Almost Never", typeof(float)),
           new Value("Rarely", typeof(float)),
           new Value("Somewhat", typeof(float)),
           new Value("Often", typeof(float)),
           new Padding(24),
           });
       }
    }
}
