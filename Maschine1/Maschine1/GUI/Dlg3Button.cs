using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maschine1.GUI
{
    public partial class Dlg3Button : Form
    {
        private Dlg3Button()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Konstruktor für modalen Dialog mit Textausgabe und 3 Tasten
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="TextButton1"></param>
        /// <param name="TextButton2"></param>
        /// <param name="TextButton3"></param>
        public Dlg3Button(string Msg, string TextButton1, string TextButton2, string TextButton3)
        {
            InitializeComponent();
            m_Msg = Msg;
            m_TextButton1 = TextButton1;
            m_TextButton2 = TextButton2;
            m_TextButton3 = TextButton3;
            richTextBox1.Text = m_Msg;
            if (m_TextButton1 == "" &&
                m_TextButton2 == "" &&
                m_TextButton3 == "")
            {
                m_TextButton1 = "OK";   //keine Taste vorgegeben?
            };

            if (m_TextButton1 != "")
            {
                this.button1.Text = m_TextButton1;
                this.button1.Visible = true;
            }
            else 
            {
                this.button1.Visible = false;
            }
            if (m_TextButton2 != "")
            {
                this.button2.Text = m_TextButton2;
                this.button2.Visible = true;
            }
            else
            {
                this.button2.Visible = false;
            }
            if (m_TextButton3 != "")
            {
                this.button3.Text = m_TextButton3;
                this.button3.Visible = true;
            }
            else
            {
                this.button3.Visible = false;
            }
        }

        private string m_Msg;
        private string m_TextButton1;
        private string m_TextButton2;
        private string m_TextButton3;
    }
}
