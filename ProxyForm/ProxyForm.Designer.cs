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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbc = new System.Windows.Forms.GroupBox();
            this.InterruptPaketC = new System.Windows.Forms.CheckBox();
            this.sendInteruptC = new System.Windows.Forms.Button();
            this.bgs = new System.Windows.Forms.GroupBox();
            this.InterruptPaketS = new System.Windows.Forms.CheckBox();
            this.sendInteruptS = new System.Windows.Forms.Button();
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
            this.panel1.SuspendLayout();
            this.gbc.SuspendLayout();
            this.bgs.SuspendLayout();
            this.BlockedPaketes.SuspendLayout();
            this.AllPaketes.SuspendLayout();
            this.SuspendLayout();
            // 
            // startBt
            // 
            this.startBt.Location = new System.Drawing.Point(6, 3);
            this.startBt.Name = "startBt";
            this.startBt.Size = new System.Drawing.Size(75, 23);
            this.startBt.TabIndex = 0;
            this.startBt.Text = "Start";
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
            this.clie.Size = new System.Drawing.Size(663, 221);
            this.clie.TabIndex = 1;
            this.clie.UseCompatibleStateImageBehavior = false;
            this.clie.View = System.Windows.Forms.View.Details;
            this.clie.DoubleClick += new System.EventHandler(this.DoubleClick);
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
            this.serv.Location = new System.Drawing.Point(676, 16);
            this.serv.Name = "serv";
            this.serv.Size = new System.Drawing.Size(559, 221);
            this.serv.TabIndex = 2;
            this.serv.UseCompatibleStateImageBehavior = false;
            this.serv.View = System.Windows.Forms.View.Details;
            this.serv.DoubleClick += new System.EventHandler(this.DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Data";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbc);
            this.panel1.Controls.Add(this.bgs);
            this.panel1.Controls.Add(this.startBt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1238, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 451);
            this.panel1.TabIndex = 3;
            // 
            // gbc
            // 
            this.gbc.Controls.Add(this.InterruptPaketC);
            this.gbc.Controls.Add(this.sendInteruptC);
            this.gbc.Location = new System.Drawing.Point(3, 132);
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
            this.sendInteruptC.Location = new System.Drawing.Point(28, 42);
            this.sendInteruptC.Name = "sendInteruptC";
            this.sendInteruptC.Size = new System.Drawing.Size(75, 23);
            this.sendInteruptC.TabIndex = 6;
            this.sendInteruptC.Text = "Send";
            this.sendInteruptC.UseVisualStyleBackColor = true;
            this.sendInteruptC.Click += new System.EventHandler(this.sendInteruptC_Click);
            // 
            // bgs
            // 
            this.bgs.Controls.Add(this.InterruptPaketS);
            this.bgs.Controls.Add(this.sendInteruptS);
            this.bgs.Location = new System.Drawing.Point(3, 53);
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
            this.sendInteruptS.Location = new System.Drawing.Point(28, 42);
            this.sendInteruptS.Name = "sendInteruptS";
            this.sendInteruptS.Size = new System.Drawing.Size(75, 23);
            this.sendInteruptS.TabIndex = 5;
            this.sendInteruptS.Text = "Send";
            this.sendInteruptS.UseVisualStyleBackColor = true;
            this.sendInteruptS.Click += new System.EventHandler(this.sendInteruptS_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(666, 16);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 221);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // BlockedPaketes
            // 
            this.BlockedPaketes.Controls.Add(this.serB);
            this.BlockedPaketes.Controls.Add(this.splitter2);
            this.BlockedPaketes.Controls.Add(this.cliB);
            this.BlockedPaketes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BlockedPaketes.Location = new System.Drawing.Point(0, 240);
            this.BlockedPaketes.Name = "BlockedPaketes";
            this.BlockedPaketes.Size = new System.Drawing.Size(1238, 211);
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
            this.serB.Location = new System.Drawing.Point(676, 16);
            this.serB.Name = "serB";
            this.serB.Size = new System.Drawing.Size(559, 192);
            this.serB.TabIndex = 6;
            this.serB.UseCompatibleStateImageBehavior = false;
            this.serB.View = System.Windows.Forms.View.Details;
            this.serB.DoubleClick += new System.EventHandler(this.DoubleClick);
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
            this.splitter2.Location = new System.Drawing.Point(666, 16);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(10, 192);
            this.splitter2.TabIndex = 7;
            this.splitter2.TabStop = false;
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
            this.cliB.Size = new System.Drawing.Size(663, 192);
            this.cliB.TabIndex = 5;
            this.cliB.UseCompatibleStateImageBehavior = false;
            this.cliB.View = System.Windows.Forms.View.Details;
            this.cliB.DoubleClick += new System.EventHandler(this.DoubleClick);
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
            this.AllPaketes.Size = new System.Drawing.Size(1238, 240);
            this.AllPaketes.TabIndex = 6;
            this.AllPaketes.TabStop = false;
            this.AllPaketes.Text = "AllPaketes";
            // 
            // ProxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 451);
            this.Controls.Add(this.AllPaketes);
            this.Controls.Add(this.BlockedPaketes);
            this.Controls.Add(this.panel1);
            this.Name = "ProxyForm";
            this.Text = "ProxyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProxyForm_FormClosed);
            this.Load += new System.EventHandler(this.ProxyForm_Load);
            this.panel1.ResumeLayout(false);
            this.gbc.ResumeLayout(false);
            this.gbc.PerformLayout();
            this.bgs.ResumeLayout(false);
            this.bgs.PerformLayout();
            this.BlockedPaketes.ResumeLayout(false);
            this.AllPaketes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startBt;
        private System.Windows.Forms.ListView clie;
        private System.Windows.Forms.ListView serv;
        private System.Windows.Forms.Panel panel1;
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
    }
}

