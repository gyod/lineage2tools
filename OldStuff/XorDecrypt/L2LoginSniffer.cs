using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using L2PacketDecrypt.Crypt;
using L2PacketDecrypt.Packets;

namespace L2PacketDecrypt
{
    public class L2LoginSniffer
    {
        /**
         * Kurze vorgehensweise:
         * Instanz von LoginCrypt erstellen
         * das Init Packet verarbeiten mit staticDecrypt
         * daraus BlowfishKey und PublicKey holen
         * neuen Key in der LoginCrypt setzen.
         * staticCrypt auf false setzen
         * 
         */
        private bool staticCrypt = true;
        private byte[] blowfishKey;
        private byte[] publicKey;
        private uint sessionId;
        private uint protocolVer;
        private LoginCrypt crypt = new LoginCrypt();

        public L2Packet handlePacket(byte[] incPacket, bool fromServer)
        {
            if (!fromServer)
                return null;

            this.decryptPacket(incPacket);
            ByteBuffer binPacket = new ByteBuffer(incPacket);
            binPacket.ReadBytes(2); // discard first 2 bytes

            byte id = binPacket.ReadByte();
            switch (id)
            {
                case 0x00: // Init
                    //Console.Out.WriteLine("LoginServerpacket::Init");
                    this.handleInit(binPacket);
                    break;
                case 0x01: // LoginFail
                    //Console.Out.WriteLine("LoginServerpacket::LoginFail");
                    break;
                case 0x02: // AccountKicked
                    //Console.Out.WriteLine("LoginServerpacket::AccountKicked");
                    break;
                case 0x03: // LoginOk
                    //Console.Out.WriteLine("LoginServerpacket::LoginOk");
                    break;
                case 0x04: // Serverlist
                    //Console.Out.WriteLine("LoginServerpacket::Serverlist");
                    break;
                case 0x06: // PlayFail
                    //Console.Out.WriteLine("LoginServerpacket::PlayFail");
                    break;
                case 0x07: // PlayOk
                    //Console.Out.WriteLine("LoginServerpacket::PlayOK");
                    break;
                case 0x0b: // GGAuth
                   // Console.Out.WriteLine("LoginServerpacket::GGAuth");
                    break;
            }
            return new LoginServerPacket(binPacket);
        }

        private void decryptPacket(byte[] raw)
        {
            if (staticCrypt)
            {
                this.crypt.staticDecrypt(raw, 2, raw.Length);
                this.staticCrypt = false;
            }
            else
            {
                this.crypt.decrypt(raw, 2, raw.Length);
            }
        }

        private void handleInit(ByteBuffer packet)
        {
            this.sessionId = packet.ReadUInt32();
            this.protocolVer = packet.ReadUInt32();
            this.publicKey = packet.ReadBytes(0x80);
            // 4x read D (GameGuard);
            for (int i = 0; i < 4; i++)
                packet.ReadUInt32();
            this.blowfishKey = packet.ReadBytes(0x10);

            // set new key in LoginCrypt
            crypt.setKey(this.blowfishKey);
        }

        // Getter/Setter
        public byte[] BlowfishKey
        {
            get
            {
                return blowfishKey;
            }
        }

        public byte[] PublicKey
        {
            get
            {
                return publicKey;
            }
        }

        public uint SessionId
        {
            get
            {
                return sessionId;
            }
        }

        public uint ProtocolVersion
        {
            get
            {
                return protocolVer;
            }
        }
    }
}
