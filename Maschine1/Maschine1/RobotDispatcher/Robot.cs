using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using WagoBK;

namespace Maschine1
{
    public enum RobotMode
    {
        None =0,
        Homing,
        Automatic,
        Setup,
        Step
    }
    //Hilfsklasse zum Übergeben der AnzeigeReferenzen an den Robot
    public class RobotInterface
    {
        public RobotInterface()
        { }
        public RobotInterface(StationDisplay statDisplay, ModuleDisplay ModuleDisplayCntrl,
            ResultTable ResultTableCntrl, PartHistoryTable HistoryCntrl, 
            ModuleState ModuleStateInfo, WagoBKBase BK)
        {
            m_StationDisplay = statDisplay;
            m_ResultDisplay = ResultTableCntrl;
            m_PartHistoryDisplay = HistoryCntrl;
            m_ModuleDisplay = ModuleDisplayCntrl;
            m_ModuleState = ModuleStateInfo;
            m_BK = BK;
        }
        public StationDisplay GetStationDisplay()
        {
            return m_StationDisplay;
        }
        public ResultTable GetResultDisplay()
        {
            return m_ResultDisplay;
        }
        public PartHistoryTable GetPartHistoryDisplay()
        {
            return m_PartHistoryDisplay;
        }
        public ModuleDisplay GetModuleDisplay()
        {
            return m_ModuleDisplay;
        }
        public ModuleState GetModuleState()
        {
            return m_ModuleState;
        }
        public WagoBKBase GetBK()
        {
            return m_BK;
        }
        private ModuleState m_ModuleState;
        private StationDisplay m_StationDisplay;
        private ResultTable m_ResultDisplay;
        private PartHistoryTable m_PartHistoryDisplay;
        private ModuleDisplay m_ModuleDisplay;
        private WagoBKBase m_BK;
    }
    //Zustand des Moduls
    public class ModuleState
    {
        public event OnUpdateEventHandler EventUpdate;
        public delegate void OnUpdateEventHandler(object sender, EventArgs e);
        protected virtual void OnUpdate()
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            OnUpdateEventHandler handler = EventUpdate;
            EventArgs e = new EventArgs();
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Format the string to send inside the EventArgs parameter
                //e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        public void SetActiveMode(RobotMode Mode)
        {
            //??error if switching directly from automatic to manual
            RobotMode OldMode= m_ActiveMode;
            m_ActiveMode=Mode;
            if(OldMode != m_ActiveMode)  {
                OnUpdate();
            };
        }
        public RobotMode GetActiveMode()
        {
            return m_ActiveMode;
        }
        public void SetActiveType(string Type)
        {
            string OldType= m_ActiveType;
            m_ActiveType=Type;
            if(OldType != m_ActiveType)
            {
                OnUpdate();
            };
        }
        public string GetActiveType()
        {
            return m_ActiveType;
        }

        protected RobotMode m_ActiveMode;
        protected string m_ActiveType;
    }
    //Zustand des Robots
    public class RobotState
    {
        public RobotState() {
            m_Mode = RobotMode.None;
            m_Cycle = 0;
            m_Step = 0;
        }
        public RobotMode m_Mode;
        public int m_Cycle;
        public int m_Step;
        public string m_ActiveType;
    }
    public class Robot {

        private Robot() {
            m_State = new RobotState();
        }
        public Robot(RobotInterface Interface, ManualResetEvent Stop, ManualResetEvent Stopped): this(){
            m_Interface = Interface;
            m_TypeParams = new TypeParams();
            m_EventStopThread = Stop;
            m_EventThreadStopped = Stopped;
        }
        //bewirkt ein abbrechen des Zyklus und beenden des Threads
        public virtual void Stop()        {
            SetStop(true);
        }
        private void SetStop(bool value)  {
            m_StateSemaphore.EnterWriteLock();
            try            {
                m_StopRequested = value;
            } finally {
                m_StateSemaphore.ExitWriteLock();
            }
        }
        /// <summary>
        /// bewirkt ein abbrechen des Zyklus, Thread wartet auf fortsetzen mit Run
        /// </summary>
        public virtual void BreakCycle()        {
            SetRun(false);
            SetRunOnce(false);
        }
        public void RunOnce()        {
            SetRun(false);
            SetRunOnce(true);
            SetStop(false);
        }
        private void SetRunOnce(bool value){
            m_StateSemaphore.EnterWriteLock();
            try            {
                m_RunOnce = value;
            }
            finally            {
                m_StateSemaphore.ExitWriteLock();
            }
        }
        private bool GetRunOnce()        {
            bool Run = false;
            m_StateSemaphore.EnterReadLock();
            try            {
                Run=m_RunOnce;
            }
            finally            {
                m_StateSemaphore.ExitReadLock();
            }
            return Run;
        }
        public void Run()        {
            SetRun(true);
            SetRunOnce(false);
            SetStop(false);
        }
        private void SetRun(bool value)
        {
            m_StateSemaphore.EnterWriteLock();
            try
            {
                m_Run = value;
            }
            finally
            {
                m_StateSemaphore.ExitWriteLock();
            }
        }
        private bool GetRun() {
            bool Run = false;
            m_StateSemaphore.EnterReadLock();
            try  {
                Run = m_Run;
            }
            finally  {
                m_StateSemaphore.ExitReadLock();  
            }
            return Run;
        }
        //this is the function executed by the thread
        public void DoAutomatic() {
            try     {
                SetStop(false);
                bool Cycling = false; //
                InitAutomatic();
                //StopCycle();    //in Wartemodus setzen
                while (!ShouldStop())   {
                    if (GetRunOnce() || GetRun()) {
                        if (!Cycling) CycleStarted();   // run only on change
                        Cycling = true;
                        m_State.m_Step = Step(m_State.m_Mode,m_State.m_Step);
                        if (GetRunOnce() && m_State.m_Step == 0)   {
                            //Flag zurücksetzen beim Neustart des Zyklus
                            SetRunOnce(false);
                        };
                    } else    {
                        if (Cycling) CycleStopped();   // run only on change
                        Cycling = false;
                        Thread.Sleep(100);
                    }
                }
                CycleStopped();
                
                UpdateStatDisplay(string.Format("gestoppt"));
                PostAutomatic();
                m_EventThreadStopped.Set();
            }
            catch (SystemException e) {
                GUI.DlgError.HandleException(e);
                Stop();
            }
            finally  {
            }
        }
        //Robot is ready for new cycle if currently not running and Step=0
        public bool ReadyForCycle()   {
            bool Ready = false;
            m_StateSemaphore.EnterReadLock();
            try {
                Ready = (m_State.m_Step==0)
                           && !m_Run
                           && !m_RunOnce;
            }
            finally {
                m_StateSemaphore.ExitReadLock();
            }
            return Ready;
        }
        public bool Aborted() {
            return m_StopRequested;
        }
        public event OnStepExitEventHandler EventStepExit;
        public delegate void OnStepExitEventHandler(object sender, EventArgs e);
        protected virtual void OnStepExit() {
            OnStepExitEventHandler handler = EventStepExit;
            EventArgs e = new EventArgs();
            if (handler != null) {
                handler(this, e);
            }
        }
        public Statistic.TypeCounter GetRefCounter()        {
            return m_Counter;
        }

        /// <summary>
        /// Hier drin muss der Arbeits-Ablauf implementiert werden
        /// </summary>
        /// <param name="StepIndex"></param>
        /// <returns></returns>
        protected virtual int Step(RobotMode Mode,int StepIndex)        {
            int NextStep = 0;
            //UpdateStatDisplay(string.Format("Tick {0:d}", StepIndex));
            //UpdateModulDisplay();

            if (StepIndex == 0)
            {
                InitCycle();
            };
            if (StepIndex == 6)
            {
                NextStep = 0;
            }
            else
            {
                NextStep = StepIndex+1;
            }
            //?? auf Stepoverflow überwachen
            return NextStep;
        }
        protected void UpdateModulDisplay()   {
            if (m_Interface.GetModuleDisplay() != null && m_Interface.GetModuleDisplay().IsHandleCreated)  {
                if (m_Interface.GetModuleDisplay().InvokeRequired) {
                    m_Interface.GetModuleDisplay().Invoke(
                        new ModuleDisplay.DelegateSetStatus(m_Interface.GetModuleDisplay().SetStatus), 
                        new System.Object[] { m_State.m_Step });
                }   else  {
                    m_Interface.GetModuleDisplay().SetStatus(m_State.m_Step);
                }
            }
        }
        protected void UpdateStatDisplay(string Status)    {
            if (m_Interface.GetStationDisplay() != null && m_Interface.GetStationDisplay().IsHandleCreated)
            {
                if (m_Interface.GetStationDisplay().InvokeRequired)  {
                    m_Interface.GetStationDisplay().Invoke(
                        new StationDisplay.DelegateSetStatus(m_Interface.GetStationDisplay().SetStatus), 
                        new System.Object[] { Status });
                }
                else   {
                    m_Interface.GetStationDisplay().SetStatus(Status);
                }
            }
        }
        protected void UpdateStatDisplay(ResultDef.TestResultEnum Result, string text) {
            if (m_Interface.GetStationDisplay() != null && m_Interface.GetStationDisplay().IsHandleCreated) {
                if (m_Interface.GetStationDisplay().InvokeRequired) {
                    m_Interface.GetStationDisplay().Invoke(
                        new StationDisplay.DelegateSetResult(m_Interface.GetStationDisplay().SetResult),
                        new System.Object[] { Result,text });
                } else {
                    m_Interface.GetStationDisplay().SetResult(Result, text);
                }
            }
        }
        protected void UpdateResultDisplay(WTFileBase m_WTFile) {

            if (m_Interface.GetResultDisplay() != null && m_Interface.GetResultDisplay().IsHandleCreated)
            {
                if (m_Interface.GetResultDisplay().InvokeRequired)
                {
                    m_Interface.GetResultDisplay().Invoke(
                        new ResultTable.DelegateSetStatus(m_Interface.GetResultDisplay().SetStatus), 
                        new System.Object[] { m_WTFile.GetResultDef() });
                }
                else
                {
                    m_Interface.GetResultDisplay().SetStatus(m_WTFile.GetResultDef());
                }
            }
            if (m_Interface.GetPartHistoryDisplay() != null && m_Interface.GetPartHistoryDisplay().IsHandleCreated)
            {
                if (m_Interface.GetPartHistoryDisplay().InvokeRequired)
                {
                    m_Interface.GetPartHistoryDisplay().Invoke(
                        new PartHistoryTable.DelegateAddPart(m_Interface.GetPartHistoryDisplay().AddPart), 
                        new System.Object[] { m_WTFile });
                }
                else
                {
                    m_Interface.GetPartHistoryDisplay().AddPart(m_WTFile);
                }
            }
        }
        protected virtual void InitAutomatic() {
            m_EventThreadStopped.Reset();
            m_State.m_Step = 0;
            m_State.m_Mode = m_Interface.GetModuleState().GetActiveMode();
        }
        protected virtual void InitCycle() {
            try {
                //aktuellen Typ ermitteln
                m_State.m_ActiveType=m_Interface.GetModuleState().GetActiveType();
                //Typparameter laden und validieren
                m_TypeParams.LoadParams(m_State.m_ActiveType);
                m_Counter.LoadCounter(m_State.m_ActiveType);
                ResultDef ResultDef = m_TypeParams.GetParams().m_ResultDef;
               if (!ResultDef.ValidateResultDef())  {  
                 //prüfen ob Limits und Teststep richtig definiert sind??
                };
                //Ergebnisvariablen initialisieren
               WTFileCollection.GetWTFile(0).SetResultDef(ResultDef);
               WTFileCollection.GetWTFile(0).SetTypeName(m_State.m_ActiveType);
               
            }
            catch (SystemException e)   {
                throw(e); 
            }
            finally {     }
        }
        protected virtual void PostAutomatic(){
        }
        protected virtual void PostCycle(){
        }
        protected virtual void CycleStarted(){
            try
            {            }
            catch (SystemException e)  {
                throw (e);
            }
            finally
            {    }
        }
        protected virtual void CycleStopped()  {
            try  {            }
            catch (SystemException e)
            {
                throw (e);
            }
            finally
            {        }
        }
        private bool ShouldStop()        {
            bool Stop = false;
            m_StateSemaphore.EnterReadLock();
            try
            {
                Stop = m_StopRequested;
            }
            finally
            {
                m_StateSemaphore.ExitReadLock();
            }
            if (m_EventStopThread.WaitOne(0, true)) {
                Stop = true;
            }
            return Stop;
        }

        private ReaderWriterLockSlim m_StateSemaphore =new ReaderWriterLockSlim();
        private bool m_RunOnce;
        private bool m_StopRequested;
        private bool m_Run;
        protected ManualResetEvent m_EventStopThread=null;
        protected ManualResetEvent m_EventThreadStopped=null;
        protected RobotInterface m_Interface;
        protected RobotState m_State;
        protected TypeParams m_TypeParams;
       // protected WTFileBase m_WTFile; //??public
        protected Statistic.TypeCounter m_Counter = new Statistic.TypeCounter();

    }

}
