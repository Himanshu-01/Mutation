using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global.Events
{
    public class UserInterface
    {
        public delegate void LoadProjectHandler();
        public event LoadProjectHandler LoadProject;

        public delegate void UnloadProjectHandler();
        public event UnloadProjectHandler UnloadProject;

        public delegate void PromptSaveHandler();
        public event PromptSaveHandler PromptSaveProject;

        public void OnLoadProject()
        {
            // Check for a valid event handler routine
            if (LoadProject != null)
                LoadProject();
        }

        public void OnUnloadProject()
        {
            // Check for a valid event handler routine
            if (UnloadProject != null)
                UnloadProject();
        }

        public void OnPromptSaveProject()
        {
            // Check for a valid event handler routine
            if (PromptSaveProject != null)
                PromptSaveProject();
        }
    }
}
