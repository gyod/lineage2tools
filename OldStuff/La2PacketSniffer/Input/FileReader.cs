using System;
using System.Collections.Generic;
using System.Text;
using Tamir.IPLib;
using Tamir.IPLib.Packets;
using L2PacketDecrypt.Packets;
using L2PacketDecrypt;
using La2PacketSniffer.DataHolding;
using TcpRecon;

namespace La2PacketSniffer
{
    class FileReader
    {
        private PacketContainer packetContainer;
        private bool gotLock = false;
        private int count = 0;
        private int port;

        private L2GameSniffer gameSniffer = null;
        private L2PacketStream clientStr = null;
        private L2PacketStream serverStr = null;

        private Dictionary<TCPConnection, TcpRecon.TcpRecon> sharpPcapDict = new Dictionary<TCPConnection, TcpRecon.TcpRecon>();

        public FileReader(PacketContainer pc, int port)
        {
            this.port = port;
            this.packetContainer = pc;
            this.gameSniffer = new L2GameSniffer();
            this.clientStr = new L2PacketStream();
            this.serverStr = new L2PacketStream();
        }

        public void ReadPcapFile(string capFile)
        {
            PcapDevice device;

            try
            {
                //Get an offline file pcap device
                device = SharpPcap.GetPcapOfflineDevice(capFile);
                //Open the device for capturing
                device.PcapOpen();
            }
            catch (Exception)
            {
                return;
            }

            //Register our handler function to the 'packet arrival' event
            device.PcapOnPacketArrival +=
                new SharpPcap.PacketArrivalEvent(device_PcapOnPacketArrival);

            //device.PcapSetFilter(this.tcpDumpFilter);

            //Start capture 'INFINTE' number of packets
            //This method will return when EOF reached.
            device.PcapCapture(SharpPcap.INFINITE);

            //Close the pcap device
            device.PcapClose();
        }

        private void device_PcapOnPacketArrival(object sender, Packet packet)
        {
            if (!(packet is TCPPacket)) return;

            TCPPacket tcpPacket = (TCPPacket)packet;
            // Creates a key for the dictionary
            TCPConnection c = new TCPConnection(tcpPacket);

            // Todo: gescheiter Filter
            if (!(c.DestinationPort == port || c.SourcePort == port))
                return;

            // create a new entry if the key does not exists
            if (!sharpPcapDict.ContainsKey(c) && !this.gotLock)
            {
                TcpRecon.TcpRecon tcpRecon = new TcpRecon.TcpRecon(this.serverStr, this.clientStr);
                sharpPcapDict.Add(c, tcpRecon);
                this.gotLock = true;
            }

            // Use the TcpRecon class to reconstruct the session
            if (this.sharpPcapDict.ContainsKey(c))
            {
                sharpPcapDict[c].ReassemblePacket(tcpPacket);
            }

            L2Packet l2packet = null;

            
             while (this.clientStr.MorePackets())
            {
                l2packet = this.gameSniffer.handlePacket(this.clientStr.ReadPacket(), false);
                if (l2packet != null)
                {
                    this.packetContainer.AddPacket(l2packet);
                    l2packet.PacketNo = this.count++;
                }
            }
            while (this.serverStr.MorePackets())
            {
                l2packet = this.gameSniffer.handlePacket(this.serverStr.ReadPacket(), true);
                if (l2packet != null)
                {
                    this.packetContainer.AddPacket(l2packet);
                    l2packet.PacketNo = this.count++;
                }
            }
        }
    }
}
