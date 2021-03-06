using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt.Crypt
{
    class LoginCrypt
    {
        private static readonly byte[] STATIC_BLOWFISH_KEY =
	{
		(byte) 0x6b, (byte) 0x60, (byte) 0xcb, (byte) 0x5b,
		(byte) 0x82, (byte) 0xce, (byte) 0x90, (byte) 0xb1,
		(byte) 0xcc, (byte) 0x2b, (byte) 0x6c, (byte) 0x55,
		(byte) 0x6c, (byte) 0x6c, (byte) 0x6c, (byte) 0x6c
	}; /* 6B 60 CB 5B 82 CE 90 B1 CC 2B 6C 55 6C 6C 6C 6C */

        private NewCrypt _staticCrypt = new NewCrypt(STATIC_BLOWFISH_KEY);
        private NewCrypt _crypt;

        public void setKey(byte[] key)
        {
            _crypt = new NewCrypt(key);
        }

        public bool decrypt(byte[] raw, int offset, int size)
        {
            _crypt.decrypt(raw, offset, size);
            return NewCrypt.verifyChecksum(raw, offset, size);
        }

        public bool staticDecrypt(byte[] raw, int offset, int size)
        {
            _staticCrypt.decrypt(raw, offset, size);
            NewCrypt.decXORPass(raw);
            return NewCrypt.verifyChecksum(raw, offset, size);
        }

        public int staticEncrypt(byte[] raw, int offset, int size, uint xorKey)
        {
            // reserve checksum
            size += 4;

            // reserve for XOR "key"
            size += 4;

            // padding
            size += 8 - size % 8;
            NewCrypt.encXORPass(raw, offset, size, xorKey);
            _staticCrypt.crypt(raw, offset, size);

            return size;
        }

        public int encrypt(byte[] raw, int offset, int size)
        {
            // reserve checksum
            size += 4;

            // padding
            size += 8 - size % 8;
            NewCrypt.appendChecksum(raw, offset, size);
            _crypt.crypt(raw, offset, size);

            return size;
        }
    }
}
