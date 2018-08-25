using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maschine1 {
    /// <summary>
    /// Collection der WT-Files
    /// </summary>
    public class WTFileCollection {
        /// <summary>
        /// Holt die WT-Daten für eine Station aus der Collection
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static WTFileBase GetWTFile(int index) {
            if (m_WTFile == null) ResetStation(0);  //??
            return m_WTFile;
        }
        /// <summary>
        /// Schreibt die WT-Daten zurück in die Collection.
        /// Daten werden in Datei gespeichert und sind persistent.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="WTFile"></param>
        public static void SetWTFile(int index,WTFileBase WTFile) {
            if (WTFile != null) m_WTFile = WTFile;
        }
        /// <summary>
        /// Löscht die Daten aller WT-Files.
        /// </summary>
        /// <param name="NoStations"></param>
        public static void ResetAll(int NoStations) {}
        public static void ResetStation(int Station) {
            m_WTFile = new WTFileBase(Station);
        }
        private static WTFileBase m_WTFile = new WTFileBase(0);
    }
}
