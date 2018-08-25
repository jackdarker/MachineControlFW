using System;
using System.Collections.Generic;
using System.Text;


namespace Maschine1.Logging {

    [Serializable]
    public class LogData {
        public DateTime m_Timestamp=DateTime.Now;
        public ThreatLevel m_ThreatLevel=ThreatLevel.Error;
        public int m_ErrorCode;
        public string m_ErrorText="";
        public string m_Programm="";
    }
    public enum ThreatLevel {
        Error = 10,
        Warning = 20,
        Info = 30
    }
    public class ExceptionBase : System.Exception {
    }
    /// <summary>
    /// Interface für Logging-Entry
    /// </summary>
    public interface ILogEntry {
        string GetMsg();
        LogData GetData();
    }

    /// <summary>
    /// Interface für Logging-Sammlung
    /// </summary>
    public interface ILogger {
        List<LogEntry> LoadHistory();  //??warum kann ich nicht ILogEntry verwednen
        void SaveEntry(ILogEntry Entry);
    }
}
