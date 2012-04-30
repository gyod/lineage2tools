using L2PacketDecrypt;
using L2PacketDecrypt.Packets;

namespace SpoilStatus
{
    class L2Plegde
    {
        private int _clanId;
        private string _clanName;
        private int _allyId;

        public int ClanId
        {
            get { return _clanId; }
        }

        public string ClanName
        {
            get { return _clanName; }
        }

        public int AllyId
        {
            get { return _allyId; }
        }

        public L2Plegde(GameServerPacket packet)
        {
            this.parsePacket(packet.Data);
        }

        private void parsePacket(ByteBuffer data)
        {
            data.SetIndex(3);
            this._clanId = data.ReadInt32();
            this._clanName = data.ReadString();
            this._allyId = data.ReadInt32();
        }
    }
}
