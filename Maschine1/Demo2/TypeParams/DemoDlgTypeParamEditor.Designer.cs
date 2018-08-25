namespace Demo2 {
    partial class DemoDlgTypeParamEditor {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        new private void InitializeComponent() {
            this.lbCustomerNo = new System.Windows.Forms.Label();
            this.txtCustomerNo = new System.Windows.Forms.TextBox();
            this.lbVoltage = new System.Windows.Forms.Label();
            this.txtVoltage = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoltage)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtVoltage);
            this.tabPage1.Controls.Add(this.lbVoltage);
            this.tabPage1.Controls.Add(this.txtCustomerNo);
            this.tabPage1.Controls.Add(this.lbCustomerNo);
            // 
            // lbCustomerNo
            // 
            this.lbCustomerNo.AutoSize = true;
            this.lbCustomerNo.Location = new System.Drawing.Point(36, 23);
            this.lbCustomerNo.Name = "lbCustomerNo";
            this.lbCustomerNo.Size = new System.Drawing.Size(35, 13);
            this.lbCustomerNo.TabIndex = 0;
            this.lbCustomerNo.Text = "label3";
            // 
            // txtCustomerNo
            // 
            this.txtCustomerNo.Location = new System.Drawing.Point(39, 39);
            this.txtCustomerNo.Name = "txtCustomerNo";
            this.txtCustomerNo.Size = new System.Drawing.Size(141, 20);
            this.txtCustomerNo.TabIndex = 1;
            // 
            // lbVoltage
            // 
            this.lbVoltage.AutoSize = true;
            this.lbVoltage.Location = new System.Drawing.Point(36, 76);
            this.lbVoltage.Name = "lbVoltage";
            this.lbVoltage.Size = new System.Drawing.Size(35, 13);
            this.lbVoltage.TabIndex = 0;
            this.lbVoltage.Text = "label3";
            // 
            // txtVoltage
            // 
            this.txtVoltage.DecimalPlaces = 2;
            this.txtVoltage.Location = new System.Drawing.Point(39, 92);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.Size = new System.Drawing.Size(85, 20);
            this.txtVoltage.TabIndex = 2;
            // 
            // DemoDlgTypeParamEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(685, 481);
            this.Name = "DemoDlgTypeParamEditor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoltage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.NumericUpDown txtVoltage;
        private System.Windows.Forms.Label lbVoltage;
        private System.Windows.Forms.TextBox txtCustomerNo;
        private System.Windows.Forms.Label lbCustomerNo;


    }
}