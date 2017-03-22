using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class snde : TagDefinition
    {
       public snde() : base("snde", "sound_environment", 72)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(8),
           new Value("Priority", typeof(float)),
           new Value("Room intensity(dB)", typeof(float)),
           new Value("Room intensity hf(dB)", typeof(float)),
           new Value("Room Rolloff(0 to 10)", typeof(float)),
           new Value("Decay Time(.1 to 20) Sec", typeof(float)),
           new Value("Decay Hf Ratio(.1 to 2)", typeof(float)),
           new Value("Reflections Delay (0 to 0.3) Sec", typeof(float)),
           new Value("Reverb Intensity(dB -100,20)", typeof(float)),
           new Value("Reverb Delay(0 to .1) Sec", typeof(float)),
           new Value("Diffusion", typeof(float)),
           new Value("Density", typeof(float)),
           new Value("Hf Refrence(20 to 20,000)Hz", typeof(float)),
           new Padding(16),
           });
       }
    }
}
