using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Utils
{
    public static class Extensions
    {
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke(action);
        }

        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dict)
            {
                action.Invoke(pair.Key, pair.Value);
            }
        }

        public static string AddLines(this string text, int lines)
        {
            for (int i = 0; i < lines; i++)
                text += Environment.NewLine;
            return text;
        }

        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static T First<T>(this List<T> list)
        {
            return list[0];
        }
    }
}
