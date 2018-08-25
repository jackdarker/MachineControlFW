using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Maschine1
{
    public partial class ResultTable : UserControl
    {
        public ResultTable()
        {
            InitializeComponent();
            CreateControl();    //erzwinge Erstellung Handle
        }
        public delegate void DelegateSetStatus(ResultDef Results);
        public void SetStatus(ResultDef Results)
        {
            dataGridView1.Rows.Clear();
            if (Results == null)
            {
            }
            else 
            {
                ICollection Names=Results.GetTestStepNames();
                ResultDef.TestStepBase Step;
                ResultDef.LimitDefBase Limit;
                foreach (string Name in Names)
                {
                    Results.GetResultValue(Name,out Step);
                    Results.GetLimitValue(Step.GetLimitName(), out Limit);
                    DataGridViewRow ResultRow = new DataGridViewRow();
                    ResultRow.CreateCells(dataGridView1,
                        new string[]{   Name,
                                        Step.GetResult().ToString(),
                                        Step.GetFailCode(),
                                        Limit.GetValueString(),
                                        Step.GetValueString(),
                                        Step.GetTestTime().ToString("HH:mm:ss.f")
                        });
                    dataGridView1.Rows.Add(ResultRow);
                }               
            }

        }

    }
}
