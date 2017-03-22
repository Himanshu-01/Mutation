using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Mutation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            // Setup some form stuff
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Check if we should load the debug console
            if (Global.Application.Instance.Settings.EnableDebugConsole)
            {
                // Allocate a console
                Global.Events.Debugging.OpenConsole();
            }

            // Login to the default user account if there is one
            //Global.Application.Instance.UserAccount.Login();

            //Blam.TagFiles.TagGroups.TestTagLayouts();

            // Load Splash
            Splash s = new Splash();
            s.Show();

            // Wait
            int start = DateTime.Now.Second;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.Elapsed.Seconds < 3)
                Application.DoEvents();

            // Close
            sw.Stop();
            s.Close();

            //Login Succeded
            DevExpress.UserSkins.BonusSkins.Register();
            Application.Run(new Main(Args));
        }
    }
}
