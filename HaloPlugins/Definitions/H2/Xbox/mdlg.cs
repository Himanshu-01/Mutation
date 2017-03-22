using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class mdlg : TagDefinition
    {
       public mdlg() : base("mdlg", "ai_mission_dialogue", 8)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Lines", 16, 500, new MetaNode[] { 
               new StringId("Name"),
               new TagBlock("Variants", 16, 10, new MetaNode[] { 
                   new StringId("Name"),
                   new TagReference("Sound", "snd!"),
                   new Padding(4),
               }),
               new StringId("Default Sound Effect"),
           }),
           });
       }
    }
}
