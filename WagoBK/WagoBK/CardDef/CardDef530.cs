using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    public class CardDef530 : CardDefDigitalIO
    {
        public CardDef530(int Slot)
            : base(Slot)
        {
        }
        public override int GetChannelCount()
        {
            return 8;
        }
        public override ChannelType GetChannelType()
        {
            return ChannelType.DO;
        }
        public override string GetCardName()
        {
            return "750-530";
        }
        public override string GetCardDescrName()
        {
            return "8xDO 24V";
        }
        public override CardType GetCardType()
        {
            return CardType.SimpleIO;
        }
        public override bool GetDO(int Ch)
        {
            bool Value = false;
            if (Ch >= 0 && Ch <= 7)
            {
                Value = ((m_OutPA[0] >> Ch) & 0x1) > 0;
            }
            return Value;
        }
        public override void SetDO(int Ch, bool Value)
        {
            if(Ch>=0 && Ch<=7)
            {
                int x =(1<<Ch);
                int nx = (0xFF - x);
                m_OutPA[0] = (byte)(m_OutPA[0] & (nx));
                if (Value) m_OutPA[0] = (byte)(m_OutPA[0] | x);
            }
        }
        public override byte[] CreateOutPA()
        {
            return m_OutPA;
        }
        public override int GetOutPALength()
        {
            //(1x0x00 + 1xStatus + 2 Data)*2
            return 8*8;
        }
        public override void ParseInPA(byte[] PA)
        {        }
        public override void ParseOutPA(byte[] PA)
        {
            if(PA.Length==1) m_OutPA = PA;
        }
        byte[] m_OutPA = new byte[] {0};
    }
}
//}
