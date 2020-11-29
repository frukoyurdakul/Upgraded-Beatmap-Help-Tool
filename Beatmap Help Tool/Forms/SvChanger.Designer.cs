using Beatmap_Help_Tool.Views;

namespace Beatmap_Help_Tool.Forms
{
    partial class SvChanger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SvChanger));
            this.label1 = new System.Windows.Forms.Label();
            this.activateTimeModeCheckBox = new System.Windows.Forms.CheckBox();
            this.rememberCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.copyTimeLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.putPointsByNotesCheckBox = new System.Windows.Forms.CheckBox();
            this.increaseModeComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.increaseMultiplierTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.lastTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.lastSvTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.firstSvTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.firstTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.svOffsetTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.gridSnapTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.targetBpmTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // activateTimeModeCheckBox
            // 
            this.activateTimeModeCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.activateTimeModeCheckBox.AutoSize = true;
            this.activateTimeModeCheckBox.Checked = true;
            this.activateTimeModeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.activateTimeModeCheckBox.Location = new System.Drawing.Point(89, 88);
            this.activateTimeModeCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.activateTimeModeCheckBox.Name = "activateTimeModeCheckBox";
            this.activateTimeModeCheckBox.Size = new System.Drawing.Size(160, 17);
            this.activateTimeModeCheckBox.TabIndex = 1;
            this.activateTimeModeCheckBox.Text = "Activate between time mode";
            this.toolTip1.SetToolTip(this.activateTimeModeCheckBox, resources.GetString("activateTimeModeCheckBox.ToolTip"));
            this.activateTimeModeCheckBox.UseVisualStyleBackColor = true;
            this.activateTimeModeCheckBox.CheckedChanged += new System.EventHandler(this.activateTimeModeCheckBox_CheckedChanged);
            // 
            // rememberCheckBox
            // 
            this.rememberCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.rememberCheckBox.AutoSize = true;
            this.rememberCheckBox.Location = new System.Drawing.Point(46, 113);
            this.rememberCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rememberCheckBox.Name = "rememberCheckBox";
            this.rememberCheckBox.Size = new System.Drawing.Size(246, 17);
            this.rememberCheckBox.TabIndex = 2;
            this.rememberCheckBox.Text = "Remember the values and re-open the window";
            this.toolTip1.SetToolTip(this.rememberCheckBox, "Remebers the values and re-opens the window after\r\nadding SVs into the map once, " +
        "with the same values.");
            this.rememberCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 189);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Copy first time: ";
            this.toolTip1.SetToolTip(this.label2, "The start time of the SV change section. Examples:\r\n01:42:169 - \r\n02:34:223 (10) " +
        "- \r\n03:54:480");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 212);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Set first SV: ";
            this.toolTip1.SetToolTip(this.label3, "First SV value. Should be obvious.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 235);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Set last SV: ";
            this.toolTip1.SetToolTip(this.label4, "Last SV value. Can be lower than first SV, or higher than last SV, \r\nor equal if " +
        "Target BPM is defined.");
            // 
            // copyTimeLabel
            // 
            this.copyTimeLabel.AutoSize = true;
            this.copyTimeLabel.Location = new System.Drawing.Point(12, 258);
            this.copyTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.copyTimeLabel.Name = "copyTimeLabel";
            this.copyTimeLabel.Size = new System.Drawing.Size(78, 13);
            this.copyTimeLabel.TabIndex = 9;
            this.copyTimeLabel.Text = "Copy last time: ";
            this.toolTip1.SetToolTip(this.copyTimeLabel, resources.GetString("copyTimeLabel.ToolTip"));
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 280);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Target BPM: ";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 303);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Grid Snap:";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // putPointsByNotesCheckBox
            // 
            this.putPointsByNotesCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.putPointsByNotesCheckBox.AutoSize = true;
            this.putPointsByNotesCheckBox.Checked = true;
            this.putPointsByNotesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.putPointsByNotesCheckBox.Location = new System.Drawing.Point(103, 354);
            this.putPointsByNotesCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.putPointsByNotesCheckBox.Name = "putPointsByNotesCheckBox";
            this.putPointsByNotesCheckBox.Size = new System.Drawing.Size(142, 17);
            this.putPointsByNotesCheckBox.TabIndex = 15;
            this.putPointsByNotesCheckBox.Text = "Put points by note snaps";
            this.toolTip1.SetToolTip(this.putPointsByNotesCheckBox, resources.GetString("putPointsByNotesCheckBox.ToolTip"));
            this.putPointsByNotesCheckBox.UseVisualStyleBackColor = true;
            this.putPointsByNotesCheckBox.CheckedChanged += new System.EventHandler(this.putPointsByNotesCheckBox_CheckedChanged);
            // 
            // increaseModeComboBox
            // 
            this.increaseModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.increaseModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.increaseModeComboBox.FormattingEnabled = true;
            this.increaseModeComboBox.Items.AddRange(new object[] {
            "Linear",
            "Exponential",
            "Logarithmic"});
            this.increaseModeComboBox.Location = new System.Drawing.Point(115, 142);
            this.increaseModeComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.increaseModeComboBox.Name = "increaseModeComboBox";
            this.increaseModeComboBox.Size = new System.Drawing.Size(222, 21);
            this.increaseModeComboBox.TabIndex = 16;
            this.increaseModeComboBox.SelectedIndexChanged += new System.EventHandler(this.increaseModeComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 145);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "SV increase mode: ";
            this.toolTip1.SetToolTip(this.label8, "Selects the SV increase mode. Default is linear.");
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(93, 395);
            this.applyButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(160, 24);
            this.applyButton.TabIndex = 18;
            this.applyButton.Text = "Apply!";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 326);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "SV offset: ";
            this.toolTip1.SetToolTip(this.label9, "The offsets of the inherited points depending on \r\nthe original change. Values wi" +
        "ll not be affected,\r\nonly the offsets of them will be changed. Must\r\nbe between " +
        "-10 and 0 and has to be an integer.");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 168);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Increase multiplier:";
            this.toolTip1.SetToolTip(this.label10, "This defines the increase multiplier. Only effective if \"Exponential\" or\r\n\"Logari" +
        "thmic\" is selected. Value needs to be above than 0.");
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.AutoPopDelay = 32767;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // increaseMultiplierTextBox
            // 
            this.increaseMultiplierTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.increaseMultiplierTextBox.Enabled = false;
            this.increaseMultiplierTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.increaseMultiplierTextBox.ForeColor = System.Drawing.Color.Gray;
            this.increaseMultiplierTextBox.Location = new System.Drawing.Point(115, 167);
            this.increaseMultiplierTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.increaseMultiplierTextBox.Name = "increaseMultiplierTextBox";
            this.increaseMultiplierTextBox.PlaceHolderText = "Defines the exponential increase rate. Go higher value for steeper results.";
            this.increaseMultiplierTextBox.Size = new System.Drawing.Size(222, 19);
            this.increaseMultiplierTextBox.TabIndex = 26;
            // 
            // lastTimeTextBox
            // 
            this.lastTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.lastTimeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.lastTimeTextBox.Location = new System.Drawing.Point(115, 257);
            this.lastTimeTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lastTimeTextBox.Name = "lastTimeTextBox";
            this.lastTimeTextBox.PlaceHolderText = "Copy the last point offset.";
            this.lastTimeTextBox.Size = new System.Drawing.Size(222, 19);
            this.lastTimeTextBox.TabIndex = 24;
            // 
            // lastSvTextBox
            // 
            this.lastSvTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastSvTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.lastSvTextBox.ForeColor = System.Drawing.Color.Gray;
            this.lastSvTextBox.Location = new System.Drawing.Point(115, 235);
            this.lastSvTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lastSvTextBox.Name = "lastSvTextBox";
            this.lastSvTextBox.PlaceHolderText = "Set last SV value. Has to be above 0.";
            this.lastSvTextBox.Size = new System.Drawing.Size(222, 19);
            this.lastSvTextBox.TabIndex = 23;
            // 
            // firstSvTextBox
            // 
            this.firstSvTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstSvTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.firstSvTextBox.ForeColor = System.Drawing.Color.Gray;
            this.firstSvTextBox.Location = new System.Drawing.Point(115, 212);
            this.firstSvTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.firstSvTextBox.Name = "firstSvTextBox";
            this.firstSvTextBox.PlaceHolderText = "Set first SV value. Has to be above 0.";
            this.firstSvTextBox.Size = new System.Drawing.Size(222, 19);
            this.firstSvTextBox.TabIndex = 22;
            // 
            // firstTimeTextBox
            // 
            this.firstTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.firstTimeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.firstTimeTextBox.Location = new System.Drawing.Point(115, 189);
            this.firstTimeTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.firstTimeTextBox.Name = "firstTimeTextBox";
            this.firstTimeTextBox.PlaceHolderText = "Copy the time of the SV change start.";
            this.firstTimeTextBox.Size = new System.Drawing.Size(222, 19);
            this.firstTimeTextBox.TabIndex = 21;
            // 
            // svOffsetTextBox
            // 
            this.svOffsetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.svOffsetTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.svOffsetTextBox.ForeColor = System.Drawing.Color.Gray;
            this.svOffsetTextBox.Location = new System.Drawing.Point(115, 323);
            this.svOffsetTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.svOffsetTextBox.Name = "svOffsetTextBox";
            this.svOffsetTextBox.PlaceHolderText = "e.g. 1/4. All snaps are supported.";
            this.svOffsetTextBox.Size = new System.Drawing.Size(222, 19);
            this.svOffsetTextBox.TabIndex = 20;
            // 
            // gridSnapTextBox
            // 
            this.gridSnapTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSnapTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.gridSnapTextBox.Enabled = false;
            this.gridSnapTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.gridSnapTextBox.ForeColor = System.Drawing.Color.Gray;
            this.gridSnapTextBox.Location = new System.Drawing.Point(115, 301);
            this.gridSnapTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gridSnapTextBox.Name = "gridSnapTextBox";
            this.gridSnapTextBox.PlaceHolderText = "(Optional) e.g. 1/4. All snaps are supported.";
            this.gridSnapTextBox.Size = new System.Drawing.Size(222, 19);
            this.gridSnapTextBox.TabIndex = 14;
            // 
            // targetBpmTextBox
            // 
            this.targetBpmTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetBpmTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.targetBpmTextBox.ForeColor = System.Drawing.Color.Gray;
            this.targetBpmTextBox.Location = new System.Drawing.Point(115, 278);
            this.targetBpmTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.targetBpmTextBox.Name = "targetBpmTextBox";
            this.targetBpmTextBox.PlaceHolderText = "Optional, current BPM is first.";
            this.targetBpmTextBox.Size = new System.Drawing.Size(222, 19);
            this.targetBpmTextBox.TabIndex = 12;
            // 
            // SvChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 429);
            this.Controls.Add(this.increaseMultiplierTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lastTimeTextBox);
            this.Controls.Add(this.lastSvTextBox);
            this.Controls.Add(this.firstSvTextBox);
            this.Controls.Add(this.firstTimeTextBox);
            this.Controls.Add(this.svOffsetTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.increaseModeComboBox);
            this.Controls.Add(this.putPointsByNotesCheckBox);
            this.Controls.Add(this.gridSnapTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.targetBpmTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.copyTimeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rememberCheckBox);
            this.Controls.Add(this.activateTimeModeCheckBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(362, 455);
            this.Name = "SvChanger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SV Editor";
            this.Load += new System.EventHandler(this.SvChanger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox activateTimeModeCheckBox;
        private System.Windows.Forms.CheckBox rememberCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label copyTimeLabel;
        private System.Windows.Forms.Label label6;
        private PlaceHolderTextBox targetBpmTextBox;
        private System.Windows.Forms.Label label7;
        private PlaceHolderTextBox gridSnapTextBox;
        private System.Windows.Forms.CheckBox putPointsByNotesCheckBox;
        private System.Windows.Forms.ComboBox increaseModeComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button applyButton;
        private PlaceHolderTextBox svOffsetTextBox;
        private System.Windows.Forms.Label label9;
        private PlaceHolderTextBox lastSvTextBox;
        private PlaceHolderTextBox firstSvTextBox;
        private PlaceHolderTextBox firstTimeTextBox;
        private PlaceHolderTextBox lastTimeTextBox;
        private PlaceHolderTextBox increaseMultiplierTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}