using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WagoBK
{
    /// <summary>
    /// UI-control for setting/ getting BK-channel state
    /// </summary>
    public partial class ChannelAI : UserControl
    {
        public ChannelAI()
        {
            InitializeComponent();
        }
        public ChannelAI(AIChannelDef Channel)
        {
            m_Channel = Channel;
            InitializeComponent();
            this.ChBMK.Text = m_Channel.GetBMK();
            this.ChName.Text = m_Channel.GetName();
        }

        public void UpdateState()
        {
            this.numericUpDown1.Value =(decimal)m_Channel.Get();
        }
        protected AIChannelDef m_Channel;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            m_Channel.Set((double)numericUpDown1.Value);
        }

    }
    public class AIChannelDef : ChannelDefBase
    {
        public AIChannelDef(string Name, string BMK, CardDefAnalogIO Card, int Channel)
            : base()
        {
            m_ChType = ChannelType.AI;
            base.SetName(Name);
            base.SetBMK(BMK);
            base.SetChannel(Card, Channel);
        }
        public void Set(double Value)
        {   
            m_Value = Value;
            ((CardDefAnalogIO)m_Card).ForceAI(GetChannel(),m_Value); 
        }
        public new double Get()
        {
            m_Value = ((CardDefAnalogIO)m_Card).GetAI(GetChannel()); 
            return m_Value; 
        }
        private double m_Value;
    }



}
