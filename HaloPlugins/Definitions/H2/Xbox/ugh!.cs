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
    public class ugh : TagDefinition
    {
       public ugh() : base("ugh!", "sound_diagnostics", 88, new SoundDefinition())
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Playbacks", 56, 32767, new MetaNode[] { 
               new Value("Minimum Distance", typeof(float)),
               new Value("Maximum Distance", typeof(float)),
               new Value("Skip Fraction", typeof(float)),
               new Value("Maximum Blend Per Second", typeof(float)),
               new Value("Gain Base", typeof(float)),
               new Value("Gain Veriance", typeof(float)),
               new Value("Random Pitch Bounds", typeof(short)),
               new Value("To", typeof(short)),
               new Value("Inner Cone Angle", typeof(float)),
               new Value("Outer Cone Angle", typeof(float)),
               new Value("Outer Cone Gain", typeof(float)),
               new Flags("Flags", new string[] { "Override_Azimuth", "Override_3D_Gain", "Override_Speaker_Gain" }, 32),
               new Value("Positional Gain", typeof(float)),
               new Value("First Person Gain", typeof(float)),
               new Padding(4),
           }),
           new TagBlock("Scales", 20, 32767, new MetaNode[] { 
               new Value("Gain Modifier", typeof(float)),
               new Value("To", typeof(float)),
               new Value("Pitch Modifier", typeof(short)),
               new Value("To", typeof(short)),
               new Value("Skip Fraction Modifier", typeof(float)),
               new Value("To", typeof(float)),
           }),
           new TagBlock("Import Names", 4, 32767, new MetaNode[] { 
               new StringId("Name"),
           }),
           new TagBlock("Pitch Range Parameters", 10, 32767, new MetaNode[] { 
               new Value("Natural Pitch", typeof(short)),
               new Value("Blend Bounds", typeof(short)),
               new Value("To", typeof(short)),
               new Value("Max Gain Pitch Bounds", typeof(short)),
               new Value("To", typeof(short)),
           }),
           new TagBlock("Permutations", 12, 32767, new MetaNode[] { 
               new Value("Name Index", typeof(short)),
               new Value("Emcoded Skip Fraction", typeof(byte)),
               new Value("Encoded Gain", typeof(byte)),
               new Value("Permutation Info Index", typeof(byte)),
               new Value("Language Neatral Time", typeof(byte)),
               new Value("Sample Size", typeof(short)),
               new Value("Sound Choices Index", typeof(short)),
               new Value("Sound Choices Count", typeof(short)),
           }),
           new TagBlock("Sound Choices", 16, 32767, new MetaNode[] { 
               new Value("Sound Names Index", typeof(uint)),
               new Value("Padding?", typeof(int)),
               new Value("Total Raw Size", typeof(int)),
               new Value("Sound Raw Cache Index", typeof(short)),
               new Value("Sound Raw Chunk Count", typeof(short)),
           }),
           new TagBlock("Custom Playbacks", 52, 32767, new MetaNode[] { 
               new Flags("FLags", new string[] { "Use_3d_Radio_Hack" }, 32),
               new Padding(16),
               new TagBlock("Filter", 72, -1, new MetaNode[] { 
                   new HaloPlugins.Objects.Data.Enum("One", new string[] { "Parametric_EQ", "DLS2", "Both_(only_valid_for_mono)" }, 32),
                   new Value("Filter Width", typeof(float)),
                   new Value("Left Filter Frequence Scale Bounds", typeof(float)),
                   new Value("To", typeof(float)),
                   new Value("Left Filter Frequence Random Base and Variance", typeof(float)),
                   new Value("To", typeof(float)),
                   new Value("Left Filter Gain Scale Bounds", typeof(float)),
                   new Value("To", typeof(float)),
                   new Value("Left Filter Gain Random Base and Variance", typeof(float)),
                   new Value("To", typeof(float)),
                   new Value("Right Filter Frequence Scale Bounds", typeof(float)),
                   new Value("To", typeof(float)),
                   new Value("Right Filter Frequence Random Base and Variance", typeof(float)),
                   new Value("To", typeof(float)),
                   new Value("Right Filter Gain Scale Bounds", typeof(float)),
                   new Value("To", typeof(float)),
                   new Value("Right Filter Gain Random Base and Variance", typeof(float)),
                   new Value("To", typeof(float)),
               }),
               new Padding(24),
           }),
           new TagBlock("Runtime Permutation Flags", 1, 32767, new MetaNode[] { 
               new Padding(1),
           }),
           new TagBlock("Sound Raw Cache", 12, 32767, new MetaNode[] { 
               new Value("Offset", typeof(int)),
               new Value("Size", typeof(int)),
               new Value("Effect", typeof(int)),
           }),
           new TagBlock("Promotions", 28, 32767, new MetaNode[] { 
               new TagBlock("Promotion Rules", 16, -1, new MetaNode[] { 
                   new Value("Pitch Range Index", typeof(short)),
                   new Value("Maximum Playing Count", typeof(short)),
                   new Value("Suppression Time", typeof(float)),
                   new Value("", typeof(byte)),
                   new Value("", typeof(byte)),
                   new Value("", typeof(short)),
                   new Value("", typeof(byte)),
                   new Value("", typeof(byte)),
                   new Value("", typeof(short)),
               }),
               new TagBlock("Unknown81", 4, -1, new MetaNode[] { 
                   new Value("", typeof(int)),
               }),
               new Padding(12),
           }),
           new TagBlock("Extra Infos", 44, 32767, new MetaNode[] { 
               new TagBlock("Encoded Permutation Section", 0, 1, new MetaNode[] { 
               }),
               new Value("Offset", typeof(int)), // uint
               new Value("Size", typeof(int)), // uint
               new Value("Header Size", typeof(uint)),
               new Value("Data Size", typeof(uint)),
               new TagBlock("Resources", 16, -1, new MetaNode[] { 
                   new Value("", typeof(short)),
                   new Value("", typeof(short)),
                   new Value("", typeof(short)),
                   new Value("", typeof(short)),
                   new Value("Size", typeof(uint)),
                   new Value("Offset", typeof(uint)),
               }),
               new TagIndex("Owner_Tag_Section_Offset", "****"),
               new Value("Constant", typeof(int)),
               new Value("Constant", typeof(int)),
           }),
           });
       }
    }
}
