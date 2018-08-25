using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WagoBK
{
    public partial class ChannelDI : UserControl
    {
        public ChannelDI() 
        {
            InitializeComponent();
        }
        public ChannelDI(DIChannelDef Channel)
        {
            m_Channel = Channel;
            InitializeComponent();
            this.ChBMK.Text = m_Channel.GetBMK();
            this.ChName.Text = m_Channel.GetName();
        }

        public void UpdateState()
        {
            if (m_Channel.Get())
            {
                this.button1.BackColor = Color.LightGreen;
            }
            else
            {
                this.button1.BackColor = Color.DarkGreen;
            }
        }

        protected DIChannelDef m_Channel;

        private void button1_Click(object sender, EventArgs e)
        {
            m_Channel.Set(!m_Channel.Get());  //?? for simulation only!
        }
    }
    public class DIChannelDef : ChannelDefBase
    {
        public DIChannelDef(string Name, string BMK, CardDefDigitalIO Card, int Channel)
        {
            m_ChType = ChannelType.DI;
            base.SetName(Name);
            base.SetBMK(BMK);
            base.SetChannel(Card, Channel);
        }
        public void Set(bool Value)
        {
            m_Value = Value;
            ((CardDefDigitalIO)m_Card).ForceDI(GetChannel(), m_Value);
        }
        public new bool Get()
        {
            m_Value = ((CardDefDigitalIO)m_Card).GetDI(GetChannel());
            return m_Value;
        }
        private bool m_Value;
    }



}
