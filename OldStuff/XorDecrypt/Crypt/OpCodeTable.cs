using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt.Crypt
{
    public class OpCodeTable
    {
        private byte[] _opcodeTable;
        private short[] _exOpcodeTable;

        public OpCodeTable(byte[] opcodeTable, short[] exOpcodeTable)
        {
            _opcodeTable = opcodeTable;
            /*for (int i = 0; i < opcodeTable.length; i++)
            {
                System.out.printf("[%02X] = %02X \n", i, (opcodeTable[i] & 0xff));
            }*/
            _exOpcodeTable = exOpcodeTable;
            /*for (int i = 0; i < exOpcodeTable.length; i++)
            {
                System.out.printf("[%04X] = %04X \n", i, (exOpcodeTable[i] & 0xff));
            }*/
        }

        public byte getOriginalOpcode(int obfuscatedOpcode)
        {
            return _opcodeTable[obfuscatedOpcode & 0xFF];
        }

        public short getExOpcode(int obfuscatedOpcode)
        {
            return _exOpcodeTable[obfuscatedOpcode & 0xFFFF];
        }
    }
}
