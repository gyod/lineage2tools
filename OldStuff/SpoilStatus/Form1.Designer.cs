namespace SpoilStatus
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelClan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.checkBoxTopMost = new System.Windows.Forms.CheckBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelExpanderHint2 = new System.Windows.Forms.Label();
            this.labelFishShot = new System.Windows.Forms.Label();
            this.labelReel = new System.Windows.Forms.Label();
            this.labelPump = new System.Windows.Forms.Label();
            this.labelExpanderHint = new System.Windows.Forms.Label();
            this.checkBoxFishbot = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.muteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.labelTitle);
            this.splitContainer1.Panel1.Controls.Add(this.labelStatus);
            this.splitContainer1.Panel1.Controls.Add(this.labelClan);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSearch);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxTopMost);
            this.splitContainer1.Panel1.Controls.Add(this.labelName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labelExpanderHint2);
            this.splitContainer1.Panel2.Controls.Add(this.labelFishShot);
            this.splitContainer1.Panel2.Controls.Add(this.labelReel);
            this.splitContainer1.Panel2.Controls.Add(this.labelPump);
            this.splitContainer1.Panel2.Controls.Add(this.labelExpanderHint);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxFishbot);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(274, 186);
            this.splitContainer1.SplitterDistance = 103;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.ForeColor = System.Drawing.Color.LightGray;
            this.labelTitle.Location = new System.Drawing.Point(15, 35);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(23, 13);
            this.labelTitle.TabIndex = 10;
            this.labelTitle.Text = "title";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.ForeColor = System.Drawing.Color.LightGray;
            this.labelStatus.Location = new System.Drawing.Point(15, 5);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(87, 13);
            this.labelStatus.TabIndex = 13;
            this.labelStatus.Text = "Nicht Verbunden";
            // 
            // labelClan
            // 
            this.labelClan.AutoSize = true;
            this.labelClan.BackColor = System.Drawing.Color.Transparent;
            this.labelClan.ForeColor = System.Drawing.Color.LightGray;
            this.labelClan.Location = new System.Drawing.Point(15, 21);
            this.labelClan.Name = "labelClan";
            this.labelClan.Size = new System.Drawing.Size(27, 13);
            this.labelClan.TabIndex = 11;
            this.labelClan.Text = "clan";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(176, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "SpoilStatus v1.0";
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.Transparent;
            this.buttonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSearch.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.buttonSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.ForeColor = System.Drawing.Color.LightGray;
            this.buttonSearch.Location = new System.Drawing.Point(212, 75);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(1);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(53, 23);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Suche";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // checkBoxTopMost
            // 
            this.checkBoxTopMost.AutoSize = true;
            this.checkBoxTopMost.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxTopMost.ForeColor = System.Drawing.Color.LightGray;
            this.checkBoxTopMost.Location = new System.Drawing.Point(20, 81);
            this.checkBoxTopMost.Name = "checkBoxTopMost";
            this.checkBoxTopMost.Size = new System.Drawing.Size(98, 17);
            this.checkBoxTopMost.TabIndex = 7;
            this.checkBoxTopMost.Text = "Im Vordergrund";
            this.checkBoxTopMost.UseVisualStyleBackColor = false;
            this.checkBoxTopMost.Click += new System.EventHandler(this.checkBoxTopMost_CheckedChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.labelName.ForeColor = System.Drawing.Color.LightGray;
            this.labelName.Location = new System.Drawing.Point(15, 47);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(96, 25);
            this.labelName.TabIndex = 5;
            this.labelName.Text = "Kein Ziel";
            // 
            // labelExpanderHint2
            // 
            this.labelExpanderHint2.AutoSize = true;
            this.labelExpanderHint2.BackColor = System.Drawing.Color.Transparent;
            this.labelExpanderHint2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelExpanderHint2.ForeColor = System.Drawing.Color.LightGray;
            this.labelExpanderHint2.Location = new System.Drawing.Point(16, 9);
            this.labelExpanderHint2.Name = "labelExpanderHint2";
            this.labelExpanderHint2.Size = new System.Drawing.Size(70, 20);
            this.labelExpanderHint2.TabIndex = 5;
            this.labelExpanderHint2.Text = "hinweis2";
            // 
            // labelFishShot
            // 
            this.labelFishShot.AutoSize = true;
            this.labelFishShot.BackColor = System.Drawing.Color.Transparent;
            this.labelFishShot.ForeColor = System.Drawing.Color.LightGray;
            this.labelFishShot.Location = new System.Drawing.Point(180, 50);
            this.labelFishShot.Name = "labelFishShot";
            this.labelFishShot.Size = new System.Drawing.Size(85, 13);
            this.labelFishShot.TabIndex = 13;
            this.labelFishShot.Text = "F4 Fishing Shots";
            // 
            // labelReel
            // 
            this.labelReel.AutoSize = true;
            this.labelReel.BackColor = System.Drawing.Color.Transparent;
            this.labelReel.ForeColor = System.Drawing.Color.LightGray;
            this.labelReel.Location = new System.Drawing.Point(180, 37);
            this.labelReel.Name = "labelReel";
            this.labelReel.Size = new System.Drawing.Size(58, 13);
            this.labelReel.TabIndex = 13;
            this.labelReel.Text = "F3 Reeling";
            // 
            // labelPump
            // 
            this.labelPump.AutoSize = true;
            this.labelPump.BackColor = System.Drawing.Color.Transparent;
            this.labelPump.ForeColor = System.Drawing.Color.LightGray;
            this.labelPump.Location = new System.Drawing.Point(180, 24);
            this.labelPump.Name = "labelPump";
            this.labelPump.Size = new System.Drawing.Size(63, 13);
            this.labelPump.TabIndex = 13;
            this.labelPump.Text = "F2 Pumping";
            // 
            // labelExpanderHint
            // 
            this.labelExpanderHint.AutoSize = true;
            this.labelExpanderHint.BackColor = System.Drawing.Color.Transparent;
            this.labelExpanderHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.labelExpanderHint.ForeColor = System.Drawing.Color.LightGray;
            this.labelExpanderHint.Location = new System.Drawing.Point(15, 29);
            this.labelExpanderHint.Name = "labelExpanderHint";
            this.labelExpanderHint.Size = new System.Drawing.Size(84, 25);
            this.labelExpanderHint.TabIndex = 5;
            this.labelExpanderHint.Text = "hinweis";
            // 
            // checkBoxFishbot
            // 
            this.checkBoxFishbot.AutoSize = true;
            this.checkBoxFishbot.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxFishbot.ForeColor = System.Drawing.Color.LightGray;
            this.checkBoxFishbot.Location = new System.Drawing.Point(183, 8);
            this.checkBoxFishbot.Name = "checkBoxFishbot";
            this.checkBoxFishbot.Size = new System.Drawing.Size(68, 17);
            this.checkBoxFishbot.TabIndex = 7;
            this.checkBoxFishbot.Text = "Angelbot";
            this.checkBoxFishbot.UseVisualStyleBackColor = false;
            this.checkBoxFishbot.Click += new System.EventHandler(this.checkBoxTopMost_CheckedChanged);
            this.checkBoxFishbot.CheckedChanged += new System.EventHandler(this.checkBoxFishbot_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SpoilStatus";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fooToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.muteToolStripMenuItem,
            this.tToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 164);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "Optionen";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Erweitern";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // muteToolStripMenuItem
            // 
            this.muteToolStripMenuItem.Name = "muteToolStripMenuItem";
            this.muteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.muteToolStripMenuItem.Text = "Ton aus";
            this.muteToolStripMenuItem.Click += new System.EventHandler(this.muteToolStripMenuItem_Click);
            // 
            // tToolStripMenuItem
            // 
            this.tToolStripMenuItem.Name = "tToolStripMenuItem";
            this.tToolStripMenuItem.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Zurücksetzen";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Beenden";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // fooToolStripMenuItem
            // 
            this.fooToolStripMenuItem.Name = "fooToolStripMenuItem";
            this.fooToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fooToolStripMenuItem.Text = "foo";
            this.fooToolStripMenuItem.Click += new System.EventHandler(this.fooToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SpoilStatus.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(274, 186);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.5;
            this.ShowInTaskbar = false;
            this.Text = "SpoilStatus";
            this.TransparencyKey = System.Drawing.Color.Red;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelClan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.CheckBox checkBoxTopMost;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelExpanderHint;
        private System.Windows.Forms.Label labelExpanderHint2;
        private System.Windows.Forms.Label labelPump;
        private System.Windows.Forms.CheckBox checkBoxFishbot;
        private System.Windows.Forms.Label labelReel;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label labelFishShot;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem fooToolStripMenuItem;
    }
}

