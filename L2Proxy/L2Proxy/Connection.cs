using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using L2Proxy.Crypt;

namespace L2Proxy
{
    class Connection
    {
        private TcpClient connection;
        private BinaryReader connectionReader;
        private BinaryWriter connectionWriter;

        private Thread readThread;
        private Thread sendThread;
        private bool running = false;

        private Queue<L2BasePacket> readQueue = new Queue<L2BasePacket>();
        private Queue<L2BasePacket> sendQueue = new Queue<L2BasePacket>();

        private ICrypt cryptor;

        /// <summary>
        /// Stellt eine Verbindung zwischen zwei Endpunkten dar. In diesem Fall zwischen L2Server und L2Client
        /// </summary>
        /// <param name="connection">Der TcpClient der die Verbindung hält</param>
        /// <param name="cryptor">Der Cryptor der für ent-/verschlüsslung zuständig ist</param>
        public Connection(TcpClient connection, ICrypt cryptor)
        {
            this.connection = connection;
            this.connectionReader = new BinaryReader(this.connection.GetStream());
            this.connectionWriter = new BinaryWriter(this.connection.GetStream());

            this.cryptor = cryptor;

            // start the Threads
            this.running = true;

            this.readThread = new Thread(readThreadWorker);
            this.readThread.IsBackground = true;
            this.readThread.Start();

            this.sendThread = new Thread(sendThreadWorker);
            this.sendThread.IsBackground = true;
            this.sendThread.Start();
        }

        private void readThreadWorker()
        {
            ushort totalsize = 0;
            bool complete = true;
            byte[] buffer;

            while (this.running && this.connection.Connected)
            {
                // Daten zum lesen vorhanden?
                if (this.connection.Available > 2)
                {
                    if (complete)
                    {
                        complete = false;
                        totalsize = this.connectionReader.ReadUInt16();

                        // ist das nächste Packet komplett im Empfangspuffer?
                        if (this.connection.Available >= totalsize - 2)
                        {
                            // buffer Array erstellen
                            buffer = new byte[totalsize];
                            this.connectionReader.ReadBytes(totalsize - 2).CopyTo(buffer, 2);

                            // Alles empfangen
                            complete = true;

                            // decrypt
                            cryptor.Decrypt(buffer, 2, buffer.Length);

                            // syncronize
                            lock (this.readQueue)
                            {
                                // der ReadQueue hinzufügen
                                this.readQueue.Enqueue(new L2BasePacket(buffer));
                            }
                        }
                    }
                    else
                    {
                        // packet endlich komplett im Empfangspuffer?
                        if (this.connection.Available >= totalsize - 2)
                        {
                            buffer = new byte[totalsize];
                            this.connectionReader.ReadBytes(totalsize - 2).CopyTo(buffer, 2);

                            cryptor.Decrypt(buffer, 0, buffer.Length);

                            complete = true;
                            // syncronize
                            lock (this.readQueue)
                            {
                                this.readQueue.Enqueue(new L2BasePacket(buffer));
                            }
                        }
                    }
                }
                try
                {
                    Thread.Sleep(1);
                }
                catch (ThreadInterruptedException)
                {
                    // alles bestens
                }
            }
        }

        private void sendThreadWorker()
        {
            while (this.running && this.connection.Connected)
            {
                while (this.sendQueue.Count > 0)
                {
                    // syncronize
                    lock (this.sendQueue)
                    {
                        this.connectionWriter.Write(this.sendQueue.Dequeue().Data.Get_ByteArray());
                    }
                }
                try
                {
                    Thread.Sleep(1);
                }
                catch (ThreadInterruptedException)
                {
                    // alles bestens
                }
                //this.readThread.Interrupt();
            }
        }

        /// <summary>
        /// Liefert das nächste L2Basepacket aus der ReadQueue zurück und entfernt es aus der Readqueue.
        /// Ist kein L2BasePacket vorhanden wird null zurückgeliefert!
        /// </summary>
        /// <returns>Das L2BasePacket oder null</returns>
        public L2BasePacket GetPacket()
        {
            L2BasePacket p = null;
            if (this.readQueue.Count > 0)
            {
                // syncronize
                lock (this.readQueue)
                {
                    p = this.readQueue.Dequeue();
                }
            }
            return p;
        }

        public void StopThreads()
        {
            this.running = false;
        }

        public void Send(L2BasePacket packet)
        {
            byte[] size = BitConverter.GetBytes((uint)packet.Data.Length());
            packet.Data[0] = size[0];
            packet.Data[1] = size[1];

            // encrypt the packet
            this.cryptor.Encrypt(packet.Data.GetData(), 2, packet.Data.Length());

            // syncronize
            lock (this.sendQueue)
            {
                this.sendQueue.Enqueue(packet);
            }
            // Wake up the SendThread
            //this.sendThread.Interrupt();
        }
    }
}
