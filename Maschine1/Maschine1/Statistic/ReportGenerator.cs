using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Maschine1.FileIO;
using Maschine1;

namespace Maschine1.Statistic {
    /// <summary>
    /// Erzeugt csv-Datei mit Messwerttabelle.
    /// </summary>
    public class ReportGenerator : IReportGenerator {
        public ReportGenerator() { }
        void IReportGenerator.AddTotalResult(WTFileBase Info) {
            string _strType=Info.GetTypeName();
            FileInfo fInfo = new FileInfo(FileIO.AppFolders.getAppFolders.ResultsDir.FullName +
                "\\" + _strType + "_" + DateTime.Now.ToString("yyMMdd") + ".csv");
            Boolean _writeHeader = !fInfo.Exists;
            StreamWriter _stream = new StreamWriter(fInfo.FullName, true);
            ICollection Names = Info.GetResultDef().GetTestStepNames();
            ResultDef.TestStepBase Step;
            ResultDef.LimitDefBase Limit;
            
            if(_writeHeader) {
                string[] _Lines1 = new String[Names.Count*2+_StandardLines1.Length];
                string[] _Lines2 = new String[Names.Count*2+_StandardLines1.Length];
                string[] _Lines3 = new String[Names.Count*2+_StandardLines1.Length];
                string[] _Lines4 = new String[Names.Count*2+_StandardLines1.Length];
                int _colOffset=_StandardLines1.Length;
                for (int i = 0; i < _colOffset; i++) {
                    _Lines1[i] = _StandardLines1[i];
                    _Lines2[i] = _StandardLines2[i];
                    _Lines3[i] = _StandardLines3[i];
                    _Lines4[i] = _StandardLines4[i];
                }
                string Unit = "";
                string Min = "";
                string Max = "";
                foreach (string Name in Names) {
                    Unit = Min= Max = "";
                    Info.GetResultDef().GetResultValue(Name, out Step);
                    Info.GetResultDef().GetLimitValue(Step.GetLimitName(), out Limit);
                    if (Limit.GetType().Equals(typeof(ResultDef.LimitDefDouble))) {
                        ResultDef.LimitDefDouble.strLimit _LimitDbl = ((ResultDef.LimitDefDouble)Limit).GetLimit();
                        Unit = _LimitDbl.m_Unit;
                        Min = _LimitDbl.m_LowerLimit.ToString();
                        Max = _LimitDbl.m_UpperLimit.ToString();
                    }
                    _Lines1[_colOffset] = Step.GetName();
                    _Lines1[_colOffset + 1] = _Lines1[_colOffset] + "_pass/fail";
                    _Lines2[_colOffset] = Unit;
                    _Lines2[_colOffset + 1] = "";
                    _Lines3[_colOffset] = Max;
                    _Lines3[_colOffset + 1] = "";
                    _Lines4[_colOffset] = Min;
                    _Lines4[_colOffset + 1] = "";
                    _colOffset+=2;
                }
                //Header schreiben
                _stream.Write(_strType + _nl);
                _stream.Write(_nl + _nl + _nl + _nl + _nl + _nl + _nl); 
                foreach (string Text in _Lines1) {
                    _stream.Write(Text);
                    _stream.Write(_sep);
                }     
                _stream.Write(_nl);
                foreach (string Text in _Lines2) {
                    _stream.Write(Text);
                    _stream.Write(_sep);
                }
                _stream.Write(_nl);
                foreach (string Text in _Lines3) {
                    _stream.Write(Text);
                    _stream.Write(_sep);
                }
                _stream.Write(_nl);
                foreach (string Text in _Lines4) {
                    _stream.Write(Text);
                    _stream.Write(_sep);
                }
                _stream.Write(_nl);
                _stream.Write(_nl);
            }
            _stream.Write(_sep);
            _stream.Write(Info.GetSN());
            _stream.Write(_sep);
            _stream.Write(Info.GetCustSN()); 
            _stream.Write(_sep);
            //_stream.Write(Info.GetRework()); 
            _stream.Write(_sep);
            //_stream.Write(Info.WT);
            _stream.Write(_sep);
            _stream.Write(DateTime.Now.ToShortDateString());
            _stream.Write(_sep);
            _stream.Write(DateTime.Now.ToShortTimeString());
            _stream.Write(_sep);
            _stream.Write(UserManagement.UserManager.getUserManager.GetCurrentUser().Name) ;
            _stream.Write(_sep);
            _stream.Write(Info.GetResultDef().GetTotalResult().ToString());
            foreach (string Name in Names) {
                _stream.Write(_sep);
                Info.GetResultDef().GetResultValue(Name, out Step);
                Info.GetResultDef().GetLimitValue(Step.GetLimitName(), out Limit);
                _stream.Write(Step.GetValueString());
                _stream.Write(_sep);
                _stream.Write(Step.GetResult().ToString());
            }
            _stream.Write(_nl);
            _stream.Flush();

        }
        string _sep = ";";
        string _nl = "\r\n";
        string[] _StandardLines1 = { "","ProdNo", "SerialNo", "Rework", "WT", "Date", "Time", "Worker", "Test Result" };
        string[] _StandardLines2 = { "Unit", "", "", "", "", "", "", "","" };
        string[] _StandardLines3 = { "maxSet", "", "", "", "", "", "", "","" };
        string[] _StandardLines4 = { "minSet", "", "", "", "", "", "", "","" };

    }
}
