using System.Collections.Generic;
namespace Maschine1.GUI {
    partial class DlgLogBrowser {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.btLoadHistory = new System.Windows.Forms.Button();
            this.iLogEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbMinThreatLevel = new System.Windows.Forms.ComboBox();
            this.cbMaxThreatLevel = new System.Windows.Forms.ComboBox();
            this.cbMinTimestamp = new System.Windows.Forms.DateTimePicker();
            this.cbMaxTimestamp = new System.Windows.Forms.DateTimePicker();
            this.btShowFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iLogEntryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btLoadHistory
            // 
            this.btLoadHistory.Location = new System.Drawing.Point(13, 13);
            this.btLoadHistory.Name = "btLoadHistory";
            this.btLoadHistory.Size = new System.Drawing.Size(75, 23);
            this.btLoadHistory.TabIndex = 0;
            this.btLoadHistory.Text = "Load";
            this.btLoadHistory.UseVisualStyleBackColor = true;
            this.btLoadHistory.Click += new System.EventHandler(this.btLoadHistory_Click);
            // 
            // iLogEntryBindingSource
            // 
            this.iLogEntryBindingSource.DataSource = typeof(System.Collections.Generic.List<Maschine1.Logging.LogEntry>);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(12, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(656, 328);
            this.dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "GetData().m_Timestamp";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.cbMinThreatLevel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbMaxThreatLevel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbMaxTimestamp, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbMinTimestamp, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(110, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(558, 100);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cbMinThreatLevel
            // 
            this.cbMinThreatLevel.FormattingEnabled = true;
            this.cbMinThreatLevel.Location = new System.Drawing.Point(178, 3);
            this.cbMinThreatLevel.Name = "cbMinThreatLevel";
            this.cbMinThreatLevel.Size = new System.Drawing.Size(121, 21);
            this.cbMinThreatLevel.TabIndex = 0;
            // 
            // cbMaxThreatLevel
            // 
            this.cbMaxThreatLevel.FormattingEnabled = true;
            this.cbMaxThreatLevel.Location = new System.Drawing.Point(384, 3);
            this.cbMaxThreatLevel.Name = "cbMaxThreatLevel";
            this.cbMaxThreatLevel.Size = new System.Drawing.Size(121, 21);
            this.cbMaxThreatLevel.TabIndex = 1;
            // 
            // cbMinTimestamp
            // 
            this.cbMinTimestamp.Location = new System.Drawing.Point(178, 53);
            this.cbMinTimestamp.Name = "cbMinTimestamp";
            this.cbMinTimestamp.Size = new System.Drawing.Size(169, 20);
            this.cbMinTimestamp.TabIndex = 2;
            // 
            // cbMaxTimestamp
            // 
            this.cbMaxTimestamp.Location = new System.Drawing.Point(384, 53);
            this.cbMaxTimestamp.Name = "cbMaxTimestamp";
            this.cbMaxTimestamp.Size = new System.Drawing.Size(171, 20);
            this.cbMaxTimestamp.TabIndex = 3;
            // 
            // btShowFilter
            // 
            this.btShowFilter.Location = new System.Drawing.Point(13, 42);
            this.btShowFilter.Name = "btShowFilter";
            this.btShowFilter.Size = new System.Drawing.Size(75, 23);
            this.btShowFilter.TabIndex = 0;
            this.btShowFilter.Text = "Filter...";
            this.btShowFilter.UseVisualStyleBackColor = true;
            this.btShowFilter.Click += new System.EventHandler(this.btShowFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Threatlevel between ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "and";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(353, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "and";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Timestamp between ";
            // 
            // DlgLogBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btShowFilter);
            this.Controls.Add(this.btLoadHistory);
            this.Name = "DlgLogBrowser";
            this.Text = "DlgLogBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.iLogEntryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btLoadHistory;
        private System.Windows.Forms.BindingSource iLogEntryBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cbMinThreatLevel;
        private System.Windows.Forms.DateTimePicker cbMinTimestamp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMaxThreatLevel;
        private System.Windows.Forms.DateTimePicker cbMaxTimestamp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btShowFilter;
    }
}