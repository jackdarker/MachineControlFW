using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    public class CardDef479 : CardDefAnalogIO
    {
        public CardDef479(int Slot)
            : base(Slot)
        {
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
            return "750-479";
        }
        public override string GetCardDescrName()
        {
            return "2xAI -10..10V";
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
            Int16 unscaled = 0;
            if (PA.Length >= m_InPA.Length)
            {
                for (int i = 0; i < m_AI.Length; i++)
                {
                    m_InPA[i * 2] = PA[i * 2];
                    m_InPA[1 + (i * 2)] = PA[1 + (i * 2)];
                    unscaled = (Int16)((m_InPA[i * 2] << 8) + m_InPA[(i * 2) + 1]);
                    m_AI[i] = ((double)unscaled/4 ) * 10 / 8191;   //shifted right 2 bit, 10 V = 8191dig
                }
            }
        }
        public override int GetInPALength()
        {
            return m_InPA.Length * 8;
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
            if (Ch >= 0 && Ch <= 1)
            {
                m_AI[Ch] = Value;
            };
        }
        private double[] m_AI = new double[2];
        private byte[] m_OutPA = new byte[0];
        private byte[] m_InPA = new byte[4];
    }
}
