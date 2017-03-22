using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class item
    {
        public List<MetaNode> Fields = new List<MetaNode>();

        public item()
        {
            // Obje
            Fields.AddRange(new obje().Fields.ToArray());

            // Item
            Fields.AddRange(new MetaNode[] {
            new Flags("Item Flags", new string[] { "Always_Maintains_Z_Up", "Destroyed_By_Explosions", "Unaffected_By_Gravity" }, 32),
            new Value("Old Message Index", typeof(short)),
            new Value("Sort Order", typeof(short)),
            new Value("Multiplayer On-ground Scale", typeof(float)),
            new Value("Campaign On-Ground Scale", typeof(float)),
            new StringId("Pickup"),
            new StringId("Swap"),
            new StringId("Pickup or Dual Wield"),
            new StringId("Swap or Dual Wield"),
            new StringId("Dual Wield Only"),
            new StringId("Picked Up"),
            new StringId("Ammo Singular"),
            new StringId("Ammo Plural"),
            new StringId("Switched To"),
            new StringId("Swap AI"),
            new TagReference("unused", "foot"),
            new TagReference("Collision_Sound", "snd!"),
            new TagBlock("Predicted Bitmaps", 8, 8, new MetaNode[] { 
               new TagReference("Bitmap", "bitm"),
            }),
            new TagReference("Detonation_Damage_Effect", "jpt!"),
            new Value("Detonation Delay", typeof(float)),
            new Value("...To", typeof(float)),
            new TagReference("Deonating_Effect", "effe"),
            new TagReference("Detonation_Effect", "effe"),
            });
        }
    }
}
