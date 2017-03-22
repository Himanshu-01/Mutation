using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class pmov : TagDefinition
    {
       public pmov() : base("pmov", "particle_physics", 20)
       {
           Fields.AddRange(new MetaNode[] {
           new TagReference("Particle_Physics", "pmov"),
           new Flags("Flags", new string[] { "Physics", "Collide_with_Structure", "Collide_with_Media", "Collide_with_Scenery", "Collide_with_Vehicles", "Collide_with_Bipeds", "Swarm", "Wind" }, 32),
           new TagBlock("Unknown6", 20, 4, new MetaNode[] { 
               new Value("Unknown", typeof(int)),
               new TagBlock("Unknown8", 20, 9, new MetaNode[] { 
                   new Value("Unknown", typeof(int)),
                   new Value("Unknown", typeof(int)),
                   new Value("Unknown", typeof(int)),
                   new TagBlock("Unknown12", 1, -1, new MetaNode[] { 
                       new Value("Floats", typeof(byte)),
                   }),
               }),
               new Value("Unknown", typeof(int)),
               new Value("Unknown", typeof(int)),
           }),
           });
       }
    }
}
