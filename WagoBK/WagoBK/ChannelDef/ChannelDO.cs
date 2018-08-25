using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WagoBK
{
    public partial class ChannelDO : UserControl
    {
        public ChannelDO()
        {
            InitializeComponent();
        }
        public ChannelDO(DOChannelDef Channel)
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
            this.button1.Enabled = !m_Channel.IsUIDisabled();
        }
        protected DOChannelDef m_Channel; //reference to WagoBK.Channels

        private void button1_Click(object sender, EventArgs e)
        {
            m_Channel.Set(!m_Channel.Get());
        }
    }
    public class DOChannelDef : ChannelDefBase
    {
        public DOChannelDef(string Name,string BMK, CardDefDigitalIO Card, int Channel) : base()
        {
            m_ChType = ChannelType.DO;
            base.SetName(Name);
            base.SetBMK(BMK);
            base.SetChannel(Card, Channel);
        }
        public void Set(bool Value)
        {
            m_Value = Value;
            ((CardDefDigitalIO)m_Card).SetDO(GetChannel(),m_Value); 
        }
        public new bool Get()
        {
            m_Value = ((CardDefDigitalIO)m_Card).GetDO(GetChannel()); 
            return m_Value;
        }
        private bool m_Value;
    }

  
}
