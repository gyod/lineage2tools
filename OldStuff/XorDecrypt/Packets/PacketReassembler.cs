using System;
using System.Collections.Generic;
using System.Text;
using Tamir.IPLib.Packets;
using System.Collections;

namespace L2PacketDecrypt.Packets
{
    /// <summary>
    /// Trys to reassemble TCPPackets
    /// </summary>
    class PacketReassembler
    {
        private ArrayList buffer = new ArrayList();
        private const int PACKET_LENGHT = 1452;

        public TCPPacket processPacket(TCPPacket packet)
        {
            if (packet.FragmentFlags == 0x04) // not Fragmented
            {
                return packet;
            }
            else if (packet.FragmentFlags == 0x00 && packet.Data.Length == PacketReassembler.PACKET_LENGHT) //maybe Fragmented?
            {
                buffer.Add(packet);
            }
            else if (packet.FragmentFlags == 0x00 
                && packet.Data.Length != PacketReassembler.PACKET_LENGHT) //maybe Fragmented?
            {
                buffer.Add(packet);

                long ackNr = packet.AcknowledgmentNumber;
                int size = packet.HeaderLength;
                foreach (TCPPacket p in buffer)
                {
                    if (p.AcknowledgmentNumber == ackNr)
                    {
                        size += p.Data.Length;
                    }
                }
                if (size == packet.HeaderLength)
                {
                    return null;
                }
                byte[] data = new byte[size];
                Array.Copy(packet.Header, 0, data, 0, packet.HeaderLength);
                int pos = packet.HeaderLength;
                foreach (TCPPacket p in buffer)
                {
                    if (p.AcknowledgmentNumber == ackNr)
                    {
                        Array.Copy(p.Data, 0, data, pos, p.Data.Length);
                        pos += p.Data.Length;
                        buffer.Remove(p);
                    }
                }
                
                TCPPacket reassembledPacket = new TCPPacket((int)size, data);
                return reassembledPacket;
            }

            return null;
        }
    }
}
