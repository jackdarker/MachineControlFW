using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WagoBK;

namespace BKDemo
{
    public partial class BKDemoApp : Form
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
        public BKDemoApp()
        {
            InitializeComponent();
        }
        public BKDemoApp(BKDemoBK BK)
            : this()
        {
            m_BK = BK;
            m_State = State.Unknown;
            m_BK.ConnectToEvent(new WagoBK.WagoSocketBase.OnDIChangeEventHandler(OnDIChangeEvent));
            wagoBKPane1.SetBK(m_BK);
        }
        private BKDemoBK m_BK;
        private State m_State;
        private State m_NextState;
        void OnDIChangeEvent(object sender, EventArgs e)
        {

            Console.WriteLine("DI changed");
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
         /*   if (m_BK.Ch().DI_SledgeInWP)
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
         */   return StateOK;
            
        }
        private bool SledgeOut()
        {
            bool StateOK = false;
            /*   if (m_BK.Ch().DI_SledgeInHP)
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
           */    return StateOK;
            
        }
        private bool IndexDown()
        {
            bool StateOK = false;
            /*   if (m_BK.Ch().DI_IndexInWP)
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
            */   return StateOK;
            
        }
        private bool IndexUp()
        {
            bool StateOK = false;
            /*    if (m_BK.Ch().DI_IndexInHP)
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
              */  return StateOK;
            
        }
        private bool InHP()
        {
            bool InHP = true;
            /*    InHP &= m_BK.Ch().DI_MainPressureReady;
                InHP &= m_BK.Ch().DI_SledgeInHP;
                InHP &= m_BK.Ch().DI_IndexInHP;
             */   return InHP;

        }
        private bool PowerOn()
        {
            bool StateOK = false;
            /*  if (m_BK.Ch().DI_MainPressureReady)
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
            */  return StateOK;
        }
        private bool PowerOff()
        {
            bool StateOK = false;
          /*  if (!m_BK.Ch().DI_MainPressureReady)
            {
                m_State = State.Unknown;
                StateOK = true;
            }
            else
            {
                //m_State = State.Unknown;
                //m_BK.Ch().DO_PCReady = false;
            }
           */ return StateOK;
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
        private void button2_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = (decimal)m_BK.Ch().CardDef511_2.GetFreq(0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_BK.Ch().CardDef511_2.SetFreq(0, (double)numericUpDown1.Value);
        }
    }
    static class Program
    {
        public class MyApplicationContext : ApplicationContext
        {
            private int formCount;
            private BKDemo.BKDemoBK BK = new BKDemo.BKDemoBK();
            //private DlgWagoBKControl WagoPane = new DlgWagoBKControl();
            //private WagoBK.Test.BKSim BKSim = new WagoBK.Test.BKSim(new BKDemo.BKDemoBK.BKDemoChannels());
            private BKDemo.BKDemoApp DemoApp;

            public MyApplicationContext()
            {
                formCount = 0;
                // Handle the ApplicationExit event to know when the application is exiting.
                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

                try
                {
                    DemoApp = new BKDemo.BKDemoApp(BK);
                }
                catch (Exception e)
                {
                    // Inform the user that an error occurred.
                    MessageBox.Show("An error occurred while attempting to show the application." +
                            "The error is:" + e.ToString());

                    // Exit the current thread instead of showing the windows.
                    ExitThread();
                }

                // Create both application forms and handle the Closed event to know when both forms are closed.
                //WagoPane.Closed += new EventHandler(OnFormClosed);
                //WagoPane.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                //formCount++;
               
                //BKSim.Closed += new EventHandler(OnFormClosed);
                //BKSim.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                //formCount++;
                DemoApp.Closed += new EventHandler(OnFormClosed);
                DemoApp.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                formCount++;
                //WagoPane.ConnectToBK(BK);
                BK.SetConfig("192.168.2.10", 502);
                //??BK.Start();
                //BKSim.Show();
                //WagoPane.Show();
                DemoApp.Show();
            }

            private void OnApplicationExit(object sender, EventArgs e)
            {
                BK.Stop(true);
            }
            private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
            {
            }

            private void OnFormClosed(object sender, EventArgs e)
            {
                // When a form is closed, decrement the count of open forms.
                // When the count gets to 0, exit the app by calling
                // ExitThread().
                formCount--;
                if (formCount == 0)
                {
                    ExitThread();
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyApplicationContext AppContext = new MyApplicationContext();

            Application.Run(AppContext);
            Console.WriteLine("Signaling threads to terminate...");
        }
    }
}
