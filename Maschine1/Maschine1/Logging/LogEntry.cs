using System;
using System.Collections.Generic;
using System.Text;

namespace Maschine1.Logging {
    /// <summary>
    /// 
    /// </summary>
    public class LogEntry : ILogEntry {
        public LogEntry() { }
        /// <summary>
        /// Copy-Constructor
        /// </summary>
        /// <param name="Error"></param>
        public LogEntry(LogData Error) {
            m_Error = new LogData();
            m_Error.m_ErrorCode = Error.m_ErrorCode;
            m_Error.m_ErrorText = Error.m_ErrorText;
            m_Error.m_ThreatLevel = Error.m_ThreatLevel;
            m_Error.m_Timestamp = Error.m_Timestamp;
        }
        public LogEntry(int ErrorCode, string ErrorText) {
            m_Error = new LogData();
            m_Error.m_ErrorCode = ErrorCode;
            m_Error.m_ErrorText = ErrorText;
            m_Error.m_ThreatLevel = ThreatLevel.Error;
            m_Error.m_Timestamp = DateTime.Now;
        }
        public string GetMsg() {
            return string.Format("{0:g}  " + m_Error.m_ErrorText, m_Error.m_Timestamp);
        }
 
        private LogData m_Error;
        public LogData GetData() {
            return m_Error;
        }
    }
}
