using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class fpch : TagDefinition
    {
       public fpch() : base("fpch", "fog_patch", 80)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Separate_Layer_Depths", "Sort_Behind_Transparents" }, 32),
           new Value("Movement Rotation Multiplier", typeof(float)),
           new Value("Movement Strafing Multiplier", typeof(float)),
           new Value("Movement Zoom Multiplier", typeof(float)),
           new Value("Noise Map Scale", typeof(float)),
           new TagReference("Noise_Map", "bitm"),
           new Value("Noise Vertical Scale Forward", typeof(float)),
           new Value("Noise Vertical Scale Up", typeof(float)),
           new Value("Noise Opacity Scale Up", typeof(float)),
           new Value("Animation Period (sec)", typeof(float)),
           new Value("Wind Velocity", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Wind Period", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Wind Acceleration Weight", typeof(float)),
           new Value("Wind Perpendicular Weight", typeof(float)),
           new Value("Wind Constant Velocity X", typeof(float)),
           new Value("Wind Constant Velocity Y", typeof(float)),
           new Value("Wind Constant Velocity Z", typeof(float)),
           });
       }
    }
}
