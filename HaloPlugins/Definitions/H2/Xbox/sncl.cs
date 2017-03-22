using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class sncl : TagDefinition
    {
       public sncl() : base("sncl", "sound_class", 8)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Sound Classes", 92, 54, new MetaNode[] { 
               new Value("Max Sounds Per Tag", typeof(short)),
               new Value("Max Sounds Per Object", typeof(short)),
               new Value("Preemption Time", typeof(int)),
               new Flags("Internal Flags", new string[] { "Valid", "Is_Speech", "Scripted", "Stops_With_Object", "unused", "Valid_Doppler_Factor", "Valid_Obstruction_Factor", "Multilingual" }, 16),
               new Flags("Flags", new string[] { "Plays_During_Pause", "Dry_Stereo_Mix", "No_Object_Obstruction", "Use_Center_Speaker_Unspatialized", "Send_(Mono)_To_LFE", "Deterministic", "Use_Huge_Transmission", "Always_Use_Speakers", "Don't_Strip_From_Mainmenu", "Ignore_Stereo_Headroom", "Loop_Fade_Out_Is_Linear", "Stop_When_Object_Dies", "Allow_Cache_File_Editing" }, 16),
               new Value("Priority", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Cache Miss Mode", new string[] {  }, 16),
               new Value("Reverb Gain dB", typeof(float)),
               new Value("Override Speaker Gain", typeof(float)),
               new Value("Distance Bounds Lower", typeof(float)),
               new Value("Distance Bounds Upper", typeof(float)),
               new Value("Gain Bounds Lower", typeof(float)),
               new Value("Gain Bounds Upper", typeof(float)),
               new Value("Cutscene Ducking dB", typeof(float)),
               new Value("Cutscene Ducking Fade In Time Sec", typeof(float)),
               new Value("Cutscene Ducking Sustain Time Sec", typeof(float)),
               new Value("Cutscene Ducking Fade Out Time Sec", typeof(float)),
               new Value("Scripted Dialog Ducking dB", typeof(float)),
               new Value("Scripted Dialog Ducking Fade In Time Sec", typeof(float)),
               new Value("Scripted Dialog Ducking Sustain Time Sec", typeof(float)),
               new Value("Scripted Dialog Ducking Fade Out Time Sec", typeof(float)),
               new Value("Doppler Factor", typeof(float)),
               new HaloPlugins.Objects.Data.Enum("Stereo Playback Type", new string[] { "First_Person", "Ambient" }, 32),
               new Value("Transmission Multiplier", typeof(float)),
               new Value("Obstruction Max Bend", typeof(float)),
               new Value("Occlusion Max Bend", typeof(float)),
           }),
           });
       }
    }
}
