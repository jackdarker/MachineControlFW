using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    public class CardDef468 :CardDefAnalogIO
    {
        public CardDef468(int Slot)
            : base(Slot)
        {
            m_OutPA.Initialize();
            m_InPA.Initialize();
            m_AI.Initialize();
            m_OvL.Initialize();
        }       
        public override int GetChannelCount()
        {
            return 4;
        }
        public override ChannelType GetChannelType()
        {
            return ChannelType.AI;
        }
        public override string GetCardName()
        {
            return "750-468";
        }
        public override string GetCardDescrName()
        {
            return "4xAI 0..10V";
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
            if (PA.Length >= 8)
            {
                for (int i = 0; i < m_AI.Length; i++)
                {
                    m_InPA[i * 2] = PA[i * 2];
                    m_InPA[1 + (i * 2)] = PA[1 + (i * 2)];
                    unscaled = ((int)m_InPA[i * 2] << 8) + (int)m_InPA[(i * 2) + 1];
                    m_OvL[i] = (unscaled & 0x1) > 0;
                    m_AI[i] = ((double)unscaled / 8) * 10 / 4096;   //shifted right 3 bit, 10 V = 4096dig
                }
            }
        }
        public override int GetInPALength()
        {
            //( 2*Data)*4* 8
            return 8*8;
        }
        public override double GetAI(int Ch)
        {
            double Value = 0;

            if (Ch >= 0 && Ch <= 3)
            {
                Value = m_AI[Ch];
            }
            return Value;
        }
        public override void ForceAI(int Ch, double Value)
        {
            if (Ch >= 0 && Ch <= 3)
            {
                m_AI[Ch] = Value;
            };
        }
        private double[] m_AI = new double[4];
        private bool[] m_OvL = new bool[4];
        private byte[] m_OutPA = new byte[0];
        private byte[] m_InPA = new byte[8];
    }
}
