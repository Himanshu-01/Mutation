using HaloPlugins;
using LayoutViewer.CodeDOM;
using LayoutViewer.Guerilla;
using Mutation.Halo.TagGroups.Attributes;
using Mutation.HEK.Common;
using Mutation.HEK.Common.TagFieldDefinitions;
using Mutation.HEK.Guerilla;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutViewer.Forms
{
    public partial class PluginConverter : Form
    {
        // Background worker to handle the plugin conversion.
        BackgroundWorker conversionWorker;

        public PluginConverter()
        {
            InitializeComponent();
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            // Initialize a folder browse dialog to save the new plugins to.
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Output folder";

            // Prompt the user to select an output folder.
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Set the folder path to the textbox.
                this.txtOutputFolder.Text = fbd.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Check if the conversion worker is currently running.
            if (this.conversionWorker != null && this.conversionWorker.IsBusy == true)
            {
                // Cancel the conversion operation.
                this.conversionWorker.CancelAsync();
            }
            else
            {
                // Close the dialog.
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            // Check that the data source has been selected.
            if (this.cmbSource.SelectedIndex == -1)
            {
                // Prompt the user to select a data source.
                MessageBox.Show("Please select a data source before starting a conversion!", 
                    "Conversion Error", MessageBoxButtons.OK);
                return;
            }

            // Check if the output folder has been set.
            if (this.txtOutputFolder.Text == "")
            {
                // Prompt the user to select an output folder.
                MessageBox.Show("Please select an output folder before starting a conversion!",
                    "Conversion Error", MessageBoxButtons.OK);
                return;
            }

            // Check if there is a conversion already running.
            if (this.conversionWorker != null && this.conversionWorker.IsBusy == true)
            {
                // This should never happen, but just warn the user and return.
                MessageBox.Show("A previous conversion is still running, please wait until it finishes before starting another!", 
                    "Conversion Error", MessageBoxButtons.OK);
                return;
            }

            // Initialize the conversion worker.
            this.conversionWorker = new BackgroundWorker();
            this.conversionWorker.WorkerReportsProgress = true;
            this.conversionWorker.WorkerSupportsCancellation = true;
            this.conversionWorker.ProgressChanged += conversionWorker_ProgressChanged;
            this.conversionWorker.RunWorkerCompleted += conversionWorker_RunWorkerCompleted;

            // Set the worker function based on the data source.
            if (this.cmbSource.SelectedIndex == 0)
                this.conversionWorker.DoWork += ConvertGuerillaPlugins;
            else
                this.conversionWorker.DoWork += ConvertMutationPlugins;

            // Disable the conversion button.
            this.btnConvert.Enabled = false;

            // Run the conversion.
            this.conversionWorker.RunWorkerAsync(this.txtOutputFolder.Text);
        }

        void conversionWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the status label and progress bar.
            this.lblStatus.Text = (string)e.UserState;
            this.progressBar1.Value = e.ProgressPercentage;
        }

        void conversionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Enable the convert button.
            this.btnConvert.Enabled = true;

            // Change the status label and clear the progress bar.
            this.lblStatus.Text = "";
            this.progressBar1.Value = 0;

            // Display a message to the user.
            MessageBox.Show((string)e.Result, "Conversion Results", MessageBoxButtons.OK);
        }

        private void ConvertMutationPlugins(object sender, DoWorkEventArgs e)
        {
            // Cast the sender object to a background worker.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Update the status label.
            worker.ReportProgress(0, "Initializing...");

            // Initialize the mutation plugin repository.
            HaloPlugins.Halo2Xbox h2xPlugins = new HaloPlugins.Halo2Xbox();

            // Loop through all of the plugins and process each one.
            for (int i = 0; i < h2xPlugins.TagExtensions.Length; i++)
            {
                // Create an instance of the current plugin definition.
                TagDefinition definition = h2xPlugins.CreateInstance(h2xPlugins.TagExtensions[i]);
            }
        }

        private void ConvertGuerillaPlugins(object sender, DoWorkEventArgs e)
        {
            // Cast the sender object to a background worker.
            BackgroundWorker worker = sender as BackgroundWorker;
            string outputFolder = e.Argument as string;

            // Update the status label.
            worker.ReportProgress(0, "Reading guerilla...");

            // Initialize a new instance of our guerilla reader and read the definitions from the executable.
            GuerillaReader reader = new GuerillaReader(Application.StartupPath + "\\H2Guerilla.exe");
            if (reader.Read() == false)
            {
                // Failed to read guerilla, return to the calling thread with an error.
                e.Result = "Error reading guerilla.exe!";
                return;
            }

            // Create a new tag layout generator, and process all of the tag layouts.
            TagLayoutGenerator layoutGenerator = new TagLayoutGenerator();
            layoutGenerator.GenerateLayouts(reader, outputFolder);

            // Set the worker result value.
            e.Result = (string)"Completed successfully!";
        }
    }
}
