namespace WagoBK
{
    partial class ChannelDO
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
                this.button1 = new System.Windows.Forms.Button();
                this.ChName = new System.Windows.Forms.Label();
                this.ChBMK = new System.Windows.Forms.Label();
                this.SuspendLayout();
                // 
                // button1
                // 
                this.button1.Enabled = false;
                this.button1.Location = new System.Drawing.Point(0, 0);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(44, 26);
                this.button1.TabIndex = 0;
                this.button1.UseVisualStyleBackColor = true;
                this.button1.Click += new System.EventHandler(this.button1_Click);
                // 
                // ChName
                // 
                this.ChName.AutoSize = true;
                this.ChName.Location = new System.Drawing.Point(50, 13);
                this.ChName.Name = "ChName";
                this.ChName.Size = new System.Drawing.Size(85, 13);
                this.ChName.TabIndex = 1;
                this.ChName.Text = "My new channel";
                // 
                // ChBMK
                // 
                this.ChBMK.AutoSize = true;
                this.ChBMK.Location = new System.Drawing.Point(50, 0);
                this.ChBMK.Name = "ChBMK";
                this.ChBMK.Size = new System.Drawing.Size(85, 13);
                this.ChBMK.TabIndex = 1;
                this.ChBMK.Text = "My new channel";
                // 
                // ChannelDO
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.Controls.Add(this.ChBMK);
                this.Controls.Add(this.ChName);
                this.Controls.Add(this.button1);
                this.Name = "ChannelDO";
                this.Size = new System.Drawing.Size(201, 28);
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Button button1;
            private System.Windows.Forms.Label ChName;
            private System.Windows.Forms.Label ChBMK;
    }
}
