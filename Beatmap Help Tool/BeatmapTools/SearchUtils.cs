using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System.Collections.Generic;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class SearchUtils
    {
        public static TimingPoint getClosestInheritedPoint(List<TimingPoint> points, double offset)
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
                    if (i < points.Count - 1 && points[i].Offset == offset && 
                        points[i].IsInherited)
                        return points[i];
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

        public static TimingPoint GetClosestNextTimingPoint(List<TimingPoint> points, TimingPoint point)
        {
            for (int i = points.IndexOf(point) + 1; i < points.Count; i++)
            {
                if (!points[i].IsInherited)
                    return points[i];
            }
            return null;
        }

        public static double GetBpmValueInOffset(List<TimingPoint> points, double offset)
        {
            TimingPoint point = GetClosestTimingPoint(points, offset);
            if (point != null)
                return point.PointValue;
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

        public static void SortBeatmapElements(List<TimingPoint> points)
        {
            if (!AreTimingsSorted(points))
                points.Sort();
        }

        public static void SortBeatmapElements(List<HitObject> points)
        {
            if (!AreTimingsSorted(points))
                points.Sort();
        }

        public static void SortBeatmapElements(List<Bookmark> points)
        {
            if (!AreTimingsSorted(points))
                points.Sort();
        }

        public static void SortBeatmapElements(List<BeatmapElement> points)
        {
            if (!AreTimingsSorted(points))
                points.Sort();
        }

        public static void GetObjectsInBetween(Beatmap beatmap, int startOffset, int endOffset,
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

        public static void GetObjectsInBetween(Beatmap beatmap, int startOffset, int endOffset,
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

        private static int GetClosestPointIndex(List<TimingPoint> points, double offset)
        {
            SortBeatmapElements(points);

            // Check if the searched offset is actually bigger than the last point.
            if (offset >= points[points.Count - 1].Offset)
                return points.Count - 1;

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

        private static bool AreTimingsSorted(List<BeatmapElement> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].Offset > points[i + 1].Offset)
                    return false;
            }
            return true;
        }

        private static bool AreTimingsSorted(List<Bookmark> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].Offset > points[i + 1].Offset)
                    return false;
            }
            return true;
        }

        private static bool AreTimingsSorted(List<TimingPoint> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].Offset > points[i + 1].Offset)
                    return false;
            }
            return true;
        }

        private static bool AreTimingsSorted(List<HitObject> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].Offset > points[i + 1].Offset)
                    return false;
            }
            return true;
        }
    }
}
