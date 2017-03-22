using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class snd : TagDefinition
    {
       public snd() : base("snd!", "sound", 20)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Fit_To_ADPMC_BlockSize", "Split_Long_Sound_Into_Permutations", "Always_Spatialize", "Never_Obstruct", "Internal_Dont_Touch", "Used_Huge_Sound_Transmission", "Link_Count_to_Owner_Unit", "Pitch_Range_is_Language", "Don't_use_Sound_Class_Speaker_Flag", "Don't_use_Lipsync_Data" }, 16),
           new Value("Sound Class", typeof(short)),
           new HaloPlugins.Objects.Data.Enum("Compression", new string[] { "None", "XBox_ADPCM", "WMA" }, 8),
           //new HaloObjects.Enum("Format", new string[] { "Mono_22050kbps", "Stero_44100kbps", "WMA_Specific" }, 8),
           new HaloPlugins.Objects.Data.Enum("Compression", new string[] { "None_(big_Endian)" }, 8),
           new Value("Playback Index", typeof(short)),
           new Value("First Pitch Range Index", typeof(short)),
           new Value("Pitch Range Count", typeof(byte)),
           new Value("Scale Index", typeof(byte)),
           new Value("Promotion Index", typeof(short)),
           new Value("Custom Playback Index", typeof(short)),
           new Value("Extra Info Index", typeof(int)),
           });
       }
    }
}
