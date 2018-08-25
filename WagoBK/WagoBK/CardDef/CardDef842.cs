using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    public class CardDef842 : CardDefBase
    {
        public CardDef842(int Slot)
            : base(Slot)
        {
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
            return "750-482";
        }
        public override string GetCardDescrName()
        {
            return "Koppler 10Mbits";
        }
        public override CardType GetCardType()
        {
            return CardType.Controller;
        }
    }
}
