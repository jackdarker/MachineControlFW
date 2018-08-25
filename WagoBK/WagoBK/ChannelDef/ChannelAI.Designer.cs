namespace WagoBK
{
    partial class ChannelAI
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChName = new System.Windows.Forms.Label();
            this.ChBMK = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // ChName
            // 
            this.ChName.AutoSize = true;
            this.ChName.Location = new System.Drawing.Point(82, 13);
            this.ChName.Name = "ChName";
            this.ChName.Size = new System.Drawing.Size(85, 13);
            this.ChName.TabIndex = 1;
            this.ChName.Text = "My new channel";
            // 
            // ChBMK
            // 
            this.ChBMK.AutoSize = true;
            this.ChBMK.Location = new System.Drawing.Point(82, 0);
            this.ChBMK.Name = "ChBMK";
            this.ChBMK.Size = new System.Drawing.Size(85, 13);
            this.ChBMK.TabIndex = 1;
            this.ChBMK.Text = "My new channel";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Location = new System.Drawing.Point(3, 3);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(73, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // ChannelAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.ChBMK);
            this.Controls.Add(this.ChName);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.Name = "ChannelAI";
            this.Size = new System.Drawing.Size(201, 26);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ChName;
        private System.Windows.Forms.Label ChBMK;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
