using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public interface IL2NetCrypt
    {    
        byte[] SetKey
        {
            set;
        }

        void EnCrypt(ref byte[] raw, int lenght, int offset, PacketType type);

        void DeCrypt(ref byte[] raw, int lenght, int offset, PacketType type);
    }
}
