using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaloPlugins
{
    public class Engine
    {
        // Indexers
        public virtual object this[string Type] { get { return null; } set { } }
        public virtual string this[string Type, string Value] { get { return null; } }

        // Methods
        public virtual TagDefinition CreateInstance(string Value) { return null; }
    }
}
