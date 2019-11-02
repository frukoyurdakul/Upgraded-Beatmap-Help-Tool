﻿using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class SearchTools
    {
        private static readonly Dictionary<List<TimingPoint>, bool> sortInfo =
            new Dictionary<List<TimingPoint>, bool>();

        public static void resetSortStatus()
        {
            sortInfo.Clear();
        }

        public static TimingPoint GetClosestActiveInheritedPoint(List<TimingPoint> points, double offset)
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
                        TimingPoint inheritedDummy = new TimingPoint(points[i]);
                        inheritedDummy.PointValue = -100d;
                        inheritedDummy.IsInherited = true;
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
                    // instead of the timing point. The point before should be
                    // the timing point we seek for. If not, an error will be shown.
                    if (i > 0 && points[i - 1].Offset == offset && !points[i - 1].IsInherited)
                        return points[i - 1];
                    else
                    {
                        MessageBoxUtils.showError("Somehow an inherited point has been found but there were no " +
                            "timing points to take reference for. Notes could not be processed.");
                        return null;
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

        private static int GetClosestPointIndex(List<TimingPoint> points, double offset)
        {
            SortTimingPoints(points);
            // Now that the points are sorted, start the binary search algorithm.

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

            // The "mid" we found could still be bigger than the one we seek, which is the
            // first lowest, so if the offset is bigger, keep searching until it is smaller.
            if (points[mid].Offset > offset)
            {
                for (int i = mid; i >= 0; i--)
                {
                    if (points[i].Offset <= offset)
                        return i;
                }

                // If we reached here, it means there is no timing point before this offset.
                // So, return -1 instead which will show a message box while creating objects.
                return -1;
            }
            else
                return mid;
        }

        public static void SortTimingPoints(List<TimingPoint> points)
        {
            if (!AreTimingsSorted(points))
                points.Sort();
        }

        private static bool AreTimingsSorted(List<TimingPoint> points)
        {
            if (sortInfo[points])
                return true;

            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].Offset > points[i + 1].Offset)
                    return false;
            }

            sortInfo.Add(points, true);
            return true;
        }
    }
}
