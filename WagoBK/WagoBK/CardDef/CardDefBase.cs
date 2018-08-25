using System;
using System.Collections.Generic;
using System.Windows.Forms; //for property dialog??
using System.Text;

namespace WagoBK
{

    public abstract class CardDefBase
    {
        public enum CardType
        {
            SimpleIO = 1,
            Controller = 2,
            SpecialIO = 3
        }
        private CardDefBase()
        { }
        public CardDefBase(int Slot)
        {
            m_Slot = Slot;
        }
        public virtual int GetChannelCount() 
        { 
            return 0;
        }
        public virtual ChannelType GetChannelType() 
        {
            return ChannelType.DI;
        }
        /// <summary>
        /// convert data to process output
        /// </summary>
        /// <returns></returns>
        public virtual byte[] CreateOutPA()
        {
            return new byte[0];
        }
        /// <summary>
        /// returns the length of data to output in bits
        /// this may depend if card is in PA mode or control mode
        /// </summary>
        /// <returns></returns>
        public virtual int GetOutPALength()
        {
            return 0;
        }
        /// <summary>
        /// signals that this may be the first cycle
        /// after power on and configuration should be done in 
        /// following cycles (f.e. setting PWM output frequency )
        /// </summary>
        public virtual void CardPOR()
        { }
        /// <summary>
        /// converts readback data 
        /// </summary>
        /// <param name="PA"></param>
        public virtual void ParseInPA(byte[] PA)
        {        }
        /// <summary>
        /// is queried after ParseInPA to check if values have changed
        /// </summary>
        /// <returns></returns>
        public virtual bool InputStateModified()
        {
            return false;
        }
        /// <summary>
        /// returns the length of input data in bits
        /// this may depend if card is in PA mode or control mode
        /// </summary>
        /// <returns></returns>
        public virtual int GetInPALength()
        {
            return 0;
        }
        /// <summary>
        /// the currently set output value can be read back from koppler
        /// this may be handy if you restart app and want to get current 
        /// koppler state
        /// Will only be called for DO, AO
        /// </summary>
        /// <param name="PA"></param>
        public virtual void ParseOutPA(byte[] PA)
        {        }
        /// <summary>
        /// should return "750-481/0000"
        /// </summary>
        /// <returns></returns>
        public virtual string GetCardName()
        {
            return "??";
        }
        /// <summary>
        /// should return "4xAI 0..10V"
        /// </summary>
        /// <returns></returns>
        public virtual string GetCardDescrName()
        {
            return "???";
        }
        public virtual CardType GetCardType()
        {
            return CardType.SimpleIO;
        }
        /// <summary>
        /// set Artikelnummer extension
        /// </summary>
        /// <param name="SubType"></param>
        public virtual void SetSubType(int SubType)
        {        }
        /// <summary>
        /// return Artikelnummer extension
        /// </summary>
        /// <returns></returns>
        public virtual int GetSubType()
        {
            return 0;
        }
        /// <summary>
        /// creates a form for setting extended properties
        /// </summary>
        /// <returns></returns>
        public virtual Form GetPropertyDialog()
        {
            return null;
        }

        public int GetSlot()
        {
            return m_Slot;
        }
        private int m_Slot=0;
    }

    public abstract class CardDefDigitalIO : CardDefBase
    {
        public CardDefDigitalIO(int Slot)
            : base(Slot)        { }
        /// <summary>
        /// returns input value of a single channel of the card
        /// </summary>
        /// <param name="Ch"></param>
        /// <returns></returns>
        public virtual bool GetDI(int Ch)
        {
            return false;
        }
        /// <summary>
        /// forces input to value (for simulation); value may be overwritten with next card read cycle 
        /// </summary>
        /// <param name="Ch"></param>
        /// <param name="Value"></param>
        public virtual void ForceDI(int Ch, bool Value)
        {   }
        /// <summary>
        /// returns current output value of a single channel of the card
        /// </summary>
        /// <param name="Ch"></param>
        /// <returns></returns>
        public virtual bool GetDO(int Ch)
        {
            return false;
        }
        /// <summary>
        /// set output value of a single channel of the card
        /// </summary>
        /// <param name="Ch"></param>
        /// <param name="Value"></param>
        public virtual void SetDO(int Ch, bool Value)
        {        }
    }
    public abstract class CardDefAnalogIO : CardDefBase
    {
        public CardDefAnalogIO(int Slot)
            : base(Slot) { }
        public virtual double GetAI(int Ch)
        {
            return 0;
        }
        /// <summary>
        /// forces input to value (for simulation)
        /// </summary>
        /// <param name="Ch"></param>
        /// <param name="Value"></param>
        public virtual void ForceAI(int Ch, double Value)
        { }
        public virtual double GetAO(int Ch)
        {
            return 0;
        }
        public virtual void SetAO(int Ch, double Value)
        { }
    }

}
