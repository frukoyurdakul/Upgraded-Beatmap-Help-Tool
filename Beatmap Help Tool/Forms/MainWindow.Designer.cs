using Beatmap_Help_Tool.Views;
using System.Windows.Forms;

namespace Beatmap_Help_Tool
{
    partial class MainWindow
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalFunctionsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.loadCurrentBeatmapButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel13 = new System.Windows.Forms.Panel();
            this.playTaikoDiffsButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel12 = new System.Windows.Forms.Panel();
            this.snapGreenToRedPointsButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel11 = new System.Windows.Forms.Panel();
            this.positionAllNotesButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.whistleToClapButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.button10 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel18 = new System.Windows.Forms.Panel();
            this.button9 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel17 = new System.Windows.Forms.Panel();
            this.button36 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel16 = new System.Windows.Forms.Panel();
            this.button37 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel15 = new System.Windows.Forms.Panel();
            this.button35 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.svFunctionsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button14 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel20 = new System.Windows.Forms.Panel();
            this.button15 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel21 = new System.Windows.Forms.Panel();
            this.button16 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel22 = new System.Windows.Forms.Panel();
            this.equalizeSvForAllPointsButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel23 = new System.Windows.Forms.Panel();
            this.svChangerButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel24 = new System.Windows.Forms.Panel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.button19 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel26 = new System.Windows.Forms.Panel();
            this.button20 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel27 = new System.Windows.Forms.Panel();
            this.button21 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel28 = new System.Windows.Forms.Panel();
            this.button22 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel29 = new System.Windows.Forms.Panel();
            this.button23 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.bpmFunctionsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel30 = new System.Windows.Forms.Panel();
            this.button25 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel31 = new System.Windows.Forms.Panel();
            this.button26 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel32 = new System.Windows.Forms.Panel();
            this.button27 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel33 = new System.Windows.Forms.Panel();
            this.button28 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel34 = new System.Windows.Forms.Panel();
            this.panel35 = new System.Windows.Forms.Panel();
            this.button29 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel36 = new System.Windows.Forms.Panel();
            this.button30 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel37 = new System.Windows.Forms.Panel();
            this.button31 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel38 = new System.Windows.Forms.Panel();
            this.button32 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel39 = new System.Windows.Forms.Panel();
            this.button33 = new Beatmap_Help_Tool.Views.MultilineButton();
            this.nominationFunctionsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.unsnappedNoteBarlineButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel46 = new System.Windows.Forms.Panel();
            this.flyingBarlinesButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel45 = new System.Windows.Forms.Panel();
            this.checkDoubleBarlinesButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel44 = new System.Windows.Forms.Panel();
            this.timingInconsistenciesButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.panel43 = new System.Windows.Forms.Panel();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel42 = new System.Windows.Forms.Panel();
            this.runningProcessLabel = new System.Windows.Forms.Label();
            this.panel41 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel40 = new System.Windows.Forms.Panel();
            this.lastSaveTimeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.browseButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.redoButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.saveButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.undoButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.mainDisplayView = new Beatmap_Help_Tool.Views.DoubleBufferGridView();
            this.timeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bpmColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.meterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volumeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kiaiColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.inheritedPointsButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.timingPointsButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.allPointsButton = new Beatmap_Help_Tool.Views.MultilineButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.generalFunctionsPage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel15.SuspendLayout();
            this.svFunctionsPage.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel21.SuspendLayout();
            this.panel22.SuspendLayout();
            this.panel23.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel26.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel28.SuspendLayout();
            this.panel29.SuspendLayout();
            this.bpmFunctionsPage.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel30.SuspendLayout();
            this.panel31.SuspendLayout();
            this.panel32.SuspendLayout();
            this.panel33.SuspendLayout();
            this.panel34.SuspendLayout();
            this.panel35.SuspendLayout();
            this.panel36.SuspendLayout();
            this.panel37.SuspendLayout();
            this.panel38.SuspendLayout();
            this.panel39.SuspendLayout();
            this.nominationFunctionsPage.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel46.SuspendLayout();
            this.panel45.SuspendLayout();
            this.panel44.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel42.SuspendLayout();
            this.panel41.SuspendLayout();
            this.panel40.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplayView)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.mainDisplayView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 183F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(583, 449);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.generalFunctionsPage);
            this.tabControl1.Controls.Add(this.svFunctionsPage);
            this.tabControl1.Controls.Add(this.bpmFunctionsPage);
            this.tabControl1.Controls.Add(this.nominationFunctionsPage);
            this.tabControl1.Controls.Add(this.settingsPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(2, 268);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 4;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(579, 179);
            this.tabControl1.TabIndex = 1;
            // 
            // generalFunctionsPage
            // 
            this.generalFunctionsPage.Controls.Add(this.tableLayoutPanel2);
            this.generalFunctionsPage.Location = new System.Drawing.Point(4, 4);
            this.generalFunctionsPage.Margin = new System.Windows.Forms.Padding(2);
            this.generalFunctionsPage.Name = "generalFunctionsPage";
            this.generalFunctionsPage.Padding = new System.Windows.Forms.Padding(2);
            this.generalFunctionsPage.Size = new System.Drawing.Size(571, 153);
            this.generalFunctionsPage.TabIndex = 0;
            this.generalFunctionsPage.Text = "General Functions";
            this.generalFunctionsPage.ToolTipText = "Contains functions that are related to slider velocity changes.";
            this.generalFunctionsPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(567, 149);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panel14);
            this.panel1.Controls.Add(this.panel13);
            this.panel1.Controls.Add(this.panel12);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 145);
            this.panel1.TabIndex = 0;
            this.panel1.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // panel14
            // 
            this.panel14.AutoSize = true;
            this.panel14.Controls.Add(this.loadCurrentBeatmapButton);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(0, 129);
            this.panel14.Margin = new System.Windows.Forms.Padding(2);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel14.Size = new System.Drawing.Size(262, 31);
            this.panel14.TabIndex = 10;
            // 
            // loadCurrentBeatmapButton
            // 
            this.loadCurrentBeatmapButton.AutoSize = true;
            this.loadCurrentBeatmapButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loadCurrentBeatmapButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadCurrentBeatmapButton.Location = new System.Drawing.Point(0, 8);
            this.loadCurrentBeatmapButton.Margin = new System.Windows.Forms.Padding(0);
            this.loadCurrentBeatmapButton.Name = "loadCurrentBeatmapButton";
            this.loadCurrentBeatmapButton.Size = new System.Drawing.Size(262, 23);
            this.loadCurrentBeatmapButton.TabIndex = 3;
            this.loadCurrentBeatmapButton.TabStop = false;
            this.loadCurrentBeatmapButton.Text = "Find current open beatmap in editor and load";
            this.toolTip1.SetToolTip(this.loadCurrentBeatmapButton, "Loads the beatmap into the program if any\r\nmaps are open in the editor. If the sa" +
        "me map\r\nis open, it will be re-loaded.");
            this.loadCurrentBeatmapButton.UseVisualStyleBackColor = true;
            this.loadCurrentBeatmapButton.Click += new System.EventHandler(this.loadCurrentBeatmapButton_Click);
            // 
            // panel13
            // 
            this.panel13.AutoSize = true;
            this.panel13.Controls.Add(this.playTaikoDiffsButton);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 98);
            this.panel13.Margin = new System.Windows.Forms.Padding(2);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel13.Size = new System.Drawing.Size(262, 31);
            this.panel13.TabIndex = 9;
            // 
            // playTaikoDiffsButton
            // 
            this.playTaikoDiffsButton.AutoSize = true;
            this.playTaikoDiffsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playTaikoDiffsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.playTaikoDiffsButton.Location = new System.Drawing.Point(0, 8);
            this.playTaikoDiffsButton.Margin = new System.Windows.Forms.Padding(0);
            this.playTaikoDiffsButton.Name = "playTaikoDiffsButton";
            this.playTaikoDiffsButton.Size = new System.Drawing.Size(262, 23);
            this.playTaikoDiffsButton.TabIndex = 3;
            this.playTaikoDiffsButton.TabStop = false;
            this.playTaikoDiffsButton.Text = "Play taiko diffs simultaneously";
            this.toolTip1.SetToolTip(this.playTaikoDiffsButton, "Opens a window where you can view every diff\r\nsimultaneously while the music is p" +
        "laying.\r\nCurrently it\'s really primitive but will be improved\r\nas I find the mot" +
        "ivation.");
            this.playTaikoDiffsButton.UseVisualStyleBackColor = true;
            this.playTaikoDiffsButton.Click += new System.EventHandler(this.playTaikoDiffsButton_Click);
            // 
            // panel12
            // 
            this.panel12.AutoSize = true;
            this.panel12.Controls.Add(this.snapGreenToRedPointsButton);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 54);
            this.panel12.Margin = new System.Windows.Forms.Padding(2);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel12.Size = new System.Drawing.Size(262, 44);
            this.panel12.TabIndex = 8;
            // 
            // snapGreenToRedPointsButton
            // 
            this.snapGreenToRedPointsButton.AutoSize = true;
            this.snapGreenToRedPointsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.snapGreenToRedPointsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.snapGreenToRedPointsButton.Location = new System.Drawing.Point(0, 8);
            this.snapGreenToRedPointsButton.Margin = new System.Windows.Forms.Padding(0);
            this.snapGreenToRedPointsButton.Name = "snapGreenToRedPointsButton";
            this.snapGreenToRedPointsButton.Size = new System.Drawing.Size(262, 36);
            this.snapGreenToRedPointsButton.TabIndex = 3;
            this.snapGreenToRedPointsButton.TabStop = false;
            this.snapGreenToRedPointsButton.Text = "Snap green points onto red points in a\r\nspecific region";
            this.toolTip1.SetToolTip(this.snapGreenToRedPointsButton, resources.GetString("snapGreenToRedPointsButton.ToolTip"));
            this.snapGreenToRedPointsButton.UseVisualStyleBackColor = true;
            this.snapGreenToRedPointsButton.Click += new System.EventHandler(this.snapGreenToRedPointsButton_Click);
            // 
            // panel11
            // 
            this.panel11.AutoSize = true;
            this.panel11.Controls.Add(this.positionAllNotesButton);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 23);
            this.panel11.Margin = new System.Windows.Forms.Padding(2);
            this.panel11.Name = "panel11";
            this.panel11.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel11.Size = new System.Drawing.Size(262, 31);
            this.panel11.TabIndex = 7;
            // 
            // positionAllNotesButton
            // 
            this.positionAllNotesButton.AutoSize = true;
            this.positionAllNotesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.positionAllNotesButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.positionAllNotesButton.Location = new System.Drawing.Point(0, 8);
            this.positionAllNotesButton.Margin = new System.Windows.Forms.Padding(0);
            this.positionAllNotesButton.Name = "positionAllNotesButton";
            this.positionAllNotesButton.Size = new System.Drawing.Size(262, 23);
            this.positionAllNotesButton.TabIndex = 3;
            this.positionAllNotesButton.TabStop = false;
            this.positionAllNotesButton.Text = "Position all notes (Taiko mode)";
            this.toolTip1.SetToolTip(this.positionAllNotesButton, "Puts the notes by their type. By default, it puts dons are top-left, kats are top" +
        "-right,\ndon finishers are bottom-left and kat finishers are bottom-right side.\nY" +
        "ou can customize the positions, though.");
            this.positionAllNotesButton.UseVisualStyleBackColor = true;
            this.positionAllNotesButton.Click += new System.EventHandler(this.positionAllNotesButton_Click);
            // 
            // panel10
            // 
            this.panel10.AutoSize = true;
            this.panel10.Controls.Add(this.whistleToClapButton);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Margin = new System.Windows.Forms.Padding(2);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(262, 23);
            this.panel10.TabIndex = 0;
            // 
            // whistleToClapButton
            // 
            this.whistleToClapButton.AutoSize = true;
            this.whistleToClapButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.whistleToClapButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.whistleToClapButton.Location = new System.Drawing.Point(0, 0);
            this.whistleToClapButton.Margin = new System.Windows.Forms.Padding(0);
            this.whistleToClapButton.Name = "whistleToClapButton";
            this.whistleToClapButton.Size = new System.Drawing.Size(262, 23);
            this.whistleToClapButton.TabIndex = 2;
            this.whistleToClapButton.TabStop = false;
            this.whistleToClapButton.Text = "Set whistle sounds to claps";
            this.toolTip1.SetToolTip(this.whistleToClapButton, "In the beatmap, sets all the whistle sounds to claps, including finishers. \r\nGood" +
        " for mappers that hate whistle sounds while listening hitsounds in the editor :)" +
        "");
            this.whistleToClapButton.UseVisualStyleBackColor = true;
            this.whistleToClapButton.Click += new System.EventHandler(this.whistleToClapButton_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.panel19);
            this.panel2.Controls.Add(this.panel18);
            this.panel2.Controls.Add(this.panel17);
            this.panel2.Controls.Add(this.panel16);
            this.panel2.Controls.Add(this.panel15);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(285, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(280, 145);
            this.panel2.TabIndex = 1;
            this.panel2.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // panel19
            // 
            this.panel19.AutoSize = true;
            this.panel19.Controls.Add(this.button10);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel19.Location = new System.Drawing.Point(0, 116);
            this.panel19.Margin = new System.Windows.Forms.Padding(2);
            this.panel19.Name = "panel19";
            this.panel19.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel19.Size = new System.Drawing.Size(263, 31);
            this.panel19.TabIndex = 7;
            // 
            // button10
            // 
            this.button10.AutoSize = true;
            this.button10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button10.Dock = System.Windows.Forms.DockStyle.Top;
            this.button10.Location = new System.Drawing.Point(0, 8);
            this.button10.Margin = new System.Windows.Forms.Padding(0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(263, 23);
            this.button10.TabIndex = 3;
            this.button10.TabStop = false;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // panel18
            // 
            this.panel18.AutoSize = true;
            this.panel18.Controls.Add(this.button9);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel18.Location = new System.Drawing.Point(0, 85);
            this.panel18.Margin = new System.Windows.Forms.Padding(2);
            this.panel18.Name = "panel18";
            this.panel18.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel18.Size = new System.Drawing.Size(263, 31);
            this.panel18.TabIndex = 6;
            // 
            // button9
            // 
            this.button9.AutoSize = true;
            this.button9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button9.Dock = System.Windows.Forms.DockStyle.Top;
            this.button9.Location = new System.Drawing.Point(0, 8);
            this.button9.Margin = new System.Windows.Forms.Padding(0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(263, 23);
            this.button9.TabIndex = 3;
            this.button9.TabStop = false;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // panel17
            // 
            this.panel17.AutoSize = true;
            this.panel17.Controls.Add(this.button36);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(0, 54);
            this.panel17.Margin = new System.Windows.Forms.Padding(2);
            this.panel17.Name = "panel17";
            this.panel17.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel17.Size = new System.Drawing.Size(263, 31);
            this.panel17.TabIndex = 5;
            // 
            // button36
            // 
            this.button36.AutoSize = true;
            this.button36.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button36.Dock = System.Windows.Forms.DockStyle.Top;
            this.button36.Location = new System.Drawing.Point(0, 8);
            this.button36.Margin = new System.Windows.Forms.Padding(0);
            this.button36.Name = "button36";
            this.button36.Size = new System.Drawing.Size(263, 23);
            this.button36.TabIndex = 3;
            this.button36.TabStop = false;
            this.button36.Text = "button8";
            this.toolTip1.SetToolTip(this.button36, resources.GetString("button36.ToolTip"));
            this.button36.UseVisualStyleBackColor = true;
            // 
            // panel16
            // 
            this.panel16.AutoSize = true;
            this.panel16.Controls.Add(this.button37);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Location = new System.Drawing.Point(0, 23);
            this.panel16.Margin = new System.Windows.Forms.Padding(2);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel16.Size = new System.Drawing.Size(263, 31);
            this.panel16.TabIndex = 4;
            // 
            // button37
            // 
            this.button37.AutoSize = true;
            this.button37.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button37.Dock = System.Windows.Forms.DockStyle.Top;
            this.button37.Location = new System.Drawing.Point(0, 8);
            this.button37.Margin = new System.Windows.Forms.Padding(0);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(263, 23);
            this.button37.TabIndex = 3;
            this.button37.TabStop = false;
            this.button37.Text = "button7";
            this.toolTip1.SetToolTip(this.button37, "Loads the beatmap into the program if any\r\nmaps are open in the editor. If the sa" +
        "me map\r\nis open, it will be re-loaded.");
            this.button37.UseVisualStyleBackColor = true;
            // 
            // panel15
            // 
            this.panel15.AutoSize = true;
            this.panel15.Controls.Add(this.button35);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Margin = new System.Windows.Forms.Padding(2);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(263, 23);
            this.panel15.TabIndex = 0;
            // 
            // button35
            // 
            this.button35.AutoSize = true;
            this.button35.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button35.Dock = System.Windows.Forms.DockStyle.Top;
            this.button35.Location = new System.Drawing.Point(0, 0);
            this.button35.Margin = new System.Windows.Forms.Padding(0);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(263, 23);
            this.button35.TabIndex = 3;
            this.button35.TabStop = false;
            this.button35.Text = "button6";
            this.toolTip1.SetToolTip(this.button35, resources.GetString("button35.ToolTip"));
            this.button35.UseVisualStyleBackColor = true;
            // 
            // svFunctionsPage
            // 
            this.svFunctionsPage.Controls.Add(this.tableLayoutPanel3);
            this.svFunctionsPage.Location = new System.Drawing.Point(4, 4);
            this.svFunctionsPage.Margin = new System.Windows.Forms.Padding(2);
            this.svFunctionsPage.Name = "svFunctionsPage";
            this.svFunctionsPage.Padding = new System.Windows.Forms.Padding(2);
            this.svFunctionsPage.Size = new System.Drawing.Size(571, 153);
            this.svFunctionsPage.TabIndex = 1;
            this.svFunctionsPage.Text = "SV Functions";
            this.svFunctionsPage.ToolTipText = "Contains functions that are related to editor changes (re-placing notes on screen" +
    " etc.)";
            this.svFunctionsPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel24, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(567, 149);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel20);
            this.panel3.Controls.Add(this.panel21);
            this.panel3.Controls.Add(this.panel22);
            this.panel3.Controls.Add(this.panel23);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(279, 145);
            this.panel3.TabIndex = 0;
            this.panel3.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.button14);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 116);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel4.Size = new System.Drawing.Size(279, 29);
            this.panel4.TabIndex = 10;
            // 
            // button14
            // 
            this.button14.AutoSize = true;
            this.button14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button14.Dock = System.Windows.Forms.DockStyle.Top;
            this.button14.Location = new System.Drawing.Point(0, 8);
            this.button14.Margin = new System.Windows.Forms.Padding(0);
            this.button14.MinimumSize = new System.Drawing.Size(0, 19);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(279, 23);
            this.button14.TabIndex = 3;
            this.button14.TabStop = false;
            this.button14.Text = "button14";
            this.button14.UseVisualStyleBackColor = true;
            // 
            // panel20
            // 
            this.panel20.AutoSize = true;
            this.panel20.Controls.Add(this.button15);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel20.Location = new System.Drawing.Point(0, 85);
            this.panel20.Margin = new System.Windows.Forms.Padding(2);
            this.panel20.Name = "panel20";
            this.panel20.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel20.Size = new System.Drawing.Size(279, 31);
            this.panel20.TabIndex = 9;
            // 
            // button15
            // 
            this.button15.AutoSize = true;
            this.button15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button15.Dock = System.Windows.Forms.DockStyle.Top;
            this.button15.Location = new System.Drawing.Point(0, 8);
            this.button15.Margin = new System.Windows.Forms.Padding(0);
            this.button15.MinimumSize = new System.Drawing.Size(0, 19);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(279, 23);
            this.button15.TabIndex = 3;
            this.button15.TabStop = false;
            this.button15.Text = "button15";
            this.button15.UseVisualStyleBackColor = true;
            // 
            // panel21
            // 
            this.panel21.AutoSize = true;
            this.panel21.Controls.Add(this.button16);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel21.Location = new System.Drawing.Point(0, 54);
            this.panel21.Margin = new System.Windows.Forms.Padding(2);
            this.panel21.Name = "panel21";
            this.panel21.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel21.Size = new System.Drawing.Size(279, 31);
            this.panel21.TabIndex = 8;
            // 
            // button16
            // 
            this.button16.AutoSize = true;
            this.button16.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button16.Dock = System.Windows.Forms.DockStyle.Top;
            this.button16.Location = new System.Drawing.Point(0, 8);
            this.button16.Margin = new System.Windows.Forms.Padding(0);
            this.button16.MinimumSize = new System.Drawing.Size(0, 19);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(279, 23);
            this.button16.TabIndex = 3;
            this.button16.TabStop = false;
            this.button16.Text = "button16";
            this.button16.UseVisualStyleBackColor = true;
            // 
            // panel22
            // 
            this.panel22.AutoSize = true;
            this.panel22.Controls.Add(this.equalizeSvForAllPointsButton);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel22.Location = new System.Drawing.Point(0, 23);
            this.panel22.Margin = new System.Windows.Forms.Padding(2);
            this.panel22.Name = "panel22";
            this.panel22.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel22.Size = new System.Drawing.Size(279, 31);
            this.panel22.TabIndex = 7;
            // 
            // equalizeSvForAllPointsButton
            // 
            this.equalizeSvForAllPointsButton.AutoSize = true;
            this.equalizeSvForAllPointsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.equalizeSvForAllPointsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.equalizeSvForAllPointsButton.Location = new System.Drawing.Point(0, 8);
            this.equalizeSvForAllPointsButton.Margin = new System.Windows.Forms.Padding(0);
            this.equalizeSvForAllPointsButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.equalizeSvForAllPointsButton.Name = "equalizeSvForAllPointsButton";
            this.equalizeSvForAllPointsButton.Size = new System.Drawing.Size(279, 23);
            this.equalizeSvForAllPointsButton.TabIndex = 3;
            this.equalizeSvForAllPointsButton.TabStop = false;
            this.equalizeSvForAllPointsButton.Text = "Equalize SV for all points";
            this.toolTip1.SetToolTip(this.equalizeSvForAllPointsButton, resources.GetString("equalizeSvForAllPointsButton.ToolTip"));
            this.equalizeSvForAllPointsButton.UseVisualStyleBackColor = true;
            this.equalizeSvForAllPointsButton.Click += new System.EventHandler(this.equalizeSvForAllPointsButton_Click);
            // 
            // panel23
            // 
            this.panel23.AutoSize = true;
            this.panel23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel23.Controls.Add(this.svChangerButton);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Margin = new System.Windows.Forms.Padding(2);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(279, 23);
            this.panel23.TabIndex = 0;
            // 
            // svChangerButton
            // 
            this.svChangerButton.AutoSize = true;
            this.svChangerButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.svChangerButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.svChangerButton.Location = new System.Drawing.Point(0, 0);
            this.svChangerButton.Margin = new System.Windows.Forms.Padding(0);
            this.svChangerButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.svChangerButton.Name = "svChangerButton";
            this.svChangerButton.Size = new System.Drawing.Size(279, 23);
            this.svChangerButton.TabIndex = 2;
            this.svChangerButton.TabStop = false;
            this.svChangerButton.Text = "Add inherited points to change SV smoothly";
            this.toolTip1.SetToolTip(this.svChangerButton, resources.GetString("svChangerButton.ToolTip"));
            this.svChangerButton.UseVisualStyleBackColor = true;
            this.svChangerButton.Click += new System.EventHandler(this.svChangerButton_Click);
            // 
            // panel24
            // 
            this.panel24.AutoScroll = true;
            this.panel24.Controls.Add(this.panel25);
            this.panel24.Controls.Add(this.panel26);
            this.panel24.Controls.Add(this.panel27);
            this.panel24.Controls.Add(this.panel28);
            this.panel24.Controls.Add(this.panel29);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel24.Location = new System.Drawing.Point(285, 2);
            this.panel24.Margin = new System.Windows.Forms.Padding(2);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(280, 145);
            this.panel24.TabIndex = 1;
            this.panel24.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // panel25
            // 
            this.panel25.AutoSize = true;
            this.panel25.Controls.Add(this.button19);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel25.Location = new System.Drawing.Point(0, 116);
            this.panel25.Margin = new System.Windows.Forms.Padding(2);
            this.panel25.Name = "panel25";
            this.panel25.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel25.Size = new System.Drawing.Size(263, 31);
            this.panel25.TabIndex = 7;
            // 
            // button19
            // 
            this.button19.AutoSize = true;
            this.button19.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button19.Dock = System.Windows.Forms.DockStyle.Top;
            this.button19.Location = new System.Drawing.Point(0, 8);
            this.button19.Margin = new System.Windows.Forms.Padding(0);
            this.button19.MinimumSize = new System.Drawing.Size(0, 19);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(263, 23);
            this.button19.TabIndex = 3;
            this.button19.TabStop = false;
            this.button19.Text = "button19";
            this.button19.UseVisualStyleBackColor = true;
            // 
            // panel26
            // 
            this.panel26.AutoSize = true;
            this.panel26.Controls.Add(this.button20);
            this.panel26.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel26.Location = new System.Drawing.Point(0, 85);
            this.panel26.Margin = new System.Windows.Forms.Padding(2);
            this.panel26.Name = "panel26";
            this.panel26.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel26.Size = new System.Drawing.Size(263, 31);
            this.panel26.TabIndex = 6;
            // 
            // button20
            // 
            this.button20.AutoSize = true;
            this.button20.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button20.Dock = System.Windows.Forms.DockStyle.Top;
            this.button20.Location = new System.Drawing.Point(0, 8);
            this.button20.Margin = new System.Windows.Forms.Padding(0);
            this.button20.MinimumSize = new System.Drawing.Size(0, 19);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(263, 23);
            this.button20.TabIndex = 3;
            this.button20.TabStop = false;
            this.button20.Text = "button20";
            this.button20.UseVisualStyleBackColor = true;
            // 
            // panel27
            // 
            this.panel27.AutoSize = true;
            this.panel27.Controls.Add(this.button21);
            this.panel27.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel27.Location = new System.Drawing.Point(0, 54);
            this.panel27.Margin = new System.Windows.Forms.Padding(2);
            this.panel27.Name = "panel27";
            this.panel27.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel27.Size = new System.Drawing.Size(263, 31);
            this.panel27.TabIndex = 5;
            // 
            // button21
            // 
            this.button21.AutoSize = true;
            this.button21.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button21.Dock = System.Windows.Forms.DockStyle.Top;
            this.button21.Location = new System.Drawing.Point(0, 8);
            this.button21.Margin = new System.Windows.Forms.Padding(0);
            this.button21.MinimumSize = new System.Drawing.Size(0, 19);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(263, 23);
            this.button21.TabIndex = 3;
            this.button21.TabStop = false;
            this.button21.Text = "button21";
            this.button21.UseVisualStyleBackColor = true;
            // 
            // panel28
            // 
            this.panel28.AutoSize = true;
            this.panel28.Controls.Add(this.button22);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel28.Location = new System.Drawing.Point(0, 23);
            this.panel28.Margin = new System.Windows.Forms.Padding(2);
            this.panel28.Name = "panel28";
            this.panel28.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel28.Size = new System.Drawing.Size(263, 31);
            this.panel28.TabIndex = 4;
            // 
            // button22
            // 
            this.button22.AutoSize = true;
            this.button22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button22.Dock = System.Windows.Forms.DockStyle.Top;
            this.button22.Location = new System.Drawing.Point(0, 8);
            this.button22.Margin = new System.Windows.Forms.Padding(0);
            this.button22.MinimumSize = new System.Drawing.Size(0, 19);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(263, 23);
            this.button22.TabIndex = 3;
            this.button22.TabStop = false;
            this.button22.Text = "button22";
            this.button22.UseVisualStyleBackColor = true;
            // 
            // panel29
            // 
            this.panel29.AutoSize = true;
            this.panel29.Controls.Add(this.button23);
            this.panel29.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel29.Location = new System.Drawing.Point(0, 0);
            this.panel29.Margin = new System.Windows.Forms.Padding(2);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(263, 23);
            this.panel29.TabIndex = 0;
            // 
            // button23
            // 
            this.button23.AutoSize = true;
            this.button23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button23.Dock = System.Windows.Forms.DockStyle.Top;
            this.button23.Location = new System.Drawing.Point(0, 0);
            this.button23.Margin = new System.Windows.Forms.Padding(0);
            this.button23.MinimumSize = new System.Drawing.Size(0, 19);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(263, 23);
            this.button23.TabIndex = 3;
            this.button23.TabStop = false;
            this.button23.Text = "button23";
            this.button23.UseVisualStyleBackColor = true;
            // 
            // bpmFunctionsPage
            // 
            this.bpmFunctionsPage.Controls.Add(this.tableLayoutPanel4);
            this.bpmFunctionsPage.Location = new System.Drawing.Point(4, 4);
            this.bpmFunctionsPage.Margin = new System.Windows.Forms.Padding(2);
            this.bpmFunctionsPage.Name = "bpmFunctionsPage";
            this.bpmFunctionsPage.Padding = new System.Windows.Forms.Padding(2);
            this.bpmFunctionsPage.Size = new System.Drawing.Size(571, 153);
            this.bpmFunctionsPage.TabIndex = 2;
            this.bpmFunctionsPage.Text = "BPM Functions";
            this.bpmFunctionsPage.ToolTipText = "Contains functions that detects snappings, editing BPM of a point and whatsoever." +
    "";
            this.bpmFunctionsPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel34, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(567, 149);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.panel30);
            this.panel5.Controls.Add(this.panel31);
            this.panel5.Controls.Add(this.panel32);
            this.panel5.Controls.Add(this.panel33);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(2, 2);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(279, 145);
            this.panel5.TabIndex = 0;
            this.panel5.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // panel30
            // 
            this.panel30.AutoSize = true;
            this.panel30.Controls.Add(this.button25);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel30.Location = new System.Drawing.Point(0, 85);
            this.panel30.Margin = new System.Windows.Forms.Padding(2);
            this.panel30.Name = "panel30";
            this.panel30.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel30.Size = new System.Drawing.Size(279, 31);
            this.panel30.TabIndex = 9;
            // 
            // button25
            // 
            this.button25.AutoSize = true;
            this.button25.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button25.Dock = System.Windows.Forms.DockStyle.Top;
            this.button25.Location = new System.Drawing.Point(0, 8);
            this.button25.Margin = new System.Windows.Forms.Padding(0);
            this.button25.MinimumSize = new System.Drawing.Size(0, 19);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(279, 23);
            this.button25.TabIndex = 3;
            this.button25.TabStop = false;
            this.button25.Text = "button25";
            this.button25.UseVisualStyleBackColor = true;
            // 
            // panel31
            // 
            this.panel31.AutoSize = true;
            this.panel31.Controls.Add(this.button26);
            this.panel31.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel31.Location = new System.Drawing.Point(0, 54);
            this.panel31.Margin = new System.Windows.Forms.Padding(2);
            this.panel31.Name = "panel31";
            this.panel31.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel31.Size = new System.Drawing.Size(279, 31);
            this.panel31.TabIndex = 8;
            // 
            // button26
            // 
            this.button26.AutoSize = true;
            this.button26.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button26.Dock = System.Windows.Forms.DockStyle.Top;
            this.button26.Location = new System.Drawing.Point(0, 8);
            this.button26.Margin = new System.Windows.Forms.Padding(0);
            this.button26.MinimumSize = new System.Drawing.Size(0, 19);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(279, 23);
            this.button26.TabIndex = 3;
            this.button26.TabStop = false;
            this.button26.Text = "button26";
            this.button26.UseVisualStyleBackColor = true;
            // 
            // panel32
            // 
            this.panel32.AutoSize = true;
            this.panel32.Controls.Add(this.button27);
            this.panel32.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel32.Location = new System.Drawing.Point(0, 23);
            this.panel32.Margin = new System.Windows.Forms.Padding(2);
            this.panel32.Name = "panel32";
            this.panel32.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel32.Size = new System.Drawing.Size(279, 31);
            this.panel32.TabIndex = 7;
            // 
            // button27
            // 
            this.button27.AutoSize = true;
            this.button27.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button27.Dock = System.Windows.Forms.DockStyle.Top;
            this.button27.Location = new System.Drawing.Point(0, 8);
            this.button27.Margin = new System.Windows.Forms.Padding(0);
            this.button27.MinimumSize = new System.Drawing.Size(0, 19);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(279, 23);
            this.button27.TabIndex = 3;
            this.button27.TabStop = false;
            this.button27.Text = "button27";
            this.button27.UseVisualStyleBackColor = true;
            // 
            // panel33
            // 
            this.panel33.AutoSize = true;
            this.panel33.Controls.Add(this.button28);
            this.panel33.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel33.Location = new System.Drawing.Point(0, 0);
            this.panel33.Margin = new System.Windows.Forms.Padding(2);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(279, 23);
            this.panel33.TabIndex = 0;
            // 
            // button28
            // 
            this.button28.AutoSize = true;
            this.button28.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button28.Dock = System.Windows.Forms.DockStyle.Top;
            this.button28.Location = new System.Drawing.Point(0, 0);
            this.button28.Margin = new System.Windows.Forms.Padding(0);
            this.button28.MinimumSize = new System.Drawing.Size(0, 19);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(279, 23);
            this.button28.TabIndex = 2;
            this.button28.TabStop = false;
            this.button28.Text = "button28";
            this.button28.UseVisualStyleBackColor = true;
            // 
            // panel34
            // 
            this.panel34.AutoScroll = true;
            this.panel34.Controls.Add(this.panel35);
            this.panel34.Controls.Add(this.panel36);
            this.panel34.Controls.Add(this.panel37);
            this.panel34.Controls.Add(this.panel38);
            this.panel34.Controls.Add(this.panel39);
            this.panel34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel34.Location = new System.Drawing.Point(285, 2);
            this.panel34.Margin = new System.Windows.Forms.Padding(2);
            this.panel34.Name = "panel34";
            this.panel34.Size = new System.Drawing.Size(280, 145);
            this.panel34.TabIndex = 1;
            this.panel34.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // panel35
            // 
            this.panel35.AutoSize = true;
            this.panel35.Controls.Add(this.button29);
            this.panel35.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel35.Location = new System.Drawing.Point(0, 116);
            this.panel35.Margin = new System.Windows.Forms.Padding(2);
            this.panel35.Name = "panel35";
            this.panel35.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel35.Size = new System.Drawing.Size(263, 31);
            this.panel35.TabIndex = 7;
            // 
            // button29
            // 
            this.button29.AutoSize = true;
            this.button29.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button29.Dock = System.Windows.Forms.DockStyle.Top;
            this.button29.Location = new System.Drawing.Point(0, 8);
            this.button29.Margin = new System.Windows.Forms.Padding(0);
            this.button29.MinimumSize = new System.Drawing.Size(0, 19);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(263, 23);
            this.button29.TabIndex = 3;
            this.button29.TabStop = false;
            this.button29.Text = "button29";
            this.button29.UseVisualStyleBackColor = true;
            // 
            // panel36
            // 
            this.panel36.AutoSize = true;
            this.panel36.Controls.Add(this.button30);
            this.panel36.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel36.Location = new System.Drawing.Point(0, 85);
            this.panel36.Margin = new System.Windows.Forms.Padding(2);
            this.panel36.Name = "panel36";
            this.panel36.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel36.Size = new System.Drawing.Size(263, 31);
            this.panel36.TabIndex = 6;
            // 
            // button30
            // 
            this.button30.AutoSize = true;
            this.button30.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button30.Dock = System.Windows.Forms.DockStyle.Top;
            this.button30.Location = new System.Drawing.Point(0, 8);
            this.button30.Margin = new System.Windows.Forms.Padding(0);
            this.button30.MinimumSize = new System.Drawing.Size(0, 19);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(263, 23);
            this.button30.TabIndex = 3;
            this.button30.TabStop = false;
            this.button30.Text = "button30";
            this.button30.UseVisualStyleBackColor = true;
            // 
            // panel37
            // 
            this.panel37.AutoSize = true;
            this.panel37.Controls.Add(this.button31);
            this.panel37.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel37.Location = new System.Drawing.Point(0, 54);
            this.panel37.Margin = new System.Windows.Forms.Padding(2);
            this.panel37.Name = "panel37";
            this.panel37.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel37.Size = new System.Drawing.Size(263, 31);
            this.panel37.TabIndex = 5;
            // 
            // button31
            // 
            this.button31.AutoSize = true;
            this.button31.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button31.Dock = System.Windows.Forms.DockStyle.Top;
            this.button31.Location = new System.Drawing.Point(0, 8);
            this.button31.Margin = new System.Windows.Forms.Padding(0);
            this.button31.MinimumSize = new System.Drawing.Size(0, 19);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(263, 23);
            this.button31.TabIndex = 3;
            this.button31.TabStop = false;
            this.button31.Text = "button31";
            this.button31.UseVisualStyleBackColor = true;
            // 
            // panel38
            // 
            this.panel38.AutoSize = true;
            this.panel38.Controls.Add(this.button32);
            this.panel38.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel38.Location = new System.Drawing.Point(0, 23);
            this.panel38.Margin = new System.Windows.Forms.Padding(2);
            this.panel38.Name = "panel38";
            this.panel38.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel38.Size = new System.Drawing.Size(263, 31);
            this.panel38.TabIndex = 4;
            // 
            // button32
            // 
            this.button32.AutoSize = true;
            this.button32.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button32.Dock = System.Windows.Forms.DockStyle.Top;
            this.button32.Location = new System.Drawing.Point(0, 8);
            this.button32.Margin = new System.Windows.Forms.Padding(0);
            this.button32.MinimumSize = new System.Drawing.Size(0, 19);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(263, 23);
            this.button32.TabIndex = 3;
            this.button32.TabStop = false;
            this.button32.Text = "button32";
            this.button32.UseVisualStyleBackColor = true;
            // 
            // panel39
            // 
            this.panel39.AutoSize = true;
            this.panel39.Controls.Add(this.button33);
            this.panel39.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel39.Location = new System.Drawing.Point(0, 0);
            this.panel39.Margin = new System.Windows.Forms.Padding(2);
            this.panel39.Name = "panel39";
            this.panel39.Size = new System.Drawing.Size(263, 23);
            this.panel39.TabIndex = 0;
            // 
            // button33
            // 
            this.button33.AutoSize = true;
            this.button33.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button33.Dock = System.Windows.Forms.DockStyle.Top;
            this.button33.Location = new System.Drawing.Point(0, 0);
            this.button33.Margin = new System.Windows.Forms.Padding(0);
            this.button33.MinimumSize = new System.Drawing.Size(0, 19);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(263, 23);
            this.button33.TabIndex = 3;
            this.button33.TabStop = false;
            this.button33.Text = "button33";
            this.button33.UseVisualStyleBackColor = true;
            // 
            // nominationFunctionsPage
            // 
            this.nominationFunctionsPage.Controls.Add(this.tableLayoutPanel8);
            this.nominationFunctionsPage.Location = new System.Drawing.Point(4, 4);
            this.nominationFunctionsPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nominationFunctionsPage.Name = "nominationFunctionsPage";
            this.nominationFunctionsPage.Size = new System.Drawing.Size(571, 153);
            this.nominationFunctionsPage.TabIndex = 4;
            this.nominationFunctionsPage.Text = "Nomination Checks";
            this.nominationFunctionsPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.panel9, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.panel43, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(571, 153);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.AutoScroll = true;
            this.panel9.Controls.Add(this.panel6);
            this.panel9.Controls.Add(this.panel46);
            this.panel9.Controls.Add(this.panel45);
            this.panel9.Controls.Add(this.panel44);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(2, 2);
            this.panel9.Margin = new System.Windows.Forms.Padding(2);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(281, 149);
            this.panel9.TabIndex = 0;
            this.panel9.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.unsnappedNoteBarlineButton);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 111);
            this.panel6.Margin = new System.Windows.Forms.Padding(2);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel6.Size = new System.Drawing.Size(281, 31);
            this.panel6.TabIndex = 3;
            // 
            // unsnappedNoteBarlineButton
            // 
            this.unsnappedNoteBarlineButton.AutoSize = true;
            this.unsnappedNoteBarlineButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.unsnappedNoteBarlineButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.unsnappedNoteBarlineButton.Location = new System.Drawing.Point(0, 8);
            this.unsnappedNoteBarlineButton.Name = "unsnappedNoteBarlineButton";
            this.unsnappedNoteBarlineButton.Size = new System.Drawing.Size(281, 23);
            this.unsnappedNoteBarlineButton.TabIndex = 0;
            this.unsnappedNoteBarlineButton.Text = "Find notes that are off from barlines";
            this.unsnappedNoteBarlineButton.UseVisualStyleBackColor = true;
            this.unsnappedNoteBarlineButton.Click += new System.EventHandler(this.unsnappedNoteBarlineButton_Click);
            // 
            // panel46
            // 
            this.panel46.AutoSize = true;
            this.panel46.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel46.Controls.Add(this.flyingBarlinesButton);
            this.panel46.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel46.Location = new System.Drawing.Point(0, 80);
            this.panel46.Margin = new System.Windows.Forms.Padding(2);
            this.panel46.Name = "panel46";
            this.panel46.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel46.Size = new System.Drawing.Size(281, 31);
            this.panel46.TabIndex = 2;
            // 
            // flyingBarlinesButton
            // 
            this.flyingBarlinesButton.AutoSize = true;
            this.flyingBarlinesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flyingBarlinesButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.flyingBarlinesButton.Location = new System.Drawing.Point(0, 8);
            this.flyingBarlinesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flyingBarlinesButton.Name = "flyingBarlinesButton";
            this.flyingBarlinesButton.Size = new System.Drawing.Size(281, 23);
            this.flyingBarlinesButton.TabIndex = 0;
            this.flyingBarlinesButton.TabStop = false;
            this.flyingBarlinesButton.Text = "Find flying barlines in the mapset";
            this.toolTip1.SetToolTip(this.flyingBarlinesButton, resources.GetString("flyingBarlinesButton.ToolTip"));
            this.flyingBarlinesButton.UseVisualStyleBackColor = true;
            this.flyingBarlinesButton.Click += new System.EventHandler(this.flyingBarlinesButton_Click);
            // 
            // panel45
            // 
            this.panel45.AutoSize = true;
            this.panel45.Controls.Add(this.checkDoubleBarlinesButton);
            this.panel45.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel45.Location = new System.Drawing.Point(0, 36);
            this.panel45.Margin = new System.Windows.Forms.Padding(2);
            this.panel45.Name = "panel45";
            this.panel45.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel45.Size = new System.Drawing.Size(281, 44);
            this.panel45.TabIndex = 1;
            // 
            // checkDoubleBarlinesButton
            // 
            this.checkDoubleBarlinesButton.AutoSize = true;
            this.checkDoubleBarlinesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.checkDoubleBarlinesButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkDoubleBarlinesButton.Location = new System.Drawing.Point(0, 8);
            this.checkDoubleBarlinesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.checkDoubleBarlinesButton.Name = "checkDoubleBarlinesButton";
            this.checkDoubleBarlinesButton.Size = new System.Drawing.Size(281, 36);
            this.checkDoubleBarlinesButton.TabIndex = 0;
            this.checkDoubleBarlinesButton.Text = "Check red points and double barlines for the\r\nentire mapset";
            this.toolTip1.SetToolTip(this.checkDoubleBarlinesButton, resources.GetString("checkDoubleBarlinesButton.ToolTip"));
            this.checkDoubleBarlinesButton.UseVisualStyleBackColor = true;
            this.checkDoubleBarlinesButton.Click += new System.EventHandler(this.checkDoubleBarlinesButton_Click);
            // 
            // panel44
            // 
            this.panel44.AutoSize = true;
            this.panel44.Controls.Add(this.timingInconsistenciesButton);
            this.panel44.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel44.Location = new System.Drawing.Point(0, 0);
            this.panel44.Margin = new System.Windows.Forms.Padding(2);
            this.panel44.Name = "panel44";
            this.panel44.Size = new System.Drawing.Size(281, 36);
            this.panel44.TabIndex = 0;
            // 
            // timingInconsistenciesButton
            // 
            this.timingInconsistenciesButton.AutoSize = true;
            this.timingInconsistenciesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.timingInconsistenciesButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.timingInconsistenciesButton.Location = new System.Drawing.Point(0, 0);
            this.timingInconsistenciesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.timingInconsistenciesButton.Name = "timingInconsistenciesButton";
            this.timingInconsistenciesButton.Size = new System.Drawing.Size(281, 36);
            this.timingInconsistenciesButton.TabIndex = 0;
            this.timingInconsistenciesButton.Text = "Check red - green point inconsistencies on all\r\ndifficulties";
            this.toolTip1.SetToolTip(this.timingInconsistenciesButton, "This function checks for volume, kiai and omitted\r\nbarline inconsistencies throug" +
        "hout the entire\r\nmapset.");
            this.timingInconsistenciesButton.UseVisualStyleBackColor = true;
            this.timingInconsistenciesButton.Click += new System.EventHandler(this.timingInconsistenciesButton_Click);
            // 
            // panel43
            // 
            this.panel43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel43.Location = new System.Drawing.Point(287, 2);
            this.panel43.Margin = new System.Windows.Forms.Padding(2);
            this.panel43.Name = "panel43";
            this.panel43.Size = new System.Drawing.Size(282, 149);
            this.panel43.TabIndex = 1;
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.panel7);
            this.settingsPage.Location = new System.Drawing.Point(4, 4);
            this.settingsPage.Margin = new System.Windows.Forms.Padding(2);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Size = new System.Drawing.Size(571, 153);
            this.settingsPage.TabIndex = 3;
            this.settingsPage.Text = "Settings";
            this.settingsPage.ToolTipText = "\"Shows general information about the program and the map.\"";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.AutoScroll = true;
            this.panel7.Controls.Add(this.tableLayoutPanel7);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.tableLayoutPanel6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(571, 153);
            this.panel7.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel7.Controls.Add(this.panel42, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel41, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel40, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 60);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(571, 93);
            this.tableLayoutPanel7.TabIndex = 8;
            // 
            // panel42
            // 
            this.panel42.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel42.Controls.Add(this.runningProcessLabel);
            this.panel42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel42.Location = new System.Drawing.Point(380, 0);
            this.panel42.Margin = new System.Windows.Forms.Padding(0);
            this.panel42.Name = "panel42";
            this.panel42.Size = new System.Drawing.Size(191, 93);
            this.panel42.TabIndex = 2;
            // 
            // runningProcessLabel
            // 
            this.runningProcessLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runningProcessLabel.Location = new System.Drawing.Point(0, 0);
            this.runningProcessLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.runningProcessLabel.Name = "runningProcessLabel";
            this.runningProcessLabel.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.runningProcessLabel.Size = new System.Drawing.Size(191, 93);
            this.runningProcessLabel.TabIndex = 2;
            // 
            // panel41
            // 
            this.panel41.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel41.Controls.Add(this.label8);
            this.panel41.Controls.Add(this.label9);
            this.panel41.Controls.Add(this.label6);
            this.panel41.Controls.Add(this.label7);
            this.panel41.Controls.Add(this.label4);
            this.panel41.Controls.Add(this.label5);
            this.panel41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel41.Location = new System.Drawing.Point(190, 0);
            this.panel41.Margin = new System.Windows.Forms.Padding(0);
            this.panel41.Name = "panel41";
            this.panel41.Size = new System.Drawing.Size(190, 93);
            this.panel41.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 7);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "F5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Refresh:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ctrl + Z";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Undo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ctrl + Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 34);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Redo:";
            // 
            // panel40
            // 
            this.panel40.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel40.Controls.Add(this.lastSaveTimeLabel);
            this.panel40.Controls.Add(this.label3);
            this.panel40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel40.Location = new System.Drawing.Point(0, 0);
            this.panel40.Margin = new System.Windows.Forms.Padding(0);
            this.panel40.Name = "panel40";
            this.panel40.Size = new System.Drawing.Size(190, 93);
            this.panel40.TabIndex = 0;
            // 
            // lastSaveTimeLabel
            // 
            this.lastSaveTimeLabel.AutoSize = true;
            this.lastSaveTimeLabel.Location = new System.Drawing.Point(83, 7);
            this.lastSaveTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lastSaveTimeLabel.Name = "lastSaveTimeLabel";
            this.lastSaveTimeLabel.Size = new System.Drawing.Size(56, 13);
            this.lastSaveTimeLabel.TabIndex = 1;
            this.lastSaveTimeLabel.Text = "Undefined";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Last Save Time:";
            // 
            // panel8
            // 
            this.panel8.AutoSize = true;
            this.panel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel8.Controls.Add(this.button1);
            this.panel8.Controls.Add(this.browseButton);
            this.panel8.Controls.Add(this.filePathTextBox);
            this.panel8.Controls.Add(this.label1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 27);
            this.panel8.Margin = new System.Windows.Forms.Padding(2);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panel8.Size = new System.Drawing.Size(571, 33);
            this.panel8.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(451, 7);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Load Current";
            this.toolTip1.SetToolTip(this.button1, "Loads the current beatmap that\'s open in\r\nosu! editor to the program.");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.loadCurrentBeatmapButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.AutoSize = true;
            this.browseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseButton.Location = new System.Drawing.Point(384, 7);
            this.browseButton.Margin = new System.Windows.Forms.Padding(2);
            this.browseButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(61, 23);
            this.browseButton.TabIndex = 5;
            this.browseButton.TabStop = false;
            this.browseButton.Text = "Browse...";
            this.toolTip1.SetToolTip(this.browseButton, "Browses a map. If osu! is running, it will open the songs folder directly, otherw" +
        "ise it will open the last folder.");
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.filePathTextBox.Enabled = false;
            this.filePathTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.filePathTextBox.Location = new System.Drawing.Point(67, 8);
            this.filePathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(314, 20);
            this.filePathTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File Path: ";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel6.Controls.Add(this.redoButton, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.saveButton, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.undoButton, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(571, 27);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // redoButton
            // 
            this.redoButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.redoButton.AutoSize = true;
            this.redoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.redoButton.Location = new System.Drawing.Point(454, 2);
            this.redoButton.Margin = new System.Windows.Forms.Padding(2);
            this.redoButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(43, 23);
            this.redoButton.TabIndex = 2;
            this.redoButton.TabStop = false;
            this.redoButton.Text = "Redo";
            this.toolTip1.SetToolTip(this.redoButton, "Recovers the last change after an undo.");
            this.redoButton.UseVisualStyleBackColor = true;
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveButton.AutoSize = true;
            this.saveButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveButton.Location = new System.Drawing.Point(264, 2);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(42, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.TabStop = false;
            this.saveButton.Text = "Save";
            this.toolTip1.SetToolTip(this.saveButton, "Saves the beatmap, although I\'ve never used it before.");
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // undoButton
            // 
            this.undoButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.undoButton.AutoSize = true;
            this.undoButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.undoButton.Location = new System.Drawing.Point(73, 2);
            this.undoButton.Margin = new System.Windows.Forms.Padding(2);
            this.undoButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(43, 23);
            this.undoButton.TabIndex = 0;
            this.undoButton.TabStop = false;
            this.undoButton.Text = "Undo";
            this.toolTip1.SetToolTip(this.undoButton, "Loads the last state of the map before any changes from the program.");
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // mainDisplayView
            // 
            this.mainDisplayView.AllowUserToAddRows = false;
            this.mainDisplayView.AllowUserToDeleteRows = false;
            this.mainDisplayView.AllowUserToResizeColumns = false;
            this.mainDisplayView.AllowUserToResizeRows = false;
            this.mainDisplayView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mainDisplayView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mainDisplayView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.mainDisplayView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDisplayView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.timeColumn,
            this.bpmColumn,
            this.meterColumn,
            this.volumeColumn,
            this.kiaiColumn});
            this.mainDisplayView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDisplayView.Location = new System.Drawing.Point(4, 46);
            this.mainDisplayView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainDisplayView.Name = "mainDisplayView";
            this.mainDisplayView.ReadOnly = true;
            this.mainDisplayView.RowHeadersWidth = 51;
            this.mainDisplayView.RowTemplate.Height = 24;
            this.mainDisplayView.Size = new System.Drawing.Size(575, 215);
            this.mainDisplayView.TabIndex = 3;
            this.mainDisplayView.MouseEnter += new System.EventHandler(this.processSender);
            // 
            // timeColumn
            // 
            this.timeColumn.HeaderText = "Offset";
            this.timeColumn.MinimumWidth = 6;
            this.timeColumn.Name = "timeColumn";
            this.timeColumn.ReadOnly = true;
            this.timeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.timeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.timeColumn.ToolTipText = "Shows where the point is, in milliseconds and formatted.";
            // 
            // bpmColumn
            // 
            this.bpmColumn.HeaderText = "BPM (SV)";
            this.bpmColumn.MinimumWidth = 6;
            this.bpmColumn.Name = "bpmColumn";
            this.bpmColumn.ReadOnly = true;
            this.bpmColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bpmColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bpmColumn.ToolTipText = "Shows BPM for timing points and slider velocity for inherited points.";
            // 
            // meterColumn
            // 
            this.meterColumn.HeaderText = "Meter";
            this.meterColumn.MinimumWidth = 6;
            this.meterColumn.Name = "meterColumn";
            this.meterColumn.ReadOnly = true;
            this.meterColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.meterColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.meterColumn.ToolTipText = "Shows the measure of the red timing point. Does not contain any data for inherite" +
    "d points.";
            // 
            // volumeColumn
            // 
            this.volumeColumn.HeaderText = "Volume";
            this.volumeColumn.MinimumWidth = 6;
            this.volumeColumn.Name = "volumeColumn";
            this.volumeColumn.ReadOnly = true;
            this.volumeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.volumeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.volumeColumn.ToolTipText = "Shows volume in percentage for both timing and inherited points.";
            // 
            // kiaiColumn
            // 
            this.kiaiColumn.HeaderText = "Kiai";
            this.kiaiColumn.MinimumWidth = 6;
            this.kiaiColumn.Name = "kiaiColumn";
            this.kiaiColumn.ReadOnly = true;
            this.kiaiColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.kiaiColumn.ToolTipText = "Shows whether the point has a kiai or not.";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel5.Controls.Add(this.inheritedPointsButton, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.timingPointsButton, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.allPointsButton, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(579, 37);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // inheritedPointsButton
            // 
            this.inheritedPointsButton.AutoSize = true;
            this.inheritedPointsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inheritedPointsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inheritedPointsButton.Location = new System.Drawing.Point(386, 2);
            this.inheritedPointsButton.Margin = new System.Windows.Forms.Padding(2);
            this.inheritedPointsButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.inheritedPointsButton.Name = "inheritedPointsButton";
            this.inheritedPointsButton.Size = new System.Drawing.Size(191, 33);
            this.inheritedPointsButton.TabIndex = 2;
            this.inheritedPointsButton.TabStop = false;
            this.inheritedPointsButton.Text = "Inherited Points";
            this.toolTip1.SetToolTip(this.inheritedPointsButton, "Shows only inherited (green) points in the data below.");
            this.inheritedPointsButton.UseVisualStyleBackColor = true;
            this.inheritedPointsButton.Click += new System.EventHandler(this.inheritedPointsButton_Click);
            // 
            // timingPointsButton
            // 
            this.timingPointsButton.AutoSize = true;
            this.timingPointsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.timingPointsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timingPointsButton.Location = new System.Drawing.Point(194, 2);
            this.timingPointsButton.Margin = new System.Windows.Forms.Padding(2);
            this.timingPointsButton.MinimumSize = new System.Drawing.Size(0, 19);
            this.timingPointsButton.Name = "timingPointsButton";
            this.timingPointsButton.Size = new System.Drawing.Size(188, 33);
            this.timingPointsButton.TabIndex = 1;
            this.timingPointsButton.TabStop = false;
            this.timingPointsButton.Text = "Timing Points";
            this.toolTip1.SetToolTip(this.timingPointsButton, "Shows only timing (red) points in the data below.");
            this.timingPointsButton.UseVisualStyleBackColor = true;
            this.timingPointsButton.Click += new System.EventHandler(this.timingPointsButton_Click);
            // 
            // allPointsButton
            // 
            this.allPointsButton.AutoSize = true;
            this.allPointsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.allPointsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allPointsButton.Location = new System.Drawing.Point(2, 2);
            this.allPointsButton.Margin = new System.Windows.Forms.Padding(2);
            this.allPointsButton.Name = "allPointsButton";
            this.allPointsButton.Size = new System.Drawing.Size(188, 33);
            this.allPointsButton.TabIndex = 0;
            this.allPointsButton.TabStop = false;
            this.allPointsButton.Text = "All Points";
            this.toolTip1.SetToolTip(this.allPointsButton, "Shows both timing and inherited points in the data below.");
            this.allPointsButton.UseVisualStyleBackColor = true;
            this.allPointsButton.Click += new System.EventHandler(this.allPointsButton_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.AutoPopDelay = 32767;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 449);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(483, 396);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beatmap Help Tool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.generalFunctionsPage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.svFunctionsPage.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel29.ResumeLayout(false);
            this.panel29.PerformLayout();
            this.bpmFunctionsPage.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel30.ResumeLayout(false);
            this.panel30.PerformLayout();
            this.panel31.ResumeLayout(false);
            this.panel31.PerformLayout();
            this.panel32.ResumeLayout(false);
            this.panel32.PerformLayout();
            this.panel33.ResumeLayout(false);
            this.panel33.PerformLayout();
            this.panel34.ResumeLayout(false);
            this.panel34.PerformLayout();
            this.panel35.ResumeLayout(false);
            this.panel35.PerformLayout();
            this.panel36.ResumeLayout(false);
            this.panel36.PerformLayout();
            this.panel37.ResumeLayout(false);
            this.panel37.PerformLayout();
            this.panel38.ResumeLayout(false);
            this.panel38.PerformLayout();
            this.panel39.ResumeLayout(false);
            this.panel39.PerformLayout();
            this.nominationFunctionsPage.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel46.ResumeLayout(false);
            this.panel46.PerformLayout();
            this.panel45.ResumeLayout(false);
            this.panel45.PerformLayout();
            this.panel44.ResumeLayout(false);
            this.panel44.PerformLayout();
            this.settingsPage.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.panel42.ResumeLayout(false);
            this.panel41.ResumeLayout(false);
            this.panel41.PerformLayout();
            this.panel40.ResumeLayout(false);
            this.panel40.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplayView)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalFunctionsPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel19;
        private MultilineButton button10;
        private System.Windows.Forms.Panel panel18;
        private MultilineButton button9;
        private System.Windows.Forms.Panel panel17;
        private MultilineButton button36;
        private System.Windows.Forms.Panel panel16;
        private MultilineButton button37;
        private System.Windows.Forms.Panel panel15;
        private MultilineButton button35;
        private System.Windows.Forms.TabPage svFunctionsPage;
        private System.Windows.Forms.TabPage bpmFunctionsPage;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private MultilineButton inheritedPointsButton;
        private MultilineButton timingPointsButton;
        private MultilineButton allPointsButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel10;
        private MultilineButton whistleToClapButton;
        private System.Windows.Forms.Panel panel8;
        private MultilineButton browseButton;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel11;
        private MultilineButton positionAllNotesButton;
        private System.Windows.Forms.Panel panel14;
        private MultilineButton loadCurrentBeatmapButton;
        private System.Windows.Forms.Panel panel13;
        private MultilineButton playTaikoDiffsButton;
        private System.Windows.Forms.Panel panel12;
        private MultilineButton snapGreenToRedPointsButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private MultilineButton button14;
        private System.Windows.Forms.Panel panel20;
        private MultilineButton button15;
        private System.Windows.Forms.Panel panel21;
        private MultilineButton button16;
        private System.Windows.Forms.Panel panel22;
        private MultilineButton equalizeSvForAllPointsButton;
        private System.Windows.Forms.Panel panel23;
        private MultilineButton svChangerButton;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Panel panel25;
        private MultilineButton button19;
        private System.Windows.Forms.Panel panel26;
        private MultilineButton button20;
        private System.Windows.Forms.Panel panel27;
        private MultilineButton button21;
        private System.Windows.Forms.Panel panel28;
        private MultilineButton button22;
        private System.Windows.Forms.Panel panel29;
        private MultilineButton button23;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel30;
        private MultilineButton button25;
        private System.Windows.Forms.Panel panel31;
        private MultilineButton button26;
        private System.Windows.Forms.Panel panel32;
        private MultilineButton button27;
        private System.Windows.Forms.Panel panel33;
        private MultilineButton button28;
        private System.Windows.Forms.Panel panel34;
        private System.Windows.Forms.Panel panel35;
        private MultilineButton button29;
        private System.Windows.Forms.Panel panel36;
        private MultilineButton button30;
        private System.Windows.Forms.Panel panel37;
        private MultilineButton button31;
        private System.Windows.Forms.Panel panel38;
        private MultilineButton button32;
        private System.Windows.Forms.Panel panel39;
        private MultilineButton button33;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private MultilineButton redoButton;
        private MultilineButton saveButton;
        private MultilineButton undoButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel panel42;
        private System.Windows.Forms.Panel panel41;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel40;
        private System.Windows.Forms.Label lastSaveTimeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label runningProcessLabel;
        private DoubleBufferGridView mainDisplayView;
        private DataGridViewTextBoxColumn timeColumn;
        private DataGridViewTextBoxColumn bpmColumn;
        private DataGridViewTextBoxColumn meterColumn;
        private DataGridViewTextBoxColumn volumeColumn;
        private DataGridViewCheckBoxColumn kiaiColumn;
        private Button button1;
        private TabPage nominationFunctionsPage;
        private TableLayoutPanel tableLayoutPanel8;
        private Panel panel9;
        private Panel panel43;
        private Panel panel46;
        private MultilineButton flyingBarlinesButton;
        private Panel panel45;
        private MultilineButton checkDoubleBarlinesButton;
        private Panel panel44;
        private MultilineButton timingInconsistenciesButton;
        private Panel panel6;
        private MultilineButton unsnappedNoteBarlineButton;
    }
}

