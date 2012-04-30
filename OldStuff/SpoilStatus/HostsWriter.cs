using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SpoilStatus
{
    public class HostsWriter
    {
        private static HostsWriter instance = null;

        private FileStream hostFile;
        private string lastIp;
        private string lastHostname;

        private HostsWriter()
        {
        }

        public static HostsWriter GetInstance()
        {
            if (HostsWriter.instance == null)
                HostsWriter.instance = new HostsWriter();
            return HostsWriter.instance;
        }

        public void AddEntry(string ip, string hostname, string comment)
        {
            this.lastIp = ip;
            this.lastHostname = hostname;
            hostFile = new FileStream(Environment.SystemDirectory + @"\drivers\etc\hosts", FileMode.Open);
            StreamReader hosts = new StreamReader(hostFile);
            // A StringBuilder for the new hosts
            StringBuilder newHost = new StringBuilder((int)hostFile.Length + 255);
            bool entryExists = false;

            while (!hosts.EndOfStream)
            {
                string line = hosts.ReadLine();
                if (line.ToLower().Contains(hostname.ToLower()))
                {
                    line = ip + "\t" + hostname + "\t" + comment;
                    entryExists = true;
                }
                newHost.AppendLine(line);
            }
            if (!entryExists)
            {
                newHost.AppendLine(ip + "\t" + hostname + "\t" + comment);
            }
            StreamWriter wrt = new StreamWriter(hostFile);
            wrt.Write(newHost.ToString());
            hostFile.Close();
        }

        public void RemoveEntry(string hostname)
        {
            hostFile = new FileStream(Environment.SystemDirectory + @"\drivers\etc\hosts", FileMode.Open);
            StreamReader hosts = new StreamReader(hostFile);
            // A StringBuilder for the new hosts
            StringBuilder newHost = new StringBuilder((int)hostFile.Length);

            while (!hosts.EndOfStream)
            {
                string line = hosts.ReadLine();
                if (line.ToLower().Contains(hostname.ToLower()))
                {
                    continue;
                }
                newHost.AppendLine(line);
            }
            StreamWriter wrt = new StreamWriter(hostFile);
            wrt.Write(newHost.ToString());
            hostFile.Close();
        }

        public bool CheckLastEntry()
        {
            bool success = false;
            System.Net.IPAddress[] ips =  System.Net.Dns.GetHostAddresses("localhost");
            System.Net.IPAddress lastAdr = System.Net.IPAddress.Parse("127.0.0.1");
            foreach (System.Net.IPAddress adr in ips)
            {
                success = adr.Equals(lastAdr);
            }
            return success;
        }
    }
}
