using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using HaloPlugins.Objects.Strings;

namespace HaloPlugins.Xbox
{
    public class styl : TagDefinition
    {
       public styl() : base("styl", "style", 92)
       {
           Fields.AddRange(new MetaNode[] {
           new ShortString("Name", StringType.Asci),
           new HaloPlugins.Objects.Data.Enum("Combat Status Decay Options", new string[] { "Latch_at_Idle", "Latch_at_Alert", "Latch_at_Combat" }, 32),
           new Value("Unknown", typeof(int)),
           new HaloPlugins.Objects.Data.Enum("Attitude", new string[] { "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Engaged Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Evasion Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Cover Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Search Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Presearch Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Retreat Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Charge Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Ready Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Idle Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Weapon Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new HaloPlugins.Objects.Data.Enum("Swarm Attitude", new string[] { "Default", "Normal", "Timid", "Agressive" }, 8),
           new Flags("Style Control", new string[] { "New_Behaviors_Default_to_ON" }, 32),
           new Flags("Behaviors 1", new string[] { "----General----", "Root", "Null", "Null_Discrete", "Obey", "Guard", "Follow_Behavior", "Ready", "Smash_Obstacle", "Destroy_Obstacle", "Perch", "Cover_Friend", "Blind_Panic", "----Engage----", "Engage", "Fight", "Melee_Charge", "Melee_Leaping_Charge", "Surprise", "Grenade_Impulse", "Anti_Vehicle_Grenade", "Stalk", "Berserk_Wander_Impulse", "----Berserk----", "Last_Man_Berserk", "Stuck_with_Grenade_Berserk", "----Presearch----", "Presearch", "Presearch_Uncover", "Destroy_Cover", "Suppressing_Fire", "Grenade_Uncover" }, 32),
           new Flags("Behaviors 2", new string[] { "Leap_on_Cover", "----Search----", "Search", "Uncover", "Investigate", "Pursuit_Sync", "Pursuit", "Post_Search", "Coverme_Investigate", "----Self_Defense----", "Self_Preservation", "Cover", "Cover_Peek", "Avoid", "Evasion_Impulse", "Dive_Impulse", "Danger_Cover_Impulse", "Danger_Crouch_Impulse", "Proximity_Melee", "Proximity_Self_Preservation", "Unreachable_Enemy_Cover", "Scary_Target_Cover", "Group_Emerge", "----Retreat----", "Retreat", "Retreat_Grenade", "Flee", "Cower", "Low_Shield_Retreat", "Scary_Target_Retreat", "Leader_Dead_Retreat", "Peer_Dead_Retreat" }, 32),
           new Flags("Behaviors 3", new string[] { "Danger_Retreat", "Proximity_Retreat", "Charge_When_Cornered", "Surprise_Retreat", "Overheated_Weapon_Retreat", "----Ambush----", "Ambush", "Coordinated_Ambush", "Proximity_Ambush", "Vulnerable_Enemy_Ambush", "No_where_to_RUn_Ambush", "----Vehicle----", "Vehicle", "Enter_Friendly_vehicle", "Re-Enter_Flipped_Vehicle", "Vehicle_Entry_Engaged_Impulse", "Vehicle_Board", "Vehicle_Fight", "Vehicle_Charge", "Vehicle_Ram_Behavior", "Vehicle_Cover", "Damage_Vehicle_Cover", "Exposed_Rear_Cover_Impulse", "Player_Endagered_Cover_Impulse", "Vehicle_Avoid", "Vehicle_Pickup", "Vehicle_Player_Pickup", "Vehicle_Exit_Impulse", "Danger_Vehicle_Exit_Impulse", "Vehicle_Flip", "Vehicle_Turtle", "Vehicle_Engaged_Patrol_Impulse" }, 32),
           new Flags("Behaviors 4", new string[] { "Vehicle_Engage_Wander_Impact", "----POSTCOMBAT----", "Post_Combat", "Post_Post_Combat", "Check_Friend", "Shoot_Corpse", "Post_Combat_Approach", "----Alert----", "Alert", "----Idle----", "Idle", "Wander_Behavior", "Flight_Wander", "Patrol", "Fall_Asleep", "----Buggers----", "Bugger_Ground_Uncover", "----Swarms----", "Swarm_Root", "Swarm_Attack", "Support_Attack", "Infect", "Scatter", "Eject_Parasite", "Flood_Self_Preservation", "Juggernaunt_Flurry", "----Sentinels----", "Enforcer_Weapon_Control", "Grapple", "----Special----", "Formation", "Grunt_Scared_by_Elite" }, 32),
           new Flags("Behaviors 5", new string[] { "Stunned", "Cure_Isolation", "Deploy_Turrent" }, 32),
           new TagBlock("Special Movement", 4, 1, new MetaNode[] { 
               new Flags("Special Movement 1", new string[] { "Jump", "Climb", "Vault", "Mount", "Hoist", "Wall_Jump" }, 32),
           }),
           new TagBlock("Behavior List", 32, 160, new MetaNode[] { 
               new ShortString("Behavior Name", StringType.Asci),
           }),
           });
       }
    }
}
