using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public abstract class ReadablePacket
    {
        private Bytebuffer buffer;

        public ReadablePacket(Bytebuffer buffer)
        {
            this.buffer = buffer;
        }

        public ReadablePacket(byte[] buffer)
        {
            this.buffer = new Bytebuffer(buffer);
        }

        public byte ReadByte()
        {
            return (byte)this.buffer.ReadByte();
        }

        public short ReadShort()
        {
            return this.buffer.ReadShort();
        }

        public int ReadInt()
        {
            return this.buffer.ReadInt();
        }

        public long ReadLong()
        {
            return this.buffer.ReadLong();
        }

        public double ReadDouble()
        {
            return this.buffer.ReadDouble();
        }

        public byte[] ReadBytes(int count)
        {
            return this.buffer.ReadBytes(count);
        }

        public string ReadString()
        {
            return this.buffer.ReadString();
        }
    }
}
