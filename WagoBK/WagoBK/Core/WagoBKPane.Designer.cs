namespace WagoBK
{
    partial class WagoBKPane
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
            this.components = new System.ComponentModel.Container();
            this.numRefreshTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.DOutputs = new System.Windows.Forms.FlowLayoutPanel();
            this.DInputs = new System.Windows.Forms.FlowLayoutPanel();
            this.btRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.AOutputs = new System.Windows.Forms.FlowLayoutPanel();
            this.AInputs = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime)).BeginInit();
            this.SuspendLayout();
            // 
            // numRefreshTime
            // 
            this.numRefreshTime.Location = new System.Drawing.Point(149, 28);
            this.numRefreshTime.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numRefreshTime.Name = "numRefreshTime";
            this.numRefreshTime.Size = new System.Drawing.Size(63, 20);
            this.numRefreshTime.TabIndex = 0;
            this.numRefreshTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRefreshTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numRefreshTime.ValueChanged += new System.EventHandler(this.numRefreshTime_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Refresh [ms]";
            // 
            // txtStatus
            // 
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatus.Location = new System.Drawing.Point(6, 28);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(137, 20);
            this.txtStatus.TabIndex = 2;
            // 
            // DOutputs
            // 
            this.DOutputs.AutoScroll = true;
            this.DOutputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DOutputs.Location = new System.Drawing.Point(6, 54);
            this.DOutputs.Name = "DOutputs";
            this.DOutputs.Size = new System.Drawing.Size(240, 300);
            this.DOutputs.TabIndex = 3;
            // 
            // DInputs
            // 
            this.DInputs.AutoScroll = true;
            this.DInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DInputs.Location = new System.Drawing.Point(252, 54);
            this.DInputs.Name = "DInputs";
            this.DInputs.Size = new System.Drawing.Size(240, 300);
            this.DInputs.TabIndex = 3;
            // 
            // btRefresh
            // 
            this.btRefresh.Location = new System.Drawing.Point(218, 28);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(65, 23);
            this.btRefresh.TabIndex = 4;
            this.btRefresh.Text = "Refresh";
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "BKState";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AOutputs
            // 
            this.AOutputs.AutoScroll = true;
            this.AOutputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AOutputs.Location = new System.Drawing.Point(6, 360);
            this.AOutputs.Name = "AOutputs";
            this.AOutputs.Size = new System.Drawing.Size(240, 93);
            this.AOutputs.TabIndex = 3;
            // 
            // AInputs
            // 
            this.AInputs.AutoScroll = true;
            this.AInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AInputs.Location = new System.Drawing.Point(252, 360);
            this.AInputs.Name = "AInputs";
            this.AInputs.Size = new System.Drawing.Size(240, 93);
            this.AInputs.TabIndex = 3;
            // 
            // WagoBKPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.DInputs);
            this.Controls.Add(this.AInputs);
            this.Controls.Add(this.AOutputs);
            this.Controls.Add(this.DOutputs);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numRefreshTime);
            this.Name = "WagoBKPane";
            this.Size = new System.Drawing.Size(497, 456);
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numRefreshTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.FlowLayoutPanel DOutputs;
        private System.Windows.Forms.FlowLayoutPanel DInputs;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FlowLayoutPanel AOutputs;
        private System.Windows.Forms.FlowLayoutPanel AInputs;
    }
}
