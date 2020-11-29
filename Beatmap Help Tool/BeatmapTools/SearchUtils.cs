using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System.Collections.Generic;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class SearchUtils
    {
        public static readonly Dictionary<IEnumerable<BeatmapElement>, bool> sortInfo =
            new Dictionary<IEnumerable<BeatmapElement>, bool>();

        public static void MarkChangeMade(IEnumerable<BeatmapElement> elements)
        {
            if (sortInfo.ContainsKey(elements))
                sortInfo[elements] = false;
        }

        public static void MarkSorted(IEnumerable<BeatmapElement> elements)
        {
            sortInfo[elements] = true;
        }

        public static void ClearSortInfo()
        {
            sortInfo.Clear();
        }

        public static TimingPoint GetClosestInheritedPoint(List<TimingPoint> points, double offset)
        {
            int i = GetClosestPointIndex(points, offset);
            if (i >= 0)
            {
                if (points[i].IsInherited)
                    return points[i];
                else
                {
                    // This condition might be true because the index has found the
                    // timing point but at the same time there can be another inherited
                    // point which should be the next element in this list.
                    // Check for that first.
                    if (i < points.Count - 1 && points[i + 1].Offset == offset && 
                        points[i].IsInherited)
                        return points[i + 1];
                    else
                    {
                        // It means there is no inherited point after this red point. Get that red point
                        // and return it as an inherited one by changing the values.
                        TimingPoint inheritedDummy = new TimingPoint(points[i], points)
                        {
                            PointValue = -100d,
                            IsInherited = true
                        };
                        return inheritedDummy;
                    }
                }
            }
            else
            {
                // There is no active any points before this offset even though we return a dummy
                // even if there is no points.
                MessageBoxUtils.showError("No timing points have been found, notes could not be processed.");
                return null;
            }
        }

        public static TimingPoint GetClosestTimingPoint(List<TimingPoint> points, double offset)
        {
            int i = GetClosestPointIndex(points, offset);
            if (i >= 0)
            {
                if (!points[i].IsInherited)
                    return points[i];
                else
                {
                    // The search might have been found the inherited point
                    // instead of the timing point. Loop until the closest timing point
                    // has been found. If index becomes 0, throw an error.
                    for (; i >= 0; i--)
                    {
                        if (points[i].Offset <= offset && !points[i].IsInherited)
                            return points[i];
                    }

                    // At this point, it is time to throw the error.
                    MessageBoxUtils.showError("Somehow an inherited point has been found but there were no " +
                            "timing points to take reference for.");
                    return null;
                }
            }
            else
            {
                // There is no active any points before this offset even though we return a dummy
                // even if there is no points.
                MessageBoxUtils.showError("No timing points have been found, notes could not be processed.");
                return null;
            }
        }

        public static TimingPoint GetClosestPoint(List<TimingPoint> points, double offset, bool preferInherited)
        {
            if (preferInherited)
            {
                TimingPoint point = GetClosestInheritedPoint(points, offset);
                if (point != null)
                    return point;
                else
                    return GetClosestTimingPoint(points, offset);
            }
            else
            {
                TimingPoint point = GetClosestTimingPoint(points, offset);
                if (point != null)
                    return point;
                else
                    return GetClosestInheritedPoint(points, offset);
            }
        }

        public static TimingPoint GetExactTimingPoint(List<TimingPoint> points, double offset)
        {
            return VerifyUtils.safeGetItemFromList(points, GetExactPointIndex(points, offset, false));
        }

        public static TimingPoint GetExactInheritedPoint(List<TimingPoint> points, double offset)
        {
            return VerifyUtils.safeGetItemFromList(points, GetExactPointIndex(points, offset, true));
        }

        public static int GetExactPointIndex(List<TimingPoint> points, double offset, bool isInherited)
        {
            // Perform a direct binary search.
            SortBeatmapElements(points);

            int first = 0;
            int last = points.Count - 1;
            int mid = 0;
            do
            {
                mid = first + (last - first) / 2;
                if (offset > points[mid].Offset)
                    first = mid + 1;
                else
                    last = mid - 1;
                if (points[mid].Offset == offset)
                    return mid;
            } while (first <= last);

            return -1;
        }

        public static TimingPoint GetClosestNextTimingPoint(List<TimingPoint> points, TimingPoint point)
        {
            for (int i = points.IndexOf(point) + 1; i < points.Count; i++)
            {
                if (!points[i].IsInherited)
                    return points[i];
            }
            return null;
        }

        // Returns the original bpm value in milliseconds.
        // Example, If the BPM is 200, this returns 300.
        public static double GetBpmValueInOffset(List<TimingPoint> points, double offset)
        {
            TimingPoint point = GetClosestTimingPoint(points, offset);
            if (point != null)
                return point.PointValue;
            else
                return -1;
        }

        // Returns the BPM representation of the milliseconds per beat.
        // Example, If the BPM is 200, this returns 200.
        public static double GetBpmInOffset(List<TimingPoint> points, double offset)
        {
            TimingPoint point = GetClosestTimingPoint(points, offset);
            if (point != null)
                return 60000d / point.PointValue;
            else
                return -1;
        }

        public static bool IsFirstPointTimingPoint(List<TimingPoint> points)
        {
            return points.Count > 0 && !points[0].IsInherited;
        }

        public static bool ContainsTimingPoint(List<TimingPoint> points)
        {
            foreach (TimingPoint point in points)
                if (!point.IsInherited)
                    return true;
            return false;
        }

        public static bool ContainsInheritedPoint(List<TimingPoint> points)
        {
            foreach (TimingPoint point in points)
                if (point.IsInherited)
                    return true;
            return false;
        }

        public static bool ContainsBpmChanges(List<TimingPoint> points)
        {
            ISet<double> bpmValues = new HashSet<double>();
            foreach(TimingPoint point in points)
            {
                if (!point.IsInherited)
                    bpmValues.Add(point.PointValue);

                // At least 2 points are considered as the list
                // containing BPM changes. Finding one timing
                // point is false in this case.
                if (bpmValues.Count > 1)
                    return true;
            }
            return false;
        }

        public static bool TogglesKiai(List<TimingPoint> points, TimingPoint point)
        {
            // Always sort the elements.
            SortBeatmapElements(points);

            double offset = point.Offset;
            bool isKiaiOpen = point.IsKiaiOpen;

            // Now, here we need to get all points before that offset.
            // A kiai can be opened by both inherited and timing points,
            // and it can be closed if timing point has a kiai but inherited
            // has not. Depending on this, get the previous points, both
            // inherited and timing. If offsets are changed after
            // getting the first one, ignore the other point.
            TimingPoint timingPoint = GetClosestPoint(points, offset, false);
            TimingPoint inheritedPoint = GetClosestPoint(points, offset, true);

            // The reference point we use to determine previous kiai status.
            TimingPoint referencePoint;

            // Check which one is closer. If offsets are the same, take the
            // inherited one.
            if (timingPoint.Offset == inheritedPoint.Offset)
                referencePoint = inheritedPoint;
            else if (timingPoint.Offset > inheritedPoint.Offset)
                referencePoint = timingPoint;
            else
                referencePoint = inheritedPoint;

            // Check the difference and see if it actually toggles.
            return isKiaiOpen != referencePoint.IsKiaiOpen;
        }

        public static void SortBeatmapElements(List<TimingPoint> points)
        {
            if (!AreTimingsSorted(points))
            {
                points.Sort();
                MarkSorted(points);
            }
        }

        public static void SortBeatmapElements(List<HitObject> points)
        {
            if (!AreTimingsSorted(points))
            {
                points.Sort();
                MarkSorted(points);
            }
        }

        public static void SortBeatmapElements(List<Bookmark> points)
        {
            if (!AreTimingsSorted(points))
            {
                points.Sort();
                MarkSorted(points);
            }
        }

        public static void SortBeatmapElements(List<BeatmapElement> points)
        {
            if (!AreTimingsSorted(points))
            {
                points.Sort();
                MarkSorted(points);
            }
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out List<TimingPoint> points)
        {
            List<TimingPoint> pointsInternal = new List<TimingPoint>();

            SortBeatmapElements(beatmap.TimingPoints);

            foreach (TimingPoint element in beatmap.TimingPoints)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                    pointsInternal.Add(element);
            }

            points = pointsInternal;
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out List<HitObject> objects)
        {
            List<HitObject> objectsInternal = new List<HitObject>();

            SortBeatmapElements(beatmap.HitObjects);

            foreach (HitObject element in beatmap.HitObjects)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                    objectsInternal.Add(element);
            }

            objects = objectsInternal;
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out List<TimingPoint> points, out List<HitObject> objects)
        {
            List<TimingPoint> pointsInternal = new List<TimingPoint>();
            List<HitObject> objectsInternal = new List<HitObject>();

            SortBeatmapElements(beatmap.TimingPoints);
            SortBeatmapElements(beatmap.HitObjects);

            foreach (TimingPoint element in beatmap.TimingPoints)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                    pointsInternal.Add(element);
            }
            foreach (HitObject element in beatmap.HitObjects)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                    objectsInternal.Add(element);
            }

            points = pointsInternal;
            objects = objectsInternal;
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out List<Bookmark> bookmarks, out List<TimingPoint> points, out List<HitObject> objects)
        {
            List<Bookmark> bookmarksInternal = new List<Bookmark>();
            List<TimingPoint> pointsInternal = new List<TimingPoint>();
            List<HitObject> objectsInternal = new List<HitObject>();

            SortBeatmapElements(beatmap.Bookmarks);
            SortBeatmapElements(beatmap.TimingPoints);
            SortBeatmapElements(beatmap.HitObjects);

            foreach (Bookmark element in beatmap.Bookmarks)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                    bookmarksInternal.Add(element);
            }
            foreach (TimingPoint element in beatmap.TimingPoints)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                    pointsInternal.Add(element);
            }
            foreach (HitObject element in beatmap.HitObjects)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                    objectsInternal.Add(element);
            }

            bookmarks = bookmarksInternal;
            points = pointsInternal;
            objects = objectsInternal;
        }

        public static int GetClosestZeroSnapPointIndex(List<TimingPoint> points)
        {
            SortBeatmapElements(points);
            for (int i = points.Count - 1; i > 0; i--)
            {
                if (points[i].GetSnap() == 0)
                    return i;
            }

            // We return 0 here because 0 is not searched and it always have to be
            // the 0 snap point.
            return 0;
        }

        public static int GetAdditionIndex(List<TimingPoint> points, TimingPoint point)
        {
            // Apply a brute-force way as to where the offset is higher
            // than this point.
            // TODO Find a better algorithm for this.

            double offset;
            TimingPoint pointInternal;
            for (int i = 0; i < points.Count; i++)
            {
                pointInternal = points[i];
                offset = pointInternal.Offset;
                if (offset > point.Offset)
                    return i;
                else if (offset == point.Offset)
                {
                    // If the added point is inherited, it should be
                    // after the timing point.
                    // If the added point is timing, it should be before
                    // inherited. Determine the state here.
                    if (point.IsInherited && !pointInternal.IsInherited)
                    {
                        if (i + 1 < points.Count)
                            return i + 1;
                        else
                            return i;
                    }
                    else if (!point.IsInherited && pointInternal.IsInherited)
                    {
                        if (i - 1 >= 0)
                            return i - 1;
                        else
                            return i;
                    }
                    else
                    {
                        // We need to enforce an exception here. Offset cannot be the same
                        // and type cannot be the same if we want to add a point.
                        MessageBoxUtils.showError("Program will throw an exception\n, a duplicate inherited or timing point is found in the list.");
                        return -1;
                    }
                }
            }

            // Default return 0 here.
            return 0;
        }

        private static int GetClosestPointIndex(List<TimingPoint> points, double offset)
        {
            SortBeatmapElements(points);

            // Check if the searched offset is actually bigger than the last point.
            if (offset >= points[points.Count - 1].Offset)
                return points.Count - 1;
            // Also check if the searched offset is actually smaller than the first point.
            else if (offset <= points[0].Offset)
                return 0;

            // If not, start the binary search. Totally copied from StackOverflow.
            int first = 0;
            int last = points.Count - 1;
            int mid = 0;
            do
            {
                mid = first + (last - first) / 2;
                if (offset > points[mid].Offset)
                    first = mid + 1;
                else
                    last = mid - 1;
                if (points[mid].Offset == offset)
                    return mid;
            } while (first <= last);

            // If we are here, it means there are no points with the exact offset.
            // Keep searching from the "last" index.
            if (points[last].Offset > offset)
            {
                for (int i = last; i >= 0; i--)
                {
                    if (points[i].Offset <= offset)
                        return i;
                }

                // If we reached here, it means there is no timing point before this offset.
                // So, return -1 instead which will show a message box while creating objects.
                return -1;
            }
            else
            {
                // Otherwise, the "first" is already pointing the first object that is earlier
                // than the offset we seek. Just return that.
                return first;
            }
        }

        private static bool AreTimingsSorted(IEnumerable<BeatmapElement> elements)
        {
            if (sortInfo.ContainsKey(elements))
                return sortInfo[elements];

            BeatmapElement previous = null;
            IEnumerator<BeatmapElement> enumerator = elements.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (previous != null && enumerator.Current != null && previous.Offset > enumerator.Current.Offset)
                    return false;
                previous = enumerator.Current;
            }
            return true;
        }
    }
}
