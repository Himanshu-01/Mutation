using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Objects;
using HaloPlugins.Objects.Data;
using HaloPlugins.Objects.Reference;

namespace HaloPlugins.Xbox
{
    public class coll : TagDefinition
    {
       public coll() : base("coll", "collision_model", 52)
       {
           Fields.AddRange(new MetaNode[] {
           new Padding(20),
           new TagBlock("Materials", 4, 32, new MetaNode[] { 
               new StringId("Name"),
           }),
           new TagBlock("Regions", 12, 16, new MetaNode[] {
               new StringId("Name"),
               new TagBlock("Premutations", 20, 32, new MetaNode[] { 
                   new StringId("Name"),
                   new TagBlock("BSP", 68, 64, new MetaNode[] { 
                       new Value("Root Node Index", typeof(int)),
                       new TagBlock("3D Nodes", 8, -1, 8, new MetaNode[] { 
                           new Value("Plane", typeof(short)),
                           new Value("First Child", typeof(short)),
                           new Value("Unknown", typeof(byte)),
                           new Value("Second Child", typeof(short)),
                           new Value("Unknown", typeof(byte)),
                       }),
                       new TagBlock("Planes", 16, -1, 16, new MetaNode[] { 
                           new Value("i", typeof(float)),
                           new Value("I", typeof(float)),
                           new Value("k", typeof(float)),
                           new Value("d", typeof(float)),
                       }),
                       new TagBlock("Leaves", 4, -1, 16, new MetaNode[] { 
                           new Flags("Flags", new string[] { "Contains_Double_Sided_Surfaces" }, 8),
                           new Value("2D Reference COunt", typeof(byte)),
                           new Value("First 2D Reference", typeof(short)),
                       }),
                       new TagBlock("2D Refrences", 4, -1, new MetaNode[] { 
                           new Value("Plane", typeof(short)),
                           new Value("Node", typeof(short)),
                       }),
                       new TagBlock("2D Nodes", 16, -1, 16, new MetaNode[] { 
                           new Value("Plane i", typeof(float)),
                           new Value("Plane j", typeof(float)),
                           new Value("Plane k", typeof(float)),
                           new Value("Left Child", typeof(short)),
                           new Value("Right Child", typeof(short)),
                       }),
                       new TagBlock("Surfaces", 8, -1, 8, new MetaNode[] { 
                           new Value("Plane", typeof(short)),
                           new Value("First Edge", typeof(short)),
                           new Flags("Flags", new string[] { "Two-Sided", "Invisible", "Climbable", "Breakable", "Invalid", "Conveyor" }, 8),
                           new Value("Breakable Surface", typeof(byte)),
                           new Value("Material", typeof(short)),
                       }),
                       new TagBlock("Edges", 12, -1, 12, new MetaNode[] { 
                           new Value("Start Vertex", typeof(short)),
                           new Value("End Vertex", typeof(short)),
                           new Value("Forward Edge", typeof(short)),
                           new Value("Reverse Edge", typeof(short)),
                           new Value("Left Surface", typeof(short)),
                           new Value("Right Surface", typeof(short)),
                       }),
                       new TagBlock("Vertices", 16, -1, 16, new MetaNode[] { 
                           new Value("x", typeof(float)),
                           new Value("y", typeof(float)),
                           new Value("z", typeof(float)),
                           new Value("First Edge", typeof(int)),
                       }),
                   }),
                   new TagBlock("BSP Physics", 112, 1024, new MetaNode[] { 
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Padding(4),
                       new Value("Unknown", typeof(byte)),
                       new Value("Unknown", typeof(byte)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(int)),
                       new Value("Unknown", typeof(int)),
                       new Value("Unknown", typeof(int)),
                       new Padding(4),
                       new Value("Unknown", typeof(int)),
                       new Value("Unknown", typeof(int)),
                       new Value("Unknown", typeof(int)),
                       new Padding(4),
                       new TagIndex("Object_Properties", "****"),
                       new Padding(12),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Padding(4),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Padding(4),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(short)),
                       new Value("Unknown", typeof(byte)),
                       new Value("Unknown", typeof(byte)),
                       new Value("Unknown", typeof(short)),
                       new TagBlock("MOPP Code", 1, -1, 16, new MetaNode[] { 
                           new Value("Data", typeof(byte)),
                       }),
                       new Padding(4),
                   }),
               }),
           }),
           new TagBlock("Path Finding Spheres", 20, 32, new MetaNode[] { 
               new Value("Node Index", typeof(short)),
               new Flags("Flags", new string[] { "Remains_When_Open", "Vehicle_Only", "With_Sectors" }, 16),
               new Value("Center Point X", typeof(float)),
               new Value("Center Point Y", typeof(float)),
               new Value("Center Point Z", typeof(float)),
               new Value("Radius", typeof(float)),
           }),
           new TagBlock("Nodes", 12, 255, new MetaNode[] { 
               new StringId("Name"),
               new Value("Parent", typeof(short)),
               new Value("Child", typeof(short)),
               new Value("Next Sibling", typeof(short)),
               new Value("First Child Node", typeof(short)),
           }),
           });
       }
    }
}
