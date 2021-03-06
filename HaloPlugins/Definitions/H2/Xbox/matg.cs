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
    public class matg : TagDefinition
    {
       public matg() : base("matg", "match_globals", 644)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(172),
           new HaloPlugins.Objects.Data.Enum("Language", new string[] {  }, 32),
           new TagBlock("Havok Cleanup Resources", 8, 1, new MetaNode[] {
               new TagReference("Object_Cleanup_Effect", "effe"),
           }),
           new TagBlock("Collision Damage", 72, 1, new MetaNode[] { 
               new TagReference("Collision_Damage", "jpt!"),
               new Value("Min Game Acc (Default)", typeof(float)),
               new Value("Max Game Acc (Default)", typeof(float)),
               new Value("Min Gam Scale (Default)", typeof(float)),
               new Value("Max Gam Scale (Default)", typeof(float)),
               new Value("Min abs Acc (Default)", typeof(float)),
               new Value("Max abs Acc (Default)", typeof(float)),
               new Value("Min abs Scale (Default)", typeof(float)),
               new Value("Max abs Scale (Default)", typeof(float)),
               new Padding(32),
           }),
           new TagBlock("Sound Info", 36, 1, new MetaNode[] { 
               new TagReference("Sound_Class", "sncl"),
               new TagReference("Sound_FX", "sfx+"),
               new TagReference("Sound_Mix", "snmx"),
               new TagReference("Sound_Combat_Dialogue", "adlg"),
               new TagIndex("Resources", "****"),
           }),
           new TagBlock("AI Information", 360, 1, new MetaNode[] { 
               new Value("Danger Broadly Facing", typeof(float)),
               new Padding(4),
               new Value("Danger Shooting Near", typeof(float)),
               new Padding(4),
               new Value("Danger Shooting At", typeof(float)),
               new Padding(4),
               new Value("Danger Extremely Close", typeof(float)),
               new Padding(4),
               new Value("Danger Sheild Damage", typeof(float)),
               new Value("Danger Extended Shield Damage", typeof(float)),
               new Value("Danger Body Damage", typeof(float)),
               new Value("Danger Extended Body Damage", typeof(float)),
               new Padding(48),
               new TagReference("AI_Dialog", "adlg"),
               new StringId("default_mission_dialogue_sound_effect"),
               new Padding(20),
               new Value("Jump Down", typeof(float)),
               new Value("Jump Step", typeof(float)),
               new Value("Jump Crouch", typeof(float)),
               new Value("Jump Stand", typeof(float)),
               new Value("Jump Storey", typeof(float)),
               new Value("Jump Tower", typeof(float)),
               new Value("Max Jump Down Height Down", typeof(float)),
               new Value("Max Jump Down Height Step", typeof(float)),
               new Value("Max Jump Down Height Crouch", typeof(float)),
               new Value("Max Jump Down Height Stand", typeof(float)),
               new Value("Max Jump Down Height Storey", typeof(float)),
               new Value("Max Jump Down Height Tower", typeof(float)),
               new Value("Hoist Step", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Hoist Crouch", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Hoist Stand", typeof(float)),
               new Value("...To", typeof(float)),
               new Padding(24),
               new Value("Vault Step", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Vault Crouch", typeof(float)),
               new Value("...To", typeof(float)),
               new Padding(48),
               new TagBlock("Gravemind Properties", 12, 1, new MetaNode[] { 
                   new Value("Min Retreat Time", typeof(float)),
                   new Value("Ideal Retreat Time", typeof(float)),
                   new Value("Max Retreat Time", typeof(float)),
               }),
               new Padding(48),
               new Value("Scary Target Threhold", typeof(float)),
               new Value("Scary Weapon Threhold", typeof(float)),
               new Value("Player Scariness", typeof(float)),
               new Value("Berserking Actor Scariness", typeof(float)),
           }),
           new TagBlock("Damage Table", 8, 1, new MetaNode[] { 
               new TagBlock("Damage Groups", 12, -1, new MetaNode[] { 
                   new StringId("Name"),
                   new TagBlock("Armor Modifiers", 8, -1, new MetaNode[] { 
                       new StringId("Name"),
                       new Value("Damage Multiplier", typeof(float)),
                   }),
               }),
           }),
           new Padding(8),
           new TagBlock("Sounds (Obsolete)", 8, 2, new MetaNode[] { 
               new TagReference("Sound", "****"),
           }),
           new TagBlock("Camera", 20, 1, new MetaNode[] { 
               new TagReference("Default_Unit_Camera_Track", "trak"),
               new Value("Default Change Pause", typeof(float)),
               new Value("1st Person Change Pause", typeof(float)),
               new Value("Following Camera Change Pause", typeof(float)),
           }),
           new TagBlock("Player Control", 128, 1, new MetaNode[] { 
               new Value("Magnetism Friction", typeof(float)),
               new Value("Magnetism Adhesion", typeof(float)),
               new Value("Inconsequential Target Scale", typeof(float)),
               new Padding(12),
               new Value("Crosshair Location X", typeof(float)),
               new Value("Crosshair Location Y", typeof(float)),
               new Value("Seconds To Start", typeof(float)),
               new Value("Seconds To Full Speed", typeof(float)),
               new Value("Decay Rate", typeof(float)),
               new Value("Full Speed Multiplier", typeof(float)),
               new Value("Pegged Magnitude", typeof(float)),
               new Value("Pegged Angular Threshold", typeof(float)),
               new Padding(8),
               new Value("Look Default Pitch Rate", typeof(float)),
               new Value("Look Default Yaw Rate", typeof(float)),
               new Value("Look Peg Threshold", typeof(float)),
               new Value("Look Yaw Acceleration Time", typeof(float)),
               new Value("Look Yaw Acceleration Scale", typeof(float)),
               new Value("Look Pitch Acceleration Time", typeof(float)),
               new Value("Look Pitch Acceleration Scale", typeof(float)),
               new Value("Look Auto-leveling Scale", typeof(float)),
               new Padding(12),
               new Value("Min Weapon Swap Ticks", typeof(short)),
               new Value("Min Autoleveling Ticks", typeof(short)),
               new Value("Gravity Scale", typeof(float)),
               new TagBlock("Look Function", 4, 16, new MetaNode[] { 
                   new Value("Scale", typeof(float)),
               }),
               new Value("Minimmum Action Hold Time", typeof(float)),
           }),
           new TagBlock("Difficulty", 644, 1, new MetaNode[] { 
               new Value("Easy Enemy Damage", typeof(float)),
               new Value("Normal Enemy Damage", typeof(float)),
               new Value("Heroic Enemy Damage", typeof(float)),
               new Value("Legendary Enemy Damage", typeof(float)),
               new Value("Easy Enemy Vitality", typeof(float)),
               new Value("Normal Enemy Vitality", typeof(float)),
               new Value("Heroic Enemy Vitality", typeof(float)),
               new Value("Legendary Enemy Vitality", typeof(float)),
               new Value("Easy Enemy Sheild", typeof(float)),
               new Value("Normal Enemy Sheild", typeof(float)),
               new Value("Heroic Enemy Sheild", typeof(float)),
               new Value("Legendary Enemy Sheild", typeof(float)),
               new Value("Easy Enemy Recharge", typeof(float)),
               new Value("Normal Enemy Recharge", typeof(float)),
               new Value("Heroic Enemy Recharge", typeof(float)),
               new Value("Legendary Enemy Recharge", typeof(float)),
               new Value("Easy Friend Damage", typeof(float)),
               new Value("Normal Friend Damage", typeof(float)),
               new Value("Heroic Friend Damage", typeof(float)),
               new Value("Legendary Friend Damage", typeof(float)),
               new Value("Easy Friend Vitality", typeof(float)),
               new Value("Normal Friend Vitality", typeof(float)),
               new Value("Heroic Friend Vitality", typeof(float)),
               new Value("Legendary Friend Vitality", typeof(float)),
               new Value("Easy Friend Sheild", typeof(float)),
               new Value("Normal Friend Sheild", typeof(float)),
               new Value("Heroic Friend Sheild", typeof(float)),
               new Value("Legendary Friend Sheild", typeof(float)),
               new Value("Easy Friend Recharge", typeof(float)),
               new Value("Normal Friend Recharge", typeof(float)),
               new Value("Heroic Friend Recharge", typeof(float)),
               new Value("Legendary Friend Recharge", typeof(float)),
               new Value("Easy Infection Forms", typeof(float)),
               new Value("Normal Infection Forms", typeof(float)),
               new Value("Heroic Infection Forms", typeof(float)),
               new Value("Legendary Infection Forms", typeof(float)),
               new Padding(16),
               new Value("Easy Rate Of Fire", typeof(float)),
               new Value("Normal Rate Of Fire", typeof(float)),
               new Value("Heroic Rate Of Fire", typeof(float)),
               new Value("Legendary Rate Of Fire", typeof(float)),
               new Value("Easy Projectile Error", typeof(float)),
               new Value("Normal Projectile Error", typeof(float)),
               new Value("Heroic Projectile Error", typeof(float)),
               new Value("Legendary Projectile Error", typeof(float)),
               new Value("Easy Burst Error", typeof(float)),
               new Value("Normal Burst Error", typeof(float)),
               new Value("Heroic Burst Error", typeof(float)),
               new Value("Legendary Burst Error", typeof(float)),
               new Value("Easy New Target Delay", typeof(float)),
               new Value("Normal New Target Delay", typeof(float)),
               new Value("Heroic New Target Delay", typeof(float)),
               new Value("Legendary New Target Delay", typeof(float)),
               new Value("Easy Burst Separation", typeof(float)),
               new Value("Normal Burst Separation", typeof(float)),
               new Value("Heroic Burst Separation", typeof(float)),
               new Value("Legendary Burst Separation", typeof(float)),
               new Value("Easy Target Tracking", typeof(float)),
               new Value("Normal Target Tracking", typeof(float)),
               new Value("Heroic Target Tracking", typeof(float)),
               new Value("Legendary Target Tracking", typeof(float)),
               new Value("Easy Target Leading", typeof(float)),
               new Value("Normal Target Leading", typeof(float)),
               new Value("Heroic Target Leading", typeof(float)),
               new Value("Legendary Target Leading", typeof(float)),
               new Value("Easy Overcharge Chance", typeof(float)),
               new Value("Normal Overcharge Chance", typeof(float)),
               new Value("Heroic Overcharge Chance", typeof(float)),
               new Value("Legendary Overcharge Chance", typeof(float)),
               new Value("Easy Special Fire Delay", typeof(float)),
               new Value("Normal Special Fire Delay", typeof(float)),
               new Value("Heroic Special Fire Delay", typeof(float)),
               new Value("Legendary Special Fire Delay", typeof(float)),
               new Value("Easy Guidance Vs Player", typeof(float)),
               new Value("Normal Guidance Vs Player", typeof(float)),
               new Value("Heroic Guidance Vs Player", typeof(float)),
               new Value("Legendary Guidance Vs Player", typeof(float)),
               new Value("Easy Melee Delay Base", typeof(float)),
               new Value("Normal Melee Delay Base", typeof(float)),
               new Value("Heroic Melee Delay Base", typeof(float)),
               new Value("Legendary Melee Delay Base", typeof(float)),
               new Value("Easy Melee Delay Scale", typeof(float)),
               new Value("Normal Melee Delay Scale", typeof(float)),
               new Value("Heroic Melee Delay Scale", typeof(float)),
               new Value("Legendary Melee Delay Scale", typeof(float)),
               new Padding(16),
               new Value("Easy Grenade Chance Scale", typeof(float)),
               new Value("Normal Grenade Chance Scale", typeof(float)),
               new Value("Heroic Grenade Chance Scale", typeof(float)),
               new Value("Legendary Grenade Chance Scale", typeof(float)),
               new Value("Easy Grenade Timer Scale", typeof(float)),
               new Value("Normal Grenade Timer Scale", typeof(float)),
               new Value("Heroic Grenade Timer Scale", typeof(float)),
               new Value("Legendary Grenade Timer Scale", typeof(float)),
               new Padding(48),
               new Value("Easy Major Upgrade (Normal)", typeof(float)),
               new Value("Normal Major Upgrade (Normal)", typeof(float)),
               new Value("Heroic Major Upgrade (Normal)", typeof(float)),
               new Value("Legendary Major Upgrade (Normal)", typeof(float)),
               new Value("Easy Major Upgrade (Few)", typeof(float)),
               new Value("Normal Major Upgrade (Few)", typeof(float)),
               new Value("Heroic Major Upgrade (Few)", typeof(float)),
               new Value("Legendary Major Upgrade (Few)", typeof(float)),
               new Value("Easy Major Upgrade (Many)", typeof(float)),
               new Value("Normal Major Upgrade (Many)", typeof(float)),
               new Value("Heroic Major Upgrade (Many)", typeof(float)),
               new Value("Legendary Major Upgrade (Many)", typeof(float)),
               new Value("Easy Player Vehicle Ram Chance", typeof(float)),
               new Value("Normal Player Vehicle Ram Chance", typeof(float)),
               new Value("Heroic Player Vehicle Ram Chance", typeof(float)),
               new Value("Legendary Player Vehicle Ram Chance", typeof(float)),
               new Padding(132),
           }),
           new TagBlock("Grenades", 44, 2, new MetaNode[] { 
               new Value("Max Count", typeof(short)),
               new Padding(2),
               new TagReference("Throwing_Effect", "effe"),
               new Padding(16),
               new TagReference("Equipment", "eqip"),
               new TagReference("Projectile", "proj"),
           }),
           new TagBlock("Rasterizer Data", 264, 1, new MetaNode[] { 
               new TagReference("Distance_Attenuation", "bitm"),
               new TagReference("Vector_Normalization", "bitm"),
               new TagReference("Gradients", "bitm"),
               new TagReference("Loading_Screen", "bitm"),
               new TagReference("Loading_Screen_Sweep", "bitm"),
               new TagReference("Loading_Screen_Spinner", "bitm"),
               new TagReference("Glow", "bitm"),
               new TagReference("Loading_Screen_Logos", "bitm"),
               new TagReference("Loading_Screen_Tickers", "bitm"),
               new Padding(16),
               new TagBlock("Global Vertex Shaders", 8, 32, new MetaNode[] { 
                   new TagReference("Vertex_Shader", "vrtx"),
               }),
               new TagReference("Default_2D", "bitm"),
               new TagReference("Default_3D", "bitm"),
               new TagReference("Default_Cube_Map", "bitm"),
               new Padding(48),
               new Padding(36),
               new TagReference("Global_Shader", "shad"),
               new Flags("Flags", new string[] { "Tint_Edge_Density" }, 32),
               new Value("Active Camo Refraction Amount", typeof(float)),
               new Value("Active Camo Distance Falloff", typeof(float)),
               new Value("Active Camo Tint Color R", typeof(float)),
               new Value("Active Camo Tint Color G", typeof(float)),
               new Value("Active Camo Tint Color B", typeof(float)),
               new Value("Active Camo Hyper Stealth Refraction", typeof(float)),
               new Value("Active Camo Hyper Stealth Distance Falloff", typeof(float)),
               new Value("Active Camo Hyper Stealth Tint  R", typeof(float)),
               new Value("Active Camo Hyper Stealth Tint G", typeof(float)),
               new Value("Active Camo Hyper Stealth Tint B", typeof(float)),
               new TagReference("unused", "bitm"),
           }),
           new TagBlock("Interface Tag Refernces", 152, 1, new MetaNode[] { 
               new TagReference("Font_System", "bitm"),
               new TagReference("Font_Terminal", "bitm"),
               new TagReference("Screen_Color_Table", "colo"),
               new TagReference("Hud_Color_Table", "colo"),
               new TagReference("Editor_Color_Table", "colo"),
               new TagReference("Dialog_Color_Table", "colo"),
               new TagReference("Hud_Globals", "hudg"),
               new TagReference("Motion_Sensor_Sweep_Bitmap", "bitm"),
               new TagReference("Motion_Sensor_Sweep_Bitmap_Mask", "bitm"),
               new TagReference("Multiplayer_Hud_Bitmap", "bitm"),
               new Padding(8),
               new TagReference("Hud_Digits_Definition", "hud#"),
               new TagReference("Motion_Sensor_Blip_Bitmap", "bitm"),
               new TagReference("Interface_Goo_Map_1", "bitm"),
               new TagReference("Interface_Goo_Map_2", "bitm"),
               new TagReference("Interface_Goo_Map_3", "bitm"),
               new TagReference("Mainmenu_UI_Globals", "wgtz"),
               new TagReference("Singleplayer_UI_Globals", "wgtz"),
               new TagReference("Multiplayer_UI_Globals", "wgtz"),
           }),
           new TagBlock("Weapon List (To Do With Hard Coded Enum)", 152, 20, new MetaNode[] { 
               new TagReference("Weapon", "item"),
               new Padding(144),
           }),
           new TagBlock("Cheat Powerups", 152, 20, new MetaNode[] { 
               new TagReference("Powerup", "eqip"),
               new Padding(144),
           }),
           new TagBlock("Multiplayer Information (depricated)", 152, 1, new MetaNode[] { 
               new TagReference("Flag", "item"),
               new TagReference("Unit", "unit"),
               new Padding(8),
               new TagReference("Hill_Shader", "shad"),
               new TagReference("Flag_Shader", "shad"),
               new TagReference("Ball", "item"),
               new Padding(104),
           }),
           new TagBlock("Player Information", 284, 1, new MetaNode[] { 
               new TagReference("unused", "unit"),
               new Padding(28),
               new Value("Walking Speed", typeof(float)),
               new Padding(4),
               new Value("Forward Run Speed", typeof(float)),
               new Value("Backward Run Speed", typeof(float)),
               new Value("Sideways Run Speed", typeof(float)),
               new Value("Run Acceleration", typeof(float)),
               new Value("Forward Crouch Speed", typeof(float)),
               new Value("Backward Crouch Speed", typeof(float)),
               new Value("Sideways Crouch Speed", typeof(float)),
               new Value("Crouch Acceleration", typeof(float)),
               new Value("Airborn Acceleration", typeof(float)),
               new Padding(16),
               new Value("Grenade Origin X", typeof(float)),
               new Value("Grenade Origin Y", typeof(float)),
               new Value("Grenade Origin Z", typeof(float)),
               new Padding(12),
               new Value("Stun Penalty Movement", typeof(float)),
               new Value("Stun Penalty Turning", typeof(float)),
               new Value("Stun Penalty Jumping", typeof(float)),
               new Value("Min Stun Time", typeof(float)),
               new Value("Max Stun Time", typeof(float)),
               new Padding(8),
               new Value("Fp Idle Time", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Fp Skip Fraction", typeof(float)),
               new Padding(16),
               new TagReference("Coop_Respawn_Effect", "effe"),
               new Value("Binoculars Zoom Count", typeof(float)),
               new Value("Binoculars Zoom Range", typeof(float)),
               new Value("...To", typeof(float)),
               new TagReference("Binoculars_Zoom_In_Sound", "snd!"),
               new TagReference("Binoculars_Zoom_Out_Sound", "snd!"),
               new Padding(16),
               new TagReference("Active_Camo_On", "snd!"),
               new TagReference("Active_Camo_Off", "snd!"),
               new TagReference("Active_Camo_Error", "snd!"),
               new TagReference("Active_Camo_Ready", "snd!"),
               new TagReference("Flashlight_On", "snd!"),
               new TagReference("Flashlight_Off", "snd!"),
               new TagReference("Ice_Cream", "snd!"),
           }),
           new TagBlock("Player Representation", 188, 4, new MetaNode[] { 
               new TagReference("1st_Person_Hands", "mode"),
               new TagReference("1st_Person_Body", "mode"),
               new Padding(160),
               new TagReference("3rd_Person_Unit", "unit"),
               new StringId("3rd Person Unit Variant"),
           }),
           new TagBlock("Falling Damage", 104, 1, new MetaNode[] { 
               new Padding(8),
               new Value("Harmful Falling Distance", typeof(float)),
               new Value("...To", typeof(float)),
               new TagReference("Falling_Damage", "jpt!"),
               new Padding(8),
               new Value("Maximum Falling Distance", typeof(float)),
               new TagReference("Distance_Damage", "jpt!"),
               new TagReference("Vehicle_Environment_Collision_Damage", "jpt!"),
               new TagReference("Vehicle_Killed_Unit_Damage_Effect", "jpt!"),
               new TagReference("Vehicle_Collision_Damage", "jpt!"),
               new TagReference("Flaming_Death_Damage", "jpt!"),
               new Padding(28),
           }),
           new TagBlock("Old Materials", 36, 33, new MetaNode[] { 
               new StringId("Material Name"),
               new StringId("General Material Name"),
               new Value("Ground Friction Scale", typeof(float)),
               new Value("Ground Friction Normal K0 Scale", typeof(float)),
               new Value("Ground Friction Normal K1 Scale", typeof(float)),
               new Value("Ground Depth Scale", typeof(float)),
               new Value("Ground Damp Fraction Scale", typeof(float)),
               new TagReference("Melee_Hit_Sound", "snd!"),
           }),
           new TagBlock("Materials", 180, 256, new MetaNode[] { 
               new StringId("Name"),
               new StringId("Parent Name"),
               new Flags("Flags", new string[] {  }, 32),
               new HaloPlugins.Objects.Data.Enum("Old Material Type", new string[] { "Dirt", "Sand", "Stone", "Snow", "Wood", "Metal_(hollow)", "Metal_(thin)", "Metal_(thick)", "Rubber", "Glass", "Force_Field", "Grunt", "Hunter_Armor", "Hunter_Skin", "Elite", "Jackal", "Jackal_Energy_Shield", "Engineer_Skin", "Engineer_Force_Field", "Flood_Combat_Form", "Flood_Carrier_Form", "Cyborg_Armor", "Cyborg_Energy_Shield", "Human_Armor", "Human_Skin", "Sentinel", "Monitor", "Plastic", "Water", "Leaves", "Elite_Energy_Shield", "Ice", "Hunter_Shield" }, 32),
               new StringId("General Armor"),
               new StringId("Specific Armor"),
               new Padding(4),
               new Value("Friction", typeof(float)),
               new Value("Restitution", typeof(float)),
               new Value("Density", typeof(float)),
               new TagReference("Old_Material_Physics", "mpdt"),
               new TagReference("Breakable_Surface", "bsdt"),
               new TagReference("Sound_Sweetener_(small)", "snd!"),
               new TagReference("Sound_Sweetener_(medium)", "snd!"),
               new TagReference("Sound_Sweetener_(large)", "snd!"),
               new TagReference("Sound_Sweetener_Rolling", "snd!"),
               new TagReference("Sound_Sweetener_Grinding", "snd!"),
               new TagReference("Sound_Sweetener_(melee)", "snd!"),
               new Padding(8),
               new TagReference("Effect_Sweetener_(small)", "effe"),
               new TagReference("Effect_Sweetener_(medium)", "effe"),
               new TagReference("Effect_Sweetener_(large)", "effe"),
               new TagReference("Effect_Sweetener_Rolling", "effe"),
               new TagReference("Effect_Sweetener_Grinding", "effe"),
               new TagReference("Effect_Sweetener_(melee)", "effe"),
               new Padding(8),
               new Flags("Sweetener Inheritance Flags", new string[] { "Sound_Small", "Sound_Medium", "Sound_Large", "Sound_Rolling", "Sound_Grinding", "Sound_Melee", "", "Effect_Small", "Effect_Medium", "Effect_Large", "Effect_Rolling", "Effect_Grinding", "Effect_Melee" }, 32),
               new TagReference("Material_Effects", "foot"),
           }),
           new TagBlock("Multiplayer UI (obsolete)", 32, 1, new MetaNode[] { 
               new Padding(32),
           }),
           new TagBlock("Profile Colors", 12, 32, new MetaNode[] { 
               new Value("R", typeof(float)),
               new Value("G", typeof(float)),
               new Value("B", typeof(float)),
           }),
           new TagReference("Multiplayer_Globals", "mulg"),
           new TagBlock("Runtime Level Data", 8, 1, new MetaNode[] { 
               new TagBlock("Campaign Levels", 264, 20, new MetaNode[] { 
                   new Value("Campaign ID", typeof(int)),
                   new Value("Map ID", typeof(int)),
                   new LongString("Scenario Path", StringType.Asci),
               }),
           }),
           new TagBlock("UI Level Data", 24, 1, new MetaNode[] { 
               new TagBlock("Campaigns", 2884, 4, new MetaNode[] { 
                   new Value("Campaign ID", typeof(int)),
                   new Padding(2880),
               }),
               new TagBlock("Campaign Levels", 2896, 20, new MetaNode[] { 
                   new Value("Campaign ID", typeof(int)),
                   new Value("Map ID", typeof(int)),
                   new TagReference("Preview_Image", "bitm"),
                   new ShortString("English Name", StringType.Unicode),
                   new ShortString("Japanese Name", StringType.Unicode),
                   new ShortString("German Name", StringType.Unicode),
                   new ShortString("French Name", StringType.Unicode),
                   new ShortString("Spanish Name", StringType.Unicode),
                   new ShortString("Italian Name", StringType.Unicode),
                   new ShortString("Korean Name", StringType.Unicode),
                   new ShortString("Chinese Name", StringType.Unicode),
                   new ShortString("Portuguese Name", StringType.Unicode),
                   new LongString("English Description", StringType.Unicode),
                   new LongString("Japanese Description", StringType.Unicode),
                   new LongString("German Description", StringType.Unicode),
                   new LongString("French Description", StringType.Unicode),
                   new LongString("Spanish Description", StringType.Unicode),
                   new LongString("Italian Description", StringType.Unicode),
                   new LongString("Korean Description", StringType.Unicode),
                   new LongString("Chinese Description", StringType.Unicode),
                   new LongString("Portuguese Description", StringType.Unicode),
               }),
               new TagBlock("Multiplayer", 3172, 50, new MetaNode[] { 
                   new Value("Map ID", typeof(int)),
                   new TagReference("Preview_Image", "bitm"),
                   new ShortString("English Name", StringType.Unicode),
                   new ShortString("Japanese Name", StringType.Unicode),
                   new ShortString("German Name", StringType.Unicode),
                   new ShortString("French Name", StringType.Unicode),
                   new ShortString("Spanish Name", StringType.Unicode),
                   new ShortString("Italian Name", StringType.Unicode),
                   new ShortString("Korean Name", StringType.Unicode),
                   new ShortString("Chinese Name", StringType.Unicode),
                   new ShortString("Portuguese Name", StringType.Unicode),
                   new LongString("English Description", StringType.Unicode),
                   new LongString("Japanese Description", StringType.Unicode),
                   new LongString("German Description", StringType.Unicode),
                   new LongString("French Description", StringType.Unicode),
                   new LongString("Spanish Description", StringType.Unicode),
                   new LongString("Italian Description", StringType.Unicode),
                   new LongString("Korean Description", StringType.Unicode),
                   new LongString("Chinese Description", StringType.Unicode),
                   new LongString("Portuguese Description", StringType.Unicode),
                   new LongString("Scenario Path", StringType.Asci),
                   new Value("Sort Order", typeof(int)),
                   new Flags("Flags", new string[] { "Unlockable" }, 16),
                   new Value("Max Teams None", typeof(byte)),
                   new Value("Max Teams CTF", typeof(byte)),
                   new Value("Max Teams Slayer", typeof(byte)),
                   new Value("Max Teams Oddball", typeof(byte)),
                   new Value("Max Teams KOTH", typeof(byte)),
                   new Value("Max Teams Race", typeof(byte)),
                   new Value("Max Teams Headhunter", typeof(byte)),
                   new Value("Max Teams Juggernaught", typeof(byte)),
                   new Value("Max Teams Territories", typeof(byte)),
                   new Value("Max Teams Assault", typeof(byte)),
                   new Value("Max Teams Stub 10", typeof(byte)),
                   new Value("Max Teams Stub 11", typeof(byte)),
                   new Value("Max Teams Stub 12", typeof(byte)),
                   new Value("Max Teams Stub 13", typeof(byte)),
                   new Value("Max Teams Stub 14", typeof(byte)),
                   new Value("Max Teams Stub 15", typeof(byte)),
                   new Padding(2),
               }),
           }),
           new TagReference("Default_Global_Lighting", "gldf"),
           new Padding(8),
           new Value("English String Count", typeof(int)),
           new Value("English String Table Size", typeof(int)),
           new Value("English String Index Offset", typeof(int)),
           new Value("English String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("Japanese String Count", typeof(int)),
           new Value("Japanese String Table Size", typeof(int)),
           new Value("Japanese String Index Offset", typeof(int)),
           new Value("Japanese String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("Dutch String Count", typeof(int)),
           new Value("Dutch String Table Size", typeof(int)),
           new Value("Dutch String Index Offset", typeof(int)),
           new Value("Dutch String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("French String Count", typeof(int)),
           new Value("French String Table Size", typeof(int)),
           new Value("French String Index Offset", typeof(int)),
           new Value("French String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("Spanish String Count", typeof(int)),
           new Value("Spanish String Table Size", typeof(int)),
           new Value("Spanish String Index Offset", typeof(int)),
           new Value("Spanish String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("Italian String Count", typeof(int)),
           new Value("Italian String Table Size", typeof(int)),
           new Value("Italian String Index Offset", typeof(int)),
           new Value("Italian String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("Korean String Count", typeof(int)),
           new Value("Korean String Table Size", typeof(int)),
           new Value("Korean String Index Offset", typeof(int)),
           new Value("Korean String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("Chinese String Count", typeof(int)),
           new Value("Chinese String Table Size", typeof(int)),
           new Value("Chinese String Index Offset", typeof(int)),
           new Value("Chinese String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(8),
           new Value("Portuguese String Count", typeof(int)),
           new Value("Portuguese String Table Size", typeof(int)),
           new Value("Portuguese String Index Offset", typeof(int)),
           new Value("Portuguese String Table Offset", typeof(int)),
           new Value("Unknown", typeof(int)),
           });
       }
    }
}
