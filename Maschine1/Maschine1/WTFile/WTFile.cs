using System;
using System.Collections;
using System.Text;

namespace Maschine1
{   /// <summary>
    /// Struktur zum speichern von Teiledaten
    /// </summary>
    #region WTFileBase
    public class WTFileBase
    {
        public WTFileBase(int Index) {
            m_Index = Index;
        }
        /// <summary>
        /// Copy-constructor
        /// </summary>
        /// <param name="CopyThis"></param>
        public WTFileBase(WTFileBase CopyThis) {
            SetCustSN(CopyThis.GetCustSN());
            SetProgram(CopyThis.GetProgram());
            SetSN(CopyThis.GetSN());
            SetTypeName(CopyThis.GetTypeName());
            SetResultDef(new ResultDef(CopyThis.GetResultDef()));

        }
        public virtual void CreateFile(){
            //??
        }
        public virtual void LoadFile() {
            //??
        }
        public virtual void SaveFile() {
            //??
        }
        public void SetResultDef(ResultDef ResultDefinition) {
            m_ResultDef = ResultDefinition;
        }
        public ResultDef GetResultDef()  {
            return m_ResultDef;
        }
        /// <summary>
        /// Produktionsseriennummer
        /// </summary>
        /// <returns></returns>
        public string GetSN() {
            return m_SN;
        }
        public void SetSN(string SN) {
            m_SN=SN;
        }
      
        /// <summary>
        /// Kundenseriennummer
        /// </summary>
        /// <returns></returns>
        public string GetCustSN() {
            return m_CustSN;
        }
        public void SetCustSN(string SN) {
            m_CustSN=SN;
        }
        /// <summary>
        /// Typvorwahl
        /// </summary>
        /// <returns></returns>
        public string GetTypeName()  {
            return m_Type;
        }
        public void SetTypeName(string Type) {
            m_Type = Type;
        }
        /// <summary>
        /// Unterprogrammnr.
        /// </summary>
        /// <returns></returns>
        public int GetProgram() {
            return m_Prg;
        }
        public void SetProgram(int Prg) {
            m_Prg = Prg;
        }
        private int m_Prg;  
        private int m_Index;  //Dateien haben die Nummern der WTs 
        private string m_SN;
        private string m_CustSN;
        private string m_Type;  //der ausgewählte Typname
        private ResultDef m_ResultDef;
    }

    #endregion
}
