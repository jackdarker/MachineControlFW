using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WagoBK
{
    public partial class ChannelAO : UserControl
    {
        public ChannelAO() 
        {
            InitializeComponent();
        }
        public ChannelAO(AOChannelDef Channel)
        {
            m_Channel = Channel;
            InitializeComponent();
            this.ChBMK.Text = m_Channel.GetBMK();
            this.ChName.Text = m_Channel.GetName();
            updateContextMenu();
        }

        public void UpdateState()
        {
           this.numericUpDown1.Value =(decimal)m_Channel.Get();
           this.numericUpDown1.Enabled = !m_Channel.IsUIDisabled();
        }
        protected AOChannelDef m_Channel;

        private void updateContextMenu()
        {
            if (m_Channel.GetCard().GetPropertyDialog() != null)
            {
                ctxMenu.Enabled = mniProperties.Enabled = true;
            }
            else
            {
                mniProperties.Enabled = false;
                ctxMenu.Enabled = false;
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            m_Channel.Set((double)numericUpDown1.Value);
        }

        private void mniProperties_Click(object sender, EventArgs e)
        {
            m_Channel.GetCard().GetPropertyDialog().Show();
        }
    }
    public class AOChannelDef : ChannelDefBase
    {
        //Scaling,Byteshift,sign Unit??
        public AOChannelDef(string Name, string BMK, CardDefAnalogIO Card, int Channel) : base()
        {
            m_ChType = ChannelType.AI;
            base.SetName(Name);
            base.SetBMK(BMK);
            base.SetChannel(Card, Channel);
        }
        public void Set(double Value)
        {
            m_Value = Value;
            ((CardDefAnalogIO)m_Card).SetAO(GetChannel(), m_Value);
        }
        public new double Get()
        {
            m_Value = ((CardDefAnalogIO)m_Card).GetAO(GetChannel());
            return m_Value;
        }
        private double m_Value;
    }



}
