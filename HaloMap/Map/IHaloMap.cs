using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Global;

namespace HaloMap
{
    public interface IHaloMap
    {
        // Basic Map Function
        void Initialize();
        void Destroy();

        // Info Functions
        string GetEngine();
        string GetName();

        // Compile/Decompile Functions
        void Compile(Global.Data.Project project, System.ComponentModel.BackgroundWorker worker);
        void Decompile(Global.Data.Project project, System.ComponentModel.BackgroundWorker worker);
    }
}
