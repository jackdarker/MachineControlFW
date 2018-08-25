using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    public class CardDef461 : CardDefAnalogIO
    {
        public CardDef461(int Slot)
            : base(Slot)
        {
            SetSubType(0);
            m_OutPA.Initialize();
            m_InPA.Initialize();
            m_AI.Initialize();
        }
        public CardDef461(int Slot,int Subtype)
            : base(Slot)
        {
            SetSubType(Subtype);
            m_OutPA.Initialize();
            m_InPA.Initialize();
            m_AI.Initialize();
        }
        public override int GetChannelCount()
        {
            return 2;
        }
        public override ChannelType GetChannelType()
        {
            return ChannelType.AI;
        }
        public override string GetCardName()
        {
            return "750-461";
        }
        public override string GetCardDescrName()
        {
            string Descr = "2xAI Pt100";
            switch (m_Subtype)
            {
                case 2:
                    Descr = "2xAI 10..1200Ohm";
                    break;
                case 3:
                    Descr = "2xAI Pt1000";
                    break;
                case 4:
                    Descr = "2xAI Ni100";
                    break;
                case 5:
                    Descr = "2xAI Ni1000 TK6180";
                    break;
                case 7:
                    Descr = "2xAI 10..5000Ohm";
                    break;
                case 9:
                    Descr = "2xAI Ni1000 TK5000";
                    break;
                default:
                    break;
            }
            return Descr;
        }
        public override CardType GetCardType()
        {
            return CardType.SpecialIO;
        }
        public override byte[] CreateOutPA()
        {
            return m_OutPA;
        }
        public override int GetOutPALength()
        {
            return 0;
        }
        public override void ParseInPA(byte[] PA)
        {
            int unscaled = 0;
            if (PA.Length >= m_InPA.Length)
            {
                for (int i = 0; i < m_AI.Length; i++)
                {
                    m_InPA[i * 2] = PA[i * 2];
                    m_InPA[1 + (i * 2)] = PA[1 + (i * 2)];
                    unscaled = ((int)m_InPA[i * 2] << 8) + (int)m_InPA[(i * 2) + 1];
                    m_AI[i] = ((double)unscaled) * m_UnitPerDigit;   
                }
            }
        }
        public override int GetInPALength()
        {
            //( 2Data)*2ch* 8bit
            return 4 * 8;
        }
        public override double GetAI(int Ch)
        {
            double Value = 0;

            if (Ch >= 0 && Ch <= 1)
            {
                Value = m_AI[Ch];
            }
            return Value;
        }
        public override void ForceAI(int Ch, double Value)
        {
            if (Ch >= 0 && Ch <=1 )
            {
                m_AI[Ch] = Value;
            };
        }
        public override int GetSubType()
        {
            return m_Subtype;
        }
        public override void SetSubType(int SubType)
        {
            m_Subtype = SubType;
            switch (SubType)
            {
                case 7:
                    m_UnitPerDigit = 1 / 2; //
                    break;
                default:
                    m_UnitPerDigit = (1 / 10); // 800°C  = 8000dig
                    break;
            }
        }
        private double[] m_AI = new double[2];
        private byte[] m_OutPA = new byte[0];
        private byte[] m_InPA = new byte[4];
        private double m_UnitPerDigit = 0;
        private int m_Subtype = 0;  //
    }

}
