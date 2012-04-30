using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace L2PacketDecrypt.Packets
{
    /// <summary>
    /// Stellt einen dynamischen RingPuffer als Stream dar,
    /// in den bytearray geschrieben und gelesen werden können.
    /// Gelesene bytes automatisch entfernt.
    /// </summary>
    public class L2PacketStream : Stream
    {
        private const int MAX_SIZE = 0x1000; // 0x1000 = 4Kb
        private Queue<byte> dataQueue;

        public L2PacketStream()
        {
            this.dataQueue = new Queue<byte>(MAX_SIZE);
        }

        public L2PacketStream(byte[] data)
        {
            this.dataQueue = new Queue<byte>(data.Length);
            foreach (byte b in data)
            {
                dataQueue.Enqueue(b);
            }
        }

        public byte GetByte()
        {
            return this.dataQueue.Dequeue();
        }

        public byte Peek()
        {
            return this.dataQueue.Peek();
        }

        /// <summary>
        /// liefert das nächste L2Packet als ByteArray zurück und entfernt es aus dem L2PacketBuffer.
        /// Wenn keine Packets mehr im Buffer sind gibts ne EndOfStreamException
        /// </summary>
        /// <exception cref="EndOfStreamException"></exception>
        /// <returns></returns>
        public byte[] ReadPacket()
        {
            if (!this.MorePackets())
                throw new EndOfStreamException("Kein weiteres Packet vorhanden");

            int size = this.getNextPacketSize();
            byte[] tmp = new byte[size];

            for (int i = 0; i < size; i++)
            {
                tmp[i] = this.dataQueue.Dequeue();
            }
            return tmp;
        }

        protected void AddData(byte[] data)
        {
            foreach (byte b in data)
            {
                this.dataQueue.Enqueue(b);
            }
        }

        /// <summary>
        /// Gibt an ob sich noch ein komplettes Packet im Puffer befindet
        /// </summary>
        /// <returns>true, wenn Packet vorhanden</returns>
        public bool MorePackets()
        {
            return this.getNextPacketSize() < this.dataQueue.Count;
        }

        // gibt die nächsten 2 Bytes zurück ohne sie aus der Queue zu entfernen
        private int getNextPacketSize()
        {
            byte[] tmp = new byte[2];
            int i = 0;
            foreach (byte b in this.dataQueue)
            {
                tmp[i++] = b;
                if (i == 2)
                    return BitConverter.ToUInt16(tmp, 0);
            }
            return BitConverter.ToUInt16(tmp, 0);
        }

        // Streammember
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            this.dataQueue.Clear();
        }

        public override long Length
        {
            get { return this.dataQueue.Count; }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException("The method or operation is not Supported.");
            }
            set
            {
                throw new NotSupportedException("The method or operation is not Supported.");
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int i = 0;
            for (i = offset; i < count && i < this.dataQueue.Count; i++)
            {
                buffer[i] = this.dataQueue.Dequeue();
            }
            if (this.dataQueue.Count == 0)
            {
                i = 0;
            }
            return i;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("The method or operation is not Supported.");
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("The method or operation is not Supported.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            for (int i = offset; i < count; i++)
            {
                this.dataQueue.Enqueue(buffer[i]);
            }
        }
    }
}
