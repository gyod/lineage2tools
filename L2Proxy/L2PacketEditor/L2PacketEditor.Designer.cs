namespace L2PacketEditor
{
    partial class L2PacketEditor
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
            this.textBoxGSIP = new System.Windows.Forms.TextBox();
            this.textBoxListenIP = new System.Windows.Forms.TextBox();
            this.textBoxGSPort = new System.Windows.Forms.TextBox();
            this.textBoxListenPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.checkedListBoxFilters = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAddFilters = new System.Windows.Forms.Button();
            this.buttonListen = new System.Windows.Forms.Button();
            this.buttonCloseCon = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.logTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxGSIP
            // 
            this.textBoxGSIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxGSIP.Location = new System.Drawing.Point(84, 13);
            this.textBoxGSIP.Name = "textBoxGSIP";
            this.textBoxGSIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxGSIP.TabIndex = 0;
            this.textBoxGSIP.Text = "87.106.90.181";
            // 
            // textBoxListenIP
            // 
            this.textBoxListenIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxListenIP.Location = new System.Drawing.Point(84, 40);
            this.textBoxListenIP.Name = "textBoxListenIP";
            this.textBoxListenIP.Size = new System.Drawing.Size(100, 20);
            this.textBoxListenIP.TabIndex = 1;
            this.textBoxListenIP.Text = "127.0.0.1";
            // 
            // textBoxGSPort
            // 
            this.textBoxGSPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxGSPort.Location = new System.Drawing.Point(223, 13);
            this.textBoxGSPort.Name = "textBoxGSPort";
            this.textBoxGSPort.Size = new System.Drawing.Size(70, 20);
            this.textBoxGSPort.TabIndex = 2;
            this.textBoxGSPort.Text = "2106";
            // 
            // textBoxListenPort
            // 
            this.textBoxListenPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxListenPort.Location = new System.Drawing.Point(223, 40);
            this.textBoxListenPort.Name = "textBoxListenPort";
            this.textBoxListenPort.Size = new System.Drawing.Size(70, 20);
            this.textBoxListenPort.TabIndex = 3;
            this.textBoxListenPort.Text = "2106";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "GS IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Listening IP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Port";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonDown);
            this.groupBox1.Controls.Add(this.buttonUp);
            this.groupBox1.Controls.Add(this.checkedListBoxFilters);
            this.groupBox1.Controls.Add(this.buttonAddFilters);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 113);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filters";
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(235, 79);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(74, 23);
            this.buttonDown.TabIndex = 12;
            this.buttonDown.Text = "Down";
            this.buttonDown.UseVisualStyleBackColor = true;
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(236, 50);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(74, 23);
            this.buttonUp.TabIndex = 12;
            this.buttonUp.Text = "Up";
            this.buttonUp.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxFilters
            // 
            this.checkedListBoxFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBoxFilters.ContextMenuStrip = this.contextMenuStrip1;
            this.checkedListBoxFilters.FormattingEnabled = true;
            this.checkedListBoxFilters.Location = new System.Drawing.Point(7, 20);
            this.checkedListBoxFilters.Name = "checkedListBoxFilters";
            this.checkedListBoxFilters.Size = new System.Drawing.Size(222, 77);
            this.checkedListBoxFilters.TabIndex = 0;
            this.checkedListBoxFilters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxFilters_ItemCheck);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 26);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.configureToolStripMenuItem.Text = "Configure";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // buttonAddFilters
            // 
            this.buttonAddFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddFilters.Location = new System.Drawing.Point(235, 20);
            this.buttonAddFilters.Name = "buttonAddFilters";
            this.buttonAddFilters.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFilters.TabIndex = 11;
            this.buttonAddFilters.Text = "Add Filters";
            this.buttonAddFilters.UseVisualStyleBackColor = true;
            this.buttonAddFilters.Click += new System.EventHandler(this.buttonAddFilters_Click);
            // 
            // buttonListen
            // 
            this.buttonListen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonListen.Location = new System.Drawing.Point(12, 284);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(75, 23);
            this.buttonListen.TabIndex = 8;
            this.buttonListen.Text = "Listen";
            this.buttonListen.UseVisualStyleBackColor = true;
            this.buttonListen.Click += new System.EventHandler(this.buttonListen_Click);
            // 
            // buttonCloseCon
            // 
            this.buttonCloseCon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCloseCon.Location = new System.Drawing.Point(104, 284);
            this.buttonCloseCon.Name = "buttonCloseCon";
            this.buttonCloseCon.Size = new System.Drawing.Size(113, 23);
            this.buttonCloseCon.TabIndex = 9;
            this.buttonCloseCon.Text = "Close Connection";
            this.buttonCloseCon.UseVisualStyleBackColor = true;
            this.buttonCloseCon.Click += new System.EventHandler(this.buttonCloseCon_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.logBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(316, 93);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logBox.Location = new System.Drawing.Point(7, 20);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(303, 67);
            this.logBox.TabIndex = 0;
            // 
            // logTimer
            // 
            this.logTimer.Interval = 250;
            this.logTimer.Tick += new System.EventHandler(this.logTimer_Tick);
            // 
            // L2PacketEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 319);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCloseCon);
            this.Controls.Add(this.buttonListen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxListenPort);
            this.Controls.Add(this.textBoxGSPort);
            this.Controls.Add(this.textBoxListenIP);
            this.Controls.Add(this.textBoxGSIP);
            this.Name = "L2PacketEditor";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxGSIP;
        private System.Windows.Forms.TextBox textBoxListenIP;
        private System.Windows.Forms.TextBox textBoxGSPort;
        private System.Windows.Forms.TextBox textBoxListenPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListBoxFilters;
        private System.Windows.Forms.Button buttonListen;
        private System.Windows.Forms.Button buttonCloseCon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.Timer logTimer;
        private System.Windows.Forms.Button buttonAddFilters;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;

    }
}

