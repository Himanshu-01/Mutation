using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class cont : TagDefinition
    {
       public cont() : base("cont", "contrail", 240)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "First_point_unfaded", "Last_point_unfaded", "Points_start_pinned_to_media", "Points_start_pinned_to_ground", "Points_always_pinned_to_media", "Points_always_pinned_to_ground", "Edge_effects_fade_slowly", "Don't_Inherit_Object_Change_Color" }, 16),
           new Flags("Scale Flags", new string[] { "Point_Generation_Rate", "Point_Velocity", "Point_Velocity_Delta", "Point_Velocity_Cone_Angle", "Inherited_Velocity_Fraction", "Sequence_Animation_Rate", "Texture_Scale_U", "Texture_Scale_V", "Texture_Animation_U", "Texture_Animation_V" }, 16),
           new Value("Point Generation Rate", typeof(float)),
           new Value("Min Point Velocity", typeof(float)),
           new Value("Max Point Velocity", typeof(float)),
           new Value("Point Velocity Cone Angle", typeof(float)),
           new Value("Inherited Velocity Fraction", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Render Type", new string[] { "Verticle_Orientation", "Horizontal_Orientation", "Media_Mapped", "Ground_Mapped", "Viewer_Facing", "Double_Marker_Linked" }, 16),
           new Padding(2),
           new Value("Texture Repeats U", typeof(float)),
           new Value("Texture Repeats V", typeof(float)),
           new Value("Texture Animation U", typeof(float)),
           new Value("Texture Animation V", typeof(float)),
           new Value("Texture Animation Rate", typeof(float)),
           new TagReference("Bitmap", "bitm"),
           new Value("First Sequence Index", typeof(short)),
           new Value("Sequence Count", typeof(short)),
           new Padding(36),
           new Flags("Shader Flags", new string[] { "Sort_Bias", "Nonlinear_Tint", "Don't_Overdraw_FP_Weapon" }, 16),
           new HaloPlugins.Objects.Data.Enum("Framebuffer Blend Function", new string[] { "Alpha_Blend", "Multiply", "Double_Multiply", "Add", "Subtract", "Component_Min", "Component_Max", "Alpha-Multiply-Add" }, 16),
           new HaloPlugins.Objects.Data.Enum("Framebuffer Fade Mode", new string[] { "None", "Fade_When_Perpendicular", "Fade_When_Parallel", "Fade_After_Duration" }, 16),
           new Flags("Map Flags", new string[] { "Unfiltered" }, 32),
           new Padding(30),
           new TagReference("Bitmap", "bitm"),
           new Padding(30),
           new HaloPlugins.Objects.Data.Enum("Anchor", new string[] { "With_Primary", "With_Screen_Space", "ZSprite" }, 16),
           new Flags("Flags", new string[] { "Unfiltered" }, 16),
           new HaloPlugins.Objects.Data.Enum("U-Animation Function", new string[] {  }, 16),
           new Value("U-Animation Period", typeof(float)),
           new Value("U-Animation Phase", typeof(float)),
           new Value("U-Animation Scale", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("V-Animation Function", new string[] {  }, 16),
           new Value("V-Animation Period", typeof(float)),
           new Value("V-Animation Phase", typeof(float)),
           new Value("V-Animation Scale", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Rotation-Animation Function", new string[] {  }, 16),
           new Value("Rotation-Animation Period", typeof(float)),
           new Value("Rotation-Animation Phase", typeof(float)),
           new Value("Rotation-Animation Scale", typeof(float)),
           new Value("Rotation-Animation Center X", typeof(float)),
           new Value("Rotation-Animation Center Y", typeof(float)),
           new Value("ZSprite Radius Sclae", typeof(float)),
           new TagBlock("Point States", 64, 16, new MetaNode[] { 
               new Value("Duration Lower", typeof(float)),
               new Value("Duration Upper", typeof(float)),
               new Value("Transition Lower", typeof(float)),
               new Value("Transition Upper", typeof(float)),
               new TagReference("Physics", "pphy"),
               new Value("Width", typeof(float)),
               new Value("Color Lower Alpha", typeof(float)),
               new Value("Color Lower Red", typeof(float)),
               new Value("Color Lower Green", typeof(float)),
               new Value("Color Lower Blue", typeof(float)),
               new Value("Color Upper Alpha", typeof(float)),
               new Value("Color Upper Red", typeof(float)),
               new Value("Color Upper Green", typeof(float)),
               new Value("Color Upper Blue", typeof(float)),
               new Flags("Scale Flags", new string[] { "Duration", "Duration_Delta", "Transition_Duration", "Transition_Duration_Delta", "Width", "Color" }, 32),
           }),
           });
       }
    }
}
