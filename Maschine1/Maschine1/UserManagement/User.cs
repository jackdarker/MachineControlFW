using System;
using System.Collections.Generic;

using System.Text;

namespace Maschine1.UserManagement {
    public class User {
        public enum EnuUserRole {
            Guest=0,
            User=1,
            Supervisor=8,
            Master = 128,
            Developer = 256
        }

        /// <summary>
        /// privater Konstruktor damit nicht beliebige User erstellt werden können
        /// </summary>
        private User() {   }
        private User(string Name, string Password, EnuUserRole Role) {
            m_Role = Role;
            m_Name = Name;
            m_EncryptedPassword = EncryptPassword(Password);
        }

        static public User CreateUser(string Name, string Password, EnuUserRole Role) {
            User _user = new User( Name,  Password,  Role);
            return _user;
        }
        /// <summary>
        /// wird zum deserializieren verwendet
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="EncryptedPassword"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        static public User CreateUserEncrypPass(string Name, string EncryptedPassword, EnuUserRole Role) {
            User _user = new User(Name, "", Role);
            _user.m_EncryptedPassword = EncryptedPassword;
            return _user;
        }
        private String m_Name = "";
        public String Name {
            get {
                return (m_Name);
            }
        }
        private EnuUserRole m_Role = EnuUserRole.Guest;
        public EnuUserRole Role {
            get {
                return (m_Role);
            }
        }
        public Boolean CheckPassword(string Password) {
            return EncryptPassword(Password) == m_EncryptedPassword;
        }
        private String m_EncryptedPassword = "";
        public String EncryptedPassword {
            get {
                return (m_EncryptedPassword);
            }
        }

        private String EncryptPassword(string Password) {
            char[] s = Password.ToCharArray();
            for(int i=0; i<s.Length;i++) {
                s[i] = (char)((s[i] % 255)+1);//Codierung mit asymetrischen Schlüssel???
            }
            return new string(s);    
        }

    }
}
