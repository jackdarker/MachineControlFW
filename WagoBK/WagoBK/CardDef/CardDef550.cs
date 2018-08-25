using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    /// <summary>
    /// BK Carddefinition
    /// "2xAO 0..10V"
    /// </summary>
    public class CardDef550 : CardDefAnalogIO
    {
        public CardDef550(int Slot)
            : base(Slot)
        {
            m_OutPA.Initialize();
            m_AO.Initialize();
        }
        public override int GetChannelCount()
        {
            return 2;
        }
        public override ChannelType GetChannelType()
        {
            return ChannelType.AO;
        }
        public override string GetCardName()
        {
            return "750-550";
        }
        public override string GetCardDescrName()
        {
            return "2xAO 0..10V";
        }
        public override CardType GetCardType()
        {
            return CardType.SpecialIO;
        }
        public override byte[] CreateOutPA()
        {
            int unscaled = 0;
            for (int i = 0; i < m_AO.Length; i++)
            {
                unscaled = (int)(m_AO[i] / m_UnitPerDigit); 
                m_OutPA[i * 2] = (byte)((unscaled >> 8) & 0xFF);
                m_OutPA[(i * 2) + 1] = (byte)((unscaled) & 0xFF);
            }
            return m_OutPA;
        }
        public override void ParseOutPA(byte[] PA)
        {
            int unscaled = 0;
            if (PA.Length >= m_OutPA.Length)
            {
                for (int i = 0; i < m_AO.Length; i++)
                {
                    m_OutPA[i * 2] = PA[i * 2];
                    m_OutPA[1 + (i * 2)] = PA[1 + (i * 2)];
                    unscaled = ((int)m_OutPA[i * 2] << 8) + (int)m_OutPA[(i * 2) + 1];
                    m_AO[i] = ((double)unscaled) * m_UnitPerDigit;
                }
            }
        }
        public override int GetOutPALength()
        {
            return m_OutPA.Length * 8;
        }
        public override void ParseInPA(byte[] PA)
        {        }
        public override int GetInPALength()
        {
            return 0;
        }
        public override double GetAO(int Ch)
        {
            double Value = 0;

            if (Ch >= 0 && Ch <= 2)
            {
                Value = m_AO[Ch];
            }
            return Value;
        }
        public override void SetAO(int Ch, double Value)
        {
            if (Ch >= 0 && Ch <= 2)
            {
                m_AO[Ch] = Value;
            }
        }
        private double m_UnitPerDigit = (double)(1 * 10) / (double)32765; //10V = 32764 dig
        private double[] m_AO = new double[2];
        private byte[] m_OutPA = new byte[4];
    }
}