
namespace Beatmap_Help_Tool.Forms
{
    partial class ChangerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.typeChangeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.extraOptionCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.allTaikoDiffsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 75);
            this.label1.TabIndex = 0;
            this.label1.Text = "Helps to change the value of item(s). Hover over to \"Type change\" to learn how th" +
    "e value will be changed.";
            // 
            // typeChangeLabel
            // 
            this.typeChangeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeChangeLabel.Location = new System.Drawing.Point(13, 88);
            this.typeChangeLabel.Name = "typeChangeLabel";
            this.typeChangeLabel.Size = new System.Drawing.Size(276, 23);
            this.typeChangeLabel.TabIndex = 1;
            this.typeChangeLabel.Text = "Type change: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Value: ";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTextBox.Location = new System.Drawing.Point(100, 114);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(189, 22);
            this.valueTextBox.TabIndex = 3;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.applyButton.Location = new System.Drawing.Point(86, 297);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(128, 29);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply!";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // extraOptionCheckBox
            // 
            this.extraOptionCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extraOptionCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.extraOptionCheckBox.Location = new System.Drawing.Point(16, 197);
            this.extraOptionCheckBox.Name = "extraOptionCheckBox";
            this.extraOptionCheckBox.Size = new System.Drawing.Size(276, 74);
            this.extraOptionCheckBox.TabIndex = 5;
            this.extraOptionCheckBox.Text = "checkBox1";
            this.extraOptionCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.extraOptionCheckBox.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.AutoPopDelay = 32767;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // allTaikoDiffsCheckBox
            // 
            this.allTaikoDiffsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.allTaikoDiffsCheckBox.Location = new System.Drawing.Point(16, 159);
            this.allTaikoDiffsCheckBox.Name = "allTaikoDiffsCheckBox";
            this.allTaikoDiffsCheckBox.Size = new System.Drawing.Size(273, 21);
            this.allTaikoDiffsCheckBox.TabIndex = 6;
            this.allTaikoDiffsCheckBox.Text = "Apply to all Taiko difficulties";
            this.allTaikoDiffsCheckBox.UseVisualStyleBackColor = true;
            // 
            // ChangerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 338);
            this.Controls.Add(this.allTaikoDiffsCheckBox);
            this.Controls.Add(this.extraOptionCheckBox);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.typeChangeLabel);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(319, 321);
            this.Name = "ChangerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label typeChangeLabel;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox valueTextBox;
        public System.Windows.Forms.Button applyButton;
        public System.Windows.Forms.CheckBox extraOptionCheckBox;
        public System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox allTaikoDiffsCheckBox;
    }
}