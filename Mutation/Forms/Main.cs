using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress;
using DevExpress.XtraBars.Ribbon;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Docking;
using HaloControls;
using DevExpress.XtraTab;
using Global;
using System.Diagnostics;
using System.IO;
//using Portal;
using System.Threading;
using HaloPlugins;
using HaloMap;
using System.Collections;

namespace Mutation
{
    public partial class Main : RibbonForm
    {
        CopyStruct copydata;

        BackgroundWorker consoleWorker;

        public Updater Updater { get; set; }

        public Main(string[] Arguments)
        {
            // Initialize the form.
            InitializeComponent();

            // Do engine initialization.
            Blam.TagFiles.TagGroups._InitializeStringIdTables();
        }

        #region Form Button Code

        private void Main_Load(object sender, EventArgs e)
        {
            // Get the Date for Easter Eggs :)
            if (DateTime.Today.Month == 12 && DateTime.Today.Day == 25)
            {
                // X-mas
                this.defaultLookAndFeel1.LookAndFeel.SkinName = "Xmas 2008 Blue";
                this.defaultLookAndFeel1.LookAndFeel.SetStyle(LookAndFeelStyle.Skin, false, false);
            }
            else if (DateTime.Today.Month == 10 && DateTime.Today.Day == 31)
            {
                // Halloween
                this.defaultLookAndFeel1.LookAndFeel.SkinName = "Pumpkin";
                this.defaultLookAndFeel1.LookAndFeel.SetStyle(LookAndFeelStyle.Skin, false, false);
            }
            else if (DateTime.Today.Month == 2 && DateTime.Today.Day == 14)
            {
                // Valintines
                this.defaultLookAndFeel1.LookAndFeel.SkinName = "Valentine";
                this.defaultLookAndFeel1.LookAndFeel.SetStyle(LookAndFeelStyle.Skin, false, false);
            }

            // Initialize our event handler callbacks
            Global.Application.Instance.UserInterface.LoadProject += new Global.Events.UserInterface.LoadProjectHandler(UserInterface_LoadProject);
            Global.Application.Instance.UserInterface.UnloadProject += new Global.Events.UserInterface.UnloadProjectHandler(UserInterface_UnloadProject);
            Global.Application.Instance.UserInterface.PromptSaveProject += new Global.Events.UserInterface.PromptSaveHandler(UserInterface_PromptSaveProject);

            // Init event handlers
            Global.EventHandler.OutputMessage_Event += new Global.EventHandler.OutputMessage_Handler(MessagesWriteLine);
            Global.EventHandler.OpenTag_Event += new Global.EventHandler.OpenTag_Handler(OpenTag);
            Global.EventHandler.GetCopyData_Event += new Global.EventHandler.GetCopyData_Handler(EventHandler_GetCopyData_Event);
            Global.EventHandler.SetCopyData_Event += new Global.EventHandler.SetCopyData_Handler(EventHandler_SetCopyData_Event);

            // Setup the treeview comparer class
            treeView1.TreeViewNodeSorter = new TreeViewNodeSorter();

            // Set the width of the dock the treeview is in.
            int width = (this.Width / 4);
            this.dckProjectExplorer.Size = new Size(width, this.dckProjectExplorer.Size.Height);

            // Set Output
            MessagesWriteLine("Welcome To Mutation");
            this.Text = "Mutation - " + Global.Application.Instance.UserAccount.UserName;

            // Initialize the console worker if the console is open
            if (Global.Application.Instance.Settings.EnableDebugConsole)
            {
                // Initialize the console worker to poll for commands
                consoleWorker = new BackgroundWorker();
                consoleWorker.WorkerReportsProgress = true;
                consoleWorker.WorkerSupportsCancellation = true;
                consoleWorker.DoWork += new DoWorkEventHandler(consoleWorker_DoWork);
                consoleWorker.ProgressChanged += new ProgressChangedEventHandler(consoleWorker_ProgressChanged);
                consoleWorker.RunWorkerAsync();
            }
        }

        void UserInterface_LoadProject()
        {
#if (GUI_BENCHMARK)

            // Create a stopwatch so we can time the opperation
            Stopwatch watch = new Stopwatch();
            watch.Start();

#endif

            /*
             * Suspend the treeview to save some rendering time.
             * 
             * For what ever reason, this actually takes longer than just adding the nodes
             * without suspending the treeview.
             */
            //treeView1.BeginUpdate();

            // Clear any old treeview nodes
            treeView1.Nodes.Clear();

            // Create a root node with the project name
            treeView1.Nodes.Add("root", Global.Application.Instance.Project.Name, 0);

            // Loop and add every tag path to the treeview
            for (int i = 0; i < Global.Application.Instance.Project.Tags.Count; i++)
            {
                // Split the path into a folder heirarchy
                string[] Paths = Global.Application.Instance.Project.Tags[i].Split(new string[] { "\\" }, StringSplitOptions.None);

                // Find the root project node
                TreeNode node = treeView1.Nodes.Find("root", false)[0];

                // Find the Nodes
                for (int x = 0; x < Paths.Length; x++)
                {
                    // Check to see if a node with this name already exists
                    TreeNode[] nodes = node.Nodes.Find(Paths[x], false);
                    if (nodes.Length != 0)
                    {
                        // Descend into the found node
                        node = nodes[0];
                    }
                    else
                    {
                        // There is no node with this name so create a new one
                        TreeNode newNode = new TreeNode(Paths[x]);
                        newNode.Name = Paths[x];

                        // Set the image index for this node
                        if (Paths[x].Contains('.'))
                        {
                            // Node is a file
                            newNode.ImageIndex = 2;
                            newNode.SelectedImageIndex = 2;
                        }
                        else
                            newNode.ImageIndex = 0;

                        // Add this node to the parent node and set it as the new parent
                        node.Nodes.Add(newNode);
                        node = newNode;
                    }
                }
            }

            // Sort the treeview, we need a custom sort handler for this
            treeView1.Sort();

            // Expand the project node
            treeView1.Nodes["root"].Expand();

            // Re-enable treeview drawing.
            //treeView1.EndUpdate();

#if (GUI_BENCHMARK)

            // Stop the watch and display the results
            watch.Stop();
            Console.WriteLine("Treeview load time: {0}", watch.Elapsed.ToString());

#endif

            // Output
            MessagesWriteLine(Global.Application.Instance.Project.Name + " opened");
            this.dckProjectExplorer.Text = "Project Explorer - " + Global.Application.Instance.Project.Name;
        }

        void UserInterface_UnloadProject()
        {
            // Reset the project explorer title
            this.dckProjectExplorer.Text = "Project Explorer";

            // Clear the tree view
            treeView1.Nodes.Clear();

            // Output
            MessagesWriteLine(Global.Application.Instance.Project.Name + " closed");
        }

        void UserInterface_PromptSaveProject()
        {
            // Check if there are any open tags, etc
        }

        private void btnQuickNewTag_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            // Quick New Tag Method
            NewTag t = new NewTag();
            t.ShowInTaskbar = false;
            t.Show();

            // Get the New Class
            string Class = t.GetTag();
            if (Class == "NONE")
                return;
            t.Close();

            // Create new Tab Page
            Stopwatch watch = new Stopwatch();
            watch.Start();
            GrimTabPage tab = new GrimTabPage(Class, true, this.xtraTabControl1);
            this.xtraTabControl1.TabPages.Add(tab);
            this.xtraTabControl1.SelectedTabPageIndex = this.xtraTabControl1.TabPages.Count - 1;

            // Output
            watch.Stop();
            MessageBox.Show(watch.Elapsed.ToString());
            textBox1.Text += Class + " Tag Created\r\n";
             * */
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            try { xtraTabControl1.SelectedTabPage.Dispose(); } catch { }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Double Check our Tags are Saved
            //for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
            //{
            //    xtraTabControl1.TabPages[i].Dispose();
            //}
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Check for an open project and if one is open close it
            if (Global.Application.Instance.IsProjectOpen)
                Global.Application.Instance.CloseProject(true);

            // Create the new project dialog
            NewProjectDialog newProject = new NewProjectDialog(Global.Data.Project.ProjectType.Decompile);
            newProject.ShowInTaskbar = false;
            newProject.ShowDialog(this);

            // Check if the project was created
            if (Global.Application.Instance.IsProjectOpen)
            {
                // Handle project types
                if (Global.Application.Instance.Project.Type == Global.Data.Project.ProjectType.Decompile)
                {
                    // Suspend the console worker
                    SuspendConsoleWorker();

                    // Setup project
                    string Path = Global.Application.Instance.Project.ProjectFiles["MapFileName"];
                    Global.Application.Instance.Project.ProjectFiles.Clear();

                    // Initialize progress bar
                    Updater = new Updater("Initializing...", 0, Path);
                    if (Updater.ShowDialog() == DialogResult.OK)
                    {
                        if (MessageBox.Show("Would you like to open your new project?", "Project Explorer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Load
                            UserInterface_LoadProject();
                        }
                    }

                    // Resume the console worker
                    ResumeConsoleWorker();
                }
            }
        }

        private void btnNewProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Suspend the console worker
            SuspendConsoleWorker();

            // Create the new project dialog
            NewProjectDialog newProject = new NewProjectDialog(Global.Data.Project.ProjectType.None);
            newProject.ShowInTaskbar = false;

            // Display the new project dialog and check the result
            if (newProject.ShowDialog(this) != DialogResult.OK)
                return;

            // Check if the project was created
            if (Global.Application.Instance.IsProjectOpen)
            {
                // Handle project types
                if (Global.Application.Instance.Project.Type == Global.Data.Project.ProjectType.Decompile)
                {
                    // Setup project
                    string Path = Global.Application.Instance.Project.ProjectFiles["MapFileName"];
                    Global.Application.Instance.Project.ProjectFiles.Clear();

                    // Initialize progress bar
                    Updater = new Updater("Initializing...", 0, Path);
                    if (Updater.ShowDialog() == DialogResult.OK)
                    {
                        if (MessageBox.Show("Would you like to open your new project?", "Project Explorer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Load
                            UserInterface_LoadProject();
                        }
                    }
                }
            }

            // Resume the console worker
            ResumeConsoleWorker();
        }

        private void btnOpenTag_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Open
                OpenTag(ofd.FileName);
            }
        }

        private void btnOpenProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Check for Open Project
            if (Global.Application.Instance.IsProjectOpen)
            {
                MessageBox.Show("Please close the already opened project.");
                return;
            }

            // Suspend the console worker
            SuspendConsoleWorker();

            // Open
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Halo 2 Projects (*.hpr)|*.hpr";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Init Project
                if (Global.Application.Instance.OpenProject(ofd.FileName, true) == true)
                {
                }
                // TODO: else, error message?
            }

            // Resume the console worker
            ResumeConsoleWorker();
        }

        private void btnQuickSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Settings s = new Settings();
            s.Show();

            while (s.Visible)
                System.Windows.Forms.Application.DoEvents();

            //this.Text = "Mutation - " + Global.User.Alias;
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Double Check our Tags are Saved
            for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
            {
                xtraTabControl1.TabPages[i].Dispose();
            }

            System.Windows.Forms.Application.Exit();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //((GrimTabPage)xtraTabControl1.SelectedTabPage).Dispose();
                //xtraTabControl1.SelectedTabPageIndex = xtraTabControl1.TabPages.Count - 1;
            }
            catch { }
        }

        private void btnCloseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
            //{
            //    try
            //    {
            //        if (xtraTabControl1.TabPages[0].GetType() == typeof(XtraTabPage))
            //        {
            //            //((TagEditorPage)xtraTabControl1.TabPages[1]).Dispose();
            //        }
            //        else
            //        {
            //            //((TagEditorPage)xtraTabControl1.TabPages[0]).Dispose();
            //        }
            //    }
            //    catch { }
            //}
        }

        private void btnCloseProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Global.Application.Instance.IsProjectOpen)
            {
                // Close Tabs
                btnCloseAll_ItemClick(null, null);

                // Refresh TreeView
                treeView1.Nodes.Clear();
                dckProjectExplorer.Text = "Project Explorer";
                MessagesWriteLine(Global.Application.Instance.Project.Name + " closed");
                
                // Close the project internally
                Global.Application.Instance.CloseProject(true);
            }
        }

        private void btnCompile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Global.Application.Instance.IsProjectOpen)
            {
                // Suspend the console worker
                SuspendConsoleWorker();

                // Initialize progress bar
                Updater = new Updater("Initializing...", 0);
                Updater.ShowDialog();

                // Resume the console worker
                ResumeConsoleWorker();
            }
        }

        #endregion

        #region Treeview Specific Code

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            treeView1.SelectedNode.ImageIndex = 1;
            treeView1.SelectedNode.SelectedImageIndex = 1;
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            treeView1.SelectedNode.ImageIndex = 0;
            treeView1.SelectedNode.SelectedImageIndex = 0;
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Make sure this node is a tag and not a folder
            if (treeView1.SelectedNode.Text.Contains('.'))
            {
                // Suspend the console worker
                SuspendConsoleWorker();

                // Get the full tag path and remove the project name from it
                string tagPath = treeView1.SelectedNode.FullPath.Replace(Global.Application.Instance.Project.Name + "\\", "");

                // 
                this.xtraTabControl1.SuspendLayout();

                // Create a new tab page and add it to the tab view.
                TagEditorPage tab = new TagEditorPage();
                this.xtraTabControl1.TabPages.Add(tab);

                this.xtraTabControl1.SelectedTabPageIndex = this.xtraTabControl1.TabPages.Count - 1;

                // 
                this.xtraTabControl1.ResumeLayout(true);

                // Setup the new tab page with the tag we are opening.
                tab.Open(Global.Application.Instance.Project.TagFolder + tagPath);

                // Do we need the Portal
                /*if (treeView1.SelectedNode.FullPath.EndsWith(".render_model") ||
                    treeView1.SelectedNode.FullPath.EndsWith(".decorator") ||
                    treeView1.SelectedNode.FullPath.EndsWith(".cloth") ||
                    treeView1.SelectedNode.FullPath.EndsWith(".collision_model"))
                    LoadPortal();*/

                // Output
                MessagesWriteLine(string.Format("\"{0}\" opened", tagPath));

                // Resume the console worker
                ResumeConsoleWorker();
            }
        }

        #endregion

        #region Feedback Code

        private void btnComment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Feedback f = new Feedback(0);
            f.ShowDialog();
        }

        private void btnSubmitBug_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Feedback f = new Feedback(1);
            f.ShowDialog();
        }

        #endregion

        #region Console Worker

        void SuspendConsoleWorker()
        {
            // Cancel the console worker so we can process something else
            consoleWorker.CancelAsync();

            // Get the console window handle
            IntPtr hConsole = Global.Events.Debugging.GetConsoleWindow();

            // Post a key down message to the console so we can kill the console worker for good
            Global.Events.Debugging.PostMessage(hConsole, /*WM_KEYDOWN*/ 0x100, /*VK_RETURN*/ 0x0D, 0);

            // Wait until the worker is done
            while (consoleWorker.IsBusy)
                System.Windows.Forms.Application.DoEvents();

            // Clear the last carret if there was one
            Console.Write("\r \r");
        }

        void ResumeConsoleWorker()
        {
            // Restart the console worker
            consoleWorker.RunWorkerAsync();
        }

        void consoleWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the background worker that owns this thread
            BackgroundWorker worker = (BackgroundWorker)sender;

            // Loop until we have a valid command
            while (true)
            {
                // Poll for a console command
                Console.Write("\n>");
                string cmd = Console.ReadLine();

                // Check for cancel before we parse the command
                if (worker.CancellationPending == true)
                {
                    // Cancel
                    e.Cancel = true;
                    return;
                }

                // Validate the command to make sure its in the proper format
                if (Global.Events.Debugging.ValidateCommand(cmd) == true)
                {
                    // Report the command to the upper level ui thread
                    worker.ReportProgress(0, cmd);
                    
                    // Cancel the current opperation
                    e.Cancel = true;
                    return;
                }
            }
        }

        void consoleWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Get the background worker that owns this thread
            BackgroundWorker worker = (BackgroundWorker)sender;

            // Wait until the console worker has canceled the opperation
            while (worker.IsBusy)
                System.Windows.Forms.Application.DoEvents();

            // Split the command so we can parse the info from it
            string[] cmd = Global.Events.Debugging.SplitCommandLine((string)e.UserState);

            // Pull out the console command structure
            Global.Events.ConsoleCommand command = Global.Events.Debugging.GetCommandEntry(cmd[0]);

            // Check if it has a default event handler
            if (command.HasDefaultHandler)
            {
                // Call the default event handler
                command.OnExecute(cmd);
            }
            else
            {
                // Handle the command manually
                switch (cmd[0])
                {
                    #region open_project

                    case "get_str_const":
                        {
                            Blam.Objects.DataTypes.string_id handle = Blam.Objects.DataTypes.string_id._string_id_empty;
                            Blam.Engine.Engine.g_string_id_globals.ContainsValue(cmd[1], ref handle);

                            if (handle == Blam.Objects.DataTypes.string_id._string_id_empty)
                                Console.WriteLine("not found");
                            else
                                Console.WriteLine("0x{0} {1}", handle.Handle.ToString("x"), cmd[1]);
                            break;
                        }

                    case "open_project":
                        {
                            // Check for Open Project
                            if (Global.Application.Instance.IsProjectOpen)
                            {
                                // Report error
                                Console.WriteLine("error only one project may be open at a time!");
                                break;
                            }

                            // Check to make sure the file exists
                            if (!File.Exists(cmd[1]))
                            {
                                // Report error
                                Console.WriteLine("error file \'{0}\' does not exist!", cmd[1]);
                                break;
                            }

                            // Init Project
                            Global.Application.Instance.OpenProject(cmd[1], true);

                            // Output
                            MessagesWriteLine(cmd[1] + " opened");
                            this.dckProjectExplorer.Text = "Project Explorer - " + Global.Application.Instance.Project.Name;
                            break;
                        }

                    #endregion
                }
            }

            // Resume the console worker thread
            worker.RunWorkerAsync();
        }

        #endregion

        public void LoadPortal()
        {
            // Add Control
            //controlContainer1.Controls.Clear();
            //controlContainer1.Controls.Add(new PortalPanel(Global.Globals.p.Directory + treeView1.SelectedNode.FullPath, false));

            // Start Rendering
            //((PortalPanel)controlContainer1.Controls[0]).BeginRender();
        }

        #region Event Handlers

        public void MessagesWriteLine(string Message)
        {
            // Write the message and a line break
            txtMessages.Text += Message + "\r\n";
        }

        public void OpenTag(string FileName)
        {
            // Create new Tab Page
            TagEditorPage tab = new TagEditorPage();
            tab.Open(FileName);
            this.xtraTabControl1.TabPages.Add(tab);
            this.xtraTabControl1.SelectedTabPageIndex = this.xtraTabControl1.TabPages.Count - 1;

            // Output
            MessagesWriteLine(FileName + " opened");
        }

        void EventHandler_SetCopyData_Event(CopyStruct CopyData)
        {
            copydata = CopyData;
        }

        CopyStruct EventHandler_GetCopyData_Event()
        {
            return copydata;
        }

        void EventHandler_UpdateProgress_Event(string Status, string Info, int Max, int Step)
        {
            Updater.Update(Status, Info, Max, Step);
        }

        void EventHandler_StepProgress_Event(string Info)
        {
            if (Info != "")
                Updater.Step(Info);
            else
                Updater.Step();
        }

        #endregion

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage != null && this.xtraTabControl1.SelectedTabPage.Text != "Main")
                ((TagEditorPage)this.xtraTabControl1.SelectedTabPage).Save();
        }
    }

    #region TreeView Sorter

    class TreeViewNodeSorter : IComparer
    {

        #region IComparer Members

        public int Compare(object x, object y)
        {
            // Convert both parameters to TreeNode
            TreeNode node1 = (TreeNode)x;
            TreeNode node2 = (TreeNode)y;

            // Check if they both have children
            if (node1.Nodes.Count == 0 && node2.Nodes.Count == 0 ||
                (node1.Nodes.Count != 0 && node2.Nodes.Count != 0))
            {
                // Sort by alphabetic
                return node1.Text.CompareTo(node2.Text);
            }
            else if ((node1.Nodes.Count == 0 && node2.Nodes.Count != 0) || 
                (node2.Nodes.Count == 0 && node1.Nodes.Count != 0))
                return node2.Nodes.Count - node1.Nodes.Count;

            return 0;
        }

        #endregion
    }

    #endregion
}
