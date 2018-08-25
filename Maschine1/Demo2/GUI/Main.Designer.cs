namespace Demo2
{
    partial class Main
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
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.tabCounters = new System.Windows.Forms.TabPage();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.ProdType = new System.Windows.Forms.TextBox();
            this.CurrUser = new System.Windows.Forms.TextBox();
            this.CurrMode = new System.Windows.Forms.TextBox();
            this.txtEquipName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.resultTable1 = new Maschine1.ResultTable();
            this.modulDisplay1 = new Demo2.DemoModuleDispl();
            this.stationDisplay2 = new Maschine1.StationDisplay();
            this.stationDisplay1 = new Maschine1.StationDisplay();
            this.partHistoryTable1 = new Maschine1.PartHistoryTable();
            this.counterEditor1 = new Maschine1.Statistic.CounterEditor();
            this.mainSoftKeys1 = new Maschine1.GUI.MainSoftKeys();
            this.tabCtrl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.tabCounters.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tabMain);
            this.tabCtrl.Controls.Add(this.tabResults);
            this.tabCtrl.Controls.Add(this.tabCounters);
            this.tabCtrl.Controls.Add(this.tabInfo);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(111, 52);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(674, 522);
            this.tabCtrl.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.resultTable1);
            this.tabMain.Controls.Add(this.modulDisplay1);
            this.tabMain.Controls.Add(this.stationDisplay2);
            this.tabMain.Controls.Add(this.stationDisplay1);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(666, 496);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Status";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.partHistoryTable1);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults.Size = new System.Drawing.Size(666, 496);
            this.tabResults.TabIndex = 1;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // tabCounters
            // 
            this.tabCounters.Controls.Add(this.counterEditor1);
            this.tabCounters.Location = new System.Drawing.Point(4, 22);
            this.tabCounters.Name = "tabCounters";
            this.tabCounters.Padding = new System.Windows.Forms.Padding(3);
            this.tabCounters.Size = new System.Drawing.Size(666, 496);
            this.tabCounters.TabIndex = 3;
            this.tabCounters.Text = "Counters";
            this.tabCounters.UseVisualStyleBackColor = true;
            // 
            // tabInfo
            // 
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(666, 496);
            this.tabInfo.TabIndex = 2;
            this.tabInfo.Text = "Log";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // ProdType
            // 
            this.ProdType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProdType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdType.Location = new System.Drawing.Point(221, 9);
            this.ProdType.Name = "ProdType";
            this.ProdType.ReadOnly = true;
            this.ProdType.Size = new System.Drawing.Size(235, 26);
            this.ProdType.TabIndex = 2;
            // 
            // CurrUser
            // 
            this.CurrUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrUser.Location = new System.Drawing.Point(462, 9);
            this.CurrUser.Name = "CurrUser";
            this.CurrUser.ReadOnly = true;
            this.CurrUser.Size = new System.Drawing.Size(102, 26);
            this.CurrUser.TabIndex = 2;
            // 
            // CurrMode
            // 
            this.CurrMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrMode.Location = new System.Drawing.Point(570, 9);
            this.CurrMode.Name = "CurrMode";
            this.CurrMode.ReadOnly = true;
            this.CurrMode.Size = new System.Drawing.Size(102, 26);
            this.CurrMode.TabIndex = 2;
            // 
            // txtEquipName
            // 
            this.txtEquipName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEquipName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEquipName.Location = new System.Drawing.Point(3, 9);
            this.txtEquipName.Name = "txtEquipName";
            this.txtEquipName.ReadOnly = true;
            this.txtEquipName.Size = new System.Drawing.Size(212, 26);
            this.txtEquipName.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(117, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 340);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.txtEquipName);
            this.panel2.Controls.Add(this.ProdType);
            this.panel2.Controls.Add(this.CurrMode);
            this.panel2.Controls.Add(this.CurrUser);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(785, 52);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.mainSoftKeys1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(111, 522);
            this.panel3.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Demo2.Properties.Resources._01_PrehLogo_basis_rgb;
            this.pictureBox1.InitialImage = global::Demo2.Properties.Resources._01_PrehLogo_basis_rgb;
            this.pictureBox1.Location = new System.Drawing.Point(685, -13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // resultTable1
            // 
            this.resultTable1.Location = new System.Drawing.Point(276, 142);
            this.resultTable1.Name = "resultTable1";
            this.resultTable1.Size = new System.Drawing.Size(379, 343);
            this.resultTable1.TabIndex = 3;
            // 
            // modulDisplay1
            // 
            this.modulDisplay1.Location = new System.Drawing.Point(6, 142);
            this.modulDisplay1.Name = "modulDisplay1";
            this.modulDisplay1.Size = new System.Drawing.Size(264, 343);
            this.modulDisplay1.TabIndex = 2;
            // 
            // stationDisplay2
            // 
            this.stationDisplay2.AutoSize = true;
            this.stationDisplay2.Location = new System.Drawing.Point(214, 16);
            this.stationDisplay2.Name = "stationDisplay2";
            this.stationDisplay2.Size = new System.Drawing.Size(189, 101);
            this.stationDisplay2.TabIndex = 1;
            // 
            // stationDisplay1
            // 
            this.stationDisplay1.AutoSize = true;
            this.stationDisplay1.Location = new System.Drawing.Point(6, 16);
            this.stationDisplay1.Name = "stationDisplay1";
            this.stationDisplay1.Size = new System.Drawing.Size(188, 101);
            this.stationDisplay1.TabIndex = 0;
            // 
            // partHistoryTable1
            // 
            this.partHistoryTable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partHistoryTable1.Location = new System.Drawing.Point(3, 3);
            this.partHistoryTable1.Name = "partHistoryTable1";
            this.partHistoryTable1.Size = new System.Drawing.Size(660, 490);
            this.partHistoryTable1.TabIndex = 2;
            // 
            // counterEditor1
            // 
            this.counterEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.counterEditor1.Location = new System.Drawing.Point(3, 3);
            this.counterEditor1.Name = "counterEditor1";
            this.counterEditor1.Size = new System.Drawing.Size(660, 490);
            this.counterEditor1.TabIndex = 0;
            // 
            // mainSoftKeys1
            // 
            this.mainSoftKeys1.Location = new System.Drawing.Point(3, 3);
            this.mainSoftKeys1.Name = "mainSoftKeys1";
            this.mainSoftKeys1.Size = new System.Drawing.Size(106, 399);
            this.mainSoftKeys1.TabIndex = 1;
            this.mainSoftKeys1.ButtonClick += new Maschine1.GUI.MainSoftKeys.ButtonClickEventHandler(this.mainSoftKeys1_ButtonClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 574);
            this.Controls.Add(this.tabCtrl);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.tabCtrl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabResults.ResumeLayout(false);
            this.tabCounters.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabResults;
        private Maschine1.GUI.MainSoftKeys mainSoftKeys1;
        private Maschine1.StationDisplay stationDisplay1;
        private Maschine1.StationDisplay stationDisplay2;
        private System.Windows.Forms.TabPage tabInfo;
        
        private DemoModuleDispl modulDisplay1;
        //private Maschine1.ModuleDisplay modulDisplay1;

        private Maschine1.PartHistoryTable partHistoryTable1;
        private Maschine1.ResultTable resultTable1;
        private System.Windows.Forms.TabPage tabCounters;
        private System.Windows.Forms.TextBox ProdType;
        private System.Windows.Forms.TextBox CurrUser;
        private System.Windows.Forms.TextBox CurrMode;
        private Maschine1.Statistic.CounterEditor counterEditor1;
        private System.Windows.Forms.TextBox txtEquipName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

