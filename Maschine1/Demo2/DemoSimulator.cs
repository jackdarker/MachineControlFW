using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Demo2 {
    public partial class DemoSimulator : Form {
        public DemoSimulator() {
            InitializeComponent();
            m_Timer = new System.Threading.Timer(new TimerCallback(Simulate), null, 1000, m_StepDelay);
        }

        public void SetReferences(DemoBK BK) {
            m_BK = BK;
        }
        private void button1_Click(object sender, EventArgs e) {
            _SimulateAuto = this.radioButton2.Checked;
            _SimulateHP = this.radioButton3.Checked;
        }
        private int m_StepDelay = 200;
        private int m_StepTimer = 0;
        private int m_StepStart = 0;

        private void Simulate(Object state) {
            
            m_StepStart = System.Environment.TickCount;
            m_StepTimer -=  m_StepDelay;
            if (m_StepTimer < 0) m_StepTimer = 0;
            if (m_StepTimer == 0) state = 1;
            if (!_SimulateAuto && !_SimulateHP) return;
            if (m_BK.Ch().DI_MainPressureReady == false) {
                m_BK.Ch().DIChannels["MainPressureReady"].Set(true);
                m_BK.Ch().DIChannels["SledgeInHP"].Set(true);
                m_BK.Ch().DIChannels["IndexInHP"].Set(true);
                m_BK.Ch().DIChannels["GripperInHP"].Set(true);
            }

            if (m_BK.Ch().DO_SledgeToWP && m_BK.Ch().DI_SledgeInHP) {
                m_BK.Ch().DIChannels["SledgeInHP"].Set(false);
                m_StepTimer = 800;
                
            }else if (m_BK.Ch().DO_SledgeToWP && state!=null) {   //delayed call
                m_BK.Ch().DIChannels["SledgeInWP"].Set(true);
            }
            if (!m_BK.Ch().DO_SledgeToWP && m_BK.Ch().DI_SledgeInWP) {
                m_BK.Ch().DIChannels["SledgeInWP"].Set(false);
                m_StepTimer = 600;
                
            } else if (!m_BK.Ch().DO_SledgeToWP && state != null) {
                m_BK.Ch().DIChannels["SledgeInHP"].Set(true);
            }
            if (m_BK.Ch().DO_IndexToWP && m_BK.Ch().DI_IndexInHP) {
                m_BK.Ch().DIChannels["IndexInHP"].Set(false);
                m_StepTimer = 400;
                
            } else if (m_BK.Ch().DO_IndexToWP && state != null) {
                m_BK.Ch().DIChannels["IndexInWP"].Set(true);
            }
            if (!m_BK.Ch().DO_IndexToWP && m_BK.Ch().DI_IndexInWP) {
                m_BK.Ch().DIChannels["IndexInWP"].Set(false);
                m_StepTimer = 400;
                
            } else if (!m_BK.Ch().DO_IndexToWP && state != null) {   //delayed call
                m_BK.Ch().DIChannels["IndexInHP"].Set(true);
            }
            if (m_BK.Ch().DO_CloseGripper && m_BK.Ch().DI_GripperInHP) {
                m_BK.Ch().DIChannels["GripperInHP"].Set(false);
                m_StepTimer = 400;
                
            } else if (m_BK.Ch().DO_CloseGripper && state != null) {   //delayed call
                m_BK.Ch().DIChannels["GripperInWP"].Set(true);     
            }
            if (!m_BK.Ch().DO_CloseGripper && m_BK.Ch().DI_GripperInWP) {
                m_BK.Ch().DIChannels["GripperInWP"].Set(false);
                m_StepTimer = 400;
                
            } else if (!m_BK.Ch().DO_CloseGripper && state != null) {   //delayed call
                m_BK.Ch().DIChannels["GripperInHP"].Set(true);
            }
        }
        /*
        private void Simulate(Object state) {
            m_StepStart = System.Environment.TickCount;
            if (!_SimulateAuto && !_SimulateHP) return;
            if (m_BK.Ch().DI_MainPressureReady == false) {
                m_BK.Ch().DIChannels["MainPressureReady"].Set(true);
                m_BK.Ch().DIChannels["SledgeInHP"].Set(true);
                m_BK.Ch().DIChannels["IndexInHP"].Set(true);
                m_BK.Ch().DIChannels["GripperInHP"].Set(true);
            }

            if (m_BK.Ch().DO_SledgeToWP && m_BK.Ch().DI_SledgeInHP) {
                m_BK.Ch().DIChannels["SledgeInHP"].Set(false);
                 //Thread.Sleep(500);
                m_BK.Ch().DIChannels["SledgeInWP"].Set(true);
            }
            if (!m_BK.Ch().DO_SledgeToWP && m_BK.Ch().DI_SledgeInWP) {
                m_BK.Ch().DIChannels["SledgeInWP"].Set(false);
                //Thread.Sleep(500);
                m_BK.Ch().DIChannels["SledgeInHP"].Set(true);
            }
            if (m_BK.Ch().DO_IndexToWP && m_BK.Ch().DI_IndexInHP) {
                m_BK.Ch().DIChannels["IndexInHP"].Set(false);
                //Thread.Sleep(500);
                m_BK.Ch().DIChannels["IndexInWP"].Set(true);
            }
            if (!m_BK.Ch().DO_IndexToWP && m_BK.Ch().DI_IndexInWP) {
                m_BK.Ch().DIChannels["IndexInWP"].Set(false);
                //Thread.Sleep(500);
                m_BK.Ch().DIChannels["IndexInHP"].Set(true);
            }
            if (m_BK.Ch().DO_CloseGripper && m_BK.Ch().DI_GripperInHP) {
                m_BK.Ch().DIChannels["GripperInHP"].Set(false);
                //Thread.Sleep(500);
                m_BK.Ch().DIChannels["GripperInWP"].Set(true);
            }
            if (!m_BK.Ch().DO_CloseGripper && m_BK.Ch().DI_GripperInWP) {
                m_BK.Ch().DIChannels["GripperInWP"].Set(false);
                //Thread.Sleep(500);
                m_BK.Ch().DIChannels["GripperInHP"].Set(true);
            }
        }*/
        public delegate void DelegateOnEventStepExitRobot2(object sender,EventArgs e);
        public void OnEventStepExitRobot2(object sender, EventArgs e) {
            if (this.InvokeRequired) {
                this.Invoke(new DelegateOnEventStepExitRobot2(OnEventStepExitRobot2), new System.Object[] { sender, e });
            } else {

             // Simulate(null);

            }
        }
        System.Threading.Timer m_Timer=null;
        DemoBK m_BK = null;
        Boolean _SimulateAuto = false;
        Boolean _SimulateHP = false;

    }
}
