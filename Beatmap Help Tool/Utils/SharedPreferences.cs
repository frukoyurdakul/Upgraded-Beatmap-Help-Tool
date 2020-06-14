using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public static class SharedPreferences
    { 
        private const string SETTINGS_FILE_PATH = "data\\settings.txt";
        private const string DATA_DIR_PATH = "data";
        private static readonly Dictionary<string, object> pairs;
        static SharedPreferences()
        {
            if (!File.Exists(SETTINGS_FILE_PATH))
            {
                Directory.CreateDirectory(DATA_DIR_PATH);
                pairs = new Dictionary<string, object>();
            }
            else
            {
                pairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText("data\\settings.txt"));
                if (pairs == null)
                    pairs = new Dictionary<string, object>();
            }
        }

        public static T get<T>(string key, T defaultValue)
        {
            lock (pairs)
            {
                if (pairs.ContainsKey(key))
                    return (T)pairs[key];
                else
                    return defaultValue;
            }
        }

        public static Editor edit()
        {
            return new Editor();
        }

        public class Editor
        {
            private static readonly Dictionary<string, object> pendingPairs = new Dictionary<string, object>();

            public Editor put<T>(string key, T value)
            {
                lock (pendingPairs)
                {
                    pendingPairs[key] = value;
                }
                return this;
            }

            public void apply()
            {
                lock (pairs)
                {
                    foreach (KeyValuePair<string, object> entry in pendingPairs)
                    {
                        pairs[entry.Key] = entry.Value;
                    }
                }
                pendingPairs.Clear();
                ThreadUtils.executeOnBackground(new Action(() =>
                {
                    string saveText;
                    lock (pairs)
                    {
                        saveText = JsonConvert.SerializeObject(pairs);
                    }
                    File.WriteAllText(SETTINGS_FILE_PATH, saveText);
                }));
            }
        }
    }
}
