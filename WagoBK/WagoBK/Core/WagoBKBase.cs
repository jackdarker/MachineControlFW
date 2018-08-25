using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace WagoBK
{
    /*! \page page2 WagoBK in Programm einbinden
 * zum Erzeugen des Objekts und Starten der Kommunikation:
 * \code
    private DemoBK BK = new DemoBK();   //Koppler Object instanzieren
    private DlgWagoBKControl WagoPane = new DlgWagoBKControl(); //Bedienpanel instanzieren (optional)
    WagoPane.ConnectToBK(BK); //Bedienpanel mit Koppler verbinden (optional)
    BK.SetConfig(); // Koppler initialisieren
    BK.Start(); //Kommunikation starten
    WagoPane.Show();  //Bedienpanel einblenden (optional)
 * \endcode
   Eine Art EA-Simulation ist möglich indem BK.Start() nicht ausgeführt wird und die EAs im WagoPanel manuell gesetzt werden.
 */
    /*! \page page1 Neue BK-Klasse erstellen
     * - \ref sec
     * - \ref subsection1
     * - \ref subsection2
     * 
     * Um eine neue BK-Klasse anzulegen:
     * -# Klasse von WagoBKBase ableiten
     * -# Klasse von Channels ableiten und InitChannelList überladen, jeden Kanal durch ein Property darstellen
     * -# CreateChannels überladen und neue Channels-Klasse zurückgeben  
     * Statt Karten und Channels vorzudefinieren, kann auch Selbsterkennung gestartet werden
     * Dies kann auch über den WagoBKWizard automatisch erfolgen.
     * For more info see page \ref page2.
     * \section sec Beispiel
     * z.B.
     * \code
    public class DemoBK : WagoBKBase
    {
        public class DemoChannels : Channels
        {
            public DemoChannels():base()
            {  }
            protected override void InitChannelList()
            {
                //create list of cards
                int Slot=0;
                ProjectCards.AddCard(Slot++, new CardDef482);
                ProjectCards.AddCard(Slot++, new CardDef530);
                ProjectCards.AddCard(Slot++, new CardDef430);
                
                //create channel collection
                DOChannels.Add("PCReady", new DOChannelDef("PCReady","",ProjectCards.GetCard(1),0));

                DIChannels.Add("MainPressureReady", new DIChannelDef("MainPressureReady", "", ProjectCards.GetCard(2),0));
            }
            //create properties for simple access
            public bool DO_PCReady
            {
                get { return DOChannels["PCReady"].Get(); }
                set { DOChannels["PCReady"].Set(value); }
            }
            public bool DI_MainPressureReady
            {
                get { return DIChannels["MainPressureReady"].Get(); }
            }
        }
        public DemoBK():base()
        {
        }
        protected override Channels CreateChannels()
        {
            return new DemoChannels();
        }
        public new DemoChannels Ch()
        {
            return (DemoChannels)base.Ch();
        }
    }
     * \endcode
     * \subsection subsection1 Kanäle als Properties
     * \code
     * if(DI_MainPressureReady==true)
     * {    }
     * \endcode
     * statt
     * \code
     * if(DemoBK.Ch().DIChannels["MainPressureReady"].Get()==true}
     * {   }
     * \endcode
     * \subsection subsection2 Automatische Kanalerkennung
     * Für eine schnelle Inbetriebnahme kann auch eine automatische Erkennung gestartet werden.
     * 
     * \code
     * protected override void InitChannelList()
     * {
     *    AutodetectChannels = true;
     * }
     * \endcode
     */


    #region HelperClasses
    /// <summary>
    /// Status des Buskopplers
    /// </summary>
        public enum WagoBKState 
        { 
            Unknown=0,
            Disconnected,   //initialised but no connection
            Connected,      //can communicate but PA is not transfered
            Running         //transfering PA
        }
        /// stores setup
        public class BKSetup
        {
            public void SetWatchdog(int TO)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_WatchdogTO = TO;
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public int GetWatchDog()
            {
                int TO = 0;
                m_RWL.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    TO = m_WatchdogTO;
                }
                finally
                {
                    m_RWL.ReleaseReaderLock();
                }
                return TO;
            }
            public void SetIP(string IP, int Port)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_IP = IP;
                    m_Port = Port;
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public string GetIP()
            {
                string IP = "";
                m_RWL.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    IP = m_IP;
                }
                finally
                {
                    m_RWL.ReleaseReaderLock();
                }
                return IP;
            }
            public int GetPort()
            {
                int Port = 0;
                m_RWL.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    Port = m_Port;
                }
                finally
                {
                    m_RWL.ReleaseReaderLock();
                }
                return Port;
            }
            protected ReaderWriterLock m_RWL = new ReaderWriterLock();
            private int m_WatchdogTO;
            private int m_Port;
            private string m_IP;
        }
        ///obsolete, will be removed??
        public class BKConfiguration
        {
          /*  public void SetChannelList(Channels List)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_Channels=List;
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public Channels GetChannelList()
            {
                Channels List = new Channels();
                m_RWL.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    List = m_Channels;
                }
                finally
                {
                    m_RWL.ReleaseReaderLock();
                }
                return List;
            }*/
            public void SetError(string Msg)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_ErrorPending=true;
                    m_ErrorTxt = Msg;
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public bool GetError(ref string Msg)
            {
                bool Pending = false;
                Msg = "";
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    Msg = m_ErrorTxt;
                    Pending = m_ErrorPending;
                    m_ErrorPending = false; //mark error as read   
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
                return Pending;
            }
            public void SetState(WagoBKState State)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_State = State;
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public WagoBKState GetState()
            {
                WagoBKState State = WagoBKState.Unknown;
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    State = m_State;
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
                return State;
            }
            protected ReaderWriterLock m_RWL = new ReaderWriterLock();
            private bool m_ErrorPending;
            private string m_ErrorTxt;
            private WagoBKState m_State;
        }
        ///stores output data
        public class BKOut
        {
            public BKOut()
            {
                m_Channels = new Channels();
            }
            public BKOut(Channels List)
            {
                m_Channels = List; //use copy constructor??
            }
            public void SetOutput(params DOChannelDef[] Channels)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    if (Channels.GetLength(0) > 0)
                    { 
                        foreach(DOChannelDef Channel in Channels)
                        {   //exception if invalid name??
                            m_Channels.DOChannels[Channel.GetName()] = Channel; //copy data only??
                        }
                    }    
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public void SetAllOutputs(Channels List)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_Channels = List; //copy data only??
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public Channels GetOutput()
            {
                Channels List = new Channels();
                m_RWL.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    List = m_Channels;  //copy data only??
                }
                finally
                {
                    m_RWL.ReleaseReaderLock();
                }
                return List;
            }
            protected ReaderWriterLock m_RWL = new ReaderWriterLock();
            private Channels m_Channels;
        }
        ///stores input data
        public class BKIn
        {
            public BKIn()
            {
                m_Channels = new Channels();
            }
            public BKIn(Channels List)
            {
                m_Channels = List; //use copy constructor??
            }
            public void SetAllInputs(Channels List)
            {
                m_RWL.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    m_Channels = List;
                }
                finally
                {
                    m_RWL.ReleaseWriterLock();
                }
            }
            public Channels GetInput()
            {
                Channels List = new Channels();
                m_RWL.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    List = m_Channels;
                }
                finally
                {
                    m_RWL.ReleaseReaderLock();
                }
                return List;
            }
            protected ReaderWriterLock m_RWL = new ReaderWriterLock();
            private Channels m_Channels;
        }
        ///events to stop WagoSocket
        public class SyncEvents
        {
            public SyncEvents()
            {

                _newItemEvent = new AutoResetEvent(false);
                _exitThreadEvent = new ManualResetEvent(false);
                _eventArray = new WaitHandle[2];
                _eventArray[0] = _newItemEvent;
                _eventArray[1] = _exitThreadEvent;
            }

            public EventWaitHandle ExitThreadEvent
            {
                get { return _exitThreadEvent; }
            }
            public EventWaitHandle NewItemEvent
            {
                get { return _newItemEvent; }
            }
            public WaitHandle[] EventArray
            {
                get { return _eventArray; }
            }

            private EventWaitHandle _newItemEvent;
            private EventWaitHandle _exitThreadEvent;
            private WaitHandle[] _eventArray;
        }
    #endregion
   #region Channels
    /// <summary>
    /// Basisklasse die die Auflistung der BK-Kanäle enthält.
    /// </summary>
        public class Channels
        {
            public Dictionary<string, DOChannelDef> DOChannels = new Dictionary<string, DOChannelDef>();
            public Dictionary<string, DIChannelDef> DIChannels = new Dictionary<string, DIChannelDef>();
            public Dictionary<string, AIChannelDef> AIChannels = new Dictionary<string, AIChannelDef>();
            public Dictionary<string, AOChannelDef> AOChannels = new Dictionary<string, AOChannelDef>();

            public CardManager ProjectCards = new CardManager();

            public Channels()
            {
                InitChannelList();
            }
            /// <summary>
            /// Muss überladen werden. Initialisiert die Kanalliste und Kartenliste. 
            /// oder Autodetect aktivieren
            /// </summary>
            protected virtual void InitChannelList()
            { }
            private bool m_AutodetectChannels = false;
            public bool AutodetectChannels
            {
                set { m_AutodetectChannels = value;  }
                get { return m_AutodetectChannels;  }
            }
        }
    #endregion
    public class WagoBKBase
    {
        #region accessors
        public static bool WaitForDI(DIChannelDef Ch1, bool State, int TO)
        {
            Ch1.Get();//??
            return false;
        }
        public WagoBKBase()
        {
            m_Channels = CreateChannels();
            m_SyncEvents = new SyncEvents();
            m_BKSetup = new BKSetup();
            m_BKConfig= new BKConfiguration();
            m_BKIn = new BKIn(m_Channels);
            m_BKOut = new BKOut(m_Channels);
            //create the Communication-Thread
            m_WagoSocket = new WagoSocketBase(m_SyncEvents,
                m_BKSetup, m_BKConfig, m_BKIn, m_BKOut);
            m_WagoSocketThread = new Thread(m_WagoSocket.ThreadRun);
        }

        protected void Init()
        {
        }
        /// <summary>
        /// Erzeugt benutzerdefinierte Channels-Klasse. Muss überladen werden.
        /// </summary>
        /// <returns></returns>
        protected virtual Channels CreateChannels()
        {
            return new Channels();
        }
        /// <summary>
        /// Kann überladen werden.
        /// Diese Funktion wird aufgerufen bevor der Status der Bedienelemente aktualisiert wird.
        /// Hier können Bedienelemente deaktiviert werden:
        /// Ch().AOChannels["PWM0"].DisableUI( !DIChannels["MainPressure"].Get());
        /// </summary>
        public virtual void CheckInterlock()
        { }
        /// <summary>
        /// Gibt die benutzerdefinierte Channels-Klasse zurück. Muss überladen werden.
        /// </summary>
        public virtual Channels Ch()
        {
            return m_Channels;
        }
        /* WagoBK1.PCReady=True;  if(WagoBK1.UUT_locked==true)
        
          WagoBK1.Channels().PCReady.Set(true);
         WagoBK1.Ch().PCReady.Set(true)
        //WagoBK1.PCReady=true
        //WagoBK1.SetOutput(WagoBK1.PCReady.Set(true), WagoBK1.LockUUT(true));
        /*  
         WagoBK1.SetOutput(WagoBK1.Channel(enum.PCReady).Set(true)
         */
        /// <summary>
        /// Konfiguriert die Einstellungen
        /// Muss vor dem Starten der Kommunikation ausgeführt werden
        /// </summary>
        /// <param name="RemoteIP"></param>
        /// <param name="RemotePort"></param>
        public void SetConfig(string RemoteIP, int RemotePort)
        {
            m_BKSetup.SetWatchdog(0);
            m_BKSetup.SetIP(RemoteIP, RemotePort);
        }
        /// <summary>
        /// Startet den Thread der die Kommunikation zum Buskoppler handelt
        /// </summary>
        public void Start()
        {
            m_WagoSocketThread.Start();
        }
        /// <summary>
        /// Stoppt den Thread der die Kommunikation zum Buskoppler handelt
        /// </summary>
        /// <param name="Abort"></param>
        public void Stop(bool Abort)
        {
            m_SyncEvents.ExitThreadEvent.Set();
            if (m_WagoSocketThread.ThreadState != ThreadState.Unstarted)
            {
                m_WagoSocketThread.Join();
            };
        }
        protected void UpdateState()
        { 
        }
        /// <summary>
        /// Liefert den aktuellen Verbindungsstatus zurück
        /// </summary>
        /// <returns></returns>
        public WagoBKState GetStateInfo()
        {
            return m_BKConfig.GetState();
            
        }
        /// <summary>
        /// Registriert Ereignishandler für DI-Pegelwechsel
        /// Das Ereignis wird pro Bus-Zyklus ausgelöst wenn mindestens ein DI-Eingang einen Flankenwechsel hat
        /// </summary>
        /// <param name="Handler"></param>
        public void ConnectToEvent(WagoSocketBase.OnDIChangeEventHandler Handler)
        {
            m_WagoSocket.EventDIChange += Handler;
        }
        /// <summary>
        /// Registriert Ereignishandler für Kartenwechsel
        /// d.h. es wird ausgelöst sobald Anzahl & Typ der physikalisch gesteckten Karten als geändert erkannt wird
        /// </summary>
        /// <param name="Handler"></param>
        public void ConnectToEvent(WagoSocketBase.OnCardChangeEventHandler Handler)
        {
            m_WagoSocket.EventCardChange += Handler;
        }
        #endregion

        SyncEvents m_SyncEvents = new SyncEvents();
        protected Channels m_Channels;
        protected WagoSocketBase m_WagoSocket;
        protected Thread m_WagoSocketThread;
        protected WagoBKState m_State = WagoBKState.Unknown;
        protected BKSetup m_BKSetup;
        protected BKConfiguration m_BKConfig;
        protected BKIn m_BKIn;
        protected BKOut m_BKOut;

    }

 
    /*#region async Interface updater
    //this thread can be used to receive data from WagoSocket and update a WagoBKPane
    public class WagoUpdater
    {
        public WagoUpdater(Queue<int> q, SyncEvents e)
        {
            _queue = q;
            _syncEvents = e;
        }

        public void ThreadRun()
        {
            int count = 0;
            while (WaitHandle.WaitAny(_syncEvents.EventArray) != 1)
            {
                lock (((ICollection)_queue).SyncRoot)
                {
                    int item = _queue.Dequeue();
                }
                count++;
            }
            Console.WriteLine("Consumer Thread: consumed {0} items", count);
        }
        private Queue<int> _queue;
        private SyncEvents _syncEvents;
    }
    #endregion
    /*public class ThreadSyncSample
    {
        private static void ShowQueueContents(Queue<int> q)
        {
            lock (((ICollection)q).SyncRoot)
            {
                foreach (int item in q)
                {
                    Console.Write("{0} ", item);
                }
            }
            Console.WriteLine();
        }
    */
}

