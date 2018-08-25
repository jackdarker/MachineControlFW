using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Maschine1.Localization {
    public partial class DlgTranslatorSelection : Form {
        public DlgTranslatorSelection() {
            InitializeComponent();
        }

        private void DlgTranslatorSelection_Load(object sender, EventArgs e) {

            foreach (object x in Translator.getTranslator.GetAvailableLanguages()) {
                cbLanguage.Items.Add(x);
            }
            cbLanguage.SelectedItem = Translator.getTranslator.GetLanguage().ProgName;
            this.cbLanguage.SelectedValueChanged += new System.EventHandler(this.cbLanguage_SelectedValueChanged);
        }

        private void cbLanguage_SelectedValueChanged(object sender, EventArgs e) {
            Translator.getTranslator.SetLanguage(cbLanguage.SelectedItem.ToString());
        }

        private void btOK_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
