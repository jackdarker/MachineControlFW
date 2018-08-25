namespace ModbusServerDemo
{
    partial class ModbusClientDemo
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
            this.btConnect = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.btFC3 = new System.Windows.Forms.Button();
            this.modbusLog1 = new Modbus.ModbusLog();
            this.SuspendLayout();
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(23, 28);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 0;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(23, 57);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btDisconnect.TabIndex = 0;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // btFC3
            // 
            this.btFC3.Location = new System.Drawing.Point(23, 99);
            this.btFC3.Name = "btFC3";
            this.btFC3.Size = new System.Drawing.Size(75, 23);
            this.btFC3.TabIndex = 0;
            this.btFC3.Text = "Send FC3";
            this.btFC3.UseVisualStyleBackColor = true;
            this.btFC3.Click += new System.EventHandler(this.btFC3_Click);
            // 
            // modbusLog1
            // 
            this.modbusLog1.Location = new System.Drawing.Point(104, 12);
            this.modbusLog1.Name = "modbusLog1";
            this.modbusLog1.Size = new System.Drawing.Size(428, 325);
            this.modbusLog1.TabIndex = 1;
            // 
            // ModbusClientDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 349);
            this.Controls.Add(this.modbusLog1);
            this.Controls.Add(this.btFC3);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.btConnect);
            this.Name = "ModbusClientDemo";
            this.Text = "ModbusClientDemo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.Button btFC3;
        private Modbus.ModbusLog modbusLog1;
    }
}