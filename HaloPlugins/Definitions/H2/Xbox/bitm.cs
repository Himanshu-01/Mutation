using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.H2.Xbox;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using HaloPlugins.Objects.Strings;
using HaloPlugins.Objects.Vector;

namespace HaloPlugins.Xbox
{
    public class bitm : TagDefinition
    {
       public bitm() : base("bitm", "bitmap", 76, new BitmapDefinition())
       {
           Fields.AddRange(new MetaNode[] {
           new HaloPlugins.Objects.Data.Enum("Type", new string[] { "2D_Textures", "3D_Textures", "CubeMap", "Sprite", "UI_Bitmap" }, 16),
           new HaloPlugins.Objects.Data.Enum("Format", new string[] { "DXT1_Encoded", "DXT2/3_Encoded", "DXT4/5_Encoded", "16-Bit_Colour", "32-Bit_Colour", "Monochrome" }, 16),
           new HaloPlugins.Objects.Data.Enum("Usage", new string[] { "Alpha_Blend", "Default", "Height_Map", "Light_Map", "Vector_Map", "Height_Map_BLUE_255", "embm", "Height_Map_A8L8", "Height_Map_G8B8", "Height_Map_G8B8_/w_Alpha" }, 16),
           new Flags("Format", new string[] { "Enable_Diffusion_Dithering", "Disable_Height_map_compression", "Uniform_Sprite_Sequnces", "Filthy_Sprite_Bugfix", "Use_Sharp_Bump_Filter", "unused", "Use_Clamped/Mirrored_Bump_Filter", "Invert_Detail_Fade", "Swap_x-y_Vector_Components", "Convert_From_Signed", "Convert_To_Signed", "Import_Mipmap_Chains", "Internationally_True_Color" }, 16),
           new Value("Detail Fade Factor", typeof(float)),
           new Value("Sharpen Amount", typeof(float)),
           new Value("Bump Height", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Sprite Budget Size", new string[] { "32x32", "64x64", "128x128", "256x256", "512x512" }, 16),
           new Value("Sprite Budget Count", typeof(short)),
           new Value("Colour Plate Width", typeof(short)),
           new Value("Colour Plate Height", typeof(short)),
           new Value("Compressed Colour Plate Data", typeof(int)),
           new Padding(12),
           new Value("Blur Filter Size", typeof(float)),
           new Value("Alpha Bias", typeof(float)),
           new Value("MipMap Count", typeof(short)),
           new HaloPlugins.Objects.Data.Enum("Sprite Usage", new string[] { "Blend/Add/Subtract/Max", "Multiply/Min", "Double_Multiply" }, 16),
           new Value("Sprite Spacing", typeof(short)),
           new HaloPlugins.Objects.Data.Enum("Face Format", new string[] { "Default", "Force_G8B8", "Force_DXT1", "Force_DXT3", "Force_DXT5", "Force_Alpha-Luminance8", "Force_A4R4G4B4" }, 16),
           new TagBlock("Squences", 60, 256, new MetaNode[] { 
               new ShortString("Name", StringType.Asci),
               new Value("First Bitmap Index", typeof(short)),
               new Value("Bitmap Count", typeof(short)),
               new Padding(16),
               new TagBlock("Sprites", 32, 64, new MetaNode[] { 
                   new Value("Bitmap Index", typeof(int)),
                   new Padding(4),
                   new Value("Left", typeof(float)),
                   new Value("Right", typeof(float)),
                   new Value("Top", typeof(float)),
                   new Value("Bottom", typeof(float)),
                   new RealPoint2d("Registration Point"),
               }),
           }),
           new TagBlock("Bitmaps", 116, 65536, new MetaNode[] { 
               new Tag("Signature"),
               new Value("Width", typeof(short)),
               new Value("Height", typeof(short)),
               new Value("Depth", typeof(short)),
               new Value("Type", typeof(short)),
               new Value("Format", typeof(short)),
               new Flags("Flags", new string[] { "^2_Dimensions", "Compressed", "Palettized", "Swizzled", "Linear", "v16u16", "HUD_Bitmap?", "Always_on?", "Interlaced?" }, 16),
               new Value("Reg X", typeof(short)),
               new Value("Reg Y", typeof(short)),
               new Value("MipMap Count", typeof(short)),
               new Value("Pixel Offset", typeof(short)),
               new Value("Zero", typeof(int)),
               new Value("LOD1 Offset", typeof(int)),
               new Value("LOD2 Offset", typeof(int)),
               new Value("LOD3 Offset", typeof(int)),
               new Value("LOD4 Offset", typeof(int)),
               new Value("LOD5 Offset", typeof(int)),
               new Value("LOD6 Offset", typeof(int)),
               new Value("LOD1 Size", typeof(int)),
               new Value("LOD2 Size", typeof(int)),
               new Value("LOD3 Size", typeof(int)),
               new Value("LOD4 Size", typeof(int)),
               new Value("LOD5 Size", typeof(int)),
               new Value("LOD6 Size", typeof(int)),
               new TagIndex("Owner", "bitm"),
               new Padding(8),
               new Flags("Flags(CBZ)", new string[] {  }, 32),
               new Padding(4),
               new Value("Unknown(CBZ)", typeof(int)),
               new Value("Unknown(CBZ)", typeof(int)),
               new Value("Unknown(CBZ)", typeof(int)),
               new Value("Unknown(CBZ)", typeof(int)),
               new Padding(4),
           }),
           });
       }
    }
}
