using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpoilStatus
{
    public partial class DropInfoForm : Form
    {
        private List<Drop> activeList = new List<Drop>();
        private bool isShown = false;

        public DropInfoForm()
        {
            InitializeComponent();
        }

        public void Show(int mobId)
        {
            this.activeList = DropData.GetInstance().GetDrops(mobId);
            if (this.activeList.Count == 0)
            {
                this.myClose();
                return;
            }

            this.dataGridView1.DataSource = this.activeList;
            this.Size = calcNewSize();
            this.Text = NpcNames.GetInstance().GetName(mobId);


            // Show the Form
            if (!this.isShown)
            {
                this.isShown = true;
                this.Show();
            }
        }


        private void myClose()
        {
            this.Hide();
            this.isShown = false;
        }

        private Size calcNewSize()
        {
            // Calculate the new witdh
            int newWidth = this.dataGridView1.RowHeadersWidth;
            foreach (DataGridViewColumn col in this.dataGridView1.Columns)
            {
                newWidth += col.Width;
            }
            newWidth += this.dataGridView1.Margin.Left;
            newWidth += this.dataGridView1.Margin.Right;

            // Calculate the new height
            int newHeight = this.dataGridView1.ColumnHeadersHeight*2;
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                newHeight += row.Height;
            }
            newHeight += this.dataGridView1.Margin.Top;
            newHeight += this.dataGridView1.Margin.Bottom;

            return new Size(newWidth, newHeight);
        }

        private void DropInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.myClose();
        }
    }
}