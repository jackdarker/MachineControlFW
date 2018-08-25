using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Modbus;

namespace ModbusServerDemo
{
    public partial class ModbusServerDemo : Form
    {
        private Modbus.ModbusServer m_Server;

        public ModbusServerDemo()
        {
            InitializeComponent();
            m_Server = new ModbusServer("", 502);
           modbusLog1.SetServerToLog(m_Server);
        }
        

        private void btStart_Click(object sender, EventArgs e)
        {
            m_Server.Start();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            m_Server.Stop(true);
        }

        private void ModbusServerDemo_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Server.Stop(true);
        }
    }
}
