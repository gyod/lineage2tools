using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCryptHellbound
{
    class NewCrypt
    {
        BlowfishEngine _crypt;
        BlowfishEngine _decrypt;

        /// <summary>
        /// Packet is first XOR encoded with <code>key</code>
        /// Then, the last 4 bytes are overwritten with the the XOR "key".
        /// Thus this assume that there is enough room for the key to fit without overwriting data.
        /// </summary>
        /// <param name="raw">The raw bytes to be encrypted</param>
        /// <param name="offset">offset The begining of the data to be encrypted</param>
        /// <param name="size">Length of the data to be encrypted</param>
        /// <param name="key">The 4 bytes (int) XOR key</param>
        public static void encXORPass(ref byte[] raw, int offset, int size, uint key)
        {
            int stop = size - 8;
            int pos = 4 + offset;
            int edx;
            int ecx = (int)key; // Initial xor key

            while (pos < stop)
            {
                edx = (raw[pos] & 0xFF);
                edx |= (raw[pos + 1] & 0xFF) << 8;
                edx |= (raw[pos + 2] & 0xFF) << 16;
                edx |= (raw[pos + 3] & 0xFF) << 24;

                ecx += edx;

                edx ^= ecx;

                raw[pos++] = (byte)(edx & 0xFF);
                raw[pos++] = (byte)(edx >> 8 & 0xFF);
                raw[pos++] = (byte)(edx >> 16 & 0xFF);
                raw[pos++] = (byte)(edx >> 24 & 0xFF);
            }

            raw[pos++] = (byte)(ecx & 0xFF);
            raw[pos++] = (byte)(ecx >> 8 & 0xFF);
            raw[pos++] = (byte)(ecx >> 16 & 0xFF);
            raw[pos++] = (byte)(ecx >> 24 & 0xFF);
        }

        /// <summary>
        /// Decrypts the raw packet which contains Blowfish keypairs.
        /// </summary>
        /// <param name="raw">The raw bytes to be decrypted</param>
        /// <returns>the orginal XOR-Key</returns>
        public static int decXORPass(ref byte[] raw)
        {
            return decXORPass(ref raw, 2);
        }

        /// <summary>
        /// Decrypts the raw packet which contains Blowfish keypairs.
        /// </summary>
        /// <param name="raw">The raw bytes to be decrypted</param>
        /// <param name="offset">normally 2</param>
        /// <returns>the orginal XOR-Key</returns>
        public static int decXORPass(ref byte[] raw, int offset)
        {
            int pos = raw.Length - 8;
            int stop = 3 + offset;
            int edx;

            // get the key
            int ecx;
            ecx = (raw[raw.Length - 8] & 0xFF);
            ecx |= (raw[raw.Length - 7] & 0xFF) << 8;
            ecx |= (raw[raw.Length - 6] & 0xFF) << 16;
            ecx |= (raw[raw.Length - 5] & 0xFF) << 24;

            while (pos > stop)
            {
                edx = (raw[pos] & 0xFF);
                edx |= (raw[pos + 1] & 0xFF) << 8;
                edx |= (raw[pos + 2] & 0xFF) << 16;
                edx |= (raw[pos + 3] & 0xFF) << 24;

                edx ^= ecx;

                ecx -= edx;

                raw[pos] = (byte)(edx & 0xFF);
                raw[pos + 1] = (byte)(edx >> 8 & 0xFF);
                raw[pos + 2] = (byte)(edx >> 16 & 0xFF);
                raw[pos + 3] = (byte)(edx >> 24 & 0xFF);
                pos -= 4;
            }

            return ecx; // initial key
        }

        /**
         * @param blowfishKey
         */
        public NewCrypt(byte[] blowfishKey)
        {

            /* _crypt = new BlowfishEngine();
             _crypt.Init(true, new KeyParameter(blowfishKey));
             _decrypt = new BlowfishEngine();
             _decrypt.Init(false, new KeyParameter(blowfishKey));*/

            _crypt = new BlowfishEngine();
            _crypt.Init(true, blowfishKey);
            _decrypt = new BlowfishEngine();
            _decrypt.Init(false, blowfishKey);
        }

        public static bool verifyChecksum(byte[] raw)
        {
            return NewCrypt.verifyChecksum(raw, 0, raw.Length);
        }

        public static bool verifyChecksum(byte[] raw, int offset, int size)
        {
            // check if size is multiple of 4 and if there is more then only the checksum
            if ((size & 3) != 0 || size <= 4)
            {
                return false;
            }

            long chksum = 0;
            int count = size - 4;
            long check = -1;
            int i;

            for (i = offset; i < count; i += 4)
            {
                check = raw[i] & 0xff;
                check |= raw[i + 1] << 8 & (uint)0xff00;
                check |= raw[i + 2] << 0x10 & (uint)0xff0000;
                check |= raw[i + 3] << 0x18 & 0xff000000;

                chksum ^= check;
            }

            check = raw[i] & 0xff;
            check |= raw[i + 1] << 8 & (uint)0xff00;
            check |= raw[i + 2] << 0x10 & (uint)0xff0000;
            check |= raw[i + 3] << 0x18 & 0xff000000;

            return check == chksum;
        }

        public static void appendChecksum(byte[] raw)
        {
            NewCrypt.appendChecksum(ref raw, 0, raw.Length);
        }

        public static void appendChecksum(ref byte[] raw, int offset, int size)
        {
            long chksum = 0;
            int count = size - 4;
            long ecx;
            int i;

            for (i = offset; i < count; i += 4)
            {
                ecx = raw[i] & 0xff;
                ecx |= raw[i + 1] << 8 & (uint)0xff00;
                ecx |= raw[i + 2] << 0x10 & (uint)0xff0000;
                ecx |= raw[i + 3] << 0x18 & 0xff000000;

                chksum ^= ecx;
            }

            ecx = raw[i] & 0xff;
            ecx |= raw[i + 1] << 8 & (uint)0xff00;
            ecx |= raw[i + 2] << 0x10 & (uint)0xff0000;
            ecx |= raw[i + 3] << 0x18 & 0xff000000;

            raw[i] = (byte)(chksum & 0xff);
            raw[i + 1] = (byte)(chksum >> 0x08 & 0xff);
            raw[i + 2] = (byte)(chksum >> 0x10 & 0xff);
            raw[i + 3] = (byte)(chksum >> 0x18 & 0xff);
        }

        public byte[] decrypt(ref byte[] raw)
        {
            byte[] result = new byte[raw.Length];
            int count = raw.Length / 8;

            for (int i = 0; i < count; i++)
            {
                _decrypt.ProcessBlock(raw, i * 8, result, i * 8);
            }

            return result;
        }

        public void decrypt(ref byte[] raw, int offset, int size)
        {
            byte[] result = new byte[size];
            int count = size / 8;

            for (int i = 0; i < count; i++)
            {
                _decrypt.ProcessBlock(raw, offset + i * 8, result, i * 8);
            }
            // TODO can the crypt and decrypt go direct to the array
            Array.Copy(result, 0, raw, offset, size - offset); //FIXME eventuell fehlerhaft
        }

        public byte[] crypt(byte[] raw)
        {
            int count = raw.Length / 8;
            byte[] result = new byte[raw.Length];

            for (int i = 0; i < count; i++)
            {
                _crypt.ProcessBlock(raw, i * 8, result, i * 8);
            }

            return result;
        }

        public void crypt(ref byte[] raw, int offset, int size)
        {
            int count = size / 8;
            byte[] result = new byte[size];

            for (int i = 0; i < count; i++)
            {
                _crypt.ProcessBlock(raw, offset + i * 8, result, i * 8);
            }
            // TODO can the crypt and decrypt go direct to the array
            Array.Copy(result, 0, raw, offset, size - offset); //FIXME eventuell fehlerhaft
        }
    }
}
