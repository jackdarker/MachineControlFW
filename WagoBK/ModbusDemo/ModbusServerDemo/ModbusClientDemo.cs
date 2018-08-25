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
    public partial class ModbusClientDemo : Form
    {
        private Modbus.ModbusCore m_Client;
        private Modbus.ModbusDataStruct m_DataRec = new ModbusDataStruct();
        public ModbusClientDemo()
        {
            InitializeComponent();
            m_Client = new ModbusCore();
            modbusLog1.SetClientToLog(m_Client);
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            m_Client.RequestTO = 10000;
            m_Client.Connect("127.0.0.1", 502);       //?? IP anpassen!      
        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {
            m_Client.Disconnect();
        }

        private void btFC3_Click(object sender, EventArgs e)
        {
            m_Client.ExecFC3(1, 1, ref m_DataRec);

        }

    }
}
