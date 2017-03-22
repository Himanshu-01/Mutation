using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class devi
    {
        public List<MetaNode> Fields = new List<MetaNode>();

        public devi()
        {
            // Obje
            Fields.AddRange(new obje().Fields.ToArray());

            // Devi
            Fields.AddRange(new MetaNode[] {
            new Flags("Flags", new string[] { "Position_Loops", "unused", "Position_Iterpolation" }, 32),
            new Value("Power Transition Time", typeof(float)),
            new Value("Power Acceleration Time", typeof(float)),
            new Value("Position Transition Time", typeof(float)),
            new Value("Position Acceleration Time", typeof(float)),
            new Value("Depowered Transition Time", typeof(float)),
            new Value("Depowered Acceleration Time", typeof(float)),
            new Flags("Lightmap Flags", new string[] { "Don't_Use_In_Lightmap", "Don't_Use_In_Lightprobe" }, 32),
            new TagReference("Open_(up)", "****"),
            new TagReference("Close_(down)", "****"),
            new TagReference("Opened", "****"),
            new TagReference("Closed", "****"),
            new TagReference("Depowered", "****"),
            new TagReference("Repowered", "****"),
            new Value("Delay Time", typeof(float)),
            new TagReference("Delay_Effect", "****"),
            new Value("Automatic Activation Radius", typeof(float)),
            });
        }
    }
}
