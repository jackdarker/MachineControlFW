using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Maschine1{
    // [Serializable]
    public class TranslationLookup : Core.SerializableDictionary<String,String>{
        public bool modified {
            get;
            set;
        }
        private string _ret;
        new public String this[String key] {
            get {

                if (this.TryGetValue(key, out _ret)) {
                    return (_ret);
                } else {
                    Add(key, key); //flag setzen:needstranslation ?? 
                    modified = true;
                    return key;
                }
            }
            set {
                this[key] = value;
            }
        }

       /* public ICollection Keys {
            get {
                return (Dictionary.Keys);
            }
        }

        public ICollection Values {
            get {
                return (Dictionary.Values);
            }
        }
        
        public void Add(String key, String value) {
            Dictionary.Add(key, value);
        }

        public bool Contains(String key) {
            return (Dictionary.Contains(key));
        }

        public void Remove(String key) {
            Dictionary.Remove(key);
        }*/
    }
}
