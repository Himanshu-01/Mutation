using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using HaloPlugins.Objects.Vector;

namespace HaloPlugins.Xbox
{
    public class unit
    {
        public List<MetaNode> Fields = new List<MetaNode>();

        public unit()
        {
            // Obje
            Fields.AddRange(new obje().Fields.ToArray());

            // Unit
            Fields.AddRange(new MetaNode[] {
            new InfoBlock("<--- Unit --->"),
            new Flags("Flags", new string[] { "Circular_Aiming", "Destroyed_After_Dying", "Half-speed_Interpolation", "Fires_From_Camera", "Entrance_Inside_Bounding_sphere_", "Doesn't_Show_Readied_Weapon", "Couses_Passenger_Dialogue", "Resists_Pings", "Melee_Attack_Is_Fatal", "Don't_Reface_During_Pings", "Has_No_Aiming", "Simple_Creature", "Impact_Melle_Attaches_To_Unit", "Impact_Melee_Dies_On_Shield", "Cannot_Open_Doors_Automatically", "Melee_Attackers_Cannot_Attach", "Not_Instantly_Killed_By_Melee", "Shield_Sapping", "Runs_Around_Flaming", "Inconsequential", "Special_Cinematic_Unit", "Ignored_By_AutoAiming", "Shields_Fry_Infection_Forms", "unused", "unused", "Acts_As_Gunner_For_Parent", "Controlled_By_Parent_Gunner", "Parent's_Primary_Weapon", "Unit_Has_Boost" }, 32),
            new HaloPlugins.Objects.Data.Enum("Default Team", new string[] { "Deafult", "Player", "Human", "Covenant", "Flood", "Sentinel", "Heretic", "Prophet" }, 16),
            new HaloPlugins.Objects.Data.Enum("Constant Sound Volume", new string[] { "Silent", "Medium", "Loud", "Shout", "Quiet" }, 16),
            new TagReference("Integrated_Light_Toggle", "effe"),
            new Value("Camera Field of View", typeof(float)),
            new Value("Camera Stiffness", typeof(float)),
            new StringId("Camera Marker Name"),
            new StringId("Camera Submerged Marker Name"),
            new Value("Pitch Auto-Level", typeof(float)),
            new Value("Pitch Range", typeof(float)),
            new Value("...To", typeof(float)),
            new TagBlock("Camera Tracks", 8, -1, new MetaNode[] { 
               new TagReference("Track", ""),
            }),
            new Value("Acceleration Scale i", typeof(float)),
            new Value("Acceleration Scale j", typeof(float)),
            new Value("Acceleration Scale k", typeof(float)),
            new Value("Acceleration Action Scale", typeof(float)),
            new Value("Acceleration Attach Scale", typeof(float)),
            new Value("Soft Ping Threshold", typeof(float)),
            new Value("Soft Ping Interrupt Time", typeof(float)),
            new Value("Hard Ping Threshold", typeof(float)),
            new Value("Hard Ping Interrupt Time", typeof(float)),
            new Value("Hard Ping Death Threshold", typeof(float)),
            new Value("Feign Death Threshold", typeof(float)),
            new Value("Feign Death Time", typeof(float)),
            new Value("Dist Of Evade Anim", typeof(float)),
            new Value("Dist of Dive Anim", typeof(float)),
            new Value("Stunned Movement Threshold", typeof(float)),
            new Value("Feign Death Chance", typeof(float)),
            new Value("Feign Repeat Chance", typeof(float)),
            new TagReference("Spawned_Turret_Actor", "char"),
            new Value("Spawned Actor Count", typeof(short)),
            new Value("...To", typeof(short)),
            new Value("Spawned Velocity", typeof(float)),
            new Value("Aiming Velocity Max", typeof(float)),
            new Value("Aiming Accel Max", typeof(float)),
            new Value("Casual Aiming Modifier", typeof(float)),
            new Value("Looking Velocity Max", typeof(float)),
            new Value("Looking Accel Max", typeof(float)),
            new StringId("Right Hand Node"),
            new StringId("Left Hand Node"),
            new StringId("Preferred Gun Node"),
            new TagReference("Melee_Damage", "jpt!"),
            new TagReference("Boarding_Melee_Damage", "jpt!"),
            new TagReference("Boarding_Melee_Response", "jpt!"),
            new TagReference("Landing_Melee_Damage", "jpt!"),
            new TagReference("Flurry_Melee_Damage", "jpt!"),
            new TagReference("Obstacle_Smash_Damage", "jpt!"),
            new HaloPlugins.Objects.Data.Enum("Motion Sensor Blip Size", new string[] { "Medium", "Small", "Large" }, 32),
            new TagBlock("Postures", 16, 20, new MetaNode[] { 
               new StringId("Name"),
               new RealVector3d("Pill Offset"),
            }),
            new TagBlock("New Hud Interfaces", 8, 2, new MetaNode[] { 
               new TagReference("New_Unit_Hud_Interface", "nhdt"),
            }),
            new TagBlock("Dialogue Variants", 24, 16, new MetaNode[] { 
               new Value("Variant Number", typeof(short)),
               new Padding(10),
               new TagReference("Dialogue", "udlg"),
               new Padding(4),
            }),
            new Value("Grenade Velocity", typeof(float)),
            new HaloPlugins.Objects.Data.Enum("Grenade Type", new string[] { "Human_Fragmentation", "Covenant_Plasma" }, 16),
            new Value("Grenade Count", typeof(ushort)),
            new TagBlock("Powered Seats", 8, 2, new MetaNode[] { 
               new Value("Driver Powerup Time", typeof(float)),
               new Value("Driver Powerdown Time", typeof(float)),
            }),
            new TagBlock("Weapon", 8, 4, new MetaNode[] { 
               new TagReference("Weapon", "weap"),
            }),
            new TagBlock("Seats", 176, 32, new MetaNode[] { 
               new Flags("Flags", new string[] { "Invisible", "Locked", "Driver", "Gunner", "3rd_Person_Camera", "Allows_Weapons", "3rd_Person_On_Enter", "1st_Person_Camera_Slaved_To_Gun", "Allow_Vehicle_Communication_Animations", "Not_Valid_Without_Driver", "Allow_AI_NonCombatants", "Boarding_Seat", "AI_Firing_Disabled_By_Max_Acceleration", "Boarding_Enters_Seat", "Boarding_Need_Any_Passenger", "Invaild_For_Player", "Invaild_For_Non-Player", "Gunner_(Player_Only)", "Invisible_Under_Major_Damage" }, 32),
               new StringId("Label"),
               new StringId("Sitting Postion Marker"),
               new StringId("Entry Marker(s) Name"),
               new StringId("Boarding Grenade Marker"),
               new StringId("Boarding Grenade String"),
               new StringId("Boarding Melee String"),
               new Value("Ping Scale", typeof(float)),
               new Value("Turnover Time (sec)", typeof(float)),
               new Value("Accel Range i", typeof(float)),
               new Value("Accel Range j", typeof(float)),
               new Value("Accel Range k", typeof(float)),
               new Value("Accel Action Scale", typeof(float)),
               new Value("Accel Attach Scale", typeof(float)),
               new Value("AI Scariness", typeof(float)),
               new HaloPlugins.Objects.Data.Enum("AI Seat Type", new string[] { "None", "Passenger", "Gunner", "Small_Cargo", "Large_Cargo", "Driver" }, 16),
               new Value("Boarding Seat #", typeof(short)),
               new Value("Listener Interpolation Factor", typeof(float)),
               new Value("Yaw Rate Bounds", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Pitch Rate Bounds", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Min Speed Ref", typeof(float)),
               new Value("Max Speed Ref", typeof(float)),
               new Value("Speed Exponent", typeof(float)),
               new StringId("Camera Marker Name"),
               new StringId("Camera Submerged Marker name"),
               new Value("Pitch Auto-level", typeof(float)),
               new Value("Pitch Range Lower", typeof(float)),
               new Value("Pitch Range Upper", typeof(float)),
               new TagBlock("Camera Tracks", 8, -1, new MetaNode[] { 
                   new TagReference("Track", "trak"),
               }),
               new TagBlock("Unit Hud Interface", 8, 2, new MetaNode[] { 
                   new TagReference("New_Unit_Hud_Interface", "nhdt"),
               }),
               new StringId("Enter Seat String"),
               new Value("Yaw Min", typeof(float)),
               new Value("Yaw Max", typeof(float)),
               new TagReference("Built-in_Gunner", "char"),
               new Value("Entry Radius", typeof(float)),
               new Value("Entry Marker Cone Angle", typeof(float)),
               new Value("Entry Marker Facing Angle", typeof(float)),
               new Value("Maximum Relative Velocity", typeof(float)),
               new StringId("Invisible Seat Region"),
               new Value("Runtime Invisible Seat Region Index", typeof(float)),
            }),
            new Value("Boost Peak Power", typeof(float)),
            new Value("Boost Rise Power", typeof(float)),
            new Value("Boost Peak Time", typeof(float)),
            new Value("Boost Fall Power", typeof(float)),
            new Value("Dead Time", typeof(float)),
            new Value("Lip Sync Attack Weight", typeof(float)),
            new Value("Lip Sync decay Weight", typeof(float)),
            });
        }
    }
}
