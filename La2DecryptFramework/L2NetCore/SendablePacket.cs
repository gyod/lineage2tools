using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public abstract class SendablePacket
    {
        private Bytebuffer buffer;
        private int size = 0;

        public SendablePacket()
        {
            this.buffer = new Bytebuffer();
        }

        public void WriteByte(byte data)
        {
            this.buffer.Write(data);
        }

        public void WriteBytes(byte[] data)
        {
            this.buffer.Write(data);
        }

        public void WriteShort(short data)
        {
            this.buffer.Write(data);
        }

        public void WriteInt(int data)
        {
            this.buffer.Write(data);
        }

        public void WriteLong(long data)
        {
            this.buffer.Write(data);
        }

        public void WriteDouble(double data)
        {
            this.buffer.Write(data);
        }

        public void WriteString(string data)
        {
            this.buffer.Write(data);
        }

        /// <summary>
        /// liefert das Packet als bytearray zurück. Inklusive längenangabe in den ersten 2bytes. Also fertig zum verschicken.
        /// </summary>
        /// <returns></returns>
        public byte[] GetPacket()
        {
            byte[] tmp = this.buffer.ToArray();
            byte[] packet = new byte[tmp.Length + 2];
            BitConverter.GetBytes((ushort)tmp.Length).CopyTo(packet, 0);
            Array.Copy(tmp, 0, packet, 2, tmp.Length);
            return packet;
        }
    }
}
