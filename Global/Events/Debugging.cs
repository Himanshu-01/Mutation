using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace Global.Events
{
    public class ConsoleCommand
    {
        public string Command;
        public string[] Arguments;

        public delegate void ExecuteEventHandler(params object[] args);
        public event ExecuteEventHandler Execute;

        /// <summary>
        /// Gets a boolean value indicating if this console command has a
        /// valid default event handler associated with it, or if it must
        /// be handled externally.
        /// </summary>
        public bool HasDefaultHandler
        {
            get { return (Execute != null); }
        }

        public ConsoleCommand(string cmd, params string[] args)
        {
            // Set fields
            Command = cmd;
            Execute = null;
            Arguments = args;
        }

        public ConsoleCommand(string cmd, ExecuteEventHandler onExecuteHandler, params string[] args)
        {
            // Set fields
            Command = cmd;
            Execute = onExecuteHandler;
            Arguments = args;
        }

        public void OnExecute(params object[] args)
        {
            // Check if our event handler is null
            if (Execute != null)
                Execute(args);
        }

        public override string ToString()
        {
             // Build command line string
            string cmd = Command;
            for (int i = 0; i < Arguments.Length; i++)
                cmd += string.Format(" {0}", Arguments[i]);

            // Done
            return cmd;
        }
    }

    public class Debugging
    {
        #region Imports

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        #endregion

        #region Constants

        public static readonly ConsoleCommand[] ConsoleCommands = new ConsoleCommand[]
        {
            new ConsoleCommand("memstat", new ConsoleCommand.ExecuteEventHandler(MemStat)),
            new ConsoleCommand("open_project", "<String file_name>"),
            new ConsoleCommand("get_str_const", "<String str_const>")
        };

        #endregion

        #region Methods

        public static bool OpenConsole()
        {
            // Allocate a console and associate it with this process
            if (AllocConsole() == 0)
                return false;

            // Print welcome banner
            Console.WriteLine("**********************************************************");
            Console.WriteLine("* Mutation Debug Console v1.0.0 - " + DateTime.Now.ToString() +
                (DateTime.Now.Hour == 10 || DateTime.Now.Hour == 11 || DateTime.Now.Hour == 12 ? " *" : " *"));
            Console.WriteLine("**********************************************************\r\n");

            // Check if there are default user settings for the console window
            if (Global.Application.Instance.Settings.ConsoleHasCustomSettings)
            {
                // Get the console window handle
                IntPtr hConsole = GetConsoleWindow();

                // Get the second monitor Screen object
                Screen screen = Screen.AllScreens[1];

                // Set the new position and size of the console window
                SetWindowPos(hConsole, 0, Global.Application.Instance.Settings.DefaultConsoleLocation.X, 
                    Global.Application.Instance.Settings.DefaultConsoleLocation.Y,
                    Global.Application.Instance.Settings.DefaultConsoleSize.Width, 
                    Global.Application.Instance.Settings.DefaultConsoleSize.Height, 0x0040);
            }

            // Done
            return true;
        }

        public static string[] SplitCommandLine(string command)
        {
            // Declare a list to add all the cmd parts to
            List<String> parts = new List<string>();

            // Loop through the command string and parse each argument
            int i = 0;
            string arg = "";
            bool isMidArg = false;
            while (i != command.Length)
            {
                // Check the next character
                char c = command[i++];
                if (c == ' ' && isMidArg == false)
                {
                    // If the arg is valid add it to the list
                    if (arg != "")
                        parts.Add(arg);

                    // Reset arg
                    arg = "";
                }
                else if (c == '\"')
                {
                    // If we were in mid arg, add it to the list and reset
                    if (isMidArg)
                    {
                        // Add to list and reset
                        parts.Add(arg);
                        isMidArg = false;
                        arg = "";
                    }
                    else
                        isMidArg = true;
                }
                else
                {
                    // Just a standard character
                    arg += c;

                    // Check if we are done
                    if (i == command.Length)
                        parts.Add(arg);
                }
            }

            // Done
            return parts.ToArray();
        }

        public static bool ValidateCommand(string command)
        {
            // Split the command string into pieces so we can analyze it
            string[] cmdlist = SplitCommandLine(command);

            // Check the length of the command
            if (cmdlist.Length == 0)
                return false;

            // Loop through all the console commands and
            for (int i = 0; i < ConsoleCommands.Length; i++)
            {
                // Check if the console command matches
                if (ConsoleCommands[i].Command.Equals(cmdlist[0]))
                {
                    // Check to make sure we have the correct amount of arguments for this command
                    if (cmdlist.Length - 1 != ConsoleCommands[i].Arguments.Length)
                    {
                        // Invalid argument list
                        Console.WriteLine("invalid arguments for command \'{0}\'\n\n{1}",
                            cmdlist[0], ConsoleCommands[i].ToString());
                        return false;
                    }

                    // The command should be proper enough to execute
                    return true;
                }
            }

            // The console command was not found
            Console.WriteLine("\'{0}\' is not a valid command", cmdlist[0]);
            return false;
        }

        public static ConsoleCommand GetCommandEntry(string command)
        {
            // Loop through the command list and search for the command
            for (int i = 0; i < ConsoleCommands.Length; i++)
            {
                // Check if the console command matches
                if (ConsoleCommands[i].Command.Equals(command))
                    return ConsoleCommands[i];
            }

            // It was not found return null
            return null;
        }

        #endregion

        #region Command Handlers

        public static void MemStat(params object[] args)
        {
            // Get the applications process object
            Process proc = Process.GetCurrentProcess();

            // Print memory stats
            Console.WriteLine("\nPhysical Memory: {0} MB", proc.PeakWorkingSet64 / (1024 * 1024));
            Console.WriteLine("Virtual Memory:  {0} MB", proc.PeakVirtualMemorySize64 / (1024 * 1024));
            Console.WriteLine("Virtual Paging:  {0} MB", proc.PeakPagedMemorySize64 / (1024 * 1024));
        }

        #endregion
    }
}
