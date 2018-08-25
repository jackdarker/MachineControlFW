namespace Maschine1
{
    partial class LimitEditor
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
            this.TestGrid = new System.Windows.Forms.DataGridView();
            this.TestStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FailCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestIgnored = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LimitGrid = new System.Windows.Forms.DataGridView();
            this.LimitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LimitType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LimitUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LimitValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btDeleteLimit = new System.Windows.Forms.Button();
            this.btNewTest = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.cbAddLimit = new System.Windows.Forms.ComboBox();
            this.btDeleteTest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TestGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TestGrid
            // 
            this.TestGrid.AllowUserToAddRows = false;
            this.TestGrid.AllowUserToDeleteRows = false;
            this.TestGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TestGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TestStep,
            this.FailCode,
            this.TestIgnored,
            this.Limit});
            this.TestGrid.Location = new System.Drawing.Point(0, 0);
            this.TestGrid.Name = "TestGrid";
            this.TestGrid.ReadOnly = true;
            this.TestGrid.RowHeadersVisible = false;
            this.TestGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TestGrid.Size = new System.Drawing.Size(599, 113);
            this.TestGrid.TabIndex = 1;
            this.TestGrid.SelectionChanged += new System.EventHandler(this.TestGrid_SelectionChanged);
            // 
            // TestStep
            // 
            this.TestStep.HeaderText = "TestStep";
            this.TestStep.Name = "TestStep";
            this.TestStep.ReadOnly = true;
            this.TestStep.Width = 200;
            // 
            // FailCode
            // 
            this.FailCode.HeaderText = "FC";
            this.FailCode.Name = "FailCode";
            this.FailCode.ReadOnly = true;
            this.FailCode.Width = 50;
            // 
            // TestIgnored
            // 
            this.TestIgnored.HeaderText = "Ignored";
            this.TestIgnored.Name = "TestIgnored";
            this.TestIgnored.ReadOnly = true;
            this.TestIgnored.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TestIgnored.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TestIgnored.Width = 25;
            // 
            // Limit
            // 
            this.Limit.HeaderText = "Limit";
            this.Limit.Name = "Limit";
            this.Limit.ReadOnly = true;
            this.Limit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Limit.Width = 200;
            // 
            // LimitGrid
            // 
            this.LimitGrid.AllowUserToAddRows = false;
            this.LimitGrid.AllowUserToDeleteRows = false;
            this.LimitGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LimitGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LimitName,
            this.LimitType,
            this.LimitUnit,
            this.LimitValue});
            this.LimitGrid.Location = new System.Drawing.Point(0, 119);
            this.LimitGrid.Name = "LimitGrid";
            this.LimitGrid.ReadOnly = true;
            this.LimitGrid.RowHeadersVisible = false;
            this.LimitGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.LimitGrid.Size = new System.Drawing.Size(599, 119);
            this.LimitGrid.TabIndex = 1;
            this.LimitGrid.SelectionChanged += new System.EventHandler(this.LimitGrid_SelectionChanged);
            // 
            // LimitName
            // 
            this.LimitName.HeaderText = "Limit";
            this.LimitName.Name = "LimitName";
            this.LimitName.ReadOnly = true;
            this.LimitName.Width = 200;
            // 
            // LimitType
            // 
            this.LimitType.HeaderText = "Type";
            this.LimitType.Name = "LimitType";
            this.LimitType.ReadOnly = true;
            // 
            // LimitUnit
            // 
            this.LimitUnit.HeaderText = "Unit";
            this.LimitUnit.Name = "LimitUnit";
            this.LimitUnit.ReadOnly = true;
            this.LimitUnit.Width = 50;
            // 
            // LimitValue
            // 
            this.LimitValue.HeaderText = "LimitValue";
            this.LimitValue.Name = "LimitValue";
            this.LimitValue.ReadOnly = true;
            this.LimitValue.Width = 200;
            // 
            // btDeleteLimit
            // 
            this.btDeleteLimit.Location = new System.Drawing.Point(482, 268);
            this.btDeleteLimit.Name = "btDeleteLimit";
            this.btDeleteLimit.Size = new System.Drawing.Size(114, 23);
            this.btDeleteLimit.TabIndex = 2;
            this.btDeleteLimit.Text = "Delete Limit";
            this.btDeleteLimit.UseVisualStyleBackColor = true;
            this.btDeleteLimit.Click += new System.EventHandler(this.btDeleteLimit_Click);
            // 
            // btNewTest
            // 
            this.btNewTest.Location = new System.Drawing.Point(482, 297);
            this.btNewTest.Name = "btNewTest";
            this.btNewTest.Size = new System.Drawing.Size(114, 23);
            this.btNewTest.TabIndex = 2;
            this.btNewTest.Text = "New Test";
            this.btNewTest.UseVisualStyleBackColor = true;
            this.btNewTest.Click += new System.EventHandler(this.btNewTest_Click);
            // 
            // btApply
            // 
            this.btApply.Location = new System.Drawing.Point(410, 244);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(66, 23);
            this.btApply.TabIndex = 2;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // cbAddLimit
            // 
            this.cbAddLimit.FormattingEnabled = true;
            this.cbAddLimit.Location = new System.Drawing.Point(482, 241);
            this.cbAddLimit.Name = "cbAddLimit";
            this.cbAddLimit.Size = new System.Drawing.Size(114, 21);
            this.cbAddLimit.TabIndex = 3;
            this.cbAddLimit.SelectionChangeCommitted += new System.EventHandler(this.cbAddLimit_SelectionChangeCommitted);
            // 
            // btDeleteTest
            // 
            this.btDeleteTest.Location = new System.Drawing.Point(482, 326);
            this.btDeleteTest.Name = "btDeleteTest";
            this.btDeleteTest.Size = new System.Drawing.Size(114, 23);
            this.btDeleteTest.TabIndex = 2;
            this.btDeleteTest.Text = "Delete Test";
            this.btDeleteTest.UseVisualStyleBackColor = true;
            this.btDeleteTest.Click += new System.EventHandler(this.btDeleteTest_Click);
            // 
            // LimitEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbAddLimit);
            this.Controls.Add(this.btDeleteTest);
            this.Controls.Add(this.btNewTest);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.btDeleteLimit);
            this.Controls.Add(this.LimitGrid);
            this.Controls.Add(this.TestGrid);
            this.Name = "LimitEditor";
            this.Size = new System.Drawing.Size(604, 358);
            ((System.ComponentModel.ISupportInitialize)(this.TestGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TestGrid;
        private System.Windows.Forms.DataGridView LimitGrid;
        private System.Windows.Forms.Button btDeleteLimit;
        private System.Windows.Forms.Button btNewTest;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn LimitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LimitType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LimitUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn LimitValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn FailCode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TestIgnored;
        private System.Windows.Forms.DataGridViewTextBoxColumn Limit;
        private System.Windows.Forms.ComboBox cbAddLimit;
        private System.Windows.Forms.Button btDeleteTest;
    }
}
