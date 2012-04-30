using System.Collections.Generic;
using Tamir.IPLib;
using Tamir.IPLib.Packets;
using L2NetCore;
using System;

namespace L2NetSniffer
{
    public class L2NetSniffer : IL2StreamProvider
    {
        private PcapDevice device = null;
        private string tcpDumpFilter = null;
        private int dst_port;

        private L2NetStream clientStr = null;
        private L2NetStream serverStr = null;

        private TcpRecon connection = null;

        private Dictionary<TCPConnection, TcpRecon> sharpPcapDict = new Dictionary<TCPConnection, TcpRecon>();

        /// <summary>
        /// Stellt eine Klasse da die das Sniffen von Packets regelt
        /// </summary>
        /// <param name="port">Der Port auf den der TCPStreamAssembler horchen soll</param>
        public L2NetSniffer(PcapDevice device, int dst_port)
        {
            this.dst_port = dst_port;
            this.device = device;

            /* 
             * (((ip.src == 192.168.100.150) && (tcp.dstport == 7777) ) || ((ip.src == 78.46.33.43) && (tcp.dstport == 52784)))
             */
            this.tcpDumpFilter = "port " + dst_port;

            this.clientStr = new L2NetStream();
            this.serverStr = new L2NetStream();
            // TODO: wenn keine devices gefunden wurden, Meldung

            this.Init();
        }

        /// <summary>
        /// Initialisiert die SnifferControll
        /// </summary>
        private void Init()
        {
            //Register our handler function to the 'packet arrival' event
            this.device.PcapOnPacketArrival +=
                new SharpPcap.PacketArrivalEvent(device_PcapOnPacketArrival);

            //Open the device for capturing
            //true -- means promiscuous mode
            //1000 -- means a read wait of 1000ms
            this.device.PcapOpen(false, 100);

            //Associate the filter with this capture
            this.device.PcapSetFilter(this.tcpDumpFilter);

            // Start Sniffing
            this.device.PcapStartCapture();
        }


        /// <summary>
        /// Stoppt den Sniffer
        /// </summary>
        public void Stop()
        {
            if (this.device != null)
            {
                this.device.PcapStopCapture();
                this.device.PcapClose();
            }
        }

        private void device_PcapOnPacketArrival(object sender, Packet packet)
        {
            if (!(packet is TCPPacket)) return;

            TCPPacket tcpPacket = (TCPPacket)packet;
            // Creates a key for the dictionary
            TCPConnection connection = new TCPConnection(tcpPacket);

            // create a new entry if the key does not exists and its a new Connection
            if (!sharpPcapDict.ContainsKey(connection) 
                && tcpPacket.Syn && (tcpPacket.DestinationPort == this.dst_port))
            {
                TcpRecon tcpRecon = new TcpRecon(this.ServerStr, this.ClientStr);
                sharpPcapDict.Add(connection, tcpRecon);
                this.connection = tcpRecon;

                // (((ip.src == 192.168.100.150) && (tcp.dstport == 7777) ) || ((ip.src == 78.46.33.43) && (tcp.dstport == 52784)))

                string filter = string.Format("(((ip.src == {0}) && (tcp.dstport == {1})) || ((ip.src == {2}) && (tcp.dstport == {3})))"
                    , tcpPacket.SourceAddress, tcpPacket.DestinationPort, tcpPacket.DestinationAddress, tcpPacket.SourcePort);

                this.tcpDumpFilter = filter;
                // Geht oder geht nicht??? das is die große Frage ;)
                this.device.PcapSetFilter(filter);
            }

            // Use the TcpRecon class to reconstruct the session
            if (this.sharpPcapDict.ContainsKey(connection))
            {
                sharpPcapDict[connection].ReassemblePacket(tcpPacket);
            }
        }

        #region IL2StreamProvider Member

        public L2NetStream ClientStreamIn
        {
            get { return this.clientStr; }
        }

        public L2NetStream ServerStreamIn
        {
            get { return this.serverStr; }
        }

        public L2NetStream ClientStreamOut
        {
            get { throw new NotImplementedException(); }
        }

        public L2NetStream ServerStreamOut
        {
            get { throw new NotImplementedException(); }
        }

        public L2NetMode ProviderType
        {
            get { return L2NetMode.Sniffer; }
        }

        #endregion
    }
}
