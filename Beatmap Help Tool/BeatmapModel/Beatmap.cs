using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public class Beatmap
    {
        // Constant integers that helps defining the beatmap mode.
        private const int MODE_STANDARD = 0;
        private const int MODE_TAIKO = 1;
        private const int MODE_MANIA = 2;
        private const int MODE_CTB = 3;

        // Constant integers that what the datagridview is showing
        // currently. If any change function is called while
        // the inner value is the same, the data will not
        // be updated unnecessarily.
        private const int DISPLAY_MODE_ALL = 0;
        private const int DISPLAY_MODE_TIMING_ONLY = 1;
        private const int DISPLAY_MODE_INHERITED_ONLY = 2;

        // File path. Required to write over the file again.
        public string FilePath { get; }

        // Datagridview display mode.
        private int displayMode = DISPLAY_MODE_ALL;

        // Osu file format. Currently at v14 or v15.
        public string FileFormat { get; }

        // Information about beatmap in general.
        public string AudioFilename { get; set; }
        private int AudioLeadIn = 0;
        private int PreviewTime = 0;
        private int Countdown = 0;
        private string SampleSet = "";
        private double StackLeniency = 0.7;
        public int BeatmapMode { get; set; }
        private int LetterboxInBreaks = 0;
        private int WidescreenStoryboard = 0;

        // Bookmarks.
        public List<int> Bookmarks { get; } = new List<int>();
        private double DistanceSpacing = 1;
        private int BeatDivisor = 4;
        private int GridSize = 32;
        private double TimelineZoom = 3.79;

        // Metadata.
        public string Title { get; set; }
        public string TitleUnicode { get; set; }
        public string Artist { get; set; }
        public string ArtistUnicode { get; set; }
        public string Creator { get; set; }
        public string Version { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
        public int BeatmapID { get; set; }
        public int BeatmapSetID { get; set; }

        // Difficulty
        private int HPDrainRate = 0;
        private int CircleSize = 0;
        private int OverallDifficulty = 0;
        private int ApproachRate = 10;
        public double SliderMultiplier { get; internal set; }
        private int SliderTickRate = 1;

        // Events (basically copied from the beatmap itself, will
        // be printed as a whole string while saving)
        private readonly string Events = "";

        // Timing points
        public List<TimingPoint> TimingPoints = new List<TimingPoint>();

        // Colors
        private readonly string Colors = "";

        // Hit objects
        public List<HitObject> HitObjects = new List<HitObject>();

        // The main constructor. Reads the file and parses every line.
        public Beatmap(string beatmapPath)
        {
            // Set the beatmap path.
            FilePath = beatmapPath;

            // Read the file content (always extract the empty lines)
            List<string> lines = File.ReadAllLines(beatmapPath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    lines.RemoveAt(i--);
            }

            // Parse the file content line by line.
            int index = 0;
            if (lines[index].StartsWith("osu file format"))
                FileFormat = lines[index];
            index = 1;

            // From now on, map every value in itself.
            int dotIndex;
            string line;
            string[] splitted;
            for (; index < lines.Count; index++)
            {
                line = lines[index];
                if (IsSection(line))
                {
                    // Reached a section. If it is "[Events]", break from this loop.
                    // The rest should be handled differently.
                    if (line.Trim() == "[Events]")
                    {
                        // The next sequence needs to be addressed into 
                        // the events string.
                        index++;
                        for (; index < lines.Count; index++)
                        {
                            if (IsSection(lines[index]))
                                break;
                            Events += lines[index] + Environment.NewLine;
                        }
                    }
                    // If we reach timing points, then break.
                    if (lines[index].Trim() == "[TimingPoints]")
                    {
                        index++;
                        break;
                    }
                    // Otherwise, go on.
                    else
                        continue;
                }
                else
                {
                    dotIndex = line.IndexOf(":");
                    if (dotIndex >= 0)
                    {
                        splitted = line.Split(':');
                        if (splitted.Length == 2)
                            AssignValueByKey(splitted[0], splitted[1].TrimStart());
                    }
                }
            }

            // Timing points case.
            TimingPoint point;
            for (; !IsSection(lines[index]) && index < lines.Count; index++)
            {
                point = TimingPoint.ParseLine(TimingPoints, lines[index]);
                TimingPoints.Add(point);
                point.GetSnap();
                if (TimingPoints[TimingPoints.Count - 1] == null)
                {
                    MessageBoxUtils.showError("Process aborted.");
                    return;
                }
            }

            // Check if beatmap has the property "colors". If it does,
            // just add it and skip to the hit objects.
            if (lines[index] == "[Colours]")
            {
                // Welp, beatmap has colors. Add them to the colors.
                index++;
                for (; !IsSection(lines[index]) && index < lines.Count; index++)
                    Colors += lines[index];
            }

            // It will break after finding a section, so raise index again.
            index++;

            // Hit objects case.
            for (; index < lines.Count; index++)
            {
                HitObjects.Add(HitObject.ParseLine(this, lines[index]));
                if (HitObjects[HitObjects.Count - 1] == null)
                {
                    MessageBoxUtils.showError("Process aborted.");
                    return;
                }
            }
        }

        private void AssignValueByKey(string key, string value)
        {
            switch (key)
            {
                case "AudioFilename":
                    AudioFilename = value;
                    break;
                case "AudioLeadIn":
                    AudioLeadIn = Convert.ToInt32(value.Trim());
                    break;
                case "PreviewTime":
                    PreviewTime = Convert.ToInt32(value.Trim());
                    break;
                case "Countdown":
                    Countdown = Convert.ToInt32(value.Trim());
                    break;
                case "Sampleset":
                    SampleSet = value;
                    break;
                case "StackLeniency":
                    StackLeniency = Convert.ToDouble(value.Trim());
                    break;
                case "Mode":
                    BeatmapMode = Convert.ToInt32(value.Trim());
                    break;
                case "LetterboxInBreaks":
                    LetterboxInBreaks = Convert.ToInt32(value.Trim());
                    break;
                case "WidescreenStoryboard":
                    WidescreenStoryboard = Convert.ToInt32(value.Trim());
                    break;
                case "Bookmarks":
                    {
                        string[] allBookmarks = value.Trim().Split(',');
                        foreach (string bookmark in allBookmarks)
                            Bookmarks.Add(Convert.ToInt32(bookmark));
                        break;
                    }
                case "DistanceSpacing":
                    DistanceSpacing = Convert.ToDouble(value.Trim());
                    break;
                case "BeatDivisor":
                    BeatDivisor = Convert.ToInt32(value.Trim());
                    break;
                case "GridSize":
                    GridSize = Convert.ToInt32(value.Trim());
                    break;
                case "TimelineZoom":
                    TimelineZoom = Convert.ToDouble(value.Trim());
                    break;
                case "Title":
                    Title = value;
                    break;
                case "TitleUnicode":
                    TitleUnicode = value;
                    break;
                case "Artist":
                    Artist = value;
                    break;
                case "ArtistUnicode":
                    ArtistUnicode = value;
                    break;
                case "Creator":
                    Creator = value;
                    break;
                case "Version":
                    Version = value;
                    break;
                case "Source":
                    Source = value;
                    break;
                case "Tags":
                    Tags = value;
                    break;
                case "BeatmapID":
                    BeatmapID = Convert.ToInt32(value.Trim());
                    break;
                case "BeatmapSetID":
                    BeatmapSetID = Convert.ToInt32(value.Trim());
                    break;
                case "SliderMultiplier":
                    SliderMultiplier = Convert.ToDouble(value.Trim());
                    break;
            }
        }

        public void showInheritedPointsOnly(DataGridView dataGridView)
        {
            if (displayMode != DISPLAY_MODE_INHERITED_ONLY)
            {
                insertPoints(dataGridView, TimingPoints.Where(x => x.IsInherited).ToList());
                displayMode = DISPLAY_MODE_INHERITED_ONLY;
            }
        }

        public void showTimingPointsOnly(DataGridView dataGridView)
        {
            if (displayMode != DISPLAY_MODE_TIMING_ONLY)
            {
                insertPoints(dataGridView, TimingPoints.Where(x => !x.IsInherited).ToList());
                displayMode = DISPLAY_MODE_TIMING_ONLY;
            }
        }

        public void showAllPoints(DataGridView dataGridView)
        {
            if (displayMode != DISPLAY_MODE_ALL)
            {
                insertPoints(dataGridView, TimingPoints);
                displayMode = DISPLAY_MODE_ALL;
            }
        }

        // Fills the data grid view with related data. Has to be called
        // from GUI thread otherwise it will throw an exception.
        public void fillDataGridView(DataGridView dataGridView)
        {
            insertPoints(dataGridView, TimingPoints);
        }

        private void insertPoints(DataGridView dataGridView, List<TimingPoint> points)
        {
            if (dataGridView.Rows.Count > 0)
                dataGridView.Rows.Clear();
            foreach (TimingPoint point in points)
            {
                dataGridView.Rows.Add(point.getDisplayOffset(), point.getDisplayValue(),
                    point.getDisplayMeter(), point.getDisplayVolume(), point.getDisplayKiai());
            }
        }

        private bool IsSection(string text)
        {
            return text.Trim().StartsWith("[") && text.Trim().EndsWith("]");
        }
    }
}
