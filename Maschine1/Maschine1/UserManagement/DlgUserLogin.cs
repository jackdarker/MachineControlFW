using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maschine1.UserManagement {
    public partial class DlgUserLogin : Form {
        public DlgUserLogin() {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e) {
            if (UserManager.getUserManager.LoginUser(cbUser.Text, txtPassword.Text)) {
                this.Close();
            } else {
                toolTip1.ToolTipIcon = ToolTipIcon.Error;
                toolTip1.Show("Passwort falsch", txtPassword, 3000);
            }
        }

        private void btCancle_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void DlgUserLogin_Load(object sender, EventArgs e) {
            foreach (object x in UserManager.getUserManager.GetUsers()) {
                cbUser.Items.Add(x);
            }
            cbUser.SelectedItem = UserManager.getUserManager.GetCurrentUser().Name;
            cbUser.SelectedValueChanged += new System.EventHandler(cbUser_SelectedValueChanged);            
        }
        private void cbUser_SelectedValueChanged(object sender, EventArgs e) {
            this.txtPassword.Text = "";  
        }
        private DlgUserManagement dlg = null;
        private void btManager_Click(object sender, EventArgs e) {
            if (dlg == null) {
                dlg = new DlgUserManagement();
            };
            dlg.Show(this);
            dlg = null;
        }
    }
}
