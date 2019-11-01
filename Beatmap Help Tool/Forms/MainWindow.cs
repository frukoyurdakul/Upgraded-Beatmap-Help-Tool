using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
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

        private void mainForm_Load(object sender, EventArgs e)
        {
            ThreadUtils.executeOnBackground(new Action(() => determineInitialProcess()));
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThreadUtils.exitLooperThread();
        }

        private void determineInitialProcess()
        {
            Process[] processes = Process.GetProcessesByName("osu!");
            if (processes.Length > 0)
            {
                // The program was opened while osu was running.
                Process osuProcess = processes[0];
                string fileName = osuProcess.MainModule.FileName;
                string windowTitle = osuProcess.MainWindowTitle;
                DirectoryInfo directory = Directory.GetParent(fileName);
                if (directory != null)
                {
                    // Osu directory has been successfully found. Prompt the user to
                    // try to see if it can find the difficulty that they are mapping.
                    // This has to be done on UI thread.
                    BeginInvoke(new Action(() =>
                    {
                        if (MessageBoxUtils.showQuestionYesNo("osu! seems to be running, would you like to search for current beatmap?") ==
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
                    // opening process. Continue with last saved path if exists.
                }
            }
            else
            {
                // Osu is not running, continue with last saved path if exists.
            }
        }

        private void searchCurrentOpenBeatmap(string beatmapFileName, string songsPath)
        {
            string[] osuFilesInFolder = Directory.GetFiles(songsPath, "*.osu", SearchOption.AllDirectories);
            string lastPart;
            string targetPath = "";
            int lastSlashIndex;
            foreach (string path in osuFilesInFolder)
            {
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
            {
                // At this point, file path has been found, load the beatmap and
                // set the data to datagridview.
                // First, invoke the running process label.
                BeginInvoke(new Action(() =>
                {
                    runningProcessLabel.Text = beatmapFileName + " has been found, loading...";
                }));
            }
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
    }
}
