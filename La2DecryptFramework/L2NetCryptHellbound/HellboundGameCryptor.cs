using System;
using System.Collections.Generic;
using System.Text;
using L2NetCore;

namespace L2NetCryptHellbound
{
    public class HellboundGameCryptor : IL2NetCrypt
    {
        private Gamecrypt clientCrypt = new Gamecrypt();
        private Gamecrypt serverCrypt = new Gamecrypt();

        private byte[] cryptKey;
        private int obfusicateKey;
        private ConnectionState state = ConnectionState.Connected;

        #region ServerCrypt Member

        /// <summary>
        /// 16 byte long key
        /// </summary>
        public byte[] SetKey
        {
            set
            {
                if (value.Length > 16)
                    throw new ArgumentOutOfRangeException("byte[] key", "Key is to long");
                serverCrypt.setKey(value);
                clientCrypt.setKey(value);
            }
        }

        public void EnCrypt(ref byte[] raw, int lenght, int offset, PacketType type)
        {
            Gamecrypt crypt;
            if (type == PacketType.ClientToGameserver)
            {
                crypt = this.clientCrypt;
            }
            else if (type == PacketType.GameserverToClient)
            {
                crypt = this.serverCrypt;
            }
            else
            {
                throw new Exception("Wrong PacketType "
                    + type.ToString() + " for " + this.ToString());
            }

            crypt.encrypt(ref raw, offset, lenght);
        }

        public void DeCrypt(ref byte[] raw, int lenght, int offset, PacketType type)
        {
            Gamecrypt crypt;
            if (type == PacketType.ClientToGameserver)
            {
                crypt = this.clientCrypt;
            }
            else if (type == PacketType.GameserverToClient)
            {
                crypt = this.serverCrypt;
            }
            else
            {
                throw new Exception("Wrong PacketType "
                    + type.ToString() + " for " + this.ToString());
            }


            crypt.decrypt(ref raw, offset, lenght);
            if (this.state == ConnectionState.Connected)
            {
                if (raw[0 + offset] == 0x2e)
                {
                    Bytebuffer bb = new Bytebuffer(raw);
                    bb.Position = offset;
                    this.onKeyPacket(bb);
                }
                else if (raw[0 + offset] == 0x09)
                {
                    // CharSelectionInfo
                    this.state = ConnectionState.Authed;
                }
            }
            else if (this.state == ConnectionState.Authed)
            {
                if (raw[0 + offset] == 0x73)
                {
                    this.state = ConnectionState.InGame;
                }
            }
            else if (this.state == ConnectionState.InGame)
            {
                if (raw[0 + offset] == 0x0b)
                {
                    //CharSelected
                    Bytebuffer bb = new Bytebuffer(raw);
                    bb.Position = offset;
                    this.onCharSelected(new Bytebuffer(raw));
                }
            }
        }

        #endregion

        private void onKeyPacket(Bytebuffer packet)
        {
            packet.ReadByte();
            this.cryptKey = packet.ReadBytes(16);

            this.cryptKey[8] = (byte)0xc8;
            this.cryptKey[9] = (byte)0x27;
            this.cryptKey[10] = (byte)0x93;
            this.cryptKey[11] = (byte)0x01;
            this.cryptKey[12] = (byte)0xa1;
            this.cryptKey[13] = (byte)0x6c;
            this.cryptKey[14] = (byte)0x31;
            this.cryptKey[15] = (byte)0x97;

            serverCrypt.setKey(this.cryptKey);
            clientCrypt.setKey(this.cryptKey);

            // ddcd
            packet.ReadInt();
            packet.ReadInt();
            packet.ReadByte();
            this.obfusicateKey = packet.ReadInt();

            serverCrypt.generateOpcodeTable(this.obfusicateKey);
            clientCrypt.generateOpcodeTable(this.obfusicateKey);

        }

        private void onCharSelected(Bytebuffer packet)
        {
            packet.ReadString(); // name
            packet.ReadInt(); // CharId
            packet.ReadString(); // Title
            packet.ReadBytes(196);// 196 bytes
            this.obfusicateKey = packet.ReadInt();

            serverCrypt.generateOpcodeTable(this.obfusicateKey);
            clientCrypt.generateOpcodeTable(this.obfusicateKey);
        }
    }
}
