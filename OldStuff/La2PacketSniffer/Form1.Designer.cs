namespace La2PacketSniffer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyBytesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonListDevices = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNewCapture = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStopCatpure = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAutoUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.buttonClearFilter = new System.Windows.Forms.Button();
            this.buttonFilterView = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnNo = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnSource = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnType = new BrightIdeasSoftware.OLVColumn();
            this.olvColumnLength = new BrightIdeasSoftware.OLVColumn();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.eToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(876, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyBytesToolStripMenuItem});
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.eToolStripMenuItem.Text = "Edit";
            // 
            // copyBytesToolStripMenuItem
            // 
            this.copyBytesToolStripMenuItem.Name = "copyBytesToolStripMenuItem";
            this.copyBytesToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.copyBytesToolStripMenuItem.Text = "Copy Bytes";
            this.copyBytesToolStripMenuItem.Click += new System.EventHandler(this.copyBytesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "Help";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonListDevices,
            this.toolStripButtonNewCapture,
            this.toolStripButtonStopCatpure,
            this.toolStripSeparator1,
            this.toolStripButtonOpenFile,
            this.toolStripSeparator2,
            this.toolStripButtonAutoUpdate,
            this.toolStripButtonRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(876, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonListDevices
            // 
            this.toolStripButtonListDevices.Image = global::La2PacketSniffer.Properties.Resources.netdevice;
            this.toolStripButtonListDevices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonListDevices.Name = "toolStripButtonListDevices";
            this.toolStripButtonListDevices.Size = new System.Drawing.Size(143, 22);
            this.toolStripButtonListDevices.Text = "Setup Network Device";
            this.toolStripButtonListDevices.ToolTipText = "List Devices";
            this.toolStripButtonListDevices.Click += new System.EventHandler(this.toolStripButtonListDevices_Click);
            // 
            // toolStripButtonNewCapture
            // 
            this.toolStripButtonNewCapture.Enabled = false;
            this.toolStripButtonNewCapture.Image = global::La2PacketSniffer.Properties.Resources.go;
            this.toolStripButtonNewCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewCapture.Name = "toolStripButtonNewCapture";
            this.toolStripButtonNewCapture.Size = new System.Drawing.Size(119, 22);
            this.toolStripButtonNewCapture.Text = "Start new capture";
            this.toolStripButtonNewCapture.ToolTipText = "Start New Capture";
            this.toolStripButtonNewCapture.Click += new System.EventHandler(this.toolStripButtonNewCapture_Click);
            // 
            // toolStripButtonStopCatpure
            // 
            this.toolStripButtonStopCatpure.Enabled = false;
            this.toolStripButtonStopCatpure.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStopCatpure.Image")));
            this.toolStripButtonStopCatpure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStopCatpure.Name = "toolStripButtonStopCatpure";
            this.toolStripButtonStopCatpure.Size = new System.Drawing.Size(94, 22);
            this.toolStripButtonStopCatpure.Text = "Stop capture";
            this.toolStripButtonStopCatpure.ToolTipText = "Stop Catpure";
            this.toolStripButtonStopCatpure.Click += new System.EventHandler(this.toolStripButtonStopCapture_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonOpenFile
            // 
            this.toolStripButtonOpenFile.Image = global::La2PacketSniffer.Properties.Resources.fileopen;
            this.toolStripButtonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenFile.Name = "toolStripButtonOpenFile";
            this.toolStripButtonOpenFile.Size = new System.Drawing.Size(77, 22);
            this.toolStripButtonOpenFile.Text = "Open File";
            this.toolStripButtonOpenFile.ToolTipText = "Open File";
            this.toolStripButtonOpenFile.Click += new System.EventHandler(this.toolStripButtonOpenFile_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAutoUpdate
            // 
            this.toolStripButtonAutoUpdate.Checked = true;
            this.toolStripButtonAutoUpdate.CheckOnClick = true;
            this.toolStripButtonAutoUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonAutoUpdate.Image = global::La2PacketSniffer.Properties.Resources.stock_redo;
            this.toolStripButtonAutoUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAutoUpdate.Name = "toolStripButtonAutoUpdate";
            this.toolStripButtonAutoUpdate.Size = new System.Drawing.Size(89, 22);
            this.toolStripButtonAutoUpdate.Text = "Autorefresh";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.Image = global::La2PacketSniffer.Properties.Resources.refresh;
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(66, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 542);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(876, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "Waiting for Connection";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fastObjectListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(876, 493);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToFilterToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 26);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // addToFilterToolStripMenuItem
            // 
            this.addToFilterToolStripMenuItem.Name = "addToFilterToolStripMenuItem";
            this.addToFilterToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.addToFilterToolStripMenuItem.Text = "Add to Filter";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.hexBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.buttonClearFilter);
            this.splitContainer2.Panel2.Controls.Add(this.buttonFilterView);
            this.splitContainer2.Size = new System.Drawing.Size(637, 493);
            this.splitContainer2.SplitterDistance = 457;
            this.splitContainer2.TabIndex = 0;
            // 
            // hexBox1
            // 
            this.hexBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hexBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hexBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox1.HexCasing = Be.Windows.Forms.HexCasing.Lower;
            this.hexBox1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox1.LineInfoVisible = true;
            this.hexBox1.Location = new System.Drawing.Point(0, 0);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(637, 457);
            this.hexBox1.StringViewVisible = true;
            this.hexBox1.TabIndex = 0;
            this.hexBox1.UseFixedBytesPerLine = true;
            this.hexBox1.VScrollBarVisible = true;
            // 
            // buttonClearFilter
            // 
            this.buttonClearFilter.Location = new System.Drawing.Point(84, 5);
            this.buttonClearFilter.Name = "buttonClearFilter";
            this.buttonClearFilter.Size = new System.Drawing.Size(75, 23);
            this.buttonClearFilter.TabIndex = 2;
            this.buttonClearFilter.Text = "Clear Filter";
            this.buttonClearFilter.UseVisualStyleBackColor = true;
            this.buttonClearFilter.Click += new System.EventHandler(this.buttonClearFilter_Click);
            // 
            // buttonFilterView
            // 
            this.buttonFilterView.Location = new System.Drawing.Point(3, 5);
            this.buttonFilterView.Name = "buttonFilterView";
            this.buttonFilterView.Size = new System.Drawing.Size(75, 23);
            this.buttonFilterView.TabIndex = 1;
            this.buttonFilterView.Text = "View Filter";
            this.buttonFilterView.UseVisualStyleBackColor = true;
            this.buttonFilterView.Click += new System.EventHandler(this.buttonFilterView_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Pcap-Files|*.pcap|La2 Packet Sniffer File|*.l2ps";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            this.openFileDialog1.Title = "Open File";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "l2ps";
            this.saveFileDialog1.FileName = "capture.l2ps";
            this.saveFileDialog1.Filter = "La2 Packet Sniffer File|*.l2ps";
            this.saveFileDialog1.Title = "Save File";
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvColumnNo);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnSource);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnType);
            this.fastObjectListView1.AllColumns.Add(this.olvColumnLength);
            this.fastObjectListView1.AllowColumnReorder = true;
            this.fastObjectListView1.AlternateRowBackColor = System.Drawing.Color.Empty;
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnNo,
            this.olvColumnSource,
            this.olvColumnType,
            this.olvColumnLength});
            this.fastObjectListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastObjectListView1.FullRowSelect = true;
            this.fastObjectListView1.GridLines = true;
            this.fastObjectListView1.HideSelection = false;
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 0);
            this.fastObjectListView1.MultiSelect = false;
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(235, 493);
            this.fastObjectListView1.TabIndex = 0;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            this.fastObjectListView1.SelectedIndexChanged += new System.EventHandler(this.fastObjectListView1_SelectedIndexChanged);
            // 
            // olvColumnNo
            // 
            this.olvColumnNo.AspectName = null;
            this.olvColumnNo.IsEditable = false;
            this.olvColumnNo.Text = "No.";
            this.olvColumnNo.Width = 38;
            // 
            // olvColumnSource
            // 
            this.olvColumnSource.AspectName = "FromServer";
            this.olvColumnSource.IsEditable = false;
            this.olvColumnSource.Text = "Source";
            this.olvColumnSource.Width = 61;
            // 
            // olvColumnType
            // 
            this.olvColumnType.AspectName = "OpCode";
            this.olvColumnType.IsEditable = false;
            this.olvColumnType.Text = "Type";
            this.olvColumnType.Width = 76;
            // 
            // olvColumnLength
            // 
            this.olvColumnLength.AspectName = "Length";
            this.olvColumnLength.IsEditable = false;
            this.olvColumnLength.Text = "Size";
            this.olvColumnLength.Width = 46;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 564);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Lineage 2 Packet Sniffer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonListDevices;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewCapture;
        private System.Windows.Forms.ToolStripButton toolStripButtonStopCatpure;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenFile;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.FastObjectListView fastObjectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumnNo;
        private BrightIdeasSoftware.OLVColumn olvColumnSource;
        private BrightIdeasSoftware.OLVColumn olvColumnType;
        private BrightIdeasSoftware.OLVColumn olvColumnLength;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAutoUpdate;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyBytesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.Button buttonFilterView;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonClearFilter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToFilterToolStripMenuItem;
    }
}

