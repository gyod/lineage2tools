using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt.Packets
{
    public class ClientPacket : L2Packet
    {
        public ClientPacket(ByteBuffer data)
            : base(data)
        {
        }

        public ClientPacket()
        {
        }

        public override int OpCode
        {
            get
            {
                int oc = this.data.GetByte(2);
                if (this.data.GetByte(2) == 0xd0)
                {
                    oc = 0;
                    oc = this.data.GetByte(3) & 0xff;
                    oc |= this.data.GetByte(2) << 8 & 0xff00;

                }
                return oc;
            }
        }

        public override string ToString()
        {
            return String.Format("ClientPacket, OpCode: 0x{0:x2} ", this.OpCode);
        }
    }
}
