using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml;
using System.Xml.Serialization;
using Maschine1.FileIO;

namespace Maschine1
{
    
    public class Translator
    {
        public class Language {
            public Language() {
                Reloadlookup();
            }
            public Language(string ProgName, string DisplayName) {
                m_DisplayName = DisplayName;
                m_ProgName = ProgName;
                Reloadlookup();
            }
            public string ProgName {
                get {
                    return (m_ProgName);
                }
            }
            public string DisplayName {
                get {
                    return (m_DisplayName);
                }
            }
            /// <summary>
            /// lädt Übersetzungsliste aus Datei ,"Tr_"+ProgName.txt ??
            /// </summary>
            public void Reloadlookup() {
                if (m_Lookup==null) {
                    m_Lookup = new TranslationLookup();
                    Load();
                }
            }
            public string TrText(String Text) {
                string _ret = m_Lookup[Text];             
                if (m_Lookup.modified) { Save(); }
                return _ret;
            }
            private void Save() {
                Stream _stream = new FileStream(AppFolders.getAppFolders.AppDataDir.FullName + "\\Setup\\Localization\\TR_" + m_ProgName + ".xml", 
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                try {
                XmlSerializer formatter = new XmlSerializer(typeof(TranslationLookup));
                   formatter.Serialize(_stream, m_Lookup);
                } catch (Exception e) {
                    throw (e);
                } finally {
                    _stream.Close();
                }
            }
            private void Load() {
                Stream _stream= null;
                XmlSerializer formatter = new XmlSerializer(typeof(TranslationLookup));
                try {
                    _stream = new FileStream(AppFolders.getAppFolders.AppDataDir.FullName + "\\Setup\\Localization\\TR_" + m_ProgName + ".xml", 
                        FileMode.Open, FileAccess.Read, FileShare.None);
                    m_Lookup = (TranslationLookup)formatter.Deserialize(_stream);
                } catch (Exception e) {
                    throw (e);
                } finally {
                    _stream.Close();
                }
            }
            private string m_DisplayName = "default";
            private string m_ProgName = "default";
            private TranslationLookup m_Lookup;
        }
        /// <summary>
        /// Liste von Übersetzungstexten für eine Sprache
        /// </summary>
        public class LanguageCollection : DictionaryBase {
            public LanguageCollection() {
            }
            public Language this[String key] {
                get {
                    return ((Language)Dictionary[key]);
                }
                set {
                    Dictionary[key] = value;
                }
            }
            public ICollection Keys {
                get {
                    return (Dictionary.Keys);
                }
            }
            public ICollection Values {
                get {
                    return (Dictionary.Values);
                }
            }
            public void Add(Language value) {
                Dictionary.Add(value.ProgName, value);
            }

            public bool Contains(String key) {
                return (Dictionary.Contains(key));
            }

            public void Remove(String key) {
                Dictionary.Remove(key);
            }
        }
        public delegate void DelegateRetranslate();
        public event OnRetranslateEventHandler EventRetranslate;
        public delegate void OnRetranslateEventHandler(object sender, EventArgs e);
        protected virtual void OnRetranslate() {
            OnRetranslateEventHandler handler = EventRetranslate;
            EventArgs e = new EventArgs();
            if (handler != null) {
                handler(this, e);
            }
        }
        /*! \page Translator Übersetzung in UI-Code integrieren
         * \code

        //1) Innerhalb des UI-Konstruktors nach Erzeugung der Bedienelemente auf Retranslate-Event registrieren
            Translator.getTranslator.EventRetranslate += new Translator.OnRetranslateEventHandler(OnRetranslateEvent);
        //2) Funktion definieren in der die Bedienelement-Text übersetzt werden und Invoke-Funktion anlegen
        private void Retranslate()
        {
            this.ProdType.Text = m_ModuleState.GetActiveType();
            this.CurrMode.Text = m_ModuleState.GetActiveMode().ToString();
            this.CurrUser.Text = "";
        }
        private void OnRetranslateEvent(object sender, EventArgs e)
        {
            if (this != null && this.IsHandleCreated){
                if (this.InvokeRequired){
                    this.Invoke(new Translator.DelegateRetranslate(Retranslate));
                }
                else{
                    Retranslate();
                }
            }
        }
         * \endcode */

        private static readonly Translator s_Translator = new Translator();
        public static Translator getTranslator{
                get {
                    return s_Translator;
                }
        }
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
        static Translator() { }
        private Translator() {
            Init();
        }
        
        /// <summary>
        /// über diese Funktion kann ein Text übersetzt werden
        /// statischer Wrapper von Txt(...)
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public static string Tr(string Text, string Context) {
            return Translator.getTranslator.Txt(Text, Context);
        }
        /// <summary>
        /// über diese Funktion kann ein Text übersetzt werden
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Context"></param>
        /// <returns></returns>
        public string Txt(string Text, string Context) {
            return m_CurrLanguage.TrText(Text);
        }

        public Language GetLanguage() {
            return m_CurrLanguage;
        }
        public ICollection GetAvailableLanguages() {
            return m_Languages.Keys;
        }
        //globale Umschaltung der Sprache, löst Retranslate-Event aus
        public void SetLanguage(string Language) {
            if (m_Languages.Contains(Language)) {
                m_CurrLanguage = m_Languages[Language];
                OnRetranslate();
                SaveLookup();
            } else {
                throw new KeyNotFoundException("invalid language-key");
            }
        }
        private TranslatorSettings m_Settings;
        private void Init(){
            m_Settings = new TranslatorSettings();
            m_Languages = new LanguageCollection();
            m_CurrLanguage = new Language();
            LoadLookup();
            
        }
        private void LoadLookup() {
            m_Settings.Load();
            foreach (string s in m_Settings.AvailableLanguages) {
                m_Languages.Add(new Language(s,s));
            }
            SetLanguage(m_Settings.DefaultLanguage);
            /*m_Languages.Add(new Language("english", "english"));
            m_Languages.Add(new Language("german", "deutsch"));
            m_Settings.DefaultLanguage = "german";
            m_Settings.AvailableLanguages = new string[]{"english","german"};
            m_Settings.Save();*/

        }
        private void SaveLookup() {
            m_Settings.DefaultLanguage = m_CurrLanguage.ProgName;
            m_Settings.AvailableLanguages = new String[GetAvailableLanguages().Count];
            GetAvailableLanguages().CopyTo(m_Settings.AvailableLanguages, 0);
            m_Settings.Save();
        }
        /// <summary>
        /// Helper class for settings
        /// </summary>
        class TranslatorSettings : Core.ISerializable {
            public string DefaultLanguage = "";
            public string[] AvailableLanguages= {""};

            #region Save/Load
            private static string s_File = "TrSettings.xml";
            private static string s_FilePath = AppFolders.getAppFolders.AppDataDir.FullName + "\\Setup\\Localization\\" + s_File;
            public void WriteToSerializer(Core.SerializerBase Stream) {
                Stream.WriteElementStart("Languages");
                Stream.WriteData("DefaultLanguage", DefaultLanguage);
                Stream.WriteData("Count", AvailableLanguages.Length);
                IEnumerator Iterator = AvailableLanguages.GetEnumerator();
                string Node = "Lang";
                while (Iterator.MoveNext()) {
                    Stream.WriteElementStart(Node);
                    Stream.WriteData("Name", (string)Iterator.Current);
                    Stream.WriteElementEnd(Node);
                }
                Stream.WriteElementEnd("Languages");
            }
            public void ReadFromSerializer(Core.SerializerBase Stream) {
                int i=0;
                string NodeGroup;
                int StartNodeLevel = 0, CurrNodeLevel = 0;
                do {
                    NodeGroup = Stream.GetNodeName();
                    CurrNodeLevel = Stream.GetNodeLevel();
                    if (CurrNodeLevel < StartNodeLevel) { break; }
                    if (Stream.GetNodeType() != Core.SerializerBase.NodeType.NodeEnd) {
                        if (NodeGroup == "Languages") {
                            DefaultLanguage=Stream.ReadAsString("DefaultLanguage");
                            AvailableLanguages = new string[Stream.ReadAsInt("Count")];
                        }
                        if (NodeGroup == "Lang") {
                            string s = Stream.ReadAsString("Name");
                            AvailableLanguages[i]= s;
                            i++;

                        } else if (NodeGroup == Core.SerializerXML.FieldName.SerializerDocName.ToString()) {
                            if (NodeGroup != s_File)
                                throw new Exception(Core.SerializerXML.FieldName.SerializerDocName.ToString() + " unknown");
                        }
                    }

                } while (Stream.ReadNext());
            }
            public void Save() {
                Core.SerializerXML _stream = null;
                try {
                    _stream = new Core.SerializerXML(s_File, "1.0.0.0");
                    _stream.OpenOutputStream(s_FilePath);
                    _stream.WriteElementStart("Application");
                    WriteToSerializer(_stream);
                    _stream.WriteElementEnd("Application");
                    _stream.CloseOutputStream();
                    _stream = null;
                } catch (Exception e) {
                    GUI.DlgError.HandleException(e);
                } finally {
                    if (_stream != null) _stream.CloseOutputStream();
                }
            }
            public void Load() {
                string DocType = string.Empty;
                Core.SerializerXML _stream = null;
                try {
                    _stream = new Core.SerializerXML(s_File, "1.0.0.0");
                    _stream.OpenInputStream(s_FilePath);
                    if (_stream.GetDetectedSerializerName() != s_File)
                        throw new FormatException("");
                    string NodeGroup;
                    do {
                        NodeGroup = _stream.GetNodeName();
                        if (_stream.GetNodeType() != Core.SerializerBase.NodeType.NodeEnd) {
                            if (NodeGroup == "Application") {
                                ReadFromSerializer(_stream);
                            }
                        }
                    } while (_stream.ReadNext());

                    _stream.CloseInputStream();
                    _stream = null;
                } catch (Exception e) {
                    GUI.DlgError.HandleException(e);
                } finally {
                    if (_stream != null) _stream.CloseInputStream();
                }
            }
            #endregion
        }
        private LanguageCollection m_Languages;
        private Language m_CurrLanguage;
    }
}
