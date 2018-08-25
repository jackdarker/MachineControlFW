namespace Demo
{
    partial class DemoApp
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
            this.btPowerOn = new System.Windows.Forms.Button();
            this.btGS = new System.Windows.Forms.Button();
            this.btSledgeIn = new System.Windows.Forms.Button();
            this.btSledgeOut = new System.Windows.Forms.Button();
            this.btIndexDown = new System.Windows.Forms.Button();
            this.btIndexUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btPowerOn
            // 
            this.btPowerOn.Location = new System.Drawing.Point(12, 12);
            this.btPowerOn.Name = "btPowerOn";
            this.btPowerOn.Size = new System.Drawing.Size(115, 33);
            this.btPowerOn.TabIndex = 0;
            this.btPowerOn.Text = "PowerOn";
            this.btPowerOn.UseVisualStyleBackColor = true;
            this.btPowerOn.Click += new System.EventHandler(this.btPowerOn_Click);
            // 
            // btGS
            // 
            this.btGS.Location = new System.Drawing.Point(12, 51);
            this.btGS.Name = "btGS";
            this.btGS.Size = new System.Drawing.Size(115, 33);
            this.btGS.TabIndex = 0;
            this.btGS.Text = "Homing";
            this.btGS.UseVisualStyleBackColor = true;
            // 
            // btSledgeIn
            // 
            this.btSledgeIn.Location = new System.Drawing.Point(12, 109);
            this.btSledgeIn.Name = "btSledgeIn";
            this.btSledgeIn.Size = new System.Drawing.Size(115, 33);
            this.btSledgeIn.TabIndex = 0;
            this.btSledgeIn.Text = "Sledge In";
            this.btSledgeIn.UseVisualStyleBackColor = true;
            this.btSledgeIn.Click += new System.EventHandler(this.btSledgeIn_Click);
            // 
            // btSledgeOut
            // 
            this.btSledgeOut.Location = new System.Drawing.Point(12, 229);
            this.btSledgeOut.Name = "btSledgeOut";
            this.btSledgeOut.Size = new System.Drawing.Size(115, 33);
            this.btSledgeOut.TabIndex = 0;
            this.btSledgeOut.Text = "Sledge Out";
            this.btSledgeOut.UseVisualStyleBackColor = true;
            this.btSledgeOut.Click += new System.EventHandler(this.btSledgeOut_Click);
            // 
            // btIndexDown
            // 
            this.btIndexDown.Location = new System.Drawing.Point(133, 148);
            this.btIndexDown.Name = "btIndexDown";
            this.btIndexDown.Size = new System.Drawing.Size(115, 33);
            this.btIndexDown.TabIndex = 0;
            this.btIndexDown.Text = "IndexDown";
            this.btIndexDown.UseVisualStyleBackColor = true;
            this.btIndexDown.Click += new System.EventHandler(this.btIndexDown_Click);
            // 
            // btIndexUp
            // 
            this.btIndexUp.Location = new System.Drawing.Point(133, 187);
            this.btIndexUp.Name = "btIndexUp";
            this.btIndexUp.Size = new System.Drawing.Size(115, 33);
            this.btIndexUp.TabIndex = 0;
            this.btIndexUp.Text = "IndexUp";
            this.btIndexUp.UseVisualStyleBackColor = true;
            this.btIndexUp.Click += new System.EventHandler(this.btIndexUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // DemoApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 355);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSledgeOut);
            this.Controls.Add(this.btIndexUp);
            this.Controls.Add(this.btIndexDown);
            this.Controls.Add(this.btSledgeIn);
            this.Controls.Add(this.btGS);
            this.Controls.Add(this.btPowerOn);
            this.Name = "DemoApp";
            this.Text = "DemoApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btPowerOn;
        private System.Windows.Forms.Button btGS;
        private System.Windows.Forms.Button btSledgeIn;
        private System.Windows.Forms.Button btSledgeOut;
        private System.Windows.Forms.Button btIndexDown;
        private System.Windows.Forms.Button btIndexUp;
        private System.Windows.Forms.Label label1;
    }
}