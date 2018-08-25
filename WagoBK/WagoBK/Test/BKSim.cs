using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using WagoBK;

namespace WagoBK.Test
{
    public partial class BKSim : Form
    {
        public static int CountInstances;
        public BKSim(WagoBK.Channels ChannelDef)
        {
            InitializeComponent();
            txtIP.Text = "192.168.2.24";
            txtPort.Text = "51557";
            btListen.Checked = true;
            CountInstances++;
            btConnect_Click(null,new EventArgs());
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            //m_Channels = new Demo.DemoBK.DemoChannels();
            m_Channels = ChannelDef;
            CreateChannelCtrls(m_Channels);
            //timer1.Stop(); //fucking timer doesnt work if enabled at start??

        }
        protected void CreateChannelCtrls(WagoBK.Channels ChList)
        {
            //delete existing controls
            this.DInputs.Controls.Clear();
            this.DOutputs.Controls.Clear();

            //create new controls
            foreach (string Name in ChList.DIChannels.Keys)
            {
                WagoBK.ChannelDI channel = new WagoBK.ChannelDI(ChList.DIChannels[Name]);
                this.DInputs.Controls.Add(channel);
            }
            foreach (string Name in ChList.DOChannels.Keys)
            {
                WagoBK.ChannelDO channel = new WagoBK.ChannelDO(ChList.DOChannels[Name]);
                this.DOutputs.Controls.Add(channel);
            }
        }
        private void btDisconnect_Click(object sender, EventArgs e)
        {
            timer1.Stop() ;
            
            if (btListen.Checked)
            {
                m_Listener.Stop();
            }
            else
            {
            }
            if(m_Client!=null) m_Client.Close();
        }
        public delegate void DelegateUpdateState();
        public void UpdateState()
        {
            try
            {
                NetworkStream stream = m_Client.GetStream();
                string data;
                int DataLength;
                Byte[] bytes = new Byte[256];
                while (stream.DataAvailable)
                {
                    // Translate data bytes to a ASCII string.
                    DataLength = stream.Read(RcvBytes, 0, RcvBytes.Length);
                    data = System.Text.Encoding.ASCII.GetString(RcvBytes, 0, DataLength);
                    txtReceive.Text = data;
                }

               int i = 0;
               foreach (DOChannelDef Channel in this.m_Channels.DOChannels.Values)
               {
                   Channel.Set(RcvBytes[i] > 0x30);
                   i++;
               }
                foreach (WagoBK.ChannelDO Ctrl in this.DOutputs.Controls)
                {
                    Ctrl.UpdateState();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        private void btSend_Click(object sender, EventArgs e)
        {     
            try
            {
                foreach (WagoBK.ChannelDI Ctrl in this.DInputs.Controls)
                {
                    Ctrl.UpdateState();
                }
                int i = 0;
                foreach (DIChannelDef Channel in m_Channels.DIChannels.Values)
                {
                    
                    SndBytes[i] = (byte)(Channel.Get() ? 0x31 : 0x30);
                    i++;
                }
                NetworkStream stream = m_Client.GetStream();
                stream.Write(SndBytes, 0, SndBytes.Length);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        private void btReceive_Click(object sender, EventArgs e)
        {
            UpdateState();
        }
        private void btConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btListen.Checked)
                {
                    m_Listener = new TcpListener(IPAddress.Parse(txtIP.Text), Convert.ToInt32(txtPort.Text));
                    m_Listener.Start();
                    m_Listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback),
                        m_Listener);
                }
                else
                {
                    m_Client = new TcpClient("localhost", Convert.ToInt32(txtPort.Text));
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        public void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            TcpListener listener = (TcpListener)ar.AsyncState;

            // End the operation and display the received data on 
            // the console.
            m_Client = listener.EndAcceptTcpClient(ar);

            // Process the connection here. (Add the client to a
            // server table, read data, etc.)
            Console.WriteLine("Client connected completed");
            timer1.Start();
        }
        private byte[] RcvBytes = new byte[256];
        private byte[] SndBytes = new byte[256];
        //Demo.DemoBK.DemoChannels m_Channels;
        WagoBK.Channels m_Channels;
        private TcpClient m_Client;
        private TcpListener m_Listener;
        private System.Timers.Timer timer1 = new System.Timers.Timer(50);  
        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateUpdateState(UpdateState));
                }
                else
                {
                    UpdateState();
                }
            }
            catch (ObjectDisposedException exception)
            {
                ;//catch exception if form is closed an timer fires event
            }
        }
        private void BKSim_FormClosing(object sender, FormClosingEventArgs e)
        {
            btDisconnect_Click(sender, e);
            while (timer1.Enabled) { };
            
        }

    }
}
