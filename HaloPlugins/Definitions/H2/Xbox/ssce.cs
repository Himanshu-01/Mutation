using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class ssce : TagDefinition
    {
       public ssce() : base("ssce", "sound_scenery", 204)
       {
           // Obje
           Fields.AddRange(new obje().Fields.ToArray());

           // Ssce
           Fields.AddRange(new MetaNode[] {
           new Padding(16),
           });
       }
    }
}
