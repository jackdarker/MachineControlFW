namespace Maschine1.UserManagement {
    partial class DlgUserManagement {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btCancle = new System.Windows.Forms.Button();
            this.btCreate = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.txtPassword1 = new System.Windows.Forms.TextBox();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.Role = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurrUserPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCurrUser = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.info = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbUser
            // 
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(12, 74);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(156, 21);
            this.cbUser.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "User";
            // 
            // btCancle
            // 
            this.btCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancle.Location = new System.Drawing.Point(300, 12);
            this.btCancle.Name = "btCancle";
            this.btCancle.Size = new System.Drawing.Size(75, 23);
            this.btCancle.TabIndex = 9;
            this.btCancle.Text = "Cancle";
            this.btCancle.UseVisualStyleBackColor = true;
            this.btCancle.Click += new System.EventHandler(this.btCancle_Click);
            // 
            // btCreate
            // 
            this.btCreate.Location = new System.Drawing.Point(15, 12);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(75, 23);
            this.btCreate.TabIndex = 8;
            this.btCreate.Text = "Create";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(96, 12);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 23);
            this.btDelete.TabIndex = 8;
            this.btDelete.Text = "Delete";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // txtPassword1
            // 
            this.txtPassword1.Location = new System.Drawing.Point(12, 165);
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.Size = new System.Drawing.Size(156, 20);
            this.txtPassword1.TabIndex = 10;
            this.txtPassword1.UseSystemPasswordChar = true;
            // 
            // cbRole
            // 
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Location = new System.Drawing.Point(12, 116);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(156, 21);
            this.cbRole.TabIndex = 5;
            // 
            // Role
            // 
            this.Role.AutoSize = true;
            this.Role.Location = new System.Drawing.Point(12, 100);
            this.Role.Name = "Role";
            this.Role.Size = new System.Drawing.Size(29, 13);
            this.Role.TabIndex = 11;
            this.Role.Text = "Role";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password";
            // 
            // txtPassword2
            // 
            this.txtPassword2.Location = new System.Drawing.Point(12, 204);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(156, 20);
            this.txtPassword2.TabIndex = 10;
            this.txtPassword2.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Reenter Password";
            // 
            // txtCurrUserPassword
            // 
            this.txtCurrUserPassword.Location = new System.Drawing.Point(218, 117);
            this.txtCurrUserPassword.Name = "txtCurrUserPassword";
            this.txtCurrUserPassword.Size = new System.Drawing.Size(156, 20);
            this.txtCurrUserPassword.TabIndex = 10;
            this.txtCurrUserPassword.UseSystemPasswordChar = true;
            this.txtCurrUserPassword.Leave += new System.EventHandler(this.txtCurrUserPassword_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Enter Password of current user";
            // 
            // txtCurrUser
            // 
            this.txtCurrUser.Location = new System.Drawing.Point(218, 75);
            this.txtCurrUser.Name = "txtCurrUser";
            this.txtCurrUser.ReadOnly = true;
            this.txtCurrUser.Size = new System.Drawing.Size(156, 20);
            this.txtCurrUser.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Current User";
            // 
            // info
            // 
            this.info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.info.Location = new System.Drawing.Point(177, 149);
            this.info.Multiline = true;
            this.info.Name = "info";
            this.info.ReadOnly = true;
            this.info.Size = new System.Drawing.Size(197, 75);
            this.info.TabIndex = 10;
            // 
            // DlgUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 273);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Role);
            this.Controls.Add(this.txtPassword2);
            this.Controls.Add(this.info);
            this.Controls.Add(this.txtCurrUser);
            this.Controls.Add(this.txtCurrUserPassword);
            this.Controls.Add(this.txtPassword1);
            this.Controls.Add(this.btCancle);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btCreate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.cbUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgUserManagement";
            this.Text = "DlgUserManagement";
            this.Load += new System.EventHandler(this.DlgUserManagement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCancle;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.TextBox txtPassword1;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Label Role;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrUserPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCurrUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox info;
    }
}