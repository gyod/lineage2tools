using System;
using System.Collections.Generic;
using System.Text;
using L2Proxy;

namespace L2PacketEditor
{
    class Hellbound2Kamael : IPacketFilter
    {
        private void serverPacket(L2BasePacket packet)
        {
            int opcode = packet.GetOpcode();

            switch (opcode)
            {
                case 0x2e: //keypacket
                    packet.Data.SetIndex(3);
                    packet.Data.ReadByte();
                    packet.Data.ReadBytes(8);
                    packet.Data.WriteInt32(0x01);
                    packet.Data.WriteInt32(0x01);
                    packet.Data.WriteByte(0x00);
                    packet.Data.WriteInt32(0x00);
                    break;
                default:
                    break;
            }
        }

        private void clientPacket(L2BasePacket packet)
        {
            int opcode = packet.GetOpcode();

            switch (opcode)
            {
                case 0x0e: // Protcolversion
                    packet.Data.SetIndex(2);
                    packet.Data.ReadByte(); //Opcode
                    packet.Data.WriteUInt32(828); // Protocolversion
                    break;
                default:
                    break;
            }
        }

        #region IPacketFilter Member

        public void FilterPacket(L2BasePacket packet)
        {
            if (packet.FromServer)
            {
                this.serverPacket(packet);
            }
            else
            {
                this.clientPacket(packet);
            }
        }

        public string GetDiscription()
        {
            return "Kamael2Hellbound";
        }

        public override string ToString()
        {
            return this.GetDiscription();
        }

        public void Configure()
        {
            // nothing to configure
        }

        #endregion
    }
}
