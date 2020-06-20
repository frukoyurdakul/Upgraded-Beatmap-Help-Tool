using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Forms;
using Beatmap_Help_Tool.Properties;
using Beatmap_Help_Tool.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Beatmap_Help_Tool
{
    public partial class MainWindow : Form
    {
        public static string lastAction = "";

        private Beatmap beatmap;

        public MainWindow()
        {
            InitializeComponent();
            disableTabs();
        }

        // Activates double-buffering through the entire form.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #region Util functions
        private void performBrowse()
        {
            string targetPath;
            if (beatmap != null)
                targetPath = Directory.GetParent(beatmap.FilePath).FullName;
            else
            {
                targetPath = getSongsPathFromProcess();
                if (string.IsNullOrWhiteSpace(targetPath))
                    targetPath = "";
            }
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = SharedPreferences.get(PreferencesKeys.LAST_BEATMAP_DIR_PATH, ""),
                Title = "Select .osu file",
                Filter = ".osu files|*.osu"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ThreadUtils.executeOnBackground(new Action(() =>
                    loadBeatmap(dialog.FileName, dialog.SafeFileName)));
            }
        }

        private void loadBeatmap(string targetPath, string beatmapFileName)
        {
            // At this point, file path has been found, load the beatmap and
            // set the data to datagridview.
            // First, invoke the running process label.
            Invoke(new Action(() =>
            {
                runningProcessLabel.Text = beatmapFileName + " has been found, loading...";
            }));
            beatmap = new Beatmap(targetPath);
            Invoke(new Action(() =>
            {
                string finalPath = Directory.GetParent(targetPath).FullName;
                beatmap.fillMainDisplayView(mainDisplayView);
                enableTabs();
                runningProcessLabel.Text = beatmapFileName + " has been loaded.";
                Text = "Beatmap Help Tool - " + beatmapFileName;
                filePathTextBox.Text = finalPath;
                SharedPreferences.edit().put(PreferencesKeys.LAST_BEATMAP_DIR_PATH, finalPath).apply();
                tabControl1.SelectedIndex = 0;
            }));
        }

        private void saveBeatmap(string action)
        {
            ThreadUtils.executeOnBackground(new Action(() =>
            {
                Invoke(new Action(() =>
                {
                    runningProcessLabel.Text = "Saving beatmap...";
                }));
                beatmap.save(action);
                Invoke(new Action(() =>
                {
                    MessageBoxUtils.show("Beatmap has been saved.");
                    runningProcessLabel.Text = "Beatmap has been saved.";
                    lastSaveTimeLabel.Text = DateTime.Now.ToLongTimeString();
                }));
            }));
        }

        private void saveBeatmap(string action, string path)
        {
            ThreadUtils.executeOnBackground(new Action(() =>
            {
                Invoke(new Action(() =>
                {
                    runningProcessLabel.Text = "Saving beatmap to path: " + path + "\\" + beatmap.FileName;
                }));
                beatmap.save(action, path);
                Invoke(new Action(() =>
                {
                    MessageBoxUtils.show("Beatmap has been saved.");
                    runningProcessLabel.Text = "Beatmap has been saved.";
                    lastSaveTimeLabel.Text = DateTime.Now.ToLongTimeString();
                }));
            }));
        }

        private void refreshContent()
        {
            if (beatmap != null)
                beatmap.fillMainDisplayView(mainDisplayView);
        }

        private string getSongsPathFromProcess()
        {
            Process[] processes = Process.GetProcessesByName("osu!");
            if (processes.Length > 0)
            {
                DirectoryInfo osuDirectory = Directory.GetParent(processes[0].MainModule.FileName);
                return osuDirectory.FullName + "\\Songs";
            }
            return null;
        }

        private void determineInitialProcess()
        {
            Process[] processes = Process.GetProcessesByName("osu!");
            if (processes.Length > 0)
            {
                // The program was opened while osu was running.
                Process osuProcess = processes[0];
                string fileName = osuProcess.MainModule.FileName;
                string windowTitle = osuProcess.MainWindowTitle.TrimEnd();
                DirectoryInfo directory = Directory.GetParent(fileName);
                if (directory != null && windowTitle.EndsWith(".osu"))
                {
                    // Osu directory has been successfully found. Prompt the user to
                    // try to see if it can find the difficulty that they are mapping.
                    // This has to be done on UI thread.
                    Invoke(new Action(() =>
                    {
                        if (MessageBoxUtils.showQuestionYesNo("osu! Editor seems to be running, would you like to load the current beatmap?") ==
                            DialogResult.Yes)
                        {
                            // Show the current process on the label and 
                            // start searching for the file. It should be exactly
                            // the same, otherwise it cannot be found.
                            string beatmapFileName = getBeatmapFileName(windowTitle);
                            runningProcessLabel.Visible = true;
                            runningProcessLabel.Text = "Searching for " + beatmapFileName;
                            ThreadUtils.executeOnBackground(new Action(() =>
                                searchCurrentOpenBeatmap(beatmapFileName, directory.FullName + "\\Songs")));
                        }
                        else
                        {
                            // Continue with last saved path if it exists.
                        }
                    }));
                }
                else
                {
                    // Osu directory somehow cannot be found, refer to old ways or keep
                    // opening process.
                }
            }
            else
            {
                // Osu is not running, user can browse if they want.
            }
        }

        private void searchCurrentOpenBeatmap(string beatmapFileName, string songsPath)
        {
            string[] osuFilesInFolder = Directory.GetFiles(songsPath, "*.osu", SearchOption.AllDirectories);
            string lastPart;
            string targetPath = "";
            string path;
            int lastSlashIndex;
            for (int i = 0; i < osuFilesInFolder.Length; i++)
            {
                path = osuFilesInFolder[i];
                lastSlashIndex = path.LastIndexOf("\\");
                if (lastSlashIndex >= 0 && path.Length > lastSlashIndex + 1)
                {
                    lastPart = path.Substring(lastSlashIndex + 1);
                    if (lastPart == beatmapFileName)
                    {
                        targetPath = path;
                        break;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(targetPath))
                loadBeatmap(targetPath, beatmapFileName);
            else
            {
                Invoke(new Action(() =>
                {
                    runningProcessLabel.Text = beatmapFileName + " could not be found in Songs folder.";
                }));
            }
        }

        private string getBeatmapFileName(string osuWindowTitle)
        {
            // Title can be either cutting edge or stable. If it is beta, 
            // give a warning about it if it is necessary.
            string mapName;
            if (osuWindowTitle.StartsWith("osu!cuttingedge"))
            {
                mapName = returnMapName(osuWindowTitle, StringUtils.getIndexOfWithCount(osuWindowTitle, " ", 3));
            }
            else if (osuWindowTitle.StartsWith("osu!"))
            {
                mapName = returnMapName(osuWindowTitle, StringUtils.getIndexOfWithCount(osuWindowTitle, " ", 2));
            }
            else
            {
                throw new InvalidOperationException("Beatmap search is only supported in stable and cutting edge versions.");
            }

            // At this point, map name cannot be null. Return the name and display it on label, also
            // start searching on background thread.
            return mapName;
        }

        private string returnMapName(string osuWindowTitle, int spaceIndex)
        {
            if (spaceIndex >= 0 && osuWindowTitle.Length > spaceIndex + 1)
            {
                string mapName = osuWindowTitle.Substring(spaceIndex + 1);
                if (mapName.EndsWith(".osu"))
                {
                    // It is an .osu file alright, return the name.
                    return mapName;
                }
                else
                    return "";
            }
            else
                return "";
        }

        private void enableTabs()
        {
            (generalFunctionsPage as Control).Enabled = true;
            (svFunctionsPage as Control).Enabled = true;
            (bpmFunctionsPage as Control).Enabled = true;
        }

        private void disableTabs()
        {
            (generalFunctionsPage as Control).Enabled = false;
            (svFunctionsPage as Control).Enabled = false;
            (bpmFunctionsPage as Control).Enabled = false;
        }
        #endregion

        #region Mouse focus functions
        private void processSender(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                (sender as Control).Focus();
            }
        }
        #endregion

        #region Form functions
        private void mainForm_Load(object sender, EventArgs e)
        {
            ThreadUtils.executeOnBackground(new Action(() => determineInitialProcess()));
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThreadUtils.exitLooperThread();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            mainDisplayView.Focus();
            performBrowse();
        }

        private void allPointsButton_Click(object sender, EventArgs e)
        {
            if (beatmap != null)
                beatmap.showAllPoints(mainDisplayView);
            mainDisplayView.Focus();
        }

        private void timingPointsButton_Click(object sender, EventArgs e)
        {
            if (beatmap != null)
                beatmap.showTimingPointsOnly(mainDisplayView);
            mainDisplayView.Focus();
        }

        private void inheritedPointsButton_Click(object sender, EventArgs e)
        {
            if (beatmap != null)
                beatmap.showInheritedPointsOnly(mainDisplayView);
            mainDisplayView.Focus();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            if (Beatmap.hasPreviousState())
            {
                runningProcessLabel.Text = "Undoing...";
                ThreadUtils.executeOnBackground(new Action(() =>
                {
                    Beatmap beatmap = Beatmap.getPreviousSavedState();
                    if (beatmap != null)
                    {
                        this.beatmap = beatmap;
                        Invoke(new Action(() =>
                        {
                            beatmap.fillMainDisplayView(mainDisplayView);
                            runningProcessLabel.Text = string.Format("Previous state loaded ({0})",
                                Beatmap.getSavedStateAction());
                        }));
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            runningProcessLabel.Text = "An error occurred while fetching previous state.";
                        }));
                    }
                }));
            }
            else
                runningProcessLabel.Text = "There are no other previous states.";
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            if (Beatmap.hasNextState())
            {
                runningProcessLabel.Text = "Redoing...";
                ThreadUtils.executeOnBackground(new Action(() =>
                {
                    Beatmap beatmap = Beatmap.getNextSavedState();
                    if (beatmap != null)
                    {
                        this.beatmap = beatmap;
                        Invoke(new Action(() =>
                        {
                            beatmap.fillMainDisplayView(mainDisplayView);
                            runningProcessLabel.Text = string.Format("Next state loaded ({0})", 
                                Beatmap.getSavedStateAction());
                        }));
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            runningProcessLabel.Text = "An error occurred while fetching next state.";
                        }));
                    }
                }));
            }
            else
                runningProcessLabel.Text = "There are no other next states.";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (beatmap == null)
                MessageBoxUtils.showError("No beatmap has been loaded.");
            else
            {
                DialogResult result = (MessageBoxUtils.showQuestionYesNoCancel("Save to beatmap path? Choosing \"No\" will " +
                    "bring up the file chooser."));
                if (result == DialogResult.Yes)
                    saveBeatmap("Saved content");
                else if (result == DialogResult.No)
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog
                    {
                        DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        IsFolderPicker = true,
                        Multiselect = false
                    };
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                        saveBeatmap(dialog.FileName);
                }
            }
        }

        private bool checkBeatmapLoaded()
        {
            if (beatmap == null)
            {
                MessageBoxUtils.showError("No beatmap has been loaded.");
                return false;
            }
            return true;
        }

        private void showMessageAndSaveBeatmap(string popupMessage, 
            string runningProcessLabelMessage, string saveBeatmapMessage)
        {
            MessageBoxUtils.show(popupMessage);
            runningProcessLabel.Text = runningProcessLabelMessage;
            saveBeatmap(saveBeatmapMessage);
        }

        private void whistleToClapButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                if (MessageBoxUtils.showQuestionYesNo("Are you sure?") == DialogResult.Yes)
                {
                    runningProcessLabel.Text = "Converting all hitsounds to claps...";
                    ThreadUtils.executeOnBackground(new Action(() =>
                    {
                        NoteUtils.setAllWhistlesToClaps(beatmap);
                        Invoke(new Action(() =>
                        {
                            showMessageAndSaveBeatmap("Converted all hitsounds to claps successfully.",
                                "Converted all hitsounds to claps.",
                                "Converted all hitsounds to claps");
                        }));
                    }));
                }
            }
        }

        private void positionAllNotesButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                if (beatmap.isModeTaiko())
                {
                    // Here, we have a taiko mode beatmap. Positioning the notes
                    // is fine.
                    using (PositionNotesForm form = new PositionNotesForm())
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            runningProcessLabel.Text = "Re-positioning notes for taiko mode.";
                            int[] donPositions = form.donPosition;
                            int[] katPositions = form.katPosition;
                            int[] donFinishPositions = form.donFinisherPosition;
                            int[] katFinishPositions = form.katFinisherPosition;
                            ThreadUtils.executeOnBackground(new Action(() =>
                            {
                                NoteUtils.positionAllNotesForTaiko(beatmap, donPositions, katPositions, donFinishPositions, katFinishPositions);
                                Invoke(new Action(() =>
                                {
                                    showMessageAndSaveBeatmap("Re-positioned notes successfully.",
                                        "Re-positioned notes for Taiko mode successfully.",
                                        "Re-positioned notes for Taiko mode");
                                }));
                            }));
                        }
                    }
                }
                else
                {
                    // Otherwise, warn user about the map not being taiko.
                    MessageBoxUtils.showError("Beatmap mode is not defined as \"Taiko\".");
                }
            }
        }

        private void svChangerButton_Click(object sender, EventArgs e)
        {
            int firstOffset = -1;
            int lastOffset = -1;
            double firstSv = -1;
            double lastSv = -1;
            double targetBpm = -1;
            double gridSnap = -1;
            int svOffset = -1;
            int svIncreaseMode = 0; // default index
            int count = -1;
            double svIncreaseMultiplier = 1;
            bool putPointsByNotes = true;
            bool applied = false;
            using (SvChanger changer = new SvChanger())
            {
                if (changer.ShowDialog() == DialogResult.OK)
                {
                    // Do stuff.
                    firstOffset = changer.FirstOffset;
                    lastOffset = changer.LastOffset;
                    firstSv = changer.FirstSv;
                    lastSv = changer.LastSv;
                    targetBpm = changer.TargetBpm;
                    gridSnap = changer.GridSnap;
                    svOffset = changer.SvOffset;
                    svIncreaseMode = changer.SvIncreaseMode;
                    count = changer.Count;
                    svIncreaseMultiplier = changer.SvIncreaseMultiplier;
                    putPointsByNotes = changer.PutPointsByNotes;
                    applied = true;
                }
            }
            if (applied)
            {
                // Start adding the SVs here.
                ThreadUtils.executeOnBackground(new Action(() =>
                {
                    InheritedPointUtils.AddSvChanges(this, beatmap, firstOffset, lastOffset, firstSv, lastSv,
                        targetBpm, gridSnap, svOffset, svIncreaseMode, count, svIncreaseMultiplier, putPointsByNotes);
                }));
            }
        }
        #endregion
    }
}
