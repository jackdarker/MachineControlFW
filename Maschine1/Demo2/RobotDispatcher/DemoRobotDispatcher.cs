using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using WagoBK;
using Maschine1;

namespace Demo2 {
    //
    class DemoRobotDispatcher : RobotDispatcher {
        public DemoRobotDispatcher(ManualResetEvent Stop, ManualResetEvent Stopped, RobotInterface Dispatcher, RobotInterface Robot1, RobotInterface Robot2)
            : base(Stop, Stopped, Dispatcher, Robot1) {
            CreateRobot(2, Robot2);

            //hardware simulation starten
            DemoSimulator sim = new DemoSimulator();
            GetRefRobot2().EventStepExit += new OnStepExitEventHandler(sim.OnEventStepExitRobot2);
            sim.SetReferences((DemoBK)Robot2.GetBK());
            sim.Show();

        }
        protected override void CreateRobot(int Index, RobotInterface Robot) {
            switch (Index) {
                case 1:
                    if (m_EventStopThread == null) m_EventStopThread = new ManualResetEvent(false);
                    m_EventThreadStopped = new ManualResetEvent(false);
                    m_Robot1 = new DemoRobot1(Robot, m_EventStopThread, m_EventThreadStopped);
                    break;
                case 2:
                    if (m_EventStopThread == null) m_EventStopThread = new ManualResetEvent(false);
                    m_EventThreadStopped2 = new ManualResetEvent(false);
                    m_Robot2 = new DemoRobot2(Robot, m_EventStopThread, m_EventThreadStopped);
                    break;
                default:
                    break;
            }
        }
        protected override void InitAutomatic() {
            base.InitAutomatic();
            //ToDo: RobotThreads erzeugen
            m_EventStopThread.Reset();
            m_EventThreadStopped.Reset();
            m_EventThreadStopped2.Reset();
            m_EventThreadStopped.Reset();
            if (m_RobotThread1 == null ||
                m_RobotThread1.ThreadState == ThreadState.Stopped) {
                m_RobotThread1 = new Thread(new ThreadStart(m_Robot1.DoAutomatic));
                m_RobotThread1.Name = "DemoRobot1";
            }
            if (m_RobotThread2 == null ||
                m_RobotThread2.ThreadState == ThreadState.Stopped) {
                m_RobotThread2 = new Thread(new ThreadStart(m_Robot2.DoAutomatic));
                m_RobotThread2.Name = "DemoRobot2";
            }
        }
        protected override void PostAutomatic() {
            ;
        }
        protected override void InitCycle() {
            try {
                //ToDo: RobotThreads starten
                if ((m_RobotThread1.ThreadState &
                    (ThreadState.Stopped | ThreadState.Unstarted)) != 0) {
                    m_RobotThread1.Start();
                }
                // Thread.Sleep(1500);
                if ((m_RobotThread2.ThreadState &
                    (ThreadState.Stopped | ThreadState.Unstarted)) != 0) {
                    m_RobotThread2.Start();
                }
            } catch (SystemException e) {
                throw (e);
            } finally { }
        }
        protected override int Step(RobotMode Mode, int StepIndex) {
            int NextStep = StepIndex;
            Thread.Sleep(10);
            if (StepIndex == 0) {
                InitCycle();
                m_Robot1.RunOnce();
                m_Robot2.RunOnce();
                NextStep++;
            };
            if (StepIndex == 1) {//Todo: synchronize Robots by waiting for Ready
                if (m_Robot1.ReadyForCycle() && m_Robot2.ReadyForCycle()) {
                    NextStep = 0;
                } else if (m_Robot1.Aborted() || m_Robot2.Aborted()) {
                    Stop();
                } 
            };
            return NextStep;
        }

        public override void Stop() {
            base.Stop();
            m_Robot1.Stop();
            m_Robot2.Stop();
            m_EventStopThread.Set();
            int _RunningThreads = 2;
            while ((m_RobotThread1 != null && m_RobotThread1.IsAlive) ||
                   (m_RobotThread2 != null && m_RobotThread2.IsAlive)) {
                if (m_EventThreadStopped.WaitOne(0)) {
                  _RunningThreads--;
                  m_EventThreadStopped.Reset();
                }
                if (m_EventThreadStopped2.WaitOne(0)) {
                    _RunningThreads--;
                    m_EventThreadStopped2.Reset();
                }
                Application.DoEvents();
                if (_RunningThreads<=0) break; 
          }
                
            
        }
        public override void Abort() {
            TryAbort(m_RobotThread2);
            base.Abort();
        }
        public override bool IsRunning() {
            bool Running = IsRobotRunning(m_RobotThread1) ||
                IsRobotRunning(m_RobotThread2);
            return Running;
        }
        public override Robot GetRefRobot1() {
            return base.GetRefRobot1();
        }
        public virtual Robot GetRefRobot2() {
            return m_Robot2;
        }
        protected ManualResetEvent m_EventThreadStopped2=null;
        protected Robot m_Robot2;
        protected Thread m_RobotThread2;
    }
}
