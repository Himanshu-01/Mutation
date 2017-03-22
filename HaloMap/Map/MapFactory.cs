using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaloMap
{
    public class MapFactory
    {
        public static IHaloMap GetMap(string File)
        {
            return new H2.Xbox.HaloMap(File);
            //return new H1.Xbox.HaloMap(File);
        }

        public static IHaloMap GetMapFromEngine(string Engine)
        {
            if (Engine == "Halo2Xbox")
                return new H2.Xbox.HaloMap();
            else
                return null;
        }
    }
}
