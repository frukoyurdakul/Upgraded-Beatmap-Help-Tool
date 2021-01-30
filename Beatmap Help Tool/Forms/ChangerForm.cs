using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.ValueChangerRules;
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
    public partial class ChangerForm : Form
    {
        public double valueDouble;
        public int valueInt;
        public bool valueAllTaikoDiffs;
        public bool valueExtraOption;

        private readonly ValueChanger changer;

        public ChangerForm(ValueChanger valueChanger)
        {
            InitializeComponent();
            changer = valueChanger;
            valueChanger.fillForm(this);
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            valueExtraOption = extraOptionCheckBox.Checked;
            valueAllTaikoDiffs = allTaikoDiffsCheckBox.Checked;
            switch (changer)
            {
                case ValueChanger.BPM:
                    if (changer.verifyInput(valueTextBox.Text, out valueDouble))
                        VerifyUtils.performDefaultFormQuestion(this);
                    break;
                default:
                    if (changer.verifyInput(valueTextBox.Text, out valueInt))
                        VerifyUtils.performDefaultFormQuestion(this);
                    break;
            }
        }
    }
}
