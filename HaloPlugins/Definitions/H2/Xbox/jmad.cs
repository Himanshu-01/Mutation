using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.H2.Xbox;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class jmad : TagDefinition
    {
       public jmad() : base("jmad", "model_animation_graph", 188, new AnimationDefinition())
       {
           Fields.AddRange(new MetaNode[] {
           new TagReference("Parent_Animation", "jmad"),
           new Flags("Inheritance flags", new string[] { "Inherit_Root_Trans_Scale_Only", "Inherit_for_use_on_player" }, 8),
           new Flags("Private FLags", new string[] { "Prepared_for_cahce", "unused", "Imported_with_codec_compressor", "unused_smelly_flag", "Written_to_Cache", "Animation_Data_Recorded" }, 8),
           new Value("Codec", typeof(short)),
           new TagBlock("Skeleton Nodes", 32, -1, new MetaNode[] { 
               new StringId("Name"),
               new Value("Next Sibling Node Index", typeof(short)),
               new Value("First Child Node Index", typeof(short)),
               new Value("Parent Node Index", typeof(short)),
               new Flags("Model Flags", new string[] { "Primary_Model", "Secondary_Model", "Local_Root", "Left_Hand", "Right_Hand", "Left_Arm_Member" }, 8),
               new Flags("Node Joint Flags", new string[] { "Ball_Socket", "Hinge", "No_Movement" }, 8),
               new Value("Base Vector I", typeof(float)),
               new Value("Base Vector J", typeof(float)),
               new Value("Base Vector K", typeof(float)),
               new Value("Vector Range", typeof(float)),
               new Value("z_pos", typeof(float)),
           }),
           new TagBlock("Sounds", 12, -1, new MetaNode[] { 
               new TagReference("Sound", "snd!"),
               new Flags("FLags", new string[] { "Allow_On_Player", "Left_Arm_Only", "Right_Arm_Only", "First_Person_Only", "Foward_Only", "Reverse_Only" }, 32),
           }),
           new TagBlock("Effects", 12, -1, new MetaNode[] { 
               new TagReference("Effect", "effe"),
               new Flags("FLags", new string[] { "Allow_On_Player", "Left_Arm_Only", "Right_Arm_Only", "First_Person_Only", "Foward_Only", "Reverse_Only" }, 32),
           }),
           new TagBlock("Blend Screens", 28, -1, new MetaNode[] { 
               new StringId("label"),
               new Value("Right yaw per frame", typeof(float)),
               new Value("Left yaw per frame", typeof(float)),
               new Value("Right frame count", typeof(short)),
               new Value("Left frame count", typeof(short)),
               new Value("Down pitch per frame", typeof(float)),
               new Value("Up pitch per frame", typeof(float)),
               new Value("Down pitch frame count", typeof(short)),
               new Value("Up pitch frame count", typeof(short)),
           }),
           new TagBlock("Animations", 108, -1, new MetaNode[] { 
               new StringId("Name"),
               new Value("Node List Checksum", typeof(int)),
               new Value("Production Checksum", typeof(int)),
               new Value("Import Checksum", typeof(int)),
               new HaloPlugins.Objects.Data.Enum("Type Index", new string[] { "Base" }, 8),
               new HaloPlugins.Objects.Data.Enum("Frame Info Type Index", new string[] { "None" }, 8),
               new Value("Blend Screen Index", typeof(byte)),
               new Value("Node Count", typeof(byte)),
               new Value("Frame Count", typeof(short)),
               new Flags("Internal Flags", new string[] { "Unused", "World_Relative", "Unused", "Unused", "Unused", "Compression_Disabled", "Old_Production_Checksum", "Valid_Production_Checksum" }, 16),
               new Flags("Production Flags", new string[] { "Do_not_monitor_changes", "Verify_Sound_Events", "Do_not_inherit_for_Player_Graphs" }, 16),
               new Flags("Playback FLags", new string[] { "Disable_Interpolation_In", "Disable_Interpolation_Out", "Disable_Mode_ik", "Disable_Weapon_ik", "Disable_Weapon_Aim/1st_Person", "Disable_Look_Screen", "Disable_Transition_Adjustment" }, 16),
               new Value("Unknown", typeof(float)),
               new Padding(4),
               new Value("Raw Chunk #", typeof(int)),
               new Value("Offset in raw", typeof(short)),
               new Value("Unused", typeof(int)),
               new Flags("Doesn't Repeat", new string[] { "Doesn't_Repeat" }, 16),
               new Value("Unknown", typeof(short)),
               new Value("Unknown", typeof(short)),
               new Padding(8),
               new Value("Unknown", typeof(byte)),
               new Value("Unknown", typeof(byte)),
               new Value("Unknown", typeof(byte)),
               new Value("Unknown", typeof(byte)),
               new Value("Unknown", typeof(short)),
               new Value("Animation Raw Something", typeof(short)),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(byte)),
               new Value("Unknown", typeof(byte)),
               new Value("Loop Frame Index", typeof(short)),
               new TagBlock("Frame Events", 4, -1, new MetaNode[] { 
                   new HaloPlugins.Objects.Data.Enum("Type", new string[] {  }, 16),
                   new Value("Frame", typeof(short)),
               }),
               new TagBlock("Sounds", 8, -1, new MetaNode[] { 
                   new Value("Sound Index", typeof(short)),
                   new Value("Frame", typeof(short)),
                   new StringId("Marker Name"),
               }),
               new TagBlock("Effects", 4, -1, new MetaNode[] { 
                   new Value("Effect Index", typeof(short)),
                   new Value("Frame", typeof(short)),
               }),
               new TagBlock("Object-Space Parent Nodes", 28, -1, new MetaNode[] { 
                   new Value("Node Index", typeof(short)),
                   new Flags("Component Flags", new string[] { "Rotation", "Translation", "Scale" }, 16),
                   new Value("Rotation X", typeof(short)),
                   new Value("Rotation Y", typeof(short)),
                   new Value("Rotation Z", typeof(short)),
                   new Value("Rotation W", typeof(short)),
                   new Value("Default Translation X", typeof(float)),
                   new Value("Default Translation Y", typeof(float)),
                   new Value("Default Translation Z", typeof(float)),
                   new Value("Default Scale", typeof(float)),
               }),
           }),
           new TagBlock("Modes", 20, -1, new MetaNode[] { 
               new StringId("Label"),
               new TagBlock("Weapon Class", 20, -1, new MetaNode[] { 
                   new StringId("Label"),
                   new TagBlock("Weapon Type", 52, -1, new MetaNode[] { 
                       new StringId("Label"),
                       new TagBlock("Actions", 8, -1, new MetaNode[] { 
                           new StringId("Label"),
                           new Value("Graph Index", typeof(short)),
                           new Value("Animation", typeof(short)),
                       }),
                       new TagBlock("Overlays", 8, -1, new MetaNode[] { 
                           new StringId("Label"),
                           new Value("Graph Index", typeof(short)),
                           new Value("Animation", typeof(short)),
                       }),
                       new TagBlock("Death and Damage", 12, -1, new MetaNode[] { 
                           new StringId("Label"),
                           new TagBlock("Directions", 8, -1, new MetaNode[] { 
                               new TagBlock("Regions", 4, -1, new MetaNode[] { 
                                   new Value("Graph Index", typeof(short)),
                                   new Value("Animation Index", typeof(short)),
                               }),
                           }),
                       }),
                       new TagBlock("Transitions", 20, -1, new MetaNode[] { 
                           new StringId("Full Name"),
                           new StringId("State Name"),
                           new Value("Index A", typeof(short)),
                           new Value("Index B", typeof(short)),
                           new TagBlock("Destinations", 20, -1, new MetaNode[] { 
                               new StringId("Full Name"),
                               new StringId("mode"),
                               new StringId("State Name"),
                               new Value("Index A", typeof(short)),
                               new Value("Index B", typeof(short)),
                               new Value("Graph Index", typeof(short)),
                               new Value("Animation Index", typeof(short)),
                           }),
                       }),
                       new TagBlock("High PreCache", 4, -1, new MetaNode[] { 
                           new Value("Cache Block Index", typeof(int)),
                       }),
                       new TagBlock("Low PreCache", 4, -1, new MetaNode[] { 
                           new Value("Cahce Block Index", typeof(int)),
                       }),
                   }),
                   new TagBlock("Weapon ik", 8, -1, new MetaNode[] { 
                       new StringId("Marker"),
                       new StringId("Attach to Marker"),
                   }),
               }),
               new TagBlock("Mode ik", 8, -1, new MetaNode[] { 
                   new StringId("Marker"),
                   new StringId("Attach to Marker"),
               }),
           }),
           new TagBlock("Vehicle Suspension", 40, -1, new MetaNode[] { 
               new StringId("Label"),
               new Value("Graph Index", typeof(short)),
               new Value("ANimation Index", typeof(short)),
               new StringId("Marker Name"),
               new Value("Mass Point Offset (maybe int)", typeof(float)),
               new Value("Full Extension Ground Depth (maybe int)", typeof(float)),
               new Value("Full COmpresion Ground Depth (maybe int)", typeof(float)),
               new StringId("Region Name"),
               new Value("Destroyed Mass Point Offset(maybe int)", typeof(float)),
               new Value("Destroyed Full Extension Ground Depth (maybe int)", typeof(float)),
               new Value("Destroyed Full COmpression Ground Depth (maybe int)", typeof(float)),
           }),
           new TagBlock("Object Overlays", 20, -1, new MetaNode[] { 
               new StringId("Label"),
               new Value("Graph Index", typeof(short)),
               new Value("Animation Index", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Function COntrols", new string[] {  }, 16),
               new Padding(2),
               new StringId("Function"),
               new Padding(4),
           }),
           new TagBlock("Inheritence List", 32, -1, new MetaNode[] { 
               new TagReference("Inherited_Graph", "jmad"),
               new TagBlock("Node Map", 2, -1, new MetaNode[] { 
                   new Value("Local Node", typeof(short)),
               }),
               new TagBlock("Node Map Flags", 4, -1, new MetaNode[] { 
                   new Value("Local Node Flags", typeof(int)),
               }),
               new Value("Root Z Offset", typeof(float)),
               new Value("Inheritence Flags", typeof(float)),
           }),
           new TagBlock("Weapon List", 8, -1, new MetaNode[] { 
               new StringId("Name"),
               new StringId("Class"),
           }),
           new Value("Unknown", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(24),
           new Value("Unknown", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Padding(36),
           new TagBlock("Raw Blocks", 20, -1, new MetaNode[] { 
               new TagIndex("Class ID", "jmad"),
               new Value("Raw Size", typeof(int)),
               new Value("Raw Offset", typeof(int)),
               new Value("Unknown", typeof(int)),
               new Value("Unknown", typeof(int)),
           }),
           new TagBlock("Additional Node Data", 24, 255, new MetaNode[] { 
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
               new Value("Default Scale", typeof(float)),
           }),
           });
       }
    }
}
