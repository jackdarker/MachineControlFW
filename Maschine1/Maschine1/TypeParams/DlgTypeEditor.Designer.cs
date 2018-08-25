namespace Maschine1
{
    partial class DlgTypeEditor
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
        protected void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.limitEditor1 = new Maschine1.LimitEditor();
            this.BtSave = new System.Windows.Forms.Button();
            this.BtExit = new System.Windows.Forms.Button();
            this.BtDelete = new System.Windows.Forms.Button();
            this.cbTypeSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(661, 420);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(653, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Global";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.limitEditor1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(653, 394);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Results&Limits";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // limitEditor1
            // 
            this.limitEditor1.Location = new System.Drawing.Point(0, 3);
            this.limitEditor1.Name = "limitEditor1";
            this.limitEditor1.Size = new System.Drawing.Size(599, 322);
            this.limitEditor1.TabIndex = 0;
            // 
            // BtSave
            // 
            this.BtSave.Enabled = false;
            this.BtSave.Location = new System.Drawing.Point(316, 446);
            this.BtSave.Name = "BtSave";
            this.BtSave.Size = new System.Drawing.Size(75, 23);
            this.BtSave.TabIndex = 1;
            this.BtSave.Text = "Save";
            this.BtSave.UseVisualStyleBackColor = true;
            this.BtSave.Click += new System.EventHandler(this.BtSave_Click);
            // 
            // BtExit
            // 
            this.BtExit.Location = new System.Drawing.Point(12, 446);
            this.BtExit.Name = "BtExit";
            this.BtExit.Size = new System.Drawing.Size(75, 23);
            this.BtExit.TabIndex = 2;
            this.BtExit.Text = "Exit";
            this.BtExit.UseVisualStyleBackColor = true;
            this.BtExit.Click += new System.EventHandler(this.BtExit_Click);
            // 
            // BtDelete
            // 
            this.BtDelete.Enabled = false;
            this.BtDelete.Location = new System.Drawing.Point(397, 446);
            this.BtDelete.Name = "BtDelete";
            this.BtDelete.Size = new System.Drawing.Size(75, 23);
            this.BtDelete.TabIndex = 1;
            this.BtDelete.Text = "Delete";
            this.BtDelete.UseVisualStyleBackColor = true;
            this.BtDelete.Click += new System.EventHandler(this.BtDelete_Click);
            // 
            // cbTypeSelector
            // 
            this.cbTypeSelector.Enabled = false;
            this.cbTypeSelector.FormattingEnabled = true;
            this.cbTypeSelector.Location = new System.Drawing.Point(121, 448);
            this.cbTypeSelector.Name = "cbTypeSelector";
            this.cbTypeSelector.Size = new System.Drawing.Size(189, 21);
            this.cbTypeSelector.TabIndex = 0;
            this.cbTypeSelector.SelectedValueChanged += new System.EventHandler(this.TypeSelector_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select the type to edit";
            // 
            // DlgTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 481);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTypeSelector);
            this.Controls.Add(this.BtExit);
            this.Controls.Add(this.BtDelete);
            this.Controls.Add(this.BtSave);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgTypeEditor";
            this.Text = "TypeEditor";
            this.Load += new System.EventHandler(this.TypeEditor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtSave;
        private System.Windows.Forms.Button BtExit;
        private System.Windows.Forms.Button BtDelete;
        private System.Windows.Forms.ComboBox cbTypeSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private LimitEditor limitEditor1;
        private System.Windows.Forms.ToolTip toolTip1;
        protected System.Windows.Forms.TabControl tabControl1;
        protected System.Windows.Forms.TabPage tabPage1;
    }
}