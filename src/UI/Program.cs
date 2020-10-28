using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AITool
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]


        static void Main()
        {

            //To prevent more than one copy running in memory, all trying to access same log and settings files
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Length > 1)
            {
                //MessageBox.Show("Another instance of this program is already running.", "Warning!");
                //return;
                AppSettings.AlreadyRunning = true;
            }

            AppSettings.LastShutdownState = Global.GetSetting("LastShutdownState", "not set");
            AppSettings.LastLogEntry = Global.GetSetting("LastLogEntry", "not set");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Shell());
            Shell frmshell = new Shell();
            //frmshell.WindowState = FormWindowState.Minimized;
            //frmshell.ShowInTaskbar = false;
            Application.Run(frmshell);

        }
    }
}
