namespace SpoilStatus
{
    /// <summary>
    /// Hält Dropinformationen
    /// </summary>
    class Drop
    {
        public Drop(int mobId, int itemId, int min, int max, int category, int chance)
        {
            this.mobId = mobId;
            this.itemId = itemId;
            this.min = min;
            this.max = max;
            this.category = category;
            this.chance = chance;
        }

        public string ItemName
        {
            get
            {
                return ItemNames.GetInstance().GetItemName(this.itemId);
            }
        }

        public string DropAmount
        {
            get
            {
                if (min == max)
                    return min.ToString();
                else
                    return min + " - " + max;
            }
        }

        public string DropChance
        {
            get
            {
                double droprate = (this.chance / 10000.0);
                if (this.IsSpoil)
                {
                    droprate *= OptionsForm.Instance.DefaultServer.SpoilRate;
                }
                if (this.itemId == 57)
                {
                    droprate *= OptionsForm.Instance.DefaultServer.AdenaRate;
                }
                else
                {
                    droprate *= OptionsForm.Instance.DefaultServer.DropRate;
                }
                return droprate + "%";
            }
        }

        public bool IsSpoil
        {
            get
            {
                return this.category < 0;
            }
        }

        #region Fields
        private int mobId;

        public int MobId
        {
            get { return mobId; }
            set { mobId = value; }
        }
        private int itemId;

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        private int min;

        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        private int max;

        public int Max
        {
            get { return max; }
            set { max = value; }
        }
        private int category;

        public int Category
        {
            get { return category; }
            set { category = value; }
        }
        private int chance;

        public int Chance
        {
            get { return chance; }
            set { chance = value; }
        }
        #endregion
    }
}
