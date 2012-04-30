using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tamir.IPLib;
using System.Media;
using System.IO;
using L2PacketDecrypt.Packets;
using System.Diagnostics;

namespace SpoilStatus
{
    public partial class Form1 : Form
    {
        private readonly List<SnifferControl> sniffers = new List<SnifferControl>();
        private PcapDeviceList deviceList;
        private SoundPlayer beep;

        // GameRelated
        private string myCharName = "";
        private int myCharObjId = 0;
        private int lastTarget;
        private int lastNpcId = 0;

        //Dataholding
        private readonly Dictionary<int, int> knownNpcs = new Dictionary<int, int>(50);
        private readonly Dictionary<int, L2Pc> knownPcs = new Dictionary<int, L2Pc>(50);
        private readonly Dictionary<int, L2Plegde> knownPlegdes = new Dictionary<int, L2Plegde>();

        private readonly DropInfoForm dropInfoForm = new DropInfoForm();

        // FishingBot
        private readonly FishBot fishBot = new FishBot("Lineage II");

        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.checkBoxTopMost.Checked = this.TopMost;
            this.labelTitle.Text = "";
            this.labelClan.Text = "";
            this.labelExpanderHint.Text = "";
            this.labelExpanderHint2.Text = "";

            // register Mouse Eventhandlers
            this.MouseDown += new MouseEventHandler(app_MouseDown);
            this.splitContainer1.MouseDown += new MouseEventHandler(app_MouseDown);
            this.splitContainer1.Panel1.MouseDown += new MouseEventHandler(app_MouseDown);
            this.splitContainer1.Panel2.MouseDown += new MouseEventHandler(app_MouseDown);

            this.MouseMove += new MouseEventHandler(app_MouseMove);
            this.splitContainer1.MouseMove += new MouseEventHandler(app_MouseMove);
            this.splitContainer1.Panel1.MouseMove += new MouseEventHandler(app_MouseMove);
            this.splitContainer1.Panel2.MouseMove += new MouseEventHandler(app_MouseMove);

            this.MouseUp += new MouseEventHandler(app_MouseUp);
            this.splitContainer1.MouseUp += new MouseEventHandler(app_MouseUp);
            this.splitContainer1.Panel1.MouseUp += new MouseEventHandler(app_MouseUp);
            this.splitContainer1.Panel2.MouseUp += new MouseEventHandler(app_MouseUp);

            this.Expanded = false;

            OptionsForm.Instance.OnOpacityPropertyChanged += new OptionsForm.OpacityPropertyEventHandler(OnOpacityPropertyChanged);
            OptionsForm.Instance.OnPropertiesChanged += new OptionsForm.PropertiesChangedEventHandler(OnPropertiesChanged);
            this.OnPropertiesChanged(this);

            Initialize();
            if (File.Exists(OptionsForm.Instance.Lineage2FilePath))
                this.ShowBallon("SpoilStatus bereit!", "Hier klicken um Lineage II jetzt zu starten!", 5000);
            else
                this.ShowBallon("SpoilStatus bereit!", "Du kannst jetzt Lineage II starten. Rechtsklick auf dieses Symbol für mehr Optionen.", 1000);
        }

        void OnOpacityPropertyChanged(int opacity)
        {
            this.Opacity = opacity / 100.0;
        }

        void OnPropertiesChanged(object sender)
        {
            this.muteToolStripMenuItem.Checked = OptionsForm.Instance.MuteSounds;
            this.labelPump.Text = OptionsForm.Instance.PumpKey + " Pumping";
            this.labelReel.Text = OptionsForm.Instance.ReelKey + " Reeling";
            this.labelFishShot.Text = OptionsForm.Instance.FishshotKey + " Fishingshot";
            this.checkBoxFishbot.Checked = OptionsForm.Instance.EnableFishbot;
            this.Opacity = OptionsForm.Instance.MainFormOpacity / 100.0;
        }

        private void Initialize()
        {

            try
            {
                // Try to find a Networkinterface
                this.deviceList = SharpPcap.GetAllDevices();
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show("Es sieht so aus das du kein WinPcap installiert hast. besorg es dir von hier: http://www.winpcap.org", "Fehlende DLL-Datei", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            catch (Exception e)
            {
#if DEBUG
                Program.debugStream.WriteLine(e.Message);
                Program.debugStream.WriteLine(e.StackTrace);
                Program.debugStream.WriteLine(e.Source);
#endif
            }

            if (this.deviceList.Count < 1)
            {
                MessageBox.Show("Keine Netzwerk Schnittstellen gefunden =(");
                return;
            }
            try
            {
                this.beep = new SoundPlayer(Environment.CurrentDirectory + "\\data\\ok.wav");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(Environment.CurrentDirectory + "\\data\\ok.wav nicht gefunden");
            }
            if(!OptionsForm.Instance.MuteSounds)
                this.beep.Play();
            
            this.startSniffers();
        }

        private void startSniffers()
        {
            foreach (PcapDevice dev in this.deviceList)
            {
                try
                {
                    SnifferControl sniff = new SnifferControl(7777);
                    sniff.Init(dev, "port 7777");
                    sniff.Start();
                    sniff.NewPacketArrived += new SnifferControl.NewPacketHandler(sniffer_NewPacketArrived);
                    sniff.OnSynRecived += new SnifferControl.SynRecivedEventHandler(sniffer_OnSynRecived);
                    sniff.OnFinRecived += new SnifferControl.FinRecivedEventHandler(sniffer_OnFinRecived);
                    this.sniffers.Add(sniff);
#if DEBUG
                    Program.debugStream.WriteLine("Sniffer: " + sniff + " Started");
#endif
                }
                catch (Exception e)
                {
#if DEBUG
                    Program.debugStream.WriteLine(e.Message);
#endif
                }
            }
        }

        void sniffer_OnFinRecived(object sender)
        {
            if (OptionsForm.Instance.AutoReset)
                Application.Restart();
        }

        void sniffer_OnSynRecived(object sender)
        {
            this.labelStatus.Text = "Verbunden";
            this.notifyIcon1.Text = "Spoilstatus, Verbunden!";
        }

        #region PacketHandler
        void sniffer_NewPacketArrived(object sender, L2Packet packet)
        {
            if (packet is ClientPacket)
                return;

            int opCode = packet.OpCode;

            switch (opCode)
            {
                case 0x62: //SystemMessage
                    this.onSpoil(packet);
                    break;
                case 0x0b: //CharSelected
                    this.onCharSelected(packet);
                    break;
                case 0xb9: //MyTargetSelected
                    this.onMyTargetSelected(packet);
                    break;
                case 0x0c: //NpcInfo
                    onNpcInfo(packet);
                    break;
                case 0x31: //CharInfo
                    onCharInfo(packet);
                    break;
                case 0x32: // Userinfo
                    onUserInfo(packet);
                    break;
                case 0x08: //DeleteObject
                    onDeleteObject(packet);
                    break;
                case 0x89: //PlegdeInfo
                    onPledgeInfo(packet);
                    break;
                case 0xfe1e: //ExFishingStart
                    // Expanderausklappen
                    onExFishingStart(packet);
                    break;
                case 0xfe1f: //ExFishingStop
                    // Expander einklappen
                    onExFishingStop(packet);
                    break;
                case 0xfe27: //ExFishingStartCombat
                    onExFishingStartCombat(packet);
                    break;
                case 0xfe28: //ExFishingHpRegen
                    onExFishingHpRegen(packet);
                    break;
            }

        }

        private void onUserInfo(L2Packet packet)
        {
            packet.Data.SetIndex(3);
            packet.Data.ReadInt32();
            packet.Data.ReadInt32();
            packet.Data.ReadInt32();
            packet.Data.ReadInt32();
            this.myCharObjId = packet.Data.ReadInt32();
            //string ownName = packet.Data.ReadString();
        }

        private void onExFishingHpRegen(L2Packet packet)
        {
            packet.Data.SetIndex(5);
            int obId = packet.Data.ReadInt32();
            if (obId != this.myCharObjId)
                return;
            int time = packet.Data.ReadInt32();
            int hp = packet.Data.ReadInt32();
            short hpMode = packet.Data.ReadInt16();
            short gootUse = packet.Data.ReadInt16();
            short anim = packet.Data.ReadInt16();
            int penalty = packet.Data.ReadInt32();
            byte hpBarCol = packet.Data.ReadByte();

            switch (hpMode)
            {
                case 0:
                    this.labelExpanderHint.Text = hpBarCol == 0 ? "Pumping" : "Reeling";
                    //this.fishBot.UsePump();
                    if (OptionsForm.Instance.EnableFishbot)
                    {
                        if (hpBarCol == 0x00)
                            this.fishBot.UsePump();
                        else
                            this.fishBot.UseReel();
                    }
                    break;
                case 1:
                    this.labelExpanderHint.Text = hpBarCol == 0 ? "Reeling" : "Pumping";
                    //this.fishBot.UseReel();
                    if (OptionsForm.Instance.EnableFishbot)
                    {
                        if (hpBarCol == 0x00)
                            this.fishBot.UseReel();
                        else
                            this.fishBot.UsePump();
                    }
                    break;
            }
        }

        private void onExFishingStartCombat(L2Packet packet)
        {
            packet.Data.SetIndex(5);
            int obId = packet.Data.ReadInt32();
            if (obId != this.myCharObjId)
                return;
            int time = packet.Data.ReadInt32();
            int hp = packet.Data.ReadInt32();
            short mode = packet.Data.ReadInt16();
            short lureType = packet.Data.ReadInt16();
            short deceptiveMode = packet.Data.ReadInt16();

            if (deceptiveMode == 1)
                this.labelExpanderHint2.Text = "Täuschender Modus!";
        }

        private void onExFishingStop(L2Packet packet)
        {
            // Stop Fishing, Collapse the Window
            packet.Data.SetIndex(5);
            int obId = packet.Data.ReadInt32();
            if (obId != this.myCharObjId)
                return;

            this.Expanded = false;
            this.labelExpanderHint.Text = "";
            this.labelExpanderHint2.Text = "";
        }

        private void onExFishingStart(L2Packet packet)
        {
            // Start Fishing, Expand the Window
            packet.Data.SetIndex(5);
            int obId = packet.Data.ReadInt32();
            if (obId != this.myCharObjId)
                return;

            this.Expanded = true;
            this.labelExpanderHint.Text = "Warte auf einen Fisch...";
            this.labelExpanderHint2.Text = "";

        }

        private void onPledgeInfo(L2Packet packet)
        {
            packet.Data.SetIndex(3);
            L2Plegde clan = new L2Plegde((GameServerPacket)packet);

            if (this.knownPlegdes.ContainsKey(clan.ClanId))
                return;

            this.knownPlegdes.Add(clan.ClanId, clan);
        }

        private void onCharInfo(L2Packet packet)
        {
            packet.Data.SetIndex(3 + 16);
            //writeD(_x);
            //writeD(_y);
            //writeD(_z);
            //writeD(0x00);

            //writeD(_activeChar.getObjectId());

            int objId = packet.Data.ReadInt32();

            if (this.knownPcs.ContainsKey(objId))
                return;

            this.knownPcs.Add(objId, new L2Pc((GameServerPacket)packet));
        }

        private void onNpcInfo(L2Packet packet)
        {
            packet.Data.SetIndex(3);
            //writeD(_activeChar.getObjectId());
            //writeD(_idTemplate + 1000000);  // npctype id
            int objId = packet.Data.ReadInt32();
            int npcId = packet.Data.ReadInt32() - 1000000;

            if (this.knownNpcs.ContainsKey(objId))
                return;

            this.knownNpcs.Add(objId, npcId);
        }

        private void onDeleteObject(L2Packet packet)
        {
            packet.Data.SetIndex(3);
            int objId = packet.Data.ReadInt32();
            if (this.knownNpcs.ContainsKey(objId))
            {
                this.knownNpcs.Remove(objId);
            }

            if (this.knownPcs.ContainsKey(objId))
            {
                this.knownPcs.Remove(objId);
            }
        }

        private void onCharSelected(L2Packet packet)
        {
            packet.Data.SetIndex(3);
            this.myCharName = packet.Data.ReadString();
            this.labelStatus.Text = String.Format("Verbunden als {0}", this.myCharName);
        }

        private void onMyTargetSelected(L2Packet packet)
        {
            packet.Data.SetIndex(3);
            int objId = packet.Data.ReadInt32();
            if (this.lastTarget != objId)
            {
                this.labelClan.Text = "";
                if (this.knownNpcs.ContainsKey(objId))
                {
                    int npcId = this.knownNpcs[objId];
                    this.labelName.Text = NpcNames.GetInstance().GetName(npcId);
                    this.labelName.ForeColor = Color.LightGray;
                    this.labelTitle.Text = NpcNames.GetInstance().GetTitle(npcId);
                    this.labelTitle.ForeColor = NpcNames.GetInstance().GetColor(npcId);

                    this.lastNpcId = npcId;
                }
                else if (this.knownPcs.ContainsKey(objId))
                {
                    L2Pc pc = this.knownPcs[objId];
                    this.labelName.Text = pc.VisibleName;
                    this.labelTitle.Text = pc.Title;
                    this.labelTitle.ForeColor = Color.LightGray;

                    // Karma/PvP
                    this.labelName.ForeColor = Color.LightGray;
                    if (pc.PvpFlag)
                        this.labelName.ForeColor = Color.Pink;
                    if (pc.Karma > 0)
                        this.labelName.ForeColor = Color.Red;

                    // Clan
                    if (this.knownPlegdes.ContainsKey(pc.ClanId))
                    {
                        this.labelClan.Text = "Clan: " + this.knownPlegdes[pc.ClanId].ClanName;
                    }
                }
                else if (this.myCharObjId == objId) // myself
                {
                    this.labelName.Text = this.myCharName;
                    this.labelTitle.Text = "";
                    this.labelTitle.ForeColor = Color.LightGray;
                }
                else
                {
                    this.labelName.Text = "id:" + objId;
                    this.labelName.ForeColor = Color.LightGray;
                    this.labelTitle.Text = "";
                }
                this.lastTarget = objId;
            }
        }

        private void onSpoil(L2Packet packet)
        {
            packet.Data.SetIndex(3);
            int messageId = packet.Data.ReadInt32();

            switch (messageId)
            {
                case 612: // Spoil ok
                    this.labelName.Text = "Gespoilt";
                    if (!OptionsForm.Instance.MuteSounds)
                        this.beep.Play();
                    break;
                case 357: // Allready Spoiled
                    this.labelName.Text = "Schon gespoilt";
                    break;
            }
        }
        #endregion

        #region Form control stuff
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (SnifferControl sniff in this.sniffers)
            {
                sniff.Stop();
            }
            Environment.Exit(0);
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.checkBoxTopMost.Checked;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.dropInfoForm.Show(this.lastNpcId);
        }

        private void checkBoxFishbot_CheckedChanged(object sender, EventArgs e)
        {
            OptionsForm.Instance.EnableFishbot = this.checkBoxFishbot.Checked;
        }

        #region FormMove
        private bool mouseDown = false;
        private Point moveDelta;

        private void app_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = true;
                this.moveDelta = new Point(MousePosition.X - this.Location.X
                    ,MousePosition.Y - this.Location.Y);
            }
        }

        private void app_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.mouseDown)
                return;
            //Console.Out.WriteLine("MouseLoc" + MousePosition + "FormLoc "+ this.Location);
            this.Location = new Point(MousePosition.X - this.moveDelta.X
                , MousePosition.Y - this.moveDelta.Y);
        }

        private void app_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.mouseDown = false;
        }
        #endregion

        #region Expander

        private int expandedSize;
        private bool isExpanded = false;

        /// <summary>
        /// Erweitert oder Verkleinert das Form
        /// Abhänging von splitContainer1
        /// </summary>
        public bool Expanded
        {
            set
            {
                int formHeaderHeight = this.Height - splitContainer1.Height;
                if (value)
                {
                    this.Height = this.expandedSize + formHeaderHeight;
                    this.isExpanded = true;
                }
                else
                {
                    this.expandedSize = this.splitContainer1.Height;
                    this.Height = splitContainer1.Panel1.Height + formHeaderHeight;
                    this.isExpanded = false;
                }
            }
            get
            {
                return this.isExpanded;
            }
        }
        #endregion

        #region ToolStripMenue
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm.Instance.ShowDialog();
        }

        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = this.muteToolStripMenuItem.Checked;
            this.muteToolStripMenuItem.Checked = !check;
            OptionsForm.Instance.MuteSounds = this.muteToolStripMenuItem.Checked;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool check = this.toolStripMenuItem1.Checked;
            this.toolStripMenuItem1.Checked = !check;
            this.Expanded = this.toolStripMenuItem1.Checked;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool onTop = this.TopMost;
            if (onTop)
                this.TopMost = false;
            DialogResult res = MessageBox.Show("Wirklich Beenden?", "Beenden", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.No)
            {
                if (onTop)
                    this.TopMost = true;
                return;
            }

            foreach (SnifferControl sniff in this.sniffers)
            {
                sniff.Stop();
            }
            this.notifyIcon1.Visible = false;
            Environment.Exit(0);
        }
        #endregion

        #endregion

        #region notifyIcon

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Minimized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        public void ShowBallon(string title, string message, int timeout)
        {
            this.notifyIcon1.ShowBalloonTip(timeout, title, message, ToolTipIcon.Info);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (e is MouseEventArgs && ((MouseEventArgs)e).Button != System.Windows.Forms.MouseButtons.Left)
                return;

            if (File.Exists(OptionsForm.Instance.Lineage2FilePath))
            {
                Process.Start(OptionsForm.Instance.Lineage2FilePath);
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (!this.TopMost && this.WindowState == FormWindowState.Normal)
            {
                this.Activate();
            }
        }

        #endregion

        private void fooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HostsWriter.GetInstance().AddEntry("123.123.123.123", "google.de", "#test"); 
            HostsWriter.GetInstance().CheckLastEntry();
        }
    }
}