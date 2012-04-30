using System;
using System.Collections.Generic;
using System.Text;

namespace L2PacketDecrypt
{
    public class ByteBuffer
    {
        private byte[] _data;
        private int _index;
        private int _length;
        private int _maxlength;
        private const int MAX_LENGTH = 0x400;

        public ByteBuffer()
        {
            this._maxlength = 0x400;
            this._data = new byte[this._maxlength];
            this._length = this._maxlength;
            this._index = 0;
        }

        public ByteBuffer(int len)
        {
            if (len > this._maxlength)
            {
                this._maxlength = len;
            }
            this._data = new byte[this._maxlength];
            this._length = len;
            this._index = 0;
        }

        public ByteBuffer(byte[] buff)
        {
            this._length = buff.Length;
            if (this._length > this._maxlength)
            {
                this._maxlength = this._length;
            }
            this._data = new byte[this._maxlength];
            this._index = 0;
            buff.CopyTo(this._data, 0);
        }

        /// <summary>
        /// Löscht den Inhalt des ByteBuffers
        /// </summary>
        public void ClearData()
        {
            for (int i = 0; i < this._maxlength; i++)
            {
                this._data[i] = 0;
            }
        }

        /// <summary>
        /// liefert eine Kopie der Daten im ByteBuffer 
        /// </summary>
        /// <returns>Eine Kopie der Daten</returns>
        public byte[] Get_ByteArray()
        {
            byte[] buffer = new byte[this._length];
            for (int i = 0; i < this._length; i++)
            {
                buffer[i] = this._data[i];
            }
            return buffer;
        }

        /// <summary>
        /// Liefert das Byte an der angegebenen Position und erhöht index NICHT
        /// </summary>
        /// <param name="ind">Position des Bytes</param>
        /// <returns></returns>
        public byte GetByte(int ind)
        {
            if (this._length >= ind)
            {
                return this._data[ind];
            }
            return 0;
        }

        /// <summary>
        /// Aktuelle Position im ByteBuffer
        /// </summary>
        /// <returns></returns>
        public int GetIndex()
        {
            return this._index;
        }

        public int Length()
        {
            return this._length;
        }

        public byte ReadByte()
        {
            if (this._length >= (this._index + 1))
            {
                byte num = this._data[this._index];
                this._index++;
                return num;
            }
            return 0;
        }

        /// <summary>
        /// Liest n bytes und erhöht index um n
        /// </summary>
        /// <param name="n">Anzahl der zu lesenden bytes</param>
        /// <returns>new byte[]</returns>
        public byte[] ReadBytes(int n)
        {
            byte[] buf = new byte[n];
            if (this._length >= (this._index + n))
            {
                Array.Copy(this._data, this._index, buf, 0, n);
                this._index += n;
            }
            return buf;
        }

        public char ReadChar()
        {
            if (this._length >= (this._index + 1))
            {
                char ch = BitConverter.ToChar(this._data, this._index);
                this._index++;
                return ch;
            }
            return '\0';
        }

        public double ReadDouble()
        {
            if (this._length >= (this._index + 8))
            {
                double num = BitConverter.ToDouble(this._data, this._index);
                this._index += 8;
                return num;
            }
            return 0.0;
        }

        public short ReadInt16()
        {
            if (this._length >= (this._index + 2))
            {
                short num = BitConverter.ToInt16(this._data, this._index);
                this._index += 2;
                return num;
            }
            return 0;
        }

        public int ReadInt32()
        {
            if (this._length >= (this._index + 4))
            {
                int num = BitConverter.ToInt32(this._data, this._index);
                this._index += 4;
                return num;
            }
            return 0;
        }

        public long ReadInt64()
        {
            if (this._length >= (this._index + 8))
            {
                long num = BitConverter.ToInt64(this._data, this._index);
                this._index += 8;
                return num;
            }
            return 0L;
        }

        public string ReadString()
        {
            try
            {
                string str = "";
                for (char ch = (char)this._data[this._index]; ch != '\0'; ch = (char)this._data[this._index])
                {
                    str = str + ch;
                    this._index += 2;
                }
                this._index += 2;
                return str;
            }
            catch
            {
                return "";
            }
        }

        public ushort ReadUInt16()
        {
            if (this._length >= (this._index + 2))
            {
                ushort num = BitConverter.ToUInt16(this._data, this._index);
                this._index += 2;
                return num;
            }
            return 0;
        }

        public uint ReadUInt32()
        {
            if (this._length >= (this._index + 4))
            {
                uint num = BitConverter.ToUInt32(this._data, this._index);
                this._index += 4;
                return num;
            }
            return 0;
        }

        public ulong ReadUInt64()
        {
            if (this._length >= (this._index + 8))
            {
                ulong num = BitConverter.ToUInt64(this._data, this._index);
                this._index += 8;
                return num;
            }
            return 0L;
        }

        public void ResetIndex()
        {
            this._index = 0;
        }

        public void Resize(int len)
        {
            if (len <= this._maxlength)
            {
                this._length = len;
            }
            if (len > this._maxlength)
            {
                byte[] array = new byte[this._length];
                this._data.CopyTo(array, this._length);
                this._maxlength = len;
                this._data = new byte[this._maxlength];
                array.CopyTo(this._data, this._length);
                this._length = this._maxlength;
            }
        }

        public void SetByte(byte b)
        {
            if (this._length >= (this._index + 1))
            {
                this._index++;
                this._data[this._index] = b;
            }
        }

        public void SetByte(byte b, int ind)
        {
            if (this._length >= ind)
            {
                this._data[ind] = b;
            }
        }

        public void SetIndex(int ind)
        {
            this._index = ind;
        }

        public void WriteByte(byte val)
        {
            if (this._length >= (this._index + 1))
            {
                this._data[this._index] = val;
                this._index++;
            }
        }

        public void WriteDouble(double val)
        {
            if (this._length >= (this._index + 8))
            {
                byte[] bytes = new byte[8];
                bytes = BitConverter.GetBytes(val);
                this._data[this._index] = bytes[0];
                this._data[this._index + 1] = bytes[1];
                this._data[this._index + 2] = bytes[2];
                this._data[this._index + 3] = bytes[3];
                this._data[this._index + 4] = bytes[4];
                this._data[this._index + 5] = bytes[5];
                this._data[this._index + 6] = bytes[6];
                this._data[this._index + 7] = bytes[7];
                this._index += 8;
            }
        }

        public void WriteInt16(short val)
        {
            if (this._length >= (this._index + 2))
            {
                byte[] bytes = new byte[2];
                bytes = BitConverter.GetBytes(val);
                this._data[this._index] = bytes[0];
                this._data[this._index + 1] = bytes[1];
                this._index += 2;
            }
        }

        public void WriteInt32(int val)
        {
            if (this._length >= (this._index + 4))
            {
                byte[] bytes = new byte[4];
                bytes = BitConverter.GetBytes(val);
                this._data[this._index] = bytes[0];
                this._data[this._index + 1] = bytes[1];
                this._data[this._index + 2] = bytes[2];
                this._data[this._index + 3] = bytes[3];
                this._index += 4;
            }
        }

        public void WriteInt64(long val)
        {
            if (this._length >= (this._index + 8))
            {
                byte[] bytes = new byte[8];
                bytes = BitConverter.GetBytes(val);
                this._data[this._index] = bytes[0];
                this._data[this._index + 1] = bytes[1];
                this._data[this._index + 2] = bytes[2];
                this._data[this._index + 3] = bytes[3];
                this._data[this._index + 4] = bytes[4];
                this._data[this._index + 5] = bytes[5];
                this._data[this._index + 6] = bytes[6];
                this._data[this._index + 7] = bytes[7];
                this._index += 8;
            }
        }

        public void WriteString(string text)
        {
            if (this._length >= ((this._index + (text.Length * 2)) + 2))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    this.WriteByte(Convert.ToByte(text[i]));
                    this.WriteByte(0);
                }
                this.WriteByte(0);
                this.WriteByte(0);
            }
        }

        public void WriteUInt16(ushort val)
        {
            if (this._length >= (this._index + 2))
            {
                byte[] bytes = new byte[2];
                bytes = BitConverter.GetBytes(val);
                this._data[this._index] = bytes[0];
                this._data[this._index + 1] = bytes[1];
                this._index += 2;
            }
        }

        public void WriteUInt32(uint val)
        {
            if (this._length >= (this._index + 4))
            {
                byte[] bytes = new byte[4];
                bytes = BitConverter.GetBytes(val);
                this._data[this._index] = bytes[0];
                this._data[this._index + 1] = bytes[1];
                this._data[this._index + 2] = bytes[2];
                this._data[this._index + 3] = bytes[3];
                this._index += 4;
            }
        }

        public void WriteUInt64(ulong val)
        {
            if (this._length >= (this._index + 8))
            {
                byte[] bytes = new byte[8];
                bytes = BitConverter.GetBytes(val);
                this._data[this._index] = bytes[0];
                this._data[this._index + 1] = bytes[1];
                this._data[this._index + 2] = bytes[2];
                this._data[this._index + 3] = bytes[3];
                this._data[this._index + 4] = bytes[4];
                this._data[this._index + 5] = bytes[5];
                this._data[this._index + 6] = bytes[6];
                this._data[this._index + 7] = bytes[7];
                this._index += 8;
            }
        }
    }
}
