using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.SaveModel;
using Beatmap_Help_Tool.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Forms
{
    public partial class SvChanger : Form
    {
        public int FirstOffset = -1;
        public int LastOffset = -1;
        public double FirstSv = -1;
        public double LastSv = -1;
        public double TargetBpm = -1;
        public double GridSnap = -1;
        public int SvOffset = -1;
        public int SvIncreaseMode = 0; // default index
        public int Count = -1;
        public double SvIncreaseMultiplier = 1;
        public bool PutPointsByNotes = true;

        public SvChanger()
        {
            InitializeComponent();
            increaseModeComboBox.SelectedIndex = 0;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (checkValues())
            {
                // Ask for user's confirmation about the change and close the dialog
                // if "Yes" or "No" is pressed. Cancel will return to this form instead.
                VerifyUtils.performDefaultFormQuestion(this);
                if (rememberCheckBox.Checked)
                {
                    new SvChangerModel(firstTimeTextBox.Text, lastTimeTextBox.Text,
                        firstSvTextBox.Text, lastSvTextBox.Text, targetBpmTextBox.Text, gridSnapTextBox.Text,
                        svOffsetTextBox.Text, increaseModeComboBox.SelectedIndex, lastTimeTextBox.Text,
                        increaseMultiplierTextBox.Text, putPointsByNotesCheckBox.Checked,
                        activateTimeModeCheckBox.Checked).saveToPreferences();
                }
            }
        }

        // Checks the values and sets them on the fly.
        //
        private bool checkValues()
        {
            bool check = true;

            // Check the SV increase mode first.
            check = VerifyUtils.verifyRange("You need to select a SV increase mode.",
                0, increaseModeComboBox.Items.Count, increaseModeComboBox.SelectedIndex) &&

            // Check the necessary textboxes afterwards.
            VerifyUtils.verifyTextBoxes("You need to fill necessary fields.",
                increaseModeComboBox.SelectedIndex != 0 ? increaseMultiplierTextBox : null,
                firstTimeTextBox,
                lastTimeTextBox,
                firstSvTextBox,
                lastSvTextBox) &&

            // Check if grid snap is entered if "Put points by note snaps"
            // is not enabled.
            (putPointsByNotesCheckBox.Checked || VerifyUtils.verifyTextBoxes(
                "You need to fill the \"Grid Snap\" value if you don\'t check\n" +
                "\"Put points by note snaps\" checkbox."));

            // If anything was wrong here, don't check the rest.
            if (!check)
                return false;

            // Set the sv increase mode.
            SvIncreaseMode = increaseModeComboBox.SelectedIndex;

            // Starting from here, check the values themselves.
            // Start with the extracted times.
            check = VerifyUtils.verifyOffsetFormat("First entered time format is wrong.",
                firstTimeTextBox.Text, out FirstOffset);

            if (!check)
                return false;

            // Check the last time text box if time mode checkbox is checked.
            // Otherwise, try to parse the integer as count.
            if (activateTimeModeCheckBox.Checked)
            {
                check = VerifyUtils.verifyOffsetFormat("Last entered time format is wrong.", 
                    lastTimeTextBox.Text, out LastOffset);

                if (!check)
                    return false;
                else if (LastOffset <= FirstOffset)
                {
                    MessageBoxUtils.showError("You cannot use the last time point before the first time point.");
                    return false;
                }
            }
            else
            {
                check = VerifyUtils.verifyRangeFromString("Count text is wrong, value must be higher than 0.", lastTimeTextBox.Text,
                    0, int.MaxValue, out Count);

                if (!check)
                    return false;
            }

            check = ParseUtils.GetDouble(firstSvTextBox.Text, out FirstSv);
            if (!check)
            {
                MessageBoxUtils.showError("Entered first SV value is wrong.");
                return false;
            }

            check = ParseUtils.GetDouble(lastSvTextBox.Text, out LastSv);
            if (!check)
            {
                MessageBoxUtils.showError("Entered last SV value is wrong.");
                return false;
            }

            if (SvIncreaseMode != 0)
            {
                check = VerifyUtils.verifyRangeFromString("Sv increase multiplier text is wrong, value must be higher than 0.", increaseMultiplierTextBox.Text,
                    0, int.MaxValue, out SvIncreaseMultiplier);

                if (!check)
                    return false;
            }

            // If "Put points by notes" is not checked,
            // the grid snap value has to be defined.
            // Check that there.
            if (putPointsByNotesCheckBox.Checked)
            {
                GridSnap = 0;
                PutPointsByNotes = true;
                check = true;
            }
            else
            {
                check = VerifyUtils.verifyGridSnap("Grid snap value is wrong. Example: 1/4", 
                    gridSnapTextBox.Text, out GridSnap);

                if (!check)
                    return false;
            }

            // Check the target BPM and SV offset here. These fields are optional,
            // so accept blank text, but if it cannot parse, warn the user.
            check = VerifyUtils.verifyRangeFromString("Target BPM is wrong, example: 200.\nThis field is optional, so you can keep it blank.",
                targetBpmTextBox.Text, 0, double.MaxValue, out TargetBpm, true);

            if (!check)
                return false;

            check = VerifyUtils.verifyRangeFromString("SV offset is wrong, it should be between -10 and 0.\nThis field is optional, so you can keep it blank.",
                svOffsetTextBox.Text, -10, 0, out SvOffset, true);

            // Finally, return true or false if checks are done.
            return check;
        }

        private void increaseModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (increaseModeComboBox.SelectedIndex == 0)
            {                
                increaseMultiplierTextBox.Text = "";
                increaseMultiplierTextBox.Enabled = false;
                Focus();
            }
            else
            {
                increaseMultiplierTextBox.Enabled = true;
                increaseMultiplierTextBox.Focus();
                increaseMultiplierTextBox.Text = "2";
                increaseMultiplierTextBox.SelectionStart = increaseMultiplierTextBox.Text.Length;
            }
            SvIncreaseMode = increaseModeComboBox.SelectedIndex;
        }

        private void putPointsByNotesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (putPointsByNotesCheckBox.Checked)
            {
                gridSnapTextBox.Enabled = false;
                if (!gridSnapTextBox.PlaceHolderText.Contains("(Optional) "))
                    gridSnapTextBox.PlaceHolderText = "(Optional) " + gridSnapTextBox.PlaceHolderText;
                gridSnapTextBox.Text = "";
            }
            else
            {
                gridSnapTextBox.Enabled = true;
                if (gridSnapTextBox.PlaceHolderText.Contains("(Optional) "))
                    gridSnapTextBox.PlaceHolderText = gridSnapTextBox.PlaceHolderText.Replace("(Optional) ", "");
            }
        }

        private void activateTimeModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (activateTimeModeCheckBox.Checked)
            {
                copyTimeLabel.Text = "Copy last time:";
                lastTimeTextBox.PlaceHolderText = "Copy the last point offset.";
            }
            else
            {
                copyTimeLabel.Text = "Count:";
                lastTimeTextBox.PlaceHolderText = "Set how many inherited points you want.";
            }
        }

        private void SvChanger_Load(object sender, EventArgs e)
        {
            string savedModel = SharedPreferences.get<string>(SvChangerModel.KEY, null);
            SvChangerModel model = savedModel != null ? JsonConvert.DeserializeObject<SvChangerModel>(savedModel) : null;
            if (model != null)
            {
                firstTimeTextBox.Text = model.FirstOffsetText;
                firstSvTextBox.Text = model.FirstSvText;
                lastSvTextBox.Text = model.LastSvText;
                increaseModeComboBox.SelectedIndex = model.SvIncreaseMode;
                increaseMultiplierTextBox.Text = model.SvIncreaseMultiplierText;
                targetBpmTextBox.Text = model.TargetBpmText;
                gridSnapTextBox.Text = model.GridSnapText;
                svOffsetTextBox.Text = model.SvOffsetText;
                activateTimeModeCheckBox.Checked = model.ActivateBetweenTimeMode;
                putPointsByNotesCheckBox.Checked = model.PutPointsByNotes;
                if (model.ActivateBetweenTimeMode)
                    lastTimeTextBox.Text = model.LastOffsetText;
                else
                    lastTimeTextBox.Text = model.CountText;
            }
        }
    }
}
