using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WagoBK;
using WagoBK.Test;
using Maschine1.Core;
using Maschine1;
using Maschine1.GUI;
using Maschine1.Localization;
using Maschine1.UserManagement;
using Maschine1.FileIO;

namespace Demo2
{
    public partial class Main : Form   {

        protected class ProgramSettings : Maschine1.Core.ISerializable    {
            public String m_LastTypeName = "";

            #region Save/Load
            private static string s_UserFile = "Program.xml";
            private static string s_UserFilePath = AppFolders.getAppFolders.AppDataDir.FullName+"\\"+s_UserFile;
            public void WriteToSerializer(SerializerBase Stream) {
                Stream.WriteElementStart(s_UserFile);
                Stream.WriteData("LastType",m_LastTypeName);
                
                Stream.WriteElementEnd(s_UserFile);
            }
            public void ReadFromSerializer(SerializerBase Stream) {
                string NodeGroup;
                int StartNodeLevel = 0, CurrNodeLevel = 0;
                do {
                    NodeGroup = Stream.GetNodeName();
                    CurrNodeLevel = Stream.GetNodeLevel();
                    if (CurrNodeLevel < StartNodeLevel) { break; }
                    if (Stream.GetNodeType() != SerializerBase.NodeType.NodeEnd) {
                        if (NodeGroup == s_UserFile) {
                            m_LastTypeName = Stream.ReadAsString("LastType");

                        } else if (NodeGroup == SerializerXML.FieldName.SerializerDocName.ToString()) {
                            if (NodeGroup != "UserDat")
                                throw new Exception(SerializerXML.FieldName.SerializerDocName.ToString() + " unknown");
                        }
                    }

                } while (Stream.ReadNext());
            }
            public void Save() {
                SerializerXML _stream = null;
                try {
                    _stream = new SerializerXML("UserDat", "1.0.0.0");
                    _stream.OpenOutputStream(s_UserFilePath);
                    _stream.WriteElementStart("Application");
                    WriteToSerializer(_stream);
                    _stream.WriteElementEnd("Application");
                    _stream.CloseOutputStream();
                    _stream = null;
                } catch (Exception e) {
                    DlgError.HandleException(e);
                } finally {
                    if (_stream != null) _stream.CloseOutputStream();
                }
            }
            public void Load() {
                string DocType = string.Empty;
                SerializerXML _stream = null;
                try {
                    _stream = new SerializerXML("UserDat", "1.0.0.0");
                    _stream.OpenInputStream(s_UserFilePath);
                    if (_stream.GetDetectedSerializerName() != "UserDat")
                        throw new FormatException("");
                    string NodeGroup;
                    do {
                        NodeGroup = _stream.GetNodeName();
                        if (_stream.GetNodeType() != SerializerBase.NodeType.NodeEnd) {
                            if (NodeGroup == "Application") {
                                ReadFromSerializer(_stream);
                            }
                        }
                    } while (_stream.ReadNext());

                    _stream.CloseInputStream();
                    _stream = null;
                } catch (Exception e) {
                    DlgError.HandleException(e);
                } finally {
                    if (_stream != null) _stream.CloseInputStream();
                }
            }
            #endregion
           /* public void Load()
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
            }*/
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
        private void SaveSettings() {
            //??
            m_Settings.m_LastTypeName = m_ModuleState.GetActiveType();
            m_Settings.Save();
        }
        private void LoadSettings()
        {
            //?? Sprache, letzter Typ, User
            UserManager.getUserManager.GetCurrentUser(); //force init of UserManager
            Translator.Language l = Translator.getTranslator.GetLanguage(); //force init of Translator
            m_Settings.Load();
            m_ModuleState.SetActiveType( m_Settings.m_LastTypeName);
            UserManager.getUserManager.LoginUser("user", "user");
        }

        private void InitControls() {
            //SoftKeys definieren, max 10 pro Menu
            MainSoftKeys.ButtonNavigation SoftKeyMenuDesc = new MainSoftKeys.ButtonNavigation();
            //??mainSoftKeys1ButtonConfig. = new MainSoftKeys.ButtonConfig.EnableItems(EnableMainSoftKeys);
            //Hauptmenu
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("AutomaticStop", "Automatic Stop","",MainSoftKeys.ButtonFunction.Call ));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("HomingStart", "Homing", "", MainSoftKeys.ButtonFunction.Call,EnableMainSoftKeys));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("AutomaticStart", "Automatic Start", "", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("AutomaticStartOnce", "Automatic Once", "", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("SelectUser", "switch User", "", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("SelectType", "switch Type", "", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("SettingsType", "edit Type", "", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("trace", "Traceability", "", MainSoftKeys.ButtonFunction.OpenSubMenu));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("options", "Service...", "", MainSoftKeys.ButtonFunction.OpenSubMenu));
            //Optionen
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("", "<--","options",MainSoftKeys.ButtonFunction.ReturnSubMenu ));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("Language", "Language", "options", MainSoftKeys.ButtonFunction.Call));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("ManualMode", "Manual control", "options", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("LogHistory", "Log-History", "options", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));
            //OPC&Trace
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("", "<--", "trace", MainSoftKeys.ButtonFunction.ReturnSubMenu));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("PLC1", "PLC1", "trace", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));
            SoftKeyMenuDesc.Add(new MainSoftKeys.ButtonConfig("Trace1", "Trace1", "trace", MainSoftKeys.ButtonFunction.Call, EnableMainSoftKeys));

            this.mainSoftKeys1.SetButtonNavigation(SoftKeyMenuDesc);

            modulDisplay1.ClearStatus();

            
        }
        private bool EnableMainSoftKeys(MainSoftKeys.ButtonConfig BtCfg) {
            User.EnuUserRole _Role = UserManager.getUserManager.GetCurrentUser().Role;
            string Function = BtCfg.GetValue() ;
            if (_Role < User.EnuUserRole.User &&
                (Function == "SettingsType" ||
                Function == "HomingStart" ||
                Function == "AutomaticStart" ||
                Function == "AutomaticStartOnce" ||
                Function == "LogHistory"
                )) return false;
            if (_Role < User.EnuUserRole.Supervisor &&
                (
                Function == "ManualMode" ||
                Function == "PLC1" ||
                Function == "Trace1"
                )) return false;
            return true;
        }
        private void mainSoftKeys1_ButtonClick(object sender, MainSoftKeys.ButtonClickEventArgs e) {
            //ToDo: Kommandos von Softkeys hinzufügen
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
            } else if (Command == "LogHistory") {
                ShowLogDlg();
            }

        }
        private void ConnectControls(){
            m_ModuleState.EventUpdate += new ModuleState.OnUpdateEventHandler(OnUpdateStateEvent);
            Translator.getTranslator.EventRetranslate += new Translator.OnRetranslateEventHandler(OnRetranslateEvent);
            UserManager.getUserManager.EventUserChanged += new UserManager.OnUserChangedEventHandler(OnUserChangedEvent);
            Retranslate();
            bool _loggedIn= UserManager.getUserManager.LoginUser("user", "xxx");
        }
        private void CreateRobotDispatcher()
        {
            try
            {
                WagoPane.ConnectToBK(m_BK); //Todo: Deviceinit wo?
                m_BK.SetConfig("127.0.0.1",502);
                m_BK.Start();
                m_EventStopThread = new ManualResetEvent(false);
                m_EventThreadStopped = new ManualResetEvent(false);
                RobotInterface DispatcherInterface = new RobotInterface(null, modulDisplay1, null, null, 
                    m_ModuleState, null);
                RobotInterface Robot1Interface = new RobotInterface(stationDisplay1, null, null, null,
                    m_ModuleState, m_BK);
                RobotInterface Robot2Interface = new RobotInterface(stationDisplay2, modulDisplay1, resultTable1, partHistoryTable1, 
                    m_ModuleState, m_BK);

                m_RobotDispatcher = new DemoRobotDispatcher(m_EventStopThread,m_EventThreadStopped,DispatcherInterface, Robot1Interface, Robot2Interface);
                
                //ToDo: Anzeigeelemnte mit Ereignishandlern verbinden
                this.counterEditor1.ConnectEvent(m_RobotDispatcher.GetRefRobot2().GetRefCounter());
            }
            catch (SystemException e)
            {
                DlgError.HandleException(e);
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
            m_ModuleState.SetActiveMode(RobotMode.None);
        }
        private void AbortRobots()
        {
            m_RobotDispatcher.Abort();
        }
        private void ShowTypeEditor() {
            if (m_TypeEditor == null)
            {
                m_TypeEditor = new DemoDlgTypeParamEditor();
            };
            m_TypeEditor.Show(this);
            m_TypeEditor=null;

        }
        private void ShowLanguageDlg() {
            if (m_DlgLanguage == null) {
                m_DlgLanguage = new DlgTranslatorSelection();
            };
            m_DlgLanguage.Show(this);
            m_DlgLanguage = null;

        }
        private void ShowLogDlg() {
            if (m_DlgLogBrowser == null) {
                m_DlgLogBrowser = new DlgLogBrowser();
            };
            m_DlgLogBrowser.Show(this);
            m_DlgLogBrowser = null;
        }
        private void ShowUserDlg() {
            if (m_DlgUsers == null) {
                m_DlgUsers = new DlgUserLogin();
            };
            m_DlgUsers.Show(this);
            m_DlgUsers = null;

        }
        private void ShowTypeSelector()  {
            if (m_ModuleState.GetActiveMode() != RobotMode.None)  {
                //kein Typwechsel wenn in automatic
            } else   {
                if (m_TypeSelector == null) {
                    m_TypeSelector = new DlgTypeSelector();
                };
                m_TypeSelector.SetPreselectedType(m_ModuleState.GetActiveType());
                m_TypeSelector.ShowDialog(this);
                m_ModuleState.SetActiveType( m_TypeSelector.GetSelectedType());
                m_TypeSelector = null; 
            }
        }

        private void OnUpdateStateEvent(object sender, EventArgs e)       {
            if (this != null && this.IsHandleCreated)   {
                if (this.InvokeRequired)   {
                    this.Invoke(new DelegateUIUpdate(UIUpdate));
                } else {
                    UIUpdate();
                }
            }

        }
        private void OnApplicationExit(object sender, EventArgs e)  {
            m_BK.Stop(true);
        }
        public delegate void DelegateUIUpdate();
        private void UIUpdate() {
            this.ProdType.Text = m_ModuleState.GetActiveType();
            this.CurrMode.Text = m_ModuleState.GetActiveMode().ToString();
            UserChanged();
        }
        private void Retranslate() {

            this.tabMain.Text = Translator.Tr(this.tabMain.Name, "Main");
            this.tabResults.Text = Translator.Tr(this.tabResults.Name, "Main");
            this.tabCounters.Text = Translator.Tr(this.tabCounters.Name, "Main");
            this.tabInfo.Text = Translator.Tr(this.tabInfo.Name, "Main");
            stationDisplay1.SetTitle(Translator.Tr("InputStation",""));
            stationDisplay2.SetTitle(Translator.Tr("Classification", ""));
            txtEquipName.Text = Translator.Tr("Classification", "");
            this.Text = txtEquipName.Text;
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
        private void UserChanged() {
            this.CurrUser.Text = UserManager.getUserManager.GetCurrentUser().Name;
        }
        private void OnUserChangedEvent(object sender, EventArgs e) {
            if (this != null && this.IsHandleCreated) {
                if (this.InvokeRequired) {
                    this.Invoke(new UserManager.DelegateUserChanged(UserChanged));
                } else {
                    UserChanged();
                }
            }
        }
        DemoBK m_BK = new DemoBK();
        DemoRobotDispatcher m_RobotDispatcher;
        Thread m_RobotDispThread;
        
        DlgWagoBKControl WagoPane = new DlgWagoBKControl();
        DlgTypeEditor m_TypeEditor;
        DlgTranslatorSelection m_DlgLanguage;
        DlgLogBrowser m_DlgLogBrowser;
        DlgUserLogin m_DlgUsers;
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
