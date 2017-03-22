using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class skin : TagDefinition
    {
       public skin() : base("skin", "user_interface_list_skin_definition", 60)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Unused" }, 32),
           new TagReference("Arrow_bitmap", "bitm"),
           new Value("Up-Arrows Offset X", typeof(short)),
           new Value("Up-Arrows Offset Y", typeof(short)),
           new Value("Down-Arrows Offset X", typeof(short)),
           new Value("Down-Arrows Offset Y", typeof(short)),
           new TagBlock("Item Animations", 16, 7, new MetaNode[] { 
               new Flags("Flags", new string[] { "Unused" }, 32),
               new Value("Animation Period", typeof(float)),
               new TagBlock("Keyframes", 20, 64, new MetaNode[] { 
                   new Value("Alpha", typeof(float)),
                   new Value("Bitmap Flashing speed", typeof(float)),
                   new Value("X", typeof(float)),
                   new Value("Y", typeof(float)),
                   new Value("X", typeof(float)),
               }),
           }),
           new TagBlock("Text Blocks", 44, 64, new MetaNode[] { 
               new Flags("Flags", new string[] { "Left_Justify_Text", "Right_Justify_Text", "Pulsating_Text", "Callout_text", "Smal_(31_char)_Buffer" }, 32),
               new Value("Animation Index (0 = None, Max = 63)", typeof(int)),
               new Value("Intro Animation Delay Milliseconds", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Custom Font", new string[] { "Terminal", "Body_Text", "Title", "Super_Large_Font", "Large_Body_Text", "Split_Screen_HUD_Message", "Full_Screen_HUD_Message", "English_Body_Text", "HUD_Number_Text", "Subtitle_Font", "Main_Menu_Font", "Text_Char_Font" }, 16),
               new Value("Text Color Alpha", typeof(float)),
               new Value("Text Color AColor Red", typeof(float)),
               new Value("Text Color AColor Green", typeof(float)),
               new Value("Text Color AColor Blue", typeof(float)),
               new Value("Text Bounds Top", typeof(short)),
               new Value("Text Bounds Left", typeof(short)),
               new Value("Text Bounds Bottom", typeof(short)),
               new Value("Text Bounds Right", typeof(short)),
               new StringId("String name"),
               new Value("Render Depth Bias", typeof(int)),
           }),
           new TagBlock("Bitmap Blocks", 56, 64, new MetaNode[] { 
               new Flags("Flags", new string[] { "Ignore_For_the_List_Skin_Size_Calculator", "Swap_on_Relative_List_Position", "Render_As_Progress_Bar" }, 32),
               new Value("Animation Index (0 = None, Max = 63)", typeof(int)),
               new Value("Into Animation Delay Milliseconds", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Blend Bitmap Method", new string[] { "Standard", "Multiply", "Unused" }, 16),
               new Value("Bitmap Placement coord X", typeof(short)),
               new Value("Bitmap Placement coord Y", typeof(short)),
               new Value("Horizontal Texture Wraps/Second", typeof(float)),
               new Value("Vertical Texture Wraps/Second", typeof(float)),
               new TagReference("Bitmap", "bitm"),
               new Value("Render Depth Bias", typeof(short)),
               new Value("Unused", typeof(short)),
               new Value("Sprite Animation Speed FPS", typeof(float)),
               new Value("Progress Bottom Left X", typeof(short)),
               new Value("Progress Bottom Left Y", typeof(short)),
               new StringId("Bitmap color variation fuction"),
               new Value("Progress Scale i", typeof(float)),
               new Value("Progress Scale j", typeof(float)),
           }),
           new TagBlock("HUD Blocks", 36, 64, new MetaNode[] { 
               new Flags("Flags", new string[] { "Ignore_for_List_Skin_Size", "Needs_Valid_Rank" }, 16),
               new Value("Animation Index (0 = None, Max = 63)", typeof(int)),
               new Value("Intro Animation Delay Milliseconds", typeof(short)),
               new Value("Render Depth Bias", typeof(short)),
               new Value("Starting Bitmap Sequence Index", typeof(short)),
               new TagReference("Bitmap", "bitm"),
               new TagReference("Shader", "shad"),
               new Value("Bounds Top", typeof(short)),
               new Value("Bounds Left", typeof(short)),
               new Value("Bounds Bottom", typeof(short)),
               new Value("Bounds Right", typeof(short)),
           }),
           new TagBlock("Players Block", 24, 64, new MetaNode[] { 
               new Padding(4),
               new TagReference("Skin_tag", "skin"),
               new Value("Bottom Left X", typeof(short)),
               new Value("Bottom Left Y", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Table Order", new string[] { "Table_Major", "Column_Major" }, 8),
               new Value("Max Player Count", typeof(byte)),
               new Value("Raw Count", typeof(byte)),
               new Value("Column Count", typeof(byte)),
               new Value("Row Height", typeof(short)),
               new Value("Column Width", typeof(short)),
           }),
           });
       }
    }
}
