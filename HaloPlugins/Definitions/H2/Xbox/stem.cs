using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class stem : TagDefinition
    {
       public stem() : base("stem", "shader_template", 96)
       {
           Fields.AddRange(new MetaNode[] {
           //new TagBlock("Documentation", 1, -1, new MetaNode[] { 
           //    new Value("Char Data", typeof(byte)),
           //}),
           new Padding(8),
           new StringId("Default Material Name"),
           new Flags("Flags", new string[] { "Force_Active_Camo", "Water", "Foliage", "Hide_Standard_Parameters" }, 32),
           new Padding(16),
           new TagReference("Light_Response", "slit"),
           new Padding(24),
           new TagReference("Aux_1_Shader", "shad"),
           new HaloPlugins.Objects.Data.Enum("Aux 1 Layer", new string[] { "Texaccum", "Environment_Map", "Self-Illumination", "Overlay", "Transparent", "Lightmap_(Indirect)", "Diffuse", "Specular", "Shadow_Generate", "Shadow_Apply", "Boom", "Fog", "Sh_Prt", "Active_Camo", "Water_Edge_Blend", "Decal", "Active_Camo_Stencil_Modulate", "Hologram", "Light_Albedo" }, 32),
           new TagReference("Aux_2_Shader", "shad"),
           new HaloPlugins.Objects.Data.Enum("Aux 2 Layer", new string[] { "Texaccum", "Environment_Map", "Self-Illumination", "Overlay", "Transparent", "Lightmap_(Indirect)", "Diffuse", "Specular", "Shadow_Generate", "Shadow_Apply", "Boom", "Fog", "Sh_Prt", "Active_Camo", "Water_Edge_Blend", "Decal", "Active_Camo_Stencil_Modulate", "Hologram", "Light_Albedo" }, 32),
           new TagBlock("Postprocess Definition", 40, 1, new MetaNode[] { 
               new TagBlock("Levels Of Detail", 10, 1024, 4, new MetaNode[] { 
                   new Value("Layers", typeof(short)),
                   new Value("Available Layers", typeof(int)),
                   new Value("Projected Height Percentage", typeof(float)),
               }),
               new TagBlock("Layers", 2, 1024, 4, new MetaNode[] { 
                   new Value("Block Index Data", typeof(short)),
               }),
               new TagBlock("Passes", 10, 1024, 4, new MetaNode[] { 
                   new TagReference("Pass", "spas"),
                   new Value("Block Index Data", typeof(short)),
               }),
               new TagBlock("Implementations", 6, 1024, 4, new MetaNode[] { 
                   new Value("Bitmaps", typeof(short)),
                   new Value("Pixel Constant", typeof(short)),
                   new Value("Vertex Constant", typeof(short)),
               }),
               new TagBlock("Remappings", 4, 1024, 4, new MetaNode[] { 
                   new Value("Source Index", typeof(short)),
                   new Padding(2),
               }),
           }),
           });
       }
    }
}
