using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class wgtz : TagDefinition
    {
       public wgtz() : base("wgtz", "user_interface_globals_definition", 32)
       {
           Fields.AddRange(new MetaNode[] {
           new TagReference("Globals", "wigl"),
           new TagBlock("Collection", 8, 256, new MetaNode[] { 
               new TagReference("Game_Shell", "wgit"),
           }),
           new TagReference("Gametype_Collection", "goof"),
           new TagReference("Unicode_Strings", "unic"),
           });
       }
    }
}
