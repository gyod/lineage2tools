using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace La2PacketSniffer
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public int GetPort()
        {
            return (int)this.numericUpDown1.Value;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}