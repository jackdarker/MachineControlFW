using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    public enum ChannelType
    {
        DI = 1,
        DO = 2,
        AI = 3,
        AO = 4,
        None =5 //has no channels
    }
    public abstract class ChannelDefBase
    {
        public ChannelDefBase()
        {
            m_Card = null;
        }
        public virtual void Set() { }
        public virtual void Get() { }      
        public string GetName() 
            {
                return m_Name; 
            }
        public void SetName(string Name)
            {
                m_Name=Name;
            }
        public string GetBMK()
            {
                return m_BMK;
            }
        public void SetBMK(string BMK)
            {
                m_BMK = BMK;
            }
        public void SetChannel(CardDefBase Card, int Channel)
        {
                m_Card = Card;
                m_Channel = Channel;
        }
        public int GetChannel()
        {
                return m_Channel;
        }
        public void DisableUI(bool Flag)
        {
            m_DisableUI = Flag;
        }
        public bool IsUIDisabled()
        {
            return m_DisableUI;
        }
        public CardDefBase GetCard()
        {
            return m_Card;
        }
        protected ChannelType m_ChType;
        protected string m_Name;
        protected string m_BMK;
        protected int m_Channel;
        protected CardDefBase m_Card;

        protected bool m_DisableUI= false;
    }
}
