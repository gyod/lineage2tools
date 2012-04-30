using System;
using System.Collections.Generic;
using System.Text;

namespace L2Proxy.Crypt
{
    class NullCrypt : ICrypt
    {
        #region ICrypt Member

        public void Decrypt(byte[] raw, int offset, int size)
        {
            
        }

        public void Encrypt(byte[] raw, int offset, int size)
        {
            
        }

        public void SetKey(byte[] key)
        {
            
        }

        #endregion
    }
}
