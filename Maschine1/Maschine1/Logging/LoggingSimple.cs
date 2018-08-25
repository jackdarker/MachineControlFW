using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using Maschine1.FileIO;

namespace Maschine1.Logging
{        

    /// <summary>
    /// Implementierung des Logging-Providers
    /// da in File geschrieben wird, verwenden wir ein Singleton
    /// </summary>
    public class Logger : ILogger {
        public void SaveEntry(ILogEntry Entry) {
            BinaryWriter _stream = null;
            BinaryWriter _stream2 = null;
            try {
                BuildLogFile();
                _stream = new BinaryWriter(File.Open(m_LogFile.FullName, FileMode.OpenOrCreate));
                _stream2 = new BinaryWriter(File.Open(m_IdxFile.FullName, FileMode.OpenOrCreate));
                _stream.Seek(0, SeekOrigin.End);
                _stream2.Seek(0, SeekOrigin.End);
                _stream2.Write((uint)m_LogFile.Length);
                LogData Data = Entry.GetData();
                string ThreadLevel = Data.m_ThreatLevel.ToString();
                string Time = Data.m_Timestamp.ToString("yyyy.MM.dd hh:mm:ss.fff", CultureInfo.InvariantCulture);
                string Program = Data.m_Programm.ToString();
                string ErrorText = Data.m_ErrorText.ToString();
                _stream.Write(ThreadLevel.Length);
                _stream.Write(Time.Length);
                _stream.Write(Program.Length);
                _stream.Write(ErrorText.Length);
                _stream.Write((int)0);
                _stream.Write((int)0);
                _stream.Write(ThreadLevel.ToString());
                _stream.Write(Time);
                _stream.Write(Program);
                _stream.Write(ErrorText);
                _stream.Write("");
                _stream.Write("");
                _stream.Flush();
                _stream2.Flush();
            } catch (Exception e) {
                throw e;
            } finally {
                if(_stream != null) {
                _stream.Close();
                }
                if(_stream2 != null) {
                    _stream2.Close();
                }
            }
        }
        public List<LogEntry> LoadHistory() { //??
            BuildLogFile();
            BinaryReader _stream = null;
            BinaryReader _stream2 = null;
            List<LogEntry> _LogEntrys = null;
            try {
                if (m_LogFile == null || !m_LogFile.Exists) {
                    return null;
                }
                int _NoEntrys= (int)(m_IdxFile.Length /4);
                _stream = new BinaryReader(File.Open(m_LogFile.FullName, FileMode.Open));
                _stream2 = new BinaryReader(File.Open(m_IdxFile.FullName, FileMode.Open));
                LogData _Entry;
                int _Pos, _Pos2,_Length;
                int _a,_b,_c,_d,_e,_f;
               _LogEntrys = new List<LogEntry>(_NoEntrys);
                string _tmp;
                
                _Pos=_stream2.ReadInt32();
                _tmp = (new string(_stream.ReadChars(_Pos)));//read Dat-Header??
                for(int i=0;i< _NoEntrys;i++){
                    _Entry=new LogData();
                 /*   _Pos2=_stream2.ReadInt32();
                    _Length = _Pos2-_Pos;   //check Length ??
                   */ _a=_stream.ReadInt32();
                    _b=_stream.ReadInt32();
                    _c=_stream.ReadInt32();
                    _d=_stream.ReadInt32();
                    _e=_stream.ReadInt32();
                    _f=_stream.ReadInt32();
                    _tmp = (_stream.ReadString());
                    _Entry.m_ThreatLevel = (ThreatLevel)Enum.Parse(typeof(ThreatLevel), _tmp);
                    //_tmp = (new string(_stream.ReadChars(_b)));
                    _tmp = (_stream.ReadString());
                    _Entry.m_Timestamp = DateTime.Parse(_tmp,CultureInfo.InvariantCulture);
                    //_tmp = (new string(_stream.ReadChars(_c)));
                    _tmp = (_stream.ReadString());
                    _Entry.m_Programm = _tmp;
                    //_tmp = (new string(_stream.ReadChars(_d)));
                    _tmp = (_stream.ReadString());
                    _Entry.m_ErrorText = _tmp;
                    _tmp = (_stream.ReadString());
                    _tmp = (_stream.ReadString());
                    //_tmp = (new string(_stream.ReadChars(_e)));
                    //_tmp = (new string(_stream.ReadChars(_f)));
                    _LogEntrys.Add(new LogEntry(_Entry));
                   //_Pos=_Pos2;
                }
                

            } catch (Exception e) {
                throw e;
            } finally {
                if(_stream != null) {
                _stream.Close();
                }
                if(_stream2 != null) {
                    _stream2.Close();
                }
            }
            return _LogEntrys;
        }
        private void BuildLogFile() {
            DirectoryInfo _di = new DirectoryInfo(GetLogDirectory());
            FileInfo[] _files = _di.GetFiles("*.dat",SearchOption.TopDirectoryOnly);
            string _fileName = "";
            FileInfo _fi=null;
            for(int i=0;i< _files.Length;i++) { //get the newest fileset if there is one
                if(_fi==null) _fi=_files[i];
                if(_fi.Name.CompareTo(_files[i].Name)<0) _fi=_files[i];
            }
            _fileName = DateTime.Now.ToString("yyMMddHHmmss");
            if (_fi == null || _fi.Length > 10000) { //create new fileset if there is none or already full
                m_LogFile = new FileInfo(GetLogDirectory() + "\\Log" + _fileName + ".dat");
                m_IdxFile = new FileInfo(GetLogDirectory() + "\\Log" + _fileName + ".idx");
                BinaryWriter _stream = null;
                try {
                    _stream = new BinaryWriter(File.Open(m_LogFile.FullName, FileMode.OpenOrCreate));
                    _stream.Write("01.00.00");  //64byte header with version-number
                    _stream.Write(new byte[54]);
                    _stream.Flush();
                } catch (Exception e) {
                    throw(e);
                } finally {
                    _stream.Close();
                }

            } else {
                m_LogFile = _fi;
                m_IdxFile = new FileInfo(_fi.DirectoryName+"\\"+_fi.Name.Replace("dat", "idx"));
            }
        }
        private string GetLogDirectory() {
            return FileIO.AppFolders.getAppFolders.ResultsDir.FullName+"\\Logging";
        }

    ///////////////////////////////////////////////////////////
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        private static readonly Logger s_LogCollection = new Logger();
        static Logger() { }
        private Logger() {
            
        }
        public static Logger getLogger {
            get {
                return s_LogCollection;
            }
        }
        private FileInfo m_LogFile = null;
        private FileInfo m_IdxFile = null;
    }
}
