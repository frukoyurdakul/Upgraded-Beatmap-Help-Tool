using System;
using System.Drawing;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Utils
{
    public static class StringUtils
    {
        public static int GetIndexOfWithCount(string text, string searchText, int count)
        {
            if (count == 1)
                return text.IndexOf(searchText);

            int targetSize = searchText.Length;
            int countInternal = 0;
            string substring;
            for (int i = 0; i < text.Length - targetSize; i++)
            {
                substring = text.Substring(i, targetSize);
                if (substring == searchText && ++countInternal == count)
                    return i;
            }
            return -1;
        }

        public static int GetStringCountInString(string text, string searchText)
        {
            if (text.Length < searchText.Length)
                return 0;

            int count = 0;
            int iterationCount = text.Length - searchText.Length;
            int searchLength = searchText.Length;
            for (int i = 0; i < iterationCount; i++)
            {
                if (text.Substring(i, searchLength) == searchText)
                    count++;
            }
            return count;
        }

        public static string ReplaceNewLinesWithSpaces(this string text)
        {
            return text.Replace(Environment.NewLine, " ");
        }

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void AppendTextToRichTextBox(this RichTextBox box, string text, int color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = Color.FromArgb(color);
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static string GetDisplayOffset(double offset)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(offset);
            if (t.Hours > 0)
            {
                return offset.ToString() + " (" + string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds) + ")";
            }
            else
            {
                return offset.ToString() + " (" + string.Format("{0:D2}:{1:D2}:{2:D3}", t.Minutes, t.Seconds, t.Milliseconds) + ")";
            }
        }

        public static string GetSimpleDisplayOffset(double offset)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(offset);
            if (t.Hours > 0)
            {
                return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            }
            else
            {
                return string.Format("{0:D2}:{1:D2}:{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
            }
        }

        public static string GetOffsetWithLink(double offset)
        {
            string displayOffset = GetSimpleDisplayOffset(offset);
            return "<a href=\"osu://edit/" + displayOffset + "\">" + displayOffset + "</a>";
        }
    }
}
