using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Maschine1
{
    public partial class StationDisplay : UserControl
    {
        public StationDisplay()
        {
            InitializeComponent();
        }
        public void SetTitle(string text) {
            textBox1.Text=text;
        }
        public delegate void DelegateSetStatus(string text);
        public void SetStatus(string text) {
            textBox2.BackColor = Color.LightGray;
            textBox2.Text = text;
        }
        public delegate void DelegateSetResult(ResultDef.TestResultEnum Result, string text);
        public void SetResult(ResultDef.TestResultEnum Result,string text ) {
            if(Result == ResultDef.TestResultEnum.Pass) textBox2.BackColor = Color.Green;
            else textBox2.BackColor = Color.Red;
            textBox2.Text = Result.ToString()+ "\r\n" + text ;
        }
    }
}
