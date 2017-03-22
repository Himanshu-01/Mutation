using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class snmx : TagDefinition
    {
       public snmx() : base("snmx", "sound_mixture", 88)
       {
           Fields.AddRange(new MetaNode[] {
           new Value("First Person Left Side Left Stereo Gain", typeof(float)),
           new Value("First Person Left Side Right Stereo Gain", typeof(float)),
           new Value("First Person Middle Side Left Stereo Gain", typeof(float)),
           new Value("First Person Middle Side Right Stereo Gain", typeof(float)),
           new Value("First Person Right Side Left Stereo Gain", typeof(float)),
           new Value("First Person Right Side Right Stereo Gain", typeof(float)),
           new Value("First Person Stereo Front Speaker Gain dB", typeof(float)),
           new Value("First Person Stereo Rear Speaker Gain dB", typeof(float)),
           new Value("Ambient Stereo Front Speaker Gain dB", typeof(float)),
           new Value("Ambient Stereo Rear Speaker Gain dB", typeof(float)),
           new Value("Global Mono Unspatialized Gain dB", typeof(float)),
           new Value("Global Stereo To 3D Gain dB", typeof(float)),
           new Value("Global Rear Surround To Front Stereo Gain dB", typeof(float)),
           new Value("Surround Center Speaker Gain dB", typeof(float)),
           new Value("Surround Center Speaker Gain dB", typeof(float)),
           new Value("Stereo Front Speaker Gain dB", typeof(float)),
           new Value("Stereo Center Speaker Gain dB", typeof(float)),
           new Value("Stereo Unspatialized Gain dB", typeof(float)),
           new Value("Solo Player Fade Out Delay", typeof(float)),
           new Value("Solo Player Fade Out Time Sec", typeof(float)),
           new Value("Solo Player Fade In Time Sec", typeof(float)),
           new Value("Game Music Fade Out Time Sec", typeof(float)),
           });
       }
    }
}
