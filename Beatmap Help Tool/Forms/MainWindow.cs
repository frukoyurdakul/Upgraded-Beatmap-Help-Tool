﻿using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Forms;
using Beatmap_Help_Tool.Models;
using Beatmap_Help_Tool.Properties;
using Beatmap_Help_Tool.TaikoPlayer;
using Beatmap_Help_Tool.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using static Beatmap_Help_Tool.Utils.HtmlUtils;

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
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                hideButtonsWithNoClickListeners(this);
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

        // Save hotkey function
        public void onSaveHotkey()
        {
            ThreadUtils.executeOnBackground(() =>
            {
                Thread.Sleep(100);
                if (isSelectedBeatmapOpenInOsuEditor())
                    beatmap.reload(mainDisplayView);
            });
        }

        private bool handleHtmlDisplayer(HtmlDisplayer htmlDisplayer, string successMessage)
        {
            if (htmlDisplayer.containsElements())
            {
                this.Invoke(() =>
                {
                    using (InconsistencyResultForm form = new InconsistencyResultForm(htmlDisplayer.ToString()))
                    {
                        htmlDisplayer.recycle();
                        form.ShowDialog();
                        form.Dispose();
                    }
                });
                return true;
            }
            else
            {
                this.Invoke(() => showSuccessMessage(successMessage));
                return false;
            }
        }

        private bool handleHtmlDisplayer(HtmlDisplayer htmlDisplayer, string successMessage, onPreActionCallback callback)
        {
            if (htmlDisplayer.containsElements())
            {
                if (callback != null)
                    callback.Invoke();

                this.Invoke(() =>
                {
                    using (InconsistencyResultForm form = new InconsistencyResultForm(htmlDisplayer.ToString()))
                    {
                        htmlDisplayer.recycle();
                        form.ShowDialog();
                        form.Dispose();
                    }
                });
                return true;
            }
            else
            {
                this.Invoke(() => showSuccessMessage(successMessage));
                return false;
            }
        }

        private void showSuccessMessage(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                MessageBoxUtils.show(text);
        }

        private void hideButtonsWithNoClickListeners(Control control)
        {
            foreach (Control child in control.Controls)
            {
                if (child is Button)
                {
                    if (!HasEventHandler(child, "EventClick"))
                    {
                        child.Hide();
                        child.Parent.Hide();
                    }
                }
                hideButtonsWithNoClickListeners(child);
            }
        }

        private bool HasEventHandler(Control control, string eventName)
        {
            EventHandlerList events =
                (EventHandlerList)
                typeof(Component)
                 .GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance)
                 .GetValue(control, null);

            object key = typeof(Control)
                .GetField(eventName, BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);

            Delegate handlers = events[key];

            return handlers != null && handlers.GetInvocationList().Any();
        }

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
                ThreadUtils.executeOnBackground(() => loadBeatmap(dialog.FileName, dialog.SafeFileName));
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
            this.Invoke(() =>
            {
                MessageBoxUtils.show(popupMessage);
                runningProcessLabel.Text = runningProcessLabelMessage;
                saveBeatmap(saveBeatmapMessage);
            });
        }

        private void showFailureMessage(string popupMessage)
        {
            this.Invoke(() => MessageBoxUtils.showWarning(popupMessage));
        }

        private void loadBeatmap(string targetPath, string beatmapFileName)
        {
            // At this point, file path has been found, load the beatmap and
            // set the data to datagridview.
            // First, invoke the running process label.
            this.Invoke(() => runningProcessLabel.Text = beatmapFileName + " has been found, loading...");
            beatmap = new Beatmap(targetPath);
            this.Invoke(() =>
            {
                string finalPath = Directory.GetParent(targetPath).FullName;
                beatmap.fillMainDisplayView(mainDisplayView);
                enableTabs();
                runningProcessLabel.Text = beatmapFileName + " has been loaded.";
                Text = "Beatmap Help Tool - " + beatmapFileName;
                filePathTextBox.Text = finalPath;
                SharedPreferences.edit().put(PreferencesKeys.LAST_BEATMAP_DIR_PATH, finalPath).apply();
                tabControl1.SelectedIndex = 0;
            });
        }

        private void saveBeatmap(string action)
        {
            ThreadUtils.executeOnBackground(new Action(() =>
            {
                this.Invoke(() => runningProcessLabel.Text = "Saving beatmap...");
                beatmap.save(action);
                this.Invoke(() =>
                {
                    MessageBoxUtils.show("Beatmap has been saved.");
                    runningProcessLabel.Text = "Beatmap has been saved.";
                    lastSaveTimeLabel.Text = DateTime.Now.ToLongTimeString();
                    ThreadUtils.executeOnBackground(() =>
                    {
                        beatmap.reload(mainDisplayView);
                        this.Invoke(() => beatmap.fillMainDisplayView(mainDisplayView));
                        reloadBeatmapIfNecessary();
                    });
                });
            }));
        }

        private void saveBeatmap(string action, string path)
        {
            ThreadUtils.executeOnBackground(() =>
            {
                this.Invoke(() => runningProcessLabel.Text = "Saving beatmap to path: " + path + "\\" + beatmap.FileName);
                beatmap.save(action, path);
                this.Invoke(() =>
                {
                    MessageBoxUtils.show("Beatmap has been saved.");
                    runningProcessLabel.Text = "Beatmap has been saved.";
                    lastSaveTimeLabel.Text = DateTime.Now.ToLongTimeString();
                });
                ThreadUtils.executeOnBackground(() =>
                {
                    beatmap.reload(mainDisplayView);
                    this.Invoke(() => beatmap.fillMainDisplayView(mainDisplayView));
                    reloadBeatmapIfNecessary();
                });
            });
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
                        loadCurrentOpenBeatmap();
                    }
                    else
                    {
                        // Continue with last saved path if it exists.
                    }
                }));
            }
        }

        private void loadCurrentOpenBeatmap()
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
                return returnMapName(osuWindowTitle, StringUtils.GetIndexOfWithCount(osuWindowTitle, " ", 3));
            }
            else if (osuWindowTitle.StartsWith("osu!"))
            {
                return returnMapName(osuWindowTitle, StringUtils.GetIndexOfWithCount(osuWindowTitle, " ", 2));
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
            (nominationFunctionsPage as Control).Enabled = true;
        }

        private void disableTabs()
        {
            (generalFunctionsPage as Control).Enabled = false;
            (svFunctionsPage as Control).Enabled = false;
            (bpmFunctionsPage as Control).Enabled = false;
            (nominationFunctionsPage as Control).Enabled = false;
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
            ThreadUtils.executeOnBackground(determineInitialProcess);
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
                ThreadUtils.executeOnBackground(() =>
                {
                    Beatmap beatmap = Beatmap.getPreviousSavedState();
                    if (beatmap != null)
                    {
                        this.beatmap = beatmap;
                        this.Invoke(() =>
                        {
                            beatmap.fillMainDisplayView(mainDisplayView);
                            runningProcessLabel.Text = string.Format("Previous state loaded ({0})",
                                Beatmap.getSavedStateAction());
                        });
                    }
                    else
                    {
                        this.Invoke(() => runningProcessLabel.Text = "An error occurred while fetching previous state.");
                    }
                });
            }
            else
                runningProcessLabel.Text = "There are no other previous states.";
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            if (Beatmap.hasNextState())
            {
                runningProcessLabel.Text = "Redoing...";
                ThreadUtils.executeOnBackground(() =>
                {
                    Beatmap beatmap = Beatmap.getNextSavedState();
                    if (beatmap != null)
                    {
                        this.beatmap = beatmap;
                        this.Invoke(() =>
                        {
                            beatmap.fillMainDisplayView(mainDisplayView);
                            runningProcessLabel.Text = string.Format("Next state loaded ({0})",
                                Beatmap.getSavedStateAction());
                        });
                    }
                    else
                    {
                        this.Invoke(() => runningProcessLabel.Text = "An error occurred while fetching next state.");
                    }
                });
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

        private void whistleToClapButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                if (MessageBoxUtils.showQuestionYesNo("Are you sure?") == DialogResult.Yes)
                {
                    runningProcessLabel.Text = "Converting all hitsounds to claps...";
                    ThreadUtils.executeOnBackground(() =>
                    {
                        NoteUtils.setAllWhistlesToClaps(beatmap);
                        showMessageAndSaveBeatmap("Converted all hitsounds to claps successfully.",
                                "Converted all hitsounds to claps.",
                                "Converted all hitsounds to claps");
                    });
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
                            ThreadUtils.executeOnBackground(() =>
                            {
                                if (form.applyToAllDiffs)
                                {
                                    SearchUtils.GetAllBeatmaps(beatmap, out List<Beatmap> allMaps);
                                    allMaps = allMaps.Where(x => x.isModeTaiko()).ToList();
                                    foreach (Beatmap beatmap in allMaps)
                                    {
                                        NoteUtils.positionAllNotesForTaiko(beatmap, donPositions, katPositions, donFinishPositions, katFinishPositions);
                                        beatmap.overwrite();
                                    }
                                    this.Invoke(() =>
                                    {
                                        MessageBoxUtils.show("Re-positioned all notes on all taiko difficulties in this beatmapset.");
                                        ThreadUtils.executeOnBackground(() =>
                                        {
                                            beatmap.reload(mainDisplayView);
                                            reloadBeatmapIfNecessary();
                                        });
                                    });
                                }
                                else
                                {
                                    NoteUtils.positionAllNotesForTaiko(beatmap, donPositions, katPositions, donFinishPositions, katFinishPositions);
                                    showMessageAndSaveBeatmap("Re-positioned notes successfully.",
                                            "Re-positioned notes for Taiko mode successfully.",
                                            "Re-positioned notes for Taiko mode");
                                }
                            });
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
                ThreadUtils.executeOnBackground(() =>
                {
                    bool result = InheritedPointUtils.AddSvChanges(this, beatmap, firstOffset, lastOffset, firstSv, lastSv,
                        targetBpm, gridSnap, svOffset, svIncreaseMode, count, svIncreaseMultiplier, putPointsByNotes);
                    if (result)
                    {
                        showMessageAndSaveBeatmap("Added SV changes successfully.",
                            "Added SV changes successfully.",
                            "Added SV changes");
                    }
                });
            }
        }

        private void equalizeSvForAllPointsButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                if (!beatmap.isModeTaiko())
                {
                    if (MessageBoxUtils.showQuestionYesNo("You are about to open a map that is not osu!taiko. This function is specifically designed for osu!taiko maps." + Environment.NewLine + Environment.NewLine + "Are you sure you want to continue?") == DialogResult.No)
                    {
                        MessageBoxUtils.show("Process aborted.");
                        return;
                    }
                }

                // Now open the adjustment panel.
                using (EqualizeSvForm form = new EqualizeSvForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // We should have all the values here.
                        // Gather the starting and ending point of the objects and equalize SV
                        // based on the settings.

                        // Check for verifications first.
                        if (beatmap.TimingPoints.Count == 0)
                        {
                            MessageBoxUtils.showError("No timing points or inherited points found on this map. Aborting.");
                            return;
                        }

                        // Switch to background thread.
                        ThreadUtils.executeOnBackground(() =>
                        {
                            // Check if the equalization attempt is successful.
                            bool result = InheritedPointUtils.EqualizeSvInArea(form, beatmap);

                            // At this point, we have the changes. Save the beatmap.
                            if (result)
                                showMessageAndSaveBeatmap("The specified range has been equalized.", "Equalize SV function", "EqualizeSvFunction");
                        });
                    }
                }
            }
        }

        private void snapGreenToRedPointsButton_Click(object sender, EventArgs e)
        {
            using (TimingRegionSelector selector = new TimingRegionSelector((sender as Control).Text.ReplaceNewLinesWithSpaces()))
            {
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    ThreadUtils.executeOnBackground(() =>
                    {
                        if (InheritedPointUtils.SnapInheritedPointsOnClosestTimingPoints(this, beatmap,
                            selector.FirstOffset, selector.LastOffset))
                        {
                            showMessageAndSaveBeatmap("Snapped the inherited points in the region to the closest timing points.",
                           "Snapped the inherited points in the region to the closest timing points.",
                           "Snapped green points to red points");
                        }
                        else
                            showFailureMessage("Process aborted.");
                    });
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
                ThreadUtils.executeOnBackground(() =>
                {
                    // If we only have 1 beatmap, then bail. It's not necessary to parse and check for inconsistencies.
                    if (SearchUtils.GetBeatmapCountInMapset(beatmap) == 1)
                    {
                        MessageBoxUtils.showWarning("This beatmapset only contains 1 beatmap. The function only works for beatmapsets which contains more than 2 maps. Aborting.");
                        return;
                    }

                    // Fetch beatmaps with necessary information.
                    SearchUtils.GetBeatmapPointsInMapset(beatmap, out Dictionary<Beatmap, List<TimingPoint>> timingPointsPerBeatmap, out List<List<TimingPoint>> allPoints);

                    // Create the line list with colors. We need to display it in a text form
                    // in WebBrowser.
                    HtmlDisplayer htmlDisplayer = newHtmlDisplayer();

                    // Then, start checking for timing and inherited points.

                    // Now, we need to print inconsistent timing points. If the 
                    // specified offset exists in the point, we print it with black color.
                    // Otherwise, we print it with red color.

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
                            htmlDisplayer.addLineWithBreak("Uninherited point count inconsistency found across all difficulties.");

                            foreach (KeyValuePair<Beatmap, List<TimingPoint>> pair in timingPointsPerBeatmap)
                            {
                                htmlDisplayer.addLine(pair.Key.DifficultyName);

                                // Check every position.
                                foreach (double position in pointPositions)
                                {
                                    // Print the text with proper HTML formatting.
                                    bool exists = pair.Value.Find(target => target.Offset == position) != null;
                                    if (exists)
                                        htmlDisplayer.addLine(StringUtils.GetOffsetWithLink(position) + " - exists.");
                                    else
                                        htmlDisplayer.addLine(StringUtils.GetOffsetWithLink(position) + " - does not exist.");
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
                        this.Invoke(() =>
                        {
                            using (InconsistencyResultForm form = new InconsistencyResultForm(htmlDisplayer.ToString()))
                            {
                                htmlDisplayer.recycle();
                                form.ShowDialog();
                                form.Dispose();
                            }
                        });
                        return;
                    }

                    // The check above basically ensures that there are points within all offsets anyway,
                    // so directly check omitted and non-omitted barlines.
                    // This should give insight as to which ones are inconsistent in which diffs.
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

                                if (!isOmitTitleAdded)
                                {
                                    htmlDisplayer.addLineWithBreak("Omitted barline inconsistencies found on red points.");
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
                                        htmlDisplayer.addLine("Omit status for uninherited point at " + StringUtils.GetOffsetWithLink(first[pos].Offset));
                                        foreach (KeyValuePair<Beatmap, List<TimingPoint>> pair in timingPointsPerBeatmap)
                                        {
                                            Beatmap beatmap = pair.Key;
                                            List<TimingPoint> points = pair.Value;
                                            if (pair.Value[pos].IsOmitted != omitStatusDefault)
                                                htmlDisplayer.addWarning(pair.Key.DifficultyName + ": " + (points[pos].IsOmitted ? "Omitted" : "Not omitted"));
                                            else
                                                htmlDisplayer.addLine(pair.Key.DifficultyName + ": " + (points[pos].IsOmitted ? "Omitted" : "Not omitted"));
                                        }
                                        htmlDisplayer.addLineBreak();
                                    }
                                }

                                // At this point, we are basically done. Exit from the loop.
                                break;
                            }
                        }
                    }

                    // If we have found an inconsistency, we should open up a new window and show it.
                    // Otherwise we need to show a success message box.
                    handleHtmlDisplayer(htmlDisplayer, "No inconsistencies found in red points on this mapset.");
                });
            }
        }

        private void checkDoubleBarlinesButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                ThreadUtils.executeOnBackground(() =>
                {
                    // Fetch all .osu files using the root folder path,
                    // including the current open one.
                    SearchUtils.GetBeatmapPointsInMapset(beatmap, out Dictionary<Beatmap, List<TimingPoint>> timingPointsPerBeatmap, out List<List<TimingPoint>> allPoints);

                    // Now that we have all the timing points, we can start checking for double barlines.
                    // Target barlines will be found by starting from the first one, checking if they are omitted
                    // or not, then add lines until the closest timing point changes. If there is only 1 line,
                    // then the check will be skipped immediately.

                    // Create a html displayer.
                    HtmlDisplayer htmlDisplayer = newHtmlDisplayer();

                    // Create the list for barlines. Avoid doing this
                    // in the foreach loop and use clear instead.
                    List<double> barlines;

                    // If any of them is calculated before, make this as true.
                    bool isAnyCalculated = false;

                    // If any of them is skipped before, make this as true.
                    bool isAnySkipped = false;

                    // If the inconsistencies have print, this becomes true. Write
                    // the inconsistencies only once.
                    bool isInconsistenciesWritten = false;

                    // If we find any double barlines here, write them down.
                    // This defines whether a title for double barlines has been
                    // written down.
                    bool isDoubleBarlineTitleWritten = false;

                    foreach (List<TimingPoint> redPoints in allPoints)
                    {
                        bool isBeatmapDifficultyWritten = false;
                        bool isFirstLineBreakAdded = false;
                        if (redPoints.Count == 1)
                        {
                            isAnySkipped = true;
                            if (isAnyCalculated)
                            {
                                // There is an inconsistency here. The red points are missing or excessive on
                                // one of them. Find the differences here.
                                if (!isInconsistenciesWritten)
                                {
                                    if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                        htmlDisplayer.addLineBreak();
                                    isFirstLineBreakAdded = true;
                                    TimingPointUtils.addInconsistentTimingPoints(htmlDisplayer, timingPointsPerBeatmap);
                                    isInconsistenciesWritten = true;
                                }
                            }
                            continue;
                        }

                        // Now that we know there are at least 2 timing points, get all the barline offsets
                        // in a list. If any of them are close within each other less than 20 ms, then it is
                        // considered as a double barline.

                        // This adjusts the double barline value.
                        const double closestBarlineGapMillis = 20d;

                        // Fetch the beatmap.
                        Beatmap beatmap = timingPointsPerBeatmap.FirstOrDefault(x => x.Value == redPoints).Key;

                        // Get all the necessary information we need.
                        // In this case it's barlines only without getting dangerous barlines.
                        SearchUtils.GetBarlines(redPoints, beatmap.HitObjects, out barlines);

                        // At this point, we know all the barlines. Now, get
                        // differences on all barlines after making the list distinct.
                        //
                        // The distinct process is a bit different: since the calculated
                        // offsets are in double, there might be an integer offset
                        // with the same double offset. So, if two of them are closer than
                        // 1 millisecond, keep the integer one.
                        for (int i = 0; i < barlines.Count - 1; i++)
                        {
                            // Keep the integer one if the next barline is 
                            // a double but there is another exist in integer form,
                            // which is probably from the 2nd point.
                            double firstBarline = barlines[i];
                            double secondBarline = barlines[i + 1];
                            if (secondBarline - firstBarline < 1d)
                                barlines.RemoveAt(i--);
                            // Actual check for double barlines is here. We need to
                            // determine whether the next point is too close (a.k.a 
                            // value of closestBarlineGapMillis variable).
                            else if (secondBarline - firstBarline <= closestBarlineGapMillis)
                            {
                                // If title is not written, then write it down.
                                if (!isDoubleBarlineTitleWritten)
                                {
                                    if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                        htmlDisplayer.addLineBreak();
                                    isFirstLineBreakAdded = true;
                                    htmlDisplayer.addSection("Double barlines detected.");
                                    isDoubleBarlineTitleWritten = true;
                                }

                                // Add the beatmap difficulty with a line break and add the warning text.
                                if (!isBeatmapDifficultyWritten)
                                {
                                    if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                        htmlDisplayer.addLineBreak();
                                    isFirstLineBreakAdded = true;
                                    htmlDisplayer.addSubsection("Beatmap difficulty: " + beatmap.DifficultyName);
                                    isBeatmapDifficultyWritten = true;
                                }
                                if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                    htmlDisplayer.addLineBreak();
                                isFirstLineBreakAdded = true;
                                htmlDisplayer.addLine("At " + StringUtils.GetOffsetWithLink((double)firstBarline) + " and " + StringUtils.GetOffsetWithLink((double)secondBarline));
                            }
                        }

                        isAnyCalculated = true;
                        if (isAnySkipped)
                        {
                            // There is an inconsistency here. The red points are missing or excessive on
                            // one of them. Find the differences here.
                            if (!isInconsistenciesWritten)
                            {
                                TimingPointUtils.addInconsistentTimingPoints(htmlDisplayer, timingPointsPerBeatmap);
                                isInconsistenciesWritten = true;
                            }
                        }
                    }

                    // At this point, either we have a window to display, or everything is correct
                    // with this mapset. We can understand that if we have added any lines
                    // to the html displayer.
                    handleHtmlDisplayer(htmlDisplayer, "No double barlines found in this mapset.");
                });
            }
        }

        private void loadCurrentBeatmapButton_Click(object sender, EventArgs e)
        {
            if (isAnyMapOpenInOsuEditor())
                loadCurrentOpenBeatmap();
            else
                MessageBoxUtils.showError("No beatmaps are currently open inside osu!, skipping load attempt." + Environment.NewLine + Environment.NewLine + "If you have loaded any beatmaps with this tool, click \"Browse\" button to open last loaded beatmap folder.");
        }

        private void flyingBarlinesButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                ThreadUtils.executeOnBackground(() =>
                {
                    // Fetch all .osu files using the root folder path,
                    // including the current open one.
                    string folderPath = beatmap.FolderPath;
                    List<Beatmap> beatmapList = new List<Beatmap>();
                    foreach (string file in Directory.GetFiles(folderPath, "*.osu"))
                        beatmapList.Add(new Beatmap(file, false));

                    // First, check total of red points in all diffs. If they are inconsistent,
                    // it means the timing is wrong anyway. We should dump those first.
                    Dictionary<Beatmap, List<TimingPoint>> timingPointsPerBeatmap = new Dictionary<Beatmap, List<TimingPoint>>();
                    foreach (Beatmap beatmap in beatmapList)
                        timingPointsPerBeatmap.Add(beatmap, beatmap.TimingPoints.FindAll(target => !target.IsInherited));

                    // Get the elements as list. Order is not important.
                    List<List<TimingPoint>> allPoints = timingPointsPerBeatmap.Values.ToList();

                    // Now that we have all the timing points, we can start checking for double barlines.
                    // Target barlines will be found by starting from the first one, checking if they are omitted
                    // or not, then add lines until the closest timing point changes. If there is only 1 line,
                    // then the check will be skipped immediately.

                    // Create a html displayer.
                    HtmlDisplayer htmlDisplayer = newHtmlDisplayer();

                    // There should be a margin for this approach. And, that is,
                    // to get the inherited point that is closest to the 1/36 snap.
                    // If there is a point with the SV change, it might have been put
                    // mistakenly. We should point those out as well.
                    double snapDivisor = 36;

                    // If any of them is calculated before, make this as true.
                    bool isAnyCalculated = false;

                    // If any of them is skipped before, make this as true.
                    bool isAnySkipped = false;

                    // If the inconsistencies have print, this becomes true. Write
                    // the inconsistencies only once.
                    bool isInconsistenciesWritten = false;

                    // If we find any double barlines here, write them down.
                    // This defines whether a title for double barlines has been
                    // written down.
                    bool isFlyingBarlineTitleWritten = false;

                    // This is the core dictionary to add the elements if we ever find any.
                    Dictionary<Beatmap, List<TimingPoint>> mistakenInheritedPointsDict = new Dictionary<Beatmap, List<TimingPoint>>();

                    foreach (List<TimingPoint> redPoints in allPoints)
                    {
                        bool isBeatmapDifficultyWritten = false;
                        bool isFirstLineBreakAdded = false;
                        if (redPoints.Count == 1)
                        {
                            isAnySkipped = true;
                            if (isAnyCalculated)
                            {
                                // There is an inconsistency here. The red points are missing or excessive on
                                // one of them. Find the differences here.
                                if (!isInconsistenciesWritten)
                                {
                                    if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                        htmlDisplayer.addLineBreak();
                                    isFirstLineBreakAdded = true;
                                    TimingPointUtils.addInconsistentTimingPoints(htmlDisplayer, timingPointsPerBeatmap);
                                    isInconsistenciesWritten = true;
                                }
                            }
                            continue;
                        }

                        // Now that we know there are at least 2 timing points, get all the barline offsets
                        // in a list. If any of them are close within each other less than 20 ms, then it is
                        // considered as a double barline.

                        // Fetch the beatmap.
                        Beatmap beatmap = timingPointsPerBeatmap.FirstOrDefault(x => x.Value == redPoints).Key;

                        // Get all the necessary information we need.
                        // In this case it's everything regarding to barlines.
                        SearchUtils.GetBarlines(redPoints, beatmap.HitObjects, out List<double> barlinesDouble, out List<decimal> barlines, out List<decimal> dangerousBarlines);

                        if (dangerousBarlines.Count > 0)
                        {
                            // Now we know that there are dangerous barlines. Let's see
                            // if any of them have SVs changed compared to the previous section
                            // at the exact same spot.

                            // This is the list for inherited points who are set
                            // a bit further of the dangerous barlines, which might have
                            // been put to change SV but failed to do so because
                            // of the gap.
                            List<TimingPoint> mistakenInheritedPoints = new List<TimingPoint>();

                            foreach (double offset in dangerousBarlines)
                            {
                                // Get the offset as integer. The added lines are probably integers anyway.
                                int offsetInt = (int)offset;

                                // Get the exact inherited point. It can be null.
                                TimingPoint exactPoint = SearchUtils.GetExactInheritedPoint(beatmap.TimingPoints, offsetInt);

                                // If the closest inherited point has the same offset, then it means we've found it.
                                if (exactPoint != null && exactPoint.Offset == offsetInt)
                                {
                                    // Now, if it exists, find the previous one. We need to determine if there is a SV change here.
                                    TimingPoint closestPoint = SearchUtils.GetClosestInheritedPoint(beatmap.TimingPoints, offsetInt - 1);

                                    if (closestPoint.Offset < offsetInt)
                                    {
                                        // We also have a point beforehand. This means we can check for SV changes.
                                        // The SVs apparently can get registered as -99.99999999999 for 1.00x SVs
                                        // from the editor and also not get fixed for unknown reasons. Ignore the
                                        // barline if the change is unnoticeably small.
                                        if (Math.Abs(exactPoint.PointValue - closestPoint.PointValue) > 0.00000000001d)
                                        {
                                            // The values are different. The SV is not applied to this barline. We need to add it.
                                            if (!isFlyingBarlineTitleWritten)
                                            {
                                                if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                                    htmlDisplayer.addLineBreak();
                                                isFirstLineBreakAdded = true;
                                                htmlDisplayer.addSection("Flying barlines detected.");
                                                isFlyingBarlineTitleWritten = true;
                                            }

                                            if (!isBeatmapDifficultyWritten)
                                            {
                                                if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                                    htmlDisplayer.addLineBreak();
                                                isFirstLineBreakAdded = true;
                                                htmlDisplayer.addSubsection("Beatmap difficulty: " + beatmap.DifficultyName);
                                                isBeatmapDifficultyWritten = true;
                                            }

                                            if (htmlDisplayer.containsElements() && !isFirstLineBreakAdded)
                                                htmlDisplayer.addLineBreak();
                                            isFirstLineBreakAdded = true;
                                            htmlDisplayer.addLine(StringUtils.GetOffsetWithLink(offsetInt));
                                        }
                                    }
                                    else
                                    {
                                        // I don't really know what this means yet. We may discover it at some point.
                                        // Currently don't do anything.
                                    }
                                }
                                else
                                {
                                    // If there is a inherited point with 1/24 snap margin to the right of this
                                    // barline, find it and record it as a different section.

                                    // Find the closest timing point.
                                    TimingPoint closestTimingPoint = SearchUtils.GetClosestTimingPoint(beatmap.TimingPoints, offsetInt);

                                    // Find the beat snap value and search an inherited point and timing point closest to it.
                                    int offsetIntWithMargin = offsetInt + Convert.ToInt32(closestTimingPoint.PointValue / snapDivisor);

                                    // Find the closest next point. If the point is different, then do not even bother checking the
                                    // next inherited point.
                                    TimingPoint closestNextTimingPoint = SearchUtils.GetClosestTimingPoint(beatmap.TimingPoints, offsetIntWithMargin);

                                    // If the timing point above is not equal to the closest timing point, assign this as null.
                                    // Otherwise, search for it and check for equality. If they are different, that means
                                    // either the next point does not exist, or it does exist and does not affect the barline.
                                    TimingPoint closestNextInheritedPoint = closestNextTimingPoint == closestTimingPoint
                                        ? SearchUtils.GetClosestInheritedPoint(beatmap.TimingPoints, offsetIntWithMargin)
                                        : null;

                                    // If we have found a point within this margin, then add this to the list. We will add these points at the end of the
                                    // html displayer.
                                    if (closestNextInheritedPoint != null && VerifyUtils.verifyRange(offsetInt, offsetIntWithMargin, closestNextInheritedPoint.Offset))
                                        mistakenInheritedPoints.Add(closestNextInheritedPoint);
                                }
                            }

                            // If we reached here, it means we have finished checking the exact points,
                            // but a the list of mistaken inherited points
                            // may have been waiting for us to put and display on the html displayer.
                            if (mistakenInheritedPoints.Count > 0)
                                mistakenInheritedPointsDict.Add(beatmap, mistakenInheritedPoints);
                        }
                        else
                        {
                            // There can't be flying barlines on this map. Move onto the next
                            // one if it exists.
                        }
                        isAnyCalculated = true;
                        if (isAnySkipped)
                        {
                            // There is an inconsistency here. The red points are missing or excessive on
                            // one of them. Find the differences here.
                            if (!isInconsistenciesWritten)
                            {
                                TimingPointUtils.addInconsistentTimingPoints(htmlDisplayer, timingPointsPerBeatmap);
                                isInconsistenciesWritten = true;
                            }
                        }
                    }

                    // Here, only one last remaining task exists, and that is to check
                    // mistaken inherited points per beatmap. If they exist, then
                    // we should add them into the html displayer.
                    if (mistakenInheritedPointsDict.Count > 0)
                    {
                        if (htmlDisplayer.containsElements())
                            htmlDisplayer.addLineBreak();

                        htmlDisplayer.addSection("Mistaken inherited points detected. These might have been put intentionally, but they do not affect to the previous barline. Fix them if necessary.");

                        foreach (KeyValuePair<Beatmap, List<TimingPoint>> pair in mistakenInheritedPointsDict)
                        {
                            Beatmap key = pair.Key;
                            List<TimingPoint> values = pair.Value;

                            htmlDisplayer.addSubsection("Beatmap difficulty: " + key.DifficultyName);
                            foreach (TimingPoint point in values)
                                htmlDisplayer.addLine(point.GetOffsetWithLink());
                            htmlDisplayer.addLineBreak();
                        }
                    }

                    // At this point, either we have a window to display, or everything is correct
                    // with this mapset. We can understand that if we have added any lines
                    // to the html displayer.
                    handleHtmlDisplayer(htmlDisplayer, "No flying barlines found in this mapset.");
                });
            }
        }

        private void unsnappedNoteBarlineButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                ThreadUtils.executeOnBackground(() =>
                {
                    // Get the information for all diffs.
                    SearchUtils.GetBeatmapPointsInMapset(beatmap, out Dictionary<Beatmap, List<TimingPoint>> timingPointsPerBeatmap, out List<List<TimingPoint>> allPoints);

                    // Create the html displayer.
                    HtmlDisplayer htmlDisplayer = newHtmlDisplayer();

                    // Check if section is created. This becomes true once the section gets created to avoid duplicates in a loop.
                    bool isSectionCreated = false;

                    // The note shift margin, declared as 10 milliseconds. Anything else above 10 milliseconds
                    // should be really visible to the eye.
                    const int noteShiftMarginMaximum = 10;

                    // The note shift margin bare minimum, to skip detections for minimum snapping errors.
                    // Currently set to 4ms, may be increased more in the future.
                    const int noteShiftMarginMinimum = 4;

                    // Loop through all beatmaps.
                    foreach (KeyValuePair<Beatmap, List<TimingPoint>> pair in timingPointsPerBeatmap)
                    {
                        // Get barlines and hitobjects.
                        SearchUtils.GetBarlines(pair.Value.FindAll(x => !x.IsInherited), pair.Key.HitObjects, out List<decimal> barlines, out List<decimal> dangerousBarlines);
                        List<HitObject> hitObjects = pair.Key.HitObjects;

                        // Now that we have all the hitobjects, check if any of them align with barlines and dangerous barlines.
                        // The gap is between 1 to 10 milliseconds. If the difference is 0 milliseconds, consider the note
                        // as snapped.

                        // Create a dictionary object for each object that we find how much is off. We will
                        // display the off zones with millis values, coloring the ones that will be
                        // marked dangerously according to "noteShiftMarginMinimum". The others
                        // will remain as black colored.
                        Dictionary<HitObject, int> hitObjectBarlineDifferences = new Dictionary<HitObject, int>();
                        hitObjects.ForEach(x =>
                        {
                            decimal offset = (decimal) x.Offset;
                            decimal closestHigherBarline = SearchUtils.GetClosestHigherValue(barlines, offset + 1);
                            decimal closestLowerBarline = SearchUtils.GetClosestLowerValue(barlines, offset - 1);
                            decimal exactBarline = (int) barlines.Find(y => (int)y == (int)offset);

                            // If condition does not match, that means this note is exactly snapped. 
                            // It should not be included in the final list.
                            if (offset != exactBarline)
                            {
                                // There is not an exact barline for this note. We should consider the 
                                // closest higher and lower barlines for this approach.
                                int lowerDifference = Math.Abs((int)(offset - closestLowerBarline));
                                int higherDifference = Math.Abs((int)(offset - closestHigherBarline));
                                bool isUnsnapped = VerifyUtils.verifyRangeAny(1, noteShiftMarginMaximum, lowerDifference, higherDifference);
                                if (isUnsnapped)
                                {
                                    int marginBetween = VerifyUtils.getValueInRangeBetween(1, noteShiftMarginMinimum, lowerDifference, higherDifference);
                                    hitObjectBarlineDifferences.Add(x, marginBetween);
                                }
                            }
                        });

                        // If we have found any dangerous objects, then we need to print them out since they are
                        // not snapped to the barline.
                        if (hitObjectBarlineDifferences.Count > 0)
                        {
                            if (!isSectionCreated)
                            {
                                htmlDisplayer.addSection("Unsnapped objects on barlines are detected.");
                                isSectionCreated = true;
                            }

                            bool added = false;
                            hitObjectBarlineDifferences.ForEach((key, value) =>
                            {
                                if (!added)
                                {
                                    htmlDisplayer.addSubsection("Beatmap difficulty: " + key.GetBeatmap().DifficultyName);
                                    added = true;
                                }
                                string text = key.GetOffsetWithLink() + " - " + value + " ms.";
                                if (value >= noteShiftMarginMinimum)
                                    htmlDisplayer.addWarning(text);
                                else
                                    htmlDisplayer.addLine(text);
                            });
                            htmlDisplayer.addLineBreak();
                        }
                    }

                    // If there is content in the displayer, show it or show the success message.
                    handleHtmlDisplayer(htmlDisplayer, "No unsnapped objects on barlines are found in this mapset.");
                });
            }
        }

        private void resnapAllNotesButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                bool shouldCreateBackup = false;
                string customPath = "";
                if (MessageBoxUtils.showQuestionYesNo("This action will edit your entire taiko beatmapsets, so you might want to get a backup for this since this function is still under development.".AddLines(2) + "Do you want to save backups?") == System.Windows.Forms.DialogResult.Yes)
                {
                    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                    dialog.Title = "Please select backup folder.";
                    dialog.IsFolderPicker = true;
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        shouldCreateBackup = true;
                        customPath = dialog.FileName;
                    }
                    dialog.Dispose();
                }

                DialogResult inheritedResult = MessageBoxUtils.showQuestionYesNoCancel("Do you want to snap inherited points as well?");
                if (inheritedResult == DialogResult.Cancel)
                    return;

                bool shouldSnapInheritedPoints = inheritedResult == DialogResult.Yes;
                ThreadUtils.executeOnBackground(() =>
                {
                    // Get all maps.
                    SearchUtils.GetAllBeatmaps(beatmap, out List<Beatmap> beatmaps);

                    // Extract taiko diffs in this beatmapset.
                    beatmaps = beatmaps.Where(x => x.isModeTaiko()).ToList();

                    // If taiko diffs size is 0 (which is unlikely)
                    // print an error and return.
                    if (beatmaps.Count == 0)
                    {
                        showFailureMessage("No taiko difficulties are found. If this is an error, please contact the developer.");
                        return;
                    }

                    // Create the HTML displayer in case we find stuff
                    // that should be shown.
                    HtmlDisplayer displayer = new HtmlDisplayer();

                    // Check if we had a section for the beatmap
                    // that the error is. Do it by using a dictionary.
                    Dictionary<Beatmap, bool> sectionCreated = new Dictionary<Beatmap, bool>();

                    // Check if the initial section is created.
                    bool initialSectionCreated = false;

                    // Loop through every beatmap to see if there are
                    // unsnapped notes based on our calculations,
                    // fix the possible ones and add undetermined
                    // ones in the displayer.
                    //
                    // Errors are triggered with the delagate in 2nd parameter.
                    SnapUtils.resnapAllNotes(beatmaps, customPath, shouldCreateBackup, shouldSnapInheritedPoints, (beatmap, beatmapElement, value) =>
                    {
                        if (!initialSectionCreated)
                        {
                            displayer.addSection("Undetermined possible snaps detected.");
                            initialSectionCreated = true;
                        }

                        if (!sectionCreated[beatmap])
                        {
                            if (displayer.containsSubsections())
                                displayer.addLineBreak();
                            displayer.addSection("Difficulty: " + beatmap.DifficultyName);
                            sectionCreated[beatmap] = true;
                        }

                        displayer.addLine("Close at 1/" + value + ": " + beatmapElement.GetOffsetWithLink());
                    });

                    // If we have found elements, then warn the user about it with a message. Then, open the panel.
                    // At this point, the user should reload the beatmap in editor.
                    if (!handleHtmlDisplayer(displayer, null, () => MessageBoxUtils.showWarning("Some undetermined snaps are found in the beatmapset. A window that lets you navigatate to them will open."
                            .AddLines(2, "The changed snaps are saved but you might need to fix these manually.".AddLines(2, "Remember to reload the beatmap in the editor first.")))))
                    {
                        this.Invoke(() =>
                        {
                            MessageBoxUtils.show("All notes are resnapped in the beatmapset.");
                            ThreadUtils.executeOnBackground(() =>
                            {
                                beatmap.reload(mainDisplayView);
                                reloadBeatmapIfNecessary();
                            });
                        });
                    }
                });
            }
        }

        private void bpmChangerButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                List<TimingPoint> points = beatmap.getSelectedRangePoints(mainDisplayView);
                List<TimingPoint> targets = null;
                if (points.Count == 0 || (targets = points.FindAll(x => !x.IsInherited)) == null)
                {
                    showFailureMessage("At least one timing point is required.");
                    return;
                }

                TimingPoint target = null;
                if (targets.Count > 1)
                {
                    if (MessageBoxUtils.showQuestionYesNo("More than one timing point is selected for the process, but only singular BPM change is supported.\r\nDo you want to continue with the first selected timing point?") != DialogResult.Yes)
                        return;
                }

                target = targets[0];
                bool allTaikoDiffs = false;
                bool shiftRestOfBeatmap = false;
                bool proceed = false;
                bool shouldCreateBackup = false;
                double newBpmValue = 0;
                string customPath = "";
                using (ChangerForm form = new ChangerForm(ValueChangerRules.ValueChanger.BPM))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        allTaikoDiffs = form.valueAllTaikoDiffs;
                        shiftRestOfBeatmap = form.valueExtraOption;
                        newBpmValue = 60000d / form.valueDouble;
                        proceed = true;
                    }
                }

                if (!proceed)
                    return;

                if (allTaikoDiffs)
                {
                    if (MessageBoxUtils.showQuestionYesNo("This action will edit your entire taiko beatmapsets, so you might want to get a backup for this since this function is still under development.".AddLines(2) + "Do you want to save backups?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                        dialog.Title = "Please select backup folder.";
                        dialog.IsFolderPicker = true;
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                        {
                            shouldCreateBackup = true;
                            customPath = dialog.FileName;
                        }
                        dialog.Dispose();
                    }

                    ThreadUtils.executeOnBackground(() =>
                    {
                        // Create the html displayer.
                        HtmlDisplayer htmlDisplayer = new HtmlDisplayer();

                        // Get all beatmaps.
                        SearchUtils.GetAllBeatmaps(beatmap, out List<Beatmap> beatmaps);

                        // Filter taiko beatmaps.
                        beatmaps = beatmaps.FindAll(x => x.isModeTaiko());

                        // Change all snaps for the section per beatmap.
                        foreach (Beatmap beatmap in beatmaps)
                        {
                            TimingPointUtils.changeBpmOfTimingPoint(beatmap, htmlDisplayer, target.Offset, newBpmValue,
                                shiftRestOfBeatmap, shouldCreateBackup, customPath);
                            beatmap.overwrite();
                        }

                        // If something went wrong for any map in the mapset, show it here.
                        this.Invoke(() =>
                        {
                            if (htmlDisplayer.containsElements())
                            {
                                using (InconsistencyResultForm form = new InconsistencyResultForm(htmlDisplayer.ToString()))
                                {
                                    form.ShowDialog();
                                }

                                MessageBoxUtils.show("Inconsistentices are shown, but the changes are still applied.");
                                reloadBeatmapIfNecessary();
                            }
                            else
                            {
                                MessageBoxUtils.show("The BPM value changed for the selected timing point for all taiko beatmaps.");
                                reloadBeatmapIfNecessary();
                            }
                        });
                    });
                }
                else
                {
                    ThreadUtils.executeOnBackground(() =>
                    {
                        // Create the html displayer.
                        HtmlDisplayer htmlDisplayer = new HtmlDisplayer();

                        TimingPointUtils.changeBpmOfTimingPoint(beatmap, htmlDisplayer, target.Offset, newBpmValue,
                                shiftRestOfBeatmap, shouldCreateBackup, customPath);

                        this.Invoke(() =>
                        {
                            if (htmlDisplayer.containsElements())
                            {
                                using (InconsistencyResultForm form = new InconsistencyResultForm(htmlDisplayer.ToString()))
                                {
                                    form.ShowDialog();
                                }

                                showMessageAndSaveBeatmap("Inconsistentices are shown, but the BPM changes are still applied. The backup is saved in the program until it\'s closed.", "BPM change performed.", "BPMChange");
                            }
                            else
                            {
                                showMessageAndSaveBeatmap("The BPM value changed for the selected timing point.", "BPM change performed.", "BPMChange");
                            }
                        });
                    });
                }
            }
        }

        private void shiftOffsetsOfGreenPointsSvButton_Click(object sender, EventArgs e)
        {
            if (checkBeatmapLoaded())
            {
                ChangerForm changerForm = new ChangerForm(ValueChangerRules.ValueChanger.SV_OFFSET_CHANGE);
                DialogResult result = changerForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    bool isAllTaikoDiffs = changerForm.valueAllTaikoDiffs;
                    bool movePointsInBetween = changerForm.valueExtraOption;
                    int change = changerForm.valueInt;
                    string backupLocation = "";

                    List<Beatmap> allMaps = new List<Beatmap>();

                    if (isAllTaikoDiffs)
                        SearchUtils.GetAllBeatmaps(beatmap, allMaps);
                    else
                        allMaps.Add(beatmap);

                    if (MessageBoxUtils.showQuestionYesNo("This action will edit your entire taiko beatmapsets, so you might want to get a backup for this since this function is still under development.".AddLines(2) + "Do you want to save backups?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                        dialog.Title = "Please select backup folder.";
                        dialog.IsFolderPicker = true;
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                        {
                            backupLocation = dialog.FileName;
                        }
                        dialog.Dispose();
                    }
                    if (!string.IsNullOrEmpty(backupLocation))
                    {
                        foreach (Beatmap diff in allMaps)
                        {
                            diff.saveBeatmapToPath(backupLocation + "//" + diff.FileName);
                        }
                    }

                    foreach (Beatmap diff in allMaps)
                    {
                        List<TimingPoint> allowedPoints = new List<TimingPoint>();
                        List<TimingPoint> points = diff.TimingPoints;
                        List<HitObject> hitObjects = diff.HitObjects;
                        foreach (TimingPoint point in points)
                        {
                            if (SearchUtils.CanMoveSvSafelyFromClosestNote(points, hitObjects, point, change, movePointsInBetween))
                            {
                                allowedPoints.Add(point);
                            }
                        }

                        if (allowedPoints.Count > 0)
                        {
                            foreach (TimingPoint point in allowedPoints)
                            {
                                // Perform an "add" operation since the "change" is a
                                // negative value.
                                point.Offset += change;
                            }
                            diff.overwrite();
                        }
                    }

                    MessageBoxUtils.show("The SV offsets are shifted with the given value.");
                    reloadBeatmapIfNecessary();
                }
            }
        }
    }
}
