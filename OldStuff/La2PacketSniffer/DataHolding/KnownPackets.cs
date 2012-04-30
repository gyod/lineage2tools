using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using L2PacketDecrypt.Packets;

namespace La2PacketSniffer.DataHolding
{
    public class KnownPackets
    {
        private Hashtable knownServerPackets = new Hashtable();
        private Hashtable knownClientPackets = new Hashtable();
        private FileStream listFile;
        private StreamReader reader;
        private enum state { SERVER, CLIENT, UNDEF };
        private state currentState;

        /// <summary>
        /// Liest das Propertiesfile mit den Bekannten Packeten und deren Struktur ein
        /// </summary>
        /// <param name="filepath">Pfad zur Datei</param>
        public KnownPackets(string filepath)
        {
            this.currentState = state.UNDEF;
            this.listFile = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
            this.reader = new StreamReader(this.listFile);

            this.ReadFile();
            this.reader.Close();
            this.listFile.Close();
        }

        private void ReadFile()
        {
            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.StartsWith("#")) // Kommentar
                {
                    continue;
                }
                else if (line.StartsWith("[")) // Section
                {
                    if (line.Contains("server"))
                    {
                        this.currentState = state.SERVER;
                    }
                    else if (line.Contains("client"))
                    {
                        this.currentState = state.CLIENT;
                    }
                    continue;
                }
                try
                {
                    string[] tokens = line.Split(';');
                    int opcode = int.Parse(tokens[0], System.Globalization.NumberStyles.HexNumber);
                    string name = tokens[1];
                    string structure = "";
                    if (tokens.Length > 1)
                    {
                        structure = tokens[2];
                    }
                    if (this.currentState == state.SERVER)
                    {
                        if (this.knownServerPackets.ContainsKey(opcode))
                        {
                            continue;
                        }
                        this.knownServerPackets.Add(opcode, new PacketInfo(opcode, name, null));
                    }
                    else if (this.currentState == state.CLIENT)
                    {
                        if (this.knownClientPackets.ContainsKey(opcode))
                        {
                            continue;
                        }
                        this.knownClientPackets.Add(opcode, new PacketInfo(opcode, name, null));
                    }
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Mappt den Opcode in einen Aussagekräftigen String um
        /// </summary>
        /// <param name="p">Das L2Packet</param>
        /// <returns>den Namen des L2Packets</returns>
        public string GetName(L2Packet p)
        {
            string name = String.Format("0x{0:x2}", p.OpCode);
            if (p is GameServerPacket)
            {
                if (this.knownServerPackets.ContainsKey(p.OpCode))
                {
                    name = ((PacketInfo)this.knownServerPackets[p.OpCode]).name
                        + String.Format(": 0x{0:x2}", p.OpCode);
                }
            }
            else if (p is ClientPacket)
            {
                if (this.knownClientPackets.ContainsKey(p.OpCode))
                {
                    name = ((PacketInfo)this.knownClientPackets[p.OpCode]).name
                        + String.Format(": 0x{0:x2}", p.OpCode); ;
                }
            }
            return name;
        }

        public string GetName(int opCode, bool fromServer)
        {
            string name = String.Format("0x{0:x2}", opCode);
            if (fromServer)
            {
                if (this.knownServerPackets.ContainsKey(opCode))
                {
                    name = ((PacketInfo)this.knownServerPackets[opCode]).name
                        + String.Format(": 0x{0:x2}", opCode);
                }
            }
            else
            {
                if (this.knownClientPackets.ContainsKey(opCode))
                {
                    name = ((PacketInfo)this.knownClientPackets[opCode]).name
                        + String.Format(": 0x{0:x2}", opCode); ;
                }
            }
            return name;
        }

        public ArrayList GetStructure(L2Packet p)
        {
            if (p is GameServerPacket)
            {
                if (this.knownServerPackets.ContainsKey(p.OpCode))
                {
                    return ((PacketInfo)this.knownServerPackets[p.OpCode]).structure.structure;
                }
            }
            else if (p is ClientPacket)
            {
                if (this.knownClientPackets.ContainsKey(p.OpCode))
                {
                    return ((PacketInfo)this.knownClientPackets[p.OpCode]).structure.structure;
                }
            }
            return null;
        }

        internal class PacketInfo
        {
            public int opcode;
            public string name;
            public StructureInfo structure;

            public PacketInfo(int opcode, string name, StructureInfo structure)
            {
                this.opcode = opcode;
                this.name = name;
                this.structure = structure;
            }
        }

        internal class StructureInfo
        {
            public ArrayList structure = new ArrayList();
            private string strStruct;

            public StructureInfo(string structure)
            {
                this.strStruct = structure;
                this.parse();
            }

            private void parse()
            {
                // example string
                // c(opcode)h(2ndOpcode)d(itemId)d(type
            }
        }
    }
}
