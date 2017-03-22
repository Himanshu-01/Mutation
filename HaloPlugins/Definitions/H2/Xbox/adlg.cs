using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class adlg : TagDefinition
    {
       public adlg() : base("adlg", "ai_dialogue_globals", 44)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Vocalizations", 96, 500, new MetaNode[] { 
               new StringId("Vocalization"),
               new StringId("Parent Vocalization"),
               new Value("Parent Index", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Priority", new string[] { "None", "Recall", "Idle", "Comment", "Idle-Response", "Post_Combat", "Combat", "Status", "Respond", "Warn", "Act", "React", "Involentary", "Scream", "Scripted", "Death" }, 16),
               new Flags("FLags", new string[] { "Immediate", "Interupt", "Cancel_Low_Priority" }, 32),
               new HaloPlugins.Objects.Data.Enum("Glance Behavior", new string[] { "Subject_Short", "Subject_Long", "Cause_Short", "Cause_Long", "Friend_Short", "Friend_Long" }, 16),
               new HaloPlugins.Objects.Data.Enum("Glance Recipiant Behavior", new string[] { "Subject_Short", "Subject_Long", "Cause_Short", "Cause_Long", "Friend_Short", "Friend_Long" }, 16),
               new HaloPlugins.Objects.Data.Enum("Perception Type", new string[] { "None", "Speaker", "Listener" }, 16),
               new HaloPlugins.Objects.Data.Enum("Max Combat Status", new string[] { "Asleep", "Idle", "Alert", "Active", "Uninspected", "Definite", "Certain", "Visible", "Clear_Los", "Dangerous" }, 16),
               new HaloPlugins.Objects.Data.Enum("Animation Impulse", new string[] { "None", "Shake-Fist", "Cheer", "Surprise-Front", "Surprise-Back", "Taunt", "Brace", "Point", "Hold", "Wave", "Advance", "Fallback" }, 16),
               new HaloPlugins.Objects.Data.Enum("Overlap Priority", new string[] { "None", "Recall", "Idle", "Comment", "Idle-Response", "Post_Combat", "Combat", "Status", "Respond", "Warn", "Act", "React", "Involentary", "Scream", "Scripted", "Death" }, 16),
               new Value("Sound Repetition Delay [Seconds]", typeof(float)),
               new Value("Allowable Quenue Delay [Seconds]", typeof(float)),
               new Value("Pre Voc. Delay [Seconds]", typeof(float)),
               new Value("Notification Delay [Seconds]", typeof(float)),
               new Value("Post Voc. Delay [Seconds]", typeof(float)),
               new Value("Repeat Delay [Seconds]", typeof(float)),
               new Value("Weight", typeof(float)),
               new Value("Speaker Freeze Time", typeof(float)),
               new Value("Listener Freeze Time", typeof(float)),
               new HaloPlugins.Objects.Data.Enum("Speaker Emotion", new string[] { "None", "Asleep", "Amorous", "Happy", "Inquisitive", "Repulsed", "Dissapointed", "Shocked", "Scared", "Arrogant", "Annoyed", "Angry", "Pensive", "Pain" }, 16),
               new HaloPlugins.Objects.Data.Enum("Listener Emotion", new string[] { "None", "Asleep", "Amorous", "Happy", "Inquisitive", "Repulsed", "Dissapointed", "Shocked", "Scared", "Arrogant", "Annoyed", "Angry", "Pensive", "Pain" }, 16),
               new Value("Player Skip Fraction", typeof(float)),
               new Value("Skip Fraction", typeof(float)),
               new StringId("Sample Line"),
               new TagBlock("Responses", 12, 20, new MetaNode[] { 
                   new StringId("Vocalization Name"),
                   new Flags("Flags", new string[] { "Non-Exclusive", "Trigger_Response" }, 16),
                   new Value("Vocalization Index (Post Process)", typeof(short)),
                   new HaloPlugins.Objects.Data.Enum("Reponse Type", new string[] { "Friend", "Enemy", "Listener", "Point", "Peer" }, 16),
                   new Value("Dialogue Index (Import)", typeof(short)),
               }),
               new Padding(8),
           }),
           new TagBlock("Patterns", 64, 1000, new MetaNode[] { 
               new HaloPlugins.Objects.Data.Enum("Dialogue Type", new string[] { "death", "unused", "unused", "damage", "damage unused 1", "damage unused 2", "sighted_new", "sighted_new major", "unused", "sighted_old", "sighted_first", "sighted_special", "unused", "heard_new", "unused", "heard_old", "unused", "unused", "unused", "acknowledge_multiple", "unused", "unused", "unused", "found_unit", "found_unit_presearch", "found_unit_pursuit", "found_unit_self_preserving", "found_unit_retreating", "throwing_grenade", "noticed_grenade", "fighting", "charging", "suppressing_fire", "grenade_uncover", "unused", "unused", "dive", "evade", "avoid", "surprised", "unused", "unused", "presearch", "presearch_start", "search", "search_start", "investigate_failed", "uncover_failed", "pursuit_failed", "investigate_start", "abandoned_search_space", "abandoned_search_time", "presearch_failed", "abandoned_search_restricted", "investigate_pursuit_start", "postcombat_inspect_body", "vehicle_slow_down", "vehicle_get_in", "idle", "taunt", "taunt_reply", "retreat", "retreat_from_scary_target", "retreat_from_dead_leader", "retreat_from_proximity", "retreat_from_low_shield", "flee", "cowering", "unused", "unused", "unused", "cover", "covered", "unused", "unused", "unused", "pursuit_start", "pursuit_sync_start", "pursuit_sync_join", "pursuit_sync_quorum", "melee", "unused", "unused", "unused", "vehicle_falling", "vehicle_woohoo", "vehicle_scared", "vehicle_crazy", "unused", "unused", "leap", "unused", "unused", "postcombat_win", "postcombat_lose", "postcombat_neutral", "shoot_corpse", "postcombat start", "inspect_body_start", "postcombat_status", "unused", "vehicle_entry_start_driver", "vehicle_enter", "vehicle_entry_start_gun", "vehicle_entry_start_passenger", "vehicle_exit", "evict_driver", "evict_gunner", "evict_passenger", "unused", "unused", "new_order_advance", "new_order_charge", "new_order_fallback", "new_order_retreat", "new_order_moveon", "new_order_arrival", "new_order_entervcl", "new_order_exitvcl", "new_order_fllplr", "new_order_leaveplr", "new_order_support", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "emerge", "unused", "unused", "unused", "curse", "unused", "unused", "unused", "threaten", "unused", "unused", "unused", "cover_friend", "unused", "unused", "unused", "strike", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "unused", "gloat", "unused", "unused", "unused", "greet", "unused", "unused", "unused", "unused", "player_look", "player_look_longtime", "unused", "unused", "unused", "unused", "panic_grenade_attached", "unused", "unused", "unused", "unused", "help response", "unused", "unused", "unused", "remind", "unused", "unused", "unused", "unused", "weapon_trade_better", "weapon_trade_worse", "weapon_reade_equal", "unused", "unused", "unused", "betray", "unused", "forgive", "unused", "reanimate" }, 16),
               new Value("Volcalization Index", typeof(short)),
               new StringId("Volcalization Name"),
               new HaloPlugins.Objects.Data.Enum("Speaker Type", new string[] { "subject", "cause", "friend", "target", "enemy", "vehicle", "joint", "squad", "leader", "joint_leader", "clump" }, 16),
               new Flags("Flags", new string[] { "subject visible", "cause visible", "friends present", "subject is speaker's target", "cause is speaker's target", "cause is player or speaker is player ally", "speaker is searching", "speaker is following player" }, 16),
               new HaloPlugins.Objects.Data.Enum("Listener/Target", new string[] { "subject", "cause", "friend", "target", "enemy", "vehicle", "joint", "squad", "leader", "joint_leader", "clump" }, 16),
               new Padding(6),
               new HaloPlugins.Objects.Data.Enum("Hostility", new string[] { "NONE", "self", "neutral", "friend", "enemy" }, 16),
               new HaloPlugins.Objects.Data.Enum("Damage Type", new string[] { "NONE", "falling", "bullet", "grenade", "explosive", "sniper", "melee", "flame", "mounted weapon", "vehicle", "plasma", "needle" }, 16),
               new HaloPlugins.Objects.Data.Enum("Danger Level", new string[] { "NONE", "broadly facing", "shooting near", "shooting at", "extremely close", "shield damage", "shield extended damage", "body damage" }, 16),
               new HaloPlugins.Objects.Data.Enum("Attitude", new string[] { "Normal", "Timid" }, 16),
               new Padding(4),
               new HaloPlugins.Objects.Data.Enum("Subject Actor Type", new string[] { "NONE", "elite", "jackal", "grunt", "hunter", "engineer", "assassin", "player", "marine", "crew", "combat form", "infection form", "carrier form", "monitor", "sentinel", "none", "mounted weapon", "brute", "prophet", "bugger" }, 16),
               new HaloPlugins.Objects.Data.Enum("Cause Actor Type", new string[] { "NONE", "elite", "jackal", "grunt", "hunter", "engineer", "assassin", "player", "marine", "crew", "combat form", "infection form", "carrier form", "monitor", "sentinel", "none", "mounted weapon", "brute", "prophet", "bugger" }, 16),
               new HaloPlugins.Objects.Data.Enum("Cause Type", new string[] { "NONE", "player", "actor", "biped", "body", "vehicle", "projectile", "actor or player", "turret", "unit in vehicle", "unit in turret", "driver", "gunner", "passenger", "postcombat", "postcombat_won", "postcombat_lost", "player masterchief", "player dervish", "heretic", "majorly scary", "last man in vehicle", "male", "female" }, 16),
               new HaloPlugins.Objects.Data.Enum("Subject Type", new string[] { "NONE", "player", "actor", "biped", "body", "vehicle", "projectile", "actor or player", "turret", "unit in vehicle", "unit in turret", "driver", "gunner", "passenger", "postcombat", "postcombat_won", "postcombat_lost", "player masterchief", "player dervish", "heretic", "majorly scary", "last man in vehicle", "male", "female" }, 16),
               new StringId("Cause Ai Type"),
               new HaloPlugins.Objects.Data.Enum("Spatial Relationship", new string[] { "none", "very near (<1wu)", "near (<2.5wus)", "medium range (<5wus)", "far (<10wus)", "very far (>10wus)", "in front of", "behind", "above (delta>1 wu)" }, 32),
               new StringId("Subject Ai Type"),
               new Padding(8),
               new HaloPlugins.Objects.Data.Enum("Conditions", new string[] { "asleep", "idle", "alert", "active", "uninspected orphan", "definite orphan", "certain orphan", "visible enemy", "clear los enemy", "dangerous enemy", "no vehicle", "vehicle driver" }, 32),
           }),
           new Padding(12),
           new TagBlock("Dialogue Data", 4, 200, new MetaNode[] { 
               new Value("Start Index", typeof(short)),
               new Value("Length", typeof(short)),
           }),
           new TagBlock("Involuntary Data", 4, 100, new MetaNode[] { 
               new Value("Involuntary Localization Index", typeof(int)),
           }),
           });
       }
    }
}
