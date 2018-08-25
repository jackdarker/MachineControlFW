namespace Maschine1.Localization {
    partial class DlgTranslatorSelection {
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
            this.btOK = new System.Windows.Forms.Button();
            this.btCancle = new System.Windows.Forms.Button();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(12, 53);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancle
            // 
            this.btCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancle.Location = new System.Drawing.Point(93, 53);
            this.btCancle.Name = "btCancle";
            this.btCancle.Size = new System.Drawing.Size(75, 23);
            this.btCancle.TabIndex = 0;
            this.btCancle.Text = "Cancle";
            this.btCancle.UseVisualStyleBackColor = true;
            this.btCancle.Click += new System.EventHandler(this.btOK_Click);
            // 
            // cbLanguage
            // 
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(12, 13);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(156, 21);
            this.cbLanguage.TabIndex = 1;
            
            // 
            // DlgTranslatorSelection
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancle;
            this.ClientSize = new System.Drawing.Size(178, 93);
            this.Controls.Add(this.cbLanguage);
            this.Controls.Add(this.btCancle);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgTranslatorSelection";
            this.Text = "DlgTranslatorSelection";
            this.Load += new System.EventHandler(this.DlgTranslatorSelection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancle;
        private System.Windows.Forms.ComboBox cbLanguage;
    }
}