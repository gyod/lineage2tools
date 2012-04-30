using System;
using System.Collections.Generic;
using System.Text;
using Tamir.IPLib;
using System.IO;

namespace GetNetworkInfo
{
    class Program
    {
        public static StreamWriter debugStream = new StreamWriter(Environment.CurrentDirectory + "\\Netinfo.txt");

        static void Main(string[] args)
        {
            string ver = Tamir.IPLib.Version.GetVersionString();
            /* Print SharpPcap version */
            debugStream.WriteLine("SharpPcap {0}, Example4.IfListAdv.cs", ver);

            /* Retrieve the device list */
            PcapDeviceList devices = SharpPcap.GetAllDevices();

            if (devices.Count < 1)
            {
                debugStream.WriteLine("No device found on this machine");
                return;
            }

            debugStream.WriteLine();
            debugStream.WriteLine("The following devices are available on this machine:");
            debugStream.WriteLine("----------------------------------------------------");
            debugStream.WriteLine();

            int i = 0;

            /* Scan the list printing every entry */
            foreach (PcapDevice dev in devices)
            {
                /* Description */
                debugStream.WriteLine("{0}) {1}", i, dev.PcapDescription);
                debugStream.WriteLine();
                /* Name */
                debugStream.WriteLine("\tName:\t\t{0}", dev.PcapName);
                /* Is Loopback */
                debugStream.WriteLine("\tLoopback:\t\t{0}", dev.PcapLoopback);

                /* 
                    If the device is a physical network device,
                    lets print some advanced info
                 */
                if (dev is NetworkDevice)
                {//Then..

                    /* Cast to NetworkDevice */
                    NetworkDevice netDev = (NetworkDevice)dev;
                    /* Print advanced info */
                    debugStream.WriteLine("\tIP Address:\t\t{0}", netDev.IpAddress);
                    debugStream.WriteLine("\tSubnet Mask:\t\t{0}", netDev.SubnetMask);
                    debugStream.WriteLine("\tMAC Address:\t\t{0}", netDev.MacAddress);
                    debugStream.WriteLine("\tDefault Gateway:\t{0}", netDev.DefaultGateway);
                    debugStream.WriteLine("\tPrimary WINS:\t\t{0}", netDev.WinsServerPrimary);
                    debugStream.WriteLine("\tSecondary WINS:\t\t{0}", netDev.WinsServerSecondary);
                    debugStream.WriteLine("\tDHCP Enabled:\t\t{0}", netDev.DhcpEnabled);
                    debugStream.WriteLine("\tDHCP Server:\t\t{0}", netDev.DhcpServer);
                    debugStream.WriteLine("\tDHCP Lease Obtained:\t{0}", netDev.DhcpLeaseObtained);
                    debugStream.WriteLine("\tDHCP Lease Expires:\t{0}", netDev.DhcpLeaseExpires);
                    debugStream.WriteLine("\tAdmin Status:\t{0}", netDev.AdminStatus);
                    debugStream.WriteLine("\tMedia State:\t{0}", netDev.MediaState);
                }
                debugStream.WriteLine();
                i++;
            }
            debugStream.Flush();
            debugStream.Close();
        }
    }
}
