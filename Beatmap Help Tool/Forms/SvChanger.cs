using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
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
        public int FirstOffset = 0;
        public int LastOffset = 0;
        public double FirstSv = 0;
        public double LastSv = 0;
        public double TargetBpm = -1;
        public double GridSnap = -1;
        public int SvOffset = 0;
        public int SvIncreaseMode = 0;
        public int Count = -1;
        public double SvIncreaseMultiplier = 0;

        public SvChanger()
        {
            InitializeComponent();
            increaseModeComboBox.SelectedIndex = 0;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            
        }

        private bool checkValues()
        {
            bool check = true;

            // Check the SV increase mode first.
            check = !VerifyUtils.verifyRange("You need to select a SV increase mode.",
                0, increaseModeComboBox.Items.Count, increaseModeComboBox.SelectedIndex) &&

            // Check the necessary textboxes afterwards.
            !VerifyUtils.verifyTextBoxes("You need to fill necessary fields.",
                increaseMultiplierTextBox,
                firstTimeTextBox,
                lastTimeTextBox,
                firstSvTextBox,
                lastSvTextBox) &&

            // Check if grid snap is entered if "Put points by note snaps"
            // is not enabled.
            !putPointsByNotesCheckBox.Checked && !VerifyUtils.verifyTextBoxes(
                "You need to fill the \"Grid Snap\" value if you don\'t check" +
                "\"Put points by note snaps\" checkbox.");

            // If anything was wrong here, don't check the rest.
            if (!check)
                return false;

            // Starting from here, check the values themselves.
            // Start with the extracted times.
            check = VerifyUtils.verifyOffsetFormat("First entered time format is wrong.",
                firstTimeTextBox.Text, out FirstOffset);

            if (!check)
                return false;

            if (activateTimeModeCheckBox.Checked)
            {
                check = VerifyUtils.verifyOffsetFormat("Last entered time format is wrong.", 
                    lastTimeTextBox.Text, out LastOffset);

                if (!check)
                    return false;
            }
            else
            {
                check = VerifyUtils.verifyInteger("Count text is wrong", lastTimeTextBox.Text,
                    out Count);

                if (!check)
                    return false;
            }
        }

        private void increaseModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (increaseModeComboBox.SelectedIndex == 0)
            {
                increaseMultiplierTextBox.Text = "1";
                increaseMultiplierTextBox.Enabled = false;
            }
            else
            {
                increaseMultiplierTextBox.Text = "2";
                increaseMultiplierTextBox.Enabled = true;
            }
        }

        private void putPointsByNotesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (putPointsByNotesCheckBox.Checked)
            {
                gridSnapTextBox.Enabled = false;
                if (!gridSnapTextBox.PlaceHolderText.Contains("(Optional) "))
                    gridSnapTextBox.PlaceHolderText = "(Optional) " + gridSnapTextBox.PlaceHolderText;
            }
            else
            {
                gridSnapTextBox.Enabled = true;
                if (gridSnapTextBox.PlaceHolderText.Contains("(Optional) "))
                    gridSnapTextBox.PlaceHolderText = gridSnapTextBox.PlaceHolderText.Replace("(Optional) ", "");
            }
        }
    }
}
