using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class udlg : TagDefinition
    {
       public udlg() : base("udlg", "unit_dialog", 24)
       {
           Fields.AddRange(new MetaNode[] {
           new TagReference("Global_Dialogue_Info", "adlg"),
           new HaloPlugins.Objects.Data.Enum("Flags", new string[] { "Female" }, 32),
           new TagBlock("Vocalizations", 16, 500, new MetaNode[] { 
               new Flags("Flags", new string[] { "New_Vocalization" }, 32),
               new StringId("Vocalization"),
               new TagReference("Sound", "snd!"),
           }),
           new StringId("Mission Dialogue Designator"),
           });
       }
    }
}
