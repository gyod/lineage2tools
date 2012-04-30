using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public interface IRunablePacket
    {
        short OpCode
        {
            get;
            set;
        }

        PacketType Type
        {
            get;
            set;
        }

        void RunPacket();
    }
}
