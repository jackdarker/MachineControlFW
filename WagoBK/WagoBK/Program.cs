using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WagoBK
{
    static class Program
    {
        public class MyApplicationContext : ApplicationContext
        {
            private int formCount;
            private Demo.DemoBK BK = new Demo.DemoBK();
            //private WagoBKBase BK = new WagoBKBase();
            private DlgWagoBKControl WagoPane = new DlgWagoBKControl();
            private WagoBK.Test.BKSim BKSim = new WagoBK.Test.BKSim(new Demo.DemoBK.DemoChannels());
            private Demo.DemoApp DemoApp;

            public MyApplicationContext()
            {
                formCount = 0;
                // Handle the ApplicationExit event to know when the application is exiting.
                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

                try
                {
                    DemoApp = new Demo.DemoApp(BK);
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
                WagoPane.Closed += new EventHandler(OnFormClosed);
                WagoPane.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                formCount++;
                BKSim.Closed += new EventHandler(OnFormClosed);
                BKSim.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                formCount++;
                DemoApp.Closed += new EventHandler(OnFormClosed);
                DemoApp.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                formCount++;
                WagoPane.ConnectToBK(BK);
                BK.SetConfig("192.168.2.10", 502);
                BK.Start();
                BKSim.Show();
                WagoPane.Show();
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
            MyApplicationContext AppContext=new MyApplicationContext();
            
            Application.Run(AppContext );
            Console.WriteLine("Signaling threads to terminate...");
            

            
        }
    }
}
