using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class lsnd : TagDefinition
    {
       public lsnd() : base("lsnd", "looping_sound", 44)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Deafening_To_AIs", "Not_A_Loop", "Stops_Music", "Always_Spartialize", "Synchronize_Playback", "Synchronize_Tracks", "Fake_Spatialization_With_Distance", "Combine_All_3D_Playback" }, 32),
           new Value("Marty's Music Time", typeof(float)),
           new Padding(4),
           new Value("Unknown", typeof(float)),
           new Value("Unknown", typeof(float)),
           new TagReference("Unused", "****"),
           new TagBlock("Tracks", 88, 3, new MetaNode[] { 
               new StringId("Name"),
               new Flags("Flags", new string[] { "Fade_In_At_Start", "Fade_Out_At_Stop", "Crossfade_Alternate_Loop", "Master_Surround_Sound_Track", "Fade_Out_At_Alternate_Stop" }, 32),
               new Value("Gain dB", typeof(float)),
               new Value("Fade In Duration Sec", typeof(float)),
               new Value("Fade Out Duration Sec", typeof(float)),
               new TagReference("In", "snd!"),
               new TagReference("Loop", "snd!"),
               new TagReference("Out", "snd!"),
               new TagReference("Alternate_Loop", "snd!"),
               new TagReference("Alternate_Out", "snd!"),
               new HaloPlugins.Objects.Data.Enum("Output Effect", new string[] { "None", "Output_Front_Speakers", "Output_Rear_Speakers", "Output_Center_Speakers" }, 32),
               new TagReference("Alternate_Trans_In", "snd!"),
               new TagReference("Alternate_Trans_Out", "snd!"),
               new Value("Alternate Crossfade", typeof(float)),
               new Value("Alternate Fade Out Duration", typeof(float)),
           }),
           new TagBlock("Detail Sounds", 52, 12, new MetaNode[] { 
               new StringId("Name"),
               new TagReference("Sound", "snd!"),
               new Value("Random Period Bounds Lower", typeof(float)),
               new Value("Random Period Bounds Upper", typeof(float)),
               new Flags("Flags", new string[] { "Don't_Play_With_Alternate", "Don't_Play_Without_Alternate", "Start_Immeditely_With_Loop" }, 32),
               new Padding(4),
               new Value("Yaw Bounds Lower", typeof(float)),
               new Value("Yaw Bounds Upper", typeof(float)),
               new Value("Pitch Bounds Lower", typeof(float)),
               new Value("Pitch Bounds Upper", typeof(float)),
               new Value("Distance Bounds Lower", typeof(float)),
               new Value("Distance Bounds Upper", typeof(float)),
           }),
           });
       }
    }
}
