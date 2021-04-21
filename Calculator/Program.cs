using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Calculator
{
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        internal static extern bool FreeConsole();
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);
        internal const uint ATTACH_PARENT_PROCESS = 0x0ffffffff;  // default value if not specifing a process ID

        internal const int ERROR_ACCESS_DENIED = 5; // process was already attached to another console
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // run as window app
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                ////
                //// the following should only be used in a non-console application type (C#)
                //// (since a console is allocated/attached already when you define a console app.. :) )
                ////
                //if (!NativeMethods.AttachConsole(NativeMethods.ATTACH_PARENT_PROCESS) && Marshal.GetLastWin32Error() == NativeMethods.ERROR_ACCESS_DENIED)
                //{
                //    // A console was not allocated, so we need to make one.
                //    if (!NativeMethods.AllocConsole())
                //    {
                //        MessageBox.Show("A console could not be allocated, sorry!");
                //        throw new Exception("Console Allocation Failed");
                //    }
                //    else
                //    {
                //        Console.WriteLine("Is Attached, press a key...");
                //        Console.ReadKey(true);
                //        // you now may use the Console.xxx functions from .NET framework
                //        // and they will work as normal
                //    }
                //}

                // run as console app
                NativeMethods.AttachConsole(NativeMethods.ATTACH_PARENT_PROCESS);

                NativeMethods.AllocConsole();
                AnalaizerClass.Analaizer.expression = string.Join("", args);
                string result = AnalaizerClass.Analaizer.Estimate();
                Console.WriteLine(result);
                NativeMethods.FreeConsole();
            }
        }
    }
}
