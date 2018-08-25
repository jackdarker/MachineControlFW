using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    class CardDef841 : CardDefBase
    {
        public CardDef841(int Slot)
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
            return "750-481";
        }
        public override string GetCardDescrName()
        {
            return "Koppler 100Mbits";
        }
        public override CardType GetCardType()
        {
            return CardType.Controller;
        }
    }
}
