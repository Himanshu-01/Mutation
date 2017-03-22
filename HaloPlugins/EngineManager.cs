using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaloPlugins
{
    public class EngineManager
    {
        public enum HaloEngine
        {
            Neutral,
            Halo1,
            Halo2,
            Halo3
        }

        #region Engines

        /*
         * Halo1PC
         * Halo1Xbox
         * HaloCE
         * ShadowRunBeta
         * Halo2Beta
         * Halo2Xbox
         * Halo3Cache
         * Halo3Beta
         * Halo3Xbox
         * Halo3ODST
         * HaloReachBeta
         * HaloReach
         */
        public static Dictionary<string, Engine> Engines = new Dictionary<string, Engine>()
        {
            { "Halo2Xbox", new Halo2Xbox() }
        };

        #endregion

        #region Methods

        public static bool DetermineEngineFromMapFile(string fileName, out string engine, out string mapName)
        {
            // Clear our out values
            engine = "";
            mapName = "";

            // Check the file exists
            if (System.IO.File.Exists(fileName) == false)
            {
                // Output error and return false
                Console.WriteLine("Error file \"{0}\" not found!", fileName);
                return false;
            }

            // Create a new endian reader
            IO.EndianReader br = new IO.EndianReader(IO.Endianness.Little, new System.IO.FileStream(fileName, 
                System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read));

            // Check for the 'head' or 'daeh' identifier
            bool result = false;
            int headMagic = br.ReadInt32();
            if (headMagic == /* 'head' */ 0x64616568)
            {
                // Big endian map, ex halo 3
            }
            else if (headMagic == /* 'daeh' */ 0x68656164)
            {
                // Check engine version
                int engineVersion = br.ReadInt32();
                if (engineVersion == 8)
                {
                    // Halo 2 map, but we need to check the build date to determine which version of halo 2
                    br.BaseStream.Position = 288;
                    if (br.ReadInt32() == 0)
                    {
                        // There is no build date at 288, advance to 300 and check if it is a halo 2 vista map
                        br.BaseStream.Position = 300;
                    }
                    else
                    {
                        // Parse the whole build date so we can check it
                        br.BaseStream.Position = 288;
                        string buildDate = new string(br.ReadChars(32)).Replace("\0", "");

                        // Check if the map is a final or beta build map
                        if (buildDate.Equals("02.09.27.09809") == true)
                        {
                            // It's a final halo 2 xbox map
                            engine = "Halo2Xbox";

                            // Read the map name
                            br.BaseStream.Position = 408;
                            mapName = new string(br.ReadChars(32)).Replace("\0", "");

                            // Done
                            result = true;
                        }
                        else if (buildDate.Equals("02.06.28.07902") == true)
                        {
                            // It's a halo 2 beta map
                            engine = "Halo2Beta";

                            // Read the map name
                            br.BaseStream.Position = 408;
                            mapName = new string(br.ReadChars(32)).Replace("\0", "");

                            // Done
                            result = true;
                        }
                    }
                }
            }

            // Close the reader
            br.Close();

            // Done
            return result;
        }

        #endregion
    }
}