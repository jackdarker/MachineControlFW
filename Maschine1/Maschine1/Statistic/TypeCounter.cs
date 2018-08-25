using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using Maschine1.FileIO;

namespace Maschine1.Statistic
{

    #region Collections
        [Serializable]
        public class CounterCollection : DictionaryBase
        {
            public CounterCollection()
            {

            }
            public TypeCounter this[String key]
            {
                get
                {
                    return ((TypeCounter)Dictionary[key]);
                }
                set
                {
                    Dictionary[key] = value;
                }
            }
            public ICollection Keys
            {
                get
                {
                    return (Dictionary.Keys);
                }
            }
            public ICollection Values
            {
                get
                {
                    return (Dictionary.Values);
                }
            }
            public void Add(String key, TypeCounter value)
            {
                Dictionary.Add(key, value);
            }
            public bool Contains(String key)
            {
                return (Dictionary.Contains(key));
            }
            public void Remove(String key)
            {
                Dictionary.Remove(key);
            }
        }
        #endregion
     /// <summary>
     /// Zähler für Tages/Schicht/Gesamtzähler
     /// </summary>
    [Serializable]
    public class TypeCounter
    {
        [Serializable]
        public struct strTypeCounter
        {
            public string m_TypeName;
            public int m_TotalCountPass;
            public int m_TotalCountFail;
            public int m_DayCountPass;
            public int m_DayCountFail;
            public DateTime m_DayCountUpdate;
            public int m_PackageSize;
            public int m_PackageCount;
            public string m_PackageID;  //??
        }
        public event OnUpdateEventHandler EventUpdate;
        public delegate void OnUpdateEventHandler(object sender, EventArgs e);
        protected virtual void OnUpdate()
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            OnUpdateEventHandler handler = EventUpdate;
            EventArgs e = new EventArgs();
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Format the string to send inside the EventArgs parameter
                //e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        
        public TypeCounter()   {   }
        /// <summary>
        /// Mit dieser Funktion wird der Zähler um 1 incrementiert.
        /// Der Zähler muss vorher für den Typ geladen worden sein.
        /// </summary>
        /// <param name="Pass"></param>
        public void Count(bool Pass)  {
            LoadCounter(m_Counter.m_TypeName);
            DateTime DayCountUpdate = DateTime.Now;
            TimeSpan dt=DayCountUpdate - m_Counter.m_DayCountUpdate;
            m_Counter.m_DayCountUpdate = DayCountUpdate;
            if (dt.TotalDays > 1) {
                m_Counter.m_DayCountPass = 0;
                m_Counter.m_DayCountFail = 0;
            }
            if (Pass)    {
                m_Counter.m_TotalCountPass++;
                m_Counter.m_DayCountPass++;
            }
            else {
                m_Counter.m_TotalCountFail++;
                m_Counter.m_DayCountFail++;
            }
            SaveCounter();
            OnUpdate();
        }
        public void ResetPackageCount()    {
            SetPackageCount(0);
        }
        public void SetPackageCount(int Count)   {
            LoadCounter(m_Counter.m_TypeName);
            m_Counter.m_PackageCount=Count; //Count may be larger than Package size
            SaveCounter();
            OnUpdate();
        }
        public bool IsPackageFull()    {
            bool Full = (m_Counter.m_PackageCount>=m_Counter.m_PackageSize) &&
                        (m_Counter.m_PackageSize>0);
            return Full;
        }
        public string GetTypeName()    {
            return m_Counter.m_TypeName;
        }
        public int GetTotalCountFail()
        {
            return m_Counter.m_TotalCountFail;
        }
        public int GetTotalCountPass()    {
            return m_Counter.m_TotalCountPass;
        }
        public int GetDayCountFail()    {
            return m_Counter.m_DayCountFail;
        }
        public int GetDayCountPass()     {
            return m_Counter.m_DayCountPass;
        }
        public int GetPackageCount()    {
            return m_Counter.m_PackageCount;
        }
        public int GetPackageSize()     {
            return m_Counter.m_PackageSize;
        }
        public bool TypeExist(string TypeName, bool Create)     {
            bool found;
            FileInfo fInfo = new FileInfo(GetCounterFileName(TypeName));
            if (!fInfo.Exists)
            {
                found = false;
                SaveCounter(TypeName);
            }
            else
            {
                found = true;
            }
            return found;
        }
        public string[] GetAllTypeNames()     {   
            //??static?
            //??sort alphabetical
            string[] Files = Directory.GetFiles(AppFolders.getAppFolders.ResultsDir.FullName + "\\Counter\\", "*" + FileExtension);
            string[] Types = new string[Files.GetLength(0)];
            int i = 0;
            foreach (string File in Files)   {
                FileInfo Info = new FileInfo(File);
                Types.SetValue(Info.Name.Replace(FileExtension,""),i);
                i++;
            }
            return Types;
        }
        private string GetCounterFileName(string Type){
            //auf ungültige Typnamen prüfen ("")??
            if (Type == "") {
                throw (new ArgumentOutOfRangeException());
            };
            return AppFolders.getAppFolders.ResultsDir.FullName + "\\Counter\\" +Uri.EscapeDataString(Type + FileExtension);   //??
        }
        public void DeleteCounter(string TypeName) {
            //nicht Threadsafe?? 
            
            if (!TypeExist(TypeName, false)) return;
            this.m_Counter = new strTypeCounter();
            File.Delete(GetCounterFileName(TypeName));
            OnUpdate();
        }
        public void LoadCounter(string TypeName)    { 
            //nicht Threadsafe?? 
            Stream _stream=null;
            try   {   TypeExist(TypeName, true);//create empty file if new
                _stream = new FileStream(GetCounterFileName(TypeName), FileMode.Open, FileAccess.Read, FileShare.None);
                SoapFormatter formatter = new SoapFormatter();
                m_Counter=(TypeCounter.strTypeCounter)formatter.Deserialize(_stream);
            }
            catch (SerializationException e)    {
                throw(e);
            }

            finally   {
                if(_stream!=null) _stream.Close();
            }
        }
        protected void SaveCounter(){
            SaveCounter(m_Counter.m_TypeName);
        }
        protected void SaveCounter(string TypeName)   {
            m_Counter.m_TypeName=TypeName;
            Stream _stream = new FileStream(GetCounterFileName(TypeName), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            SoapFormatter formatter = new SoapFormatter();
            try 
            {
                formatter.Serialize(_stream, m_Counter);
            }
            catch (SerializationException e) 
            {
                throw(e); 
            }
            finally 
            {
              _stream.Close();
            }

        }

        protected strTypeCounter m_Counter = new strTypeCounter();
        const string FileExtension = "Count.xml";
    }
}
