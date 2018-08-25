using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maschine1
{
    public partial class DlgTypeEditor : Form
    {
        public DlgTypeEditor()
        {
            InitializeComponent();     
        }
        virtual protected TypeParams CreateDefaultTypeParameter() {
            return new TypeParams();
        }
        protected void TypeEditor_Load(object sender, EventArgs e)
        {
            if (!DesignMode) {
                m_TypeParams = CreateDefaultTypeParameter();
                Translator.getTranslator.EventRetranslate += new Translator.OnRetranslateEventHandler(OnRetranslateEvent);
                Retranslate();
                InitTypeSelector();
            }
        }
        virtual protected void Retranslate() {
            this.BtDelete.Text = Translator.getTranslator.Txt("Delete", "Typeeditor");
            this.BtSave.Text = Translator.getTranslator.Txt("Save", "Typeeditor");
            this.BtExit.Text = Translator.getTranslator.Txt("Exit", "Typeeditor");
            this.tabPage4.Text = Translator.Tr("Result&Limits", "Typeeditor");
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
            string _currType = cbTypeSelector.Text;
            cbTypeSelector.DataSource=m_TypeParams.GetTypeNames();
            if (cbTypeSelector.Items.Contains(_currType)) {
                cbTypeSelector.SelectedItem = _currType;
                cbTypeSelector.Enabled = true;
            } else if (cbTypeSelector.Items.Count > 0)   {
                cbTypeSelector.SelectedIndex = 0;
                cbTypeSelector.Enabled = true;
            }  else  {
                cbTypeSelector.Enabled = false;
            }
            LoadData();
        }
        protected bool LoadData()
        {
            try {
                m_TypeParams.LoadParams((string)cbTypeSelector.SelectedItem);
            } catch (Exception e) {
                GUI.DlgError.HandleException(e);
                m_TypeParams = CreateDefaultTypeParameter();
            }
            ParamsToPanel();
            BtSave.Enabled = true;
            BtDelete.Enabled = true;
            return true;
        }
        protected bool SaveData()
        {
            bool _ret = PanelToParams();
            if (_ret) {
               //?? m_TypeParams.SetParams(m_TypeParams.GetParams());
                m_TypeParams.SaveParams(cbTypeSelector.Text);
            }
            return _ret;
        }
        /// <summary>
        /// Kopiert die Typ-Daten ins UI
        /// Muss überladen werden
        /// </summary>
        virtual protected bool ParamsToPanel()   {
            //update LimitEditor
            this.limitEditor1.SetResultDef(m_TypeParams.GetParams().m_ResultDef);
            return true;
        }
        /// <summary>
        /// Kopiert die Typ-Daten vom UI in Datenbaustein
        /// Muss überladen werden
        /// \return status of validation. No data will be saved if false.
        /// </summary>
        virtual protected bool PanelToParams() {
            return true;
        }
        protected TypeParams m_TypeParams;

        private void TypeSelector_SelectedValueChanged(object sender, EventArgs e) {
            LoadData();
        }

        private void BtSave_Click(object sender, EventArgs e)  {
            if (SaveData()) {
                InitTypeSelector();
            }
        }

        private void BtExit_Click(object sender, EventArgs e)  {
            this.Close();
        }

        private void BtDelete_Click(object sender, EventArgs e)  {

            if (cbTypeSelector.Items.Count <= 1)
            {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.Show(Translator.Tr("at least one type must exist",""), cbTypeSelector, 3000);
           /* } else if (m_TypeParams.GetCurrTypeName() == cbTypeSelector.Text) {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.Show(Translator.Tr("cannot delete active type", ""), cbTypeSelector, 3000);*/
            } else {
                m_TypeParams.DeleteType();
                InitTypeSelector();
            }
        }

    }
}
