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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.putPointsByNotesCheckBox = new System.Windows.Forms.CheckBox();
            this.increaseModeComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.increaseMultiplierTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.lastTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.lastSvTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.firstSvTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.firstTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.svOffsetTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.gridSnapTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.targetBpmTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 90);
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
            this.activateTimeModeCheckBox.Location = new System.Drawing.Point(119, 108);
            this.activateTimeModeCheckBox.Name = "activateTimeModeCheckBox";
            this.activateTimeModeCheckBox.Size = new System.Drawing.Size(206, 21);
            this.activateTimeModeCheckBox.TabIndex = 1;
            this.activateTimeModeCheckBox.Text = "Activate between time mode";
            this.toolTip1.SetToolTip(this.activateTimeModeCheckBox, resources.GetString("activateTimeModeCheckBox.ToolTip"));
            this.activateTimeModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(61, 139);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(323, 21);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Remember the values and re-open the window";
            this.toolTip1.SetToolTip(this.checkBox2, "Remebers the values and re-opens the window after\r\nadding SVs into the map once, " +
        "with the same values.");
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Copy first time: ";
            this.toolTip1.SetToolTip(this.label2, "The start time of the SV change section. Examples:\r\n01:42:169 - \r\n02:34:223 (10) " +
        "- \r\n03:54:480");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Set first SV: ";
            this.toolTip1.SetToolTip(this.label3, "First SV value. Should be obvious.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Set last SV: ";
            this.toolTip1.SetToolTip(this.label4, "Last SV value. Can be lower than first SV, or higher than last SV, \r\nor equal if " +
        "Target BPM is defined.");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Copy last time: ";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Target BPM: ";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 373);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 17);
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
            this.putPointsByNotesCheckBox.Location = new System.Drawing.Point(137, 436);
            this.putPointsByNotesCheckBox.Name = "putPointsByNotesCheckBox";
            this.putPointsByNotesCheckBox.Size = new System.Drawing.Size(186, 21);
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
            this.increaseModeComboBox.Location = new System.Drawing.Point(153, 175);
            this.increaseModeComboBox.Name = "increaseModeComboBox";
            this.increaseModeComboBox.Size = new System.Drawing.Size(295, 24);
            this.increaseModeComboBox.TabIndex = 16;
            this.increaseModeComboBox.SelectedIndexChanged += new System.EventHandler(this.increaseModeComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "SV increase mode: ";
            this.toolTip1.SetToolTip(this.label8, "Selects the SV increase mode. Default is linear.");
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(124, 486);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(213, 30);
            this.applyButton.TabIndex = 18;
            this.applyButton.Text = "Apply!";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 401);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "SV offset: ";
            this.toolTip1.SetToolTip(this.label9, "The offsets of the inherited points depending on \r\nthe original change. Values wi" +
        "ll not be affected,\r\nonly the offsets of them will be changed. Must\r\nbe between " +
        "-10 and 0 and has to be an integer.");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 207);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 17);
            this.label10.TabIndex = 25;
            this.label10.Text = "Increase multiplier:";
            this.toolTip1.SetToolTip(this.label10, "This defines the increase multiplier. Only effective if \"Exponential\" or\r\n\"Logari" +
        "thmic\" is selected.");
            // 
            // increaseMultiplierTextBox
            // 
            this.increaseMultiplierTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.increaseMultiplierTextBox.Enabled = false;
            this.increaseMultiplierTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.increaseMultiplierTextBox.ForeColor = System.Drawing.Color.Gray;
            this.increaseMultiplierTextBox.Location = new System.Drawing.Point(153, 205);
            this.increaseMultiplierTextBox.Name = "increaseMultiplierTextBox";
            this.increaseMultiplierTextBox.PlaceHolderText = "Defines the exponential increase rate. Go higher value for steeper results.";
            this.increaseMultiplierTextBox.Size = new System.Drawing.Size(295, 22);
            this.increaseMultiplierTextBox.TabIndex = 26;
            this.increaseMultiplierTextBox.Text = "1";
            // 
            // lastTimeTextBox
            // 
            this.lastTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.lastTimeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.lastTimeTextBox.Location = new System.Drawing.Point(153, 316);
            this.lastTimeTextBox.Name = "lastTimeTextBox";
            this.lastTimeTextBox.PlaceHolderText = "Copy the last point offset.";
            this.lastTimeTextBox.Size = new System.Drawing.Size(295, 22);
            this.lastTimeTextBox.TabIndex = 24;
            this.lastTimeTextBox.Text = "Copy the last point offset.";
            // 
            // lastSvTextBox
            // 
            this.lastSvTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastSvTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.lastSvTextBox.ForeColor = System.Drawing.Color.Gray;
            this.lastSvTextBox.Location = new System.Drawing.Point(153, 289);
            this.lastSvTextBox.Name = "lastSvTextBox";
            this.lastSvTextBox.PlaceHolderText = "Set last SV value. Has to be above 0.";
            this.lastSvTextBox.Size = new System.Drawing.Size(295, 22);
            this.lastSvTextBox.TabIndex = 23;
            this.lastSvTextBox.Text = "Set last SV value. Has to be above 0.";
            // 
            // firstSvTextBox
            // 
            this.firstSvTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstSvTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.firstSvTextBox.ForeColor = System.Drawing.Color.Gray;
            this.firstSvTextBox.Location = new System.Drawing.Point(153, 261);
            this.firstSvTextBox.Name = "firstSvTextBox";
            this.firstSvTextBox.PlaceHolderText = "Set first SV value. Has to be above 0.";
            this.firstSvTextBox.Size = new System.Drawing.Size(295, 22);
            this.firstSvTextBox.TabIndex = 22;
            this.firstSvTextBox.Text = "Set first SV value. Has to be above 0.";
            // 
            // firstTimeTextBox
            // 
            this.firstTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.firstTimeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.firstTimeTextBox.Location = new System.Drawing.Point(153, 233);
            this.firstTimeTextBox.Name = "firstTimeTextBox";
            this.firstTimeTextBox.PlaceHolderText = "Copy the time of the SV change start.";
            this.firstTimeTextBox.Size = new System.Drawing.Size(295, 22);
            this.firstTimeTextBox.TabIndex = 21;
            this.firstTimeTextBox.Text = "Copy the time of the SV change start.";
            // 
            // svOffsetTextBox
            // 
            this.svOffsetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.svOffsetTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.svOffsetTextBox.ForeColor = System.Drawing.Color.Gray;
            this.svOffsetTextBox.Location = new System.Drawing.Point(153, 398);
            this.svOffsetTextBox.Name = "svOffsetTextBox";
            this.svOffsetTextBox.PlaceHolderText = "e.g. 1/4. All snaps are supported.";
            this.svOffsetTextBox.Size = new System.Drawing.Size(295, 22);
            this.svOffsetTextBox.TabIndex = 20;
            this.svOffsetTextBox.Text = "Shifts the additional points. Default is 0.";
            // 
            // gridSnapTextBox
            // 
            this.gridSnapTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSnapTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.gridSnapTextBox.Enabled = false;
            this.gridSnapTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.gridSnapTextBox.ForeColor = System.Drawing.Color.Gray;
            this.gridSnapTextBox.Location = new System.Drawing.Point(153, 370);
            this.gridSnapTextBox.Name = "gridSnapTextBox";
            this.gridSnapTextBox.PlaceHolderText = "(Optional) e.g. 1/4. All snaps are supported.";
            this.gridSnapTextBox.Size = new System.Drawing.Size(295, 22);
            this.gridSnapTextBox.TabIndex = 14;
            this.gridSnapTextBox.Text = "(Optional) e.g. 1/4. All snaps are supported.";
            // 
            // targetBpmTextBox
            // 
            this.targetBpmTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetBpmTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.targetBpmTextBox.ForeColor = System.Drawing.Color.Gray;
            this.targetBpmTextBox.Location = new System.Drawing.Point(153, 342);
            this.targetBpmTextBox.Name = "targetBpmTextBox";
            this.targetBpmTextBox.PlaceHolderText = "Optional, current BPM is first.";
            this.targetBpmTextBox.Size = new System.Drawing.Size(295, 22);
            this.targetBpmTextBox.TabIndex = 12;
            this.targetBpmTextBox.Text = "Optional, current BPM is first.";
            // 
            // SvChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 528);
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
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.activateTimeModeCheckBox);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(478, 551);
            this.Name = "SvChanger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SV Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox activateTimeModeCheckBox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
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