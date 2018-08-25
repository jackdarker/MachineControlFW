namespace Modbus
{
    partial class ModbusUI
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbSendData = new System.Windows.Forms.ComboBox();
            this.btSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "IP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(211, 23);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(57, 20);
            this.txtPort.TabIndex = 18;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(105, 23);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 17;
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(345, 21);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btDisconnect.TabIndex = 15;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(12, 21);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 16;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 133);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(495, 293);
            this.textBox1.TabIndex = 22;
            // 
            // cbSendData
            // 
            this.cbSendData.FormattingEnabled = true;
            this.cbSendData.Location = new System.Drawing.Point(12, 69);
            this.cbSendData.Name = "cbSendData";
            this.cbSendData.Size = new System.Drawing.Size(351, 21);
            this.cbSendData.TabIndex = 23;
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(369, 67);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 23);
            this.btSend.TabIndex = 16;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // ModbusUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 438);
            this.Controls.Add(this.cbSendData);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.btConnect);
            this.Name = "ModbusUI";
            this.Text = "ModbusMonitor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.ComboBox cbSendData;
    }
}