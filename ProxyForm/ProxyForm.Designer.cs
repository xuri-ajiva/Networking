namespace ProxyForm
{
    partial class ProxyForm
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
            this.startBt = new System.Windows.Forms.Button();
            this.clie = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serv = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p1 = new System.Windows.Forms.Panel();
            this.p3 = new System.Windows.Forms.Panel();
            this.bgs = new System.Windows.Forms.GroupBox();
            this.InterruptPaketS = new System.Windows.Forms.CheckBox();
            this.sendInteruptS = new System.Windows.Forms.Button();
            this.gbc = new System.Windows.Forms.GroupBox();
            this.InterruptPaketC = new System.Windows.Forms.CheckBox();
            this.sendInteruptC = new System.Windows.Forms.Button();
            this.proxyList = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p2 = new System.Windows.Forms.Panel();
            this.stopSelectedBtw = new System.Windows.Forms.Button();
            this.stopBt = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.BlockedPaketes = new System.Windows.Forms.GroupBox();
            this.serB = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.cliB = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AllPaketes = new System.Windows.Forms.GroupBox();
            this.MainSplit = new System.Windows.Forms.SplitContainer();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p1.SuspendLayout();
            this.p3.SuspendLayout();
            this.bgs.SuspendLayout();
            this.gbc.SuspendLayout();
            this.p2.SuspendLayout();
            this.BlockedPaketes.SuspendLayout();
            this.AllPaketes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).BeginInit();
            this.MainSplit.Panel1.SuspendLayout();
            this.MainSplit.Panel2.SuspendLayout();
            this.MainSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // startBt
            // 
            this.startBt.AutoSize = true;
            this.startBt.Dock = System.Windows.Forms.DockStyle.Left;
            this.startBt.Location = new System.Drawing.Point(0, 0);
            this.startBt.Name = "startBt";
            this.startBt.Size = new System.Drawing.Size(75, 23);
            this.startBt.TabIndex = 0;
            this.startBt.Text = "Start New";
            this.startBt.UseVisualStyleBackColor = true;
            this.startBt.Click += new System.EventHandler(this.startBtw_Click);
            // 
            // clie
            // 
            this.clie.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.clie.Dock = System.Windows.Forms.DockStyle.Left;
            this.clie.HideSelection = false;
            this.clie.Location = new System.Drawing.Point(3, 16);
            this.clie.Name = "clie";
            this.clie.Size = new System.Drawing.Size(543, 248);
            this.clie.TabIndex = 1;
            this.clie.UseCompatibleStateImageBehavior = false;
            this.clie.View = System.Windows.Forms.View.Details;
            this.clie.DoubleClick += new System.EventHandler(this.DoubleClickP);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ID";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Data";
            this.columnHeader3.Width = 59;
            // 
            // serv
            // 
            this.serv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4});
            this.serv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serv.HideSelection = false;
            this.serv.Location = new System.Drawing.Point(556, 16);
            this.serv.Name = "serv";
            this.serv.Size = new System.Drawing.Size(557, 248);
            this.serv.TabIndex = 2;
            this.serv.UseCompatibleStateImageBehavior = false;
            this.serv.View = System.Windows.Forms.View.Details;
            this.serv.DoubleClick += new System.EventHandler(this.DoubleClickP);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Data";
            // 
            // p1
            // 
            this.p1.Controls.Add(this.p3);
            this.p1.Controls.Add(this.proxyList);
            this.p1.Controls.Add(this.p2);
            this.p1.Dock = System.Windows.Forms.DockStyle.Right;
            this.p1.Location = new System.Drawing.Point(1126, 0);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(295, 535);
            this.p1.TabIndex = 3;
            // 
            // p3
            // 
            this.p3.Controls.Add(this.bgs);
            this.p3.Controls.Add(this.gbc);
            this.p3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p3.Location = new System.Drawing.Point(0, 454);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(295, 81);
            this.p3.TabIndex = 9;
            // 
            // bgs
            // 
            this.bgs.Controls.Add(this.InterruptPaketS);
            this.bgs.Controls.Add(this.sendInteruptS);
            this.bgs.Location = new System.Drawing.Point(3, 3);
            this.bgs.Name = "bgs";
            this.bgs.Size = new System.Drawing.Size(142, 73);
            this.bgs.TabIndex = 7;
            this.bgs.TabStop = false;
            this.bgs.Text = "server";
            // 
            // InterruptPaketS
            // 
            this.InterruptPaketS.AutoSize = true;
            this.InterruptPaketS.Location = new System.Drawing.Point(6, 19);
            this.InterruptPaketS.Name = "InterruptPaketS";
            this.InterruptPaketS.Size = new System.Drawing.Size(100, 17);
            this.InterruptPaketS.TabIndex = 3;
            this.InterruptPaketS.Text = "InterruptPaketS";
            this.InterruptPaketS.UseVisualStyleBackColor = true;
            this.InterruptPaketS.CheckedChanged += new System.EventHandler(this.InterruptPaketS_CheckedChanged);
            // 
            // sendInteruptS
            // 
            this.sendInteruptS.Enabled = false;
            this.sendInteruptS.Location = new System.Drawing.Point(28, 42);
            this.sendInteruptS.Name = "sendInteruptS";
            this.sendInteruptS.Size = new System.Drawing.Size(75, 23);
            this.sendInteruptS.TabIndex = 5;
            this.sendInteruptS.Text = "Send";
            this.sendInteruptS.UseVisualStyleBackColor = true;
            this.sendInteruptS.Click += new System.EventHandler(this.sendInterruptS_Click);
            // 
            // gbc
            // 
            this.gbc.Controls.Add(this.InterruptPaketC);
            this.gbc.Controls.Add(this.sendInteruptC);
            this.gbc.Location = new System.Drawing.Point(151, 3);
            this.gbc.Name = "gbc";
            this.gbc.Size = new System.Drawing.Size(142, 73);
            this.gbc.TabIndex = 6;
            this.gbc.TabStop = false;
            this.gbc.Text = "client";
            // 
            // InterruptPaketC
            // 
            this.InterruptPaketC.AutoSize = true;
            this.InterruptPaketC.Location = new System.Drawing.Point(6, 19);
            this.InterruptPaketC.Name = "InterruptPaketC";
            this.InterruptPaketC.Size = new System.Drawing.Size(100, 17);
            this.InterruptPaketC.TabIndex = 4;
            this.InterruptPaketC.Text = "InterruptPaketC";
            this.InterruptPaketC.UseVisualStyleBackColor = true;
            this.InterruptPaketC.CheckedChanged += new System.EventHandler(this.InterruptPaketC_CheckedChanged);
            // 
            // sendInteruptC
            // 
            this.sendInteruptC.Enabled = false;
            this.sendInteruptC.Location = new System.Drawing.Point(28, 42);
            this.sendInteruptC.Name = "sendInteruptC";
            this.sendInteruptC.Size = new System.Drawing.Size(75, 23);
            this.sendInteruptC.TabIndex = 6;
            this.sendInteruptC.Text = "Send";
            this.sendInteruptC.UseVisualStyleBackColor = true;
            this.sendInteruptC.Click += new System.EventHandler(this.sendInterruptC_Click);
            // 
            // proxyList
            // 
            this.proxyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.proxyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proxyList.HideSelection = false;
            this.proxyList.Location = new System.Drawing.Point(0, 23);
            this.proxyList.Name = "proxyList";
            this.proxyList.Size = new System.Drawing.Size(295, 512);
            this.proxyList.TabIndex = 8;
            this.proxyList.UseCompatibleStateImageBehavior = false;
            this.proxyList.View = System.Windows.Forms.View.Details;
            this.proxyList.SelectedIndexChanged += new System.EventHandler(this.proxyList_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ID";
            this.columnHeader9.Width = 30;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Ip";
            this.columnHeader10.Width = 50;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "PortConnect";
            this.columnHeader11.Width = 75;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.stopSelectedBtw);
            this.p2.Controls.Add(this.stopBt);
            this.p2.Controls.Add(this.startBt);
            this.p2.Dock = System.Windows.Forms.DockStyle.Top;
            this.p2.Location = new System.Drawing.Point(0, 0);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(295, 23);
            this.p2.TabIndex = 11;
            // 
            // stopSelectedBtw
            // 
            this.stopSelectedBtw.AutoSize = true;
            this.stopSelectedBtw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopSelectedBtw.Location = new System.Drawing.Point(150, 0);
            this.stopSelectedBtw.Name = "stopSelectedBtw";
            this.stopSelectedBtw.Size = new System.Drawing.Size(145, 23);
            this.stopSelectedBtw.TabIndex = 11;
            this.stopSelectedBtw.Text = "Stopp Selected";
            this.stopSelectedBtw.UseVisualStyleBackColor = true;
            this.stopSelectedBtw.Click += new System.EventHandler(this.stopSelectedBtw_Click);
            // 
            // stopBt
            // 
            this.stopBt.AutoSize = true;
            this.stopBt.Dock = System.Windows.Forms.DockStyle.Left;
            this.stopBt.Location = new System.Drawing.Point(75, 0);
            this.stopBt.Name = "stopBt";
            this.stopBt.Size = new System.Drawing.Size(75, 23);
            this.stopBt.TabIndex = 10;
            this.stopBt.Text = "Stopp All";
            this.stopBt.UseVisualStyleBackColor = true;
            this.stopBt.Click += new System.EventHandler(this.stopBt_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(546, 16);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 248);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.Sm);
            // 
            // BlockedPaketes
            // 
            this.BlockedPaketes.Controls.Add(this.serB);
            this.BlockedPaketes.Controls.Add(this.splitter2);
            this.BlockedPaketes.Controls.Add(this.cliB);
            this.BlockedPaketes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BlockedPaketes.Location = new System.Drawing.Point(0, 0);
            this.BlockedPaketes.Name = "BlockedPaketes";
            this.BlockedPaketes.Size = new System.Drawing.Size(1116, 264);
            this.BlockedPaketes.TabIndex = 5;
            this.BlockedPaketes.TabStop = false;
            this.BlockedPaketes.Text = "BlockedPaketes";
            // 
            // serB
            // 
            this.serB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.serB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serB.HideSelection = false;
            this.serB.Location = new System.Drawing.Point(556, 16);
            this.serB.Name = "serB";
            this.serB.Size = new System.Drawing.Size(557, 245);
            this.serB.TabIndex = 6;
            this.serB.UseCompatibleStateImageBehavior = false;
            this.serB.View = System.Windows.Forms.View.Details;
            this.serB.DoubleClick += new System.EventHandler(this.DoubleClickP);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ID";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Data";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(546, 16);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(10, 245);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
            this.splitter2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.Sm);
            // 
            // cliB
            // 
            this.cliB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.cliB.Dock = System.Windows.Forms.DockStyle.Left;
            this.cliB.HideSelection = false;
            this.cliB.Location = new System.Drawing.Point(3, 16);
            this.cliB.Name = "cliB";
            this.cliB.Size = new System.Drawing.Size(543, 245);
            this.cliB.TabIndex = 5;
            this.cliB.UseCompatibleStateImageBehavior = false;
            this.cliB.View = System.Windows.Forms.View.Details;
            this.cliB.DoubleClick += new System.EventHandler(this.DoubleClickP);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "ID";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Data";
            this.columnHeader8.Width = 59;
            // 
            // AllPaketes
            // 
            this.AllPaketes.Controls.Add(this.serv);
            this.AllPaketes.Controls.Add(this.splitter1);
            this.AllPaketes.Controls.Add(this.clie);
            this.AllPaketes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllPaketes.Location = new System.Drawing.Point(0, 0);
            this.AllPaketes.Name = "AllPaketes";
            this.AllPaketes.Size = new System.Drawing.Size(1116, 267);
            this.AllPaketes.TabIndex = 6;
            this.AllPaketes.TabStop = false;
            this.AllPaketes.Text = "AllPaketes";
            // 
            // MainSplit
            // 
            this.MainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplit.Location = new System.Drawing.Point(0, 0);
            this.MainSplit.Name = "MainSplit";
            this.MainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplit.Panel1
            // 
            this.MainSplit.Panel1.Controls.Add(this.AllPaketes);
            // 
            // MainSplit.Panel2
            // 
            this.MainSplit.Panel2.Controls.Add(this.BlockedPaketes);
            this.MainSplit.Size = new System.Drawing.Size(1116, 535);
            this.MainSplit.SplitterDistance = 267;
            this.MainSplit.TabIndex = 7;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter3.Location = new System.Drawing.Point(1116, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(10, 535);
            this.splitter3.TabIndex = 12;
            this.splitter3.TabStop = false;
            this.splitter3.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.Sm);
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "PortListen";
            // 
            // ProxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 535);
            this.Controls.Add(this.MainSplit);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.p1);
            this.Name = "ProxyForm";
            this.Text = "ProxyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProxyForm_FormClosed);
            this.Load += new System.EventHandler(this.ProxyForm_Load);
            this.p1.ResumeLayout(false);
            this.p3.ResumeLayout(false);
            this.bgs.ResumeLayout(false);
            this.bgs.PerformLayout();
            this.gbc.ResumeLayout(false);
            this.gbc.PerformLayout();
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            this.BlockedPaketes.ResumeLayout(false);
            this.AllPaketes.ResumeLayout(false);
            this.MainSplit.Panel1.ResumeLayout(false);
            this.MainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplit)).EndInit();
            this.MainSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startBt;
        private System.Windows.Forms.ListView clie;
        private System.Windows.Forms.ListView serv;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox InterruptPaketC;
        private System.Windows.Forms.CheckBox InterruptPaketS;
        private System.Windows.Forms.GroupBox BlockedPaketes;
        private System.Windows.Forms.GroupBox AllPaketes;
        private System.Windows.Forms.ListView serB;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ListView cliB;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.GroupBox bgs;
        private System.Windows.Forms.Button sendInteruptS;
        private System.Windows.Forms.Button sendInteruptC;
        private System.Windows.Forms.GroupBox gbc;
        private System.Windows.Forms.ListView proxyList;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.SplitContainer MainSplit;
        private System.Windows.Forms.Button stopBt;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Button stopSelectedBtw;
        private System.Windows.Forms.ColumnHeader columnHeader12;
    }
}

