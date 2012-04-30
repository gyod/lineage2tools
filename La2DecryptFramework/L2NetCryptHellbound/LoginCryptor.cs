using System;
using System.Collections.Generic;
using System.Text;
using L2NetCore;

namespace L2NetCryptHellbound
{
    public class LoginCryptor : IL2NetCrypt
    {
        private Logincrypt serverCrypt = new Logincrypt();
        private Logincrypt clientCrypt = new Logincrypt();

        private bool staticCrypt = true;
        private byte[] blowfishKey;

        #region ServerCrypt Member

        public byte[] SetKey
        {
            set
            {
            }
        }

        public void EnCrypt(ref byte[] raw, int lenght, int offset, PacketType type)
        {
            Logincrypt crypt;
            if (type == PacketType.ClientToLoginserver)
            {
                crypt = this.clientCrypt;
            }
            else if (type == PacketType.LoginserverToClient)
            {
                crypt = this.serverCrypt;
            }
            else
            {
                throw new Exception("Wrong PacketType " 
                    + type.ToString() + " for " + this.ToString());
            }

            if (this.staticCrypt)
            {
                crypt.staticEncrypt(ref raw, offset, lenght, (uint)new Random().Next());
                enableCrypt();
            }
            else
            {
                crypt.encrypt(ref raw, offset, lenght);
            }
        }

        public void DeCrypt(ref byte[] raw, int lenght, int offset, PacketType type)
        {
            Logincrypt crypt;
            if (type == PacketType.ClientToLoginserver)
            {
                crypt = this.clientCrypt;
            }
            else if (type == PacketType.LoginserverToClient)
            {
                crypt = this.serverCrypt;
            }
            else
            {
                throw new Exception("Wrong PacketType "
                    + type.ToString() + " for " + this.ToString());
            }

            if (this.staticCrypt)
            {
                crypt.staticDecrypt(ref raw, offset, lenght);
                this.staticCrypt = false;
                if (raw[0 + offset] == 0x00)
                {
                    Console.WriteLine("Got Initpacket");
                    handleInit(new Bytebuffer(raw));
                }
            }
            else
            {
                crypt.decrypt(ref raw, offset, lenght);
            }
        }

        #endregion

        private void handleInit(Bytebuffer packet)
        {
            int sessionId = packet.ReadInt();
            int protocolVer = packet.ReadInt();
            byte[] publicKey = packet.ReadBytes(0x80);
            // 4x read D (GameGuard);
            for (int i = 0; i < 4; i++)
                packet.ReadInt();
            byte[] blowfishKey = packet.ReadBytes(0x10);

            // set new key in LoginCrypt
            serverCrypt.setKey(blowfishKey);
            clientCrypt.setKey(blowfishKey);
        }

        private void enableCrypt()
        {
            Random rnd = new Random();
            this.blowfishKey = new byte[16];
            rnd.NextBytes(this.blowfishKey);
            serverCrypt.setKey(this.blowfishKey);
            clientCrypt.setKey(this.blowfishKey); 
            this.staticCrypt = false;
        }
    }
}
