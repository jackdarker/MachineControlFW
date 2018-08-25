using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WagoBK
{
    public partial class WagoBKPane : UserControl
    {
        public WagoBKPane()
        {
            InitializeComponent();
        }
        public WagoBKPane(WagoBKBase BK)
        {
            InitializeComponent();
            SetBK(BK);
        }

        public void SetBK(WagoBKBase BK)
        {
            m_BK = BK;
            CreateChannelCtrls();  //and if BK is null??
            //
            m_BK.ConnectToEvent(new WagoSocketBase.OnCardChangeEventHandler(OnCardChangeEvent));
        }
        protected void CreateChannelCtrls()
        {
            Channels ChList = m_BK.Ch();
            //delete existing controls
            DInputs.Controls.Clear();
            DOutputs.Controls.Clear();
            AInputs.Controls.Clear();
            AOutputs.Controls.Clear();
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
            foreach (string Name in ChList.AIChannels.Keys)
            {
                WagoBK.ChannelAI channel = new WagoBK.ChannelAI(ChList.AIChannels[Name]);
                this.AInputs.Controls.Add(channel);
            }
            foreach (string Name in ChList.AOChannels.Keys)
            {
                WagoBK.ChannelAO channel = new WagoBK.ChannelAO(ChList.AOChannels[Name]);
                this.AOutputs.Controls.Add(channel);
            }
        }
        protected void UpdateState()
        {
            m_BK.CheckInterlock();
            foreach (WagoBK.ChannelDO Ctrl in this.DOutputs.Controls)
            {
                Ctrl.UpdateState();
            }
            foreach (WagoBK.ChannelDI Ctrl in this.DInputs.Controls)
            {
                Ctrl.UpdateState();
            }
            foreach (WagoBK.ChannelAI Ctrl in this.AInputs.Controls)
            {
                Ctrl.UpdateState();
            }
            foreach (WagoBK.ChannelAO Ctrl in this.AOutputs.Controls)
            {
                Ctrl.UpdateState();
            }
            if (m_BK != null)
            {
                txtStatus.Text = m_BK.GetStateInfo().ToString();
                Color TextColor = Color.PaleGreen;
                switch (m_BK.GetStateInfo())
                {
                    case WagoBKState.Disconnected:
                    case WagoBKState.Unknown:
                        TextColor = Color.LightSalmon;
                        break;
                    default:
                        break;
                }
                txtStatus.BackColor = TextColor;
            }
        }
        public delegate void DelegateUpdateChannelList();
        void OnCardChangeEvent(object sender, EventArgs e)
        {
            if (this != null )//&& this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateUpdateChannelList(CreateChannelCtrls));
                }
                else
                {
                    CreateChannelCtrls();
                }
            }
             
        }
        protected WagoBKBase m_BK;
        protected int m_RefreshTime = 100;

        private void btRefresh_Click(object sender, EventArgs e)
        {
            UpdateState();
            //timer1.Enabled=true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateState();
        }
        private void numRefreshTime_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numRefreshTime.Value;
        }

    }
}
