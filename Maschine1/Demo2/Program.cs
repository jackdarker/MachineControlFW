using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WagoBK.Test;
using Maschine1;

namespace Demo2
{
    static class Program
    {
        public class MyApplicationContext : ApplicationContext
        {
            private int formCount;
            //private BKSim BKSim = new BKSim(new Demo.DemoBK.DemoChannels());
            private Main DemoApp;

            public MyApplicationContext()
            {
                formCount = 0;
                // Handle the ApplicationExit event to know when the application is exiting.
                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

                try
                {
                    DemoApp = new Main();
                }
                catch (Exception e)
                {
                    // Inform the user that an error occurred.
                    MessageBox.Show("An error occurred while attempting to show the application." +
                            "The error is:" + e.ToString());

                    // Exit the current thread instead of showing the windows.
                    ExitThread();
                }

                //BKSim.Closed += new EventHandler(OnFormClosed);
                //BKSim.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                //formCount++;
                DemoApp.Closed += new EventHandler(OnFormClosed);
                DemoApp.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                formCount++;
                //??BK.LoadConfig("");
                //??BK.Start();
                //BKSim.Show();
                DemoApp.Show();
            }

            private void OnApplicationExit(object sender, EventArgs e)
            {
                
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
        }
    }
}

