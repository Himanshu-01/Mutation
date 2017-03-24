using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using HaloPlugins.Objects.Strings;

namespace HaloPlugins.Xbox
{
    public class wgit : TagDefinition
    {
       public wgit() : base("wgit", "user_interface_screen_widget_definition", 104)
       {
           Fields.AddRange(new MetaNode[] {
           new Value("Back ground blur (blur=0 noblur=37)", typeof(short)),
           new Value("Unused", typeof(short)),
           new Value("Menu ID", typeof(short)),
           new Flags("Visibles X-A-B Options Buttons", new string[] { "A_Select_B_Cancel", "A_Select_B_Cancel", "X_Party_Privacy_A_Select_B_Cancel", "X_Options", "A_Select_B_Back" }, 16),
           new Value("Text Color A", typeof(float)),
           new Value("Text Color R", typeof(float)),
           new Value("Text Color G", typeof(float)),
           new Value("Text Color B", typeof(float)),
           new TagReference("String_List", "unic"),
           new TagBlock("Panes", 76, 16, new MetaNode[] { 
               new Value("Unused", typeof(short)),
               new Value("Animation Index", typeof(short)),
               new TagBlock("Buttons", 60, 64, new MetaNode[] { 
                   new Flags("Flags", new string[] { "Left_Justify_Text", "Right_Justify_Text", "Pulsating_Text", "Callout_Text", "Small_(31_char)_Buffer" }, 32),
                   new Value("Animation Index", typeof(short)),
                   new Value("Intro Animation Delay Milliseconds", typeof(short)),
                   new Value("", typeof(short)),
                   new Value("Font Type", typeof(short)),
                   new Value("Text Color Alpha", typeof(float)),
                   new Value("Text Color Red", typeof(float)),
                   new Value("Text Color Green", typeof(float)),
                   new Value("Text Color Blue", typeof(float)),
                   new Value("Bounds Top", typeof(short)),
                   new Value("Bounds Left", typeof(short)),
                   new Value("Bounds Bottom", typeof(short)),
                   new Value("Bounds Right", typeof(short)),
                   new TagReference("Bitmap", "bitm"),
                   new Value("Bitmap Offset X", typeof(byte)),
                   new Value("Bitmap Offset Y", typeof(byte)),
                   new Value("coord", typeof(byte)),
                   new Value("coord", typeof(byte)),
                   new StringId("String Id"),
                   new Value("Unknown", typeof(short)),
                   new Value("Unused", typeof(short)),
                   new Value("Unknown", typeof(short)),
                   new Value("Unused", typeof(short)),
               }),
               new TagBlock("Buttons 2", 24, 1, new MetaNode[] { 
                   new HaloPlugins.Objects.Data.Enum("Buttons loop", new string[] { "Yes", "No" }, 16),
                   new Value("Unused", typeof(short)),
                   new Value("Button Type (Palette [skin])", typeof(short)),
                   new Value("Number of visibles buttons", typeof(short)),
                   new Value("Buttons Placement Coord X", typeof(short)),
                   new Value("Buttons Placement Coord Y", typeof(short)),
                   new Value("Transition Type? (bitmask)", typeof(short)),
                   new Value("Variation?", typeof(short)),
                   new Padding(8),
               }),
               new Value("Unused", typeof(float)),
               new Value("Unused", typeof(float)),
               new TagBlock("Text strings", 44, 64, new MetaNode[] { 
                   new Flags("Text options", new string[] { "1", "2", "Pulsing", "Auto_Typing_text", "5" }, 32),
                   new Value("Transtion type (bitmask)", typeof(short)),
                   new Value("Variation?", typeof(short)),
                   new Value("Unused", typeof(short)),
                   new Value("Font Type", typeof(short)),
                   new Value("Alpha", typeof(float)),
                   new Value("Color Red", typeof(float)),
                   new Value("Color Green", typeof(float)),
                   new Value("Color Blue", typeof(float)),
                   new Value("Text Placement coord Y from Top", typeof(short)),
                   new Value("Text Placement coord X from Right", typeof(short)),
                   new Value("Text Placement coord Y from Bottom", typeof(short)),
                   new Value("Text Placement coord X from Left", typeof(short)),
                   new StringId("String name"),
                   new Value("Unknown", typeof(uint)),
               }),
               new TagBlock("UI bitmaps", 56, 64, new MetaNode[] { 
                   new Value("unknown", typeof(short)),
                   new Value("Unused", typeof(short)),
                   new Value("Transition type (bitmask)", typeof(short)),
                   new Value("Variation?", typeof(short)),
                   new HaloPlugins.Objects.Data.Enum("Alpha transparence", new string[] { "Normal", "Inverted" }, 16),
                   new Value("Xbox live bitmap setting", typeof(short)),
                   new Value("Bitmap Placement coord X", typeof(short)),
                   new Value("Bitmap Placement coord Y", typeof(short)),
                   new Value("Horizontal bitmap scrolling speed", typeof(float)),
                   new Value("Unused", typeof(float)),
                   new TagReference("Bitmap", "bitm"),
                   new Value("Bitmap Layer Level", typeof(short)),
                   new Value("Unused", typeof(short)),
                   new Value("Unused", typeof(float)),
                   new Value("Coord X for ?", typeof(short)),
                   new Value("Coord Y for ?", typeof(short)),
                   new StringId("Progress Bottom Left"),
                   new Value("Unused", typeof(float)),
                   new Value("Unused", typeof(float)),
               }),
               new TagBlock("ModelsScript Objects: Scenery/Bipeds...)", 76, 32, new MetaNode[] { 
                   new Value("Unused", typeof(short)),
                   new Value("Unused", typeof(short)),
                   new Value("Transition type (bitmask)", typeof(short)),
                   new Value("Variation?", typeof(short)),
                   new Value("Unknown", typeof(short)),
                   new Value("Unused", typeof(short)),
                   new TagBlock("Script Object", 32, 32, new MetaNode[] { 
                       new ShortString("Script Object name", StringType.Asci),
                   }),
                   new TagBlock("Unknown", 32, 8, new MetaNode[] { 
                       new ShortString("Unknown", StringType.Asci),
                   }),
                   new Value("Unknown", typeof(float)),
                   new Value("Unknown", typeof(float)),
                   new Value("Unknown", typeof(float)),
                   new Value("Unknown", typeof(float)),
                   new Value("Unknown", typeof(float)),
                   new Value("Unknown", typeof(float)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new Value("relatives: coord, size, palcement", typeof(byte)),
                   new StringId("Unused"),
                   new StringId("Unused"),
                   new StringId("Unused"),
               }),
               new Value("Unused", typeof(float)),
               new Value("Unused", typeof(float)),
               new Value("Unused", typeof(float)),
               new Value("Unused", typeof(float)),
               new TagBlock("Variable Buttons", 24, 64, new MetaNode[] { 
                   new Value("Unused", typeof(float)),
                   new TagReference("Skin_tag", "skin"),
                   new Value("Coord X", typeof(short)),
                   new Value("Coord Y", typeof(short)),
                   new Value("unknown", typeof(byte)),
                   new Value("Max Visible Buttons", typeof(byte)),
                   new Value("Unknown", typeof(byte)),
                   new Value("Unknown", typeof(byte)),
                   new Value("Coord X", typeof(short)),
                   new Value("Coord Y", typeof(short)),
               }),
           }),
           new Value("Font Type", typeof(short)),
           new Value("Unused", typeof(short)),
           new StringId("Header Title Text String"),
           new TagBlock("Descriptions/Help Text", 12, 16, new MetaNode[] { 
               new StringId("Text Strings"),
               new TagBlock("Notification messages", 4, 64, new MetaNode[] { 
                   new StringId("Text Strings"),
               }),
           }),
           new TagBlock("Prededicated Resources: Bitmap tags", 8, 16, new MetaNode[] { 
               new TagReference("Bitmap", "bitm"),
           }),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           new Value("SP Loading screen effects settings", typeof(float)),
           });
       }
    }
}