using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace L2NetCore
{
    public class L2NetCore
    {
        private PacketHandler packetHandler;
        private IL2StreamProvider streamProv;
        private IL2NetCrypt serverCrypt;
        private IL2NetCrypt clientCrypt;

        private Thread workerThread = null;
        private bool threadRunning = false;

        /// <summary>
        /// ohne entschlüsslung, ohne verschlüsslung, (Zum testen...)
        /// kein senden von Packeten möglich
        /// </summary>
        public L2NetCore(IL2StreamProvider streamProv)
        {
            this.packetHandler = new PacketHandler();
            this.streamProv = streamProv;
            this.clientCrypt = new NullCryptor();
            this.serverCrypt = new NullCryptor();
        }

        /// <summary>
        /// Für Reguläre nutzung, mit ver- / entschlüsslung
        /// </summary>
        public L2NetCore(IL2StreamProvider streamProv, IL2NetCrypt clientCrypt, IL2NetCrypt serverCrypt)
        {
            this.packetHandler = new PacketHandler();
            this.streamProv = streamProv;
            this.clientCrypt = clientCrypt;
            this.serverCrypt = serverCrypt;
        }

        /// <summary>
        /// Ruft den Packethandler dieser Instanz ab
        /// </summary>
        public PacketHandler PacketHandler
        {
            get
            {
                return this.packetHandler;
            }
        }

        /// <summary>
        /// Startet L2NetCore.
        /// </summary>
        public void Start()
        {
            this.workerThread = new Thread(l2netCoreThread);
            this.workerThread.IsBackground = true;
            this.workerThread.Name = "Main l2NetCore Thread";
            this.threadRunning = true;
            this.workerThread.Start();
        }

        public void SendToServer(SendablePacket packet)
        {
            if (this.streamProv.ProviderType == L2NetMode.Sniffer || this.serverCrypt is NullCryptor)
                return;

            byte[] tmp = packet.GetPacket();
            this.serverCrypt.EnCrypt(ref tmp, tmp.Length, 2, PacketType.ClientToGameserver);
            this.streamProv.ServerStreamOut.AddData(tmp);
        }

        public void SendToClient(SendablePacket packet)
        {
            if (this.streamProv.ProviderType != L2NetMode.Proxy || this.clientCrypt is NullCryptor)
                return;

            byte[] tmp = packet.GetPacket();
            this.clientCrypt.EnCrypt(ref tmp, tmp.Length, 2, PacketType.GameserverToClient);
            this.streamProv.ClientStreamOut.AddData(tmp);
        }

        public void Stop()
        {
            this.threadRunning = false;
        }

        private void l2netCoreThread()
        {
            while (this.threadRunning)
            {
                // mainloop
                
            }
        }
    }
}
