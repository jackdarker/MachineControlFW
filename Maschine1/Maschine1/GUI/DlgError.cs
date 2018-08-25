using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Maschine1.GUI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DlgError : Form
    {
        public static void HandleException(Exception e)
        {
            
            Logging.ILogEntry Error = new Logging.LogEntry(1, e.ToString()); //??exceptioncode
            Logging.Logger.getLogger.SaveEntry(Error);
            GUI.DlgError ErrorDlg = new GUI.DlgError(Error);   //?? Es sollte nur ein fenster für alle Errors aufpoppen, ähnlich SPS
            ErrorDlg.ShowDialog();  //??Set ToplevelWindow
        }
        /// <summary>
        /// Konstruktor für Fehlerausgabe
        /// </summary>
        /// <param name="Error"></param>
        public DlgError(Logging.ILogEntry Error)
        {
            InitializeComponent();
            m_Msg = Error.GetMsg();
            m_TextButton1 = "Abort";
            m_TextButton2 = "";
            m_TextButton3 = "Retry";
            richTextBox1.Text = m_Msg;
            if (m_TextButton1 == "" &&
                m_TextButton2 == "" &&
                m_TextButton3 == "")
            {
                m_TextButton1 = "OK";   //keine Taste vorgegeben??
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
