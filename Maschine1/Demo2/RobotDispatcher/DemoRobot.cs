using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using WagoBK;
using Maschine1;

namespace Demo2 {
    class DemoRobot1 : Robot
    {
        public DemoRobot1(RobotInterface Interface, ManualResetEvent Stop, ManualResetEvent Stopped)
            : base(Interface,Stop,Stopped) {
            m_BK = (DemoBK)Interface.GetBK();
            m_TypeParams = new DemoTypeParams();
        }
        // Hilfsroutine um den im Robot deklarierten TypeParams-Container zu unserem eigenen zu Casten.
        // damit auch die passenden Daten geladen werden muss im Konstruktor m_TypeParameters neu initialisiert werden.
        private Demo2.DemoTypeParams.DemoParams GetParams() {
            return ((DemoTypeParams.DemoParams)((DemoTypeParams)m_TypeParams).GetParams());
        }
        protected override int Step(RobotMode Mode,int StepIndex)        {
            Thread.Sleep(10);
            int NextStep = StepIndex;
            string UserInfo = "";
            int _i = -1;
            bool _ShowResult = false;
         
            if (StepIndex == ++_i) {
                InitCycle();
                NextStep++;
            } else if (StepIndex == ++_i) {
                UserInfo = "Teil einlegen und START drücken";
                if (m_BK.Ch().DI_PartInserted && m_BK.Ch().DI_Start) {
                    WTFileCollection.GetWTFile(0).SetProgram(1); //wird als Startflag für Robot2 verwendet
                    WTFileCollection.GetWTFile(0).SetSN(Randomize.Int(10000).ToString());  
                    NextStep++;
                } else {
                    WTFileCollection.GetWTFile(0).SetProgram(0);
                }
            } else if (StepIndex == ++_i) {
                UserInfo = "Teil einschleusen " +GetParams().m_CustTypeNumber;
                if (m_BK.Ch().DI_SledgeInWP) {
                    NextStep++;
                }
            } else if (StepIndex == ++_i) {
                UserInfo = "Teil prüfen";
                if (m_BK.Ch().DI_SledgeInHP) {
                    NextStep++;
                }
            } else if (StepIndex == ++_i) {
                _ShowResult = true;
                UserInfo = "Teil entnehmen";
                if (!m_BK.Ch().DI_PartInserted) {
                    WTFileCollection.GetWTFile(0).SetProgram(0);
                    NextStep = 0;
                }
            }
            if (!_ShowResult && ((StepIndex != NextStep) || UserInfo.Length > 0)) {
                UpdateStatDisplay(string.Format("Step {0:d} " + UserInfo, StepIndex));
            }
            if (_ShowResult && ((StepIndex != NextStep) || UserInfo.Length > 0)) {
                UpdateStatDisplay(WTFileCollection.GetWTFile(0).GetResultDef().GetTotalResult(),UserInfo);
            }
            return NextStep;
        }
        private DemoBK m_BK;
    }
    /// <summary>
    /// Robot für die 2.Station
    /// </summary>
    public class DemoRobot2 : Robot
    {
        public enum State
        {
            Unknown = 0,
            PowerOK,
            HP,
            SledgeOut,
            SledgeIn,
            IndexHP,
            IndexWP,
            GripperClosed,
            GripperOpen,
            MeasureTorque
           
        }
        public DemoRobot2(RobotInterface Interface, ManualResetEvent Stop, ManualResetEvent Stopped)
            : base(Interface, Stop, Stopped) 
        {
            m_BK = (DemoBK)Interface.GetBK();
            m_TypeParams = new DemoTypeParams();
            m_NextState = State.Unknown;
        }
        private Maschine1.Statistic.IReportGenerator m_ReportGenerator = new Maschine1.Statistic.ReportGenerator();
        private int m_StepTimeout = 0;
        private int m_StepTimer = 0;
        private int m_StepStart = 0;
        protected override int Step(RobotMode Mode,int StepIndex) {
            m_StepStart = System.Environment.TickCount;
            Thread.Sleep(10);
            int NextStep = StepIndex;
            string UserInfo = "";
            int _i = -1;
            Thread.Sleep(5);
           ((DemoModuleDispl)this.m_Interface.GetModuleDisplay()).DrawState(GetEAState());
           ((DemoModuleDispl)this.m_Interface.GetModuleDisplay()).SetMessage(GetEAState().ToString()+" ->"+m_NextState.ToString());
            
    ///////////////////////////////////////////////////////////////////////////////////////
    //Grundstellungsfahrt
        if (Mode == RobotMode.Homing) {
            #region Schrittkette Homing
            if (StepIndex == ++_i) {
                InitCycle();
                m_StepTimer = 0;
                m_StepTimeout = 0;
                NextStep = StepIndex + 1;
            } else if (StepIndex == ++_i) {
                if (GetEAState() == State.Unknown) {
                    UserInfo = "Bereit für Einschalten";
                    m_NextState = State.PowerOK;
                    SetEAState(m_NextState);
                }
                if (IsInState(State.PowerOK)) {// && m_NextState == State.PowerOK) {
                    UserInfo = "Greifer öffnen";
                    m_StepTimer = 0;
                    m_StepTimeout = 1000;
                    m_NextState = State.GripperOpen;
                    SetEAState(m_NextState);
                }
                if (IsInState(State.GripperOpen)) {//&& m_NextState == State.GripperOpen) {
                    UserInfo = "deindexieren";
                    m_StepTimer = 0;
                    m_StepTimeout = 1000;
                    m_NextState = State.IndexHP;
                    SetEAState(m_NextState);
                }
                if (IsInState(State.IndexHP)) {// && m_NextState == State.IndexHP) {
                    UserInfo = "Schlitten in HP fahren";
                    m_StepTimer = 0;
                    m_StepTimeout = 1000;
                    m_NextState = State.SledgeOut;
                    SetEAState(m_NextState);
                }
                if (IsInState(State.SledgeOut)) {//  && m_NextState == State.SledgeOut) {
                    m_NextState = State.HP;
                    m_StepTimer = 0;
                    m_StepTimeout = 1000;
                    SetEAState(m_NextState);
                } 
                if (IsInState(State.HP)) {//  && m_NextState == State.HP) {//out Station
                    NextStep = StepIndex + 1;
                } 
                //Wenn wir in keinen stabilen Zustand sind schlägt irgendwann das Timeout an
                if (m_StepTimeout>0 && m_StepTimer > m_StepTimeout) {
                        UserInfo = Translator.Tr("Timeout for settling State", "") + " " + m_NextState.ToString();
                        throw new SystemException(UserInfo);
                }
             
            } else if (StepIndex == ++_i) {
                UserInfo = "Homing ok";
                NextStep = 0;
            }
            #endregion
        }
    ///////////////////////////////////////////////////////////////////////////////////////
    //Automatik
            if (Mode == RobotMode.Automatic) {
                #region Schrittkette Automatic
                if (StepIndex == ++_i) {
                    InitCycle();
                    NextStep = StepIndex + 1;
                    m_StepTimeout = 0;
                } else if (StepIndex == ++_i) {//check power and HP
                    m_StepTimeout = 0;
                    if (!IsInState(State.HP)) {
                        UserInfo = "Grundstellungsfahr erforderlich";
                    } else {
                        m_NextState = State.HP;
                        NextStep = StepIndex + 1;
                    }
                } else if (StepIndex == ++_i) {//wait for start from Station1
                    if (WTFileCollection.GetWTFile(0).GetProgram() > 0) {
                        NextStep = StepIndex + 1;
                    }
                } else if (StepIndex == ++_i) {//into the station
                    if (IsInState(m_NextState) && m_NextState == State.HP) {
                        UserInfo = "in Grundstellung";
                        m_NextState = State.SledgeIn;
                        SetEAState(m_NextState);
                        m_StepTimer = 0;
                        m_StepTimeout = 1000;   
                    } else if (IsInState(m_NextState) && m_NextState == State.SledgeIn) {
                        UserInfo = "Schlitten eingefahren";
                        m_NextState = State.IndexWP;
                        SetEAState(m_NextState);
                        m_StepTimer = 0;
                        m_StepTimeout = 1000;
                    } else if (IsInState(m_NextState) && m_NextState == State.IndexWP) { //in Station
                        UserInfo = "indexiert";
                        m_NextState = State.GripperClosed;
                        SetEAState(m_NextState);
                        m_StepTimer = 0;
                        m_StepTimeout = 1000;
                    } else if (IsInState(m_NextState) && m_NextState == State.GripperClosed) {
                        UserInfo = "Messung vorbereiten";
                        NextStep = StepIndex + 1;
                    }
                } else if (StepIndex == ++_i) {
                    //check voltage
                    WTFileCollection.GetWTFile(0).GetResultDef().SetResultValue("AD1", ((double)(Randomize.Int(600))) / 100);
                    NextStep = StepIndex + 1;
                } else if (StepIndex == ++_i) {
                    //check button
                    MeasureTorque();
                    NextStep = StepIndex + 1;
                } else if (StepIndex == ++_i) {//out of station
                    if (IsInState(m_NextState) && m_NextState == State.GripperClosed) {
                        UserInfo = "Messung Ende";
                        m_NextState = State.GripperOpen;
                        SetEAState(m_NextState);
                        m_StepTimer = 0;
                        m_StepTimeout = 1000;
                    } else if (IsInState(m_NextState) && m_NextState == State.GripperOpen) {
                        UserInfo = "Greifer offen";
                        m_NextState = State.IndexHP;
                        SetEAState(m_NextState);
                        m_StepTimer = 0;
                        m_StepTimeout = 1000;
                    } else if (IsInState(m_NextState) && m_NextState == State.IndexHP) {
                        UserInfo = "deindexiert";
                        m_NextState = State.SledgeOut;
                        SetEAState(m_NextState);
                        m_StepTimer = 0;
                        m_StepTimeout = 1000;
                    } else if (IsInState(m_NextState) && m_NextState == State.SledgeOut) {
                        UserInfo = "Schlitten ausgefahren";
                        m_NextState = State.HP;
                        SetEAState(m_NextState);
                        m_StepTimer = 0;
                        m_StepTimeout = 1000;
                    } else if (IsInState(m_NextState) && m_NextState == State.HP) {//out Station
                        NextStep = StepIndex + 1;
                    }
                } else if (StepIndex == ++_i) {
                    m_ReportGenerator.AddTotalResult(WTFileCollection.GetWTFile(0));
                    switch (WTFileCollection.GetWTFile(0).GetResultDef().GetTotalResult()) {
                        case ResultDef.TestResultEnum.Pass:
                            UserInfo = Translator.Tr("final result OK","");
                            m_Counter.Count(true);
                            break;
                        default:
                            UserInfo = Translator.Tr("final result NOK", "");
                            m_Counter.Count(false);
                            break;
                    }
                    UpdateResultDisplay(WTFileCollection.GetWTFile(0));
                    NextStep = 0;
                }
                //Wenn wir in keinen stabilen Zustand sind schlägt irgendwann das Timeout an
                if (m_StepTimeout > 0 && m_StepTimer > m_StepTimeout) {
                    UserInfo = Translator.Tr("Timeout for settling State","") +" "+ m_NextState.ToString();
                    throw new SystemException(UserInfo);
                }
                #endregion
            }
            if ((StepIndex != NextStep) || UserInfo.Length > 0) {
                UpdateStatDisplay(string.Format("Step {0:d} " + UserInfo, StepIndex));
            }
            m_StepTimer+=System.Environment.TickCount-m_StepStart;
            OnStepExit();   //aktuell wird nur vom Simulator ausgewertet
            return NextStep;
        }
        private void MeasureTorque() {
            WTFileCollection.GetWTFile(0).GetResultDef().SetResultValue("ButtonOK", Randomize.Int(10) > 4);
        }
        private bool IsInState(State ThisState)
        {
            bool StateOK = false;
            switch (ThisState)
            {
                case State.SledgeIn:
                    StateOK = (GetEAState() == State.SledgeIn) || (GetEAState() == State.IndexHP);
                    break;
                case State.IndexWP:
                    StateOK = (GetEAState() == State.GripperOpen) || (GetEAState() == State.IndexWP);
                    break;
                case State.SledgeOut:
                    StateOK = (GetEAState() == State.HP) || (GetEAState() == State.SledgeOut);
                    break;
                default:
                    StateOK = (GetEAState() == ThisState);
                    break;
            }
            return StateOK;
        }
        /// <summary>
        /// gibt den Maschinenstatus ausgehend vom Status der Hardware (BK) zurück
        /// Ist der Status unklar wird unknown ausgegeben und als nächstes sollte Grundstellung gefahren werden
        /// </summary>
        /// <returns></returns>
        private State GetEAState()
        {
            State CurrState = State.Unknown;
            if (m_BK.Ch().DI_MainPressureReady)
            {
                CurrState = State.PowerOK;
            };
            if (m_BK.Ch().DI_MainPressureReady &&
                m_BK.Ch().DI_SledgeInHP)
            {
                CurrState = State.SledgeOut;
            };
            if (m_BK.Ch().DI_MainPressureReady &&
                m_BK.Ch().DI_SledgeInWP 
                )
            {
                CurrState = State.SledgeIn;
            };
            if (m_BK.Ch().DI_MainPressureReady &&
                m_BK.Ch().DI_SledgeInWP &&
                m_BK.Ch().DI_IndexInWP) {
                CurrState = State.IndexWP;
            };
            if (m_BK.Ch().DI_MainPressureReady &&
                m_BK.Ch().DI_SledgeInWP &&
                m_BK.Ch().DI_IndexInHP)
            {
                CurrState = State.IndexHP;
            };
            if (m_BK.Ch().DI_MainPressureReady &&
                m_BK.Ch().DI_SledgeInWP &&
                m_BK.Ch().DI_IndexInWP &&
                m_BK.Ch().DI_GripperInHP) {
                CurrState = State.GripperOpen;
            };
            if (m_BK.Ch().DI_MainPressureReady &&
                m_BK.Ch().DI_SledgeInWP &&
                m_BK.Ch().DI_IndexInWP &&
                m_BK.Ch().DI_GripperInWP) {
                CurrState = State.GripperClosed;
            };
            if (m_BK.Ch().DI_MainPressureReady &&
                !m_BK.Ch().DI_SledgeInWP &&
                m_BK.Ch().DI_SledgeInHP &&
                !m_BK.Ch().DI_IndexInWP &&
                m_BK.Ch().DI_IndexInHP &&
                !m_BK.Ch().DI_GripperInWP &&
                m_BK.Ch().DI_GripperInHP)
            {
                CurrState = State.HP;
            };
            return CurrState;
        }
        /// <summary>
        /// ändert den Hardwarestatus (BK) ausgehend vom gewünschten Zielzustand
        /// Es muss vorher immer der Istzustand geprüft werden und dann die Hardware angesteuert werden.
        /// </summary>
        /// <param name="NewState"></param>
        private void SetEAState(State NewState)
        {
            if (NewState==State.PowerOK)
            {
                if (m_BK.Ch().DO_PCReady != true)
                {
                    m_BK.Ch().DO_CloseGripper = false;
                    m_BK.Ch().DO_IndexToWP = false;
                    m_BK.Ch().DO_SledgeToWP = false;
                };
                m_BK.Ch().DO_PCReady = true;
            };
            if (NewState == State.SledgeOut)
            {
                if (m_BK.Ch().DI_IndexInHP)
                {
                    m_BK.Ch().DO_SledgeToWP = false;
                }
            }
            if (NewState == State.SledgeIn)
            {
                if (m_BK.Ch().DI_IndexInHP)
                {
                    m_BK.Ch().DO_SledgeToWP = true;
                }
            }
            if (NewState == State.GripperClosed) {
                if (m_BK.Ch().DI_IndexInWP) {
                    m_BK.Ch().DO_CloseGripper = true;
                }
            }
            if (NewState == State.GripperOpen) {
                    m_BK.Ch().DO_CloseGripper = false;
            }
            if (NewState == State.IndexHP)
            {
                if (m_BK.Ch().DI_SledgeInWP && m_BK.Ch().DI_GripperInHP)
                {
                    m_BK.Ch().DO_IndexToWP = false;
                }
            }
            if (NewState == State.IndexWP)
            {
                if (m_BK.Ch().DI_SledgeInWP)
                {
                    m_BK.Ch().DO_IndexToWP = true;
                }
            }
        }
        private DemoBK m_BK;
        private State m_NextState;
    }
    
}
