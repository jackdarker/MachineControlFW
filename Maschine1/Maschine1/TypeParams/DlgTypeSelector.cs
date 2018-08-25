using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Maschine1
{
    public partial class DlgTypeSelector : Form
    {
        public DlgTypeSelector()
        {
            InitializeComponent();
        }
        public void SetPreselectedType(string Name)
        {
            m_LastType = Name;
            m_CurrType = m_LastType;
        }
        public string GetSelectedType()
        {
            return m_CurrType;
        }

        private string m_LastType;
        private string m_CurrType;
        private TypeParams m_Params;

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //??schließen nur zulässig wenn Typ nicht leer
            m_CurrType = comboBox1.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //??schließen nur zulässig wenn Typ nicht leer
            m_CurrType = m_LastType;
            this.Close();
        }

        private void TypeSelector_Shown(object sender, EventArgs e)
        {
            m_Params = new TypeParams();
            comboBox1.Items.AddRange( m_Params.GetTypeNames());
            int Index=comboBox1.FindStringExact(m_LastType);
            comboBox1.SelectedIndex=Index;
        }
    }
}
