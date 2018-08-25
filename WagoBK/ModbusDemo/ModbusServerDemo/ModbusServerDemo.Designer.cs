namespace ModbusServerDemo
{
    partial class ModbusServerDemo
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
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.txtServerState = new System.Windows.Forms.TextBox();
            this.modbusLog1 = new Modbus.ModbusLog();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(12, 76);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(12, 106);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 0;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // txtServerState
            // 
            this.txtServerState.Location = new System.Drawing.Point(378, 52);
            this.txtServerState.Name = "txtServerState";
            this.txtServerState.ReadOnly = true;
            this.txtServerState.Size = new System.Drawing.Size(100, 20);
            this.txtServerState.TabIndex = 1;
            // 
            // modbusLog1
            // 
            this.modbusLog1.Location = new System.Drawing.Point(93, 76);
            this.modbusLog1.Name = "modbusLog1";
            this.modbusLog1.Size = new System.Drawing.Size(385, 367);
            this.modbusLog1.TabIndex = 2;
            // 
            // ModbusServerDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 470);
            this.Controls.Add(this.modbusLog1);
            this.Controls.Add(this.txtServerState);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btStart);
            this.Name = "ModbusServerDemo";
            this.Text = "ModbusServerDemo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ModbusServerDemo_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.TextBox txtServerState;
        private Modbus.ModbusLog modbusLog1;
    }
}