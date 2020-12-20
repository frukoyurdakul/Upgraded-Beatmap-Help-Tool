using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class SnapUtils
    {
        private const double BEAT_SNAP_DIVISOR = 5040;

        private const double _5 = BEAT_SNAP_DIVISOR / 5d;
        private const double _7 = BEAT_SNAP_DIVISOR / 7d;
        private const double _9 = BEAT_SNAP_DIVISOR / 9d;
        private const double _12 = BEAT_SNAP_DIVISOR / 12d;
        private const double _16 = BEAT_SNAP_DIVISOR / 16d;

        private static SortedSet<double> snaps = new SortedSet<double>();
        private static readonly Dictionary<double, int> snapMapping = new Dictionary<double, double>();
        private static readonly double[] snapsArray;

        static SnapUtils()
        {
            // Generic value.
            snaps.Add(0);
            snaps.Add(5400);
            snapMapping[0] = 0;
            snapMapping[5400] = 5400; 

            // 1/5
            addSnaps(new KeyValuePair<double[], int>(getKeys(_5), 5));

            // 1/7
            addSnaps(new KeyValuePair<double[], int>(getKeys(_7), 7));

            // 1/9
            addSnaps(new KeyValuePair<double[], int>(getKeys(_9), 9));

            // 1/12
            addSnaps(new KeyValuePair<double[], int>(getKeys(_12), 12));

            // 1/16
            addSnaps(new KeyValuePair<double[], int>(getKeys(_16), 16));

            snapsArray = new double[snaps.Count + 2];
            snapsArray[0] = 0;
            snapsArray[snaps.Count - 1] = 5400;
            List<double> snapsList = snaps.ToList();
            for (int i = 1; i < snaps.Count - 1; i++)
                snapsArray[i] = snapsList[i - 1];
            snaps.Clear();
        }

        private static void addSnaps(KeyValuePair<double[], int> pair)
        {
            foreach (double value in pair.Key)
            {
                snaps.Add(value);
                snapMapping[value] = pair.Value;
            }
        }

        private static double[] getKeys(double snapValue)
        {
            int count = (int)(BEAT_SNAP_DIVISOR / snapValue);
            double[] array = new double[count - 1];
            for (int i = 0; i < count; i++)
                array[i] = snapValue * (i + 1);
            return array;
        }

        public static double[] getRelativeSnap(List<TimingPoint> timingPoints, BeatmapElement target)
        {
            double[] result = new double[2] {0, 0};
            List<TimingPoint> redPoints = new List<TimingPoint>();
            List<TimingPoint> excludedRedPoints = new List<TimingPoint>();

            if (timingPoints.Count == 0)
                return result;

            // Extract all inherited points since they don't mean anything
            // while we are searching for the snap value.
            TimingPoint point;
            for (int i = 0; i < timingPoints.Count; i++)
            {
                point = timingPoints[i];
                if (!point.IsInherited)
                {
                    redPoints.Add(point);
                    if (target != point)
                        excludedRedPoints.Add(point);
                }
            }

            // Now, the BEAT_SNAP_DIVISOR divides a beat in 5040 
            // equal snaps, which covers all snaps supported by osu!
            // editor at the moment.
            // Depending on the division, if the division is integer,
            // or if the division difference is below 0.1 or the actual snap
            // we target for is off smaller than 1 milliseconds which can be
            // caused by rounding errors in the end,
            // we can consider that note as snapped.
            if (redPoints.Count >= 1 && redPoints[0].Offset == target.Offset)
                return result;

            TimingPoint closestTimingPoint = SearchUtils.GetClosestTimingPoint(excludedRedPoints, target.Offset);
            int zeroSnapPointIndex = SearchUtils.GetClosestZeroSnapPointIndex(excludedRedPoints);
            if (closestTimingPoint != null)
            {
                TimingPoint timingPoint1, timingPoint2;
                BeatmapElement itemInternal;
                double beatDuration;
                double finalSnap = 0d, completeSnap = 0d;
                double snapInBetween;
                bool checkedActualTarget = false;
                int redPointsCount = redPoints.Count;
                for (int i = zeroSnapPointIndex; i < redPointsCount - 1; i++)
                {
                    timingPoint1 = redPoints[i];
                    timingPoint2 = redPoints[i + 1];
                    beatDuration = timingPoint1.PointValue;
                    if (timingPoint1 == closestTimingPoint)
                    {
                        checkedActualTarget = true;
                        itemInternal = target;
                    }
                    else
                        itemInternal = timingPoint2;

                    snapInBetween = getSnapInBetween(timingPoint1, itemInternal, beatDuration);
                    if (snapInBetween >= 0)
                        finalSnap += snapInBetween;
                    else
                        finalSnap = 0d;
                    completeSnap += snapInBetween;
                    if (checkedActualTarget)
                        break;
                }

                // If we did not check the actual target here, that is because the 
                // object we seek is after the last timing point, which only requires
                // the computation of the final snap adding to the target and last point.
                if (!checkedActualTarget  && redPoints[redPointsCount - 1] == closestTimingPoint)
                {
                    point = redPoints[redPointsCount - 1];
                    snapInBetween = getSnapInBetween(point, target, point.PointValue);
                    if (snapInBetween >= 0)
                        finalSnap += snapInBetween;
                    else
                        finalSnap = 0d;
                    completeSnap += snapInBetween;
                }
                result[0] = finalSnap;
                result[1] = completeSnap;
                return result;
            }
            else
            {
                MessageBoxUtils.showError("Error while determining snap value for the object that is instance of " +
                    target.GetType() + "with the offset at " + target.Offset.ToString());
                result[0] = -1;
                result[1] = -1;
                return result;
            }
        }

        public static void getEndOffsetFromObjectsByCount(Beatmap beatmap, double startOffset,
            int count, out double endOffset)
        {
            SearchUtils.SortBeatmapElements(beatmap.HitObjects);

            int index = 1;
            int foundIndex = -1;
            for (int i = 0; i < beatmap.HitObjects.Count; i++)
            {
                if (beatmap.HitObjects[i].Offset >= startOffset)
                {
                    while (index < count && i++ < beatmap.HitObjects.Count)
                    {
                        index++;
                    }
                    foundIndex = i;
                    break;
                }
            }
            if (foundIndex != -1)
                endOffset = beatmap.HitObjects[foundIndex].Offset;
            else
                endOffset = -1;
        }

        public static int calculateEndOffset(Beatmap beatmap, double startOffset, double gridSnap, double count)
        {
            double step = gridSnap;
            double totalSnap = step * count;
            double calculatedSnap = 0;
            double targetOffset = startOffset;

            double snapOffset = 0;

            while (calculatedSnap < totalSnap)
            {
                // This one has to return non-null. If it does, the exception is deserved.
                TimingPoint closestPoint = SearchUtils.GetClosestTimingPoint(beatmap.TimingPoints, targetOffset);

                // This can return null. It means we don't need to worry about this point and
                // calculate the offset directly.
                TimingPoint nextPoint = SearchUtils.GetClosestNextTimingPoint(beatmap.TimingPoints, closestPoint);

                snapOffset = step * closestPoint.PointValue;
                targetOffset += snapOffset;
                calculatedSnap += step;

                // Now, if the target offset temp passed the next point
                // calculate an estimated snap difference.
                // This is required for unsnapped timing points and a relative
                // end offset calculation.
                if (nextPoint != null && targetOffset > nextPoint.Offset)
                {
                    double difference = nextPoint.Offset - targetOffset;
                    double differenceSnap = difference / nextPoint.PointValue;

                    // Here, we reset the target offset as the next point value
                    // and reduce the calculated total grid snap. Step value
                    // is not changed and the next snaps are calculated
                    // relatively to the next point.
                    targetOffset = nextPoint.Offset;
                    calculatedSnap -= differenceSnap;
                }
            }

            // And, at the end of the day, return the target offset.
            return (int) targetOffset;
        }

        private static int getSnapInBetween(BeatmapElement target1, BeatmapElement target2, double beatDuration)
        {
            double offsetDifference = target2.Offset - target1.Offset;
            double snap = (offsetDifference / beatDuration) * BEAT_SNAP_DIVISOR;
            int snapInt = Convert.ToInt32(snap);
            double fluctuation = Math.Abs(snap - snapInt);
            double targetOffset = target1.Offset + (snapInt / BEAT_SNAP_DIVISOR * beatDuration);
            if (fluctuation < 10.5d || Math.Abs(target2.Offset - targetOffset) < 1)
            {
                // We can consider this note as snapped in the end.
                // TODO Determine the behavior here: let the object unsnapped
                // if it is a hitsound, or show an error message.
                return snapInt;
            }
            else
            {
                // The note is definitely unsnapped. Take this note as the 0 snap and
                // reset the final snap variable.
                // TODO Either fill this area with an error message
                // or throw an exception, or find another idea to process unsnapped notes.
                Console.WriteLine("Detected an unsnapped object with type \"" + target1.GetType() + "\", " +
                    "and offset " + target1.Offset);
                return -1;
            }
        }

        private static void setOffsetOfElementToNewSnap(TimingPoint closestRedPoint, BeatmapElement element)
        {
            decimal snap = (decimal)element.GetSnap();
            decimal pointValue = (decimal)closestRedPoint.PointValue;
            decimal finalResult = (decimal)closestRedPoint.Offset + (pointValue / (decimal)BEAT_SNAP_DIVISOR * snap);
            element.SetOffset((double)finalResult);
        }

        private static double getClosestSnapValue(double rawSnap, out int closestSnap)
        {
            // Gets the closest snap value in the beat. If multiple snaps
            // are found, then we also return a closestSnapIndex with 
            // a value different than -1, in which case the note's snap
            // does not change and instead gets presented into the user.
            double closestValue = -1;
            closestSnap = -1;
            double closest = double.MaxValue;
            for (int i = 0; i < snapsArray.Length; i++)
            {
                double snapValue = snapsArray[i];
                double diff = Math.Abs(rawSnap - snapValue);

                // Diff 0 means this note is exactly snapped
                // and no changes are required.
                if (diff == 0)
                    return 0;

                // Otherwise, continue getting the closest snap value.
                else if (closest < diff)
                {
                    if (closest == diff)
                        closestSnap = snapMapping[snapValue];
                    closest = diff;
                    closestValue = i;
                }
            }
            return closestValue;
        }

        public static bool resnapAllNotes(List<Beatmap> beatmaps, onFailure<Beatmap, BeatmapElement, int> listener)
        {
            // Loop through all beatmaps.
            // Ask for confirmation first. Users might select a location
            // to save backups first.
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
            foreach (Beatmap beatmap in beatmaps)
            {
                if (shouldCreateBackup)
                    beatmap.save(customPath + "//" + beatmap.FileName);
                List<HitObject> hitObjects = beatmap.HitObjects;
                foreach (HitObject hitObject in hitObjects)
                {
                    double actualSnap = hitObject.GetSnap();
                    double rawSnapInBeat = actualSnap % BEAT_SNAP_DIVISOR;
                    double closestSnapInBeat = getClosestSnapValue(rawSnapInBeat, out int closestSnapValue);
                    if (closestSnapValue != -1)
                    {
                        // We have a note that is equal distance to defined snaps
                        // in the editor. Present this to the user.
                        listener.Invoke(beatmap, hitObject, closestSnapValue);
                    }
                    else if (closestSnapInBeat != 0)
                    {
                        // The note is not snapped. We need to snap the note with
                        // new snap value which is closestSnapInBeat + closestSnapValue.
                        // Then the offset requires a recalculation.
                        double delta = actualSnap - closestSnapInBeat;
                        hitObject.AddSnap(delta);
                        setOffsetOfElementToNewSnap(SearchUtils.GetClosestTimingPoint(beatmap.TimingPoints, hitObject.Offset), hitObject);
                    }
                }
                beatmap.overwrite();
            }
            return true;
        }
    }
}
