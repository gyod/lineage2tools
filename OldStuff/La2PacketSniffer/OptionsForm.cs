using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tamir.IPLib;

namespace La2PacketSniffer
{
    public partial class DevicesForm : Form
    {
        private PcapDeviceList deviceList;
        private PcapDevice selectedDevice;
        private int port;

        public DevicesForm()
        {
            InitializeComponent();
            try
            {
                this.deviceList = SharpPcap.GetAllDevices();
                foreach (PcapDevice pd in deviceList)
                {
                    this.comboBoxDevices.Items.Add(pd.PcapDescription);
                }
                this.comboBoxDevices.SelectedIndex = 0;
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show("It seem's you haven't installed WinPcap.\n"
                    +"You can get it from http://www.winpcap.org .", "Missing DLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.selectedDevice = this.deviceList[this.comboBoxDevices.SelectedIndex];
            this.port = (int)this.numericUpDown1.Value;
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Liefert das Ausgewähle Device zurück, oder null wenn keines ausgewählt wurde
        /// </summary>
        /// <returns>das PcapDevice</returns>
        public PcapDevice GetDevice()
        {
            return this.selectedDevice;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Der Gewählte Port</returns>
        public int GetPort()
        {
            return this.port;
        }
    }
}