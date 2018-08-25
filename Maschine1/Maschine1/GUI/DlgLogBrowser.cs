using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Windows.Forms;

namespace Maschine1.GUI {
    public partial class DlgLogBrowser : Form {
        public DlgLogBrowser() {
            InitializeComponent();
        }
        private List <Logging.LogEntry> m_LogEntrys=null;
        private void btLoadHistory_Click(object sender, EventArgs e) {
            this.dataGridView1.Rows.Clear();
            m_LogEntrys = Logging.Logger.getLogger.LoadHistory();
            ShowFilteredData();
        }

        private void ShowFilteredData() {
            this.dataGridView1.Rows.Clear();
            if (m_LogEntrys == null) return;
            foreach (Logging.LogEntry _Entry in m_LogEntrys) {
                DataGridViewRow _Row = new DataGridViewRow();
                _Row.CreateCells(dataGridView1,
                    new string[]{  _Entry.GetData().m_ThreatLevel.ToString(),
                                   _Entry.GetData().m_Timestamp.ToString("yyyy.MM.dd hh:mm:ss.fff", CultureInfo.InvariantCulture),
                                   _Entry.GetData().m_Programm,
                                   _Entry.GetData().m_ErrorText
                        });
                dataGridView1.Rows.Add(_Row);
            }
        }

        private void btShowFilter_Click(object sender, EventArgs e) {
            ShowFilteredData();
        }
    }
}
