using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class bsdt : TagDefinition
    {
       public bsdt() : base("bsdt", "breakable_surface", 32)
       {
           Fields.AddRange(new MetaNode[] {
           new Value("Max Vitality", typeof(float)),
           new TagReference("Effect", "effe"),
           new TagReference("Sound", "snd!"),
           new TagBlock("Particle Systems", 56, 32, new MetaNode[] { 
               new TagReference("Particle", "****"),
               new Value("Location", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Coordinate System", new string[] { "World", "Local", "Parent" }, 16),
               new HaloPlugins.Objects.Data.Enum("Environment", new string[] { "Any_Environment", "Air_Only", "Water_Only", "Space_Only" }, 16),
               new HaloPlugins.Objects.Data.Enum("Disposition", new string[] { "Either_Mode", "Violent_Mode_Only", "NonViolent_Mode_Only" }, 16),
               new HaloPlugins.Objects.Data.Enum("Camera Mode", new string[] { "Independant_Of_Camera_Mode", "Only_In_1st_Person", "Only_In_3rd_Person", "Both_1st_And_3rd" }, 16),
               new Value("Sort Bias", typeof(short)),
               new Flags("Flags", new string[] { "Glow", "Cinematics", "Looping_Particle", "Disabled_For_Debugging", "Inherit_Effect_Velocity", "Don't_Render_System", "Render_When_Zoomed", "Spread_Between_Ticks", "Persistent_Particle", "Expensive_Visibility" }, 32),
               new Padding(4),
               new Value("LOD In Distance", typeof(float)),
               new Value("LOD Feather In Delta", typeof(float)),
               new Value("LOD Out Distance", typeof(float)),
               new Value("LOD Feather Out Delta", typeof(float)),
               new Padding(4),
               new TagBlock("Emitters", 184, 8, -1, new MetaNode[] { 
                   new TagReference("Particle_Physics", "pmov"),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Emission Rate", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Lifespan (sec)", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Velocity (World Units Per Sec)", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Angular Velocity (Degrees Per Sec)", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Size (World Units)", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Tint", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Alpha", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Emission Shape", new string[] { "Sprayer", "Disc", "Globe", "Implode", "Tube", "Halo", "Impact_Contour", "Impact_Area", "Debris", "Line" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Emission Radius (World Units)", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Emission Angle (Degrees)", 1, -1, new MetaNode[] { 
                       new Value("Data", typeof(byte)),
                   }),
                   new Value("Translation Offset X", typeof(float)),
                   new Value("Translation Offset Y", typeof(float)),
                   new Value("Translation Offset Z", typeof(float)),
                   new Value("Relative Direction Yaw", typeof(float)),
                   new Value("Relative Direction Pitch", typeof(float)),
                   new Padding(8),
               }),
           }),
           new Value("Particle Density", typeof(float)),
           });
       }
    }
}
