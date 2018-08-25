namespace Maschine1
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
            this.mainSoftKeys1 = new MachineFramework.MainSoftKeys();
            this.ProdType = new System.Windows.Forms.TextBox();
            this.CurrUser = new System.Windows.Forms.TextBox();
            this.CurrMode = new System.Windows.Forms.TextBox();
            this.resultTable1 = new Maschine1.ResultTable();
            this.modulDisplay1 = new Maschine1.Demo.DemoModuleDispl();
            this.stationDisplay2 = new Maschine1.StationDisplay();
            this.stationDisplay1 = new Maschine1.StationDisplay();
            this.partHistoryTable1 = new Maschine1.PartHistoryTable();
            this.counterEditor1 = new Maschine1.Statistic.CounterEditor();
            this.tabCtrl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.tabCounters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tabMain);
            this.tabCtrl.Controls.Add(this.tabResults);
            this.tabCtrl.Controls.Add(this.tabCounters);
            this.tabCtrl.Controls.Add(this.tabInfo);
            this.tabCtrl.Location = new System.Drawing.Point(111, 44);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(669, 517);
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
            this.tabMain.Size = new System.Drawing.Size(661, 491);
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
            this.tabResults.Size = new System.Drawing.Size(661, 491);
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
            this.tabCounters.Size = new System.Drawing.Size(661, 491);
            this.tabCounters.TabIndex = 3;
            this.tabCounters.Text = "Counters";
            this.tabCounters.UseVisualStyleBackColor = true;
            // 
            // tabInfo
            // 
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(661, 491);
            this.tabInfo.TabIndex = 2;
            this.tabInfo.Text = "Log";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // mainSoftKeys1
            // 
            this.mainSoftKeys1.Location = new System.Drawing.Point(-1, 162);
            this.mainSoftKeys1.Name = "mainSoftKeys1";
            this.mainSoftKeys1.Size = new System.Drawing.Size(106, 399);
            this.mainSoftKeys1.TabIndex = 1;
            this.mainSoftKeys1.ButtonClick += new MachineFramework.MainSoftKeys.ButtonClickEventHandler(this.mainSoftKeys1_ButtonClick);
            // 
            // ProdType
            // 
            this.ProdType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProdType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdType.Location = new System.Drawing.Point(111, 12);
            this.ProdType.Name = "ProdType";
            this.ProdType.ReadOnly = true;
            this.ProdType.Size = new System.Drawing.Size(453, 26);
            this.ProdType.TabIndex = 2;
            // 
            // CurrUser
            // 
            this.CurrUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrUser.Location = new System.Drawing.Point(570, 12);
            this.CurrUser.Name = "CurrUser";
            this.CurrUser.ReadOnly = true;
            this.CurrUser.Size = new System.Drawing.Size(102, 26);
            this.CurrUser.TabIndex = 2;
            // 
            // CurrMode
            // 
            this.CurrMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrMode.Location = new System.Drawing.Point(678, 12);
            this.CurrMode.Name = "CurrMode";
            this.CurrMode.ReadOnly = true;
            this.CurrMode.Size = new System.Drawing.Size(102, 26);
            this.CurrMode.TabIndex = 2;
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
            this.partHistoryTable1.Location = new System.Drawing.Point(0, 3);
            this.partHistoryTable1.Name = "partHistoryTable1";
            this.partHistoryTable1.Size = new System.Drawing.Size(661, 441);
            this.partHistoryTable1.TabIndex = 2;
            // 
            // counterEditor1
            // 
            this.counterEditor1.Location = new System.Drawing.Point(0, 0);
            this.counterEditor1.Name = "counterEditor1";
            this.counterEditor1.Size = new System.Drawing.Size(661, 491);
            this.counterEditor1.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.CurrMode);
            this.Controls.Add(this.CurrUser);
            this.Controls.Add(this.ProdType);
            this.Controls.Add(this.mainSoftKeys1);
            this.Controls.Add(this.tabCtrl);
            this.Name = "Main";
            this.Text = "Drehmomentklassifizierung";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.tabCtrl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabResults.ResumeLayout(false);
            this.tabCounters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabResults;
        private MachineFramework.MainSoftKeys mainSoftKeys1;
        private StationDisplay stationDisplay1;
        private StationDisplay stationDisplay2;
        private System.Windows.Forms.TabPage tabInfo;
        
        private Maschine1.Demo.DemoModuleDispl modulDisplay1;
        //private Maschine1.ModuleDisplay modulDisplay1;
        
        private PartHistoryTable partHistoryTable1;
        private ResultTable resultTable1;
        private System.Windows.Forms.TabPage tabCounters;
        private System.Windows.Forms.TextBox ProdType;
        private System.Windows.Forms.TextBox CurrUser;
        private System.Windows.Forms.TextBox CurrMode;
        private Maschine1.Statistic.CounterEditor counterEditor1;
    }
}

