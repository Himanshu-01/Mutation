using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class deca : TagDefinition
    {
       public deca() : base("deca", "decal", 172)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Geometry_Inherited_By_Next_Decal", "Interpolate_Colour", "More_Colour", "No_Random_Rotation", "unused", "Water_Effect", "Special-EF-Snap_To_Axis", "Special-EF-Incremental_Counter", "unused", "Preserve_Aspect", "unused" }, 16),
           new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Scratch", "Splatter", "Burn", "Painted_Sign" }, 16),
           new HaloPlugins.Objects.Data.Enum("Layer", new string[] { "Lit_Alpha_Blend_Prelight", "Lit_Alpha_Blend", "Double_Multiply", "Multiply", "Max", "Add", "Error" }, 16),
           new Value("Max Overlapping Count", typeof(short)),
           new TagReference("Next_Decal_In_Chain", "deca"),
           new Value("Radius", typeof(float)),
           new Value("^To(World Units)", typeof(float)),
           new Value("Radius Overlapping Rejection", typeof(float)),
           new Value("Color Lower Bound R", typeof(float)),
           new Value("Color Lower Bound G", typeof(float)),
           new Value("Color Lower Bound B", typeof(float)),
           new Value("Color Upper Bound R", typeof(float)),
           new Value("Color Upper Bound G", typeof(float)),
           new Value("Color Upper Bound B", typeof(float)),
           new Value("Lifetime", typeof(float)),
           new Value("^To(Seconds)", typeof(float)),
           new Value("Decay Time", typeof(float)),
           new Value("^To(Seconds)", typeof(float)),
           new Padding(68),
           new TagReference("Bitmap", "bitm"),
           new Padding(20),
           new Value("Maximun Sprite Extent(pixels)", typeof(float)),
           new Padding(4),
           });
       }
    }
}
