namespace La2Launch
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.officialNcSoftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearHostsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDefault = new System.Windows.Forms.CheckBox();
            this.buttonSet = new System.Windows.Forms.Button();
            this.buttonDeleteSrv = new System.Windows.Forms.Button();
            this.buttonAddSrv = new System.Windows.Forms.Button();
            this.checkBoxAuth = new System.Windows.Forms.CheckBox();
            this.checkBoxTestAuth = new System.Windows.Forms.CheckBox();
            this.checkBoxGG = new System.Windows.Forms.CheckBox();
            this.buttonFindLa2 = new System.Windows.Forms.Button();
            this.textBoxLaPath = new System.Windows.Forms.TextBox();
            this.comboBoxServerlist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxServerIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkBoxStartWithWin = new System.Windows.Forms.CheckBox();
            this.textBoxGGIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.clearHostsToolStripMenuItem,
            this.toolStripSeparator1,
            this.einstellungenToolStripMenuItem,
            this.überToolStripMenuItem,
            this.beendenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 120);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.officialNcSoftToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // officialNcSoftToolStripMenuItem
            // 
            this.officialNcSoftToolStripMenuItem.Name = "officialNcSoftToolStripMenuItem";
            this.officialNcSoftToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.officialNcSoftToolStripMenuItem.Text = "Official NcSoft";
            // 
            // clearHostsToolStripMenuItem
            // 
            this.clearHostsToolStripMenuItem.Name = "clearHostsToolStripMenuItem";
            this.clearHostsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.clearHostsToolStripMenuItem.Text = "Säubere hosts";
            this.clearHostsToolStripMenuItem.Click += new System.EventHandler(this.clearHostsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.überToolStripMenuItem.Text = "Info";
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxDefault);
            this.groupBox1.Controls.Add(this.buttonSet);
            this.groupBox1.Controls.Add(this.buttonDeleteSrv);
            this.groupBox1.Controls.Add(this.buttonAddSrv);
            this.groupBox1.Controls.Add(this.checkBoxAuth);
            this.groupBox1.Controls.Add(this.checkBoxTestAuth);
            this.groupBox1.Controls.Add(this.checkBoxGG);
            this.groupBox1.Controls.Add(this.buttonFindLa2);
            this.groupBox1.Controls.Add(this.textBoxLaPath);
            this.groupBox1.Controls.Add(this.comboBoxServerlist);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxServerIp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 219);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // checkBoxDefault
            // 
            this.checkBoxDefault.AutoSize = true;
            this.checkBoxDefault.Location = new System.Drawing.Point(189, 56);
            this.checkBoxDefault.Name = "checkBoxDefault";
            this.checkBoxDefault.Size = new System.Drawing.Size(66, 17);
            this.checkBoxDefault.TabIndex = 20;
            this.checkBoxDefault.Text = "Standart";
            this.checkBoxDefault.UseVisualStyleBackColor = true;
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(186, 20);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(75, 23);
            this.buttonSet.TabIndex = 19;
            this.buttonSet.Text = "Setzen";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // buttonDeleteSrv
            // 
            this.buttonDeleteSrv.Location = new System.Drawing.Point(96, 20);
            this.buttonDeleteSrv.Name = "buttonDeleteSrv";
            this.buttonDeleteSrv.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteSrv.TabIndex = 18;
            this.buttonDeleteSrv.Text = "Löschen";
            this.buttonDeleteSrv.UseVisualStyleBackColor = true;
            this.buttonDeleteSrv.Click += new System.EventHandler(this.buttonDeleteSrv_Click);
            // 
            // buttonAddSrv
            // 
            this.buttonAddSrv.Location = new System.Drawing.Point(7, 20);
            this.buttonAddSrv.Name = "buttonAddSrv";
            this.buttonAddSrv.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSrv.TabIndex = 17;
            this.buttonAddSrv.Text = "Hinzufügen";
            this.buttonAddSrv.UseVisualStyleBackColor = true;
            this.buttonAddSrv.Click += new System.EventHandler(this.buttonAddSrv_Click);
            // 
            // checkBoxAuth
            // 
            this.checkBoxAuth.AutoSize = true;
            this.checkBoxAuth.Location = new System.Drawing.Point(6, 191);
            this.checkBoxAuth.Name = "checkBoxAuth";
            this.checkBoxAuth.Size = new System.Drawing.Size(61, 17);
            this.checkBoxAuth.TabIndex = 14;
            this.checkBoxAuth.Text = "l2authd";
            this.checkBoxAuth.UseVisualStyleBackColor = true;
            // 
            // checkBoxTestAuth
            // 
            this.checkBoxTestAuth.AutoSize = true;
            this.checkBoxTestAuth.Location = new System.Drawing.Point(73, 191);
            this.checkBoxTestAuth.Name = "checkBoxTestAuth";
            this.checkBoxTestAuth.Size = new System.Drawing.Size(78, 17);
            this.checkBoxTestAuth.TabIndex = 15;
            this.checkBoxTestAuth.Text = "l2testauthd";
            this.checkBoxTestAuth.UseVisualStyleBackColor = true;
            // 
            // checkBoxGG
            // 
            this.checkBoxGG.AutoSize = true;
            this.checkBoxGG.Location = new System.Drawing.Point(157, 191);
            this.checkBoxGG.Name = "checkBoxGG";
            this.checkBoxGG.Size = new System.Drawing.Size(81, 17);
            this.checkBoxGG.TabIndex = 16;
            this.checkBoxGG.Text = "Gameguard";
            this.checkBoxGG.UseVisualStyleBackColor = true;
            // 
            // buttonFindLa2
            // 
            this.buttonFindLa2.Location = new System.Drawing.Point(186, 154);
            this.buttonFindLa2.Name = "buttonFindLa2";
            this.buttonFindLa2.Size = new System.Drawing.Size(75, 23);
            this.buttonFindLa2.TabIndex = 13;
            this.buttonFindLa2.Text = "Suchen";
            this.buttonFindLa2.UseVisualStyleBackColor = true;
            // 
            // textBoxLaPath
            // 
            this.textBoxLaPath.Location = new System.Drawing.Point(6, 154);
            this.textBoxLaPath.Name = "textBoxLaPath";
            this.textBoxLaPath.Size = new System.Drawing.Size(177, 20);
            this.textBoxLaPath.TabIndex = 12;
            // 
            // comboBoxServerlist
            // 
            this.comboBoxServerlist.FormattingEnabled = true;
            this.comboBoxServerlist.Location = new System.Drawing.Point(6, 54);
            this.comboBoxServerlist.Name = "comboBoxServerlist";
            this.comboBoxServerlist.Size = new System.Drawing.Size(177, 21);
            this.comboBoxServerlist.TabIndex = 8;
            this.comboBoxServerlist.SelectedIndexChanged += new System.EventHandler(this.comboBoxServerlist_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "IpAdresse:";
            // 
            // textBoxServerIp
            // 
            this.textBoxServerIp.Location = new System.Drawing.Point(6, 103);
            this.textBoxServerIp.Name = "textBoxServerIp";
            this.textBoxServerIp.Size = new System.Drawing.Size(177, 20);
            this.textBoxServerIp.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Lineage Pfad:";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(211, 290);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(69, 23);
            this.buttonOk.TabIndex = 13;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // checkBoxStartWithWin
            // 
            this.checkBoxStartWithWin.AutoSize = true;
            this.checkBoxStartWithWin.Location = new System.Drawing.Point(13, 295);
            this.checkBoxStartWithWin.Name = "checkBoxStartWithWin";
            this.checkBoxStartWithWin.Size = new System.Drawing.Size(122, 17);
            this.checkBoxStartWithWin.TabIndex = 14;
            this.checkBoxStartWithWin.Text = "Mit Windows starten";
            this.checkBoxStartWithWin.UseVisualStyleBackColor = true;
            // 
            // textBoxGGIp
            // 
            this.textBoxGGIp.Location = new System.Drawing.Point(13, 260);
            this.textBoxGGIp.Name = "textBoxGGIp";
            this.textBoxGGIp.Size = new System.Drawing.Size(213, 20);
            this.textBoxGGIp.TabIndex = 9;
            this.textBoxGGIp.TextChanged += new System.EventHandler(this.textBoxGGIp_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "GameGuardIP ( nur bei Windows XP nötig ):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 325);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxStartWithWin);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxGGIp);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "La2Launcher";
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem officialNcSoftToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxAuth;
        private System.Windows.Forms.CheckBox checkBoxTestAuth;
        private System.Windows.Forms.CheckBox checkBoxGG;
        private System.Windows.Forms.Button buttonFindLa2;
        private System.Windows.Forms.TextBox textBoxLaPath;
        private System.Windows.Forms.ComboBox comboBoxServerlist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxServerIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonDeleteSrv;
        private System.Windows.Forms.Button buttonAddSrv;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.CheckBox checkBoxStartWithWin;
        private System.Windows.Forms.CheckBox checkBoxDefault;
        private System.Windows.Forms.ToolStripMenuItem clearHostsToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxGGIp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
    }
}

