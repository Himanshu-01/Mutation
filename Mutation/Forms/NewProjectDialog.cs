using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaloMap;
using System.Diagnostics;
using Global;

namespace Mutation
{
    public partial class NewProjectDialog : Form
    {
        // Dialog result
        private DialogResult result = DialogResult.Cancel;

        Global.Data.Project.ProjectType type = Global.Data.Project.ProjectType.Standalone;

        public NewProjectDialog(Global.Data.Project.ProjectType type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void NewProjectDialog_Load(object sender, EventArgs e)
        {
            // Set the default result
            result = DialogResult.Cancel;

            // Check the project type to determine the project type
            if (type == Global.Data.Project.ProjectType.Decompile)
                radioGroup1.SelectedIndex = 3;
            else
                radioGroup1.SelectedIndex = 2;
        }

        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (wizardControl1.SelectedPageIndex == 1 && radioGroup1.SelectedIndex == 3)
            {
                //Decompile
                type = Global.Data.Project.ProjectType.Decompile;
               
                // Add some Controls
                wizardPage1.Text = "Decompile";
                wizardPage1.Controls.Clear();
                wizardPage1.Controls.Add(new Decompile());
            }
            else if (wizardControl1.SelectedPageIndex == 1 && radioGroup1.SelectedIndex != 3)
            {
                //New Project
                type = Global.Data.Project.ProjectType.Standalone;
            }
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            // Set the dialog result
            this.DialogResult = result = DialogResult.OK;

            // Check the project type and handle accordingly
            if (type == Global.Data.Project.ProjectType.Decompile)
            {
                // Pull out the file name
                string fileName = ((Decompile)wizardPage1.Controls[0]).txtMap.Text;
                string directory = ((Decompile)wizardPage1.Controls[0]).txtProjectDirectory.Text;

                // Try to determine the engine and map name from the map file
                string engine = "", mapName = "";
                if (HaloPlugins.EngineManager.DetermineEngineFromMapFile(fileName, out engine, out mapName) == true)
                {
                    // Create a new project with the found info
                    Console.WriteLine("Found map file \"{0}\" of engine type \'{1}\'", mapName, engine);
                    Global.Application.Instance.CreateProject(directory, mapName, engine);
                    Global.Application.Instance.Project.Type = Global.Data.Project.ProjectType.Decompile;
                    Global.Application.Instance.Project.ProjectFiles.Add("MapFileName", fileName);
                }
                else
                {
                    // Report error
                    Console.WriteLine("Could not determine engine type for map file \"{0}\"!", fileName);

                    // Show some warning indicating the map is invalid
                }
            }
            else if (type == Global.Data.Project.ProjectType.Standalone)
            {

            }

            // Close the creation dialog
            this.Close();
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel? Your project will not be saved.", "New Project Dialog", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Close the form
                this.Close();
            }
        }
    }
}
