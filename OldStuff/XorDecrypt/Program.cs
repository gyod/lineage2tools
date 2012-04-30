using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Tamir.IPLib;
using Tamir.IPLib.Packets;
using L2PacketDecrypt.Packets;

namespace L2PacketDecrypt
{
    class Program
    {
        private static L2LoginSniffer client = new L2LoginSniffer();
        private static L2GameSniffer game = new L2GameSniffer();
        private static PacketHandler gpHandler = new PacketHandler();
        private static PacketHandler cpHandler = new PacketHandler();

        private static PacketReassembler reAss = new PacketReassembler();
        private static int clientPort = 0;

        public static void Main(string[] args)
        {
            readFile();
        }

        private static void readFile()
        {
            string capFile
                = @"C:\test2.pcap";//Console.ReadLine();

            PcapDevice device;

            try
            {
                //Get an offline file pcap device
                device = SharpPcap.GetPcapOfflineDevice(capFile);
                //Open the device for capturing
                device.PcapOpen();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            //Register our handler function to the 'packet arrival' event
            device.PcapOnPacketArrival +=
                new SharpPcap.PacketArrivalEvent(device_PcapOnPacketArrival);


            Console.WriteLine();
            Console.WriteLine
                ("-- Capturing from '{0}', hit 'Ctrl-C' to exit...",
                capFile);

            //Start capture 'INFINTE' number of packets
            //This method will return when EOF reached.
            device.PcapCapture(SharpPcap.INFINITE);

            //Close the pcap device
            device.PcapClose();
            Console.WriteLine("-- End of file reached.");
            Console.In.ReadLine();
        }

        private static void sniff()
        {
            string ver = Tamir.IPLib.Version.GetVersionString();
            /* Print SharpPcap version */
            Console.WriteLine("SharpPcap {0}, L2PacketDecrypt", ver);
            Console.WriteLine();

            /* Retrieve the device list */
            PcapDeviceList devices = SharpPcap.GetAllDevices();

            /*If no device exists, print error */
            if (devices.Count < 1)
            {
                Console.WriteLine("No device found on this machine");
                return;
            }

            Console.WriteLine("The following devices are available on this machine:");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();

            int i = 0;

            /* Scan the list printing every entry */
            foreach (PcapDevice dev in devices)
            {
                /* Description */
                Console.WriteLine("{0}) {1}", i, dev.PcapDescription);
                i++;
            }

            Console.WriteLine();
            Console.Write("-- Please choose a device to capture: ");
            i = int.Parse(Console.ReadLine());

            PcapDevice device = devices[i];

            //Register our handler function to the 'packet arrival' event
            device.PcapOnPacketArrival +=
                new SharpPcap.PacketArrivalEvent(device_PcapOnPacketArrival);

            //Open the device for capturing
            //true -- means promiscuous mode
            //1000 -- means a read wait of 1000ms
            device.PcapOpen(true, 1000);

            //tcpdump filter to capture only TCP/IP packets			
            string filter = "port 2106 or port 7777";
            //Associate the filter with this capture
            device.PcapSetFilter(filter);

            Console.WriteLine();
            Console.WriteLine
                ("-- The following tcpdump filter will be applied: \"{0}\"",
                filter);
            Console.WriteLine
                ("-- Listenning on {0}, hit 'Ctrl-C' to exit...",
                device.PcapDescription);

            //Start capture packets
            device.PcapCapture(SharpPcap.INFINITE);

            //Close the pcap device
            //(Note: this line will never be called since
            // we're capturing infinite number of packets
            device.PcapClose();
        }

        private static void device_PcapOnPacketArrival(object sender, Packet packet)
        {
            try
            {
                if (packet is TCPPacket)
                {
                    L2Packet l2packet = null;

                    TCPPacket etherFrame = (TCPPacket)packet;
                    if (etherFrame.Data.Length != 0 && etherFrame.SourcePort == 2106
                        && etherFrame.Ack && etherFrame.Psh) // packet contains data and is from port 2106
                    {
                        l2packet = client.handlePacket(etherFrame.Data, true);
                    }
                    else if (etherFrame.Data.Length != 0 && etherFrame.SourcePort == 7777
                        && etherFrame.Ack && etherFrame.Psh)
                    {
                        //TCPPacket tcpP = reAss.processPacket(etherFrame);
                       // if (tcpP != null)
                        gpHandler.handlePacket(etherFrame.Data);

                        while (gpHandler.BufferQueue.Count > 0)
                        {
                            l2packet = game.handlePacket(gpHandler.BufferQueue.Dequeue(), true);
                        }
                        if (clientPort == 0)
                        {
                            clientPort = etherFrame.DestinationPort;
                        }
                    }
                    else if (etherFrame.Data.Length != 0 && etherFrame.SourcePort == clientPort
                        && etherFrame.Ack && etherFrame.Psh)
                    {
                        cpHandler.handlePacket(etherFrame.Data);
                        while (cpHandler.BufferQueue.Count > 0)
                        {
                            l2packet = game.handlePacket(cpHandler.BufferQueue.Dequeue(), false);
                        }
                    }

                    if (l2packet != null)
                    {
                        Console.Out.WriteLine("Packet is " + l2packet.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.StackTrace);
            }
        }
    }

}