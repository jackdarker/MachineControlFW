using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maschine1
{
    public partial class LimitEditor : UserControl
    {
        public LimitEditor()
        {
            InitializeComponent();
            this.m_TestStepEditor = new ResultDef.TestStepBase.TestStepEditor();
            this.Controls.Add(this.m_TestStepEditor);
            this.m_TestStepEditor.Location = new System.Drawing.Point(0, this.btDeleteLimit.Top);
            this.m_TestStepEditor.Name = "TestStepEditor";
            this.m_TestStepEditor.Size = new System.Drawing.Size(300, 100);
            this.m_TestStepEditor.TabIndex = 3;
            this.m_TestStepEditor.Visible = false;

            this.m_LimitDefDoubleEditor = new ResultDef.LimitDefDouble.LimitDefDoubleEditor();
            this.Controls.Add(this.m_LimitDefDoubleEditor);
            this.m_LimitDefDoubleEditor.Location = new System.Drawing.Point(0, this.btDeleteLimit.Top);
            this.m_LimitDefDoubleEditor.Name = "LimitDefDoubleEditor";
            this.m_LimitDefDoubleEditor.Size = new System.Drawing.Size(300, 100);
            this.m_LimitDefDoubleEditor.TabIndex = 3;
            this.m_LimitDefDoubleEditor.Visible = false;

            this.m_LimitDefBoolEditor = new ResultDef.LimitDefBool.LimitDefBoolEditor();
            this.Controls.Add(this.m_LimitDefBoolEditor);
            this.m_LimitDefBoolEditor.Location = new System.Drawing.Point(0, this.btDeleteLimit.Top);
            this.m_LimitDefBoolEditor.Name = "LimitDefBoolEditor";
            this.m_LimitDefBoolEditor.Size = new System.Drawing.Size(300, 100);
            this.m_LimitDefBoolEditor.TabIndex = 3;
            this.m_LimitDefBoolEditor.Visible = false;

            cbAddLimit.DataSource = new string[] {"select Limit to add..",
                                                       "bool",
                                                       "double" };
            DisableEditors();
        }
        private ResultDef.LimitDefDouble.LimitDefDoubleEditor m_LimitDefDoubleEditor;
        private ResultDef.LimitDefBool.LimitDefBoolEditor m_LimitDefBoolEditor;
        private ResultDef.TestStepBase.TestStepEditor m_TestStepEditor;

        public void SetResultDef(ResultDef TestSteps)   {
            m_ResultDef = TestSteps;
            UpdateDisplay();
        }
        public ResultDef GetResultDef()  {
            return m_ResultDef;
        }
        private void DisableEditors()  {
            m_LimitDefBoolEditor.Visible = false;
            m_LimitDefDoubleEditor.Visible = false;
            m_TestStepEditor.Visible = false;
            btApply.Enabled = false;
        }
        private void ActivateLimitEditor(ResultDef.LimitDefBase Limit)
        {
            DisableEditors();
            if (Limit.GetType().Equals(typeof(ResultDef.LimitDefBool)))
            {
                m_LimitDefBoolEditor.SetData((ResultDef.LimitDefBool)Limit);
                m_LimitDefBoolEditor.Visible = true;
                btApply.Enabled = true;
            }
            else if (Limit.GetType().Equals(typeof(ResultDef.LimitDefDouble)))
            {
                m_LimitDefDoubleEditor.SetData((ResultDef.LimitDefDouble)Limit);
                m_LimitDefDoubleEditor.Visible = true;
                btApply.Enabled = true;
            }
        }
        private void ActivateStepEditor(ResultDef.TestStepBase Step)
        {
            DisableEditors();
            m_TestStepEditor.SetData(Step, m_ResultDef);
            m_TestStepEditor.Visible = true;
            btApply.Enabled = true;
    
        }
        private void UpdateDisplay() {
            this.LimitGrid.Rows.Clear();
            this.TestGrid.Rows.Clear();
            //UpdateLimitCombo();
            if (m_ResultDef == null) { }
            else  {
                //Display Steps
                ICollection Names = m_ResultDef.GetTestStepNames();
                ResultDef.TestStepBase Step;
                foreach (string Name in Names)  {
                    m_ResultDef.GetResultValue(Name, out Step);
                    DataGridViewRow ResultRow = new DataGridViewRow();
                    ResultRow.CreateCells(TestGrid,
                        new string[]{   Name,
                                        Step.GetFailCode(),
                                        (Step.GetResult()== ResultDef.TestResultEnum.Ignored).ToString(),
                                        Step.GetLimitName()
                                        });
                    TestGrid.Rows.Add(ResultRow);
                }
                //Display Limits
                ICollection LimitNames = m_ResultDef.GetLimitNames();
                ResultDef.LimitDefBase Limit;
                foreach (string Name in LimitNames)
                {
                    m_ResultDef.GetLimitValue(Name, out Limit);
                    DataGridViewRow LimitRow = new DataGridViewRow();
                    LimitRow.CreateCells(LimitGrid,
                        new string[]{   Name,
                                        Limit.GetType().ToString(),
                                        "",
                                        Limit.GetValueString()
                                        });
                    LimitGrid.Rows.Add(LimitRow);
                }
            }
        }
        /*private void UpdateLimitCombo()
        {
            this.Limit.Items.Clear();
            ICollection LimitNames = m_ResultDef.GetLimitNames();
            foreach (string Name in LimitNames)
            {
                this.Limit.Items.Add(Name);
            }
        }*/

        private void btApply_Click(object sender, EventArgs e)
        {
            ResultDef.LimitDefBase Limit = null;
            if (m_LimitDefBoolEditor.Visible)    {
                Limit = m_LimitDefBoolEditor.GetData();
            }
            else if (m_LimitDefDoubleEditor.Visible) {
                Limit = m_LimitDefDoubleEditor.GetData();
            }
            if (Limit != null) {
                if (m_ResultDef.LimitExists(Limit.GetName()))  {
                    m_ResultDef.ModifyLimit(Limit);
                }
                else {
                    m_ResultDef.AddLimit(Limit);
                }
            }
            ResultDef.TestStepBase Step = null;
            if (m_TestStepEditor.Visible) {
                Step = m_TestStepEditor.GetData();
            }
            if (Step != null)
            {
                if (m_ResultDef.TestStepExists(Step.GetName()))
                {
                    m_ResultDef.ModifyTestStep(Step);
                }
                else
                {
                    m_ResultDef.AddTestStep(Step);
                }
            }
            UpdateDisplay();
        }

        private ResultDef m_ResultDef;

        private void TestGrid_SelectionChanged(object sender, EventArgs e)
        {
            DisableEditors();
            LimitGrid.ClearSelection();
            if (TestGrid.SelectedRows.Count > 0)
            {
                string Name = (string)TestGrid.SelectedRows[0].Cells[TestStep.Index].Value;
                ResultDef.TestStepBase Step=null;
                m_ResultDef.GetResultValue(Name, out Step);
                ActivateStepEditor(Step);
             };
        }

        private void LimitGrid_SelectionChanged(object sender, EventArgs e)
        {
            DisableEditors();
            TestGrid.ClearSelection();
            ResultDef.LimitDefBase Limit;
            if (LimitGrid.SelectedRows.Count > 0)
            {
                string Name = (string)LimitGrid.SelectedRows[0].Cells[LimitName.Index].Value;
                m_ResultDef.GetLimitValue(Name, out Limit);
                ActivateLimitEditor(Limit);
            };
        }

        private void btDeleteLimit_Click(object sender, EventArgs e)
        {
            DisableEditors();
            if (LimitGrid.SelectedRows.Count > 0)
            {
                string Name = (string)LimitGrid.SelectedRows[0].Cells[LimitName.Index].Value;
                //??Bestätigungsabrage
                m_ResultDef.DeleteLimit(Name);
            };
        }

        private void cbAddLimit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DisableEditors();
            //geht das nicht besser??
            if (cbAddLimit.SelectedItem.ToString() == "bool")
            {
                ActivateLimitEditor(new ResultDef.LimitDefBool("",new ResultDef.LimitDefBool.strLimit()));
            }
            else if (cbAddLimit.SelectedItem.ToString() == "double")
            {
                ActivateLimitEditor(new ResultDef.LimitDefDouble("", new ResultDef.LimitDefDouble.strLimit()));
            };
        }

        private void btNewTest_Click(object sender, EventArgs e)
        {
            DisableEditors();
            ActivateStepEditor(null);
        }

        private void btDeleteTest_Click(object sender, EventArgs e)
        {
            DisableEditors();
            if (TestGrid.SelectedRows.Count > 0)
            {
                string Name = (string)TestGrid.SelectedRows[0].Cells[TestStep.Index].Value;
                //??Bestätigungsabfrage
                m_ResultDef.DeleteTestStep(Name);
            };
        }
        
    }
}
