using Beatmap_Help_Tool.BeatmapTools;
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
    public partial class TimingRegionSelector : Form
    {
        public double FirstOffset { get; internal set; }
        public double LastOffset { get; internal set; }

        public TimingRegionSelector(string functionName)
        {
            InitializeComponent();
            functionNameLabel.Text += "\"" + functionName + "\"";
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            bool check = true;
            DialogResult = DialogResult.None;

            check &= VerifyUtils.verifyTextBoxes("You need to fill necessary fields.",
                firstTimeTextBox, lastTimeTextBox);

            if (!check)
                return;

            check &= VerifyUtils.verifyOffsetFormat("Offset format is wrong on first offset field..",
                firstTimeTextBox.Text, out int firstOffset);

            if (!check)
                return;

            check &= VerifyUtils.verifyOffsetFormat("Offset format is wrong on second offset field.",
                lastTimeTextBox.Text, out int lastOffset);

            if (!check)
                return;

            VerifyUtils.performDefaultFormQuestion(this);
        }
    }
}
