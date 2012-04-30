using System;
using System.Collections.Generic;
using System.Text;

namespace L2NetCore
{
    public class PacketHandler
    {
        private Dictionary<short, IRunablePacket> clientToGs = new Dictionary<short, IRunablePacket>();
        private Dictionary<short, IRunablePacket> clientToLs = new Dictionary<short, IRunablePacket>();
        private Dictionary<short, IRunablePacket> gsToClient = new Dictionary<short, IRunablePacket>();
        private Dictionary<short, IRunablePacket> lsToClient = new Dictionary<short, IRunablePacket>();


        public void AddPacketHandler(IRunablePacket packet)
        {
            switch (packet.Type)
            {
                case PacketType.ClientToGameserver:
                    clientToGs.Add(packet.OpCode, packet);
                    break;
                case PacketType.ClientToLoginserver:
                    clientToLs.Add(packet.OpCode, packet);
                    break;
                case PacketType.GameserverToClient:
                    gsToClient.Add(packet.OpCode, packet);
                    break;
                case PacketType.LoginserverToClient:
                    lsToClient.Add(packet.OpCode, packet);
                    break;
            }
        }

        public void RemovePacketHandler(short opCode, PacketType type)
        {
            switch (type)
            {
                case PacketType.ClientToGameserver:
                    clientToGs.Remove(opCode);
                    break;
                case PacketType.ClientToLoginserver:
                    clientToLs.Remove(opCode);
                    break;
                case PacketType.GameserverToClient:
                    gsToClient.Remove(opCode);
                    break;
                case PacketType.LoginserverToClient:
                    lsToClient.Remove(opCode);
                    break;
            }
        }

        public void HandlePacket(IRunablePacket packet)
        {
            switch (packet.Type)
            {
                case PacketType.ClientToGameserver:
                    HandleClientGameserverPacket(packet);
                    break;
                case PacketType.ClientToLoginserver:
                    HandleClientLoginserverPacket(packet);
                    break;
                case PacketType.GameserverToClient:
                    HandleGameserverPacket(packet);
                    break;
                case PacketType.LoginserverToClient:
                    HandleLoginserverPacket(packet);
                    break;
            }
        }

        private void HandleGameserverPacket(IRunablePacket packet)
        {
            if (gsToClient.ContainsKey(packet.OpCode))
            {
                gsToClient[packet.OpCode].RunPacket();
            }
        }

        private void HandleLoginserverPacket(IRunablePacket packet)
        {
            if (lsToClient.ContainsKey(packet.OpCode))
            {
                lsToClient[packet.OpCode].RunPacket();
            }
        }

        private void HandleClientLoginserverPacket(IRunablePacket packet)
        {
            if (clientToLs.ContainsKey(packet.OpCode))
            {
                clientToLs[packet.OpCode].RunPacket();
            }
        }

        private void HandleClientGameserverPacket(IRunablePacket packet)
        {
            if (clientToGs.ContainsKey(packet.OpCode))
            {
                clientToGs[packet.OpCode].RunPacket();
            }
        }
    }
}
