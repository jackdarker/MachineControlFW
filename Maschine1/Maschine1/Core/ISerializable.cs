using System;
using System.Collections.Generic;
using System.Text;

namespace Maschine1.Core
{
    /// <summary>
    /// Interface for serializable Objects.
    /// Cooperates with SerializerBase.
    /// </summary>
    /// Im folgenden Beispielimplementierungen wie sie in einer Klasse verwendet werden können.
    /// Daten werden dabei in folgende XML-Struktur gewandelt
    /// <?xml version="1.0" encoding="utf-8"?>
    ///<Document SerializerDocName="UserDat" SerializerVersion="1.0.0.0">
     ///<Application>
    /// <Users>
    ///  <User Name="user" Role="1" Pass="vtfs" />
    ///  <User Name="admin" Role="128" Pass="benjo" />
    ///  <User Name="neu2" Role="128" Pass="uftu" />
    ///  </Users>
    ///</Application>
    ///</Document>
    /* \code
      private static string s_UserFile;  //Pfad zu Dateistream der die Daten enthält
      /// Hiermit wird ein Objekt in einen Stream geschrieben
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
       ///Hiermit wird ein Objekt aus einem Stream gelesen.
       ///Wichtig: Lesereihenfolge sollte identisch zur Schreibreihenfolge sein
        public void ReadFromSerializer(SerializerBase Stream) {
            User oldUser = m_User;
            m_Users = null;
            m_Users = new UserCollection();

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
     * * \endcode
        /// Irgendwo muss natürlich der Stream erzeugt werden. es kann auch ein stream verwendet werden um mehrere, verschiedene Objekte zu serialisieren. 
     * Es müssen dann verschiedene Nodegroups angelegt sein.
     * * \code
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
     * \endcode
*/
    public interface ISerializable
    {
        void WriteToSerializer(SerializerBase Stream);
        void ReadFromSerializer(SerializerBase Stream);
    }
}
