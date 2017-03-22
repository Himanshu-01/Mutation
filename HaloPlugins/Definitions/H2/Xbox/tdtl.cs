using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class tdtl : TagDefinition
    {
       public tdtl() : base("tdtl", "beam_trail", 112)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(2),
           new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Standard", "Weapon_To_Projectile", "Projectile_From_Weapon" }, 16),
           new StringId("Attachment Marker Name"),
           new Padding(56),
           new Value("Falloff Distance From Camera", typeof(float)),
           new Value("Cutoff Distance From Camera", typeof(float)),
           new Padding(32),
           new TagBlock("Arcs", 236, 3, new MetaNode[] { 
               new Flags("Flags", new string[] { "Basis_Marker-Relative", "Spread_By_External_Input", "Collide_With_Stuff", "No_Perspective_Midpoints" }, 16),
               new HaloPlugins.Objects.Data.Enum("Sprite Count", new string[] { "4_Sprites", "8_Sprites", "16_Sprites", "32_Sprites", "64_Sprites", "128_Sprites", "256_Sprites" }, 16),
               new Value("Natural Length", typeof(float)),
               new Value("Instances", typeof(short)),
               new Padding(2),
               new Value("Instance Spread Angle", typeof(float)),
               new Value("Instance Rotation Period", typeof(float)),
               new Padding(8),
               new TagReference("Material_Effects", "foot"),
               new TagReference("Bitmap", "bitm"),
               new Padding(8),
               new TagBlock("Horizontal Range", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Vertical Range", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Value("Vertical Negative Scale", typeof(float)),
               new TagBlock("Roughness", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new Padding(64),
               new Value("Octave 1 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 2 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 3 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 4 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 5 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 6 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 7 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 8 Frequency Cycles Per Sec", typeof(float)),
               new Value("Octave 9 Frequency Cycles Per Sec", typeof(float)),
               new Padding(28),
               new Flags("Octave Flags", new string[] { "Octave_1", "Octave_2", "Octave_3", "Octave_4", "Octave_5", "Octave_6", "Octave_7", "Octave_8", "Octave_9" }, 32),
               new TagBlock("Cores", 56, 4, new MetaNode[] { 
                   new Value("Bitmap Index", typeof(short)),
                   new Padding(14),
                   new TagBlock("Thickness", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new TagBlock("Color", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new TagBlock("Brightness/Time", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new TagBlock("Brightness/Facing", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new TagBlock("Along-Axis Scale", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
               }),
               new TagBlock("Range-Collision Scale", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
               new TagBlock("Brightness-Collision Scale", 1, -1, new MetaNode[] { 
                   new Value("Data", typeof(byte)),
               }),
           }),
           });
       }
    }
}
