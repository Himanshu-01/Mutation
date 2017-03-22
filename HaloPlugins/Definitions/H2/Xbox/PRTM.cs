using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.H2.Xbox;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class PRTM : TagDefinition
    {
       public PRTM() : base("PRTM", "particle_model", 220, new ParticleDefinition())
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Spins", "Random_U_Mirror", "Random_V_Mirror", "Frame_Animation_One_Shot", "Select_Random_Sequence", "Disable_Frame_Blending", "Can_Animate_Backwards", "Receive_Lightmap_Lighting", "Tint_From_Diffuse_Texture", "Dies_At_Rest", "Dies_On_Structure_Collision", "Dies_In_Media", "Dies_In_Air", "Bitmap_Authored_Vertically", "Has_Sweetener" }, 32),
           new HaloPlugins.Objects.Data.Enum("Orientation", new string[] { "Screen_Facing", "Parallel_To_Direction", "Perpendicular_To_Direction", "Vertical", "Horizontal" }, 32),
           new Padding(16),
           new TagReference("Shader", "shad"),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Scale X", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Scale Y", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Scale Z", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Rotation", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new TagReference("Collision_Effect", "****"),
           new TagReference("Death_Effect", "****"),
           new TagBlock("Locations", 4, 32, new MetaNode[] { 
               new StringId("Marker Name"),
           }),
           new TagBlock("Attached Particle System", 56, 32, new MetaNode[] { 
               new TagReference("Particle", "****"),
               new Padding(8),
               new Value("Location #", typeof(short)),
               new HaloPlugins.Objects.Data.Enum("Coordinate System", new string[] { "World", "Local", "Parent" }, 16),
               new HaloPlugins.Objects.Data.Enum("Environment", new string[] { "Any", "Air_Only", "Water_Only", "Space_Only" }, 16),
               new HaloPlugins.Objects.Data.Enum("Disposition", new string[] { "Either_Mode", "Violent_Mode", "Non-Violent_Mode" }, 16),
               new HaloPlugins.Objects.Data.Enum("Camera Mode", new string[] { "Independent_Of_Camera_Mode", "Only_In_First_Person", "Only_In_Third_Person", "Both_First_And_Third" }, 16),
               new Value("Sort Bias", typeof(short)),
               new Flags("Flags", new string[] { "Glow", "Cinematics", "Loop_Particle", "Disabled_For_Debugging", "Inherit_Effect_Velocity", "Render_When_Zoomed", "Spread_Between_Ticks", "persistent_Particle", "Expensive_Visibility" }, 32),
               new Value("LOD In Distance", typeof(float)),
               new Value("LOD Feather In Delta", typeof(float)),
               new Value("LOD Out Distance", typeof(float)),
               new Value("LOD Feather Out Delta", typeof(float)),
               new TagBlock("Emitters", 184, 8, new MetaNode[] { 
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
           new TagBlock("Models", 8, 256, new MetaNode[] { 
               new StringId("Model Name"),
               new Value("Index Start", typeof(short)),
               new Value("Index Count", typeof(short)),
           }),
           new TagBlock("Raw Vertices", 56, 32768, new MetaNode[] { 
               new Value("Pos X", typeof(float)),
               new Value("Pos Y", typeof(float)),
               new Value("Pos Z", typeof(float)),
               new Value("Normal i", typeof(float)),
               new Value("Normal j", typeof(float)),
               new Value("Normal k", typeof(float)),
               new Value("Tangent i", typeof(float)),
               new Value("Tangent j", typeof(float)),
               new Value("Tangent k", typeof(float)),
               new Value("Binormal i", typeof(float)),
               new Value("Binormal j", typeof(float)),
               new Value("Binormal k", typeof(float)),
               new Value("Texture Coord X", typeof(float)),
               new Value("Texture Coord Y", typeof(float)),
           }),
           new TagBlock("Indices", 2, 32768, new MetaNode[] { 
               new Value("Index", typeof(short)),
           }),
           new TagBlock("Cached Data", 0, 1, new MetaNode[] { 
           }),
           new Value("Raw Offset", typeof(int)), // uint
           new Value("Raw Size", typeof(int)), // uint
           new Value("Raw Header Size", typeof(uint)),
           new Value("Raw Data Size", typeof(uint)),
           new TagBlock("Resources", 16, -1, new MetaNode[] { 
               new HaloPlugins.Objects.Data.Enum("Type", new string[] {  }, 32),
               new Value("Primary Location", typeof(short)),
               new Value("Secondary Location", typeof(short)),
               new Value("Raw Data Size", typeof(uint)),
               new Value("Raw Data Offset", typeof(uint)),
           }),
           new TagIndex("Owner Tag Section Offset", "PRTM"),
           new Value("Unused", typeof(int)),
           new Value("Unused", typeof(int)),
           new Padding(24),
           });
       }
    }
}
