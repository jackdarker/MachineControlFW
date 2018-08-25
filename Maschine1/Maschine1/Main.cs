using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MachineFramework;
using WagoBK;
using WagoBK.Test;

namespace Maschine1
{
    public partial class Main : Form
    {

        [Serializable]
        public struct strProgramSettings
        {
            public string m_LastTypeName;
            public string m_LastUser;
        }
        protected class ProgramSettings
        {
            public void Load()
            {    
                Stream _stream = null;
                try
                {
                    _stream = new FileStream("Program.xml", FileMode.Open, FileAccess.Read, FileShare.None);
                    SoapFormatter formatter = new SoapFormatter();
                    m_ProgramSettings = (strProgramSettings)formatter.Deserialize(_stream);
                }
                catch (SerializationException e)
                {
                    throw (e);
                }

                finally
                {
                    if (_stream != null) _stream.Close();
                }
            }
            public void Save()
            {
                Stream _stream = new FileStream("Program.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                SoapFormatter formatter = new SoapFormatter();
                try
                {
                    formatter.Serialize(_stream, m_ProgramSettings);
                }
                catch (SerializationException e)
                {
                    throw (e);
                }
                finally
                {
                    _stream.Close();
                }
            }
            public strProgramSettings m_ProgramSettings = new strProgramSettings();
        }

        public Main()
        {
            m_ModuleState = new ModuleState();
            m_ModuleState.SetActiveMode(RobotMode.None);
            InitializeComponent();
            LoadSettings();
            InitControls();
            ConnectControls();
            CreateRobotDispatcher();
            WagoPane.Show();
            UIUpdate();  
        }
        private void SaveSettings()
        {
            //??
            m_Settings.m_ProgramSettings.m_LastTypeName = m_ModuleState.GetActiveType();
            m_Settings.Save();
        }
        private void LoadSettings()
        {
            //?? Sprache, letzter Typ, User
            UserManagement.UserManager.getUserManager.GetCurrentUser(); //force init of UserManager
            Translator.Language l = Translator.getTranslator.GetLanguage(); //force init of Translator
            m_Settings.Load();
            m_ModuleState.SetActiveType( m_Settings.m_ProgramSettings.m_LastTypeName);
        }
        private void InitControls()
        {
            //SoftKeys definieren, max 10 pro Menu
            MainSoftKeys.ButtonNavigation SoftKeyMenuDesc = new MainSoftKeys.ButtonNavigation();
            //Hauptmenu
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("AutomaticStop", "Automatik Stop","",MainSoftKeys.ButtonFunction.Call ));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("HomingStart", "Grundstellungs -fahrt", "", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("AutomaticStart", "Automatik Start","",MainSoftKeys.ButtonFunction.Call ));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("AutomaticStartOnce", "Automatik Einmal", "", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("SelectUser", "Benutzerwechsel", "", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("SelectType", "Typwechsel", "", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("SettingsType", "Typeditor", "", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("options", "Service...", "", MainSoftKeys.ButtonFunction.OpenSubMenu));
            //Optionen
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("", "<--","options",MainSoftKeys.ButtonFunction.ReturnSubMenu ));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("Language", "Sprache", "options", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("ManualMode", "Handbetrieb", "options", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("Drucker", "Drucker", "options", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("Kalibrierung", "Kalibrierung", "options", MainSoftKeys.ButtonFunction.Call));
            this.mainSoftKeys1.SetButtonNavigation(SoftKeyMenuDesc);

            stationDisplay1.SetTitle("Station1");
            stationDisplay2.SetTitle("Station2");
            modulDisplay1.ClearStatus();

            WagoPane.ConnectToBK(m_BK);
            m_BK.SetConfig("127.0.0.1",502);
            m_BK.Start();
        }
        private void mainSoftKeys1_ButtonClick(object sender, MachineFramework.MainSoftKeys.ButtonClickEventArgs e) {
            //ToDo: Kommandos hinzufügen
            string Command = e.Value;
            if (Command == "AutomaticStart") {
                RunRobots(RobotMode.Automatic);
            } else if (Command == "HomingStart") {
                RunRobotsOnce(RobotMode.Homing);
            } else if (Command == "AutomaticStartOnce") {
                RunRobotsOnce(RobotMode.Automatic);
            } else if (Command == "AutomaticStop") {
                StopRobots();
            } else if (Command == "SelectType") {
                ShowTypeSelector();
            } else if (Command == "SelectUser") {
                ShowUserDlg();
            } else if (Command == "SettingsType") {
                ShowTypeEditor();
            } else if (Command == "Language") {
                ShowLanguageDlg();
            }

        }
        private void ConnectControls()
        {
            //update UI if Modulstate changes
            m_ModuleState.EventUpdate += new ModuleState.OnUpdateEventHandler(OnUpdateStateEvent);
            Translator.getTranslator.EventRetranslate += new Translator.OnRetranslateEventHandler(OnRetranslateEvent);
            Retranslate();
        }
        private void CreateRobotDispatcher()
        {
            try
            {
                m_EventStopThread = new ManualResetEvent(false);
                m_EventThreadStopped = new ManualResetEvent(false);
                RobotInterface DispatcherInterface = new RobotInterface(null, modulDisplay1, null, null, 
                    m_ModuleState, null);
                RobotInterface Robot1Interface = new RobotInterface(stationDisplay1, null, null, null,
                    m_ModuleState, m_BK);
                RobotInterface Robot2Interface = new RobotInterface(stationDisplay2, modulDisplay1, resultTable1, partHistoryTable1, 
                    m_ModuleState, m_BK);
                m_RobotDispatcher = new Demo.DemoRobotDispatcher(m_EventStopThread,m_EventThreadStopped,DispatcherInterface, Robot1Interface, Robot2Interface);
                //ToDo: Anzeigeelemnte mit Ereignishandlern verbinden
                this.counterEditor1.ConnectEvent(m_RobotDispatcher.GetRefRobot2().GetRefCounter());
            }
            catch (SystemException e)
            {
                GUI.DlgError.HandleException(e);
            }        
        }
        private void StartRobotDispatcher()
        {
            //ToDo: RobotDispatcherThread erzeugen
            if (m_RobotDispThread == null ||
                m_RobotDispThread.ThreadState == ThreadState.Stopped)
            {
                m_RobotDispThread = new Thread(new ThreadStart(m_RobotDispatcher.DoAutomatic));
                m_RobotDispThread.Name = "RobotDispatcher";
            }
            //ToDo: RobotDispatcherThread starten
            if ((m_RobotDispThread.ThreadState &
                (ThreadState.Stopped | ThreadState.Unstarted)) != 0)
            {
                m_EventStopThread.Reset();
                m_EventThreadStopped.Reset();
                m_RobotDispThread.Start();
            }
        }
        private void RunRobots(RobotMode Mode)
        {
            m_ModuleState.SetActiveMode(Mode);
            StartRobotDispatcher();
            m_RobotDispatcher.Run();
            //m_RobotDispatcher.DoAutomatic();
        }
        private void RunRobotsOnce(RobotMode Mode)
        {
            m_ModuleState.SetActiveMode(Mode);
            StartRobotDispatcher();
            m_RobotDispatcher.RunOnce();
            //m_RobotDispatcher.DoAutomatic();
        }
        private void StopRobots()
        {
            m_RobotDispatcher.Stop(); //abort after Timeout??
        }
        private void AbortRobots()
        {
            m_RobotDispatcher.Abort();
        }
        private void ShowTypeEditor() {
            if (m_TypeEditor == null)
            {
                m_TypeEditor = new DlgTypeEditor();
            };
            m_TypeEditor.Show(this);
            m_TypeEditor=null;

        }
        private void ShowLanguageDlg() {
            if (m_DlgLanguage == null) {
                m_DlgLanguage = new Localization.DlgTranslatorSelection();
            };
            m_DlgLanguage.Show(this);
            m_DlgLanguage = null;

        }
        private void ShowUserDlg() {
            if (m_DlgUsers == null) {
                m_DlgUsers = new UserManagement.DlgUserLogin();
            };
            m_DlgUsers.Show(this);
            m_DlgUsers = null;

        }
        private void ShowTypeSelector()
        {
            if (m_ModuleState.GetActiveMode() != RobotMode.None)
            {//kein Typwechsel wenn in automatic
            }
            else 
            {
                if (m_TypeSelector == null)
                {
                    m_TypeSelector = new DlgTypeSelector();
                };
                m_TypeSelector.SetPreselectedType(m_ModuleState.GetActiveType());
                m_TypeSelector.ShowDialog(this);
                m_ModuleState.SetActiveType( m_TypeSelector.GetSelectedType());
                m_TypeSelector = null; 
            }
        }

        private void OnUpdateStateEvent(object sender, EventArgs e)
        {
            if (this != null && this.IsHandleCreated)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateUIUpdate(UIUpdate));
                }
                else
                {
                    UIUpdate();
                }
            }

        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            m_BK.Stop(true);
        }
        public delegate void DelegateUIUpdate();
        private void UIUpdate()
        {
            this.ProdType.Text = m_ModuleState.GetActiveType();
            this.CurrMode.Text = m_ModuleState.GetActiveMode().ToString();
            this.CurrUser.Text = "";
        }
        private void Retranslate() {

            this.tabMain.Text = Translator.getTranslator.Txt(this.tabMain.Name,"Main");
            this.tabResults.Text = Translator.getTranslator.Txt(this.tabResults.Name, "Main");
            this.tabCounters.Text = Translator.getTranslator.Txt(this.tabCounters.Name, "Main");
            this.tabInfo.Text = Translator.getTranslator.Txt(this.tabInfo.Name, "Main");
            ;//??
        }
        private void OnRetranslateEvent(object sender, EventArgs e) {
            if (this != null && this.IsHandleCreated) {
                if (this.InvokeRequired) {
                    this.Invoke(new Translator.DelegateRetranslate(Retranslate));
                } else {
                    Retranslate();
                }
            }
        }
        Demo.DemoBK m_BK = new Demo.DemoBK();
        Demo.DemoRobotDispatcher m_RobotDispatcher;
        Thread m_RobotDispThread;
        
        DlgWagoBKControl WagoPane = new DlgWagoBKControl();
        DlgTypeEditor m_TypeEditor;
        Localization.DlgTranslatorSelection m_DlgLanguage;
        UserManagement.DlgUserLogin m_DlgUsers;
        DlgTypeSelector m_TypeSelector;
        ModuleState m_ModuleState;
        ProgramSettings m_Settings = new ProgramSettings();
        protected ManualResetEvent m_EventStopThread;
        protected ManualResetEvent m_EventThreadStopped;

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Boolean _exit = true;
            if (m_RobotDispatcher.IsRunning())
            {
                if (MessageBox.Show("Automatik noch aktiv. abbrechen?", "Warnung", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    _exit = true;
                    StopRobots();   //try to stop normally first
                    /*Thread.Sleep(2000);
                    AbortRobots();        //then try abort
                   /* int _maxTime = 5000;
                    while (m_RobotDispatcher.IsRunning() ) {    //??warten blockiert den Abbruch der Robots?
                        Thread.Sleep(100);
                        _maxTime -= 100;
                    }*/

                } else {
                    _exit = false;
                    e.Cancel = true;
                }
            }
            if (_exit) {
                m_BK.Stop(true);
                SaveSettings();
            }
        }
        

    }
}
