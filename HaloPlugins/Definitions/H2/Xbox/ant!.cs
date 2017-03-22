using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;
using HaloPlugins.Objects.Vector;
using HaloPlugins.Objects.Color;

namespace HaloPlugins.Xbox
{
    public class ant : TagDefinition
    {
       public ant() : base("ant!", "antenna", 160)
       {
           Fields.AddRange(new MetaNode[] {
           new StringId("Attachment Marker Name"),
           new TagReference("Bitmap", "bitm"),
           new TagReference("Physics", "pphy"),
           new Padding(80),
           new Value("Spring Strength Coefficient", typeof(float)),
           new Value("Falloff Pixels", typeof(float)),
           new Value("Cutoff Pixels", typeof(float)),
           new Padding(40),
           new TagBlock("Vertices", 128, 20, new MetaNode[] { 
               new Value("Spring Strength Coefficient", typeof(float)),
               new RealAngle2d("Angles"),
               new Padding(24),
               new Value("Length(world units)", typeof(float)),
               new Value("Sequence index", typeof(int)),
               new RealColorArgb("Color"),
               new Padding(52),
               new RealColorArgb("LOD Color"),
           }),
           });
       }
    }
}
