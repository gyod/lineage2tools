using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using L2Proxy;

namespace L2PacketEditor
{
    public partial class ClientInject : Form, IPacketFilter
    {
        Proxy proxy;

        public ClientInject(Proxy p)
        {
            InitializeComponent();
            proxy = p;
        }

        #region IPacketFilter Member

        public void FilterPacket(L2BasePacket packet)
        {
            // Do Nothing
        }

        public override string ToString()
        {
            return this.GetDiscription();
        }

        public string GetDiscription()
        {
            return "Injects packets to the Client.";
        }

        public void Configure()
        {
            this.Show();
        }

        #endregion

        private void buttonInject_Click(object sender, EventArgs e)
        {
            byte[] tmp;
            tmp = ByteParser.ParseBytes(this.textBox1.Text);
            Console.Out.WriteLine(Util.printData(tmp));
            proxy.SendPacketToClient(new L2BasePacket(tmp));
        }
    }
}