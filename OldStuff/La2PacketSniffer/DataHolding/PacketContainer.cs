using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using L2PacketDecrypt.Packets;
using System.Xml.Serialization;

namespace La2PacketSniffer.DataHolding
{
    [XmlRoot("La2PacketSniffer")]
    public class PacketContainer
    {
        // Alle Packete
        private List<L2Packet> packetList = new List<L2Packet>();
        // nur die Anzuzeigenden
        private List<L2Packet> diplayedList = new List<L2Packet>();

        private Hashtable filter = new Hashtable();
        //private Dictionary<FilterItem, FilterItem> filter = new Dictionary<FilterItem, FilterItem>();

        public void AddPacket(L2Packet p)
        {
            this.packetList.Add(p);
            //wenn nicht im Filter, auch der Anzeigenden liste hinzufügen
            if (!this.filter.ContainsKey(new FilterItem(p.OpCode, (p is GameServerPacket)).GetHashCode()))
            {
                this.diplayedList.Add(p);
            }
        }

        [XmlIgnore]
        public List<L2Packet> DisplayedPackets
        {
            get
            {
                return this.diplayedList;
            }
        }

        public void AddFilter(int opcode, bool fromServer)
        {
            FilterItem fItem = new FilterItem(opcode, fromServer);
            if (this.filter.ContainsKey(fItem.GetHashCode()))
            {
                return;
            }
            this.filter.Add(fItem.GetHashCode(), fItem);

            this.applyFilter();
        }

        public void RemoveFilter(int opcode, bool fromServer)
        {
            FilterItem fItem = new FilterItem(opcode, fromServer);
            this.filter.Remove(fItem.GetHashCode());

            this.applyFilter();
        }

        public void applyFilter()
        {
            // Clear the old list
            this.diplayedList.Clear();
            // neue Größe schonmal festlegen
            this.diplayedList.Capacity = this.packetList.Count;
            FilterItem f;
            foreach (L2Packet p in this.packetList)
            {
                f = new FilterItem(p.OpCode, (p is GameServerPacket));
                if (this.filter.ContainsKey(f.GetHashCode()))
                {
                    continue;
                }
                this.diplayedList.Add(p);
            }
        }

        [XmlElement("packet")]
        public List<L2Packet> Packets
        {
            get
            {
                return this.packetList;
            }
            set
            {
                this.packetList = value;
            }
        }

        public int CapturedPackets()
        {
            return this.packetList.Count;
        }

        public void ClearFilter()
        {
            this.filter.Clear();
            this.applyFilter();
        }

        [XmlIgnore]
        public Hashtable Filter
        {
            get { return filter; }
        }

        [XmlIgnore]
        public List<FilterItem> EnumFilter
        {
            get
            {
                List<FilterItem> fi = new List<FilterItem>();
                foreach (DictionaryEntry de in this.filter)
                {
                    fi.Add((FilterItem)de.Value);
                }
                return fi;
            }
        }

        public class FilterItem
        {
            private int opCode;
            public int OpCode
            {
                get { return opCode; }
            }

            private bool fromServer;

            public bool FromServer
            {
                get { return fromServer; }
            }

            public FilterItem(int opCode, bool fromServer)
            {
                this.opCode = opCode;
                this.fromServer = fromServer;
            }

            public override int GetHashCode()
            {
 	             return this.opCode.GetHashCode() ^ this.fromServer.GetHashCode();
            }
        }
    }
}
