using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class unic : TagDefinition
    {
       public unic() : base("unic", "unicode_string_list", 52)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(16),
           new Value("English String Offset", typeof(ushort)),
           new Value("English String Count", typeof(ushort)),
           new Value("Japanese String Offset", typeof(ushort)),
           new Value("Japanese String Count", typeof(ushort)),
           new Value("German String Offset", typeof(ushort)),
           new Value("German String Count", typeof(ushort)),
           new Value("French String Offset", typeof(ushort)),
           new Value("French String Count", typeof(ushort)),
           new Value("Spanish String Offset", typeof(ushort)),
           new Value("Spanish String Count", typeof(ushort)),
           new Value("Italian String Offset", typeof(ushort)),
           new Value("Italian String Count", typeof(ushort)),
           new Value("Korean String Offset", typeof(ushort)),
           new Value("Korean String Count", typeof(ushort)),
           new Value("Chinese String Offset", typeof(ushort)),
           new Value("Chinese String Count", typeof(ushort)),
           new Value("Portuguese String Offset", typeof(ushort)),
           new Value("Portuguese String Count", typeof(ushort)),
           });
       }
    }
}
