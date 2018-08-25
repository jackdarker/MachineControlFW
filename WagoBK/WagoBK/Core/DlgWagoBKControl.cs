using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WagoBK
{
    public partial class DlgWagoBKControl : Form
    {
        public DlgWagoBKControl()
        {
            InitializeComponent();
            
            
        }
        public void ConnectToBK(WagoBKBase BK)
        {
            m_WagoBK = BK;
            this.wagoBKPane1.SetBK(m_WagoBK);
        }
        private WagoBKBase m_WagoBK;
    }
}
