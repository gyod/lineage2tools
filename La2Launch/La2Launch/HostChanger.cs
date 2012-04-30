using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace La2Launch
{
    class HostChanger
    {
        private static HostChanger instance = null;

        private FileStream hostFile;
        private string filePath = Environment.SystemDirectory + @"\drivers\etc\hosts";

        public struct hostEntry
        {
            public string ip;
            public string hostname;
            public string comment;
        }

        private HostChanger()
        {
        }

        public static HostChanger GetInstance()
        {
            if (HostChanger.instance == null)
                HostChanger.instance = new HostChanger();
            return HostChanger.instance;
        }

        public void AddEntries(hostEntry[] entry)
        {
            hostFile = new FileStream(filePath, FileMode.Open);
            StreamReader hosts = new StreamReader(hostFile);
            // A StringBuilder for the new hosts
            StringBuilder newHost = new StringBuilder((int)hostFile.Length);

            while (!hosts.EndOfStream)
            {
                string line = hosts.ReadLine();
                bool entyExists = false;
                foreach (hostEntry hEntry in entry)
                {
                    if (line.ToLower().Contains(hEntry.hostname.ToLower()) && !line.StartsWith("#"))
                    {
                        entyExists = true;
                    }
                }
                if (entyExists)
                    continue;
                newHost.AppendLine(line);
            }

            foreach (hostEntry hEntry in entry)
            {
                newHost.AppendLine(hEntry.ip + "\t" + hEntry.hostname + "\t#" + hEntry.comment);
            }

            hostFile.Close();
            StreamWriter wrt = new StreamWriter(filePath, false);
            wrt.Write(newHost.ToString().Trim());
            wrt.Flush();
            wrt.Close();
        }

        public void RemoveEntry(string hostname)
        {
            hostFile = new FileStream(filePath, FileMode.Open);
            StreamReader hosts = new StreamReader(hostFile);
            // A StringBuilder for the new hosts
            StringBuilder newHost = new StringBuilder((int)hostFile.Length);

            while (!hosts.EndOfStream)
            {
                string line = hosts.ReadLine();
                if (line.ToLower().Contains(hostname.ToLower()) && !line.StartsWith("#"))
                {
                    continue;
                }
                newHost.AppendLine(line);
            }
            hostFile.Close();

            StreamWriter wrt = new StreamWriter(filePath, false);
            wrt.Write(newHost.ToString().Trim());
            wrt.Flush();
            wrt.Close();
        }

        public bool CheckLastEntry()
        {
            bool success = false;
            /*IPAddress[] ips =  Dns.GetHostAddresses(this.lastHostname);
            IPAddress lastAdr = IPAddress.Parse(this.lastIp);
            foreach (IPAddress adr in ips)
            {
                success = adr.Equals(lastAdr);
            }*/
            return success;
        }
    }
}
