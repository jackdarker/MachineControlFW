using System;
using System.Collections.Generic;
using System.Text;

namespace WagoBK
{
    public class CardManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static CardDefBase CreateCardByName(int Slot, string Name)
        {// adding Card: add card here
            CardDefBase Card=null;
            if (Name == "841") Card = new CardDef841(Slot);
            else if (Name == "530") Card = new CardDef530(Slot);
            else if (Name == "430") Card = new CardDef430(Slot);
            else if (Name == "841") Card = new CardDef841(Slot);
            else if (Name == "842") Card = new CardDef842(Slot);
            else if (Name == "461") Card = new CardDef461(Slot);
            else if (Name == "468") Card = new CardDef468(Slot);
            else if (Name == "479") Card = new CardDef479(Slot);
            else if (Name == "511") Card = new CardDef511(Slot);
            else if (Name == "550") Card = new CardDef550(Slot);
            if (Card == null)
            {
                throw new Exception("unknown card: " + Name);
            }
            return Card;
        }
        /// <summary>
        /// converts the configuration register value of koppler into card definition
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static CardDefBase CreateCardByCode(int Slot, UInt16 Code)
        {// adding Card: add card here
            CardDefBase Card = null;
            #region digital cards detection  //special handling of digital cards goes here
            if ((Code & 0x8000) > 0)
            {
                int ChannelCount = ((int)Code >> 8) & 0x7F;
                if ((Code & 0x0003) == 3)
                {  // in & out
                    //??
                }
                else if ((Code & 0x0001) >0)
                { // in 
                    switch (ChannelCount)
                    { 
                        case 8:
                            Card = new CardDef430(Slot);
                            break;
                        default:
                            break;
                    }
                }
                else if ((Code & 0x0002) >0)
                { // out
                    switch (ChannelCount)
                    {
                        case 8:
                            Card = new CardDef530(Slot);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion
            else if (Code == 841) Card = new CardDef841(Slot);
            else if (Code == 842) Card = new CardDef842(Slot);
            else if (Code == 430) Card = new CardDef430(Slot);
            else if (Code == 461) Card = new CardDef461(Slot);
            else if (Code == 468) Card = new CardDef468(Slot);
            else if (Code == 479) Card = new CardDef479(Slot);
            else if (Code == 511) Card = new CardDef511(Slot);
            else if (Code == 530) Card = new CardDef530(Slot);
            else if (Code == 550) Card = new CardDef550(Slot);
            if (Card == null)
            {
                throw new Exception("unknown card: "+Code.ToString());
            }
            return Card;
        }
        /// <summary>
        /// returns all Card Types supported by the library
        /// </summary>
        /// <returns></returns>
        public static UInt16[] GetAvailableCardDefs()
        {// adding Card: add card here
            UInt16[] Cards = { 430, 461, 468, 479, 511, 530, 550,841, 842};
            return Cards;
        }
        public CardManager() { }
        public void AddCard(int Slot, CardDefBase Card)
        {
            m_Cards.Add(Slot, Card); //exception if already defined
            SortInOutchannels();
        }
        public int GetCardSlot(CardDefBase Card)
        {
            int i = -1;
            int[] Slots = GetSlotNumbers();
            for (int k=0;k<Slots.Length; k++)
            {
                if (GetCard(k).Equals(Card))
                {
                    i = k;
                };
            }
            return i;
        }
        public void Clear()
        {
            m_Cards.Clear();
        }
        public int[] GetSlotNumbers()
        {
            int[] Slots = new int[m_Cards.Count];
            m_Cards.Keys.CopyTo(Slots,0);
            return Slots;
        }
        protected void SortInOutchannels()
        {
            int[] Slot = GetSlotNumbers();
            int[] AOSlot = new int[Slot.Length];
            int AOSlotLength = 0;
            int[] DOSlot = new int[Slot.Length];
            int DOSlotLength = 0;
            int[] AISlot = new int[Slot.Length];
            int AISlotLength = 0;
            int[] DISlot = new int[Slot.Length];
            int DISlotLength = 0;
            CardDefBase Card;
            int BitCount;

            for (int i = 0; i < Slot.Length; i++)
            {//create 4 lists, one for AO one for DO
                Card = GetCard(Slot[i]);
                switch (Card.GetChannelType())
                {
                    case ChannelType.DO:
                        DOSlot[DOSlotLength] = i;
                        DOBitCount += Card.GetOutPALength();    
                        DOSlotLength++;
                        BitCount = Card.GetInPALength();//may also have inputs
                        if (BitCount > 0)
                        {
                            DISlot[DISlotLength] = i;
                            DIBitCount += BitCount;
                            DISlotLength++;
                        }
                        break;
                    case ChannelType.DI:
                        DISlot[DISlotLength] = i;
                        DIBitCount += Card.GetInPALength();
                        DISlotLength++;
                        break;
                    case ChannelType.AO:
                        AOSlot[AOSlotLength] = i;
                        AOBitCount += Card.GetOutPALength();
                        AOSlotLength++;
                        BitCount = Card.GetInPALength();//may also have inputs
                        if (BitCount > 0)
                        {
                            AISlot[AISlotLength] = i;
                            AIBitCount += BitCount;
                            AISlotLength++;
                        }
                        break;
                    case ChannelType.AI:
                        AISlot[AISlotLength] = i;
                        AIBitCount += Card.GetInPALength();
                        AISlotLength++;
                        break;
                    default:
                        break;
                }
            }
            AOCardSlots = new int[AOSlotLength];
            for (int i = 0; i < AOSlotLength; i++)
            {
                AOCardSlots[i] = AOSlot[i];
            }
            DOCardSlots = new int[DOSlotLength];
            for (int i = 0; i < DOSlotLength; i++)
            {
                DOCardSlots[i] = DOSlot[i];
            }
            AICardSlots = new int[AISlotLength];
            for (int i = 0; i < AISlotLength; i++)
            {
                AICardSlots[i] = AISlot[i];
            }
            DICardSlots = new int[DISlotLength];
            for (int i = 0; i < DISlotLength; i++)
            {
                DICardSlots[i] = DISlot[i];
            }
        }
        /// <summary>
        /// returns count of register to read in 
        /// Physical input area 1
        /// </summary>
        /// <returns></returns>
        public int GetPhyIn1WordCount()
        {
            int TotalPALength = AIBitCount / 16 + (DIBitCount / 16);
            TotalPALength += (DIBitCount % 16 > 0) ? 1 : 0;
            if (TotalPALength > 256)  //max 256 registers
            {
                TotalPALength = 256;
            };
            return TotalPALength;
        }
        public void ParsePhyIn1(byte[] PA)
        {
            DIStateChanged = false;
            CardDefBase Card;
            byte[] CardPA = new byte[256];  //??
            int CardPALength;
            int PAByteIndex = 0;
            int[] Slot = GetSlotNumbers();
            if ((PA.Length) != GetPhyIn1WordCount() * 2)
            {
                //??error on length mismatch
                return;
            };
            for (int i = 0; i < AICardSlots.Length; i++)
            {//
                Card = GetCard(Slot[AICardSlots[i]]);
                CardPALength = Card.GetInPALength()/8;
                for (int k = 0; k < CardPALength; k++)
                {//reformat byte[]
                    CardPA[k] = PA[PAByteIndex];
                    PAByteIndex++;
                }
                Card.ParseInPA(CardPA);
            }
            int SingleByte;
            int PABitIndex = PAByteIndex*8;
            int CardBitLength = 0;
            for (int i = 0; i < DICardSlots.Length; i++)
            {// multiple cards may be stuffed in one register
                CardPA  = new byte[256];
                
                Card = GetCard(Slot[DICardSlots[i]]);
                CardBitLength = Card.GetInPALength();
                for (int k = 0; k < CardBitLength; k++)
                {
                    PAByteIndex = PABitIndex / 8;
                    if (PAByteIndex%2 > 0)
                    {//Swap Low-High-Byte
                        PAByteIndex -=1;
                    }
                    else 
                    {
                        PAByteIndex += 1;
                    }
                    SingleByte = PA[PAByteIndex];
                    SingleByte = (SingleByte >> (PABitIndex % 8)) & 0x1;
                    SingleByte = (SingleByte << (k % 8));
                    CardPA[k/8] = (byte)(SingleByte | CardPA[k/8]);
                    PABitIndex++;
                }
                Card.ParseInPA(CardPA);
                if (Card.InputStateModified()) DIStateChanged = true;
            }
        }
        /// <summary>
        /// parses values read from output area and pushs the values into the channels
        /// </summary>
        /// <param name="PA"></param>
        public void ParsePhyOut1(byte[] PA)
        {
            CardDefBase Card;
            byte[] CardPA = new byte[256];  //??
            int CardPALength;
            int PAByteIndex = 0;
            int[] Slot = GetSlotNumbers();
            if ((PA.Length) != GetPhyOut1WordCount() * 2)
            {
                //??error on length mismatch
                return;
            };
            for (int i = 0; i < AOCardSlots.Length; i++)
            {//
                Card = GetCard(Slot[AOCardSlots[i]]);
                CardPALength = Card.GetOutPALength() / 8;
                for (int k = 0; k < CardPALength; k++)
                {//reformat byte[]
                    CardPA[k] = PA[PAByteIndex];
                    PAByteIndex++;
                }
                Card.ParseOutPA(CardPA);
            }
            int SingleByte;
            int PABitIndex = PAByteIndex * 8;
            int CardBitLength = 0;
            for (int i = 0; i < DOCardSlots.Length; i++)
            {// multiple cards may be stuffed in one register
                CardPA = new byte[256];
                Card = GetCard(Slot[DOCardSlots[i]]);
                CardBitLength = Card.GetOutPALength();
                for (int k = 0; k < CardBitLength; k++)
                {
                    PAByteIndex = PABitIndex / 8;
                    if (PAByteIndex % 2 > 0)
                    {//Swap Low-High-Byte
                        PAByteIndex -= 1;
                    }
                    else
                    {
                        PAByteIndex += 1;
                    }
                    SingleByte = PA[PAByteIndex];
                    SingleByte = (SingleByte >> (PABitIndex % 8)) & 0x1;
                    SingleByte = (SingleByte << (k % 8));
                    CardPA[k / 8] = (byte)(SingleByte | CardPA[k / 8]);
                    PABitIndex++;
                }
                Card.ParseOutPA(CardPA);
            }
        }
        public bool IsDIStateChanged()
        {
            return DIStateChanged;
        }
        public int GetPhyOut1WordCount()
        {
            int TotalPALength = AOBitCount / 16 + (DOBitCount / 16) ;
            TotalPALength += (DOBitCount % 16 > 0) ? 1 : 0;
            if (TotalPALength > 256)  //max 256 registers
            {
                TotalPALength = 256;
            };
            return TotalPALength;
        }
        public byte[] GetPhyOut1()
        {
            int[] Slot = GetSlotNumbers();
            CardDefBase Card;            
            byte [] PA = new byte[GetPhyOut1WordCount()*2];
            byte[] CardPA;
            byte[] TempCardPA;
            int PAByteIndex = 0;
            for (int i = 0; i < AOCardSlots.Length; i++)
            {//first copy to PA
                Card = GetCard(Slot[AOCardSlots[i]]);
                CardPA = Card.CreateOutPA();
                CardPA.CopyTo(PA, PAByteIndex);
                PAByteIndex += CardPA.Length;
            }
            int SingleByte;
            int PABitIndex = PAByteIndex*8;
            int CardBitLength = 0;
            for (int i = 0; i < DOCardSlots.Length; i++)
            {//append bits to PA, multiple cards may be stuffed in one register
                Card = GetCard(Slot[DOCardSlots[i]]);
                CardBitLength = Card.GetOutPALength();
                TempCardPA = Card.CreateOutPA();
                for (int k = 0; k < CardBitLength; k++)
                {
                    SingleByte = TempCardPA[k / 8];
                    SingleByte = (SingleByte >> (k % 8)) & 0x1;
                    SingleByte = SingleByte << (PABitIndex % 8);
                    PAByteIndex = PABitIndex / 8;
                    if (PAByteIndex / 2 > 0)
                    {//Swap Low-High-Byte
                        PAByteIndex = -1;
                    }
                    else
                    {
                        PAByteIndex = +1;
                    }
                    PA[PAByteIndex] = (byte)(PA[PAByteIndex] | SingleByte);
                    PABitIndex++;
                }
            }
            return PA;
        }
        public CardDefBase GetCard(int Slot)
        {
            return m_Cards[Slot];
        }
        private Dictionary<int, CardDefBase> m_Cards = new Dictionary<int, CardDefBase>();
        private int[] AICardSlots;
        private int AIBitCount;
        private int[] DICardSlots;
        private int DIBitCount;
        private bool DIStateChanged;
        private int[] AOCardSlots;
        private int AOBitCount;
        private int[] DOCardSlots;
        private int DOBitCount;
    }
}
