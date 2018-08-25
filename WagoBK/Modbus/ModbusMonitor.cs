using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Modbus
{
    public partial class ModbusUI : Form
    {
        public ModbusUI()
        {
            InitializeComponent();
            MyModbus = new ModbusCore();
            MyModbus.EventMBusException += new Modbus.ModbusCore.MBusException(MyModbus_EventMBusException);
        }

        void MyModbus_EventMBusException(object sender, ModBusException e)
        {
            textBox1.Text = e.ModBusExceptionCode.ToString() + "\r\n" + textBox1.Text;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            MyModbus.Disconnect();
            MyModbus.Connect(this.txtIP.Text, Convert.ToInt32(txtPort.Text));
        }
        private Modbus.ModbusCore MyModbus;

        private void btSend_Click(object sender, EventArgs e)
        {
            string Text = cbSendData.Text.Substring(2) ;
            string ReceivedText= "";
            byte[] Received = new byte[0];
            byte[] Data = new byte[(Text.Length) / 2];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = Convert.ToByte(Text.Substring(i * 2, 2),16);
            }
            bool OK = false;//?? MyModbus.ExecFC(Convert.ToByte(cbSendData.Text.Substring(0, 2)), Data, ref Received);
            if (OK)
            {
                byte b0, b1;
                byte vier = 4;
                for (int i = 0; i < Received.Length; i++)
                {
                    b0 = (byte)(Received[i] >> vier);
                    b1 = (byte)(Received[i] & 0x0F);
                    ReceivedText = ReceivedText + string.Format(" {0:X}{1:X}", b0, b1);
                }
            }
            textBox1.Text = ReceivedText + "\r\n" + textBox1.Text;
        }

    }
}
