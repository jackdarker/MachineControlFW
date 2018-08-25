using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Maschine1
{
    public partial class PartHistoryTable : UserControl
    {
        private const int HistoryLength=5;
        public PartHistoryTable()
        {
            InitializeComponent();
            this.dataGridView1.DoubleClick += new System.EventHandler(this.Table_DoubleClick);
            CreateControl();    //erzwinge Erstellung Handle
        }
        public void SetResultTabelHandle(ref ResultTable ResultTabelHandel)
        {
            m_ResultTable = ResultTabelHandel;
        }
        public delegate void DelegateAddPart(WTFileBase Info);
        public void AddPart(WTFileBase Info)
        {
            if(Info == null){ }
            else
            {
                History.Add(new WTFileBase(Info));
                DataGridViewRow ResultRow = new DataGridViewRow();
                ResultRow.CreateCells(dataGridView1,
                        new string[]{   string.Format("{0:G}",DateTime.Now),
                                        Info.GetTypeName(),
                                        Info.GetSN(),
                                        Info.GetResultDef().GetTotalResult().ToString()} );
                dataGridView1.Rows.Insert(0,ResultRow);
                dataGridView1.ClearSelection();
                ResultRow.Selected = true;
                if (dataGridView1.Rows.Count > HistoryLength)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                    History.RemoveAt(0);
                };
            }
        }
        public delegate void DelegateClear();
        public void Clear()
        {
            dataGridView1.Rows.Clear();
        }
        private void Table_DoubleClick(object sender, EventArgs e)
        {

        }
        private void UpdateResultTable(WTFileBase Info)
        {
            resultTable1.SetStatus(Info.GetResultDef());
        }

        private List<WTFileBase> History = new List<WTFileBase>();
        private ResultTable m_ResultTable = null;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Index = dataGridView1.SelectedRows[0].Index;
                UpdateResultTable(History[History.Count - 1 - Index]);
            }
            else
            {
                resultTable1.SetStatus(null);
            }
        }

    }
}
