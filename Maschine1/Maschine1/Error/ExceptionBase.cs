using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maschine1.Error
{        
    [Serializable]
    public class ErrorStruct 
    {
        public DateTime m_Timestamp;
        public bool m_IsError;
        public int m_ErrorCode;
        public string m_ErrorText;
        public string Programm;
    }
    public class Error
    {
        public Error()
        {}
        public Error(ErrorStruct Error)
        {}
        public Error(int ErrorCode, string ErrorText)
        {
            m_Error = new ErrorStruct();
            m_Error.m_ErrorCode=ErrorCode;
            m_Error.m_ErrorText=ErrorText;
            m_Error.m_IsError=true;
            m_Error.m_Timestamp=DateTime.Now;
        }
        public string GetMsg()
        {
            return string.Format("{0:g}  "+m_Error.m_ErrorText,m_Error.m_Timestamp);
        }
        public void Save()
        {
            //save to Logfile??
        }
        private ErrorStruct m_Error;
    }
    public class ExceptionBase: System.Exception
    {

    }


}
