using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCryptHellbound
{
    class Gamecrypt
    {
        private OpcodeTable _table = null;
        private byte[] _inKey = new byte[16];
        private byte[] _outKey = new byte[16];
        private bool _isEnabled;

        public void setKey(byte[] key)
        {
            Array.Copy(key, 0, _inKey, 0, 16);
            Array.Copy(key, 0, _outKey, 0, 16);
            this._isEnabled = true;
        }

        public void decrypt(ref byte[] raw, int offset, int size)
        {

            if (!_isEnabled)
                return;

            uint temp = 0;
            for (int i = 0; i < size - offset; i++)
            {
                uint temp2 = (uint)raw[offset + i] & 0xFF;
                raw[offset + i] = (byte)(temp2 ^ _inKey[i & 15] ^ temp);
                temp = temp2;
            }

            /*uint old = _inKey[8] & (uint)0xff;
            old |= (uint)_inKey[9] << 8 & 0xff00;
            old |= (uint)_inKey[10] << 0x10 & (uint)0xff0000;
            old |= (uint)_inKey[11] << 0x18 & 0xff000000;*/

            uint old = BitConverter.ToUInt32(_inKey, 8);
            old += (uint)(size - offset); // FUCKING BUG!! min. 24h waste of time!!! =(

            _inKey[8] = (byte)(old & 0xff);
            _inKey[9] = (byte)(old >> 0x08 & 0xff);
            _inKey[10] = (byte)(old >> 0x10 & 0xff);
            _inKey[11] = (byte)(old >> 0x18 & 0xff);

            if (_table != null)
            {
                raw[0 + offset] = _table.getOriginalOpcode(raw[0 + offset]);
                if ((raw[0 + offset] & 0xFF) == 0xd0)
                {
                    short exOpcode = _table.getExOpcode(raw[1 + offset] + (raw[2 + offset] << 8));
                    raw[1 + offset] = (byte)(exOpcode & 0xFF);
                    raw[2 + offset] = (byte)((exOpcode >> 8) & 0xFF);
                }
            }
        }

        public void encrypt(ref byte[] raw, int offset, int size)
        {
            if (!_isEnabled)
            {
                _isEnabled = true;
                return;
            }

            int temp = 0;
            for (int i = 0; i < size - offset; i++)
            {
                int temp2 = raw[offset + i] & 0xFF;
                temp = temp2 ^ _outKey[i & 15] ^ temp;
                raw[offset + i] = (byte)temp;
            }

            /*int old = _outKey[8] & 0xff;
            old |= _outKey[9] << 8 & 0xff00;
            old |= _outKey[10] << 0x10 & 0xff0000;
            old |= _outKey[11] << 0x18 & unchecked((int)0xff000000);*/

            uint old = BitConverter.ToUInt32(_inKey, 8);

            old += (uint)(size - offset);

            _outKey[8] = (byte)(old & 0xff);
            _outKey[9] = (byte)(old >> 0x08 & 0xff);
            _outKey[10] = (byte)(old >> 0x10 & 0xff);
            _outKey[11] = (byte)(old >> 0x18 & 0xff);
        }

        public OpcodeTable OpTable
        {
            get { return _table; }
            set { _table = value; }
        }

        public void generateOpcodeTable(int key)
        {
            if (key == 0)
            {
                byte[] opcodes = new byte[0xd1]; // d0 + 1
                for (int i = 0; i < opcodes.Length; i++)
                {
                    opcodes[i] = (byte)i;
                }
                short[] exOpcodes = new short[0x4e];
                for (int i = 0; i < exOpcodes.Length; i++)
                {
                    exOpcodes[i] = (byte)i;
                }
                _table = new OpcodeTable(opcodes, exOpcodes);
            }
            else
            {
                _table = OpcodeObfuscator.getObfuscatedTable(key);
            }
        }
    }
}
