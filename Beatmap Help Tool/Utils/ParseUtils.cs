using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public static class ParseUtils
    {
        public static double GetDouble(string text)
        {
            return double.Parse(text.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        public static bool GetDouble(string text, out double value)
        {
            bool result = double.TryParse(text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double valueInternal);
            if (result)
            {
                value = valueInternal;
                return true;
            }
			else
            {
                value = 0;
                return false;
            }
        }
    }
}
