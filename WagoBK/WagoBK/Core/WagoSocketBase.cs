using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace WagoBK
{
    //this thread handels ethernet communication with BK
    public class WagoSocketBase
    {
        public enum State
        {
            Unknown = 0,
            Initialize,
            Connect,
            Configure,
            GetChannelData,
            ReadbackOutput,
            ReadInPA,
            WriteOutPA,
            PutChannelData
        }
        public WagoSocketBase(SyncEvents e, BKSetup Setup, BKConfiguration Config,
            BKIn Inputs, BKOut Outputs) //??output obsolete
        {
            m_NextState = State.Unknown;
            m_LastState = State.Unknown;
            //
            m_SyncEvents = e;
            m_BKSetup = Setup;
            m_BKConfig = Config;
            m_BKIn = Inputs;
            m_Modbus = new Modbus.ModbusCore();            
            m_InstalledCards = new CardManager();
            m_BKConfig.SetState(WagoBKState.Unknown);
        }
        protected void Init()
        {
            Disconnect();
        }
        protected void WriteOutput()
        {//write output PA

            bool IsOK = false;
            if (IsConnected() & m_OutPA.GetByteCount()>0 )
            {
                IsOK = m_Modbus.ExecFC16(0x00,m_OutPA); //Registeroffset from Cardmanager??
                if (!IsOK)
                {
                    ;//??
                }
            };
        }
        protected void ReadInput()
        {
            m_TriggerDIChanged=false;
            bool ResponseOK = false;

            if (IsConnected() & m_InChList.ProjectCards.GetPhyIn1WordCount() > 0)
            {
                ResponseOK = m_Modbus.ExecFC3(0x00, (UInt16)m_InChList.ProjectCards.GetPhyIn1WordCount(), ref m_InPA);
                if (!ResponseOK)
                {
                    //??
                };
            };
            m_BKConfig.SetState(WagoBKState.Running); //zu langsam??
        }
        protected void ChannelsToPA()
        {
            m_OutPA.SetAsBytes(m_InChList.ProjectCards.GetPhyOut1(), m_InChList.ProjectCards.GetPhyOut1WordCount() * 2);
        }
        protected void PAToChannels()
        {
            m_InChList.ProjectCards.ParsePhyIn1(m_InPA.GetAsBytes());
            if(m_InChList.ProjectCards.IsDIStateChanged())
                m_TriggerDIChanged = true;
        }
        /// <summary>
        /// reads installed cards from BK
        /// </summary>
        protected void GetInstalledCards()
        {
            m_InstalledCards.Clear();
            Modbus.ModbusDataStruct m_Data = new Modbus.ModbusDataStruct();
            if (m_Modbus.ExecFC3(0x2030, 65, ref m_Data))
            {
                CreateInstalledCards(m_Data.GetAsWords());
                if (m_InChList.AutodetectChannels )//&
                    //m_InChList.AIChannels.Count==0 &
                    //m_InChList.AOChannels.Count == 0 &
                    //m_InChList.DIChannels.Count == 0 &
                    //m_InChList.DOChannels.Count == 0)
                {
                    CardDefBase Card;
                    m_InChList.ProjectCards = null;
                    m_InChList.ProjectCards = m_InstalledCards;
                    int[] Slots = m_InChList.ProjectCards.GetSlotNumbers();
                    m_InChList.AIChannels.Clear();
                    m_InChList.AOChannels.Clear();
                    m_InChList.DIChannels.Clear();
                    m_InChList.DOChannels.Clear();
                    string ChName;
                    for(int i =0;i<Slots.Length;i++)
                    {
                        Card = m_InChList.ProjectCards.GetCard(Slots[i]);
                        for(int ch=0; ch<Card.GetChannelCount();ch++)
                        {
                            ChName = string.Format("{0:G}_{1:D}_{2:D}",
                                    Card.GetChannelType(), i, ch);
                            switch(Card.GetChannelType())
                            {
                                case ChannelType.AI:
                                    m_InChList.AIChannels.Add(ChName, new AIChannelDef(ChName, "", (CardDefAnalogIO)Card, ch));
                                    break;
                                case ChannelType.AO:
                                    m_InChList.AOChannels.Add(ChName, new AOChannelDef(ChName, "", (CardDefAnalogIO)Card, ch));
                                    break;
                                case ChannelType.DI:
                                    m_InChList.DIChannels.Add(ChName, new DIChannelDef(ChName, "", (CardDefDigitalIO)Card, ch));
                                    break;
                                case ChannelType.DO:
                                    m_InChList.DOChannels.Add(ChName, new DOChannelDef(ChName, "", (CardDefDigitalIO)Card, ch));
                                    break;
                            }
                        }
                    }
                    OnCardChanged();//inform client about modification of channel list
                }
                else
                {
                    CheckInstalledCards();
                }
            }
        }
        /// <summary>
        /// creates card objects from GetCardInfo-Data
        /// </summary>
        /// <param name="CardInfo"></param>
        protected void CreateInstalledCards(UInt16[] CardInfo)
        {
            int i = 0; //
            while (i < CardInfo.Length)
            {
                if (CardInfo[i] > 0)
                {//
                    m_InstalledCards.AddCard(i, CardManager.CreateCardByCode(i,CardInfo[i]));
                };
                i++;
            }
        }
        /// <summary>
        /// Compares detected cards with ChannelDefinition
        /// Throws Exception
        /// </summary>
        protected void CheckInstalledCards()
        {
            int[] Slots = m_InstalledCards.GetSlotNumbers();
            if (Slots.GetLength(0) < 1)
            {
                throw new Exception("No cards detected.");
            }
            else if (m_InstalledCards.GetSlotNumbers().Length != 
                m_InChList.ProjectCards.GetSlotNumbers().Length)
            {
                throw new Exception("number projected and installed cards different ");
            }
            else
            {
                CardDefBase CardProjected;
                CardDefBase CardInstalled;
                foreach (int i in Slots)
                {
                    CardProjected = m_InChList.ProjectCards.GetCard(i);
                    CardInstalled = m_InstalledCards.GetCard(i);
                    if (CardProjected.GetCardName() != CardInstalled.GetCardName())
                    {
                        throw new Exception("Card type mismatch projected-installed "+
                        CardProjected.GetCardName() + "-" + CardInstalled.GetCardName());
                    }
                }
            }
        }
        /// <summary>
        /// configure BK (watchdog,..)
        /// </summary>
        protected void WriteBKSetup()
        {
            int TO = m_BKSetup.GetWatchDog();
            //??
        }
        protected void ReadbackOutputs()
        {
            Modbus.ModbusDataStruct m_Data = new Modbus.ModbusDataStruct();
            bool ResponseOK = false;
            if (IsConnected() & m_InChList.ProjectCards.GetPhyOut1WordCount() > 0)
            {
                ResponseOK = m_Modbus.ExecFC3(0x200, (UInt16)m_InChList.ProjectCards.GetPhyOut1WordCount(), ref m_InPA);
                if (!ResponseOK)
                {
                    //??
                }
                else
                {
                    m_InChList.ProjectCards.ParsePhyOut1(m_InPA.GetAsBytes());
                }

            };
        }
        protected void Connect()
        {
            {
                m_Modbus.Connect(m_BKSetup.GetIP(), m_BKSetup.GetPort());
                m_BKConfig.SetState(m_Modbus.IsConnected() ? WagoBKState.Connected : WagoBKState.Disconnected);
            }
        }
        protected bool IsConnected()
        {            
            return m_Modbus.IsConnected();
        }
        protected void Disconnect()
        {
            if (m_Modbus != null)
            {
                m_Modbus.Disconnect();
            };
            m_BKConfig.SetState( WagoBKState.Disconnected);
        }
        /// <summary>
        /// communication loop to koppler
        /// 
        /// </summary>
        public void ThreadRun()
        {
            while (!m_SyncEvents.ExitThreadEvent.WaitOne(0, false))
            {
                m_LastState = m_NextState;
                Thread.Sleep(10);
                if (m_NextState == State.Unknown)
                {
                    m_NextState = State.Initialize;
                }
                else if (m_NextState == State.Initialize)
                {//initialize internal data
                    Init();
                    m_NextState = State.Connect;  //try to conect
                }
                else if (m_NextState == State.Connect)
                {//try until conection established
                    //if(m_LastState==State.Initialize) 
                    Connect();
                    if (IsConnected())
                    { m_NextState = State.Configure; }
                    else
                    {
                        Thread.Sleep(100);
                        m_NextState = State.Connect; 
                    }
                }
                else if (m_NextState == State.Configure)
                {//configure BK
                    m_InChList = m_BKIn.GetInput();
                    GetInstalledCards();  //check if cards matches channel def
                    WriteBKSetup();
                    m_NextState = IsConnected() ? State.GetChannelData : State.Initialize;
                }
                else if (m_NextState == State.GetChannelData)
                {//get app data 
                    m_NextState = State.ReadbackOutput;
                }
                else if (m_NextState == State.ReadbackOutput)
                {//reads output values from koppler and initialises CardState
                    ReadbackOutputs();
                        int[] Slots = m_InChList.ProjectCards.GetSlotNumbers();
                        for (int i = 0; i < Slots.Length; i++)
                        {
                            m_InChList.ProjectCards.GetCard(Slots[i]).CardPOR();
                        }
                        m_NextState = IsConnected() ? State.ReadInPA : State.Initialize;
                }
                else if (m_NextState == State.ReadInPA)
                {//read BKdata 
                    ReadInput();
                    PAToChannels();
                    m_NextState = IsConnected() ? State.WriteOutPA : State.Initialize;
                }
                else if (m_NextState == State.WriteOutPA)
                {//write BKdata 
                    ChannelsToPA();
                    WriteOutput();
                    m_NextState = IsConnected() ? State.PutChannelData : State.Initialize;
                }
                    //lesen/schreiben von Kommandos auf Spezialklemmen??
                else if (m_NextState == State.PutChannelData)
                {//send data to app
                    m_BKIn.SetAllInputs(m_InChList);
                    if (m_TriggerDIChanged) OnDIChanged();
                    m_NextState = State.ReadInPA;  
                }
            }
            //terminate if client stopped
        }
        public event OnDIChangeEventHandler EventDIChange;
        public delegate void OnDIChangeEventHandler(object sender, EventArgs e);
        protected virtual void OnDIChanged()
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            OnDIChangeEventHandler handler = EventDIChange;
            EventArgs e = new EventArgs();
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        public event OnCardChangeEventHandler EventCardChange;
        public delegate void OnCardChangeEventHandler(object sender, EventArgs e);
        protected virtual void OnCardChanged()
        {
            OnCardChangeEventHandler handler = EventCardChange;
            EventArgs e = new EventArgs();
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                handler(this, e);
            };
        }

        protected BKSetup m_BKSetup;
        protected BKConfiguration m_BKConfig;
        protected BKIn m_BKIn;  //reference to BK channel def
        //protected BKOut m_BKOut; //reference to BK channel def

        private CardManager m_InstalledCards;   //list of detected cards
        private Channels m_InChList; //snapshot of app input
        private Modbus.ModbusDataStruct m_InPA = new Modbus.ModbusDataStruct();
        private Modbus.ModbusDataStruct m_OutPA = new Modbus.ModbusDataStruct();
        private bool m_TriggerDIChanged; //flag if InPA changed since last cycle
        private Modbus.ModbusCore m_Modbus;
        private State m_NextState;
        private State m_LastState;
        private SyncEvents m_SyncEvents;
    }
}