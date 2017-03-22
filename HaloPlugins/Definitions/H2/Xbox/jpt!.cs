using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class jpt : TagDefinition
    {
       public jpt() : base("jpt!", "damage_effect", 200)
       {
           Fields.AddRange(new MetaNode[] {
           new Value("Radius", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Cutoff Scale", typeof(float)),
           new Flags("Flags", new string[] { "Don't_Scale_Damage_By_Distance", "Area_Damage_Players_Only" }, 32),
           new HaloPlugins.Objects.Data.Enum("Side Effect", new string[] { "None", "Harmless", "Lethal_To_The_Unsuspecting", "Emp" }, 16),
           new HaloPlugins.Objects.Data.Enum("Category", new string[] { "None", "Falling", "Bullet", "Grenade", "High_Explosive", "Sniper", "Melee", "Flame", "Mounted_Weapon", "Vehicle", "Plasma", "Needle", "Shotgun" }, 16),
           new Flags("Flags", new string[] { "Does_Not_Hurt_Owner", "Can_Cause_Headshots", "Pings_Resistant_Units", "Does_Not_Hurt_Friends", "Does_Not_Ping_Units", "Detonates_Explosives", "Only_Hurts_Sheilds", "Causes_Flaming_Death", "Damage_Indicators_Always_Point_Down", "Skips_Sheilds", "Only_Hurts_One_Infection_Form", "Unused", "Infection_Form_Pop", "Ignore_Seat_Scale_For_Direct_Damage", "Forces_Hard_Ping", "Does_Not_Hurt_Players" }, 32),
           new Value("AOE Core Radius", typeof(float)),
           new Value("Damage Lower Bound", typeof(float)),
           new Value("Damge Upper Bound", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Damage Inner Cone Angle", typeof(float)),
           new Value("Damage Outter Cone Angle", typeof(float)),
           new Value("Active Camoflage Damage", typeof(float)),
           new Value("Stun", typeof(float)),
           new Value("Max Stun", typeof(float)),
           new Value("Stun Time", typeof(float)),
           new Value("Instantaneous Acceleration", typeof(float)),
           new Value("Rider Direct Damage Scale", typeof(float)),
           new Value("Rider Max Transfer Damage", typeof(float)),
           new Value("Rider Min Transfer Damage", typeof(float)),
           new StringId("General Damage"),
           new StringId("Specific Damage"),
           new Value("AI Stun Radius", typeof(float)),
           new Value("AI Stun Bounds", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Shake Radius", typeof(float)),
           new Value("Emp Radius", typeof(float)),
           new TagBlock("Player Responses", 76, 2, new MetaNode[] { 
               new HaloPlugins.Objects.Data.Enum("Response Type", new string[] { "Sheilded", "UnSheiled", "All" }, 16),
               new HaloPlugins.Objects.Data.Enum("Screen Flash Type", new string[] { "None", "Lighten", "Darken", "Max", "Min", "Invert", "Tint" }, 32),
               new HaloPlugins.Objects.Data.Enum("Screen Flash Priority", new string[] { "Low", "Medium", "High" }, 16),
               new Value("Screen Flash Duration", typeof(float)),
               new HaloPlugins.Objects.Data.Enum("Screen Flash Fade Function", new string[] { "Linear", "Late", "Very_Late", "Early", "Very_Early", "Cosine", "Zero", "One" }, 32),
               new Value("Screen Flash Max Intensity", typeof(float)),
               new Value("Screen Flash Color A", typeof(float)),
               new Value("Screen Flash Color R", typeof(float)),
               new Value("Screen Flash Color G", typeof(float)),
               new Value("Screen Flash Color B", typeof(float)),
               new Value("Low Frequency Vibration Duration", typeof(float)),
               new TagBlock("Low Frequency Vibration", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Value("High Frequency Vibration Duration", typeof(float)),
               new TagBlock("High Frequency Vibration", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new StringId("Sound Effect Name"),
               new Value("Sound Effect Duration", typeof(float)),
               new TagBlock("Sound Effect", 12, -1, new MetaNode[] { 
                   new Padding(12),
               }),
           }),
           new Value("Temporary Camera Impulse Duration", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Temporary Camera Impulse Fade Function", new string[] { "Linear", "Late", "Very_Late", "Early", "Very_Early", "Cosine", "Zero", "One" }, 32),
           new Value("Temporary Camera Impulse Rotation", typeof(float)),
           new Value("Temporary Camera Impulse Pushback", typeof(float)),
           new Value("Temporary Camera Impulse Jitter", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Camera Shaking Duration", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Camera Shaking Falloff Function", new string[] { "Linear", "Late", "Very_Late", "Early", "Very_Early", "Cosine", "Zero", "One" }, 32),
           new Value("Camera Shaking Random Translation", typeof(float)),
           new Value("Camera Shaking Random Rotation", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Camera Shaking Wobble Function", new string[] { "One", "Zero", "Cosine", "Cosine_(Variable_Period)", "Diagonal_Wave", "Diagonal_Wave_(Variable_Period)", "Slide", "Slide_(Variable_Period)", "Noise", "Jitter", "Wander", "Spark" }, 32),
           new Value("Camera Shaking Wobble Function Period", typeof(float)),
           new Value("Camera Shaking Wobble Weight", typeof(float)),
           new TagReference("Sound", "snd!"),
           new Value("Breaking Effect Forward Velocity", typeof(float)),
           new Value("Breaking Effect Forward Radius", typeof(float)),
           new Value("Breaking Effect Forward Exponent", typeof(float)),
           new Value("Breaking Effect Outward Velocity", typeof(float)),
           new Value("Breaking Effect Outward Radius", typeof(float)),
           new Value("Breaking Effect Outward Exponent", typeof(float)),
           });
       }
    }
}
