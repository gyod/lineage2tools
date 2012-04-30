using System;
using System.Collections.Generic;
using System.Text;

namespace L2Proxy.Crypt
{
    class LoginCrypt : ICrypt
    {
        private static readonly byte[] STATIC_BLOWFISH_KEY =
	    {
		(byte) 0x6b, (byte) 0x60, (byte) 0xcb, (byte) 0x5b,
		(byte) 0x82, (byte) 0xce, (byte) 0x90, (byte) 0xb1,
		(byte) 0xcc, (byte) 0x2b, (byte) 0x6c, (byte) 0x55,
		(byte) 0x6c, (byte) 0x6c, (byte) 0x6c, (byte) 0x6c
	    };

        private NewCrypt _staticCrypt = new NewCrypt(STATIC_BLOWFISH_KEY);
        private NewCrypt _crypt;
        private bool _static = true;

        public void SetKey(byte[] key)
        {
            _crypt = new NewCrypt(key);
        }

        public void Decrypt(byte[] raw, int offset, int size)
        {
            if (_static)
            {
                _staticCrypt.decrypt(raw, offset, size);
                NewCrypt.decXORPass(raw, offset);

                _static = false;
            }
            else
            {
                _crypt.decrypt(raw, offset, size);
                //return NewCrypt.verifyChecksum(raw, offset, size);
            }
        }

        public void Encrypt(byte[] raw, int offset, int size)
        {
            // reserve checksum
            size += 4;


            if (_static)
            {
                // reserve for XOR "key"
                size += 4;

                // padding
                size += 8 - size % 8;
                Random rnd = new Random();
                NewCrypt.encXORPass(raw, offset, size, (uint)rnd.Next());
                _staticCrypt.crypt(raw, offset, size);

                _static = false;
            }
            else
            {
                // padding
                size += 8 - size % 8;
                NewCrypt.appendChecksum(raw, offset, size);
                _crypt.crypt(raw, offset, size);
            }
            //return size;
        }
    }
}
