using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using La2PacketSniffer.DataHolding;

namespace La2PacketSniffer
{
    public partial class FilterForm : Form
    {
        private PacketContainer pc;
        private KnownPackets kp;
        public FilterForm(PacketContainer pc, KnownPackets kp)
        {
            InitializeComponent();
            this.pc = pc;
            this.kp = kp;

            if (pc != null)
                this.refresh();
        }

        private void refresh()
        {
            this.olvColumnSource.AspectGetter
                = delegate(object x) { return ((La2PacketSniffer.DataHolding.PacketContainer.FilterItem)x).FromServer ? "Server" : "Client"; };
            this.olvColumnType.AspectGetter = delegate(object x)
            {
                // Map the Name
                return this.kp.GetName(((La2PacketSniffer.DataHolding.PacketContainer.FilterItem)x).OpCode
                    , ((La2PacketSniffer.DataHolding.PacketContainer.FilterItem)x).FromServer);
            };

            this.fastObjectListView1.SetObjects(this.pc.EnumFilter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("Remove"))
            {
                La2PacketSniffer.DataHolding.PacketContainer.FilterItem fi
                    = (La2PacketSniffer.DataHolding.PacketContainer.FilterItem)this.fastObjectListView1.SelectedObject;
                if (fi != null)
                {
                    this.pc.RemoveFilter(fi.OpCode, fi.FromServer);

                    ((Form1)this.Owner).RefreshListView();

                    // Update Own Listview
                    this.fastObjectListView1.SetObjects(this.pc.EnumFilter);
                    this.fastObjectListView1.Update();
                }
            }
        }
    }
}