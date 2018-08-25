using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Maschine1.FileIO {
    /// <summary>
    /// Liefert den konkreten Pfad für AppData usw. 
    /// </summary>
    public class AppFolders {
        private static readonly AppFolders s_AppFolders = new AppFolders();
        static AppFolders() { }
        private AppFolders() {
            Init();
        }

        public static AppFolders getAppFolders {
            get {
                return s_AppFolders;
            }
        }

        private void Init() {
            DirectoryInfo _app = new DirectoryInfo(Application.ExecutablePath);
            m_Application = _app.Name;
            m_ApplicationDir = _app.Parent;
            m_RootDir = _app.Parent.Parent.Parent;
            m_AppDataDir = new DirectoryInfo(m_RootDir.FullName + "\\AppData");
            m_ResultsDir = new DirectoryInfo(m_RootDir.FullName + "\\Results");
            m_TypesDir = new DirectoryInfo(m_AppDataDir.FullName + "\\Types");
        }


        private string m_Application = "";
        public string ApplicationName {
            get { return m_Application; }
        }
        private DirectoryInfo m_ApplicationDir ;
        /// <summary>
        /// Directory where the executable is stored
        /// </summary>
        public DirectoryInfo ApplicationDir {
            get { return m_ApplicationDir; }
        }
        private DirectoryInfo m_RootDir ;
        /// <summary>
        /// Root-Directory of the project
        /// </summary>
        public DirectoryInfo RootDir {
            get { return m_RootDir; }
        }
        private DirectoryInfo m_AppDataDir ;
        /// <summary>
        /// Directory where Configuration-Data is stored
        /// </summary>
        public DirectoryInfo AppDataDir {
            get { return m_AppDataDir; }
        }
        private DirectoryInfo m_ResultsDir ;
        /// <summary>
        /// Directory where Results and other created data is stored
        /// </summary>
        public DirectoryInfo ResultsDir {
            get { return m_ResultsDir; }
        }
        private DirectoryInfo m_TypesDir ;
        /// <summary>
        /// Directory where Typeinformation is stored
        /// </summary>
        public DirectoryInfo TypesDir {
            get { return m_TypesDir; }
        }
    }
}
