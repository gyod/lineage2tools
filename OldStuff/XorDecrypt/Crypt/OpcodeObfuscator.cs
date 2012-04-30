using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt.Crypt
{
    public class OpcodeObfuscator
    {
        // static usage
        private OpcodeObfuscator()
        {

        }

        private static short[] shuffleEx(int key)
        {
            short[] array = new short[0x4e];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)i;
            }

            int edx = 0;
            int ecx = 2;

            short tmp2;
            short opcode;
            int edi = 1;

            int subKey;
            int j = 0;
            do
            {
                //MOVZX EAX,AX
                key = rotateKey(key);
                subKey = key >> 16;
                subKey &= 0x7FFF;

                //System.err.printf("SubKey1: %04X\n" , subKey);

                // CDQ
                edx = 0;

                // IDIV ECX
                //edxeax = (edx << 32) + eax;
                //eax = (int) (edxeax / ecx);
                edx = subKey % ecx++;
                //System.err.printf("SubKey2: %04X - EDX: %04X\n" , subKey, edx);

                // INC EDI
                edi++;

                // SKIPPED: DEC EBX

                // SKIPPED: MOVZX EDX,DL

                // MOV CL,BYTE PTR DS:[EDX+ESI+4174]
                opcode = array[edx];

                // MOV DL,BYTE PTR DS:[EDI-1]
                tmp2 = array[edi - 1];

                // MOV BYTE PTR DS:[EAX],DL
                array[edx] = (byte)tmp2;

                // MOV BYTE PTR DS:[EDI-1],CL
                array[edi - 1] = (byte)opcode;
            }
            while (++j < 0x4d);

            /*for (int i = 0; i < array.length; i++)
            {
                System.err.printf("array[%04X] = %04X\n", i, array[i]);
            }*/

            return array;
        }

        //private static int STORED_KEY = 0x73BD7EF4;//0x63E790F1;

        private static int rotateKey(int key)
        {
            key *= 0x343FD;
            key += 0x269EC3;
            return key;
        }

        public static OpCodeTable getObfuscatedTable(int key)
        {
            byte[] array = new byte[0xd1];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)i;
            }

            byte tmp;
            byte opcode;

            int edx = 0;
            int ecx = 2;
            int edi = 1;

            int subKey;
            int j = 0;
            do
            {
                //MOVZX EAX,AX
                key = rotateKey(key);

                subKey = key >> 16;
                subKey &= 0x7FFF;

                //System.err.printf("SubKey1: %04X\n" , eax);
                // CDQ
                edx = 0;

                // IDIV ECX
                //edxeax = (edx << 32) + eax;
                //eax = (int) (edxeax / ecx);
                edx = subKey % ecx++;
                //System.err.printf("SubKey2: %04X - EDX: %04X\n" , eax, edx);

                // INC EDI
                edi++;

                // SKIPPED: DEC EBX (ZF enabler)

                // SKIPPED: MOVZX EDX,DL

                // MOV CL,BYTE PTR DS:[EDX+ESI+4174]
                opcode = array[edx];

                // MOV DL,BYTE PTR DS:[EDI-1]
                tmp = array[edi - 1];

                // MOV BYTE PTR DS:[EAX],DL
                array[edx] = tmp;

                // MOV BYTE PTR DS:[EDI-1],CL
                array[edi - 1] = opcode;
            }
            while (++j < 0xd0);

            OpcodeObfuscator.revertOpcodeToOriginal(array, (byte)0x12);
            OpcodeObfuscator.revertOpcodeToOriginal(array, (byte)0xb1);

            /*for (int i = 0; i < array.length; i++)
            {
                System.err.printf("array[%02X] = %02X\n", i, array[i]);
            }*/

            short[] exOpcodes = OpcodeObfuscator.shuffleEx(key);

            return new OpCodeTable(array, exOpcodes);
        }

        private static void revertOpcodeToOriginal(byte[] array, byte opcode)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == opcode)
                {
                    array[i] = array[opcode & 0xFF];
                    array[opcode & 0xFF] = opcode;
                    return;
                }
            }
        }
    }
}
