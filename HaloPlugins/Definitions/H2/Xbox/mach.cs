using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class mach : TagDefinition
    {
       public mach() : base("mach", "machine", 308)
       {
           // Devi
           Fields.AddRange(new devi().Fields.ToArray());

           // Mach
           Fields.AddRange(new MetaNode[] {
           new HaloPlugins.Objects.Data.Enum("Type", new string[] { "Door", "Platform", "Gear" }, 16),
           new Flags("Flags", new string[] { "Pathfinding_Obstacle", "...But_Not_When_Open", "Elevator" }, 16),
           new Value("Door Open Time (sec)", typeof(float)),
           new Value("Door Occlusion Bounds [0,1]", typeof(float)),
           new Value("...To", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Collision Response", new string[] { "Pause_Until_Crushed", "Reverse_Directions", "Discs" }, 16),
           new Value("Elevator Node", typeof(short)),
           new HaloPlugins.Objects.Data.Enum("Pathfinding Policy", new string[] { "Discs", "Sectors", "Cut_Out", "None" }, 32),
           });
       }
    }
}
