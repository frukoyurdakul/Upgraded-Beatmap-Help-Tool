using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class VerifyUtils
    {
        public static bool verifyTextBoxes(string message, params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    MessageBoxUtils.showError(message);
                    return false;
                }
            }
            return true;
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
    }
}
