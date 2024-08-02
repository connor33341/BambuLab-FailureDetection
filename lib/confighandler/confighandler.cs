using Microsoft.Win32;
namespace confighandler
{
    public class ConfigHandler
    {
        public string SubKey = "";
        public bool ValidSubKey = false;
        public string[][] Keys = [];
        private RegistryKey OpenRegistry()
        {
            try
            {
                RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(SubKey,true);
                if (RegKey != null)
                {
                    ValidSubKey = true;
                }
                else
                {
                    RegistryKey NewRegKey = Registry.LocalMachine.CreateSubKey(SubKey,true);
                    return NewRegKey;
                }
                return RegKey;
            }
            catch
            {
                return null;
            }
        }
        private bool KeyExists(string Key)
        {
            RegistryKey RegKey = OpenRegistry();
            try
            {
                object KeyValue = RegKey.GetValue(Key);
                if (KeyValue != null)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public bool SetKey(string Key, string Value)
        {
            RegistryKey RegKey = OpenRegistry();
            if (RegKey != null)
            {
                bool Exists = KeyExists(Key);
                RegKey.SetValue(Key, Value);
                RegKey.Close();
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }
        public string ReadKey(string Key)
        {
            RegistryKey RegKey = OpenRegistry();
            if (RegKey != null)
            {
                bool Exists = KeyExists(Key);
                if (Exists)
                {
                    return RegKey.GetValue(Key).ToString();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return null;
        }
    }
}
