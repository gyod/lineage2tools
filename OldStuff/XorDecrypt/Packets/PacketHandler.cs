using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace L2PacketDecrypt.Packets
{
    class PacketHandler
    {
        private Queue<byte[]> buffer = new Queue<byte[]>(4);

        public void handlePacket(byte[] packet)
        {
            ByteBuffer rawBuffer = new ByteBuffer(packet);
            // Check if the size is the realsize
            short size = rawBuffer.ReadInt16();
            rawBuffer.SetIndex(0);
            if (size < rawBuffer.Length())
            {
                byte[] pck1;
                pck1 = rawBuffer.ReadBytes(size);
                buffer.Enqueue(pck1);
                short size2;
                size2 = rawBuffer.ReadInt16();
                rawBuffer.SetIndex(size);
                byte[] pck2;
                pck2 = rawBuffer.ReadBytes(size2);
                buffer.Enqueue(pck2);
            }
            else
            {
                buffer.Enqueue(packet);
            }
        }

        public Queue<byte[]> BufferQueue
        {
            get
            {
                return buffer;
            }
        }
    }
}
