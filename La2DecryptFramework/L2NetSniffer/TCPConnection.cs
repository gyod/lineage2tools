using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetSniffer
{
    /// <summary>
    /// Holds the connection information of the Tcp session
    /// </summary>
    public class TCPConnection
    {
        private string m_srcIp;
        public string SourceIp
        {
            get { return m_srcIp; }
        }

        private int m_srcPort;
        public int SourcePort
        {
            get { return m_srcPort; }
        }

        private string m_dstIp;
        public string DestinationIp
        {
            get { return m_dstIp; }
        }

        private int m_dstPort;
        public int DestinationPort
        {
            get { return m_dstPort; }
        }

        public TCPConnection(string sourceIP, int sourcePort, string destinationIP, int destinationPort)
        {
            m_srcIp = sourceIP;
            m_dstIp = destinationIP;
            m_srcPort = sourcePort;
            m_dstPort = destinationPort;
        }

        public TCPConnection(Tamir.IPLib.Packets.TCPPacket packet)
        {
            m_srcIp = packet.SourceAddress;
            m_dstIp = packet.DestinationAddress;
            m_srcPort = packet.SourcePort;
            m_dstPort = packet.DestinationPort;
        }

        /// <summary>
        /// Overrided in order to catch both sides of the connection 
        /// with the same connection object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is TCPConnection))
                return false;
            TCPConnection con = (TCPConnection)obj;

            bool result = ((con.SourceIp.Equals(m_srcIp)) && (con.SourcePort == m_srcPort) && (con.DestinationIp.Equals(m_dstIp)) && (con.DestinationPort == m_dstPort)) ||
                ((con.SourceIp.Equals(m_dstIp)) && (con.SourcePort == m_dstPort) && (con.DestinationIp.Equals(m_srcIp)) && (con.DestinationPort == m_srcPort));

            return result;
        }

        public override int GetHashCode()
        {
            return ((m_srcIp.GetHashCode() ^ m_srcPort.GetHashCode()) as object).GetHashCode() ^
             ((m_dstIp.GetHashCode() ^ m_dstPort.GetHashCode()) as object).GetHashCode();
        }

        public static int GenerateHashCode(Tamir.IPLib.Packets.TCPPacket packet)
        {
            return ((packet.SourceAddress.GetHashCode() ^ packet.SourcePort.GetHashCode()) as object).GetHashCode() ^
             ((packet.DestinationAddress.GetHashCode() ^ packet.DestinationPort.GetHashCode()) as object).GetHashCode();
        }

        public string getFileName(string path)
        {
            return string.Format("{0}{1}.{2}-{3}.{4}.data", path, m_srcIp, m_srcPort, m_dstIp, m_dstPort);
        }
    }
}
