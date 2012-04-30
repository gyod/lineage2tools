using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace L2PacketEditor
{
    public partial class AddFilterForm : Form
    {
        public AddFilterForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}