using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Forms
{
    public partial class PositionNotesForm : Form
    {
        public int[] donPosition { get; internal set; } = new int[2];
        public int[] katPosition { get; internal set; } = new int[2];
        public int[] donFinisherPosition { get; internal set; } = new int[2];
        public int[] katFinisherPosition { get; internal set; } = new int[2];
        public bool applyToAllDiffs { get; internal set; } = true;

        public PositionNotesForm()
        {
            InitializeComponent();
            templatesComboBox.SelectedIndex = 0;
            DialogResult = DialogResult.None;
        }

        // Activates double-buffering through the entire form.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void arrangeNotesButton_Click(object sender, EventArgs e)
        {
            // Check the textboxes first to see if their
            // texts are in appropriate format and they are filled. "True" means they are verified.
            if (VerifyUtils.verifyTextBoxes("Please fill all textboxes with correct formatting, e.g. \"192, 192\".", 
                donPositionTextBox, 
                katPositionTextBox, 
                donFinisherPositionTextBox,
                katFinisherPositionTextBox))
            {
                // Check if the entered values are in between min-max values and formatting is okay.
                // "True" means they are verified.
                if (verifyNoteCoordinates("The correct formatting type should look like \"192, 192\" where x, y coordinates are separated by a comma. The maximum allowed range is from 32, 32 to 512, 384.",
                    donPositionTextBox,
                    katPositionTextBox,
                    donFinisherPositionTextBox,
                    katFinisherPositionTextBox))
                {
                    // Ask for user's confirmation about the change and close the dialog
                    // if "Yes" or "No" is pressed. Cancel will return to this form instead.
                    VerifyUtils.performDefaultFormQuestion(this);
                }
            }
        }

        private bool verifyNoteCoordinates(string message, params TextBox[] textBoxes)
        {
            int index = 0;
            foreach (TextBox textBox in textBoxes)
            {
                string[] texts = textBox.Text.Split(',');
                if (texts.Length != 2)
                {
                    MessageBoxUtils.showError(message);
                    return false;
                }
                texts[0] = texts[0].Trim();
                texts[1] = texts[1].Trim();

                int xCoordinate = Convert.ToInt32(texts[0]);
                int yCoordinate = Convert.ToInt32(texts[1]);

                if (!VerifyUtils.verifyRange(32, 512, xCoordinate) || !VerifyUtils.verifyRange(32, 384, yCoordinate))
                {
                    MessageBoxUtils.showError(message + " Detected coordinates: \"" + xCoordinate + ", " + yCoordinate + "\"");
                    return false;
                }

                switch (index)
                {
                    case 0:
                        fillArray(donPosition, xCoordinate, yCoordinate);
                        break;
                    case 1:
                        fillArray(katPosition, xCoordinate, yCoordinate);
                        break;
                    case 2:
                        fillArray(donFinisherPosition, xCoordinate, yCoordinate);
                        break;
                    case 3:
                        fillArray(katFinisherPosition, xCoordinate, yCoordinate);
                        break;
                }
                index++;
            }
            return true;
        }

        private void fillArray(int[] array, int x, int y)
        {
            array[0] = x;
            array[1] = y;
        }

        private void templatesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (templatesComboBox.SelectedIndex)
            {
                // Default
                case 0:
                    fillTextBoxes("160, 96", "352, 96", "160, 288", "352, 288");
                    break;
                case 1:
                    fillTextBoxes("224, 160", "288, 160", "224, 224", "288, 224");
                    break;
                case 2:
                    fillTextBoxes("32, 32", "480, 32", "32, 352", "480, 352");
                    break;
            }
        }

        private void fillTextBoxes(string donPosition, string katPosition, string donFinishPosition, string katFinishPosition)
        {
            donPositionTextBox.Text = donPosition;
            katPositionTextBox.Text = katPosition;
            donFinisherPositionTextBox.Text = donFinishPosition;
            katFinisherPositionTextBox.Text = katFinishPosition;
        }
    }
}
