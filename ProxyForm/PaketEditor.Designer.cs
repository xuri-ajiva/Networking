using System;

namespace ProxyForm
{
    partial class PaketEditor
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.group = new System.Windows.Forms.GroupBox();
            this.rbutton1 = new System.Windows.Forms.RadioButton();
            this.rbutton2 = new System.Windows.Forms.RadioButton();
            this.rbutton3 = new System.Windows.Forms.RadioButton();
            this.rbutton4 = new System.Windows.Forms.RadioButton();
            this.group.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(52, 385);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sender";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(212, 385);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(190, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Set Bytes From File...";
            this.button1.Click += new System.EventHandler(this.loadBytesFromFile);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(198, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(190, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Clear Bytes";
            this.button2.Click += new System.EventHandler(this.clearBytes);
            // 
            // group
            // 
            this.group.Controls.Add(this.rbutton1);
            this.group.Controls.Add(this.rbutton2);
            this.group.Controls.Add(this.rbutton3);
            this.group.Controls.Add(this.rbutton4);
            this.group.Location = new System.Drawing.Point(418, 3);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(220, 36);
            this.group.TabIndex = 2;
            this.group.TabStop = false;
            this.group.Text = "Display Mode";
            // 
            // rbutton1
            // 
            this.rbutton1.Checked = true;
            this.rbutton1.Location = new System.Drawing.Point(6, 15);
            this.rbutton1.Name = "rbutton1";
            this.rbutton1.Size = new System.Drawing.Size(46, 16);
            this.rbutton1.TabIndex = 0;
            this.rbutton1.TabStop = true;
            this.rbutton1.Text = "Auto";
            this.rbutton1.Click += new System.EventHandler(this.changeByteMode);
            // 
            // rbutton2
            // 
            this.rbutton2.Location = new System.Drawing.Point(54, 15);
            this.rbutton2.Name = "rbutton2";
            this.rbutton2.Size = new System.Drawing.Size(50, 16);
            this.rbutton2.TabIndex = 1;
            this.rbutton2.Text = "ANSI";
            this.rbutton2.Click += new System.EventHandler(this.changeByteMode);
            // 
            // rbutton3
            // 
            this.rbutton3.Location = new System.Drawing.Point(106, 15);
            this.rbutton3.Name = "rbutton3";
            this.rbutton3.Size = new System.Drawing.Size(46, 16);
            this.rbutton3.TabIndex = 2;
            this.rbutton3.Text = "Hex";
            this.rbutton3.Click += new System.EventHandler(this.changeByteMode);
            // 
            // rbutton4
            // 
            this.rbutton4.Location = new System.Drawing.Point(152, 15);
            this.rbutton4.Name = "rbutton4";
            this.rbutton4.Size = new System.Drawing.Size(64, 16);
            this.rbutton4.TabIndex = 3;
            this.rbutton4.Text = "Unicode";
            this.rbutton4.Click += new System.EventHandler(this.changeByteMode);
            // 
            // PaketEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 410);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.group);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(660, 400);
            this.Name = "PaketEditor";
            this.Text = "PaketEditor";
            this.group.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.RadioButton rbutton1;
        private System.Windows.Forms.RadioButton rbutton2;
        private System.Windows.Forms.RadioButton rbutton3;
        private System.Windows.Forms.RadioButton rbutton4;
    }
}