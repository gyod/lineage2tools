using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using L2Proxy.Crypt;

namespace L2Proxy
{
    public class Proxy
    {
        private static int lastPort = 7777;

        // Loginserver
        private Connection loginClientCon;
        private Connection loginServerCon;

        private IPEndPoint loginListeningEndPoint;
        private IPEndPoint loginDestinationEndPoint;

        private Thread loginProxyThread;
        private bool loginRunning = false;

        private LoginCrypt loginCryptServer = new LoginCrypt();
        private LoginCrypt loginCryptClient = new LoginCrypt();

        // Gameserver
        private IPAddress gsListeningIP;

        private Connection gameClientCon;
        private Connection gameServerCon;

        private Hashtable gameListeningEndPoints = new Hashtable();
        private Hashtable gameDestinationEndPoints = new Hashtable();

        private IPEndPoint gameClientEp;
        private IPEndPoint gameServerEp;

        private Thread gameProxyThread;
        private bool gameRunning = false;

        private GameCrypt gameCryptServer = new GameCrypt();
        private GameCrypt gameCryptClient = new GameCrypt();

        // Filterzeugs
        private List<IPacketFilter> filter = new List<IPacketFilter>();

        public Proxy(IPEndPoint loginListeningEndPoint, IPEndPoint loginDestinationEndPoint, IPAddress gsListeningIP)
        {
            // Standartfilter hinzufügen, sonst geht gar nix ^^
            this.filter.Add(new Nullfilter());

            this.loginListeningEndPoint = loginListeningEndPoint;
            this.loginDestinationEndPoint = loginDestinationEndPoint;
            this.gsListeningIP = gsListeningIP;

            // Start Thread
            this.loginProxyThread = new Thread(loginProxyWorker);
            this.loginRunning = true;
            this.loginProxyThread.IsBackground = true;
            this.loginProxyThread.Start();
        }

        private void loginProxyWorker()
        {
            TcpListener clientListener = new TcpListener(this.loginListeningEndPoint);
            Console.Out.WriteLine("Warte auf Verbindung...");
            clientListener.Start();
            try
            {
                TcpClient client = clientListener.AcceptTcpClient();
                this.loginClientCon = new Connection(client, this.loginCryptClient);
                Console.Out.WriteLine("Client auf Login verbunden.");

                TcpClient serverClient = new TcpClient(this.loginDestinationEndPoint.Address.ToString()
                    , this.loginDestinationEndPoint.Port);
                Console.Out.WriteLine("Zu Loginserver verbunden.");
                this.loginServerCon = new Connection(serverClient, this.loginCryptServer);

                // Start to work! ^^

                while (this.loginRunning)
                {
                    // send ClientReadQueue to ServerSendQueue
                    L2BasePacket fromClient = this.loginClientCon.GetPacket();
                    if (fromClient != null)
                    {
                        // Handle Packets here
                        switch (fromClient.GetOpcode())
                        {
                            case 0x02: // RequestServerLogin, Start gameProxyWorker!
                                Console.Out.WriteLine("ClientPacket::RequestServerLogin, starting gameProxy");
                                this.OnRequestServerLogin(fromClient.Data);
                                break;
                        }
                        this.loginServerCon.Send(fromClient);
                    }

                    // send ServerReadQueue to ClientSendQueue
                    L2BasePacket fromServer = this.loginServerCon.GetPacket();
                    if (fromServer != null)
                    {
                        // Handle Packets here
                        switch (fromServer.GetOpcode())
                        {
                            case 0x00: // Init
                                Console.Out.WriteLine("LoginServerpacket::Init");
                                this.handleInit(fromServer.Data);
                                break;
                            case 0x04: // Serverlist
                                Console.Out.WriteLine("LoginServerpacket::Serverlist");
                                // Modify the Serverlist
                                this.modifyServerlist(fromServer.Data);
                                break;
                        }
                        this.loginClientCon.Send(fromServer);
                    }

                    // Sleep
                    Thread.Sleep(1);
                }
                Console.Out.WriteLine("LoginProxyThread Beendet");
                client.Close();
                serverClient.Close();

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        private void gameProxyWorker()
        {
            TcpListener clientListener = new TcpListener(this.gameClientEp);
            Console.Out.WriteLine("\nWarte auf Verbindung vom GameClient");
            clientListener.Start();
            try
            {
                TcpClient client = clientListener.AcceptTcpClient();

                //this.gameClientCon = new Connection(client, this.gameCryptClient);
                this.gameClientCon = new Connection(client, new NullCrypt());

                Console.Out.WriteLine("Client auf Game verbunden.");

                TcpClient serverClient = new TcpClient(this.gameServerEp.Address.ToString()
                    , this.gameServerEp.Port);
                Console.Out.WriteLine("Zu Gameserver verbunden.");

                //this.gameServerCon = new Connection(serverClient, this.gameCryptServer);
                this.gameServerCon = new Connection(serverClient, new NullCrypt());

                // Work here
                while (this.gameRunning)
                {
                    L2BasePacket fromClient = this.gameClientCon.GetPacket();
                    if (fromClient != null)
                    {
                        fromClient.FromServer = false;
                        this.applyFilters(fromClient);

                        // Handle Packets here
                        switch (fromClient.GetOpcode())
                        {
                            case 0x00:
                                break;
                        }
                        this.gameServerCon.Send(fromClient);
                    }

                    // send ServerReadQueue to ClientSendQueue
                    L2BasePacket fromServer = this.gameServerCon.GetPacket();
                    if (fromServer != null)
                    {
                        fromServer.FromServer = true;
                        this.applyFilters(fromServer);

                        // Handle Packets here
                        switch (fromServer.GetOpcode())
                        {
                            case 0x2e:
                                this.onKeyPacket(fromServer.Data);
                                // We should now stop the loginProxyThread
                                this.StopLogin();
                                break;
                        }
                        this.gameClientCon.Send(fromServer);
                    }

                    // Sleep
                    Thread.Sleep(1);
                }
                Console.Out.WriteLine("GameProxyThread Beendet");
                client.Close();
                serverClient.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        public void StopLogin()
        {
            this.loginRunning = false;
            try
            {
                this.loginClientCon.StopThreads();
                this.loginServerCon.StopThreads();
            }
            catch { }
            try
            {
                this.loginProxyThread.Abort();
            }
            catch { }
        }

        public void StopGame()
        {
            this.gameRunning = false;
            try
            {
                this.gameClientCon.StopThreads();
                this.gameServerCon.StopThreads();
            }
            catch { }
            try
            {
                this.gameProxyThread.Abort();
            }
            catch { }
        }

        private void onKeyPacket(ByteBuffer packet)
        {
            packet.SetIndex(5);
            byte[] cryptKey = packet.ReadBytes(16);
            // Kameal Hellbound PTS fix
            cryptKey[8] = (byte)0xc8;
            cryptKey[9] = (byte)0x27;
            cryptKey[10] = (byte)0x93;
            cryptKey[11] = (byte)0x01;
            cryptKey[12] = (byte)0xa1;
            cryptKey[13] = (byte)0x6c;
            cryptKey[14] = (byte)0x31;
            cryptKey[15] = (byte)0x97;
            // End fix
            gameCryptServer.SetKey(cryptKey);
            gameCryptClient.SetKey(cryptKey);
            Console.Out.WriteLine("Got Key xD");
        }

        private void OnRequestServerLogin(ByteBuffer packet)
        {
            // start and setup the gameProxy
            packet.SetIndex(11);
            int id = packet.ReadByte();
            string tmp = id.ToString();

            bool b = this.gameListeningEndPoints.ContainsKey(tmp);
            this.gameClientEp = (IPEndPoint)this.gameListeningEndPoints[tmp];
            this.gameServerEp = (IPEndPoint)this.gameDestinationEndPoints[tmp];

            this.gameProxyThread = new Thread(gameProxyWorker);
            this.gameRunning = true;
            this.gameProxyThread.IsBackground = true;
            this.gameProxyThread.Start();
        }

        private void handleInit(ByteBuffer packet)
        {
            packet.SetIndex(3);
            uint sessionId = packet.ReadUInt32();
            uint protocolVer = packet.ReadUInt32();
            byte[] publicKey = packet.ReadBytes(0x80);
            // 4x read D (GameGuard);
            for (int i = 0; i < 4; i++)
                packet.ReadUInt32();
            byte[] blowfishKey = packet.ReadBytes(0x10);

            // set new key in LoginCrypt
            this.loginCryptServer.SetKey(blowfishKey);
            this.loginCryptClient.SetKey(blowfishKey);
        }

        private void modifyServerlist(ByteBuffer packet)
        {
            // ZielP und Port in gameDestinationEndPoint schreiben
            // Lokal nach freiem Port suchen und gameListeningEndPoint erstellen
            packet.SetIndex(3);
            int serverCount = packet.ReadByte();
            int lastserver = packet.ReadByte();
            List<GameServerInfo> serverList = new List<GameServerInfo>();
            for (int i = serverCount; i <= serverCount; i++)
            {
                byte id = packet.ReadByte();
                byte[] ip = packet.ReadBytes(4);
                int port = packet.ReadInt32();
                packet.ReadBytes(12); // read 12 bytes (cchhcdc)
                serverList.Add(new GameServerInfo(new IPAddress(ip), port, id));
            }
            packet.SetIndex(5);
            
            foreach (GameServerInfo info in serverList)
            {
                this.gameDestinationEndPoints.Add(info.id.ToString(), new IPEndPoint(new IPAddress(info.adress.GetAddressBytes()), info.port));
                this.gameListeningEndPoints.Add(info.id.ToString(), new IPEndPoint(this.gsListeningIP, this.getNextFreePort()));
                packet.WriteByte(info.id);
                packet.WriteBytes(gsListeningIP.GetAddressBytes());
                packet.WriteInt32(lastPort);
                packet.SetIndex(packet.GetIndex() + 12);
            }

        }

        private int getNextFreePort()
        {
            while (!this.checkPort(lastPort))
            {
                lastPort++;
            }
            return lastPort;
        }

        private bool checkPort(int port)
        {
            bool result = true;
            TcpListener testListener = new TcpListener(this.gsListeningIP, port);
            try
            {
                testListener.Start();
                testListener.Stop();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Fügt einen Filter an der letzten stelle ein, die Reihenfolge ist gleichzeitig die Priorität
        /// </summary>
        /// <param name="filter"></param>
        public void AddFilter(IPacketFilter filter)
        {
            this.filter.Add(filter);
        }

        public void AddFilter(IPacketFilter filter, int position)
        {
            this.filter.Insert(position, filter);
        }

        public void MoveFilter(IPacketFilter filter, int newPosition)
        {
            this.filter.Remove(filter);
            this.filter.Insert(newPosition, filter);
        }

        public void RemoveFilter(IPacketFilter filter)
        {
            this.filter.Remove(filter);
        }

        public List<IPacketFilter> GetAllFilter()
        {
            return this.filter;
        }

        /// <summary>
        /// Sendet ein selbst erstelltes Packet zum Server
        /// </summary>
        /// <param name="packet"></param>
        public void SendPacketToServer(L2BasePacket packet)
        {
            this.gameServerCon.Send(packet);
        }

        /// <summary>
        /// Sendet ein selbst erstelltes Packet zum Clienten
        /// </summary>
        /// <param name="packet"></param>
        public void SendPacketToClient(L2BasePacket packet)
        {
            this.gameClientCon.Send(packet);
        }

        /// <summary>
        /// Wendet alle Filter der Reihe nach an
        /// </summary>
        /// <param name="packet"></param>
        private void applyFilters(L2BasePacket packet)
        {
            foreach (IPacketFilter f in filter)
            {
                f.FilterPacket(packet);
            }
        }

        /// <summary>
        /// Gibt einen Alternativen Output anstatt der Konsole an
        /// </summary>
        /// <param name="newOut"></param>
        public void SetStdOut(System.IO.TextWriter newOut)
        {
            Console.SetOut(newOut);
        }

        /// <summary>
        /// Holding GameServer Infos for rewriting the Serverlist
        /// </summary>
        private class GameServerInfo
        {
            public IPAddress adress;
            public int port;
            public byte id;

            public GameServerInfo(IPAddress adress, int port, byte id)
            {
                this.adress = adress;
                this.port = port;
                this.id = id;
            }
        }
    }
}
