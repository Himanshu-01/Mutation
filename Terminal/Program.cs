using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using HaloMap;

namespace Terminal
{
    class Program
    {
        static string MapsSc = "D:\\Halo 2\\Xbox\\Maps";
        static string TestsSc = "D:\\Mutation Tests";



        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Ussage: Terminal -parms -sourcemap -targetmap");
                Console.WriteLine("     Parms:");
                Console.WriteLine("         -p Print Info");
                Console.WriteLine("         -c <section> Compare Section");
                Console.WriteLine("         -d <section> <filename> Dump Section To File");
                Console.WriteLine("         -m Run Memory Checks");
                Console.WriteLine("         exit Quits the Application");
                Console.WriteLine("             Sections:");
                Console.WriteLine("                 Sound_Raw");
                Console.WriteLine("                 Model_Raw");
                Console.WriteLine("                 Bsp_Raw");
                Console.WriteLine("                 Lightmap_Raw");
                Console.WriteLine("                 Weather_Raw");
                Console.WriteLine("                 Decorator_Raw");
                Console.WriteLine("                 Particle_Raw");
                Console.WriteLine("                 Coconut_Raw");
                Console.WriteLine("                 Animation_Raw");
                //Console.WriteLine("                 Bsp_Meta");
                //Console.WriteLine("                 Lightmap_Meta");
                Console.WriteLine("                 String_Tables");
                Console.WriteLine("                 Bitmap_Raw");
                Console.WriteLine("                 Locale_Tables");
                Console.WriteLine("");

                // Get New Args
                Console.Write("terminal.exe ");

                // Run Again
                string[] Args = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.None);

                // Safty Check
                if (Args.Length > 0 && Args[0].ToLower() == "exit")
                    return;
                else
                    Main(Args);
            }
            else
            { 
                // Pull out the Maps
                string Source = args[args.Length - 2];
                string Target = args[args.Length - 1];

                // Add Shortcuts
                if (Source.Substring(0, 4).ToLower() == "maps")
                    Source = Source.Replace("maps", MapsSc);
                if (Target.Substring(0, 5).ToLower() == "tests")
                    Target = Target.Replace("tests", TestsSc);

                // Check they Exist
                if (!File.Exists(Source))
                {
                    Console.WriteLine("Error Reading Source Map!");
                    Console.ReadLine();
                    return;
                }
                if (!File.Exists(Target))
                {
                    Console.WriteLine("Error Reading Target Map!");
                    Console.ReadLine();
                    return;
                }

                // Open Both
                Console.WriteLine("Reading Source Map...");
                Map SourceMap = new Map(Source);
                SourceMap.Read(null);
                Console.WriteLine("Reading Target Map...");
                Map TargetMap = new Map(Target);
                TargetMap.Read(null);

                // Execute Parms
                int i = 0;
                while (i < args.Length - 2)
                {
                    switch (args[i])
                    {
                        case "-p":
                            {
                                // Write Header
                                Console.WriteLine();
                                Console.WriteLine("| Field | SourceMap | TargetMap |");
                                Console.WriteLine("Tag Start: " + SourceMap.Index.TagStart.ToString() + " " + TargetMap.Index.TagStart.ToString());
                                Console.WriteLine("Scenario Id: " + SourceMap.Index.ScenarioIdent.ToString() + " " + TargetMap.Index.ScenarioIdent.ToString());
                                Console.WriteLine("Globals Id: " + SourceMap.Index.GlobalsIdent.ToString() + " " + TargetMap.Index.GlobalsIdent.ToString());
                                Console.WriteLine("");
                                Console.WriteLine("Memory Magic: " + SourceMap.Index.PrimaryMagic.ToString() + " " + TargetMap.Index.PrimaryMagic.ToString());
                                Console.WriteLine("Meta Magic: " + SourceMap.Index.SecondaryMagic.ToString() + " " + TargetMap.Index.SecondaryMagic.ToString());
                                break;
                            }
                    }

                    i++;
                }

                Console.ReadLine();
            }
        }
    }
}
