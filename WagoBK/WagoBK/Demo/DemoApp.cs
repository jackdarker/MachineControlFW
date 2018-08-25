using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WagoBK;

namespace Demo
{
    public partial class DemoApp : Form
    {
        enum State 
        {
            Unknown=0,
            PowerOK,
            HP,
            SledgeOut,
            SledgeIn,
            IndexDown,
            IndexUp
        }
        public DemoApp()
        {
            InitializeComponent();
        }
        
        public DemoApp(DemoBK BK):this()
        {
            m_BK = BK;
            m_State = State.Unknown;
            m_BK.ConnectToEvent(new WagoBK.WagoSocketBase.OnDIChangeEventHandler(OnDIChangeEvent));
        }
        private DemoBK m_BK;
        private State m_State;
        private State m_NextState;
        void OnDIChangeEvent(object sender, EventArgs e)
        {

            Console.WriteLine("changed");
            DoCycle();
            /*if (this != null && this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new CounterTable.DelegateUpdateTable(UpdateTable));
                }
                else
                {
                    UpdateTable();
                }
            }*/

        }

        //??include this in robot
        private void DoCycle()
        {
            if (PowerOff())
            {
                PowerOn();
                m_NextState = State.PowerOK;
            }
            else if (PowerOn() && m_NextState == State.PowerOK)
            {
                SledgeOut();
                m_NextState = State.SledgeOut;
            }
            else if (SledgeOut() && m_NextState == State.SledgeOut)
            {
                SledgeIn();
                m_NextState = State.SledgeIn;
            }
            else if (SledgeIn() && m_NextState == State.SledgeIn)
            {
                IndexDown();
                m_NextState = State.IndexDown;
            }
            else if (IndexDown() && m_NextState == State.IndexDown)
            {
                IndexUp();
                m_NextState = State.IndexUp;
            }
            else if (IndexUp() && m_NextState == State.IndexUp)
            {
                SledgeOut();
                m_NextState = State.SledgeOut;
            } 

            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateUpdateState(UpdateState));
            }
            else
            {
                UpdateState();
            }
        }
        public delegate void DelegateUpdateState();
        private void UpdateState()
        {
            label1.Text = m_State.ToString();
        }
        private bool SledgeIn()
        {
            bool StateOK = false;
            if (m_BK.Ch().DI_SledgeInWP)
            {
                m_State = State.SledgeIn;
                StateOK = true;
            }
            else
            {
                if (m_BK.Ch().DI_IndexInHP)
                {
                    m_BK.Ch().DO_SledgeToWP = true;
                }
            }
            return StateOK;
            
        }
        private bool SledgeOut()
        {
            bool StateOK = false;
            if (m_BK.Ch().DI_SledgeInHP)
            {
                m_State = State.SledgeOut;
                StateOK = true;
            }
            else
            {
                if (m_BK.Ch().DI_IndexInHP)
                {
                    m_BK.Ch().DO_SledgeToWP = false;
                }
            }
            return StateOK;
            
        }
        private bool IndexDown()
        {
            bool StateOK = false;
            if (m_BK.Ch().DI_IndexInWP)
            {
                m_State = State.IndexDown;
                StateOK = true;
            }
            else
            {
                if (m_BK.Ch().DI_SledgeInWP)
                {
                    m_BK.Ch().DO_IndexToWP = true;
                }
            }
            return StateOK;
            
        }
        private bool IndexUp()
        {
            bool StateOK = false;
            if (m_BK.Ch().DI_IndexInHP)
            {
                m_State = State.IndexUp;
                StateOK = true;
            }
            else
            {
                if (true)
                {
                    m_BK.Ch().DO_IndexToWP = false;
                }
            }
            return StateOK;
            
        }
        private bool InHP()
        {
            bool InHP = true;
            InHP &= m_BK.Ch().DI_MainPressureReady;
            InHP &= m_BK.Ch().DI_SledgeInHP;
            InHP &= m_BK.Ch().DI_IndexInHP;
            return InHP;

        }
        private bool PowerOn()
        {
            bool StateOK = false;
            if (m_BK.Ch().DI_MainPressureReady)
            {
                m_State = State.PowerOK;
                StateOK = true;
            }
            else
            {
                m_State = State.Unknown;
                m_BK.Ch().DO_PCReady = false;
                m_BK.Ch().DO_IndexToWP= false;
                m_BK.Ch().DO_SledgeToWP = false;
                Thread.Sleep(1000);
                m_BK.Ch().DO_PCReady = true;
            }
            return StateOK;
        }
        private bool PowerOff()
        {
            bool StateOK = false;
            if (!m_BK.Ch().DI_MainPressureReady)
            {
                m_State = State.Unknown;
                StateOK = true;
            }
            else
            {
                //m_State = State.Unknown;
                //m_BK.Ch().DO_PCReady = false;
            }
            return StateOK;
        }
        private void btSledgeOut_Click(object sender, EventArgs e)
        {
            SledgeOut();
        }
        private void btPowerOn_Click(object sender, EventArgs e)
        {
            PowerOn();
        }

        private void btSledgeIn_Click(object sender, EventArgs e)
        {
            SledgeIn();
        }

        private void btIndexDown_Click(object sender, EventArgs e)
        {
            IndexDown();
        }

        private void btIndexUp_Click(object sender, EventArgs e)
        {
            IndexUp();
        }
    }
}
