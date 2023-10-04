using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Forms;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.ValueChangerRules
{
    public enum ValueChanger
    {
        BPM, VOLUME, VOLUME_DIFF, SV_OFFSET_CHANGE
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
                case ValueChanger.SV_OFFSET_CHANGE:
                    return VerifyUtils.verifyRangeFromString("Offset changes of inherited points require values between -1 and -10.".AddLines(2) + 
                        "If more offset change is necessary, repeat the process multiple times to see the effects.", text, -10, -1, out value);
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
                case ValueChanger.SV_OFFSET_CHANGE:
                    form.typeChangeLabel.Text += "SV Offset change";
                    form.label3.Text = "Difference: ";
                    form.Height -= form.extraOptionCheckBox.Height;
                    form.extraOptionCheckBox.Visible = false;
                    form.toolTip1.SetToolTip(form.typeChangeLabel, "Changes the SV offsets where a green point affects SV and is snapped onto a note, unless the point toggles kiai or is on a red point.".AddLines(2) 
                        + "Accepted values are between -1 and -10.");
                    break;
                default:
                    throw new ArgumentException("Wrong passed argument: " + changer);
            }
        }
    }
}
