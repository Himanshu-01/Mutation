using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using HaloPlugins.Objects.Color;

namespace HaloPlugins.Xbox
{
    public class obje
    {
        public List<MetaNode> Fields = new List<MetaNode>();

        public obje()
        {
            Fields.AddRange(new MetaNode[] {
            new InfoBlock("<--- Obje --->"),
            new Flags("Flags", new string[] { "Does_Not_Cast_Shadow", "Search_Cardinal_Direction_Lightmaps", "unused", "Not_A_Pathfinding_Obstacle", "Extension_Of_Parent", "Does_Not_Cause_Collision_Damage", "Early_Mover", "Early_Mover_Localized_Physics", "Use_Static_Massive_Lightmap_Sample", "Object_Scales_Attachments", "Inherits_Player's_Appearance", "Dead_Bipeds_Can't_Localize", "Attach_To_Clusters_By_Dynamic_Sphere", "Effects_Created_By_This_Object_Do_Not" }, 32),
            new Value("Bounding Radius", typeof(float)),
            new Value("Bounding Offset X", typeof(float)),
            new Value("Bounding Offset Y", typeof(float)),
            new Value("Bounding Offset Z", typeof(float)),
            new Value("Acceleration Scale", typeof(float)),
            new HaloPlugins.Objects.Data.Enum("Lightmap Shadow Mode", new string[] { "Default", "Never", "Always" }, 32),
            new HaloPlugins.Objects.Data.Enum("Sweetener Size", new string[] { "Small", "Medium", "Large" }, 32),
            new Value("Dynamic Light Sphere Radius", typeof(float)),
            new Value("Dynamic Light Sphere Offset X", typeof(float)),
            new Value("Dynamic Light Sphere Offset Y", typeof(float)),
            new Value("Dynamic Light Sphere Offset Z", typeof(float)),
            new StringId("Default Model Variant"),
            new TagReference("Model", "hlmt"),
            new TagReference("Crate_Object", "bloc"),
            new TagReference("Modifier_Shader", "shad"),
            new TagReference("Creation_Effect", "effe"),
            new TagReference("Material_Effects", "foot"),
            new TagBlock("Ai Properties", 16, 1, new MetaNode[] { 
               new Flags("Flags", new string[] { "Destroyable_Cover", "Pathfinding_Ignore_When_Dead", "Dynamic_Cover" }, 32),
               new StringId("Type Name"),
               new HaloPlugins.Objects.Data.Enum("Size", new string[] { "Default", "Tiny", "Small", "Medium", "Large", "Huge", "Immobile" }, 32),
               new HaloPlugins.Objects.Data.Enum("Leap Jump Speed", new string[] { "None", "Down", "Step", "Crouch", "Stand", "Storey", "Tower", "Infinite" }, 32),
            }),
            new TagBlock("Functions", 32, 256, new MetaNode[] { 
               new Flags("Flags", new string[] { "Invert", "Mapping_Does_Not_Controls_Active", "Always_Active", "Random_Time_Offset" }, 32),
               new StringId("Import Name"),
               new StringId("Export Name"),
               new StringId("Turn Off With"),
               new Value("Min Value", typeof(float)),
               new TagBlock("Function Type (Graph)", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new StringId("Scale By"),
            }),
            new Value("Apply Collision Damage Scale", typeof(float)),
            new Value("Min Game Acc (Default)", typeof(float)),
            new Value("Max Game Acc (Default)", typeof(float)),
            new Value("Min Gam Scale (Default)", typeof(float)),
            new Value("Max Gam Scale (Default)", typeof(float)),
            new Value("Min abs Acc (Default)", typeof(float)),
            new Value("Max abs Acc (Default)", typeof(float)),
            new Value("Min abs Scale (Default)", typeof(float)),
            new Value("Max abs Scale (Default)", typeof(float)),
            new Value("Hud Text Message Index", typeof(short)),
            new Padding(2),
            new TagBlock("Attachments", 24, 16, new MetaNode[] { 
               new TagReference("Type", "****"),
               new StringId("Marker"),
               new StringId("Change Color"),
               new StringId("Primary Scale"),
               new StringId("Secondary Scale"),
            }),
            new TagBlock("Widgets", 8, 4, new MetaNode[] { 
               new TagReference("Type", "****"),
            }),
            new TagBlock("Old Functions", 8, 4, new MetaNode[] { 
               new Padding(8),
            }),
            new TagBlock("Change Colors", 16, 4, new MetaNode[] { 
               new TagBlock("Initial Permutations", 32, 32, new MetaNode[] { 
                   new Value("Weight", typeof(float)),
                   new RealColorRgb("Color Lower Bounds"),
                   new RealColorRgb("Color Upper Bounds"),
                   new StringId("Variant Name"),
               }),
               new TagBlock("Functions", 40, 4, new MetaNode[] { 
                   new Value("", typeof(int)),
                   new Flags("Scale Flags", new string[] { "Blend_In_HSV", "...More_Colors" }, 32),
                   new Value("Color Lower Bound R", typeof(float)),
                   new Value("Color Lower Bound G", typeof(float)),
                   new Value("Color Lower Bound B", typeof(float)),
                   new Value("Color Upper Bound R", typeof(float)),
                   new Value("Color Upper Bound G", typeof(float)),
                   new Value("Color Upper Bound B", typeof(float)),
                   new StringId("Darken By"),
                   new StringId("Scale By"),
               }),
            }),
            new TagBlock("Predicted Resource", 8, 2048, new MetaNode[] { 
               new Value("Type", typeof(short)),
               new Value("Resource Index", typeof(short)),
               new TagIndex("Tag_Index", "****"),
            }), 
            });
        }
    }
}
