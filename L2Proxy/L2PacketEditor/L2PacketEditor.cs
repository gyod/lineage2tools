using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using L2Proxy;

namespace L2PacketEditor
{
    public partial class L2PacketEditor : Form
    {
        private List<IPacketFilter> allFilters = new List<IPacketFilter>();
        private Proxy proxy;
        private System.IO.StringWriter logStream = new System.IO.StringWriter();

        public L2PacketEditor()
        {
            InitializeComponent();
        }

        private void scanForFilters()
        {
            // Irgendwie Dynamisch nach Plugins suchen...

            // erstmal manuell hinzufügen
            //this.allFilters.Add(new ClientInject(proxy));
            Hellbound2Kamael f = new Hellbound2Kamael();
            this.proxy.AddFilter(f);
        }

        private void init()
        {
            IPEndPoint loginDestinationEndPoint = new IPEndPoint(IPAddress.Parse(this.textBoxGSIP.Text), 
                int.Parse(this.textBoxGSPort.Text));
            IPEndPoint loginListeningEndPoint = new IPEndPoint(IPAddress.Parse(this.textBoxListenIP.Text), 
                int.Parse(this.textBoxListenPort.Text));
            IPAddress ip = IPAddress.Parse(this.textBoxListenIP.Text);

            this.proxy = new Proxy(loginListeningEndPoint, loginDestinationEndPoint, ip);

            // Log Setzen
            this.proxy.SetStdOut(this.logStream);
            this.logBox.Text = this.logStream.ToString();

            this.scanForFilters();
            this.allFilters.AddRange(this.proxy.GetAllFilter());
        }

        private void updateFilters()
        {
            // Event entfernen wenn Box geupdated wird, sonst gibts nen Stapelüberlauf
            this.checkedListBoxFilters.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxFilters_ItemCheck);
            
            this.checkedListBoxFilters.Items.Clear();
            List<IPacketFilter> activeFilters = this.proxy.GetAllFilter();
            foreach (IPacketFilter filter in this.allFilters)
            {
                this.checkedListBoxFilters.Items.Add(filter);
            }

            int i = 0;
            foreach (IPacketFilter filter in this.allFilters)
            {
                if (activeFilters.Contains(filter))
                {
                    this.checkedListBoxFilters.SetItemChecked(i, true);
                }
                else
                {
                    this.checkedListBoxFilters.SetItemChecked(i, false);
                }
                i++;
            }

            // Eventhandler wieder hinzufügen...
            this.checkedListBoxFilters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxFilters_ItemCheck);
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            this.buttonListen.Enabled = false;
            this.init();
            this.updateFilters();
            this.logTimer.Start();
        }

        private void buttonCloseCon_Click(object sender, EventArgs e)
        {
            if (this.proxy != null)
            {
                this.proxy.StopGame();
                this.proxy.StopLogin();
            }
        }

        private void logTimer_Tick(object sender, EventArgs e)
        {
            // Nur updaten wenn sich auch was geändert hat
            // nach der länge überprüfen dürfte schneller sein als den Textinhalt zu vergleichen...
            if (this.logBox.Text.Length == this.logStream.ToString().Length)
            {
                return;
            }
            this.logBox.Text = String.Empty;
            this.logBox.AppendText(this.logStream.ToString());
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkedListBoxFilters.SelectedItem != null)
            {
                IPacketFilter filter = (IPacketFilter)this.checkedListBoxFilters.SelectedItem;
                //filter.Configure();
            }
            this.updateFilters();
        }

        private void checkedListBoxFilters_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Console.Out.WriteLine("Check Changed");  
        }

        private void buttonAddFilters_Click(object sender, EventArgs e)
        {
            AddFilterForm addFilter = new AddFilterForm();
            addFilter.ShowDialog();
        }
    }
}