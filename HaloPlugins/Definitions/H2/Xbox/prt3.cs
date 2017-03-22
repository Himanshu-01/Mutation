using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class prt3 : TagDefinition
    {
       public prt3() : base("prt3", "particle", 188)
       {
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Spins", "Random_U_Mirror", "Random_V_Mirror", "Frame_Animation_One_Shot", "Select_Random_Sequence", "Disable_Frame_Blending", "Receive_Lightmap_Lighting", "Tint_From_Diffuse_Texture", "Dies_At_Rest", "Dies_On_Structure_Collision", "Dies_In_Media", "Dies_In_Air", "Bitmap_Authored_Vertically", "Has_Sweetener" }, 32),
           new HaloPlugins.Objects.Data.Enum("Particle Billboard Style", new string[] { "Screen_Facing", "Parallel_To_Direction", "Perpendicular_To_Direction", "Vertical", "Horizontal" }, 32),
           new Value("First Billboard Style", typeof(short)),
           new Value("Sequence Count", typeof(short)),
           new TagReference("Shader_Template", "stem"),
           new TagBlock("Shader", 40, 64, new MetaNode[] { 
               new StringId("Unknown"),
               new Value("Unknown", typeof(int)),
               new TagReference("Unknown", "bitm"),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
               new Value("Unknown", typeof(float)),
               new Padding(8),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Color", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Alpha", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Scale", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Rotation", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
           new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
           new TagBlock("Frame Index", 1, -1, new MetaNode[] { 
               new Value("Data", typeof(byte)),
           }),
           new TagReference("Collision_Effect", "****"),
           new TagReference("Death_Effect", "****"),
           new TagBlock("Locations", 4, 32, new MetaNode[] { 
               new StringId("Location"),
           }),
           new TagBlock("Attached Particle Systems", 56, 32, new MetaNode[] { 
               new TagReference("Particle", "****"),
               new Value("Location", typeof(short)),
               new Padding(2),
               new HaloPlugins.Objects.Data.Enum("Coordinate System", new string[] { "World", "Local", "Parent" }, 16),
               new HaloPlugins.Objects.Data.Enum("Environment", new string[] { "Any_Environment", "Air_Only", "Water_Only", "Space_Only" }, 16),
               new HaloPlugins.Objects.Data.Enum("Disposition", new string[] { "Either_Mode", "Violent_Mode_Only", "NonViolent_Mode_Only" }, 16),
               new HaloPlugins.Objects.Data.Enum("Camera Mode", new string[] { "Independant_Of_Camera_Mode", "Only_In_1st_Person", "Only_In_3rd_Person", "Both_1st_And_3rd" }, 16),
               new Value("Sort Bias", typeof(short)),
               new Flags("Flags", new string[] { "Glow", "Cinematics", "Looping_Particle", "Disabled_For_Debugging", "Inherit_Effect_Velocity", "Don't_Render_System", "Render_When_Zoomed", "Spread_Between_Ticks", "Persistent_Particle", "Expensive_Visibility" }, 32),
               new Padding(2),
               new Value("LOD In Distance", typeof(float)),
               new Value("LOD Feather In Delta", typeof(float)),
               new Value("LOD Out Distance", typeof(float)),
               new Value("LOD Feather Out Delta", typeof(float)),
               new Padding(4),
               new TagBlock("Emitters", 184, 8, new MetaNode[] { 
                   new TagReference("Particle_Physics", "pmov"),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "", "Plus", "Times" }, 32),
                   new HaloPlugins.Objects.Data.Enum("Output Modifier", new string[] { "Particle_Age", "Particle_Emit_Time", "Particle_Random_1", "Particle_Random_2", "Emitter_Age", "Emitter_Random_1", "Emitter_Random_2", "System_LOD", "Game_Time", "Effect_A_Scale", "Effect_B_Scale", "Particle_Rotation", "Explosion_Animation", "Explosion_Rotation", "Particle_Random_3", "Particle_Random_4", "Location_Random" }, 32),
                   new TagBlock("Particle Emission Rate", 1,  -1,new MetaNode[] { 
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
           new TagBlock("Postprocess Definitions", 124, 1, new MetaNode[] { 
               new TagIndex("Shader Template", "stem"),
               new TagBlock("Bitmaps", 12, 1024, new MetaNode[] { 
                   new TagIndex("Bitmap Group", "bitm"),
                   new Value("Bitmap Index", typeof(int)),
                   new Value("Log Bitmap Dimension", typeof(float)),
               }),
               new TagBlock("Pixel Constants", 4, 1024, new MetaNode[] { 
                   new Value("Color A", typeof(byte)),
                   new Value("Color R", typeof(byte)),
                   new Value("Color G", typeof(byte)),
                   new Value("Color B", typeof(byte)),
               }),
               new TagBlock("Vertex Constants", 16, 1024, new MetaNode[] { 
                   new Value("X Scale", typeof(float)),
                   new Value("Y Scale", typeof(float)),
                   new Value("X Scale", typeof(float)),
                   new Value("W Scale", typeof(float)),
               }),
               new TagBlock("Levels Of Detail", 8, 1024, new MetaNode[] { 
                   new Value("Available Layer Flags", typeof(int)),
                   new Value("Layers", typeof(int)),
               }),
               new TagBlock("Layers", 2, 1024, 4, new MetaNode[] { //4 fixed
                   new Value("Indice", typeof(short)),
               }),
               new TagBlock("Passes", 2, 1024, 4, new MetaNode[] { //4 fixed
                   new Value("Indice", typeof(short)),
               }),
               new TagBlock("Implementations", 10, 1024, 4, new MetaNode[] { //10 fixed
                   new Value("Bitmap Transform", typeof(short)),
                   new Value("Render State", typeof(short)),
                   new Value("Texture State", typeof(short)),
                   new Value("Pixel Constant", typeof(short)),
                   new Value("Vertex Constant", typeof(short)),
               }),
               new TagBlock("Overlays", 20, 1024, 4, new MetaNode[] { 
                   new StringId("Input Name"),
                   new StringId("Range Name"),
                   new Value("Time Period In Seconds", typeof(float)),
                   new TagBlock("Functions", 1, -1, new MetaNode[] { 
                       new Value("Function", typeof(byte)),
                   }),
               }),
               new TagBlock("Overlay References", 4, 1024, new MetaNode[] { 
                   new Value("Overlay Index", typeof(short)),
                   new Value("Transform Index", typeof(short)),
               }),
               new TagBlock("Animated Parameters", 2, 1024, new MetaNode[] { // fixed
                   new Value("Overlay Reference", typeof(short)),
               }),
               new TagBlock("Animated Parameter References", 4, 1024, 4, new MetaNode[] {
                   new Value("Parameter Index", typeof(int)), // NEEDS to be 4!
               }),
               new TagBlock("Bitmap Properties", 4, 5, new MetaNode[] { 
                   new Value("Bitmap Index", typeof(short)),
                   new Value("Animated Parameter Index", typeof(short)),
               }),
               new TagBlock("Color Properties: 1st chunk = colour of emitted light, second chunk = tint colour", 12, 2, new MetaNode[] { 
                   new Value("R", typeof(float)),
                   new Value("G", typeof(float)),
                   new Value("B", typeof(float)),
               }),
               new TagBlock("Value Properties", 4, 6, new MetaNode[] { 
                   new Value("Value", typeof(float)),
               }),
               //new Value("Unknown", typeof(int)),
               //new Value("Unknown", typeof(int)),
               new Padding(8),
           }),
           new Padding(4),
           new Value("Unknown", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Value("Unknown", typeof(int)),
           new Value("Unknown", typeof(float)),
           new Value("Unknown", typeof(float)),
           new Value("Unknown", typeof(float)),
           new Value("Unknown", typeof(float)),
           new Value("Unknown", typeof(float)),
           new Value("Unknown", typeof(float)),
           });
       }
    }
}
