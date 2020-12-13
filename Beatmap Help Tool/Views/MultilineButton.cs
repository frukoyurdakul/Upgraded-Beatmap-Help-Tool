using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Views
{
    public class MultilineButton : Button
    {
        private static readonly string newLine = Environment.NewLine;

        public override string Text
        {
            get => base.Text;
            set
            {
                if (wordCount == -1 && !string.IsNullOrEmpty(value))
                {
                    string[] words = value.Split(' ');
                    if (words.Length == 1 && string.IsNullOrWhiteSpace(words[0]))
                        wordCount = 1;
                    else
                    {
                        for (int i = 0; i < words.Length; i++)
                            if (!string.IsNullOrWhiteSpace(words[i]))
                                wordCount++;
                    }
                }
                string text = value;
                base.Text = wrapText(value);
                if (base.Text != text)
                    updateSizeByText();
            }
        }

        private readonly Graphics graphics;
        private readonly int minHeight;
        private int wordCount = -1;
        private SizeF textSize;
        private string flatCopy;
        private bool calledInternally = false;
 
        public MultilineButton() : base()
        {
            graphics = CreateGraphics();
            minHeight = Size.Height + 4;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!calledInternally)
                Text = Text;
            calledInternally = false;
        }

        private string wrapText(string source)
        {
            // Do not try to re-size the text
            // if the window is minimized,
            // in that case the size is smaller than
            // 5 pixels.
            if (Width < 5)
                return source;

            // Do not touch the source if it is empty.
            if (string.IsNullOrEmpty(source))
                return source;
            
            // Always short-circuit if word count is 1.
            if (wordCount == 1)
                return source;

            int index, lastSpaceIndex;
            bool lastSpaceChanged = false;
            flatCopy = source;

            // First, look for flat string by replacing new lines with 
            // spaces.
            while ((index = flatCopy.LastIndexOf(newLine)) >= 0)
            {
                lastSpaceIndex = flatCopy.LastIndexOf(' ');
                if (!lastSpaceChanged && lastSpaceIndex > index)
                {
                    flatCopy = flatCopy.Remove(lastSpaceIndex, 1).Insert(lastSpaceIndex, newLine);
                    lastSpaceChanged = true;
                }
                flatCopy = flatCopy.Remove(index, newLine.Length).Insert(index, " ");
                if (fits(flatCopy))
                    return flatCopy;
            }

            // Before editing, check if source fits first.
            if (fits(source))
                return source;

            // Then, do the reverse, as in replace spaces with new lines.
            while ((index = source.LastIndexOf(' ')) >= 0)
            {
                source = source.Remove(index, 1).Insert(index, newLine);
                if (fits(source))
                    return source;
            }

            // Could not do anything, just return the original.
            return source;
        }

        private string divideTextLineByLine(string source)
        {
            // If sent string is empty, immediately return.
            if (string.IsNullOrEmpty(source))
                return source;
           
            // First, trim the source from start and end, and copy to
            // another variable.
            source = source.Trim();
            string word, joined = "";
            List<string> words = new List<string>();

            // From the beginning, add the parts that are divided
            // with space, then check the size.
            words.AddRange(source.Split(' '));
            for (int i = 0; i < words.Count; i++)
            {
                words[i] = words[i].Trim();
                if (string.IsNullOrEmpty(words[i]))
                    words.RemoveAt(i--);
            }

            if (words.Count > 0)
            {
                // If this is the last word remaining, return the string itself.
                // This cannot be processed anymore as word division.
                if (words.Count == 1)
                {
                    // Wrap the text again if necessary.
                    if (!fits(words[0]))
                        return getDividedWord(words[0]);
                    else
                        return source;
                }
                
                // Start checking the words. First, join by word with space,
                // if at the end it does not fit, change the last part with
                // a newLine and start checking again.
                for (int i = 0; i < words.Count; i++)
                {
                    word = (i == 0 ? "" : " ") + words[i];
                    if (!fitsTotal(joined, word))
                    {
                        // The word itself may not fit although unlikely.
                        // Still, check if it has to be wrapped.
                        if (!fits(word))
                            word = getDividedWord(word.Trim());
                        else
                            word = newLine + word.Trim();
                    }
                    joined += word;
                }
                return joined;
            }
            else
            {
                // Somehow there are no words on this string. Return the original.
                return source;
            }
        }

        private string getDividedWord(string source)
        {
            string builder = "";
            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (fits(builder))
                    builder += source[i];
                else if (builder.Length > 0)
                {
                    builder = builder.Remove(builder.Length - 1, 1);
                    builder += newLine + source[i];
                }
            }
            return builder;
        }

        private void updateSizeByText()
        {
            int lineCount = StringUtils.GetStringCountInString(Text, newLine) + 1;
            if (lineCount > 1)
            {
                textSize = graphics.MeasureString(Text, Font);
                float targetHeight = Math.Max(textSize.Height, minHeight);
                changeSize(Size.Width, Convert.ToInt32(targetHeight + Padding.Top + Padding.Bottom + 15));
            }
            else
                changeSize(Size.Width, minHeight);
        }

        private void changeSize(int width, int height)
        {
            if (width != Width || height != Height)
            {
                calledInternally = true;
                Size = new Size(width, height);
            }
        }

        private bool fits(string text)
        {
            textSize = graphics.MeasureString(text, Font);
            return fits(textSize);
        }

        private bool fits(SizeF size)
        {
            return size.Width <= getTextMaxWidth();
        }

        private bool fitsTotal(string joined, string word)
        {
            float totalWidth = graphics.MeasureString(joined, Font).Width +
                graphics.MeasureString(word, Font).Width;
            return totalWidth <= getTextMaxWidth();
        }

        private float getTextMaxWidth()
        {
            return Width - Padding.Left - Padding.Right - 30;
        }
    }
}
