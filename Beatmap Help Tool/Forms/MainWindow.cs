using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Forms;
using Beatmap_Help_Tool.Models;
using Beatmap_Help_Tool.Properties;
using Beatmap_Help_Tool.TaikoPlayer;
using Beatmap_Help_Tool.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;

namespace Beatmap_Help_Tool
{
    public partial class MainWindow : Form
    {
        public static string lastAction = "";
        private readonly InputSimulator inputSimulator = new InputSimulator();

        private Beatmap beatmap;

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

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
                    ThreadUtils.executeOnBackground(new Action(() =>
                    {
                        reloadBeatmapIfNecessary();
                    }));
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
                ThreadUtils.executeOnBackground(new Action(() =>
                {
                    reloadBeatmapIfNecessary();
                }));
                Invoke(new Action(() =>
                {
                    beatmap.fillMainDisplayView(mainDisplayView);
                    MessageBoxUtils.show("Beatmap has been saved.");
                    runningProcessLabel.Text = "Beatmap has been saved.";
                    lastSaveTimeLabel.Text = DateTime.Now.ToLongTimeString();
                }));
            }));
        }

        private void reloadBeatmapIfNecessary()
        {
            if (isSelectedBeatmapOpenInOsuEditor())
            {
                Process p = Process.GetProcessesByName("osu!").FirstOrDefault();
                if (p != null)
                {
                    IntPtr h = p.MainWindowHandle;
                    SetForegroundWindow(h);
                    inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.F5);
                    Thread.Sleep(100);
                    inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.F5);
                    Thread.Sleep(1000);
                    inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.F1);
                    Thread.Sleep(100);
                    inputSimulator.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.F1);
                    Thread.Sleep(100);
                }
            }
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

        private string getOsuBeatmapNameInEditor()
        {
            Process[] processes = Process.GetProcessesByName("osu!");
            if (processes.Length > 0)
            {
                // The program was opened while osu was running.
                Process osuProcess = processes[0];
                string fileName = osuProcess.MainModule.FileName;
                string windowTitle = osuProcess.MainWindowTitle.Trim();
                return getBeatmapFileName(windowTitle);
            }
            else
                return "";
        }

        private bool isAnyMapOpenInOsuEditor()
        {
            return getOsuBeatmapNameInEditor().EndsWith(".osu");
        }

        private bool isSelectedBeatmapOpenInOsuEditor()
        {
            return beatmap != null && getOsuBeatmapNameInEditor().Equals(beatmap.FileName);
        }

        private void determineInitialProcess()
        {
            if (isAnyMapOpenInOsuEditor())
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
                        string beatmapFileName = getOsuBeatmapNameInEditor();
                        string songsPath = getSongsPathFromProcess();
                        runningProcessLabel.Visible = true;
                        runningProcessLabel.Text = "Searching for " + beatmapFileName;
                        ThreadUtils.executeOnBackground(new Action(() => 
                            searchCurrentOpenBeatmap(beatmapFileName, songsPath)));
                    }
                    else
                    {
                        // Continue with last saved path if it exists.
                    }
                }));
            }
        }

        private void searchCurrentOpenBeatmap(string beatmapFileName, string songsPath)
        {
            // First, try to search the directory name directly, and if we can find it,
            // open that folder immediately.
            // Else, search all files from the scratch.

            string[] directories = Directory.GetDirectories(songsPath);
            string nameWithoutExtension = beatmapFileName.Replace(".osu", "");
            nameWithoutExtension = nameWithoutExtension.Substring(0, nameWithoutExtension.IndexOf('(')).Trim();

            List<string> matchedDirectories = new List<string>();
            foreach (string directory in directories)
            {
                if (directory.Contains(nameWithoutExtension))
                    matchedDirectories.Add(directory);
            }

            if (matchedDirectories.Count == 1)
            {
                string directoryPath = matchedDirectories[0] + "\\" + beatmapFileName;
                loadBeatmap(directoryPath, beatmapFileName);
                return;
            }

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
            if (osuWindowTitle.StartsWith("osu!cuttingedge"))
            {
                return returnMapName(osuWindowTitle, StringUtils.getIndexOfWithCount(osuWindowTitle, " ", 3));
            }
            else if (osuWindowTitle.StartsWith("osu!"))
            {
                return returnMapName(osuWindowTitle, StringUtils.getIndexOfWithCount(osuWindowTitle, " ", 2));
            }
            else if (!string.IsNullOrWhiteSpace(osuWindowTitle))
            {
                throw new InvalidOperationException("Beatmap search is only supported in stable and cutting edge versions.");
            }

            // At this point, map name cannot be null. Return the name and display it on label, also
            // start searching on background thread.
            return osuWindowTitle;
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
            Invoke(new Action(() =>
            {
                MessageBoxUtils.show(popupMessage);
                runningProcessLabel.Text = runningProcessLabelMessage;
                saveBeatmap(saveBeatmapMessage);
            }));
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
                        showMessageAndSaveBeatmap("Converted all hitsounds to claps successfully.",
                                "Converted all hitsounds to claps.",
                                "Converted all hitsounds to claps");
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
                                showMessageAndSaveBeatmap("Re-positioned notes successfully.",
                                        "Re-positioned notes for Taiko mode successfully.",
                                        "Re-positioned notes for Taiko mode");
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
                    showMessageAndSaveBeatmap("Added SV changes successfully.",
                            "Added SV changes successfully.",
                            "Added SV changes");
                }));
            }
        }

        private void snapGreenToRedPointsButton_Click(object sender, EventArgs e)
        {
            using (TimingRegionSelector selector = new TimingRegionSelector((sender as Control).Text))
            {
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    ThreadUtils.executeOnBackground(new Action(() =>
                    {
                        InheritedPointUtils.SnapInheritedPointsOnClosestTimingPoints(this, beatmap,
                            selector.FirstOffset, selector.LastOffset);
                        showMessageAndSaveBeatmap("Snapped the inherited points in the region to the closest timing points.",
                            "Snapped the inherited points in the region to the closest timing points.",
                            "Snapped green points to red points");
                    }));
                }
            }
        }

        private void playTaikoDiffsButton_Click(object sender, EventArgs e)
        {
            /*using (TaikoPlayerWindow window = new TaikoPlayerWindow())
            {
                window.Run(60.0);
            }*/
            MessageBoxUtils.show("This function is under development.");
        }
        #endregion

        private void timingInconsistenciesButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                ThreadUtils.executeOnBackground(new Action(() =>
                {
                    // Fetch all .osu files using the root folder path,
                    // including the current open one.
                    string folderPath = beatmap.FolderPath;
                    List<Beatmap> beatmapList = new List<Beatmap>();
                    foreach (string file in Directory.GetFiles(folderPath, "*.osu"))
                        beatmapList.Add(new Beatmap(file, false));

                    if (beatmapList.Count == 1)
                    {
                        MessageBoxUtils.showError("This beatmapset only contains 1 beatmap. Aborting.");
                        return;
                    }

                    // Create the line list with colors. We need to display it in a text form
                    // in WebBrowser.
                    StringBuilder builder = new StringBuilder();

                    builder.Append("<!DOCTYPE html><html><head><style>p{margin:1px},p.warning{margin:1px;color:#e53935}</style></head><body>");

                    // Then, start checking for timing and inherited points.

                    // First, check total of red points in all diffs. If they are inconsistent,
                    // it means the timing is wrong anyway. We should dump those first.
                    Dictionary<Beatmap, List<TimingPoint>> timingPointsPerBeatmap = new Dictionary<Beatmap, List<TimingPoint>>();
                    foreach (Beatmap beatmap in beatmapList)
                        timingPointsPerBeatmap.Add(beatmap, beatmap.TimingPoints.FindAll(target => !target.IsInherited));

                    // Now that we have all the timing points, check sizes first. If sizes are not equal, we should
                    // dump the timing point states by eliminating the differences.

                    // Get the elements as list. Order is not important.
                    List<List<TimingPoint>> allPoints = timingPointsPerBeatmap.Values.ToList();

                    // Now, we need to print inconsistent timing points. If the 
                    // specified offset exists in the point, we print it with black color.
                    // Otherwise, we print it with red color.
                    const string redColor = "#e53935";
                    string newLine = Environment.NewLine;

                    // First, add all timestamps of points into a sorted set. Then, 
                    // we will query the point per list and determine whether it exists
                    // on one beatmap and not on other.
                    SortedSet<double> pointPositions = new SortedSet<double>();
                    for (int j = 0; j < allPoints.Count; j++)
                    {
                        for (int k = 0; k < allPoints[j].Count; k++)
                        {
                            pointPositions.Add(allPoints[j][k].Offset);
                        }
                    }

                    bool inconsistentCountFound = false;
                    for (int i = 0; i < allPoints.Count - 1; i++)
                    {
                        // To skip the ones we already checked, apply this logic.
                        if (i != 0 && i < allPoints.Count - 2)
                            i++;

                        List<TimingPoint> first = allPoints[i];
                        List<TimingPoint> second = allPoints[i + 1];

                        // Check all positions exist in point positions list in both of the lists.
                        bool firstMatchesAll = first.TrueForAll(target => pointPositions.Contains(target.Offset));
                        bool secondMatchesAll = first.TrueForAll(target => pointPositions.Contains(target.Offset));

                        if (!inconsistentCountFound && (first.Count != second.Count || !firstMatchesAll || !secondMatchesAll))
                        {
                            // We already have a difference. Record which timing points exist and which do not.
                            // For this occasion, the format is like:
                            /*
                             * Uninherited point inconsistency found across all difficulties.
                             * 
                             * Easy:
                             * 00:00:000 - exists.
                             * 00:01:000 - exists.
                             * 
                             * Medium:
                             * 00:00:000 - exists.
                             * 00:01:000 - does not exist.
                             */

                            // Start printing inconsistent timing points.
                            builder.Append("<p>Uninherited point count inconsistency found across all difficulties.</p>");
                            builder.Append("</br>");
                            
                            foreach(KeyValuePair<Beatmap, List<TimingPoint>> pair in timingPointsPerBeatmap)
                            {
                                builder.Append("<p>").Append(pair.Key.DifficultyName).Append(":</p>");

                                // Check every position.
                                foreach (double position in pointPositions)
                                {
                                    // Print the text with proper HTML formatting.
                                    bool exists = pair.Value.Find(target => target.Offset == position) != null;
                                    if (exists)
                                        builder.Append("<p>")
                                            .Append(StringUtils.GetOffsetWithLink(position))
                                            .Append(" - exists.")
                                            .Append("</p>");
                                    else
                                        builder.Append("<p class=\"warning")
                                            .Append("\">")
                                            .Append(StringUtils.GetOffsetWithLink(position))
                                            .Append(" - does not exist.")
                                            .Append("</p>");
                                }
                            }

                            // Since we will print an output to the entire of the list here, we do not need to check
                            // this occasion once we find an inconsistency.
                            inconsistentCountFound = true;
                        }
                    }

                    // If we have found inconsistent timing points, just return.
                    // That problem is more important anyway.
                    if (inconsistentCountFound)
                    {
                        builder.Append("</body></html>");
                        Invoke(new Action(() =>
                        {
                            using (InconsistencyResultForm form = new InconsistencyResultForm(builder.ToString()))
                            {
                                builder.Clear();
                                form.ShowDialog();
                                form.Dispose();
                            }
                        }));
                        return;
                    }

                    // The check above basically ensures that there are points within all offsets anyway,
                    // so directly check omitted and non-omitted barlines.
                    // This should give insight as to which ones are inconsistent in which diffs.
                    bool inconsistencyFound = false;
                    for (int i = 0; i < allPoints.Count - 1; i++)
                    {
                        // To skip the ones we already checked, apply this logic.
                        if (i != 0 && i < allPoints.Count - 2)
                            i++;

                        List<TimingPoint> first = allPoints[i];
                        List<TimingPoint> second = allPoints[i + 1];

                        // Assuming the sizes are the same, cross-reference each index
                        // with their equal omit values.

                        // First, check if we added the title for this occasion.
                        bool isOmitTitleAdded = false;

                        // Allocate an array of beatmapset count, hold the values temporarily
                        // and find the difference per object.
                        bool[] omitStatus = new bool[timingPointsPerBeatmap.Count];
                        for (int j = 0; j < first.Count; j++)
                        {
                            TimingPoint firstElement = first[j];
                            TimingPoint secondElement = second[j];

                            if (firstElement.IsOmitted != secondElement.IsOmitted)
                            {
                                // Well, we found a mismatched omitted barline.
                                // Now we should print the differences.
                                inconsistencyFound = true;

                                if (!isOmitTitleAdded)
                                {
                                    builder.Append("<p>Omitted barline inconsistencies found on red points.<p></br>");
                                    isOmitTitleAdded = true;
                                }

                                // Work per list item position, a.k.a single offset per beatmap.
                                for (int pos = 0; pos < first.Count; pos++)
                                {
                                    int mapIndex = 0;
                                    foreach (KeyValuePair<Beatmap, List<TimingPoint>> pair in timingPointsPerBeatmap)
                                    {
                                        omitStatus[mapIndex++] = pair.Value[pos].IsOmitted;
                                    }

                                    int omitStatusTrue = 0;
                                    int omitStatusFalse = 0;
                                    foreach (bool val in omitStatus)
                                    {
                                        if (val)
                                            omitStatusTrue++;
                                        else
                                            omitStatusFalse++;
                                    }

                                    // Find the default omit status.
                                    bool omitStatusDefault = omitStatusTrue >= omitStatusFalse;
                                    
                                    // Reset the omit status to re-use.
                                    for (int k = 0; k < omitStatus.Length; k++) omitStatus[k] = false;

                                    // Now that we know the default omit and not omit states, print the different ones.
                                    // If we find a value that is different from others, we will print it.

                                    // Check if there is a different one. Technically if both are non-zero, there should be an inconsistent one.
                                    if (omitStatusTrue != 0 && omitStatusFalse != 0)
                                    {
                                        builder.Append("<p>Omit status for uninherited point at ").Append(StringUtils.GetOffsetWithLink(first[pos].Offset)).Append("</p>");
                                        foreach (KeyValuePair<Beatmap, List<TimingPoint>> pair in timingPointsPerBeatmap)
                                        {
                                            Beatmap beatmap = pair.Key;
                                            List<TimingPoint> points = pair.Value;
                                            if (pair.Value[pos].IsOmitted != omitStatusDefault)
                                                builder.Append("<p class=\"warning")
                                                    .Append("\">")
                                                    .Append(pair.Key.DifficultyName)
                                                    .Append(": ")
                                                    .Append(points[pos].IsOmitted ? "Omitted" : "Not omitted")
                                                    .Append("</p>");
                                            else
                                                builder.Append("<p>")
                                                    .Append(pair.Key.DifficultyName)
                                                    .Append(": ")
                                                    .Append(points[pos].IsOmitted ? "Omitted" : "Not omitted")
                                                    .Append("</p>");
                                        }
                                        builder.Append("</br>");
                                    }
                                }

                                // At this point, we are basically done. Exit from the loop.
                                break;
                            }
                        }
                    }

                    // If we have found an inconsistency, we should open up a new window and show it.
                    // Otherwise we need to show a success message box.
                    if (inconsistencyFound)
                    {
                        builder.Append("</body></html>");
                        Invoke(new Action(() =>
                        {
                            using (InconsistencyResultForm form = new InconsistencyResultForm(builder.ToString()))
                            {
                                builder.Clear();
                                form.ShowDialog();
                                form.Dispose();
                            }
                        }));
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBoxUtils.show("No inconsistencies found in red points on this mapset.");
                        }));
                    }
                }));
            }
        }
    }
}
