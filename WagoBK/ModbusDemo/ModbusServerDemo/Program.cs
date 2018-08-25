using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace ModbusServerDemo
{
    static class Program
    {
        public class MyApplicationContext : ApplicationContext
        {
            private int formCount;
            private ModbusServerDemo m_ServerForm;
            private ModbusClientDemo m_ClientForm;

            public MyApplicationContext()
            {
                formCount = 0;
                // Handle the ApplicationExit event to know when the application is exiting.
                Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

                try
                {
                    Thread ServerThread = new Thread(new ThreadStart(StartServer));
                    ServerThread.Start();
                    
                    m_ClientForm = new ModbusClientDemo();
                }
                catch (Exception e)
                {
                    // Inform the user that an error occurred.
                    MessageBox.Show("An error occurred while attempting to show the application." +
                            "The error is:" + e.ToString());

                    // Exit the current thread instead of showing the windows.
                    ExitThread();
                }

                m_ClientForm.Closed += new EventHandler(OnFormClosed);
                m_ClientForm.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                formCount++;
                //m_ServerForm.Show();
                m_ClientForm.Show();
            }
            private void StartServer()
            {
                m_ServerForm = new ModbusServerDemo();
                m_ServerForm.Closed += new EventHandler(OnFormClosed);
                m_ServerForm.Closing += new System.ComponentModel.CancelEventHandler(OnFormClosing);
                formCount++;
                Application.Run(m_ServerForm);
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
            Console.WriteLine("Signaling threads to terminate...");
        }
    }
}
