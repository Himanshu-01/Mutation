using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace Portal
{
    public class RawData
    {
        private static string MainMenu, Shared, SinglePlayerShared;
        private static bool Initialized = false;

        private static void ReadSettings()
        {
            try
            {
                XmlTextReader xr = new XmlTextReader(Application.StartupPath + "\\Settings.xml");
                while (xr.Read())
                {
                    switch (xr.Name)
                    {
                        case "MainMenu":
                            {
                                MainMenu = xr.ReadElementString();
                                Shared = xr.ReadElementString();
                                SinglePlayerShared = xr.ReadElementString();
                                break;
                            }
                    }
                }
                xr.Close();
                Initialized = true;
            }
            catch { }
        }

        public static BinaryReader OpenMap(ref int Offset)
        {
            // Initialize
            if (!Initialized)
                ReadSettings();

            // Open Map
            long Map = Offset & 0xC0000000;
            Offset &= 0x3FFFFFFF;
            switch (Map)
            {
                case 0x80000000:
                    {
                        return new BinaryReader(new FileStream(Shared, FileMode.Open, FileAccess.Read, FileShare.Read));
                    }
                case 0xC0000000:
                    {
                        return new BinaryReader(new FileStream(SinglePlayerShared, FileMode.Open, FileAccess.Read, FileShare.Read));
                    }
                case 0x40000000:
                    {
                        return new BinaryReader(new FileStream(MainMenu, FileMode.Open, FileAccess.Read, FileShare.Read));
                    }
            }
            return null;
        }
    }
}
