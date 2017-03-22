using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class ctrl : TagDefinition
    {
       public ctrl() : base("ctrl", "control", 320)
       {
           // Devi
           Fields.AddRange(new devi().Fields.ToArray());

           // Ctrl
           Fields.AddRange(new MetaNode[] {
           new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Toggle_Switch", "On_Button", "Off_Button", "Call_Button" }, 16),
           new HaloPlugins.Objects.Data.Enum("Triggers When", new string[] { "Touched_By_Player", "Destroyed" }, 16),
           new Value("Call Value", typeof(float)),
           new StringId("Action String"),
           new TagReference("On", "****"),
           new TagReference("Off", "****"),
           new TagReference("Deny", "****"),
           });
       }
    }
}
