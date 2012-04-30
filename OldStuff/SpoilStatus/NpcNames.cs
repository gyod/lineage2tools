using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace SpoilStatus
{
    /// <summary>
    /// Singelton Class
    /// </summary>
    class NpcNames
    {
        /// <summary>
        /// Speichert die Npc informationen
        /// </summary>
        private readonly Dictionary<int, NpcInfo> npcs = new Dictionary<int, NpcInfo>(8400);


        private static NpcNames instance = null;
        private NpcNames()
        {
            this.readNpcnameFile();
        }

        /// <summary>
        /// Gibt die Instanz zurück
        /// </summary>
        public static NpcNames GetInstance()
        {
            if (instance == null)
                instance = new NpcNames();
            return instance;
        }

        private void readNpcnameFile()
        {
            string filePath = Environment.CurrentDirectory + "\\data\\npcname-e.tsv";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("data\\npcname-e.tsv existiert nicht.");
#if DEBUG
                Program.debugStream.WriteLine("data\\npcname-e.tsv existiert nicht.");
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

                NpcInfo npc;
                npc.name = info[1].Replace('\"', ' ').Trim();
                npc.title = info[2].Replace('\"',' ').Trim();
                npc.color = Color.FromArgb(int.Parse(info[3], System.Globalization.NumberStyles.HexNumber));

                this.npcs.Add(int.Parse(info[0]), npc);

            } while (!npcnameFile.EndOfStream);
        }

        public string GetName(int npcId)
        {
            if (this.npcs.ContainsKey(npcId))
                return this.npcs[npcId].name;
            else
                return npcId.ToString();
        }

        public string GetTitle(int npcId)
        {
            if (this.npcs.ContainsKey(npcId))
                return this.npcs[npcId].title;
            else
                return "";
        }

        public Color GetColor(int npcId)
        {
            if (this.npcs.ContainsKey(npcId))
                return this.npcs[npcId].color;
            else
                return Color.Black;
        }

        private struct NpcInfo
        {
            public string name;
            public string title;
            public Color color;
        }

    }
}
