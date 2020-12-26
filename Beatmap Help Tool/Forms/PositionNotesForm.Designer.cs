namespace Beatmap_Help_Tool.Forms
{
    partial class PositionNotesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.templatesComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.katPositionTextBox = new System.Windows.Forms.TextBox();
            this.donFinisherPositionTextBox = new System.Windows.Forms.TextBox();
            this.donPositionTextBox = new System.Windows.Forms.TextBox();
            this.katFinisherPositionTextBox = new System.Windows.Forms.TextBox();
            this.arrangeNotesButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.allTaikoDiffsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Use template: ";
            // 
            // templatesComboBox
            // 
            this.templatesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.templatesComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.templatesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templatesComboBox.FormattingEnabled = true;
            this.templatesComboBox.Items.AddRange(new object[] {
            "Default",
            "Close to origin",
            "At corners"});
            this.templatesComboBox.Location = new System.Drawing.Point(119, 91);
            this.templatesComboBox.Name = "templatesComboBox";
            this.templatesComboBox.Size = new System.Drawing.Size(177, 24);
            this.templatesComboBox.TabIndex = 1;
            this.templatesComboBox.SelectedIndexChanged += new System.EventHandler(this.templatesComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Don position: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Kat position: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Kat finisher position: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Don finisher position: ";
            // 
            // katPositionTextBox
            // 
            this.katPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.katPositionTextBox.Location = new System.Drawing.Point(168, 166);
            this.katPositionTextBox.Name = "katPositionTextBox";
            this.katPositionTextBox.Size = new System.Drawing.Size(128, 22);
            this.katPositionTextBox.TabIndex = 6;
            this.katPositionTextBox.Text = "352, 96";
            // 
            // donFinisherPositionTextBox
            // 
            this.donFinisherPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.donFinisherPositionTextBox.Location = new System.Drawing.Point(168, 194);
            this.donFinisherPositionTextBox.Name = "donFinisherPositionTextBox";
            this.donFinisherPositionTextBox.Size = new System.Drawing.Size(128, 22);
            this.donFinisherPositionTextBox.TabIndex = 7;
            this.donFinisherPositionTextBox.Text = "160, 288";
            // 
            // donPositionTextBox
            // 
            this.donPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.donPositionTextBox.Location = new System.Drawing.Point(168, 138);
            this.donPositionTextBox.Name = "donPositionTextBox";
            this.donPositionTextBox.Size = new System.Drawing.Size(128, 22);
            this.donPositionTextBox.TabIndex = 8;
            this.donPositionTextBox.Text = "160, 96";
            // 
            // katFinisherPositionTextBox
            // 
            this.katFinisherPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.katFinisherPositionTextBox.Location = new System.Drawing.Point(168, 222);
            this.katFinisherPositionTextBox.Name = "katFinisherPositionTextBox";
            this.katFinisherPositionTextBox.Size = new System.Drawing.Size(128, 22);
            this.katFinisherPositionTextBox.TabIndex = 9;
            this.katFinisherPositionTextBox.Text = "352, 288";
            // 
            // arrangeNotesButton
            // 
            this.arrangeNotesButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.arrangeNotesButton.Location = new System.Drawing.Point(73, 311);
            this.arrangeNotesButton.Name = "arrangeNotesButton";
            this.arrangeNotesButton.Size = new System.Drawing.Size(162, 28);
            this.arrangeNotesButton.TabIndex = 10;
            this.arrangeNotesButton.Text = "Arrange Notes";
            this.arrangeNotesButton.UseVisualStyleBackColor = true;
            this.arrangeNotesButton.Click += new System.EventHandler(this.arrangeNotesButton_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(13, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(283, 51);
            this.label6.TabIndex = 11;
            this.label6.Text = "Positions the notes in editor for Taiko mode. The maximum allowed range is from 3" +
    "2, 32 to 512, 384 where x, y is specified.";
            // 
            // allTaikoDiffsCheckBox
            // 
            this.allTaikoDiffsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.allTaikoDiffsCheckBox.AutoSize = true;
            this.allTaikoDiffsCheckBox.Checked = true;
            this.allTaikoDiffsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allTaikoDiffsCheckBox.Location = new System.Drawing.Point(52, 270);
            this.allTaikoDiffsCheckBox.Name = "allTaikoDiffsCheckBox";
            this.allTaikoDiffsCheckBox.Size = new System.Drawing.Size(204, 21);
            this.allTaikoDiffsCheckBox.TabIndex = 12;
            this.allTaikoDiffsCheckBox.Text = "Apply to all Taiko difficulties";
            this.allTaikoDiffsCheckBox.UseVisualStyleBackColor = true;
            // 
            // PositionNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 351);
            this.Controls.Add(this.allTaikoDiffsCheckBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.arrangeNotesButton);
            this.Controls.Add(this.katFinisherPositionTextBox);
            this.Controls.Add(this.donPositionTextBox);
            this.Controls.Add(this.donFinisherPositionTextBox);
            this.Controls.Add(this.katPositionTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.templatesComboBox);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(326, 349);
            this.Name = "PositionNotesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Position Notes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox templatesComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox katPositionTextBox;
        private System.Windows.Forms.TextBox donFinisherPositionTextBox;
        private System.Windows.Forms.TextBox donPositionTextBox;
        private System.Windows.Forms.TextBox katFinisherPositionTextBox;
        private System.Windows.Forms.Button arrangeNotesButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox allTaikoDiffsCheckBox;
    }
}