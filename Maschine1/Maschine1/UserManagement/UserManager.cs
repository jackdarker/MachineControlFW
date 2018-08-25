using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Maschine1.Core;
using Maschine1.FileIO;


namespace Maschine1.UserManagement
{
    public class UserManager : ISerializable
	{
        private static readonly UserManager s_UserManager = new UserManager();
        public static UserManager getUserManager{
                get {
                    return s_UserManager;
                }
        }
        
        public delegate void DelegateUserChanged();
        public event OnUserChangedEventHandler EventUserChanged;
        /// <summary>
        /// Wird ausgelöst wenn Benutzer an- oder abgemeldet wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void OnUserChangedEventHandler(object sender, EventArgs e);
        protected virtual void OnUserChanged() {
            OnUserChangedEventHandler handler = EventUserChanged;
            EventArgs e = new EventArgs();
            if (handler != null)   {
                handler(this, e);
            }
        }

        private User m_User = null;
        public User GetCurrentUser() {
            return m_User;
        }
        public Boolean VerifyUser(string Name, string Password) {
            //countermeasure against bruteforce-attack: ??
            if (m_Users == null) return false;
            Boolean _ok = m_Users.ContainsKey(Name);
            if(!_ok ) return false;
            _ok = m_Users[Name].CheckPassword(Password);
            return _ok;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">If empty, Login as guest</param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Boolean LoginUser(string Name, string Password) {
            if (Name.Length == 0) {
                return Logout();
            }
            if (VerifyUser(Name, Password)) {
                m_User = m_Users[Name];
                OnUserChanged();
                return true;
            };
            return false;
        }
        public Boolean Logout() {
            m_User = s_DefaultUser;
            OnUserChanged();
            return true;
        }
        /// <summary>
        /// Löschen ist nur zulässig wenn:
        /// - angemeldeter User kann nicht gelöscht werden
        /// - User mindestens Supervisor ist
        /// - zu löschende User-Role kleiner/gleich angemeldete User-Role 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Boolean DeleteUser(string Name) {
            if (!CanDeleteUser(Name)) return false;
            m_Users.Remove(Name);
            Save();
            return true;
        }
        public Boolean CanDeleteUser(string Name) {
            if (GetCurrentUser().Name == Name) return false;
            if (GetCurrentUser().Role < User.EnuUserRole.Supervisor) return false;
            if (!m_Users.ContainsKey(Name)) return false;
            if (GetCurrentUser().Role < m_Users[Name].Role) return false;
            return true;
        }
        /// <summary>
        /// Anlegen nur zulässig wenn:
        /// - angemeldete UserRole mindestens Supervisor
        /// - anzulegende UserRole kleiner/gleich angemeldete User-Role
        /// - anzulegender User noch nicht vorhanden 
        /// - User-Name mindestens 4 Zeichen
        /// - Password mindestens 4 Zeichen
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public Boolean CreateUser(string Name, string Password, User.EnuUserRole Role) {
            if (!CanCreateUser(Name,  Password,  Role)) return false;
            m_Users.Add(Name, User.CreateUser(Name, Password, Role));
            Save();
            return true;
        }
        public Boolean CanCreateUser(string Name, string Password, User.EnuUserRole Role) {
            if (m_Users == null) return false;
            if (GetCurrentUser().Role < User.EnuUserRole.Supervisor) return false;
            if (Role > GetCurrentUser().Role) return false;
            if (Name.Length < 4) return false;
            if (Password.Length < 4) return false;
            if (m_Users.ContainsKey(Name)) return false;
            return true;
        }
        /// <summary>
        /// Ändern nur zulässig wenn:
        /// - angemeldete UserRole mindestens Supervisor
        /// - User vorhanden 
        /// - anzulegende UserRole kleiner/gleich angemeldete User-Role (man kann einem User oder sich selbst nicht eine höhere Role geben als man selbst hat)
        /// Wurde der angemeldete User geändert, wird er abgemeldet.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public Boolean ModifyRole(string Name, User.EnuUserRole Role) {
            if (!CanModifyRole(Name, Role)) return false;
            User _user = User.CreateUserEncrypPass(Name, m_Users[Name].EncryptedPassword, Role);
            m_Users[Name] = _user;
            Save();
            if (GetCurrentUser().Name == Name) Logout();
            return true;
        }
        public Boolean CanModifyRole(string Name, User.EnuUserRole Role) {
            if (m_Users == null) return false;
            if (m_Users.ContainsKey(Name)) return false;
            if (Role > GetCurrentUser().Role) return false;
            return true;
        }
        /// <summary>
        /// 
        /// Ändern nur zulässig wenn:
        /// - angemeldete UserRole mindestens Supervisor oder zu ändernder User angemeldet
        /// - angemeldete UserRole größer/gleich zu ändernder User
        /// - User vorhanden 
        /// - Passwort-Format ok
        /// Wurde der angemeldete User geändert, wird er abgemeldet.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public Boolean ModifyPassword(string Name, string NewPassword) {
            if (!CanModifyPassword(Name, NewPassword)) return false;
            User _user = User.CreateUser(Name, NewPassword, m_Users[Name].Role);
            m_Users[Name] = _user;
            Save();
            if (GetCurrentUser().Name == Name) Logout(); 
            return true;
        }
        public Boolean CanModifyPassword(string Name, string NewPassword) {
            if (m_Users == null) return false;
            if (m_Users.ContainsKey(Name)) return false;
            if (!(GetCurrentUser().Role >= User.EnuUserRole.Supervisor || GetCurrentUser().Name==Name)) return false;
            return true;
        }
        public String[] GetUsers() {
            String[] _Strings = new String[m_Users.Keys.Count];
            m_Users.Keys.CopyTo(_Strings,0);
            return _Strings;
        }
        /// <summary>
        /// returns Guest on unknown user
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public User.EnuUserRole GetUserRole(string Name) {
            User.EnuUserRole _Role = User.EnuUserRole.Guest;
            if (m_Users.ContainsKey(Name)) {
                _Role = m_Users[Name].Role;
            }
            return _Role;
        }
        //////////////////////////////////////////////////////////////////////////
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static UserManager() { }
        private UserManager() {
            s_UserFile = AppFolders.getAppFolders.AppDataDir.FullName + "\\Setup\\Users\\UserDat.xml";
            s_DefaultUser = User.CreateUser("guest", "", User.EnuUserRole.Guest);
            Init();
        }
        private void Init() {
            Load();
            /*m_User = User.CreateUser("user", "user", User.EnuUserRole.User);
            m_Users.Add(m_User.Name,m_User);
            m_User = User.CreateUser("admin", "admin", User.EnuUserRole.Master);
            m_Users.Add(m_User.Name, m_User);
            Save();*/
        }
        
        #region Save/Load
        private static string s_UserFile;
        public void WriteToSerializer(SerializerBase Stream) {
            Stream.WriteElementStart("Users");
            Dictionary<string,User>.Enumerator Iterator = m_Users.GetEnumerator();
            string Node = "User";
            while (Iterator.MoveNext()) {
                Stream.WriteElementStart(Node);
                Stream.WriteData("Name",Iterator.Current.Key);
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
            LoginUser("",""); //un-log to default user
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
        private void Save() {
            Core.SerializerXML _stream = null;
            try {
                _stream = new Core.SerializerXML("UserDat", "1.0.0.0");
                _stream.OpenOutputStream(s_UserFile);
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
        private void Load() {
            string DocType = string.Empty;
            Core.SerializerXML _stream = null;
            try {
                _stream = new Core.SerializerXML("UserDat", "1.0.0.0");
                _stream.OpenInputStream(s_UserFile);
                if (_stream.GetDetectedSerializerName() != "UserDat")
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
            } catch (Exception e) {
                GUI.DlgError.HandleException(e);
            } finally {
                if (_stream != null) _stream.CloseInputStream();
            }
        }
        #endregion
        
        private static User s_DefaultUser;
        private UserCollection m_Users = new UserCollection();
        private class UserCollection : Core.SerializableDictionary<String, User> { 
        }
        
	}
}
