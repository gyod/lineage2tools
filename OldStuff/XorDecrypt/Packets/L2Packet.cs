using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace L2PacketDecrypt.Packets
{
    [XmlInclude(typeof(ClientPacket))]
    [XmlInclude(typeof(GameServerPacket))]
    [XmlInclude(typeof(LoginServerPacket))]
    public abstract class L2Packet
    {
        protected ByteBuffer data;
        protected int packetNo;

        protected L2Packet(ByteBuffer data)
        {
            this.data = data;
        }

        protected L2Packet()
        {
        }

        public abstract override string ToString();

        public abstract int OpCode
        {
            get;
        }
        
        /// <summary>
        /// Setzt oder liest den Bytebuffer direkt
        /// </summary>
        public ByteBuffer Data
        {
            get
            {
                return data;
            }
            set
            {
                this.data = value;
            }
        }
        /// <summary>
        /// Für Serilisation. konvertiert byte[] zu String und umgekehrt
        /// </summary>
        [XmlElement("data")]
        public string ByteData
        {
            get
            {
                byte[] tmp = this.data.Get_ByteArray();
                StringBuilder strBuilder = new StringBuilder(tmp.Length*2);
                foreach (byte b in tmp)
                {
                    strBuilder.AppendFormat("{0:X2}", b);
                }
                return strBuilder.ToString();
            }
            set
            {
                char[] tmp = value.ToCharArray();
                byte[] tmpArr = new byte[tmp.Length / 2];
                int j = 0;
                for (int i = 0; i < tmp.Length; i+=2)
                {
                    tmpArr[j++] = byte.Parse((tmp[i] + "" + tmp[i + 1]), System.Globalization.NumberStyles.HexNumber);
                }
                this.data = new ByteBuffer(tmpArr);
            }
        }

        public int Length
        {
            get
            {
                return this.data.Length();
            }
        }

        public string Source
        {
            get
            {
                if (this is GameServerPacket)
                {
                    return "Server";
                }
                else
                {
                    return "Client";
                }
            }
        }

        [XmlAttribute("number")]
        public int PacketNo
        {
            get
            {
                return this.packetNo;
            }
            set
            {
                this.packetNo = value;
            }
        }

    }
}
