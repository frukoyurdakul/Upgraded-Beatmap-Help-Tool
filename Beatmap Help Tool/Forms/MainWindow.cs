using Beatmap_Help_Tool.BeatmapModel;
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
                    saveBeatmap();
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

        private void redoButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

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
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.InitialDirectory = targetPath;
            dialog.Title = "Select .osu file";
            dialog.Filter = ".osu files|*.osu";
            if (dialog.ShowDialog() == DialogResult.OK)
                ThreadUtils.executeOnBackground(new Action(() => 
                    loadBeatmap(dialog.FileName, dialog.SafeFileName)));
        }

        private void loadBeatmap(string targetPath, string beatmapFileName)
        {
            // At this point, file path has been found, load the beatmap and
            // set the data to datagridview.
            // First, invoke the running process label.
            BeginInvoke(new Action(() =>
            {
                runningProcessLabel.Text = beatmapFileName + " has been found, loading...";
            }));
            beatmap = new Beatmap(targetPath);
            BeginInvoke(new Action(() =>
            {
                beatmap.fillDataGridView(mainDisplayView);
                enableTabs();
                runningProcessLabel.Text = beatmapFileName + " has been loaded.";
                Text = "Beatmap Help Tool - " + beatmapFileName;
                filePathTextBox.Text = Directory.GetParent(targetPath).FullName;
            }));
        }

        private void saveBeatmap()
        {
            ThreadUtils.executeOnBackground(new Action(() =>
            {
                BeginInvoke(new Action(() =>
                {
                    runningProcessLabel.Text = "Saving beatmap...";
                }));
                beatmap.save();
                BeginInvoke(new Action(() =>
                {
                    runningProcessLabel.Text = "Beatmap has been saved.";
                    lastSaveTimeLabel.Text = DateTime.Now.ToLongTimeString();
                }));
            }));
        }

        private void saveBeatmap(string path)
        {
            ThreadUtils.executeOnBackground(new Action(() =>
            {
                BeginInvoke(new Action(() =>
                {
                    runningProcessLabel.Text = "Saving beatmap to path: " + path + "\\" + beatmap.FileName;
                }));
                beatmap.save(path);
                BeginInvoke(new Action(() =>
                {
                    runningProcessLabel.Text = "Beatmap has been saved.";
                    lastSaveTimeLabel.Text = DateTime.Now.ToLongTimeString();
                }));
            }));
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
                    BeginInvoke(new Action(() =>
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
                BeginInvoke(new Action(() =>
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
                mapName = returnMapName(osuWindowTitle, IndexUtils.getIndexOfWithCount(osuWindowTitle, " ", 3));
            }
            else if (osuWindowTitle.StartsWith("osu!"))
            {
                mapName = returnMapName(osuWindowTitle, IndexUtils.getIndexOfWithCount(osuWindowTitle, " ", 2));
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
            (svChangesPage as Control).Enabled = true;
            (editorFunctionsPage as Control).Enabled = true;
            (bpmFunctionsPage as Control).Enabled = true;
        }

        private void disableTabs()
        {
            (svChangesPage as Control).Enabled = false;
            (editorFunctionsPage as Control).Enabled = false;
            (bpmFunctionsPage as Control).Enabled = false;
        }
        #endregion
    }
}
