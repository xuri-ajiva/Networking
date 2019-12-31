namespace ProxyForm
{
    partial class Start
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ip = new System.Windows.Forms.TextBox();
            this.portL = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.portC = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ip
            // 
            this.ip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.ip.Location = new System.Drawing.Point(15, 25);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(220, 28);
            this.ip.TabIndex = 0;
            this.ip.Text = "127.0.0.1";
            this.ip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ip_KeyPress);
            // 
            // portL
            // 
            this.portL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.portL.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.portL.Location = new System.Drawing.Point(15, 77);
            this.portL.Name = "portL";
            this.portL.Size = new System.Drawing.Size(121, 28);
            this.portL.TabIndex = 1;
            this.portL.Text = "9900";
            this.portL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.port_KeyPress);
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.button1.Location = new System.Drawing.Point(160, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 84);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ip:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "PortToListen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "PortToConnect:";
            // 
            // portC
            // 
            this.portC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.portC.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.portC.Location = new System.Drawing.Point(15, 133);
            this.portC.Name = "portC";
            this.portC.Size = new System.Drawing.Size(121, 28);
            this.portC.TabIndex = 5;
            this.portC.Text = "9899";
            this.portC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.port_KeyPress);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 171);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.portL);
            this.Controls.Add(this.ip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Start";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.TextBox portL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox portC;
    }
}