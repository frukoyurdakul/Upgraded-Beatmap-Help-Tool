using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Models;
using Beatmap_Help_Tool.Utils;
using Newtonsoft.Json;
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
    public partial class EqualizeSvForm : Form
    {
        public int StartOffset { get; internal set; }
        public int EndOffset { get; internal set; }
        public double TargetBpm { get; internal set; } = 0;
        public double SvMultiplier { get; internal set; } = 1;
        public bool EqualizeAll { get; internal set; }
        public bool UseRelativeSv { get; internal set; }

        public EqualizeSvForm()
        {
            InitializeComponent();
        }

        private void EqualizeSvForm_Load(object sender, EventArgs e)
        {
            string savedModel = SharedPreferences.get<string>(EqualizeSvData.KEY, null);
            EqualizeSvData model = savedModel != null ? JsonConvert.DeserializeObject<EqualizeSvData>(savedModel) : null;
            if (model != null)
            {
                startTimeTextBox.Text = model.StartTime;
                endTimeTextBox.Text = model.EndTime;
                targetBpmTextBox.Text = model.TargetBPM;
                svMultiplierTextBox.Text = model.SvMultiplier;
                equalizeAllCheckBox.Checked = model.EqualizeAll;
                useRelativeSvCheckBox.Checked = model.RelativeUse;
            }
        }

        private void useFullMapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startTimeTextBox.Enabled = !equalizeAllCheckBox.Checked;
            endTimeTextBox.Enabled = !equalizeAllCheckBox.Checked;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (checkValues())
            {
                VerifyUtils.performDefaultFormQuestion(this);
                if (rememberCheckBox.Checked)
                {
                    new EqualizeSvData(startTimeTextBox.Text, endTimeTextBox.Text, targetBpmTextBox.Text, svMultiplierTextBox.Text,
                        equalizeAllCheckBox.Checked, useRelativeSvCheckBox.Checked, rememberCheckBox.Checked).saveToPreferences();
                }
            }
        }

        private bool checkValues()
        {
            if (!equalizeAllCheckBox.Checked)
            {
                if (!VerifyUtils.verifyOffsetFormat("You need to enter a valid start time.", startTimeTextBox.Text, out int startOffset))
                    return false;

                if (!VerifyUtils.verifyOffsetFormat("You need to enter a valid end time.", endTimeTextBox.Text, out int endOffset))
                    return false;

                StartOffset = startOffset;
                EndOffset = endOffset;
            }
            return true;
        }
    }
}
