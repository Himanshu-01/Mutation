using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class ligh : TagDefinition
    {
       public ligh() : base("ligh", "light", 228)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "No_Illumination", "No_Specular", "Force_Cast_Environment_Shadow", "No_Shadow", "Force_Frustum_Visibility_On_Small", "Only_Render_In_1st_Person", "Only_Render_In_3rd_Person", "Don't_Fade_When_Invisible", "Multiplayer_Override", "Animated_Gel", "Only_In_Dynamic_Envmap", "Ignore_Parent_Object", "Don't_Show_Parent", "Ignore_All_Parents", "March_Milestone_Hack", "Force_Light_Inside_World", "Environment_Doesn't_Cast_Stencil_Shadow", "1st_Person_From_Camera", "Texture_Camera_Gel", "Light_Framerate_Killer", "Allowed_In_Splitscreen", "Only_On_Parent_Bipeds" }, 32),
           new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Sphere", "Orthogonal", "Projective", "Pyramid" }, 32),
           new Value("Size Modifier", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Shadow Quality Bias", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Shadow Tap Bias", new string[] { "3_Tap", "unused", "1_Tap" }, 32),
           new Value("Sphere Light Radius", typeof(float)),
           new Value("Sphere Light Specular Radius", typeof(float)),
           new Value("Frustum Light Near Width", typeof(float)),
           new Value("Frustum Light Height Stretch", typeof(float)),
           new Value("Frustum Light Field Of View", typeof(float)),
           new Value("Frustum Light Falloff Distance", typeof(float)),
           new Value("Frustum Light Cutoff Distance", typeof(float)),
           new Flags("Interpolation Flags", new string[] { "Blend_In_HSV", "...More_Colors" }, 32),
           new Value("Bloom Bounds", typeof(float)),
           new Value("..To", typeof(float)),
           new Value("Specular Lower Bound R", typeof(float)),
           new Value("Specular Lower Bound G", typeof(float)),
           new Value("Specular Lower Bound B", typeof(float)),
           new Value("Specular Upper Bound R", typeof(float)),
           new Value("Specular Upper Bound G", typeof(float)),
           new Value("Specular Upper Bound B", typeof(float)),
           new Value("Diffuse Lower Bound R", typeof(float)),
           new Value("Diffuse Lower Bound G", typeof(float)),
           new Value("Diffuse Lower Bound B", typeof(float)),
           new Value("Diffuse Upper Bound R", typeof(float)),
           new Value("Diffuse Upper Bound G", typeof(float)),
           new Value("Diffuse Upper Bound B", typeof(float)),
           new Value("Brightness Bounds", typeof(float)),
           new Value("...To", typeof(float)),
           new TagReference("Gel_Map", "bitm"),
           new HaloPlugins.Objects.Data.Enum("Specular Mask", new string[] { "Default", "None_(no_mask)", "Gel_Alpha", "Ge_Color" }, 32),
           new Padding(4),
           new HaloPlugins.Objects.Data.Enum("Falloff Function", new string[] { "Default", "Narrow", "Broad", "Very_Broad" }, 16),
           new HaloPlugins.Objects.Data.Enum("Diffuse Contrast", new string[] { "Default_(linear)", "High", "Low", "Very_Low" }, 16),
           new HaloPlugins.Objects.Data.Enum("Specular Contrast", new string[] { "Default_(none)", "High_(linear)", "Low", "Very_Low" }, 16),
           new HaloPlugins.Objects.Data.Enum("Falloff Geometry", new string[] { "Default", "Directional", "Spherical" }, 16),
           new TagReference("Lens_Flare", "lens"),
           new Value("Bounding Radius", typeof(float)),
           new TagReference("Light_Volume", "MGS2"),
           new HaloPlugins.Objects.Data.Enum("Default Lightmap Setting", new string[] { "Dynamic_Only", "Dynamic_With_Lightmaps", "Lightmaps_Only" }, 32),
           new Value("Lightmap Half Life", typeof(float)),
           new Value("Lightmap Light Scale", typeof(float)),
           new Value("Duration", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Falloff Function", new string[] { "Linear", "Late", "Very_Late", "Early", "Very_Early", "Cosine", "Zero", "One" }, 16),
           new Padding(2),
           new HaloPlugins.Objects.Data.Enum("Illumination Fade", new string[] { "Fade_Very_Far", "Fade_Far", "Fade_Medium", "Fade_Close", "Fade_Very_Close" }, 16),
           new HaloPlugins.Objects.Data.Enum("Shadow Fade", new string[] { "Fade_Very_Far", "Fade_Far", "Fade_Medium", "Fade_Close", "Fade_Very_Close" }, 16),
           new HaloPlugins.Objects.Data.Enum("Specular Fade", new string[] { "Fade_Very_Far", "Fade_Far", "Fade_Medium", "Fade_Close", "Fade_Very_Close" }, 16),
           new Padding(2),
           new Flags("Flags", new string[] { "Synchronized" }, 32),
           new TagBlock("Brightness Animation", 76, 1, new MetaNode[] { 
               new TagBlock("Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Padding(68),
           }),
           new TagBlock("Color Animation", 188, 1, new MetaNode[] { 
               new TagBlock("Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Padding(180),
           }),
           new TagBlock("Gel Animation", 16, 1, new MetaNode[] { 
               new TagBlock("Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
           }),
           new TagReference("Shader", "shad"),
           });
       }
    }
}
