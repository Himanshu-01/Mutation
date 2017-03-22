using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class sily : TagDefinition
    {
       public sily() : base("sily", "ui_option", 36)
       {
           Fields.AddRange(new MetaNode[] {
           new HaloPlugins.Objects.Data.Enum("Setting Catergory", new string[] { "Match_Round_Setting", "Match_CTF_Score_To_Win", "Match_Slayer_Score_To_Win_Round", "Match_Oddball_Score_To_Win_Round", "Match_King_Score_To_Win_Round", "Match_Race_Score_To_Win_Round", "Match_Headhunter_Score_To_Win_Round", "Match_Juggernaught_Score_To_Win_Round", "Match_Territories_Score_To_Win_Round", "Match_Assault_Score_To_Win_Round", "Match_Round_Time_Limit", "Match_Rounds_Reset_Map", "Match_Tie_Resolution", "Match_Observers", "Match_Join_In_Progress", "Max_Players", "Lives_Per_Round", "Respawn_Time", "Suicide_Penalty", "Shields", "Motion_Sensor", "Invisibility", "Team_Changing", "Team_Scoring", "Friendly_Fire", "Team_Respawn_Setting", "Betrayal_Respawn_Penalty", "Team_Killer_Management", "Slayer_Bonus_Points", "Slayer_Suidcide_Point_Loss", "Slayer_Death_Point_Loss", "Headhunter_Moving_Head_Bin", "Headhunter_Point_Multiplier", "Headhunter_Suicide_Point_Loss", "Headhunter_Death_Point_Loss", "Headhunter_Uncontested_Bin", "Headhunter_Speed_With_Heads", "Headhunter_Max_Heads_Carried", "King_Uncontested_Hill", "King_Team_Time_Multiplier", "King_Moving_Hill", "King_Extra_Damage_On_Hill", "King_Damage_Resistance_On_Hill", "Oddball_Ball_Spawn_Count", "Oddball_Ball_Hit_Damage", "Oddball_Speed_With_Ball", "Oddball_Driving/Gunning_With_Ball", "Oddball_Waypoint_To_Ball", "Race_Random_Track", "Race_Uncontested_Flag", "CTF_Sudden_Death", "CTF_Flag_May_Be_Returned", "CTF_Flag_At_Home_To_Score", "CTF_Flag_Reset_Time", "CTF_Speed_With_Flag", "CTF_Flag_Hit_Damage", "CTF_Driving/Gunning_With_Flag", "CTF_Waypoint_To_Own_Flag", "Assault_Game_Type", "Assault_Sudden_Death", "Assault_Detonation_Time", "Assault_Bomb_At_Home_To_Score", "Assault_Arming_Time", "Assault_Speed_With_Bomb", "Assault_Bomb_Hit_Damage", "Assault_Driving/Gunning_With_Bomb", "Assault_Waypoint_To_Own_Bomb", "Juggernaught_Betrayal_Point_Loss", "Juggernaught_Juggy_Extra_Damage", "Juggernaught_Infinite_Ammo", "Juggernaught_Juggy_Oversheilds", "Juggernaught_Juggy_Active_Camo", "Juggernaught_Juggy_Montion_Sensor", "Territories_Territory_Count", "Vehi._Respawn", "Vehi._Primary_Light_Land", "Vehi._Secondary_Light_Land", "Vehi._Primary_Heavy_Land", "Vehi._Primary_Flying", "Vehi._Secondary_Heavy_Land", "Vehi._Primary_Turret", "Vehi._Secondary_Turret", "Equip._Weapons_On_Map", "Equip._Oversheilds_On_Map", "Equip._Active_Camo_On_Map", "Equip._Grenades_On_Map", "Equip._Weapon_Respawn_Times", "Equip._Starting_Grenades", "Equip._Primary_Starting_Equipment", "UNS._Max_Living_Players", "UNS._Teams_Enabled", "UNS._Assault_Bomb_May_Be_Returned", "UNS._Max_Teams", "UNS._Equip._Secondary_Starting_Equipment", "UNS._Assault_Fuse_Time", "UNS._Juggy_Movement", "UNS._Sticky_Fuse", "UNS._Terr._Contest_Time", "UNS._Terr._Control_Time", "UNS._Oddb._Carr._Invisible", "UNS._King_Invisible_In_Hill", "UNS._Ball_Carr._Dmg._Resistance", "UNS._King_Dmg._Res._In_Hill", "UNS._Players_Extra_Dmg", "UNS._Players_Dmg._Resistance", "UNS._CTF_Carr._Invisible", "UNS._Juggy_Dmg._Resistance", "UNS._Bomb_Carr._Dmg._Resistance", "UNS._Bomb_Carr._Invisible", "UNS._Force_Even_Teams" }, 32),
           new Value("Value", typeof(int)),
           new TagReference("Unicode_String_List_Of_Options", "unic"),
           new StringId("Title Text"),
           new StringId("Header Text"),
           new StringId("Description Text"),
           new TagBlock("Options", 12, 32, new MetaNode[] { 
               new Flags("Flags", new string[] { "Default" }, 32),
               new Value("Value", typeof(short)),
               new Padding(2),
               new StringId("Label"),
           }),
           });
       }
    }
}
