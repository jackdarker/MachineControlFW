using System;
using System.Collections.Generic;
using System.Text;

// UNTESTED !!!!!!!! ??

namespace WagoBK
{
    public class CardDef655 : CardDefBase
    {
        public enum SpecialFunction
        {
            none = 0,
            ReadMailboxSize,
            ReadDetectedSlaves
        }

        public CardDef655(int Slot)
            : base(Slot)
        {
            m_SpecialFunc = SpecialFunction.none;
        }
        public override int GetChannelCount()
        {
            return 0;
        }
        public override ChannelType GetChannelType()
        {
            return ChannelType.DO;
        }
        public override string GetCardName()
        {
            return "750-655";
        }
        public override string GetCardDescrName()
        {
            return "ASI Master";
        }
        public override CardType GetCardType()
        {
            return CardType.Controller; //??
        }
        public override void CardPOR()
        {
            //read Mailboxsize and PA-size from parameterregister 33 

            //read List of detected slaves GET_LDS ?
            
        }
        public override byte[] CreateOutPA()
        {
            if (m_SpecialFunc == SpecialFunction.ReadMailboxSize)
            {// create read message

            }
            else if (m_SpecialFunc == SpecialFunction.ReadDetectedSlaves)
            {// create write message
            }
            else
            {
            }
            return m_OutPA;
        }
        public override int GetOutPALength()
        {
            return m_OutPA.Length * 8; //??
        }
        public override void ParseInPA(byte[] PA)
        {
            if (PA.Length >= m_OutPA.Length)
            {
            }
        }

        public override void ParseOutPA(byte[] PA)
        {
            if (PA.Length >= m_OutPA.Length)
            {
            }
        }
        public override int GetInPALength()
        {
            return m_InPA.Length * 8;   //??
        }


        private byte[] m_OutPA;
        private byte[] m_InPA;
        private int m_MailboxSize;
        private SpecialFunction m_SpecialFunc;   //Flag if read/write is pending or normal operation
    }
}
