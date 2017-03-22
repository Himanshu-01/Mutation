using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaloMap;
using Global;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Mutation
{
    public partial class Updater : Form
    {
        BackgroundWorker backgroundWorker1;
        TaskbarManager taskbar;
        int Max = 0, i = 0;
        string MapPath = "";

        public Updater(string Status, int Max)
        {
            InitializeComponent();

            // Set Max
            this.progressBarControl2.Properties.Maximum = Max;
            try
            {
                taskbar = TaskbarManager.Instance;
                taskbar.SetProgressValue(0, Max);
            }
            catch { }
            this.Max = Max;

            // Set Message
            lblStatus.Text = Status;
            lblInfo.Location = new Point(lblStatus.Location.X + lblStatus.Size.Width + 10, lblInfo.Location.Y);

            // Setup background worker
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        public Updater(string Status, int Max, string MapPath)
        {
            InitializeComponent();

            // Set Max
            this.progressBarControl2.Properties.Maximum = Max;
            try
            {
                taskbar = TaskbarManager.Instance;
                taskbar.SetProgressValue(0, Max);
            }
            catch { }
            this.Max = Max;

            // Set Message
            lblStatus.Text = Status;
            lblInfo.Location = new Point(lblStatus.Location.X + lblStatus.Size.Width + 10, lblInfo.Location.Y);

            // Setup background worker
            this.MapPath = MapPath;
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void Updater_Load(object sender, EventArgs e)
        {
            if (MapPath != "")
                backgroundWorker1.RunWorkerAsync(MapPath);
            else
                backgroundWorker1.RunWorkerAsync();
        }

        public void Step()
        {
            // Step
            progressBarControl1.PerformStep();
            if (taskbar != null)
                taskbar.SetProgressValue((i++), Max);
        }

        public void Step(string Info)
        {
            // Set Text
            lblInfo.Text = Info;

            // Step
            progressBarControl1.PerformStep();
            if (taskbar != null)
                taskbar.SetProgressValue((i++), Max);
        }

        public void Update(string Status, string Info, int Max, int Step)
        {
            // Set Progress Properties
            this.progressBarControl1.Properties.Maximum = Max;
            progressBarControl1.Properties.Step = Step;

            // Set Message
            lblStatus.Text = Status;
            lblInfo.Location = new Point(lblStatus.Location.X + lblStatus.Size.Width + 10, lblInfo.Location.Y);
            lblInfo.Text = Info;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (taskbar != null)
                taskbar.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        #region BackgroundWorker

        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Setup operation
            BackgroundWorker worker = (BackgroundWorker)sender;
            if (e.Argument == null)
            {
                // Compile
                IHaloMap map = MapFactory.GetMapFromEngine(Global.Application.Instance.Project.Engine);
                map.Compile(Global.Application.Instance.Project, worker);

                // Done
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                // Initialize
                IHaloMap map = MapFactory.GetMap((string)e.Argument);
                Global.Application.Instance.Project.Engine = map.GetEngine();
                map.Initialize();

                // Decompile
                Global.Application.Instance.Project.Name = map.GetName();
                map.Decompile(Global.Application.Instance.Project, worker);

                // Save project
                Global.Application.Instance.Project.Save(null);

                // Done
                this.DialogResult = DialogResult.OK;
            }
        }

        public void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Done
            Close();
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                object[] objs = (object[])e.UserState;
                if (objs.Length > 1)
                    Update((string)objs[0], (string)objs[1], (int)objs[2], (int)objs[3]);
                else
                    Step((string)objs[0]);
            }
            else
                Step();
        }

        #endregion
    }
}
