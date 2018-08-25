using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maschine1.UserManagement {
    public partial class DlgUserManagement : Form {
        public DlgUserManagement() {
            InitializeComponent();
        }

        private void btCancle_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void DlgUserManagement_Load(object sender, EventArgs e) {
            cbRole.Items.Clear();
            cbRole.Items.AddRange(Enum.GetNames(typeof(User.EnuUserRole)));
            txtCurrUser.Text = UserManager.getUserManager.GetCurrentUser().Name;
            RefreshData();
            cbUser.SelectedItem = UserManager.getUserManager.GetCurrentUser().Name;
            cbUser.SelectedValueChanged += new System.EventHandler(cbUser_SelectedValueChanged);
            UserManager.getUserManager.EventUserChanged += new UserManager.OnUserChangedEventHandler(OnUserChanged);
            info.Text = "Select user to modify or delete or enter new user info.You have to enter password of current user to enable the action.";
            txtCurrUserPassword_Leave(null, null);
        }
        public delegate void DelegateOnUserChanged(object sender, EventArgs e);
        public void OnUserChanged(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new DelegateOnUserChanged(OnUserChanged), new System.Object[] { sender, e });
            } else {
                txtCurrUser.Text = UserManager.getUserManager.GetCurrentUser().Name;
                cbUser_SelectedValueChanged(null, null);
            }
        }
        private void cbUser_SelectedValueChanged(object sender, EventArgs e) {

            if (cbUser.SelectedItem == null || (string)cbUser.SelectedItem=="") {
                cbRole.Text = "";
            } else {
                cbRole.Text = Enum.GetName(typeof(User.EnuUserRole), UserManager.getUserManager.GetUserRole((string)cbUser.SelectedItem));
            }
            this.txtPassword1.Text = "";
            this.txtPassword2.Text = "";
            this.txtCurrUserPassword.Text = "";
            txtCurrUserPassword_Leave(null, null);
        }
        private void RefreshData() {
            cbUser.Items.Clear();
            foreach (object x in UserManager.getUserManager.GetUsers()) {
                cbUser.Items.Add(x);
            }
        }
        private void btCreate_Click(object sender, EventArgs e) {
            if (txtPassword1.Text != txtPassword2.Text) return;
            if (!UserManager.getUserManager.CanCreateUser(cbUser.Text,txtPassword1.Text,
                (User.EnuUserRole)Enum.Parse(typeof(User.EnuUserRole), (string)cbRole.SelectedItem))) {
                    return;
            }
            UserManager.getUserManager.CreateUser(cbUser.Text,txtPassword1.Text,
                (User.EnuUserRole)Enum.Parse(typeof(User.EnuUserRole), (string)cbRole.SelectedItem));
            RefreshData();
            cbUser_SelectedValueChanged(null,null);
        }

        private void txtCurrUserPassword_Leave(object sender, EventArgs e) {

            bool enable = (UserManager.getUserManager.VerifyUser(txtCurrUser.Text, txtCurrUserPassword.Text));
            btCreate.Enabled = enable;
            btDelete.Enabled = enable;
        }

        private void btDelete_Click(object sender, EventArgs e) {
            if (!UserManager.getUserManager.CanDeleteUser(cbUser.Text)) {
                return;
            }
            UserManager.getUserManager.DeleteUser(cbUser.Text);
            RefreshData();
            cbUser_SelectedValueChanged(null, null);
        }
    }
}
