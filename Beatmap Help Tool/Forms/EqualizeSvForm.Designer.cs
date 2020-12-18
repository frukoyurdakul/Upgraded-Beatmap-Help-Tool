
namespace Beatmap_Help_Tool.Forms
{
    partial class EqualizeSvForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EqualizeSvForm));
            this.label1 = new System.Windows.Forms.Label();
            this.equalizeAllCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.useRelativeSvCheckBox = new System.Windows.Forms.CheckBox();
            this.rememberCheckBox = new System.Windows.Forms.CheckBox();
            this.svMultiplierTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.targetBpmTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.endTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.startTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adjust the settings to equalize SV here. Hover over to the items to get detailed " +
    "info.";
            // 
            // equalizeAllCheckBox
            // 
            this.equalizeAllCheckBox.Checked = true;
            this.equalizeAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.equalizeAllCheckBox.Location = new System.Drawing.Point(16, 52);
            this.equalizeAllCheckBox.Name = "equalizeAllCheckBox";
            this.equalizeAllCheckBox.Size = new System.Drawing.Size(174, 24);
            this.equalizeAllCheckBox.TabIndex = 1;
            this.equalizeAllCheckBox.Text = "Equalize the entirety of the map";
            this.toolTip1.SetToolTip(this.equalizeAllCheckBox, "Applies the equalize SV method to the entire\r\nmap if this is checked. Unchecking " +
        "it will\r\nunlock the start - end time text boxes below.");
            this.equalizeAllCheckBox.UseVisualStyleBackColor = true;
            this.equalizeAllCheckBox.CheckedChanged += new System.EventHandler(this.useFullMapCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "End time:";
            // 
            // applyButton
            // 
            this.applyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.applyButton.Location = new System.Drawing.Point(89, 256);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(103, 23);
            this.applyButton.TabIndex = 6;
            this.applyButton.Text = "Apply!";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 32767;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Target BPM:";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "SV Multiplier:";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // useRelativeSvCheckBox
            // 
            this.useRelativeSvCheckBox.Location = new System.Drawing.Point(16, 82);
            this.useRelativeSvCheckBox.Name = "useRelativeSvCheckBox";
            this.useRelativeSvCheckBox.Size = new System.Drawing.Size(245, 24);
            this.useRelativeSvCheckBox.TabIndex = 11;
            this.useRelativeSvCheckBox.Text = "Use relative SV based on existing green points";
            this.toolTip1.SetToolTip(this.useRelativeSvCheckBox, resources.GetString("useRelativeSvCheckBox.ToolTip"));
            this.useRelativeSvCheckBox.UseVisualStyleBackColor = true;
            // 
            // rememberCheckBox
            // 
            this.rememberCheckBox.Location = new System.Drawing.Point(16, 112);
            this.rememberCheckBox.Name = "rememberCheckBox";
            this.rememberCheckBox.Size = new System.Drawing.Size(171, 24);
            this.rememberCheckBox.TabIndex = 12;
            this.rememberCheckBox.Text = "Remember values and options";
            this.toolTip1.SetToolTip(this.rememberCheckBox, resources.GetString("rememberCheckBox.ToolTip"));
            this.rememberCheckBox.UseVisualStyleBackColor = true;
            // 
            // svMultiplierTextBox
            // 
            this.svMultiplierTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.svMultiplierTextBox.Enabled = false;
            this.svMultiplierTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.svMultiplierTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.svMultiplierTextBox.Location = new System.Drawing.Point(90, 227);
            this.svMultiplierTextBox.Name = "svMultiplierTextBox";
            this.svMultiplierTextBox.PlaceHolderText = "Base SV multiplier in format of 1.10 or 1";
            this.svMultiplierTextBox.Size = new System.Drawing.Size(178, 20);
            this.svMultiplierTextBox.TabIndex = 10;
            this.toolTip1.SetToolTip(this.svMultiplierTextBox, resources.GetString("svMultiplierTextBox.ToolTip"));
            // 
            // targetBpmTextBox
            // 
            this.targetBpmTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetBpmTextBox.Enabled = false;
            this.targetBpmTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.targetBpmTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.targetBpmTextBox.Location = new System.Drawing.Point(90, 201);
            this.targetBpmTextBox.Name = "targetBpmTextBox";
            this.targetBpmTextBox.PlaceHolderText = "Optional, current BPM is first.";
            this.targetBpmTextBox.Size = new System.Drawing.Size(178, 20);
            this.targetBpmTextBox.TabIndex = 8;
            this.toolTip1.SetToolTip(this.targetBpmTextBox, resources.GetString("targetBpmTextBox.ToolTip"));
            // 
            // endTimeTextBox
            // 
            this.endTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endTimeTextBox.Enabled = false;
            this.endTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.endTimeTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.endTimeTextBox.Location = new System.Drawing.Point(90, 175);
            this.endTimeTextBox.Name = "endTimeTextBox";
            this.endTimeTextBox.PlaceHolderText = "Copy time as in 00:00:000 -";
            this.endTimeTextBox.Size = new System.Drawing.Size(178, 20);
            this.endTimeTextBox.TabIndex = 5;
            // 
            // startTimeTextBox
            // 
            this.startTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startTimeTextBox.Enabled = false;
            this.startTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startTimeTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.startTimeTextBox.Location = new System.Drawing.Point(90, 149);
            this.startTimeTextBox.Name = "startTimeTextBox";
            this.startTimeTextBox.PlaceHolderText = "Copy time as in 00:00:000 -";
            this.startTimeTextBox.Size = new System.Drawing.Size(178, 20);
            this.startTimeTextBox.TabIndex = 4;
            // 
            // EqualizeSvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 291);
            this.Controls.Add(this.rememberCheckBox);
            this.Controls.Add(this.useRelativeSvCheckBox);
            this.Controls.Add(this.svMultiplierTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.targetBpmTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.endTimeTextBox);
            this.Controls.Add(this.startTimeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.equalizeAllCheckBox);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(296, 330);
            this.Name = "EqualizeSvForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EqualizeSvForm";
            this.Load += new System.EventHandler(this.EqualizeSvForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox equalizeAllCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Views.PlaceHolderTextBox startTimeTextBox;
        private Views.PlaceHolderTextBox endTimeTextBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private Views.PlaceHolderTextBox targetBpmTextBox;
        private System.Windows.Forms.Label label4;
        private Views.PlaceHolderTextBox svMultiplierTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox useRelativeSvCheckBox;
        private System.Windows.Forms.CheckBox rememberCheckBox;
    }
}