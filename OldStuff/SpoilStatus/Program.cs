using System;
using System.IO;
using System.Windows.Forms;

namespace SpoilStatus
{
    
    static class Program
    {
        // debug
#if DEBUG
        public static StreamWriter debugStream = new StreamWriter(Environment.CurrentDirectory + "\\debug.txt", false);
#endif
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            debugStream.AutoFlush = true;
            debugStream.WriteLine("Starting new Session " + System.DateTime.Now);
#endif

            // read Data
            NpcNames.GetInstance();
            ItemNames.GetInstance();

            DropData.GetInstance().OpenDb();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OptionsForm.Instance.ReadIniFile();

            Application.Run(new Form1());
        }
    }
}