using L2PacketDecrypt;
using L2PacketDecrypt.Packets;

namespace SpoilStatus
{
    class L2Pc
    {
        public L2Pc(GameServerPacket packet)
        {
            this.parsePacket(packet.Data);
        }
        #region CharInfo Fields

        private int _x;
        private int _y;
        private int _z;
        private int _objectId;
        private string _visibleName;
        private int _race;
        private int _sex;
        private int _class;

        // Paperdolls
        private int _paperdollHairall;
        private int _paperdollHead;
        private int _paperdollRhand;
        private int _paperdollLhand;
        private int _paperdollGloves;
        private int _paperdollChest;
        private int _paperdollLegs;
        private int _paperdollFeet;
        private int _paperdollBack;
        private int _paperdollLRhand;
        private int _paperdollHair;
        private int _paperdollHair2;

        // Stats
        private int _pvpFlag;
        private int _karma;
        private int _mAtkSpd;
        private int _pAtkSpd;
        private int _speed;
        private double _movementSpeedMultip;
        private double _AttackSpeedMultip;

        // Clan
        private string _title;
        private int _clanId;
        private int _clanCrestId;
        private int _allyId;
        private int _allyCrestId;

        #endregion

        #region Getter/Setter

        public int ObjectId
        {
            get { return _objectId; }
        }

        public string VisibleName
        {
            get { return _visibleName; }
        }

        public bool PvpFlag
        {
            get { return _pvpFlag != 0; }
        }

        public int Karma
        {
            get { return _karma; }
        }

        public string Title
        {
            get { return _title; }
        }

        public int ClanId
        {
            get { return _clanId; }
        }

        public int ClanCrestId
        {
            get { return _clanCrestId; }
        }


        #endregion
        
        private void parsePacket(ByteBuffer data)
        {
            data.SetIndex(3);
            this._x = data.ReadInt32();
            this._y = data.ReadInt32();
            this._z = data.ReadInt32();
            data.ReadInt32();
            this._objectId = data.ReadInt32();
            this._visibleName = data.ReadString();
            this._race = data.ReadInt32();
            this._sex = data.ReadInt32();
            this._class = data.ReadInt32();

            this._paperdollHairall = data.ReadInt32();
            this._paperdollHead = data.ReadInt32();
            this._paperdollRhand = data.ReadInt32();
            this._paperdollLhand = data.ReadInt32();
            this._paperdollGloves = data.ReadInt32();
            this._paperdollChest = data.ReadInt32();
            this._paperdollLegs = data.ReadInt32();
            this._paperdollFeet = data.ReadInt32();
            this._paperdollBack = data.ReadInt32();
            this._paperdollLRhand = data.ReadInt32();
            this._paperdollHair = data.ReadInt32();
            this._paperdollHair2 = data.ReadInt32();

            data.SetIndex(data.GetIndex() + 112);
            this._pvpFlag = data.ReadInt32();
            this._karma = data.ReadInt32();
            this._mAtkSpd = data.ReadInt32();
            this._pAtkSpd = data.ReadInt32();

            data.SetIndex(data.GetIndex() + 8); //dd

            this._speed = data.ReadInt32();
            data.SetIndex(data.GetIndex() + 28);
            
            this._movementSpeedMultip = data.ReadDouble();
            this._AttackSpeedMultip = data.ReadDouble();

            // + ffddd
            data.SetIndex(data.GetIndex() + 28);
            this._title = data.ReadString();
            this._clanId = data.ReadInt32();
            this._clanCrestId = data.ReadInt32();
            this._allyId = data.ReadInt32();
            this._allyCrestId = data.ReadInt32();
        }
    }
}
