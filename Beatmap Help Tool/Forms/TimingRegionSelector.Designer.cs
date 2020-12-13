namespace Beatmap_Help_Tool.Forms
{
    partial class TimingRegionSelector
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
            this.functionNameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.lastTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.firstTimeTextBox = new Beatmap_Help_Tool.Views.PlaceHolderTextBox();
            this.SuspendLayout();
            // 
            // functionNameLabel
            // 
            this.functionNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.functionNameLabel.Location = new System.Drawing.Point(10, 11);
            this.functionNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.functionNameLabel.Name = "functionNameLabel";
            this.functionNameLabel.Size = new System.Drawing.Size(307, 80);
            this.functionNameLabel.TabIndex = 0;
            this.functionNameLabel.Text = "Enter the region by copying the offsets and it will send you back to the selected" +
    " function: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 93);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "First Offset:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Last Offset:";
            // 
            // applyButton
            // 
            this.applyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.applyButton.Location = new System.Drawing.Point(118, 190);
            this.applyButton.Margin = new System.Windows.Forms.Padding(2);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(86, 22);
            this.applyButton.TabIndex = 5;
            this.applyButton.Text = "Apply!";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // lastTimeTextBox
            // 
            this.lastTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lastTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.lastTimeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.lastTimeTextBox.Location = new System.Drawing.Point(78, 117);
            this.lastTimeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.lastTimeTextBox.Name = "lastTimeTextBox";
            this.lastTimeTextBox.PlaceHolderText = "Copy time as in 00:00:000 -";
            this.lastTimeTextBox.Size = new System.Drawing.Size(239, 19);
            this.lastTimeTextBox.TabIndex = 7;
            // 
            // firstTimeTextBox
            // 
            this.firstTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.firstTimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic);
            this.firstTimeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.firstTimeTextBox.Location = new System.Drawing.Point(78, 93);
            this.firstTimeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.firstTimeTextBox.Name = "firstTimeTextBox";
            this.firstTimeTextBox.PlaceHolderText = "Copy time as in 00:00:000 -";
            this.firstTimeTextBox.Size = new System.Drawing.Size(239, 19);
            this.firstTimeTextBox.TabIndex = 6;
            // 
            // TimingRegionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 228);
            this.Controls.Add(this.lastTimeTextBox);
            this.Controls.Add(this.firstTimeTextBox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.functionNameLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(343, 267);
            this.Name = "TimingRegionSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TimingRegionSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label functionNameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button applyButton;
        private Views.PlaceHolderTextBox firstTimeTextBox;
        private Views.PlaceHolderTextBox lastTimeTextBox;
    }
}