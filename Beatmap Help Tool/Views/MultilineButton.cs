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
                base.Text = wrapText(value);
                updateSizeByText();
            }
        }

        private readonly Graphics graphics;
        private SizeF textSize;
        private int textLineCount = 0, textLength, length;

        private string[] words;
        private string trimmed;
        private bool calledInternally = false;
 
        public MultilineButton() : base()
        {
            graphics = CreateGraphics();
            textLineCount = string.IsNullOrEmpty(Text) ? 0 : 1;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!calledInternally)
            {
                Text = wrapText(Text);
                updateSizeByText();
            }
        }

        private string wrapText(string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            // Extract the newLine property from the string itself.
            source = source.Replace(newLine, "");
            textSize = graphics.MeasureString(source, Font);
            if (fits(textSize))
                return source;

            // We need to wrap the text. Start dividing the texts and change size if possible.
            // Be careful about OnSizeChanged call though, prevent recursive calculations.
            words = source.Split(' ');
            if (words.Length == 1)
            {
                // There is only one word (not really likely but still) so 
                // we need to wrap the word itself. Start removing characters one by one.
                return divideTextFully(source);
            }
            else if (words.Length > 1)
            {
                // There are mulitple words, starting from the last word 
                // we need to divide the words.
                return divideTextLineByLine(source);
            }
            else
                throw new ArgumentException("Argument is wrong: \"" + source + "\", word count is: " +
                    words.Length.ToString());
        }

        private string divideTextFully(string source)
        {
            textLength = source.Length;
            int startIndex = 0;
            int endIndex = textLength;
            while (!fits(textSize) && --endIndex >= 0)
            {
                trimmed = source.Substring(0, endIndex);
                textSize = graphics.MeasureString(trimmed, Font);
            }
            if (endIndex == textLength || endIndex < 0)
            {
                // Either the text fits, or we couldn't do anything, just keep the original text.
                return source;
            }
            else
            {
                // We managed to divide the text, change size, insert a \n on the string and
                // return it. Also work recursively starting from here because the text might be
                // multiline bigger than 2.
                length = endIndex - startIndex;
                trimmed = source.Substring(startIndex, length) + newLine;
                return source.Substring(0, length) + divideTextFully(trimmed);
            }
        }

        private string divideTextLineByLine(string source)
        {
            // If sent string is empty, immediately return.
            if (string.IsNullOrEmpty(source))
                return source;
           
            // First, trim the source from start and end, and copy to
            // another variable.
            source = source.Trim();
            string trimmed, joined, joinedTemp = "";
            List<string> words = new List<string>();

            // From the beginning, add the parts that are divided
            // with space, then check the size.
            int lastIndex;
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
                // This cannot be processed anymore.
                if (words.Count == 1)
                    return source;

                // Got the words and trimmed. Now it is time to check the sizes
                // of the words after joining them.

                // Re-create the source string to eliminate multiple spaces between words.
                source = string.Join(" ", words);

                // Start checking the sizes.
                joined = words[0];
                if (!fits(graphics.MeasureString(joined, Font)))
                    return joined + newLine + divideTextLineByLine(source.Substring(joined.Length - 1));
                lastIndex = 1;
                while (lastIndex < words.Count)
                {
                    joined += " " + words[lastIndex++];
                    if (!fits(graphics.MeasureString(joined, Font)))
                    {
                        if (joinedTemp == "")
                            joinedTemp = joined;
                        break;
                    }
                    joinedTemp = joined;
                }

                // If exited from the loop, it means the text does not fit anymore.
                // If last index is bigger than 0, it means the check is rather successful.
                // Otherwise, something went wrong because we already done an early
                // check on one word issue.
                if (lastIndex > 0 && joinedTemp.Length > 0)
                {
                    // If last index is equal to list size - 1, somehow there is a 
                    // wrong logic here, but do not mind and return the source.
                    if (lastIndex == words.Count - 1)
                        return source;
                    else
                        return joinedTemp + newLine + divideTextLineByLine(source.Substring(joinedTemp.Length));
                }
                else
                    throw new Exception("Somehow the text does not fit and lines are ended. String: " + 
                        source);
            }
            else
            {
                // Somehow there are no words on this string. Return the original.
                return source;
            }
        }

        private void updateSizeByText()
        {
            textSize = graphics.MeasureString(Text, Font);
            int lineCount = StringUtils.getStringCountInString(Text, newLine) + 1;
            float targetHeight = textSize.Height * lineCount;
            if (targetHeight > 0 && lineCount > 1)
                changeSize(Size.Width, Convert.ToInt32(targetHeight + 
                    Padding.Top + Padding.Bottom + 30));
        }

        private void changeSize(int width, int height)
        {
            if (width != Width || height != Height)
            {
                calledInternally = true;
                Size = new Size(width, height);
                Update();
                calledInternally = false;
            }
        }

        private bool fits(SizeF size)
        {
            return size.Width <= Width - Padding.Left - Padding.Right - 30;
        }
    }
}
