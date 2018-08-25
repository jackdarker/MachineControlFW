using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace Maschine1
{
    public class RobotDispatcher : Robot
    {
        public static void TryAbort(Thread RobotThread)
        {
            if (RobotThread == null ||
                RobotThread.ThreadState == ThreadState.Stopped ||
                RobotThread.ThreadState == ThreadState.Aborted)
            {
                RobotThread.Abort();
            };
 
        }
        public RobotDispatcher(ManualResetEvent Stop, ManualResetEvent Stopped,
            RobotInterface Dispatcher,RobotInterface Robot1) : base(Dispatcher,Stop, Stopped)
        {
            CreateRobot(1, Robot1);
        }
        protected virtual void CreateRobot(int Index,RobotInterface Robot )
        {
            //ToDo: hier Robots erzeugen; eigene Robotklasse verwenden!
            if (m_EventStopThread == null) m_EventStopThread = new ManualResetEvent(false);
            m_EventThreadStopped = new ManualResetEvent(false);
            m_Robot1 = new Robot(Robot, m_EventStopThread, m_EventThreadStopped);
        }

        public virtual Robot GetRefRobot1() {
            return m_Robot1;
        }
        public override void Stop()   {
            base.Stop();
            m_Robot1.Stop();
            if (m_RobotThread1 != null && m_RobotThread1.IsAlive) {
                m_EventStopThread.Set();
                while (m_RobotThread1.IsAlive) {
                    // We cannot use here infinite wait because our thread
                    // makes syncronous calls to main form, this will cause deadlock.
                    // Instead of this we wait for event some appropriate time
                    // (and by the way give time to worker thread) and
                    // process events. These events may contain Invoke calls.
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { m_EventThreadStopped }), 100,true)) 
                        { break;  }
                    Application.DoEvents();
                }
            }

        }
        public virtual void Abort()
        {
            TryAbort(m_RobotThread1);
            base.Stop();
        }

        protected override void CycleStarted()
        {
           // m_Interface.GetModuleState().SetActiveMode(RobotMode.Automatic);
        }
        protected override void CycleStopped()
        {
           // m_Interface.GetModuleState().SetActiveMode(RobotMode.None);
        }
        public virtual bool IsRunning()
        {
            bool Running = IsRobotRunning(m_RobotThread1);
            return Running;
        }
        static public bool IsRobotRunning(Thread RobotThread)
        {
            bool Running = false;
            //ToDo: Robotstatus holen
            if (RobotThread == null ||
                RobotThread.ThreadState == ThreadState.Stopped)
            { }
            else
            {
                Running = true;
            }
            return Running;
        }

        /*??protected void UpdateModulDisplay()
        {
            if (m_Interface.GetModuleDisplay() != null && m_Interface.GetModuleDisplay().IsHandleCreated)
            {
                if (m_Interface.GetModuleDisplay().InvokeRequired)
                {
                    m_Interface.GetModuleDisplay().Invoke(
                        new ModuleDisplay.DelegateSetStatus(m_Interface.GetModuleDisplay().SetStatus), 
                        new System.Object[] { m_State.m_Step });
                }
                else
                {
                    m_Interface.GetModuleDisplay().SetStatus(m_State.m_Step);
                }
            }
        }

        private RobotState m_State;
        private RobotInterface m_Interface;*/
        
        protected Robot m_Robot1;
        protected Thread m_RobotThread1;
    }
}
