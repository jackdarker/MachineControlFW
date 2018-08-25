namespace BKDemo
{
    partial class BKDemoApp
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.wagoBKPane1 = new WagoBK.WagoBKPane();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btPowerOn
            // 
            this.btPowerOn.Location = new System.Drawing.Point(6, 3);
            this.btPowerOn.Name = "btPowerOn";
            this.btPowerOn.Size = new System.Drawing.Size(115, 33);
            this.btPowerOn.TabIndex = 0;
            this.btPowerOn.Text = "PowerOn";
            this.btPowerOn.UseVisualStyleBackColor = true;
            this.btPowerOn.Click += new System.EventHandler(this.btPowerOn_Click);
            // 
            // btGS
            // 
            this.btGS.Location = new System.Drawing.Point(6, 42);
            this.btGS.Name = "btGS";
            this.btGS.Size = new System.Drawing.Size(115, 33);
            this.btGS.TabIndex = 0;
            this.btGS.Text = "Homing";
            this.btGS.UseVisualStyleBackColor = true;
            // 
            // btSledgeIn
            // 
            this.btSledgeIn.Location = new System.Drawing.Point(6, 100);
            this.btSledgeIn.Name = "btSledgeIn";
            this.btSledgeIn.Size = new System.Drawing.Size(115, 33);
            this.btSledgeIn.TabIndex = 0;
            this.btSledgeIn.Text = "Sledge In";
            this.btSledgeIn.UseVisualStyleBackColor = true;
            this.btSledgeIn.Click += new System.EventHandler(this.btSledgeIn_Click);
            // 
            // btSledgeOut
            // 
            this.btSledgeOut.Location = new System.Drawing.Point(6, 220);
            this.btSledgeOut.Name = "btSledgeOut";
            this.btSledgeOut.Size = new System.Drawing.Size(115, 33);
            this.btSledgeOut.TabIndex = 0;
            this.btSledgeOut.Text = "Sledge Out";
            this.btSledgeOut.UseVisualStyleBackColor = true;
            this.btSledgeOut.Click += new System.EventHandler(this.btSledgeOut_Click);
            // 
            // btIndexDown
            // 
            this.btIndexDown.Location = new System.Drawing.Point(127, 139);
            this.btIndexDown.Name = "btIndexDown";
            this.btIndexDown.Size = new System.Drawing.Size(115, 33);
            this.btIndexDown.TabIndex = 0;
            this.btIndexDown.Text = "IndexDown";
            this.btIndexDown.UseVisualStyleBackColor = true;
            this.btIndexDown.Click += new System.EventHandler(this.btIndexDown_Click);
            // 
            // btIndexUp
            // 
            this.btIndexUp.Location = new System.Drawing.Point(127, 178);
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
            this.label1.Location = new System.Drawing.Point(158, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(594, 499);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.numericUpDown1);
            this.tabPage1.Controls.Add(this.btPowerOn);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btGS);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.btSledgeOut);
            this.tabPage1.Controls.Add(this.btSledgeIn);
            this.tabPage1.Controls.Add(this.btIndexUp);
            this.tabPage1.Controls.Add(this.btIndexDown);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(586, 473);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Location = new System.Drawing.Point(127, 304);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 331);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 29);
            this.button2.TabIndex = 0;
            this.button2.Text = "Get Freq";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Set Freq";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.wagoBKPane1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(586, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // wagoBKPane1
            // 
            this.wagoBKPane1.Location = new System.Drawing.Point(0, 0);
            this.wagoBKPane1.Name = "wagoBKPane1";
            this.wagoBKPane1.Size = new System.Drawing.Size(586, 473);
            this.wagoBKPane1.TabIndex = 0;
            // 
            // BKDemoApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 547);
            this.Controls.Add(this.tabControl1);
            this.Name = "BKDemoApp";
            this.Text = "DemoApp";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btPowerOn;
        private System.Windows.Forms.Button btGS;
        private System.Windows.Forms.Button btSledgeIn;
        private System.Windows.Forms.Button btSledgeOut;
        private System.Windows.Forms.Button btIndexDown;
        private System.Windows.Forms.Button btIndexUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private WagoBK.WagoBKPane wagoBKPane1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}