namespace SpoilStatus
{
    partial class DropInfoForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sqLiteDataAdapter1 = new Finisar.SQLite.SQLiteDataAdapter();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DropAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSpoil = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DropChance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dropBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // sqLiteDataAdapter1
            // 
            this.sqLiteDataAdapter1.DeleteCommand = null;
            this.sqLiteDataAdapter1.InsertCommand = null;
            this.sqLiteDataAdapter1.SelectCommand = null;
            this.sqLiteDataAdapter1.UpdateCommand = null;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemNameDataGridViewTextBoxColumn,
            this.DropAmount,
            this.IsSpoil,
            this.DropChance});
            this.dataGridView1.DataSource = this.dropBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 21;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(288, 264);
            this.dataGridView1.TabIndex = 0;
            // 
            // DropAmount
            // 
            this.DropAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DropAmount.DataPropertyName = "DropAmount";
            this.DropAmount.HeaderText = "Amount";
            this.DropAmount.Name = "DropAmount";
            this.DropAmount.ReadOnly = true;
            this.DropAmount.Width = 68;
            // 
            // IsSpoil
            // 
            this.IsSpoil.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.IsSpoil.DataPropertyName = "IsSpoil";
            this.IsSpoil.HeaderText = "Spoil";
            this.IsSpoil.Name = "IsSpoil";
            this.IsSpoil.ReadOnly = true;
            this.IsSpoil.Width = 36;
            // 
            // DropChance
            // 
            this.DropChance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DropChance.DataPropertyName = "DropChance";
            this.DropChance.HeaderText = "Chance";
            this.DropChance.Name = "DropChance";
            this.DropChance.ReadOnly = true;
            this.DropChance.Width = 69;
            // 
            // itemNameDataGridViewTextBoxColumn
            // 
            this.itemNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.itemNameDataGridViewTextBoxColumn.DataPropertyName = "ItemName";
            this.itemNameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.itemNameDataGridViewTextBoxColumn.Name = "itemNameDataGridViewTextBoxColumn";
            this.itemNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemNameDataGridViewTextBoxColumn.Width = 60;
            // 
            // dropBindingSource
            // 
            this.dropBindingSource.DataSource = typeof(SpoilStatus.Drop);
            // 
            // DropInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 264);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DropInfoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DropInfoForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DropInfoForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dropBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Finisar.SQLite.SQLiteDataAdapter sqLiteDataAdapter1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource dropBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DropAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSpoil;
        private System.Windows.Forms.DataGridViewTextBoxColumn DropChance;
    }
}