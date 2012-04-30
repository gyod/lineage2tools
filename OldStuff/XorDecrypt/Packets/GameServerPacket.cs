using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt.Packets
{
    public class GameServerPacket : L2Packet
    {
        public GameServerPacket(ByteBuffer data)
            : base(data)
        {
        }

        public GameServerPacket()
        {
        }

        public override int OpCode
        {
            get
            {
                int oc = this.data.GetByte(2);
                if (this.data.GetByte(2) == 0xfe)
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
            return String.Format("GameServerPacket, OpCode: 0x{0:x2} ", this.OpCode);
        }
    }
}
