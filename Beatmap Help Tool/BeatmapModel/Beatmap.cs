using Beatmap_Help_Tool.BeatmapTools;
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
        private const int MAX_SAVED_STATE_COUNT = 5;

        private static readonly List<Beatmap> savedStates = new List<Beatmap>();
        private static readonly List<string> actions = new List<string>();
        private static int savedStateIndex = 0;

        public static Beatmap getNextSavedState()
        {
            if (savedStates.Count == 0)
                return null;
            if (savedStateIndex < savedStates.Count - 1)
                return savedStates[++savedStateIndex];
            else
                return savedStates[savedStateIndex];
        }

        public static Beatmap getPreviousSavedState()
        {
            if (savedStates.Count == 0)
                return null;
            if (savedStateIndex > 0)
                return savedStates[--savedStateIndex];
            else
                return savedStates[savedStateIndex];
        }

        public static string getSavedStateAction()
        {
            if (savedStateIndex >= actions.Count)
                return "";
            return actions[savedStateIndex];
        }

        public static bool hasNextState()
        {
            return savedStates.Count > 0 && 
                savedStateIndex < savedStates.Count - 1;
        }

        public static bool hasPreviousState()
        {
            return savedStates.Count > 0 &&
                savedStateIndex > 0;
        }

        private static void addSavedState(string action, Beatmap beatmap)
        {
            if (savedStates.Count > MAX_SAVED_STATE_COUNT)
            {
                int difference = MAX_SAVED_STATE_COUNT - savedStates.Count;
                for (int i = 0; i < difference; i++)
                {
                    savedStates.RemoveAt(0);
                    actions.RemoveAt(0);
                }
            }
            else if (savedStates.Count == MAX_SAVED_STATE_COUNT)
            {
                savedStates.RemoveAt(0);
                actions.RemoveAt(0);
            }

            savedStates.Add(beatmap);
            actions.Add(getNonDuplicateString(actions, action));
            savedStateIndex = savedStates.Count - 1;
        }

        private static void clearSavedStates()
        {
            savedStates.Clear();
            savedStateIndex = 0;
        }

        private static string getNonDuplicateString(List<string> actions, string action)
        {
            if (!actions.Contains(action))
                return action;
            else
            {
                int startIndex = 1;
                string format;
                while (actions.Contains(format = string.Format("{0} ({1})", action, startIndex++))) ;
                return format;
            }
        }

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
        public string FolderPath { get; internal set; }
        public string FilePath { get; internal set; }
        public string FileName { get; internal set; }

        // Datagridview display mode. Default is all points.
        private int displayMode = DISPLAY_MODE_ALL;

        // Osu file format. Currently at v14 or v15.
        public string FileFormat { get; internal set; }

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
        public List<Bookmark> Bookmarks { get; internal set; } = new List<Bookmark>();
        private string bookmarksString = "";
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
        public string DifficultyName { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
        public int BeatmapID { get; set; }
        public int BeatmapSetID { get; set; }

        // Difficulty
        private double HPDrainRate = 0;
        private double CircleSize = 0;
        private double OverallDifficulty = 0;
        private double ApproachRate = 10;
        public double SliderMultiplier { get; internal set; }
        private double SliderTickRate = 1;

        // Events (basically copied from the beatmap itself, will
        // be printed as a whole string while saving)
        private string Events = "";

        // Timing points
        public List<TimingPoint> TimingPoints = new List<TimingPoint>();

        // Colors
        private string Colors = "";

        // Hit objects
        public List<HitObject> HitObjects = new List<HitObject>();

        public Beatmap(string beatmapPath) : this(beatmapPath, true)
        {
            
        }

        // The main constructor. Reads the file and parses every line.
        public Beatmap(string beatmapPath, bool addFirstState)
        {
            load(beatmapPath, addFirstState);
        }

        private Beatmap()
        {

        }

        public Beatmap(Beatmap source)
        {
            TimingPoints = new List<TimingPoint>();
            Bookmarks = new List<Bookmark>();
            HitObjects = new List<HitObject>();
            ApproachRate = source.ApproachRate;
            Artist = source.Artist;
            ArtistUnicode = source.ArtistUnicode;
            AudioFilename = source.AudioFilename;
            AudioLeadIn = source.AudioLeadIn;
            BeatDivisor = source.BeatDivisor;
            BeatmapID = source.BeatmapID;
            BeatmapMode = source.BeatmapMode;
            BeatmapSetID = source.BeatmapSetID;
            bookmarksString = source.bookmarksString;
            CircleSize = source.CircleSize;
            Colors = source.Colors;
            Countdown = source.Countdown;
            Creator = source.Creator;
            displayMode = source.displayMode;
            DistanceSpacing = source.DistanceSpacing;
            Events = source.Events;
            FileFormat = source.FileFormat;
            FileName = source.FileName;
            FilePath = source.FilePath;
            FolderPath = source.FolderPath;
            GridSize = source.GridSize;
            HPDrainRate = source.HPDrainRate;
            LetterboxInBreaks = source.LetterboxInBreaks;
            OverallDifficulty = source.OverallDifficulty;
            PreviewTime = source.PreviewTime;
            SampleSet = source.SampleSet;
            SliderMultiplier = source.SliderMultiplier;
            SliderTickRate = source.SliderTickRate;
            Source = source.Source;
            StackLeniency = source.StackLeniency;
            Tags = source.Tags;
            TimelineZoom = source.TimelineZoom;
            Title = source.Title;
            TitleUnicode = source.TitleUnicode;
            DifficultyName = source.DifficultyName;
            WidescreenStoryboard = source.WidescreenStoryboard;

            List<TimingPoint> sourceTimingPoints = source.TimingPoints;
            int sourceTimingPointsCount = sourceTimingPoints.Count;
            for (int i = 0; i < sourceTimingPointsCount; i++)
                TimingPoints.Add(new TimingPoint(sourceTimingPoints[i], TimingPoints));
            List<HitObject> sourceHitObjects = source.HitObjects;
            int sourceHitObjectsCount = sourceHitObjects.Count;
            for (int i = 0; i < sourceHitObjectsCount; i++)
                HitObjects.Add(HitObject.deepCopy(sourceHitObjects[i]));
            List<Bookmark> sourceBookmarks = source.Bookmarks;
            int sourceBookmarksCount = sourceBookmarks.Count;
            for (int i = 0; i < sourceBookmarksCount; i++)
                Bookmarks.Add(new Bookmark(sourceBookmarks[i]));
        }

        private void load(string beatmapPath, bool addFirstState)
        {
            // Set the beatmap path.
            FilePath = beatmapPath;
            FileName = Path.GetFileName(beatmapPath);
            FolderPath = Path.GetDirectoryName(beatmapPath);

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
                if (TimingPoints[TimingPoints.Count - 1] == null)
                {
                    MessageBoxUtils.showError("Process aborted.");
                    return;
                }
            }

            // If there are any bookmarks, after timing points, add them.
            if (!string.IsNullOrWhiteSpace(bookmarksString))
            {
                string[] bookmarksSplitted = bookmarksString.Trim().Split(',');
                foreach (string offset in bookmarksSplitted)
                    Bookmarks.Add(new Bookmark(TimingPoints, Convert.ToInt32(offset)));
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
            if (addFirstState)
                addSavedState("First load", this);
        }
        
        public void reload(DataGridView mainDisplayView)
        {
            Bookmarks = new List<Bookmark>();
            TimingPoints = new List<TimingPoint>();
            HitObjects = new List<HitObject>();
            Events = "";
            load(FilePath, true);
            mainDisplayView.Invoke(() =>
            {
                fillMainDisplayView(mainDisplayView);
            });
        }
        public bool isModeTaiko()
        {
            return BeatmapMode == MODE_TAIKO;
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
                case "SampleSet":
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
                        bookmarksString = value.Trim();
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
                    DifficultyName = value;
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
                case "HPDrainRate":
                    HPDrainRate = Convert.ToDouble(value.Trim());
                    break;
                case "CircleSize":
                    CircleSize = Convert.ToDouble(value.Trim());
                    break;
                case "OverallDifficulty":
                    OverallDifficulty = Convert.ToDouble(value.Trim());
                    break;
                case "ApproachRate":
                    ApproachRate = Convert.ToDouble(value.Trim());
                    break;
                case "SliderMultiplier":
                    SliderMultiplier = Convert.ToDouble(value.Trim());
                    break;
                case "SliderTickRate":
                    SliderTickRate = Convert.ToDouble(value.Trim());
                    break;
            }
        }

        // Required function to generate file names.
        // Necessary since metadata can also be changed from this app.
        private string generateFileName()
        {
            return ArtistUnicode + " - " + TitleUnicode + " (" + Creator + ") [" + DifficultyName + "].osu";
        }

        // Shows inherited points only.
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
        public void fillMainDisplayView(DataGridView mainDisplayView)
        {
            insertPoints(mainDisplayView, TimingPoints);
        }

        /// <summary>
        /// Saves the beatmap to the current file with overwriting it.
        /// This does not create a copy so use sparingly.
        /// </summary>
        public void overwrite()
        {
            saveBeatmapToPath(FilePath);
        }

        // Save the beatmap. Uses the beatmap's original file path.
        // To use another path, use save(string path) instead.
        public void save(string action)
        {
            save(action, FilePath);
        }

        /// <summary>
        /// Save the beatmap with the current content.
        /// Save is handled in background so let the worker thread do
        /// the saving and let the foreground thread do the label processing etc...
        /// It is better to call this on the background thread.
        /// </summary>
        /// <param name="action">the action string</param>
        /// <param name="path">the save path</param>
        public void save(string action, string path)
        {
            // If we are saving, we must create a copy first.
            addSavedState(action, new Beatmap(this));
            saveBeatmapToPath(path);
        }

        /// <summary>
        /// Saves the beatmap to the specified path. 
        /// </summary>
        /// <param name="path"></param>
        public void saveBeatmapToPath(string path)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                writer.WriteLine(FileFormat);
                writer.WriteLine();
                writer.WriteLine("[General]");
                writer.WriteLine("AudioFilename: " + AudioFilename);
                writer.WriteLine("AudioLeadIn: " + AudioLeadIn);
                writer.WriteLine("PreviewTime: " + PreviewTime);
                writer.WriteLine("Countdown: " + Countdown);
                writer.WriteLine("SampleSet: " + SampleSet);
                writer.WriteLine("StackLeniency: " + StackLeniency);
                writer.WriteLine("Mode: " + BeatmapMode);
                writer.WriteLine("LetterboxInBreaks: " + LetterboxInBreaks);
                writer.WriteLine("WidescreenStoryboard: " + WidescreenStoryboard);
                writer.WriteLine();
                writer.WriteLine("[Editor]");
                if (Bookmarks.Count > 0)
                    writer.WriteLine("Bookmarks: " + string.Join(",", Bookmarks));
                writer.WriteLine("DistanceSpacing: " + DistanceSpacing);
                writer.WriteLine("BeatDivisor: " + BeatDivisor);
                writer.WriteLine("GridSize: " + GridSize);
                writer.WriteLine("TimelineZoom: " + TimelineZoom);
                writer.WriteLine();
                writer.WriteLine("[Metadata]");
                writer.WriteLine("Title:" + Title);
                writer.WriteLine("TitleUnicode:" + TitleUnicode);
                writer.WriteLine("Artist:" + Artist);
                writer.WriteLine("ArtistUnicode:" + ArtistUnicode);
                writer.WriteLine("Creator:" + Creator);
                writer.WriteLine("Version:" + DifficultyName);
                writer.WriteLine("Source:" + Source);
                writer.WriteLine("Tags:" + Tags);
                writer.WriteLine("BeatmapID:" + BeatmapID);
                writer.WriteLine("BeatmapSetID:" + BeatmapSetID);
                writer.WriteLine();
                writer.WriteLine("[Difficulty]");
                writer.WriteLine("HPDrainRate:" + HPDrainRate);
                writer.WriteLine("CircleSize:" + CircleSize);
                writer.WriteLine("OverallDifficulty:" + OverallDifficulty);
                writer.WriteLine("ApproachRate:" + ApproachRate);
                writer.WriteLine("SliderMultiplier:" + SliderMultiplier);
                writer.WriteLine("SliderTickRate:" + SliderTickRate);
                writer.WriteLine();
                writer.WriteLine("[Events]");
                writer.WriteLine(Events);
                writer.WriteLine();
                writer.WriteLine("[TimingPoints]");

                // This has to be guaranteed.
                TimingPoints.Sort();
                int count = TimingPoints.Count;
                for (int i = 0; i < count; i++)
                    writer.WriteLine(TimingPoints[i]);
                writer.WriteLine();
                if (!string.IsNullOrWhiteSpace(Colors))
                {
                    writer.WriteLine("[Colors]");
                    writer.WriteLine(Colors);
                }
                writer.WriteLine();
                writer.WriteLine("[HitObjects]");

                // This also has to be guaranteed.
                HitObjects.Sort();
                count = HitObjects.Count;
                for (int i = 0; i < count; i++)
                    writer.WriteLine(HitObjects[i]);
            }
        }

        private void insertPoints(DataGridView dataGridView, List<TimingPoint> points)
        {
            displayMode = DISPLAY_MODE_ALL;
            if (dataGridView.Rows.Count > 0)
                dataGridView.Rows.Clear();
            TimingPoint point;
            for (int i = 0; i < points.Count; i++)
            {
                point = points[i];
                dataGridView.Rows.Add(point.getDisplayOffset(), point.getDisplayValueString(),
                    point.getDisplayMeter(), point.getDisplayVolume(), point.getDisplayKiai());
            }
        }

        private bool IsSection(string text)
        {
            return text.Trim().StartsWith("[") && text.Trim().EndsWith("]");
        }
    }
}
