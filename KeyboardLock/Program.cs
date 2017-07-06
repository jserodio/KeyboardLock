using System;
using System.Windows.Forms;

namespace KeyboardLock
{
    static class Program
    {
        /// <summary>
        /// Main program
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
