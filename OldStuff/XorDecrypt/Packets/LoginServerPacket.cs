using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt.Packets
{
    public class LoginServerPacket : L2Packet
    {
        public LoginServerPacket(ByteBuffer data)
            : base(data)
        {
        }

        public LoginServerPacket()
        {
        }

        public override int OpCode
        {
            get
            {
                return this.data.GetByte(2);
            }
        }

        public override string ToString()
        {
            return String.Format("LoginServerPacket, OpCode: 0x{0:X2} ", this.OpCode);
        }
    }
}
