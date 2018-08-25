namespace WagoBK.Test
{
    partial class BKSim
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
            this.btListen = new System.Windows.Forms.CheckBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btReceive = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.btSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DInputs = new System.Windows.Forms.FlowLayoutPanel();
            this.DOutputs = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btListen
            // 
            this.btListen.AutoSize = true;
            this.btListen.Location = new System.Drawing.Point(274, 25);
            this.btListen.Name = "btListen";
            this.btListen.Size = new System.Drawing.Size(54, 17);
            this.btListen.TabIndex = 12;
            this.btListen.Text = "Listen";
            this.btListen.UseVisualStyleBackColor = true;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(211, 25);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(57, 20);
            this.txtPort.TabIndex = 9;
            // 
            // txtReceive
            // 
            this.txtReceive.Location = new System.Drawing.Point(105, 54);
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.Size = new System.Drawing.Size(315, 20);
            this.txtReceive.TabIndex = 10;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(105, 83);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(315, 20);
            this.txtSend.TabIndex = 11;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(105, 25);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 8;
            // 
            // btReceive
            // 
            this.btReceive.Location = new System.Drawing.Point(12, 52);
            this.btReceive.Name = "btReceive";
            this.btReceive.Size = new System.Drawing.Size(75, 23);
            this.btReceive.TabIndex = 7;
            this.btReceive.Text = "Receive";
            this.btReceive.UseVisualStyleBackColor = true;
            this.btReceive.Click += new System.EventHandler(this.btReceive_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(345, 23);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btDisconnect.TabIndex = 4;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(12, 23);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 5;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(12, 81);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 23);
            this.btSend.TabIndex = 6;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Port";
            // 
            // DInputs
            // 
            this.DInputs.AutoScroll = true;
            this.DInputs.Location = new System.Drawing.Point(258, 142);
            this.DInputs.Name = "DInputs";
            this.DInputs.Size = new System.Drawing.Size(240, 378);
            this.DInputs.TabIndex = 16;
            // 
            // DOutputs
            // 
            this.DOutputs.AutoScroll = true;
            this.DOutputs.Location = new System.Drawing.Point(12, 142);
            this.DOutputs.Name = "DOutputs";
            this.DOutputs.Size = new System.Drawing.Size(240, 378);
            this.DOutputs.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Received from App";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Send to App";
            // 
            // BKSim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 532);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DInputs);
            this.Controls.Add(this.DOutputs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btListen);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btReceive);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.btSend);
            this.Name = "BKSim";
            this.Text = "BKSim";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BKSim_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox btListen;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btReceive;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel DInputs;
        private System.Windows.Forms.FlowLayoutPanel DOutputs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}