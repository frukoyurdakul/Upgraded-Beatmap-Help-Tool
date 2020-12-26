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
        private const decimal BEAT_SNAP_DIVISOR_2 = 5040m;

        private const decimal _5 = BEAT_SNAP_DIVISOR_2 / 5m;
        private const decimal _7 = BEAT_SNAP_DIVISOR_2 / 7m;
        private const decimal _9 = BEAT_SNAP_DIVISOR_2 / 9m;
        private const decimal _12 = BEAT_SNAP_DIVISOR_2 / 12m;
        private const decimal _16 = BEAT_SNAP_DIVISOR_2 / 16m;

        private static SortedSet<decimal> snaps = new SortedSet<decimal>();
        private static readonly Dictionary<decimal, int> snapMapping = new Dictionary<decimal, int>();
        private static readonly decimal[] snapsArray;

        static SnapUtils()
        {
            // Generic value.
            snaps.Add(0);
            snaps.Add(BEAT_SNAP_DIVISOR_2);
            snapMapping[0] = 0;
            snapMapping[BEAT_SNAP_DIVISOR_2] = Convert.ToInt32(BEAT_SNAP_DIVISOR); 

            // 1/5
            addSnaps(new KeyValuePair<decimal[], int>(getKeys(_5), 5));

            // 1/7
            addSnaps(new KeyValuePair<decimal[], int>(getKeys(_7), 7));

            // 1/9
            addSnaps(new KeyValuePair<decimal[], int>(getKeys(_9), 9));

            // 1/12
            addSnaps(new KeyValuePair<decimal[], int>(getKeys(_12), 12));

            // 1/16
            addSnaps(new KeyValuePair<decimal[], int>(getKeys(_16), 16));

            snapsArray = new decimal[snaps.Count];
            List<decimal> snapsList = snaps.ToList();
            for (int i = 0; i < snaps.Count; i++)
                snapsArray[i] = snapsList[i];
            snaps.Clear();
        }

        private static void addSnaps(KeyValuePair<decimal[], int> pair)
        {
            foreach (decimal value in pair.Key)
            {
                snaps.Add(value);
                snapMapping[value] = pair.Value;
            }
        }

        private static decimal[] getKeys(decimal snapValue)
        {
            int count = (int)(BEAT_SNAP_DIVISOR_2 / snapValue);
            decimal[] array = new decimal[count - 1];
            for (int i = 0; i < array.Length; i++)
                array[i] = snapValue * (i + 1);
            return array;
        }

        public static double[] getRelativeSnap(List<TimingPoint> timingPoints, BeatmapElement target)
        {
            // First holds the true snap which is from the first point,
            // second holds the snap value from the closest timing point.
            double[] result = new double[2] {0, 0};

            // If this is the first timing point or the element is snapped on the first timing point,
            // its actual and relative snaps should always equal to 0. Check the condition
            // and return it immediately.
            if (timingPoints.Count == 0 || (target.Offset == timingPoints[0].Offset && !timingPoints[0].IsInherited))
                return result;

            // Get the closest timing point to this target.
            TimingPoint closestPoint = SearchUtils.GetClosestTimingPoint(timingPoints, target.Offset);

            // Now, the beat snap divisor divides the beat to specific parts, and
            // we need to calculate both relative and actual snaps.
            // Actual snap would be closestPoint + relativeSnap while
            // relative snap is calculated by closest red point's point
            // value (a.k.a millis value between 2 beats, 60000 / BPM).

            // Diff of objects in millis calculated as decimal
            // for maximum precision.
            decimal diff = Convert.ToDecimal(target.Offset - closestPoint.Offset);

            // Relative snap value based on BEAT_SNAP_DIVISOR.
            decimal relativeSnap = diff * BEAT_SNAP_DIVISOR_2 / Convert.ToDecimal(closestPoint.PointValue);

            // Actual snap value.
            decimal actualSnap = Convert.ToDecimal(closestPoint.GetSnap()) + relativeSnap;

            // Set the results and return. First is actual snap
            // from the start of the map, second is relative
            // snap from the closest red point.
            result[0] = Convert.ToDouble(actualSnap);
            result[1] = Convert.ToDouble(relativeSnap);
            return result;
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
            if (fluctuation < 0.1d || Math.Abs(target2.Offset - targetOffset) < 1)
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

        private static int getClosestSnappedOffset(BeatmapElement element, TimingPoint closestPoint, out int closestSnapDivisor)
        {
            // Gets the closest snap value in the beat. If multiple snaps
            // are found, then we also return a closestSnapIndex with 
            // a value different than -1, in which case the note's snap
            // does not change and instead gets presented into the user.
            decimal snap = Convert.ToDecimal(element.GetClosestSnap());
            decimal rawSnap = snap % BEAT_SNAP_DIVISOR_2;
            decimal snapDiff = snap - rawSnap;
            decimal result = snapsArray.BinarySearchClosest(rawSnap);
            decimal diff = Math.Abs(rawSnap - result);
            closestSnapDivisor = -1;

            // Diff = 0 or equal offsets means this element is exactly snapped.
            if (diff == 0 || closestPoint.Offset == element.Offset)
                return 0;

            // If we calculate that this object's new snap result
            // causes the offset not to be shifted, consider
            // it as snapped.
            decimal targetSnap = snapDiff + result;

            // Make the calculation by casting it to integer. If the offset are equal, these are snapped.
            int targetOffset = (int)(Convert.ToDecimal(closestPoint.Offset) + (targetSnap / BEAT_SNAP_DIVISOR_2 * Convert.ToDecimal(closestPoint.PointValue)));
            if ((int)element.Offset == targetOffset)
                return 0;

            return targetOffset;
        }

        public static bool resnapAllNotes(List<Beatmap> beatmaps, string customPath, bool shouldCreateBackup, onFailure<Beatmap, BeatmapElement, int> listener)
        {
            // Loop through all beatmaps.
            // Ask for confirmation first. Users might select a location
            // to save backups first.
            foreach (Beatmap beatmap in beatmaps)
            {
                if (shouldCreateBackup)
                    beatmap.save(customPath + "//" + beatmap.FileName);
                List<HitObject> hitObjects = beatmap.HitObjects;
                foreach (HitObject hitObject in hitObjects)
                {
                    TimingPoint closestPoint = SearchUtils.GetClosestTimingPoint(beatmap.TimingPoints, hitObject.Offset);
                    int closestSnappedOffset = getClosestSnappedOffset(hitObject, closestPoint, out int closestSnapValue);
                    if (closestSnapValue != -1)
                    {
                        // We have a note that is equal distance to defined snaps
                        // in the editor. Present this to the user.
                        listener.Invoke(beatmap, hitObject, closestSnapValue);
                    }
                    else if (closestSnappedOffset != 0)
                    {
                        // The note is not snapped. We need to snap the note with
                        // new snap value which is closestSnapInBeat + closestSnapValue.
                        // Then the offset requires a recalculation.
                        hitObject.Offset = closestSnappedOffset;
                    }
                }
                beatmap.overwrite();
            }
            return true;
        }
    }
}
