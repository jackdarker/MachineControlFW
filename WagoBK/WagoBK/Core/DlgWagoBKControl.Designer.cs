namespace WagoBK
{
    partial class DlgWagoBKControl
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
            this.wagoBKPane1 = new WagoBK.WagoBKPane();
            this.SuspendLayout();
            // 
            // wagoBKPane1
            // 
            this.wagoBKPane1.Location = new System.Drawing.Point(12, 12);
            this.wagoBKPane1.Name = "wagoBKPane1";
            this.wagoBKPane1.Size = new System.Drawing.Size(650, 485);
            this.wagoBKPane1.TabIndex = 0;
            // 
            // DlgWagoBKControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 509);
            this.Controls.Add(this.wagoBKPane1);
            this.Name = "DlgWagoBKControl";
            this.Text = "BKPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private WagoBKPane wagoBKPane1;

    }
}

