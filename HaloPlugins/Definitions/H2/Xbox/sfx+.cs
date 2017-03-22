using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class sfx : TagDefinition
    {
       public sfx() : base("sfx+", "sound_effect_collection", 8)
       {
           Fields.AddRange(new MetaNode[] {
           new TagBlock("Sound Effects", 56, 128, new MetaNode[] { 
               new StringId("Name"),
               new Padding(8),
               new Flags("Flags", new string[] { "Use_3D_Radio_Hack" }, 32),
               new Padding(8),
               new TagBlock("Filter", 72, -1, new MetaNode[] { 
                   new HaloPlugins.Objects.Data.Enum("Filter Type", new string[] { "Parametric_EQ", "DLS2", "Both_(Mono_Only)" }, 32),
                   new Value("Filter Width", typeof(float)),
                   new Value("Left Filter Frequence Scale Bounds", typeof(float)),
                   new Value("Left Filter Frequence To", typeof(float)),
                   new Value("Left Filter Frequence Random Base and Variance", typeof(float)),
                   new Value("Left Filter Frequence To", typeof(float)),
                   new Value("Left Filter Gain Scale Bounds", typeof(float)),
                   new Value("Left Filter Gain To", typeof(float)),
                   new Value("Left Filter Gain Random Base and Variance", typeof(float)),
                   new Value("Left Filter Gain To", typeof(float)),
                   new Value("Right Filter Frequence Scale Bounds", typeof(float)),
                   new Value("Right Filter Frequence To", typeof(float)),
                   new Value("Right Filter Frequence Random Base and Variance", typeof(float)),
                   new Value("Right Filter Frequence To", typeof(float)),
                   new Value("Right Filter Gain Scale Bounds", typeof(float)),
                   new Value("Right Filter Gain To", typeof(float)),
                   new Value("Right Filter Gain Random Base and Variance", typeof(float)),
                   new Value("Right Filter Gain To", typeof(float)),
               }),
               new TagBlock("Pitch LFO", 48, -1, new MetaNode[] { 
                   new Value("Delay Scale Bounds", typeof(float)),
                   new Value("Delay Scale To", typeof(float)),
                   new Value("Delay Random Pitch and Variance", typeof(float)),
                   new Value("Delay To", typeof(float)),
                   new Value("Frequence Scale Bounds", typeof(float)),
                   new Value("Frequence To", typeof(float)),
                   new Value("Frequence Random Pitch and Variance", typeof(float)),
                   new Value("Frequence To", typeof(float)),
                   new Value("Pitch Modulation Scale Bounds", typeof(float)),
                   new Value("Pitch Modulation To", typeof(float)),
                   new Value("Pitch Modulation Random Pitch and Variance", typeof(float)),
                   new Value("Pitch Modulation To", typeof(float)),
               }),
               new TagBlock("Filter LFO", 64, -1, new MetaNode[] { 
                   new Value("Delay Scale Bounds", typeof(float)),
                   new Value("Delay Scale To", typeof(float)),
                   new Value("Delay Random Pitch and Variance", typeof(float)),
                   new Value("Delay To", typeof(float)),
                   new Value("Frequence Scale Bounds", typeof(float)),
                   new Value("Frequence To", typeof(float)),
                   new Value("Frequence Random Pitch and Variance", typeof(float)),
                   new Value("Frequence To", typeof(float)),
                   new Value("Cutoff Modulation Scale Bounds", typeof(float)),
                   new Value("Cutoff Modulation To", typeof(float)),
                   new Value("Cutoff Modulation Random Pitch and Variance", typeof(float)),
                   new Value("Cutoff Modulation To", typeof(float)),
                   new Value("Gain Modulation Scale Bounds", typeof(float)),
                   new Value("Gain Modulation To", typeof(float)),
                   new Value("Gain Modulation Random Pitch and Variance", typeof(float)),
                   new Value("Gain Modulation To", typeof(float)),
               }),
               new TagBlock("Sound Effect", 40, -1, new MetaNode[] { 
                   new TagReference("Template", "****"),
                   new TagBlock("Components", 16, -1, new MetaNode[] { 
                       new TagReference("Sound", "****"),
                       new Value("Gain", typeof(float)),
                       new Flags("Flags", new string[] { "Don't_play_at_start", "Play_On_Stop", "", "Play_Alternate", "", "Sync_With_Origin_Looping_Sound" }, 32),
                   }),
                   new Padding(16),
                   new TagBlock("Unknown57", 20, -1, new MetaNode[] { 
                       new TagBlock("Unknown58", 28, -1, new MetaNode[] { 
                           new TagBlock("Unknown59", 16, -1, new MetaNode[] { 
                               new Value("Unknown", typeof(int)),
                               new TagBlock("Unknown61", 1, -1, new MetaNode[] { 
                                   new Value("Unknown", typeof(byte)),
                               }),
                               new Value("Unknown", typeof(float)),
                           }),
                           new TagBlock("Unknown66", 4, -1, new MetaNode[] { 
                               new Value("Unknown", typeof(float)),
                           }),
                           new TagBlock("Unknown69", 4, -1, new MetaNode[] { 
                               new Value("Unknown", typeof(int)),
                           }),
                           new Value("Unknown", typeof(int)),
                       }),
                       new TagBlock("Unknown74", 16, -1, new MetaNode[] { 
                           new Value("Unknown", typeof(int)),
                           new TagBlock("Unknown76", 1, -1, new MetaNode[] { 
                               new Value("Unknown", typeof(byte)),
                           }),
                           new Value("Unknown", typeof(float)),
                       }),
                       new Value("Unknown", typeof(int)),
                   }),
               }),
           }),
           });
       }
    }
}
