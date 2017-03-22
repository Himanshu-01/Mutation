using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class eqip : TagDefinition
    {
       public eqip() : base("eqip", "equipment", 316)
       {
           // Item
           Fields.AddRange(new item().Fields.ToArray());

           // Eqip
           Fields.AddRange(new MetaNode[] {
           new HaloPlugins.Objects.Data.Enum("Powerup Type", new string[] { "None", "Double_Speed", "Oversheild", "Active_Camouflage", "Full-Spectrum_Vision", "Health", "Grenade" }, 16),
           new HaloPlugins.Objects.Data.Enum("Grenade Type", new string[] { "Human_Fragmentation", "Covenant_Plasma" }, 16),
           new Value("Powerup Time", typeof(float)),
           new TagReference("Pickup_Sound", "snd!"),
           });
       }
    }
}
