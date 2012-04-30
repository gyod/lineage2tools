using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Net;

namespace La2Launch
{
    public partial class Form1 : Form
    {
        private readonly string xmlPath = "LaServers.xml";
        private LaServerList serverList;
        private int selectedIndex = 0;

        public Form1()
        {
            InitializeComponent();
            this.serverList = LaServerList.LoadServerList(this.xmlPath);
            this.updateListBox();
            if (this.comboBoxServerlist.Items.Count > 0)
                this.comboBoxServerlist.SelectedIndex = this.selectedIndex;
            this.buildServerSubmenu();
            this.checkBoxStartWithWin.Checked = this.isInAutostart();
        }

        #region GUI Read/Write/Update 
        private void updateListBox()
        {
            this.comboBoxServerlist.SelectedIndexChanged -= new EventHandler(comboBoxServerlist_SelectedIndexChanged);
            this.comboBoxServerlist.Items.Clear();
            foreach (LaServer srv in this.serverList.ServerList)
            {
                this.comboBoxServerlist.Items.Add(srv);
            }
            this.comboBoxServerlist.SelectedIndexChanged += new EventHandler(comboBoxServerlist_SelectedIndexChanged);
            if (this.serverList.ServerList.Count > 0)
                this.comboBoxServerlist.SelectedIndex = this.selectedIndex;
        }

        private void updateUi()
        {
            LaServer srv = (LaServer)this.comboBoxServerlist.SelectedItem;
            if (srv != null)
            {
                this.textBoxServerIp.Text = srv.ServerIp;
                this.textBoxLaPath.Text = srv.AppToStart;
                this.checkBoxAuth.Checked = srv.L2auth;
                this.checkBoxTestAuth.Checked = srv.L2testauth;
                this.checkBoxGG.Checked = srv.GgServer;
                if (this.serverList[this.serverList.DefaultServer] != null  
                    && this.serverList[this.serverList.DefaultServer].Equals(srv))
                    this.checkBoxDefault.Checked = true;
                else
                    this.checkBoxDefault.Checked = false;
            }
        }

        private void comboBoxServerlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedIndex = this.comboBoxServerlist.SelectedIndex;
            this.updateUi();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.serverList.SaveServerlist(this.xmlPath);
            this.buildServerSubmenu();
            this.setAutostart(this.checkBoxStartWithWin.Checked);
            this.Visible = false;
        }

        private void buttonAddSrv_Click(object sender, EventArgs e)
        {
            LaServer srv = new LaServer();
            srv.ServerName = "ServerName";
            srv.ServerIp = "127.0.0.1";
            srv.AppToStart = @"C:\Lineage2\system\L2.exe";
            srv.L2auth = true;
            srv.L2testauth = true;
            srv.GgServer = false;
            this.serverList.AddServer(srv);
            this.updateListBox();
            this.comboBoxServerlist.SelectedItem = srv;
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            string temp = this.comboBoxServerlist.Text;
            if (this.comboBoxServerlist.Items.Count > 0)
                this.comboBoxServerlist.SelectedIndex = this.selectedIndex;

            LaServer srv = (LaServer)this.comboBoxServerlist.SelectedItem;
            if (srv != null)
            {
                srv.ServerName = temp;
                srv.ServerIp = this.textBoxServerIp.Text;
                srv.AppToStart = this.textBoxLaPath.Text;
                srv.L2auth = this.checkBoxAuth.Checked;
                srv.L2testauth = this.checkBoxTestAuth.Checked;
                srv.GgServer = this.checkBoxGG.Checked;
                this.serverList.DefaultServer = this.serverList.ServerList.IndexOf(srv);
            }
            updateListBox();
            updateUi();
        }

        private void buttonDeleteSrv_Click(object sender, EventArgs e)
        {
            LaServer srv = (LaServer)this.comboBoxServerlist.SelectedItem;
            if (srv != null)
            {
                this.serverList.DelServer(srv);
                this.selectedIndex = 0;
            }
            updateListBox();
            updateUi();
        }

        private void textBoxGGIp_TextChanged(object sender, EventArgs e)
        {
            this.serverList.GgServer = this.textBoxGGIp.Text;
        }

        private void setAutostart(bool set)
        {
            string keyName = "MyProgramName";
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;  // Or the EXE path.

            if (set)
            {
                // Set Auto-start.
                Util.SetAutoStart(keyName, assemblyLocation);
            }
            else
            {
                // Unset Auto-start.
                if (Util.IsAutoStartEnabled(keyName, assemblyLocation))
                    Util.UnSetAutoStart(keyName);
            }
        }

        private bool isInAutostart()
        {
            string keyName = "MyProgramName";
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;  // Or the EXE path.
            return Util.IsAutoStartEnabled(keyName, assemblyLocation);
        }

        #endregion

        #region NotifyIcon

        private void buildServerSubmenu()
        {
            ToolStripItemCollection subItems = ((ToolStripMenuItem)this.contextMenuStrip1.Items[0]).DropDownItems;
            subItems.Clear();
            foreach (LaServer srv in this.serverList.ServerList)
            {
                ToolStripMenuItem itm = new ToolStripMenuItem(srv.ToString());
                itm.Click += new EventHandler(serverSubmenu_Click);
                itm.Tag = srv;
                subItems.Add(itm);
            }
        }

        private void serverSubmenu_Click(object sender, EventArgs e)
        {
            LaServer srv = (LaServer)((ToolStripDropDownItem)sender).Tag;
            this.changeHostAndStartApp(srv);
        }

        private void changeHostAndStartApp(LaServer srv)
        {
            List<HostChanger.hostEntry> entries = new List<HostChanger.hostEntry>();
            if(srv.L2auth)
            {
                entries.Add(new HostChanger.hostEntry { 
                    ip = srv.ServerIp, 
                    hostname = "l2authd.lineage2.com", 
                    comment = srv.ServerName });
            }
            if (srv.L2testauth)
            {
                entries.Add(new HostChanger.hostEntry
                {
                    ip = srv.ServerIp,
                    hostname = "l2testauthd.lineage2.com",
                    comment = srv.ServerName
                });
            }
            if (srv.GgServer)
            {
                // 216.107.250.194 nprotect.lineage2.com
                entries.Add(new HostChanger.hostEntry
                {
                    ip = this.serverList.GgServer,
                    hostname = "nprotect.lineage2.com",
                    comment = "Gameguard Server"
                });
            }
            HostChanger.GetInstance().AddEntries(entries.ToArray());
            this.checkHostEntry(entries[0]);
        }

        private void clearHostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostChanger.GetInstance().RemoveEntry("l2authd.lineage2.com");
            HostChanger.GetInstance().RemoveEntry("l2testauthd.lineage2.com");
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        #endregion

        #region Network

        private void checkHostEntry(HostChanger.hostEntry entry)
        {
            Dns.BeginGetHostAddresses(entry.hostname, new AsyncCallback(ProcessHostEntries), entry);
        }

        private void ProcessHostEntries(IAsyncResult result)
        {
            IPAddress[] ips = Dns.EndGetHostAddresses(result);
        }

        #endregion

    }
}