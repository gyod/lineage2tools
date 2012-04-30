using System;
using System.Windows.Forms;
using SpoilStatus.Utils;

namespace SpoilStatus
{
    public partial class OptionsForm : Form
    {
        private static OptionsForm _instance = null;

        public static OptionsForm Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new OptionsForm();
                return _instance; 
            }
        }


        // Inform our MainForm about opacity changes
        public delegate void OpacityPropertyEventHandler(int opacity);
        public delegate void PropertiesChangedEventHandler(object sender);

        /// <summary>
        /// Wird ausgelöst wen der Schieberegler für die Transparenz bewegt wird
        /// </summary>
        public event OpacityPropertyEventHandler OnOpacityPropertyChanged;
        /// <summary>
        /// Wird ausgelöst wenn die Änderungen gespeichert werden
        /// </summary>
        public event PropertiesChangedEventHandler OnPropertiesChanged;

        private InIFile iniFile;

        #region private members
        private int mainFormOpacity;
        private bool muteSounds;
        private bool enableFishingHelper;
        private bool enableFishbot;
        private bool useFishingshots;
        private long fishingSkillReuseTime;
        private Keys pumpKey;
        private Keys reelKey;
        private Keys fishshotKey;

        private bool autoReset;
        private bool usePromiscuousMode;
        private string lineage2FilePath;

        private ServerList serverList;
        private int selectedServerIndex = 0;
        #endregion

        private OptionsForm()
        {
            InitializeComponent();
            this.TopMost = true;
            InitCombobox(this.comboBoxPumpKey);
            InitCombobox(this.comboBoxReelKey);
            InitCombobox(this.comboBoxShotKey);

            this.serverList = ServerList.LoadServerList("l2Serverlist.xml");
            this.updateServerList();
            if (this.serverList.HasElements())
                this.comboBoxServerlist.SelectedIndex = 0;
        }

        private static void InitCombobox(ComboBox box)
        {
            for (Keys k = Keys.F1; k <= Keys.F12; k++)
            {
                box.Items.Add(k);
            }
        }

        public void ReadIniFile()
        {
            this.iniFile = new InIFile(Environment.CurrentDirectory + "\\spoilstatus.ini");

            this.MainFormOpacity = iniFile.GetAsInt("MainFormOpacity", 75);
            this.MuteSounds = iniFile.GetAsBool("MuteSounds", false);
            this.EnableFishingHelper = iniFile.GetAsBool("EnableFishingHelper", true);
            this.EnableFishbot = iniFile.GetAsBool("EnableFishbot", false);
            this.UseFishingshots = iniFile.GetAsBool("UseFishingshots", false);
            this.PumpKey = (Keys)iniFile.GetAsInt("PumpKey", (int)Keys.F2);
            this.ReelKey = (Keys)iniFile.GetAsInt("ReelKey", (int)Keys.F3);
            this.FishshotKey = (Keys)iniFile.GetAsInt("FishshotKey", (int)Keys.F4);
            this.FishingSkillReuseTime = iniFile.GetAsInt("FishingSkillReuseTime", 1100);
            this.UsePromiscuousMode = iniFile.GetAsBool("UsePromiscuousMode", false);
            this.Lineage2FilePath = iniFile.GetAsString("Lineage2FilePath", string.Empty);
            this.AutoReset = iniFile.GetAsBool("AutoReset", false);
        }

        private void saveProperties()
        {
            this.iniFile.SaveProperties(this);
        }

        #region public Getter/Setter
        [InIAttr]
        public int MainFormOpacity
        {
            get { return mainFormOpacity; }
            set 
            { 
                mainFormOpacity = value;
                if (value < this.trackBarOpacity.Minimum)
                    this.trackBarOpacity.Value = this.trackBarOpacity.Minimum;
                else if (value > this.trackBarOpacity.Maximum)
                    this.trackBarOpacity.Value = this.trackBarOpacity.Maximum;
                else
                    this.trackBarOpacity.Value = value;
            }
        }
        [InIAttr]
        public bool MuteSounds
        {
            get { return muteSounds; }
            set 
            { 
                muteSounds = value;
                this.checkBoxMuteSounds.Checked = value;
            }
        }
        [InIAttr]
        public bool EnableFishingHelper
        {
            get { return enableFishingHelper; }
            set 
            { 
                enableFishingHelper = value;
                this.checkBoxFishHelper.Checked = value;
            }
        }
        [InIAttr]
        public bool EnableFishbot
        {
            get { return enableFishbot; }
            set 
            { 
                enableFishbot = value;
                this.checkBoxEnableFishbot.Checked = value;
            }
        }
        [InIAttr]
        public bool UseFishingshots
        {
            get { return useFishingshots; }
            set 
            { 
                useFishingshots = value;
                this.checkBoxFishShots.Checked = value;
            }
        }
        [InIAttr]
        public long FishingSkillReuseTime
        {
            get { return fishingSkillReuseTime; }
            set 
            { 
                fishingSkillReuseTime = value;
                this.numericUpDown1.Value = value;
            }
        }
        [InIAttr(true)]
        public Keys PumpKey
        {
            get { return pumpKey; }
            set 
            { 
                pumpKey = value;
                this.comboBoxPumpKey.SelectedItem = value;
            }
        }
        [InIAttr(true)]
        public Keys ReelKey
        {
            get { return reelKey; }
            set 
            { 
                reelKey = value;
                this.comboBoxReelKey.SelectedItem = value;
            }
        }
        [InIAttr(true)]
        public Keys FishshotKey
        {
            get { return fishshotKey; }
            set 
            { 
                fishshotKey = value;
                this.comboBoxShotKey.SelectedItem = value;
            }
        }
        [InIAttr]
        public bool UsePromiscuousMode
        {
            get { return usePromiscuousMode; }
            set
            {
                usePromiscuousMode = value;
                this.checkBoxPromiscuous.Checked = value;
            }
        }
        [InIAttr]
        public string Lineage2FilePath
        {
            get { return lineage2FilePath; }
            set
            {
                lineage2FilePath = value;
                this.textBoxLineagePath.Text = value;
            }
        }
        [InIAttr]
        public bool AutoReset
        {
            get { return autoReset; }
            set { autoReset = value; }
        }

        public ServerInfo DefaultServer
        {
            get { return this.serverList.DefaultServer; }
        }

        #endregion

        private void trackBarOpacity_Scroll(object sender, EventArgs e)
        {
            if (this.OnOpacityPropertyChanged != null)
                this.OnOpacityPropertyChanged(trackBarOpacity.Value);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.MainFormOpacity = trackBarOpacity.Value;
            this.MuteSounds = this.checkBoxMuteSounds.Checked;
            this.EnableFishingHelper = this.checkBoxFishHelper.Checked;
            this.EnableFishbot = this.checkBoxEnableFishbot.Checked;
            this.PumpKey = (Keys)this.comboBoxPumpKey.SelectedItem;
            this.ReelKey = (Keys)this.comboBoxReelKey.SelectedItem;
            this.FishshotKey = (Keys)this.comboBoxShotKey.SelectedItem;
            this.FishingSkillReuseTime = (long)this.numericUpDown1.Value;
            this.UsePromiscuousMode = this.checkBoxPromiscuous.Checked;
            
            this.saveProperties();
            this.Hide();

            if (this.OnPropertiesChanged != null) // Fire event
                this.OnPropertiesChanged(this);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (this.OnOpacityPropertyChanged != null)
                this.OnOpacityPropertyChanged(this.MainFormOpacity);
            this.Hide();
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void buttonSearchL2exe_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialogLineageExe.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                this.Lineage2FilePath = openFileDialogLineageExe.FileName;
            }
        }

        private void buttonSrvSave_Click(object sender, EventArgs e)
        {
            this.serverList.SaveServerlist("l2Serverlist.xml");
        }

        private void buttonSrvDelete_Click(object sender, EventArgs e)
        {
            if (!this.serverList.HasElements())
                return;
            int itemNr = this.comboBoxServerlist.SelectedIndex;
            this.serverList.ServerInfoList.RemoveAt(itemNr);
            this.updateServerList();
        }

        private void buttonAddServer_Click(object sender, EventArgs e)
        {
            this.comboBoxServerlist.Text = "Servername";
            this.textBoxWebsite.Text = "http://www.website.com";
            this.textBoxGsIP.Text = "hostname or IP";
            this.numericUpDownExp.Value = 1;
            this.numericUpDownSp.Value = 1;
            this.numericUpDowDrop.Value = 1;
            this.numericUpDownSpoil.Value = 1;
            this.numericUpDownAdena.Value = 1;

            ServerInfo info = new ServerInfo();
            info.ServerName = this.comboBoxServerlist.Text;
            info.ServerWebAdress = this.textBoxWebsite.Text;
            info.GameHost = this.textBoxGsIP.Text;
            info.ExpRate = (int)this.numericUpDownExp.Value;
            info.SpRate = (int)this.numericUpDownSp.Value;
            info.DropRate = (int)this.numericUpDowDrop.Value;
            info.SpoilRate = (int)this.numericUpDownSpoil.Value;
            info.AdenaRate = (int)this.numericUpDownAdena.Value;

            this.serverList.ServerInfoList.Add(info);

            this.updateServerList();
        }

        private void displayServerItem()
        {
            ServerInfo item = (ServerInfo)this.comboBoxServerlist.SelectedItem;
            if (item == null)
                return;
            this.textBoxWebsite.Text = item.ServerWebAdress;
            this.textBoxGsIP.Text = item.GameHost;
            this.numericUpDownExp.Value = item.ExpRate;
            this.numericUpDownSp.Value = item.SpRate;
            this.numericUpDowDrop.Value = item.DropRate;
            this.numericUpDownSpoil.Value = item.SpoilRate;
            this.numericUpDownAdena.Value = item.AdenaRate;
            this.checkBoxDefaultServer.Checked = this.serverList.DefaultServer.Equals(item);
        }

        private void comboBoxServerlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateServerList();
            this.selectedServerIndex = this.comboBoxServerlist.SelectedIndex;
        }

        private void comboBoxServerlist_DropDown(object sender, EventArgs e)
        {
            this.updateServerList();
        }

        private void updateServerList()
        {
            if (!this.serverList.HasElements())
                return;

            ServerInfo tmp = (ServerInfo)this.comboBoxServerlist.SelectedItem;
            this.comboBoxServerlist.BeginUpdate();
            // Eventhandler entfernen um Stackoverflow zu vermeinden
            this.comboBoxServerlist.SelectedIndexChanged -= new EventHandler(comboBoxServerlist_SelectedIndexChanged);
            this.comboBoxServerlist.Items.Clear();
            foreach (ServerInfo i in this.serverList.ServerInfoList)
            {
                this.comboBoxServerlist.Items.Add(i);
            }
            if (tmp != null)
            {
                this.comboBoxServerlist.SelectedItem = tmp;
            }
            else
                this.comboBoxServerlist.SelectedIndex = 0;

            this.comboBoxServerlist.SelectedIndexChanged += new EventHandler(comboBoxServerlist_SelectedIndexChanged);
            this.comboBoxServerlist.EndUpdate();
            this.displayServerItem();
        }

        private void buttonSetServer_Click(object sender, EventArgs e)
        {
            if (!this.serverList.HasElements())
                return;

            ServerInfo info = (ServerInfo)this.comboBoxServerlist.Items[this.selectedServerIndex];
            info.ServerName = this.comboBoxServerlist.Text;
            info.ServerWebAdress = this.textBoxWebsite.Text;
            info.GameHost = this.textBoxGsIP.Text;
            info.ExpRate = (int)this.numericUpDownExp.Value;
            info.SpRate = (int)this.numericUpDownSp.Value;
            info.DropRate = (int)this.numericUpDowDrop.Value;
            info.SpoilRate = (int)this.numericUpDownSpoil.Value;
            info.AdenaRate = (int)this.numericUpDownAdena.Value;

            if (this.checkBoxDefaultServer.Checked)
                this.serverList.DefaultServer = info;
        }
    }
}