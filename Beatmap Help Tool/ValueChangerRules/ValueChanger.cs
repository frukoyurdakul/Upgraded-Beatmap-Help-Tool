using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.ValueChangerRules
{
    public enum ValueChanger
    {
        BPM, VOLUME, VOLUME_DIFF
    }

    public static class ValueChangerMethods
    {
        public static bool verifyInput(this ValueChanger changer, string text, out double value)
        {
            switch (changer)
            {
                case ValueChanger.BPM:
                    return VerifyUtils.verifyDouble("Please enter a valid BPM.", text, out value);
                default:
                    throw new ArgumentException("Wrong passed argument: " + changer);
            }
        }

        public static bool verifyInput(this ValueChanger changer, string text, out int value)
        {
            switch (changer)
            {
                case ValueChanger.VOLUME_DIFF:
                case ValueChanger.VOLUME:
                    return VerifyUtils.verifyRangeFromString("Please enter a volume between 5 to 100.", text, 5, 100, out value);
                default:
                    throw new ArgumentException("Wrong passed argument: " + changer);
            }
        }

        public static void fillForm(this ValueChanger changer, ChangerForm form)
        {
            switch (changer)
            {
                case ValueChanger.BPM:
                    form.typeChangeLabel.Text += "New BPM";
                    form.extraOptionCheckBox.Text = "Shift the rest of the music";
                    form.toolTip1.SetToolTip(form.extraOptionCheckBox, "Shifts the rest of the music if more timing points exist\r\non the beatmapset. If not selected, the rest of the objects will be kept as is.");
                    form.toolTip1.SetToolTip(form.typeChangeLabel, "Shifts the rest of the music if more timing points exist\r\non the beatmapset. If not selected, the rest of the objects will be kept as is.");
                    break;
                case ValueChanger.VOLUME:
                    form.typeChangeLabel.Text += "New volume";
                    form.Height -= form.extraOptionCheckBox.Height;
                    form.extraOptionCheckBox.Visible = false;
                    form.toolTip1.SetToolTip(form.typeChangeLabel, "Sets the volume to the selected points, for green and red points.");
                    break;
                case ValueChanger.VOLUME_DIFF:
                    form.typeChangeLabel.Text += "Volume difference";
                    form.label3.Text = "Difference: ";
                    form.Height -= form.extraOptionCheckBox.Height;
                    form.extraOptionCheckBox.Visible = false;
                    form.toolTip1.SetToolTip(form.typeChangeLabel, "Increases or decreases volume to the selected points depending on positive and negative number, for green and red points.");
                    break;
                default:
                    throw new ArgumentException("Wrong passed argument: " + changer);
            }
        }
    }
}
