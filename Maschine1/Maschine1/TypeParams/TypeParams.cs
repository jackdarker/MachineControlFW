using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using Maschine1.Core;
using System.Text;
using System.Threading;

namespace Maschine1
{
    public class TypeParams //??: ISerializable
    {
        //Modulabhängige Parameter  
        [Serializable]
        public class Params
        {
            public Params()
            {
                m_ResultDef=new ResultDef();   
            }
            //public string m_CustTypeNumber; //?? das gehört in die Demo-Klasse
            //public double m_SupplyVoltage;
            public ResultDef m_ResultDef ;
        }

        ///////////////////////////////////
        public TypeParams() {
            m_CurrType = "";
            m_TypeNames = new SortedList(0);
            m_Params = new Params();
            GetTypeNames();
        }

        virtual public Params GetParams() {
            return m_Params;
        }
        public void SetParams(Params Parameter) {
            m_Params = Parameter;
        }
        public void SaveParams() {
            SaveParams(GetCurrTypeName());
        }
        //ToDo: Backup der Typparameter anlegen
        //1 Verzeichnis pro Typ
        //Sonderzeichen ersetzen
        public void SaveParams(string Type)  {
            m_FileSemaphore.EnterWriteLock();
            Stream _stream = new FileStream(GetTypeFileName(Type), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            SoapFormatter formatter = new SoapFormatter();
            try {
                formatter.Serialize(_stream, GetParams());
                m_CurrType = Type;
                if (!TypeExist(m_CurrType))
                {
                    m_TypeNames.Add(m_CurrType, "");
                    SaveTypeNames();
                };
            }
            catch (SerializationException e) {
                throw(e); 
            }
            finally  {
              _stream.Close();
              m_FileSemaphore.ExitWriteLock();
            }

        }
        /// <summary>
        /// muss überladen werden zum Typcasten der Daten vom Serializer in Projektabhängige Struktur 
        /// </summary>
        /// <param name="Data"></param>
        virtual protected void DeserializeObject(object Data) {
            m_Params = (TypeParams.Params)Data;
        }
        //Todo??: Daten nur aus Datei laden wenn Datei oder Typ geändert
        public void LoadParams(string Type)   {
            Stream _stream = new FileStream(GetTypeFileName(Type), FileMode.Open, FileAccess.Read, FileShare.Read);
            SoapFormatter formatter = new SoapFormatter();
            try {
                if (!TypeExist(Type)) {
                    throw new ArgumentException("undefined type");
                }
                object _data = formatter.Deserialize(_stream);
                DeserializeObject(_data);
                m_CurrType = Type;
            }
            catch (SerializationException e) {
                throw(e);
            }

            finally {
                _stream.Close();
            }
        }

        
        /// ///////////////
        /*????
      public void WriteToSerializer(SerializerBase Stream) {
          ResultDef _Results = GetParams().m_ResultDef;
          ICollection _LimitNames = _Results.GetLimitNames();
          IEnumerator Iterator= _LimitNames.GetEnumerator();
          ResultDef.LimitDefBase _Limit;
            string Node = "Limits";
            while (Iterator.MoveNext()) {
                _Results.GetLimitValue(Iterator.Current.ToString(), out _Limit);
                _Limit
                Stream.WriteElementStart(Node);  
                Stream.WriteData("Name",Iterator.Current.ToString());
                Stream.WriteData("Role", (int)Iterator.Current.Value.Role);
                Stream.WriteData("Pass", Iterator.Current.Value.EncryptedPassword);
                Stream.WriteElementEnd(Node);
            }
            Stream.WriteElementEnd("Users");
        }

        public void ReadFromSerializer(SerializerBase Stream) {
            User oldUser = m_User;
            m_Users = null;
            m_Users = new UserCollection();

            string NodeGroup;
            int StartNodeLevel = 0, CurrNodeLevel = 0;
            do {
                NodeGroup = Stream.GetNodeName();
                CurrNodeLevel = Stream.GetNodeLevel();
                if (CurrNodeLevel < StartNodeLevel) { break; }  
                if (Stream.GetNodeType() != Core.SerializerBase.NodeType.NodeEnd) {
                    if (NodeGroup == "User") {
                        string s = Stream.ReadAsString("Name");
                        int r = Stream.ReadAsInt("Role");
                        string x = Stream.ReadAsString("Pass");
                        m_Users.Add(s, User.CreateUserEncrypPass(s, x, (User.EnuUserRole)r));

                    } else if (NodeGroup == SerializerXML.FieldName.SerializerDocName.ToString()) {
                        if (NodeGroup != "UserDat")
                            throw new Exception(SerializerXML.FieldName.SerializerDocName.ToString() + " unknown");
                    }
                }

            } while (Stream.ReadNext());
        }

        public void SaveParams(string Type) {
            Core.SerializerXML _stream = null;
            try {
                m_CurrType = Type;
                if (!TypeExist(m_CurrType)) {
                    m_TypeNames.Add(m_CurrType, "");
                    SaveTypeNames();
                };
                _stream = new Core.SerializerXML("TypeDat", "1.0.0.0");
                _stream.OpenOutputStream(GetTypeFileName(Type));
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
        public void LoadParams(string Type) {
            string DocType = string.Empty;
            Core.SerializerXML _stream = null;

            try { 
                if (!TypeExist(Type)) {
                    throw new ArgumentException("undefined type");
                }
                _stream = new Core.SerializerXML("TypeDat", "1.0.0.0");
                _stream.OpenInputStream(GetTypeFileName(Type));
                if (_stream.GetDetectedSerializerName() != "TypeDat")
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
                m_CurrType = Type;
            } catch (Exception e) {
                GUI.DlgError.HandleException(e);
            } finally {
                if (_stream != null) _stream.CloseInputStream();
            }
        }
        */////////
        public void DuplicateType(string SourceType, string DestType)
        { //??
        }
        public void DeleteType() {
            //Parameter werden nicht gelöscht, nur der Typname
            m_Params = null;
            m_Params = new Params();
            if(m_TypeNames.ContainsKey(m_CurrType)) {
                m_TypeNames.Remove(m_CurrType);
                SaveTypeNames();
            }
            m_CurrType = "";

        }
        public string GetCurrTypeName()  {
            return m_CurrType;
        }
        //Todo??: Daten nur aus Datei laden wenn Datei geändert
        public string[] GetTypeNames() {
            Stream _stream = new FileStream(m_TypesFile, FileMode.Open, FileAccess.Read, FileShare.None);
            SoapFormatter formatter = new SoapFormatter();  
            try  {
                m_TypeNames = (SortedList)formatter.Deserialize(_stream);
                string[] Types = new string[m_TypeNames.Count];
                for(int i=0;i<m_TypeNames.Count;i++)  {
                    Types[i]=(string)m_TypeNames.GetKey(i);
                }
                return Types;
            }
            catch (SerializationException e)  {
                throw e; 
            }
            finally {
                _stream.Close();
            }
        }
        private string m_TypesFile = Maschine1.FileIO.AppFolders.getAppFolders.TypesDir + "\\Types.xml";
        private void SaveTypeNames()  {
            m_TypesSemaphore.EnterWriteLock();
            Stream _stream = new FileStream(m_TypesFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            try {
                SoapFormatter formatter = new SoapFormatter();
                formatter.Serialize(_stream, m_TypeNames);
            }
            catch (SerializationException e)  {
                throw e; 
            }
            finally  {
                _stream.Close();
                m_TypesSemaphore.ExitWriteLock();
            }
        }
        public bool TypeExist(string Type) {
            bool found = m_TypeNames.ContainsKey(Type);
            return found;
        }
        private string GetTypeFileName(string Type) {
            //auf ungültige Typnamen prüfen ("")??
            return Maschine1.FileIO.AppFolders.getAppFolders.TypesDir + "\\"+Type + ".xml"; 
        }
        private SortedList m_TypeNames;
        private string m_CurrType;
        private static ReaderWriterLockSlim m_FileSemaphore = new ReaderWriterLockSlim();  //wird für verriegeln Datei-Zugriff verwendet  ??
        private static ReaderWriterLockSlim m_TypesSemaphore = new ReaderWriterLockSlim();  //wird für verriegeln verwendet  ??
        //Verweis auf Parameter-Objekt
        protected Params m_Params;
    }
}
