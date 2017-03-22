using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class shad : TagDefinition
    {
       public shad() : base("shad", "shader", 84)
       {
           Fields.AddRange(new MetaNode[] {
           new TagReference("Shader_Template", "stem"),
           new StringId("Material Type"),
           new TagBlock("Runtime Properties", 80, 1, new MetaNode[] { //80
               new TagReference("Diffuse Map", "bitm"),
               new TagReference("Lightmap Emissive Map", "bitm"),
               new Value("Lightmap Emissive Color: Red", typeof(float)),
               new Value("Lightmap Emissive Color: Blue", typeof(float)),
               new Value("Lightmap Emissive Color: Green", typeof(float)),
               new Value("Lightmap Emissive Color Power", typeof(float)),
               new Value("Lightmap Resoulution Scale", typeof(float)),
               new Value("Lightmap Half-Life", typeof(float)),
               new Value("Lightmap Diffuse Scale", typeof(float)),
               new TagReference("Alpha Test Map", "bitm"),
               new TagReference("Translucent Map", "bitm"),
               new Value("Lightmap Transparent Color: Red", typeof(float)),
               new Value("Lightmap Transparent Color: Blue", typeof(float)),
               new Value("Lightmap Transparent Color: Green", typeof(float)),
               new Value("Lightmap Transparent Alpha", typeof(float)),
               new Value("Lightmap Foliage Scale", typeof(float)),
           }),
           new Padding(2),
           new Flags("Flags", new string[] { "Water", "Sort First", "No Active Cammo" }, 16),
           new Padding(8),
           new TagBlock("Postprocess Definitions", 124, 1, new MetaNode[] { //124
               new TagIndex("Template", "stem"),
               new TagBlock("Bitmaps", 12, 1024, new MetaNode[] { 
                   new TagIndex("Bitmap Group", "bitm"),
                   new Value("Bitmap Index", typeof(int)),
                   new Value("Log Bitmap Dimension", typeof(float)),
               }),
               new TagBlock("Pixel Constants", 4, 1024, new MetaNode[] { 
                   new Value("Color A", typeof(byte)),
                   new Value("Color R", typeof(byte)),
                   new Value("Color G", typeof(byte)),
                   new Value("Color B", typeof(byte)),
               }),
               new TagBlock("Vertex Constants", 16, 1024, new MetaNode[] { 
                   new Value("X Scale", typeof(float)),
                   new Value("Y Scale", typeof(float)),
                   new Value("X Scale", typeof(float)),
                   new Value("W Scale", typeof(float)),
               }),
               new TagBlock("Levels Of Detail", 6, 1024, new MetaNode[] { //8
                   new Value("Available Layer Flags", typeof(int)),
                   new Value("Layers", typeof(short)),
               }),
               new TagBlock("Layers", 2, 1024, 4, new MetaNode[] { //4 fixed
                   new Value("Indice", typeof(short)),
               }),
               new TagBlock("Passes", 2, 1024, 4, new MetaNode[] { //4 fixed
                   new Value("Indice", typeof(short)),
               }),
               new TagBlock("Implementations", 10, 1024, 4, new MetaNode[] { //10 fixed
                   new Value("Bitmap Transform", typeof(short)),
                   new Value("Render State", typeof(short)),
                   new Value("Texture State", typeof(short)),
                   new Value("Pixel Constant", typeof(short)),
                   new Value("Vertex Constant", typeof(short)),
               }),
               new TagBlock("Overlays", 20, 1024, 4, new MetaNode[] { 
                   new StringId("Input Name"),
                   new StringId("Range Name"),
                   new Value("Time Period In Seconds", typeof(float)),
                   new TagBlock("Functions", 1, -1, new MetaNode[] { 
                       new Value("Function", typeof(byte)),
                   }),
               }),
               new TagBlock("Overlay References", 4, 1024, new MetaNode[] { 
                   new Value("Overlay Index", typeof(short)),
                   new Value("Transform Index", typeof(short)),
               }),
               new TagBlock("Animated Parameters", 2, 1024, new MetaNode[] { // fixed
                   new Value("Overlay Reference", typeof(short)),
               }),
               new TagBlock("Animated Parameter References", 4, 1024, 4, new MetaNode[] {
                   new Value("Parameter Index", typeof(int)), // NEEDS to be 4!
               }),
               new TagBlock("Bitmap Properties", 4, 5, new MetaNode[] { 
                   new Value("Bitmap Index", typeof(short)),
                   new Value("Animated Parameter Index", typeof(short)),
               }),
               new TagBlock("Color Properties: 1st chunk = colour of emitted light, second chunk = tint colour", 12, 2, new MetaNode[] { 
                   new Value("R", typeof(float)),
                   new Value("G", typeof(float)),
                   new Value("B", typeof(float)),
               }),
               new TagBlock("Value Properties", 4, 6, new MetaNode[] { 
                   new Value("Value", typeof(float)),
               }),
               new Padding(8),
           }),
           new Padding(4),
           new TagBlock("Predicted Resources", 8, 2048, 4, new MetaNode[] { 
               new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Bitmap", "Sound", "Render Model Geometry", "Clustor Geometry", "Clustor Instanced Geometry", "Lightmap Geometry Object Buckets", "Lightmap Geometry Instance Buckets", "Lightmap Clustor Bitmaps" }, 16),
               new Value("Resource Index", typeof(short)),
               new TagIndex("Bitmap Used Ident", "****"),
           }),
           new TagReference("Unused Light Response", "slit"), // was Tag Index!!!!!!
           new HaloPlugins.Objects.Data.Enum("Shader Load Bias", new string[] { "None", "4x Size", "2x Size", "1/2 Size", "1/4 Size", "Never" }, 16),
           new HaloPlugins.Objects.Data.Enum("Specular Type", new string[] { "None", "Default", "Dull" }, 16),
           new HaloPlugins.Objects.Data.Enum("Lightmap Type", new string[] { "Diffuse", "Default Specular", "Dull Specular" }, 16),
           new Padding(2),
           new Value("Lightmap Specular Brightness", typeof(float)),
           new Value("Lightmap Ambient Bias", typeof(float)),
           new Padding(8),
           });
       }
    }
}
