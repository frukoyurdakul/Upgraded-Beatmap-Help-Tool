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
    public partial class SvChanger : Form
    {
        public int FirstOffset { get; internal set; } = 0;
        public int LastOffset { get; internal set; } = 0;
        public double FirstSv { get; internal set; } = 0;
        public double LastSv { get; internal set; } = 0;
        public double TargetBpm { get; internal set; } = -1;
        public double GridSnap { get; internal set; } = -1;
        public int SvOffset { get; internal set; } = 0;
        public int SvIncreaseMode { get; internal set; } = 0;
        public double SvIncreaseMultiplier { get; internal set; } = 0;

        public SvChanger()
        {
            InitializeComponent();
            increaseModeComboBox.SelectedIndex = 0;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            
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
