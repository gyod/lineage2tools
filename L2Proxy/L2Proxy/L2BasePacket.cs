using System;
using System.Collections.Generic;
using System.Text;

namespace L2Proxy
{
    public class L2BasePacket
    {
        private ByteBuffer data;
        private bool fromServer;

        public L2BasePacket(byte[] data)
        {
            this.data = new ByteBuffer(data);
        }

        public ByteBuffer Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public byte GetOpcode()
        {
            return this.data[2];
        }

        public byte GetSecondOpcode()
        {
            return this.data[3];
        }

        public bool FromServer
        {
            get
            {
                return this.fromServer;
            }
            set
            {
                this.fromServer = value;
            }
        }
    }
}
