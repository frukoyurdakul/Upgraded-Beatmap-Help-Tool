using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class VerifyUtils
    {
        private const string TIMING_REGEX_STRING = @"(\d{1}[:])?\d{2}[:]\d{2}[:]\d{3}";
        private static readonly Regex timingRegex = new Regex(TIMING_REGEX_STRING, RegexOptions.Compiled);

        public static T safeGetItemFromList<T> (IList<T> items, int index)
        {
            if (items != null && index >= 0 && index < items.Count)
                return items[index];
            return default(T);
        }

        public static IList<T> createSafeSublist<T>(List<T> items, int startIndex, int endIndex)
        {
            if (startIndex >= 0 && endIndex < items.Count)
                return new SubList<T>(items, startIndex, endIndex);
            else
                return new List<T>();
        }

        public static bool verifyTextBoxes(string message, params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                if (textBox != null && string.IsNullOrEmpty(textBox.Text))
                {
                    showErrorMessage(message);
                    return false;
                }
            }
            return true;
        }

        public static bool verifyRangeFromString(string message, 
            string text, 
            int min, 
            int max, 
            out int value, 
            bool acceptBlankText = false)
        {
            if (!acceptBlankText || !string.IsNullOrEmpty(text))
            {
                try
                {
                    int number = int.Parse(text);
                    if (verifyRange(min, max, number))
                    {
                        value = number;
                        return true;
                    }
                    else
                    {
                        value = -1;
                        showErrorMessage(message);
                        return false;
                    }
                }
                catch (FormatException)
                {
                    value = -1;
                    showErrorMessage(message);
                    return false;
                }
            }
            else
            {
                value = 0;
                return true;
            }
        }

        public static bool verifyRangeFromString(string message, 
            string text, 
            double min, 
            double max, 
            out double value,
            bool acceptBlankText = false)
        {
            if (!acceptBlankText || !string.IsNullOrEmpty(text))
            {
                try
                {
                    double number = ParseUtils.GetDouble(text);
                    if (verifyRange(min, max, number))
                    {
                        value = number;
                        return true;
                    }
                    else
                    {
                        value = -1;
                        showErrorMessage(message);
                        return false;
                    }
                }
                catch (FormatException)
                {
                    value = -1;
                    showErrorMessage(message);
                    return false;
                }
            }
            else
            {
                value = 0;
                return true;
            }
        }

        public static bool verifyRange(int min, int max, int value)
        {
            return value >= min && value <= max;
        }

        public static bool verifyRange(double min, double max, double value)
        {
            return value >= min && value <= max;
        }

        public static bool verifyRange(int min, int max, params int[] values)
        {
            foreach (int value in values)
            {
                if (value < min || value > max)
                    return false;
            }
            return true;
        }

        public static bool verifyRangeAny(int min, int max, params int[] values)
        {
            foreach (int value in values)
            {
                if (value >= min && value <= max)
                    return true;
            }
            return false;
        }

        public static bool verifyRangeAny(double min, double max, params double[] values)
        {
            foreach (int value in values)
            {
                if (value >= min && value <= max)
                    return true;
            }
            return false;
        }

        public static void verifyListRangeOrThrow<T>(IList<T> list, int startIndex, int endIndex)
        {
            if (startIndex < 0)
                throw new ArgumentException("startIndex cannot be lower than 0.");
            else if (startIndex > list.Count - 1)
                throw new ArgumentException("startIndex cannot be higher than or equal to list count. startIndex: " + startIndex + ", list count: " + list.Count);
            else if (endIndex > list.Count - 1)
                throw new ArgumentException("endIndex cannot be higher than or equal to list count. startIndex: " + startIndex + ", list count: " + list.Count);
            else if (endIndex < startIndex)
                throw new ArgumentException("endIndex cannot be higher than or equal to startIndex. startIndex: " + startIndex + ", endIndex: " + endIndex);
        }

        public static int getValueInRangeBetween(int min, int max, params int[] values)
        {
            foreach (int value in values)
            {
                if (value >= min && value <= max)
                    return value;
            }
            return int.MinValue;
        }

        public static double getValueInRangeBetween(double min, double max, params double[] values)
        {
            foreach (int value in values)
            {
                if (value >= min && value <= max)
                    return value;
            }
            return double.MinValue;
        }

        public static bool verifyRange(double min, double max, params double[] values)
        {
            foreach (int value in values)
            {
                if (value < min || value > max)
                    return false;
            }
            return true;
        }

        public static bool verifyRange(string message, int min, int max, params int[] values)
        {
            foreach (int value in values)
            {
                if (value < min || value > max)
                {
                    showErrorMessage(message);
                    return false;
                }
            }
            return true;
        }

        public static bool verifyRange(string message, double min, double max, params double[] values)
        {
            foreach (int value in values)
            {
                if (value < min || value > max)
                {
                    showErrorMessage(message);
                    return false;
                }
            }
            return true;
        }

        public static bool verifyGridSnap(string message, string text, out double value)
        {
            message = Regex.Replace(text, @"\s+", "");
            string[] dividedArray = text.Split('/');
            if (dividedArray.Length != 2)
            {
                value = -1d;
                showErrorMessage(message);
                return false;
            }
            else
            {
                try
                {
                    value = Convert.ToDouble(dividedArray[0]) / Convert.ToDouble(dividedArray[1]);
                    return true;
                }
                catch (FormatException)
                {
                    value = -1d;
                    showErrorMessage(message);
                    return false;
                }
            }
        }

        public static bool verifyInteger(string message, string text, out int value)
        {
            try
            {
                value = int.Parse(text);
                return true;
            }
            catch (FormatException)
            {
                value = -1;
                showErrorMessage(message);
                return false;
            }
        }

        public static bool verifyDouble(string message, string text, out double value)
        {
            try
            {
                value = ParseUtils.GetDouble(text);
                return true;
            }
            catch (FormatException)
            {
                value = -1;
                showErrorMessage(message);
                return false;
            }
        }

        public static bool verifyOffsetFormat(string message, string offsetText, out int offsetValue)
        {
            Match match = timingRegex.Match(offsetText);
            if (match.Success)
            {
                string matched = match.Value;
                string[] inputs = matched.Trim().Split(':');
                if (inputs.Length == 3)
                {
                    int minutes = Convert.ToInt32(inputs[0]);
                    int seconds = Convert.ToInt32(inputs[1]);
                    int milliSeconds = Convert.ToInt32(inputs[2]);

                    if (!verifyRange(0, 59, minutes, seconds) || !verifyRange(0, 999, milliSeconds))
                    {
                        offsetValue = -1;
                        showErrorMessage(message);
                        return false;
                    }

                    offsetValue = (minutes * 60 * 1000) + (seconds * 1000) + milliSeconds;
                    return true;
                }
                else if (inputs.Length == 4)
                {
                    int hours = Convert.ToInt32(inputs[0]);
                    int minutes = Convert.ToInt32(inputs[1]);
                    int seconds = Convert.ToInt32(inputs[2]);
                    int milliSeconds = Convert.ToInt32(inputs[3]);

                    if (!verifyRange(0, int.MaxValue, hours) || 
                        !verifyRange(0, 59, minutes, seconds) || 
                        !verifyRange(0, 999, milliSeconds))
                    {
                        offsetValue = -1;
                        showErrorMessage(message);
                        return false;
                    }

                    offsetValue = (hours * 60 * 60 * 1000) + (minutes * 60 * 1000) + (seconds * 1000) + milliSeconds;
                    return true;
                }
                else
                {
                    showErrorMessage(message);
                    offsetValue = -1;
                    return false;
                }
            }
            else
            {
                showErrorMessage(message);
                offsetValue = -1;
                return false;
            }
        }

        public static void performDefaultFormQuestion(Form form)
        {
            performQuestionYesNoCancel(form, "Are you sure? (\"No\" will cancel the operation and close the window while \"Cancel\" will return to this window.)");
        }

        public static void performQuestionYesNoCancel(Form form, string question)
        {
            DialogResult result = MessageBoxUtils.showQuestionYesNoCancel(question);
            if (result == DialogResult.Yes)
            {
                form.DialogResult = DialogResult.OK;
                form.Close();
            }
            else if (result == DialogResult.No)
            {
                form.DialogResult = DialogResult.Cancel;
                form.Close();
            }
        }

        private static void showErrorMessage(string message)
        {
            MessageBoxUtils.showError(message);
        }
    }
}
