using System;
using System.Collections.Generic;
using Finisar.SQLite;
using System.Windows.Forms;

namespace SpoilStatus
{
    class DropData
    {
        // We use these three SQLite objects:
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;

        private static DropData instance = null;
        private DropData()
        {
        }
        public static DropData GetInstance()
        {
            if (instance == null)
                instance = new DropData();
            return instance;
        }

        public void OpenDb()
        {
            try
            {
                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source="
                                                   + Environment.CurrentDirectory +
                                                   "\\data\\droplist.db;Version=3;New=False;Compress=False;");

                // open the connection:
                sqlite_conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseDb()
        {
            // We are ready, now lets cleanup and close our connection:
            sqlite_conn.Close();
        }

        public List<Drop> GetDrops(int mobId)
        {
            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            // But how do we read something out of our table ?
            // First lets build a SQL-Query again:
            sqlite_cmd.CommandText = "SELECT * FROM droplist where mobId = " + mobId;

            // Now the SQLiteCommand object can give us a DataReader-Object:
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            List<Drop> droplist = new List<Drop>(10);

            // The SQLiteDataReader allows us to run through the result lines:
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                int itemId = Convert.ToInt32(sqlite_datareader["itemId"]);
                int min = Convert.ToInt32(sqlite_datareader["min"]);
                int max = Convert.ToInt32(sqlite_datareader["max"]);
                int category = Convert.ToInt32(sqlite_datareader["category"]);
                int chance = Convert.ToInt32(sqlite_datareader["chance"]);
                
                droplist.Add(new Drop(mobId, itemId, min, max, category, chance));
            }
            return droplist;
        }
    }
}
