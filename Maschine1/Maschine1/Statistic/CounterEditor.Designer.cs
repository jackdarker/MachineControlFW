namespace Maschine1.Statistic
{
    partial class CounterEditor
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
            this.btPackageCounter = new System.Windows.Forms.Button();
            this.btPackageSize = new System.Windows.Forms.Button();
            this.btDeleteCounter = new System.Windows.Forms.Button();
            this.cbTypeSelector = new System.Windows.Forms.ComboBox();
            this.PackageSize = new System.Windows.Forms.NumericUpDown();
            this.PackageCount = new System.Windows.Forms.NumericUpDown();
            this.btRefresh = new System.Windows.Forms.Button();
            this.counterTable1 = new Maschine1.Statistic.CounterTable();
            ((System.ComponentModel.ISupportInitialize)(this.PackageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackageCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btPackageCounter
            // 
            this.btPackageCounter.Location = new System.Drawing.Point(511, 79);
            this.btPackageCounter.Name = "btPackageCounter";
            this.btPackageCounter.Size = new System.Drawing.Size(136, 23);
            this.btPackageCounter.TabIndex = 5;
            this.btPackageCounter.Text = "Set Package Count";
            this.btPackageCounter.UseVisualStyleBackColor = true;
            // 
            // btPackageSize
            // 
            this.btPackageSize.Location = new System.Drawing.Point(511, 50);
            this.btPackageSize.Name = "btPackageSize";
            this.btPackageSize.Size = new System.Drawing.Size(136, 23);
            this.btPackageSize.TabIndex = 4;
            this.btPackageSize.Text = "Set Package Size";
            this.btPackageSize.UseVisualStyleBackColor = true;
            // 
            // btDeleteCounter
            // 
            this.btDeleteCounter.Location = new System.Drawing.Point(511, 131);
            this.btDeleteCounter.Name = "btDeleteCounter";
            this.btDeleteCounter.Size = new System.Drawing.Size(136, 23);
            this.btDeleteCounter.TabIndex = 3;
            this.btDeleteCounter.Text = "Delete Counter";
            this.btDeleteCounter.UseVisualStyleBackColor = true;
            this.btDeleteCounter.Click += new System.EventHandler(this.btDeleteCounter_Click);
            // 
            // cbTypeSelector
            // 
            this.cbTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeSelector.Enabled = false;
            this.cbTypeSelector.FormattingEnabled = true;
            this.cbTypeSelector.Location = new System.Drawing.Point(439, 23);
            this.cbTypeSelector.Name = "cbTypeSelector";
            this.cbTypeSelector.Size = new System.Drawing.Size(208, 21);
            this.cbTypeSelector.TabIndex = 6;
            // 
            // PackageSize
            // 
            this.PackageSize.Location = new System.Drawing.Point(460, 53);
            this.PackageSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PackageSize.Name = "PackageSize";
            this.PackageSize.Size = new System.Drawing.Size(48, 20);
            this.PackageSize.TabIndex = 7;
            // 
            // PackageCount
            // 
            this.PackageCount.Location = new System.Drawing.Point(460, 82);
            this.PackageCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PackageCount.Name = "PackageCount";
            this.PackageCount.Size = new System.Drawing.Size(48, 20);
            this.PackageCount.TabIndex = 7;
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(511, 432);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(136, 23);
            this.btRefresh.TabIndex = 3;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // counterTable1
            // 
            this.counterTable1.Location = new System.Drawing.Point(0, 0);
            this.counterTable1.Name = "counterTable1";
            this.counterTable1.Size = new System.Drawing.Size(433, 460);
            this.counterTable1.TabIndex = 0;
            // 
            // CounterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PackageCount);
            this.Controls.Add(this.PackageSize);
            this.Controls.Add(this.cbTypeSelector);
            this.Controls.Add(this.btPackageCounter);
            this.Controls.Add(this.btPackageSize);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.btDeleteCounter);
            this.Controls.Add(this.counterTable1);
            this.Name = "CounterEditor";
            this.Size = new System.Drawing.Size(650, 460);
            this.Load += new System.EventHandler(this.CounterEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PackageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PackageCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CounterTable counterTable1;
        private System.Windows.Forms.Button btPackageCounter;
        private System.Windows.Forms.Button btPackageSize;
        private System.Windows.Forms.Button btDeleteCounter;
        private System.Windows.Forms.ComboBox cbTypeSelector;
        private System.Windows.Forms.NumericUpDown PackageSize;
        private System.Windows.Forms.NumericUpDown PackageCount;
        private System.Windows.Forms.Button btRefresh;
    }
}
