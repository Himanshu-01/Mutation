using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class proj : TagDefinition
    {
       public proj() : base("proj", "projectile", 420)
       {
           // Obje
           Fields.AddRange(new obje().Fields.ToArray());

           // Proj
           Fields.AddRange(new MetaNode[] {
           new Flags("Flags", new string[] { "Oriented_Along_Velocity", "AI_Must_Use_Ballistic_Aiming", "Detonation_Max_Time_If_Attached", "Has_Super_Combining_Explosion", "Damage_Scales_Based_On_Distance", "Travels_Instantaneously", "Steering_Adjusts_Orientation", "Don't_Noise_Up_Steering", "Can_Track_Behind_Itself", "Robotron_Steering", "Faster_When_Owned_By_Player" }, 32),
           new HaloPlugins.Objects.Data.Enum("Detonation Timer Starts", new string[] { "Immediately", "After_first_bounce", "When_at_rest", "After_First_Bounce_Off_Any_Surface" }, 16),
           new HaloPlugins.Objects.Data.Enum("Impact Noise", new string[] { "Silent", "Medium", "Loud", "Shout", "Quiet" }, 16),
           new Value("AI Perception Radius", typeof(float)),
           new Value("Collision Radius", typeof(float)),
           new Value("Arming Time", typeof(float)),
           new Value("Danger Radius", typeof(float)),
           new Value("Timer(sec)", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Minimum Velocity", typeof(float)),
           new Value("Maximum Range", typeof(float)),
           new HaloPlugins.Objects.Data.Enum("Detonation Noise", new string[] { "Silent", "Medium", "Loud", "Shout", "Quiet" }, 16),
           new Value("Super Detonation Projectile Count", typeof(short)),
           new TagReference("Detonation_Started", "effe"),
           new TagReference("Airborne_Detonation", "effe"),
           new TagReference("Ground_Detonation", "effe"),
           new TagReference("Detonation_Damage", "jpt!"),
           new TagReference("Attached_Detonation_Damage", "jpt!"),
           new TagReference("Super_Detonation", "effe"),
           new TagReference("Super_Detonation_Damage", "jpt!"),
           new TagReference("Detonation_Sound", "snd!"),
           new HaloPlugins.Objects.Data.Enum("Damage Reporting Type", new string[] { "The_Guardians", "Falling_Damage", "Generic_Collision_Damage", "Generic_Melee_Damage", "Generic_Explosion", "Magnum_Pistol", "Plasma_Pistol", "Needler", "SMG", "Plasma_Rifle", "Battle_Rifle", "Carbine", "Shotgun", "Sniper_Rifle", "Beam_Rifle", "Rocket_Launcher", "Flak_Cannon", "Brute_Shot", "Disintegrator", "Brute_Plasma_Rifle", "Energy_Sword", "Frag_Grenade", "Plasma_Grenade", "Flag_Melee_Damage", "Bomb_Melee_Damage", "Bomb_Explosion_Damage", "Ball_Melee_Damage", "Human_Turret", "Plasma_Turret", "Banshee", "Ghost", "Mongoose", "Scorpion", "Spectre_Driver", "Spectre_Gunner", "Warthog_Driver", "Warthog_Gunner", "Wraith", "Tank", "Sentinal_Beam", "Sentinal_RPG", "Teleporter" }, 32),
           new TagReference("Super_Attached_Detonation_Damage", "jpt!"),
           new Value("Material Effect Radius", typeof(float)),
           new TagReference("Fly_By_Sound", "snd!"),
           new TagReference("Impact_Effect", "effe"),
           new TagReference("Impact_Damage", "jpt!"),
           new Value("Boarding Detonation Time", typeof(float)),
           new TagReference("Boarding_Detonation_Damage", "jpt!"),
           new TagReference("Boarding_Attached_Detonation_Damage", "jpt!"),
           new Value("Air Gravity Scale", typeof(float)),
           new Value("Air Damage Range", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Water Gravity Scale", typeof(float)),
           new Value("Water Damage Range", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Initial Velocity", typeof(float)),
           new Value("Final Velocity", typeof(float)),
           new Value("Guided Angular Velocity Lower (degrees per sec)", typeof(float)),
           new Value("Guided Angular Velocity Upper (degrees per sec)", typeof(float)),
           new Value("Acceleration range (world units)", typeof(float)),
           new Value("...To", typeof(float)),
           new Value("Targeted Leading Fraction", typeof(float)),
           new Value("Position Loops", typeof(float)),
           new TagBlock("Material Responses", 88, 200, new MetaNode[] { 
               new Flags("Flags", new string[] { "Cannot_Be_Overpenetrated" }, 16),
               new HaloPlugins.Objects.Data.Enum("Default Response", new string[] { "Impact_(Detonate)", "Fizzle", "Overpenetrate", "Attach", "Bounce", "Bounce_(Dud)", "Fizzle_(Ricochet)" }, 16),
               new TagReference("DO_NOT_USE_(OLD_effect)", "effe"),
               new StringId("Material Name"),
               new Padding(2),
               new HaloPlugins.Objects.Data.Enum("Response", new string[] { "Impact_(Detonate)", "Fizzle", "Overpenetrate", "Attach", "Bounce", "Bounce_(Dud)", "Fizzle_(Ricochet)" }, 16),
               new Flags("Flags", new string[] { "Only_Against_Units", "Never_Against_Units" }, 32),
               new Value("Chance Fraction", typeof(float)),
               new Value("Between", typeof(float)),
               new Value("To", typeof(float)),
               new Value("And", typeof(float)),
               new Value("To", typeof(float)),
               new TagReference("DO_NOT_USE_(OLD_Effect)", "effe"),
               new HaloPlugins.Objects.Data.Enum("Scale Effects By", new string[] { "Damage", "Angle" }, 32),
               new Value("Angular Noise", typeof(float)),
               new Value("Veloctiy Noise", typeof(float)),
               new TagReference("DO_NOT_USE_(OLD_Detonation)", "effe"),
               new Value("Initial Friction", typeof(float)),
               new Value("Maximum Distance", typeof(float)),
               new Value("Parallel Friction", typeof(float)),
               new Value("Perpendicluar Friction", typeof(float)),
           }),
           });
       }
    }
}
