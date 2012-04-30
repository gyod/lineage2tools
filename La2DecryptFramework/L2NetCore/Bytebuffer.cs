using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace L2NetCore
{
    public class Bytebuffer : MemoryStream
    {
        private int packetSize = 0;

        public Bytebuffer(byte[] buffer)
            : base(buffer)
        {
            this.packetSize = buffer.Length;
        }

        public Bytebuffer()
            : base()
        { }

        public void Write(int data)
        {
            byte[] tmp = BitConverter.GetBytes(data);
            this.Write(tmp, 0, tmp.Length);
            this.packetSize += tmp.Length;
        }

        public void Write(long data)
        {
            byte[] tmp = BitConverter.GetBytes(data);
            this.Write(tmp, 0, tmp.Length);
            this.packetSize += tmp.Length;
        }

        public void Write(short data)
        {
            byte[] tmp = BitConverter.GetBytes(data);
            this.Write(tmp, 0, tmp.Length);
            this.packetSize += tmp.Length;
        }

        public void Write(double data)
        {
            byte[] tmp = BitConverter.GetBytes(data);
            this.Write(tmp, 0, tmp.Length);
            this.packetSize += tmp.Length;
        }

        public void Write(byte data)
        {
            this.WriteByte(data);
            this.packetSize++;
        }

        public void Write(byte[] data)
        {
            this.Write(data, 0, data.Length);
            this.packetSize += data.Length;
        }

        public void Write(string data)
        {
            int ln = 0;
            for (int i = 0; i < data.Length; i++)
            {
                this.WriteByte(Convert.ToByte(data[i]));
                this.WriteByte(0);
                ln += 2;
            }
            this.WriteByte(0);
            this.WriteByte(0);
            ln += 2;

            this.packetSize += ln;
        }

        public byte[] ReadBytes(int lenght)
        {
            byte[] tmp = new byte[lenght];
            this.Read(tmp, 0, lenght);
            return tmp;
        }

        public int ReadInt()
        {
            byte[] tmp = new byte[4];
            this.Read(tmp, 0, 4);
            return BitConverter.ToInt32(tmp, 0);
        }

        public short ReadShort()
        {
            byte[] tmp = new byte[2];
            this.Read(tmp, 0, 2);
            return BitConverter.ToInt16(tmp, 0);
        }

        public long ReadLong()
        {
            byte[] tmp = new byte[8];
            this.Read(tmp, 0, 8);
            return BitConverter.ToInt64(tmp, 0);
        }

        public double ReadDouble()
        {
            byte[] tmp = new byte[8];
            this.Read(tmp, 0, 8);
            return BitConverter.ToDouble(tmp, 0);
        }

        public string ReadString()
        {
            StringBuilder str = new StringBuilder();
            char ch;
            do
            {
                ch = (char)ReadByte();
                str.Append(ch);
            } while ((ch = (char)ReadByte()) != '0');

            return str.ToString();
        }

        /// <summary>
        /// gibt den nutzinhalt des buffers zurück, unabhängig von der Position im stream
        /// </summary>
        /// <returns></returns>
        public override byte[] ToArray()
        {
            byte[] tmp = new byte[this.packetSize];
            byte[] org = base.ToArray();

            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = org[i];
            }
            return tmp;
        }
    }
}
