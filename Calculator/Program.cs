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
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            if (arg.Length == 0)
            {
                // run as window app
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                // run as console app
                try
                {
                    NativeMethods.AllocConsole();
                    AnalaizerClass.Analaizer.expression = arg[0];
                    string tmp = AnalaizerClass.Analaizer.Estimate();
                    Console.WriteLine(tmp);
                    Console.ReadKey();
                    //NativeMethods.FreeConsole();
                }
                catch { }
            }
        }
    }
}
