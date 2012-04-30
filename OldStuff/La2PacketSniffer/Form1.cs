using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using L2PacketDecrypt.Packets;
using L2PacketDecrypt;
using La2PacketSniffer.DataHolding;
using System.Xml.Serialization;
using System.IO;
using Be.Windows.Forms;

namespace La2PacketSniffer
{
    public partial class Form1 : Form
    {
        private SnifferControl sniffer;
        private FileReader pcapReader;
        private PacketContainer packetContainer;
        private KnownPackets knownPackets;
        private DevicesForm devicesForm = new DevicesForm();
        private Settings settingsForm = new Settings();

        private DateTime lastUpdated = DateTime.Now;

        //Delegates
        delegate void UpdateFastObjectList(object sender);
        delegate void ChangeStatusLabelCallback(string text, Color col);

        public Form1()
        {
            InitializeComponent();
            try
            {
                this.knownPackets = new KnownPackets(Application.StartupPath + "\\packets.txt");
            }
            catch (Exception)
            {
                MessageBox.Show("There is an error with the packetdescription file.\n "
                    +"Either it's not present or it has an syntax error.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog(this);
        }

        private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            L2Packet p = (L2Packet)this.fastObjectListView1.SelectedObject;
            if (p != null)
                this.hexBox1.ByteProvider = new DynamicByteProvider(p.Data.Get_ByteArray()); 
        }

        private void fillListbox(List<L2Packet> packetlist)
        {
            this.olvColumnNo.AspectGetter = delegate(object x) { return ((L2Packet)x).PacketNo; };
            this.olvColumnSource.AspectGetter = delegate(object x) { return ((L2Packet)x).Source; };
            this.olvColumnType.AspectGetter = delegate(object x)
            {
                // Map the Name
                return this.knownPackets.GetName((L2Packet)x);
            };

            this.fastObjectListView1.SetObjects(packetlist);
            // Scroll to the End
            if (packetlist.Count > 0)
            {
                this.fastObjectListView1.EnsureVisible(packetlist.Count - 1);
            }
        }

        private void toolStripButtonListDevices_Click(object sender, EventArgs e)
        {
            this.devicesForm.ShowDialog(this);
            if (this.devicesForm.GetDevice() != null)
            {
                this.toolStripButtonNewCapture.Enabled = true;
            }
        }

        private void toolStripButtonNewCapture_Click(object sender, EventArgs e)
        {
            this.packetContainer = new PacketContainer();
            this.sniffer = new SnifferControl(this.packetContainer, this.devicesForm.GetPort());
            this.sniffer.Init(this.devicesForm.GetDevice(), ("port " + this.devicesForm.GetPort()));
            this.sniffer.Start();
            this.fillListbox(this.packetContainer.DisplayedPackets);
            this.toolStripButtonStopCatpure.Enabled = true;
            this.toolStripButtonNewCapture.Enabled = false;
            this.toolStripButtonListDevices.Enabled = false;
            this.toolStripButtonOpenFile.Enabled = false;
            this.openToolStripMenuItem.Enabled = false;

            this.hexBox1.ByteProvider = null;

            // Event Anmelden
            this.sniffer.NewPacketArrived += new SnifferControl.NewPacketHandler(packethandler_onPacketAdded);
            this.sniffer.OnSynRecived += new SnifferControl.SynRecivedEventHandler(sniffer_OnSynRecived);
            this.sniffer.OnFinRecived += new SnifferControl.FinRecivedEventHandler(sniffer_OnFinRecived);
            this.ChangeStatusLabel("Waiting for Connection", Color.Black);
        }

        private void sniffer_OnFinRecived(object sender)
        {
            this.ChangeStatusLabel("Disconnected", Color.Red);
        }

        private void sniffer_OnSynRecived(object sender)
        {
            this.ChangeStatusLabel("Connected", Color.Green);
        }

        private void ChangeStatusLabel(string text, Color col)
        {
            if (this.statusStrip1.InvokeRequired)
            {
                ChangeStatusLabelCallback d = new ChangeStatusLabelCallback(ChangeStatusLabel);
                this.Invoke(d, new object[] { text, col });
            }
            else
            {
                this.toolStripStatusLabel1.Text = text;
                this.toolStripStatusLabel1.ForeColor = col;
            }
        }

        private void packethandler_onPacketAdded(object sender)
        {
            if (this.toolStripButtonAutoUpdate.Checked && this.packetContainer != null)
            {
                if (this.fastObjectListView1.InvokeRequired)
                {
                    UpdateFastObjectList d = new UpdateFastObjectList(packethandler_onPacketAdded);
                    this.Invoke(d, new object[] { sender });
                }
                else
                {
                    // Update only after 50ms
                    if ((DateTime.Now - this.lastUpdated) > TimeSpan.FromMilliseconds(50))
                    {
                        this.fastObjectListView1.SetObjects(this.packetContainer.DisplayedPackets);
                        this.fastObjectListView1.Invalidate();
                        if (this.packetContainer.DisplayedPackets.Count > 0)
                        {
                            this.fastObjectListView1.EnsureVisible(this.packetContainer.DisplayedPackets.Count - 1);
                        }
                        this.toolStripStatusLabel.Text = "Auto Refreshed : " + ((TimeSpan)(DateTime.Now - this.lastUpdated)).Milliseconds + "ms";
                        this.lastUpdated = DateTime.Now;
                    }
                }
            }
        }

        private void toolStripButtonStopCapture_Click(object sender, EventArgs e)
        {
            this.sniffer.Stop();
            this.toolStripButtonNewCapture.Enabled = true;
            this.toolStripButtonStopCatpure.Enabled = false;
            this.toolStripButtonListDevices.Enabled = true;
            this.toolStripButtonOpenFile.Enabled = true;
            this.openToolStripMenuItem.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFile();
        }

        private void toolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            this.openFile();
        }

        private void openFile()
        {
            DialogResult res = this.openFileDialog1.ShowDialog(this);
            this.packetContainer = null;

            // Dateierweiterung Checken          
            if (Path.GetExtension(this.openFileDialog1.FileName).ToLower().Equals(".pcap") && res == DialogResult.OK)
            {
                this.packetContainer = new PacketContainer();
                this.settingsForm.ShowDialog(this);
                this.pcapReader = new FileReader(this.packetContainer, this.settingsForm.GetPort());
                this.pcapReader.ReadPcapFile(this.openFileDialog1.FileName);
            }
            else if (Path.GetExtension(this.openFileDialog1.FileName).ToLower().Equals(".l2ps") && res == DialogResult.OK)
            {
                this.packetContainer = this.deSerialize(this.openFileDialog1.FileName);
            }
            else if (res == DialogResult.OK)
            {
                MessageBox.Show("File not supported");
                return;
            }
            else
            {
                return;
            }

            this.packetContainer.applyFilter();
            this.fillListbox(this.packetContainer.DisplayedPackets);
            this.hexBox1.ByteProvider = null;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            if (this.packetContainer != null)
            {
                if (this.sniffer != null)
                    this.sniffer.processPackets();
                this.RefreshListView();
                this.toolStripStatusLabel.Text = "Refreshed...";
            }
        }

        private void serialize(PacketContainer pcon, string filepath)
        {
            XmlSerializer s = new XmlSerializer(typeof(PacketContainer));
            TextWriter w = new StreamWriter(filepath);
            s.Serialize(w, pcon);
            w.Close();
        }

        private PacketContainer deSerialize(string filepath)
        {
            PacketContainer pcon;
            XmlSerializer s = new XmlSerializer(typeof(PacketContainer));
            TextReader r = new StreamReader(filepath);
            pcon = (PacketContainer)s.Deserialize(r);
            r.Close();
            return pcon;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.ShowDialog(this);

            this.serialize(this.packetContainer, this.saveFileDialog1.FileName);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hexBox1.Copy();
        }

        /// <summary>
        /// Cleanup, vor dem schlieﬂen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.sniffer != null)
            {
                this.sniffer.Stop();
            }
        }

        private void buttonFilterView_Click(object sender, EventArgs e)
        {
            FilterForm ff = new FilterForm(this.packetContainer, this.knownPackets);
            ff.ShowDialog(this);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("Add to Filter") && this.packetContainer != null)
            {
                L2Packet p = (L2Packet)this.fastObjectListView1.SelectedObject;
                if (p != null)
                {
                    this.packetContainer.AddFilter(p.OpCode, p is GameServerPacket);
                    //refresh
                    this.RefreshListView();
                }
                
            }
        }

        public void RefreshListView()
        {
            if (this.packetContainer != null)
            {
                this.fastObjectListView1.SetObjects(this.packetContainer.DisplayedPackets);
                this.fastObjectListView1.Update();
            }
        }

        private void buttonClearFilter_Click(object sender, EventArgs e)
        {
            if (this.packetContainer != null)
            {
                this.packetContainer.ClearFilter();
                this.RefreshListView();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm hf = new HelpForm();
            hf.ShowDialog(this);
        }
    }
}