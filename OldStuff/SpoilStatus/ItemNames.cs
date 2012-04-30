using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpoilStatus
{
    class ItemNames
    {
        /// <summary>
        /// Speichert die Npc informationen
        /// </summary>
        private Dictionary<int, string> items = new Dictionary<int, string>(10700);


        private static ItemNames instance = null;
        private ItemNames()
        {
            this.readItemnameFile();
        }

        /// <summary>
        /// Gibt die Instanz zurück
        /// </summary>
        public static ItemNames GetInstance()
        {
            if (instance == null)
                instance = new ItemNames();
            return instance;
        }

        private void readItemnameFile()
        {
            string filePath = Environment.CurrentDirectory + "\\data\\itemnames.tsv";
            if (!File.Exists(filePath))
            {
                MessageBox.Show(filePath + " existiert nicht.");
#if DEBUG
                Program.debugStream.WriteLine(filePath + " existiert nicht.");
#endif
                return;
            }
            StreamReader npcnameFile = new StreamReader(filePath, Encoding.UTF8);
            do
            {
                string line = npcnameFile.ReadLine();
                if (line.StartsWith("#") || line.StartsWith(" "))
                    continue;
                string[] info = line.Split('\t');

                this.items.Add(int.Parse(info[0]), info[1]);

            } while (!npcnameFile.EndOfStream);
        }

        public string GetItemName(int itemId)
        {
            if (this.items.ContainsKey(itemId))
                return this.items[itemId];
            else
                return "";
        }
    }
}
