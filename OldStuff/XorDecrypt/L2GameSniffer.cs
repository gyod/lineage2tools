using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using L2PacketDecrypt.Crypt;
using L2PacketDecrypt.Packets;

namespace L2PacketDecrypt
{
    public class L2GameSniffer
    {
        private GameCrypt serverCrypt = new GameCrypt();
        private GameCrypt clientCrypt = new GameCrypt();

        public enum GameClientState { CONNECTED, AUTHED, IN_GAME };
        private GameClientState state = GameClientState.CONNECTED;

        private byte[] cryptKey;
        private int obfusicateKey;

        public L2Packet handlePacket(byte[] rawPacket, bool fromServer)
        {
            if (rawPacket.Length < 3) // Valid data?
                return null;
            if (fromServer)
                serverCrypt.decrypt(rawPacket, 2, rawPacket.Length);
            else
                clientCrypt.decrypt(rawPacket, 2, rawPacket.Length);

            ByteBuffer binPacket = new ByteBuffer(rawPacket);
            binPacket.ReadBytes(2); // Read 2 bytes ahead
            int opcode = binPacket.ReadByte();
            int secondOpCode = -1;
            
                switch (state)
                {
                    case GameClientState.CONNECTED:
                        // Vom Server (ServerPacket)
                        if (fromServer)
                        {
                            switch (opcode)
                            {

                                case 0x2e: // KeyPacket (Contains key, NOT Encrypted xD )
                                    this.onKeyPacket(binPacket);
                                    break;
                                case 0x09: // CharSelectionInfo
                                    this.state = GameClientState.AUTHED;
                                    break;
                            }
                        }
                        // Vom Clienten
                        else
                        {

                        }
                        break;
                    case GameClientState.AUTHED:
                        if (fromServer)
                        {
                            switch (opcode)
                            {
                                case 0x73:
                                    this.state = GameClientState.IN_GAME;
                                    //Console.Out.WriteLine(Util.printData(binPacket));
                                    break;
                            }
                        }
                        else 
                        { 
                            switch (opcode)
                            {
                                case 0x0d:
                                    // get secondOpCode and set
                                    secondOpCode = binPacket.ReadInt16();
                                    break;
                                case 0x12: // CharacterSelect
                                    break;
                            }
                        }
                        break;
                    case GameClientState.IN_GAME:
                        if (fromServer)
                        {
                            switch (opcode)
                            {
                                case 0xfe:
                                    // get secondOpCode
                                    secondOpCode = binPacket.ReadInt16();
                                    break;
                                case 0x0b: //CharSelected
                                    this.onCharSelected(binPacket);
                                    break;
                            }
                        }  
                        else
                        {
                            switch (opcode)
                            {
                                case 0x1f:
                                    break;
                                case 0xd0:
                                    // get secondOpCode
                                    secondOpCode = binPacket.ReadInt16();
                                    break;
                            }
                        }
                        break;
                }


                if (fromServer)
                    return new GameServerPacket(binPacket);
                else
                {               
                    return new ClientPacket(binPacket);
                }
        }

        private void onKeyPacket(ByteBuffer packet)
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
            packet.ReadInt32();
            packet.ReadInt32();
            packet.ReadByte();
            this.obfusicateKey = packet.ReadInt32();
            this.clientCrypt.generateOpcodeTable(this.obfusicateKey);
        }

        private void onCharSelected(ByteBuffer packet)
        {
            packet.ReadString(); // name
            packet.ReadInt32(); // CharId
            packet.ReadString(); // Title
            packet.ReadBytes(196);// 196 bytes
            this.obfusicateKey = packet.ReadInt32();
            this.clientCrypt.generateOpcodeTable(this.obfusicateKey);
        }

        public GameClientState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
    }
}
