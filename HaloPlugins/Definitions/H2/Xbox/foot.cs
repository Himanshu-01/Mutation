using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class foot : TagDefinition
    {
       public foot() : base("foot", "material_effect", 8)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Effects", 24, 21, new MetaNode[] { 
               new TagBlock("Old Materials (Do Not Use)", 24, 33, new MetaNode[] { 
                   new TagReference("Effect", "effe"),
                   new TagReference("Sound", "****"),
                   new StringId("Material Name"),
                   new HaloPlugins.Objects.Data.Enum("Sweetener Mode", new string[] { "Default", "Enabled", "Disabled" }, 32),
               }),
               new TagBlock("Sounds", 24, 500, new MetaNode[] { 
                   new TagReference("Sound_1", "****"),
                   new TagReference("Sound_2", "****"),
                   new StringId("Material Name"),
                   new HaloPlugins.Objects.Data.Enum("Sweetener Mode", new string[] { "Default", "Enabled", "Disabled" }, 32),
               }),
               new TagBlock("Effects", 24, 500, new MetaNode[] { 
                   new TagReference("Effect_1", "****"),
                   new TagReference("Effect_2", "****"),
                   new StringId("Material Name"),
                   new HaloPlugins.Objects.Data.Enum("Sweetener Mode", new string[] { "Default", "Enabled", "Disabled" }, 32),
               }),
           }),
           });
       }
    }
}
