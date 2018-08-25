using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maschine1.Statistic
{

    public partial class CounterEditor : UserControl
    {
        public CounterEditor()
        {
            InitializeComponent();
        }
        //connects to a Counter for automatic update
        public void ConnectEvent(TypeCounter Counter)
        {
            m_Counter= Counter;
            Counter.EventUpdate += new TypeCounter.OnUpdateEventHandler(OnUpdateEvent);
            Translator.getTranslator.EventRetranslate += new Translator.OnRetranslateEventHandler(OnRetranslateEvent);
            Retranslate();
        }
        void OnUpdateEvent(object sender, EventArgs e)
        {
            if (this != null && this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CounterTable.DelegateUpdateTable(UpdateTable));
                }
                else
                {
                    UpdateTable();
                }
            }

        }
        private void Retranslate() {
            this.btDeleteCounter.Text = Translator.getTranslator.Txt("Delete Counter", "Counter");
            this.btPackageCounter.Text = Translator.getTranslator.Txt("Set Package Counter", "Counter");
            this.btPackageSize.Text = Translator.getTranslator.Txt("Set Package Size", "Counter");

        }
        private void OnRetranslateEvent(object sender, EventArgs e) {
            if (this != null && this.IsHandleCreated) {
                if (this.InvokeRequired) {
                    this.Invoke(new Translator.DelegateRetranslate(Retranslate));
                } else {
                    Retranslate();
                }
            }
        }
        private void InitTypeSelector()
        {
            cbTypeSelector.DataSource = m_Counter.GetAllTypeNames();
            if (cbTypeSelector.Items.Count > 0)
            {
                cbTypeSelector.SelectedIndex = 0;
                cbTypeSelector.Enabled = true;
            }
            else
            {
                cbTypeSelector.Enabled = false;
            }     
        }
        private void UpdateTable()
        {
            counterTable1.UpdateTable();
            //also update Typeselector if Types were deleted/added
            string LastSelectedType = (string)cbTypeSelector.SelectedItem;
            cbTypeSelector.DataSource = m_Counter.GetAllTypeNames();
            if (cbTypeSelector.Items.Count > 0)
            {
                if (LastSelectedType!=null && cbTypeSelector.Items.Contains(LastSelectedType))
                {
                    cbTypeSelector.SelectedItem=LastSelectedType;
                }
                else
                {
                    cbTypeSelector.SelectedIndex = 0;
                }
                cbTypeSelector.Enabled = true;
            }
            else
            {
                cbTypeSelector.Enabled = false;
            } 
            //restore last selection if possible

        }
        private void CounterEditor_Load(object sender, EventArgs e)  {
            if (!DesignMode) {
                UpdateTable();
            }
        }
        protected Statistic.TypeCounter m_Counter;
        private void btRefresh_Click(object sender, EventArgs e)
        {
           UpdateTable();
        }

        private void btDeleteCounter_Click(object sender, EventArgs e) {
            m_Counter.DeleteCounter(cbTypeSelector.Text);
        }

    }
            //Table for CounterOverview
    public partial class CounterTable : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCountPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCountFail;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayCountPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayCountFail;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CounterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCountPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCountFail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayCountPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayCountFail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.CounterName,
                this.TotalCountPass,
                this.TotalCountFail,
                this.DayCountPass,
                this.DayCountFail});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            this.dataGridView1.Size = new System.Drawing.Size(350, 473);
            this.dataGridView1.TabIndex = 1;

            this.CounterName.HeaderText = "CounterName";
            this.CounterName.Name = "CounterName";
            this.CounterName.ReadOnly = true;
            this.CounterName.Width = 100;

            this.TotalCountPass.HeaderText = "TotalCountPass";
            this.TotalCountPass.Name = "TotalCountPass";
            this.TotalCountPass.ReadOnly = true;
            this.TotalCountPass.Width = 40;

            this.TotalCountFail.HeaderText = "TotalCountFail";
            this.TotalCountFail.Name = "TotalCountFail";
            this.TotalCountFail.ReadOnly = true;
            this.TotalCountFail.Width = 40;

            this.DayCountPass.HeaderText = "DayCountPass";
            this.DayCountPass.Name = "DayCountPass";
            this.DayCountPass.ReadOnly = true;
            this.DayCountPass.Width = 40;

            this.DayCountFail.HeaderText = "DayCountFail";
            this.DayCountFail.Name = "DayCountFail";
            this.DayCountFail.ReadOnly = true;
            this.DayCountFail.Width = 40;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "CounterTable";
            this.Size = new System.Drawing.Size(350, 473);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
        public CounterTable()
        {
            InitializeComponent();
            CreateControl();    //erzwinge Erstellung Handle
        }
        //connects to a Counter for automatic update
        public void ConnectEvent(TypeCounter Counter)
        {
            Counter.EventUpdate += new Statistic.TypeCounter.OnUpdateEventHandler(OnUpdateEvent);
        }
        void OnUpdateEvent(object sender, EventArgs e)
        {
            if (this != null && this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateUpdateTable(UpdateTable));
                }
                else
                {
                    UpdateTable();
                }
            }
            
        }
        public delegate void DelegateUpdateTable();
        public void UpdateTable()
        {
            Statistic.TypeCounter Counter = new Statistic.TypeCounter();
            string[] Types = Counter.GetAllTypeNames();
            dataGridView1.Rows.Clear();
            foreach (string Name in Types)
            {
                Counter.LoadCounter(Name);
                DataGridViewRow Row = new DataGridViewRow();
                Row.CreateCells(dataGridView1,
                        new string[]{   Counter.GetTypeName(),
                                        string.Format("{0:d}",Counter.GetTotalCountPass()),
                                        string.Format("{0:d}",Counter.GetTotalCountFail()),
                                        string.Format("{0:d}",Counter.GetDayCountPass()),
                                        string.Format("{0:d}",Counter.GetDayCountFail())
                                });
                dataGridView1.Rows.Add(Row);
            }


        }

        private TypeCounter m_Counter = new TypeCounter();
        // public delegate void DelegateSetStatus(ResultDef Results);
        /*public void SetStatus(ResultDef Results)
        {
            dataGridView1.Rows.Clear();
            if (Results == null)
            {
            }
            else
            {
                ICollection Names = Results.GetTestStepNames();
                ResultDef.TestStepBase Step;
                foreach (string Name in Names)
                {
                    Results.GetResultValue(Name, out Step);
                    DataGridViewRow ResultRow = new DataGridViewRow();
                    ResultRow.CreateCells(dataGridView1,
                        new string[]{   Name,
                                        Step.GetResult().ToString(),
                                        "",
                                        "",
                                        Step.GetValueString()});
                    dataGridView1.Rows.Add(ResultRow);
                }
            }

        }*/
    }
}
