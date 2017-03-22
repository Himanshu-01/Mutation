using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class sky : TagDefinition
    {
       public sky() : base("sky ", "sky", 172)
       {
           Fields.AddRange(new MetaNode[] {
           new TagReference("Render_Model", "mode"),
           new TagReference("Animation_Graph", "jmad"),
           new Flags("Flags", new string[] { "Fixed_In_World_Space", "Depreciated", "Sky_Casts_Light_From_Below", "Disable_Sky_In_Lightmaps", "Fog_Only_Affects_Containing_Cluster", "Use_Clear_Color" }, 32),
           new Value("Render Model Scale", typeof(float)),
           new Value("Movement Scale", typeof(float)),
           new TagBlock("Cube Map", 12, 1, new MetaNode[] { 
               new TagReference("Cube_Map", "bitm"),
               new Value("Power Scale", typeof(float)),
           }),
           new Value("Indoor Ambient Light Color R", typeof(float)),
           new Value("Indoor Ambient Light Color G", typeof(float)),
           new Value("Indoor Ambient Light Color B", typeof(float)),
           new Value("Indoor Ambient Light Color A", typeof(float)),
           new Value("Outdoor Ambient Light Color R", typeof(float)),
           new Value("Outdoor Ambient Light Color G", typeof(float)),
           new Value("Outdoor Ambient Light Color B", typeof(float)),
           new Value("Outdoor Ambient Light Color A", typeof(float)),
           new Value("Fog Spread Distance", typeof(float)),
           new TagBlock("Atmospheric Fog", 24, 1, new MetaNode[] { 
               new Value("Color R", typeof(float)),
               new Value("Color G", typeof(float)),
               new Value("Color B", typeof(float)),
               new Value("Maximum Density", typeof(float)),
               new Value("Start Distance", typeof(float)),
               new Value("Opaque Distnace", typeof(float)),
           }),
           new TagBlock("Secondary Fog", 24, 1, new MetaNode[] { 
               new Value("Color R", typeof(float)),
               new Value("Color G", typeof(float)),
               new Value("Color B", typeof(float)),
               new Value("Color A", typeof(float)),
               new Value("Maximum Density", typeof(float)),
               new Value("Opaque Distnace", typeof(float)),
           }),
           new TagBlock("Sky Fog", 16, 1, new MetaNode[] { 
               new Value("Color R", typeof(float)),
               new Value("Color G", typeof(float)),
               new Value("Color B", typeof(float)),
               new Value("Color A", typeof(float)),
           }),
           new TagBlock("Patchy Fog", 80, 1, new MetaNode[] { 
               new Value("Color R", typeof(float)),
               new Value("Color G", typeof(float)),
               new Value("Color B", typeof(float)),
               new Value("Color A", typeof(float)),
               new Padding(8),
               new Value("Density Lower", typeof(float)),
               new Value("Density Upper", typeof(float)),
               new Value("Distance Lower", typeof(float)),
               new Value("Distance Upper", typeof(float)),
               new Padding(32),
               new TagReference("Patch_Fog", "fpch"),
           }),
           new Value("Bloom Override Amount", typeof(float)),
           new Value("Bloom Override Threshold", typeof(float)),
           new Value("Bloom Override Brightness", typeof(float)),
           new Value("Bloom Override Gamma Power", typeof(float)),
           new TagBlock("Lights", 52, 8, new MetaNode[] { 
               new Value("Direction Vector i", typeof(float)),
               new Value("Direction Vector j", typeof(float)),
               new Value("Direction Vector k", typeof(float)),
               new Value("Direction Y", typeof(float)),
               new Value("Direction P", typeof(float)),
               new TagReference("Lens_Flare", "lens"),
               new TagBlock("Fog", 44, 1, new MetaNode[] { 
                   new Value("Color R", typeof(float)),
                   new Value("Color G", typeof(float)),
                   new Value("Color B", typeof(float)),
                   new Value("Color A", typeof(float)),
                   new Value("Maximum Density", typeof(float)),
                   new Value("Opaque Distnace", typeof(float)),
                   new Value("Cone Lower", typeof(float)),
                   new Value("Cone Upper", typeof(float)),
                   new Value("Atmospheroc Fog Influence", typeof(float)),
                   new Value("Secondary Fog Influence", typeof(float)),
                   new Value("Sky Fog Influence", typeof(float)),
               }),
               new TagBlock("Fog Opposite", 44, 1, new MetaNode[] { 
                   new Value("Color R", typeof(float)),
                   new Value("Color G", typeof(float)),
                   new Value("Color B", typeof(float)),
                   new Value("Color A", typeof(float)),
                   new Value("Maximum Density", typeof(float)),
                   new Value("Opaque Distnace", typeof(float)),
                   new Value("Cone Lower", typeof(float)),
                   new Value("Cone Upper", typeof(float)),
                   new Value("Atmospheroc Fog Influence", typeof(float)),
                   new Value("Secondary Fog Influence", typeof(float)),
                   new Value("Sky Fog Influence", typeof(float)),
               }),
               new TagBlock("Radiosity", 40, 1, new MetaNode[] { 
                   new Flags("Flags", new string[] { "Affects_Interiors", "Affects_Exteriors", "Direct_Illumunation_In_Lightmaps", "Indirect_Illumunation_In_Lightmaps" }, 32),
                   new Value("Color R", typeof(float)),
                   new Value("Color G", typeof(float)),
                   new Value("Color B", typeof(float)),
                   new Value("Power", typeof(float)),
                   new Value("Test Distance", typeof(float)),
                   new Padding(12),
                   new Value("Diameter", typeof(float)),
               }),
           }),
           new Value("Global Sky Rotation", typeof(float)),
           new Padding(28),
           new Value("Clear Color R", typeof(float)),
           new Value("Clear Color G", typeof(float)),
           new Value("Clear Color B", typeof(float)),
           });
       }
    }
}
