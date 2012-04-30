using System;

namespace L2Proxy.Crypt
{
    interface ICrypt
    {
        void Decrypt(byte[] raw, int offset, int size);

        void Encrypt(byte[] raw, int offset, int size);

        void SetKey(byte[] key);
    }
}
