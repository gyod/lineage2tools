using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public class NullCryptor : IL2NetCrypt
    {
        #region IL2NetCrypt Member

        public byte[] SetKey
        {
            set { byte[] foo = value; }
        }

        public void EnCrypt(ref byte[] raw, int lenght, int offset, PacketType type)
        {
            return;
        }

        public void DeCrypt(ref byte[] raw, int lenght, int offset, PacketType type)
        {
            return;
        }

        #endregion
    }
}
