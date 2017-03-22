using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Global
{
    public class Application
    {
        // Runtime instance of this class
        private static Application instance = Application.Create();
        /// <summary>
        /// Runtime instance of this class
        /// </summary>
        public static Application Instance { get { return instance; } }

        /// <summary>
        /// Gets or sets the application settings
        /// </summary>
        public Settings.Application Settings 
        {
            get { return Global.Settings.Application.Default; }
        }

        /// <summary>
        /// Gets or sets the default user account for the application
        /// </summary>
        public Accounts.UserAccount UserAccount { get; set; }

        private bool isProjectOpen;
        /// <summary>
        /// Gets a boolean value indicating if a project is currently open or not
        /// </summary>
        public bool IsProjectOpen { get { return isProjectOpen; } }

        /// <summary>
        /// Gets or sets the object for the currently opened project if one is open
        /// </summary>
        public Data.Project Project { get; set; }

        /// <summary>
        /// Gets or sets the global event handler for user interface actions
        /// </summary>
        public Events.UserInterface UserInterface { get; set; }

        Application()
        {
            // Initialize our user account object
            UserAccount = new Global.Accounts.UserAccount();

            // Initialize the project values
            Project = null;
            isProjectOpen = false;

            // Initialize our user interface callback object
            UserInterface = new Global.Events.UserInterface();
        }

        public static Application Create()
        {
            // Create a new instance of this class
            return new Application();
        }

        public bool OpenProject(string fileName, bool loadIntoUI)
        {
            // Check if there is a project already opened and close it
            if (IsProjectOpen)
                CloseProject(true);

            // Try to open the new project and return the result
            Project = Data.Project.Open(fileName);

            // Check if we have a valid project and we are supposed to load it into the ui
            if ((isProjectOpen = Project != null) == true && loadIntoUI == true)
            {
                // Call the ui callback
                UserInterface.OnLoadProject();
            }

            return isProjectOpen;
        }

        public bool CreateProject(string directory, string name, string engine)
        {
            // Check if there is a project already opened and close it
            if (IsProjectOpen)
                CloseProject(true);

            // Create a new project using the info provided
            Project = Data.Project.Create(directory, name, engine);
            isProjectOpen = Project != null;

            // Done
            return isProjectOpen;
        }

        public void CloseProject(bool unloadFromUI)
        {
            // Check if there is an existing project open
            if (!IsProjectOpen)
                return;

            // Prompt the user to save the project
            if (System.Windows.Forms.MessageBox.Show("Would you like to save changes to " + Project.Name + "?", 
                "Save changes", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Call the application save files callback
                UserInterface.OnPromptSaveProject();

                // Save the project
                Project.Save(null);
            }

            // Check if we should unload the project from the ui
            if (unloadFromUI == true)
                UserInterface.OnUnloadProject();

            // Dispose of the old project
            Project = null;

            // Indicate that there current is no project open
            isProjectOpen = false;
        }
    }
}
