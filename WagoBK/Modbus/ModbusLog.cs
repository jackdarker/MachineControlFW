using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Modbus
{
    public partial class ModbusLog : UserControl
    {
        public ModbusLog()
        {
            InitializeComponent();
        }
        public void SetServerToLog(ModbusServer ServerToLog)
        {
            ServerToLog.ConnectToEvent(new ModbusServer.OnRequestReceivedHandler(OnReceiveServer));
        }
        public void SetClientToLog(ModbusCore ClientToLog)
        {
            ClientToLog.ConnectToEvent(new ModbusCore.OnRequestReceivedHandler(OnReceiveClient));
            ClientToLog.ConnectToEvent(new ModbusCore.OnRequestSendHandler(OnSendClient));
        }
        public void AppendText(string Text)
        {
            txtLog.Text = (Text) + txtLog.Text;
        }
        public delegate void DelegateUpdateLog(string LogText);
        public static string ByteToHex(byte[] Bytes)
        {
            string Text ="" ;
            foreach(byte Char in Bytes)
            {
                Text += string.Format("{0:X}", Char);
                Text += " " ;
            }
            return Text;
        }
        void OnReceiveServer(object sender, Modbus.ModbusServer.RequestReceivedData e)
        {
            string Text = "-> [" + e.Host + "]  " + ByteToHex(new byte[] {(byte)e.FC}) + "\r\n";

            if (this != null )//&& this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateUpdateLog(AppendText), new object[] { Text });
                }
                else
                {
                    AppendText(Text);
                }
            }
        }
        void OnReceiveClient(object sender, Modbus.ModbusCore.RequestReceivedData e)
        {
            string Text = "-> [" + e.Host + "]  " + ByteToHex(e.Data.GetAsBytes()) + "\r\n";
            if (this != null)//&& this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateUpdateLog(AppendText), new object[] { Text });
                }
                else
                {
                    AppendText(Text);
                }
            }
        }
        void OnSendClient(object sender, Modbus.ModbusCore.RequestReceivedData e)
        {
            string Text = "<- [" + e.Host + "]  " + ByteToHex(e.Data.GetAsBytes()) + "\r\n";
            if (this != null)//&& this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateUpdateLog(AppendText), new object[] { Text });
                }
                else
                {
                    AppendText(Text);
                }
            }
        }    
    }
 }

