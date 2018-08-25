using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    /// <summary>
    /// BK Carddefinition 
    /// "8xDI 24V"
    /// </summary>
    public class CardDef430 : CardDefDigitalIO
    {
        public CardDef430(int Slot)
            : base(Slot)
        {
            DI = 0;
            DIStateModified = false;
        }
        public override bool GetDI(int Ch)
        {
            bool Value = false;

            if (Ch >= 0 && Ch <= 7)
            {
                Value = ((DI>>Ch) & 0x1)>0;
            }
            return Value;
        }
        public override void ForceDI(int Ch, bool Value)
        {
            if (Ch >= 0 && Ch <= 7)
            {
                if (Value)
                {
                    DI = (byte)(DI | (0x1 << Ch));
                }
                else 
                {
                    DI = (byte)(DI & 0xFF^(0x1 << Ch));
                } 
            }
        }
        public override bool InputStateModified()
        {
            return DIStateModified;   
        }
        public override int GetChannelCount()
        {
            return 8;
        }
        public override ChannelType GetChannelType()
        {
            return ChannelType.DI;
        }
        public override string GetCardName()
        {
            return "750 430";
        }
        public override string GetCardDescrName()
        {
            return "8xDI 24V";
        }
        public override CardType GetCardType()
        {
            return CardType.SimpleIO;
        }
        public override void ParseInPA(byte[] PA)
        {
            DIStateModified = false;
            if (PA.Length > 0)
            {
                DIStateModified = (PA[0] != DI);
                DI = PA[0];
            };
        }
        public override int GetInPALength()
        {
            return 8;
        }
        private byte DI;
        private bool DIStateModified;
    }
}

