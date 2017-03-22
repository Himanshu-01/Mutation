using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class lens : TagDefinition
    {
       public lens() : base("lens", "lens_flare", 100)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(8),
           new Value("Falloff Angle (Degrees)", typeof(float)),
           new Value("Cuttoff Angle (Degrees)", typeof(float)),
           new Value("Occlusion Radius", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Occlusion Offset Direction", new string[] { "Toward_Viewer", "Marker_Forward", "None" }, 16),
           new HaloPlugins.Objects.Data.Enum("Occlusion Inner Radius Scale", new string[] { "None", "1/2", "1/4", "1/8", "1/16", "1/32", "1/64" }, 16),
           new Value("Near Fade Distance", typeof(float)),
           new Value("Far Fade Distance", typeof(float)),
           new TagReference("Bitmap", "bitm"),
           new Flags("Flags", new string[] { "Sun", "No_Occlusion_Test", "Only_Render_In_1st_Person", "Only_Render_In_3rd_Person", "Fade_In_More_Quickly", "Scale_By_Marker" }, 32),
           new HaloPlugins.Objects.Data.Enum("Rotation Function", new string[] { "None", "Rotation_A", "Rotation_B", "Rotation_Translation", "Translation" }, 32),
           new Value("Rotation Function Scale", typeof(float)),
           new Value("Corona Scale i", typeof(float)),
           new Value("Corona Scale j", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Falloff Function", new string[] { "Linear", "Late", "Very_Late", "Early", "Very_Early", "Cosine", "Zero", "One" }, 32),
           new TagBlock("Reflections", 48, 32, new MetaNode[] { 
               new Flags("Flags", new string[] { "Align_Rotation_With_Screen_Center", "Radius_Not_Scaled_By_Distance", "Radius_Scaled_By_Occlusion_Factor", "Occluded_By_Solid_Objects", "Ignore_Light_Color", "Not_Affected_By_Inner_Occlusion" }, 16),
               new Value("Bitmap Index", typeof(short)),
               new Padding(4),
               new Value("Position Along Flare Axis", typeof(float)),
               new Value("Rotation Offset", typeof(float)),
               new Value("Radius", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Brightness", typeof(float)),
               new Value("...To", typeof(float)),
               new Value("Modulation Factor", typeof(float)),
               new Value("Color R", typeof(float)),
               new Value("Color G", typeof(float)),
               new Value("Color B", typeof(float)),
           }),
           new Flags("Flags", new string[] { "Synchronized" }, 32),
           new TagBlock("Brightness", 188, 1, new MetaNode[] { 
               new TagBlock("Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Padding(180),
           }),
           new TagBlock("Color", 188, 1, new MetaNode[] { 
               new TagBlock("Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Padding(180),
           }),
           new TagBlock("Rotation", 188, 1, new MetaNode[] { 
               new TagBlock("Function", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Padding(180),
           }),
           });
       }
    }
}
