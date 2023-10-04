using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                        TimingPoint inheritedDummy = new TimingPoint(points[i])
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

        public static TimingPoint GetClosestTimingPoint(IList<TimingPoint> points, double offset)
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

        public static TimingPoint GetExactTimingPoint(IList<TimingPoint> points, double offset)
        {
            return VerifyUtils.safeGetItemFromList(points, GetExactPointIndex(points, offset, false));
        }

        public static TimingPoint GetExactInheritedPoint(IList<TimingPoint> points, double offset)
        {
            return VerifyUtils.safeGetItemFromList(points, GetExactPointIndex(points, offset, true));
        }

        public static HitObject GetExactHitObject(IList<HitObject> hitObjects, double offset)
        {
            return VerifyUtils.safeGetItemFromList(hitObjects, GetExactNoteIndex(hitObjects, offset));
        }

        public static int GetExactPointIndex(IList<TimingPoint> points, double offset, bool isInherited)
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
                {
                    if (points[mid].IsInherited == isInherited)
                        return mid;
                    else
                    {
                        // If target is not inherited and we've found an inherited one, the previous
                        // point has to be the same spot timing point.
                        if (!isInherited)
                            return mid - 1;
                        else
                        {
                            // We need to check for an existing inherited point.
                            // We will definitely have a timing point, but
                            // we may not have an inherited point next.
                            mid += 1;
                            if (mid < points.Count && points[mid].Offset == offset && points[mid].IsInherited == isInherited)
                                return mid;
                            else
                                return -1;
                        }
                    }
                }
            } while (first <= last);

            // This is basically the default.
            return -1;
        }

        public static int GetExactNoteIndex(IList<HitObject> hitObjects, double offset)
        {
            // Perform a direct binary search.
            SortBeatmapElements(hitObjects);

            int first = 0;
            int last = hitObjects.Count - 1;
            int mid = 0;
            do
            {
                mid = first + (last - first) / 2;
                if (offset > hitObjects[mid].Offset)
                    first = mid + 1;
                else
                    last = mid - 1;
                if (hitObjects[mid].Offset == offset)
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
        public static double GetBpmValueInOffset(IList<TimingPoint> points, double offset)
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

        // Returns the closest previous value inside the list. The list
        // should be sorted.
        public static T GetClosestLowerValue<T>(List<T> list, T searched) where T: IComparable
        {
            // We know that this list should be sorted. That's why applying
            // binary search is crucial here.

            // If the list does not contain elements, throw an exception.
            // If it contains one element, check for closest value.
            // If it matches, return the value, otherwise return a default.
            if (list.Count == 0)
                throw new ArgumentException("The list passed does not contain values to search the closest value.");

            // If the searched value is already higher than last, return that first.
            // Or, if it's reversed, return the first element.
            if (list[list.Count - 1].CompareTo(searched) < 0)
                return default;

            int first = 0;
            int last = list.Count - 1;
            int mid;
            do
            {
                mid = first + (last - first) / 2;
                int compareResult = list[mid].CompareTo(searched);
                if (compareResult < 0)
                    first = mid + 1;
                else
                    last = mid - 1;
                if (list[mid].CompareTo(searched) == 0)
                    return list[mid];
            } while (first <= last);

            // Since we're searching closest lower value, return min of last and first.
            return VerifyUtils.safeGetItemFromList(list, Math.Min(first, last));
        }

        // Returns the closest previous value inside the list. The list
        // should be sorted.
        public static T GetClosestHigherValue<T>(List<T> list, T searched) where T : IComparable
        {
            // We know that this list should be sorted. That's why applying
            // binary search is crucial here.

            // If the list does not contain elements, throw an exception.
            // If it contains one element, check for closest value.
            // If it matches, return the value, otherwise return a default.
            if (list.Count == 0)
                throw new ArgumentException("The list passed does not contain values to search the closest value.");

            // If the searched value is already higher than last, return that first.
            // Or, if it's reversed, return the first element.
            if (list[0].CompareTo(searched) > 0)
                return list[0];

            int first = 0;
            int last = list.Count - 1;
            int mid;
            do
            {
                mid = first + (last - first) / 2;
                int compareResult = list[mid].CompareTo(searched);
                if (compareResult < 0)
                    first = mid + 1;
                else
                    last = mid - 1;
                if (list[mid].CompareTo(searched) == 0)
                    return list[mid];
            } while (first <= last);

            // Since we're searching closest higher value, return max of last and first.
            return VerifyUtils.safeGetItemFromList(list, Math.Max(first, last));
        }

        public static bool IsFirstPointTimingPoint(IList<TimingPoint> points)
        {
            return points.Count > 0 && !points[0].IsInherited;
        }

        public static bool ContainsTimingPoint(IList<TimingPoint> points)
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

        // Returns true if:
        // This is a green point and,
        // This point changes SV and,
        // The point is not toggling kiai and,
        // The point is not on a red point as well.
        public static bool ChangesSvOnly(List<TimingPoint> points, List<HitObject> hitObjects, TimingPoint currentPoint)
        {
            // Shortcut: Return false if the point is not a green point.
            if (!currentPoint.IsInherited)
                return false;

            // Always sort the elements.
            SortBeatmapElements(points);

            // Assign the offset value.
            double currentOffset = currentPoint.Offset;

            // Now, here we need to get all points before that offset.
            // Depending on the conditions of the function, check one by one.
            TimingPoint closestTimingPoint = GetClosestPoint(points, currentOffset, false);
            TimingPoint closestInheritedPoint = GetClosestPoint(points, currentOffset - 1, true);

            // The inherited point can still be a timing point. In that case,
            // assume its value is 1.00x and determine whether this point
            // has an SV change. Otherwise, compare point values directly.
            double inheritedValue = closestInheritedPoint.IsInherited ? closestInheritedPoint.PointValue : -100d;
            if (currentPoint.PointValue != inheritedValue)
            {
                // That means this point is changing SV.
                // Check the final conditions: the kiai change and the timing point.
                if (closestTimingPoint.Offset == currentOffset)
                {
                    // The inherited point is on a timing point, do not move.
                    return false;
                }
                else if (TogglesKiai(points, currentPoint))
                {
                    // The inherited point toggles kiai in some way, it should
                    // not be moved.
                    return false;
                }
                else if (GetExactNoteIndex(hitObjects, currentOffset) < 0)
                {
                    // This SV is not on a note, it should not be moved.
                    return false;
                }

                // This point is allowed to be moved.
                return true;
            }
            else
            {
                // This point is not changing SV, do not touch.
                return false;
            }
        }

        public static void SortBeatmapElements(IList<TimingPoint> points)
        {
            if (!AreTimingsSorted(points))
            {
                SortListIfType(points);
                MarkSorted(points);
            }
        }

        public static void SortBeatmapElements(IList<HitObject> points)
        {
            if (!AreTimingsSorted(points))
            {
                SortListIfType(points);
                MarkSorted(points);
            }
        }

        public static void SortBeatmapElements(IList<Bookmark> points)
        {
            if (!AreTimingsSorted(points))
            {
                SortListIfType(points);
                MarkSorted(points);
            }
        }

        public static void SortBeatmapElements(IList<BeatmapElement> points)
        {
            if (!AreTimingsSorted(points))
            {
                SortListIfType(points);
                MarkSorted(points);
            }
        }

        private static void SortListIfType<T>(IList<T> list)
        {
            if (list is List<T>)
                ((List<T>)list).Sort();
        }

        public static int GetBeatmapCountInMapset(Beatmap beatmap)
        {
            string folderPath = beatmap.FolderPath;
            string[] files = Directory.GetFiles(folderPath, "*.osu");
            return files.Length;
        }

        public static void GetBeatmapPointsInMapset(Beatmap beatmap, out Dictionary<Beatmap, List<TimingPoint>> timingPointsPerBeatmap, out List<List<TimingPoint>> allPoints)
        {
            string folderPath = beatmap.FolderPath;
            List<Beatmap> beatmapList = new List<Beatmap>();
            foreach (string file in Directory.GetFiles(folderPath, "*.osu"))
                beatmapList.Add(new Beatmap(file, false));

            // First, check total of red points in all diffs. If they are inconsistent,
            // it means the timing is wrong anyway. We should dump those first.
            timingPointsPerBeatmap = new Dictionary<Beatmap, List<TimingPoint>>();
            foreach (Beatmap beatmapInner in beatmapList)
                timingPointsPerBeatmap.Add(beatmapInner, beatmapInner.TimingPoints.FindAll(target => !target.IsInherited));

            // Get the elements as list. Order is not important.
            allPoints = timingPointsPerBeatmap.Values.ToList();
        }

        public static void GetAllBeatmaps(Beatmap beatmap, List<Beatmap> beatmaps)
        {
            string folderPath = beatmap.FolderPath;
            foreach (string file in Directory.GetFiles(folderPath, "*.osu"))
            {
                Beatmap copy = new Beatmap(file, false);
                if (copy.isModeTaiko())
                {
                    if (copy.FileName.Equals(beatmap.FileName))
                        beatmaps.Add(beatmap);
                    else
                        beatmaps.Add(copy);
                }
            }
        }

        public static void GetAllBeatmaps(Beatmap beatmap, out List<Beatmap> beatmaps)
        {
            string folderPath = beatmap.FolderPath;
            beatmaps = new List<Beatmap>();
            foreach (string file in Directory.GetFiles(folderPath, "*.osu"))
            {
                Beatmap copy = new Beatmap(file, false);
                if (copy.isModeTaiko())
                {
                    if (copy.FileName.Equals(beatmap.FileName))
                        beatmaps.Add(beatmap);
                    else
                        beatmaps.Add(copy);
                }
            }
        }

        // Get barlines as decimal. These won't have rounding errors since decimals are more precise.
        public static void GetBarlines(List<TimingPoint> redPoints, List<HitObject> hitObjects, out List<decimal> barlines)
        {
            // Since these timing points are in order, use them in our advantage.
            TimingPoint point;

            barlines = new List<decimal>();

            // Calculate the barlines using decimal. The drawn
            // barlines are double, and the drawn ones in the
            // editor are calculated with decimal.
            for (int i = 0; i < redPoints.Count; i++)
            {
                point = redPoints[i];
                decimal nextOffset;
                if (i + 1 < redPoints.Count)
                    nextOffset = (decimal)redPoints[i + 1].Offset;
                else
                    nextOffset = (decimal)hitObjects[hitObjects.Count - 1].Offset;
                decimal currentOffset = (decimal)point.Offset;
                decimal barlineValue = (decimal)(point.PointValue * point.Meter);

                // Starting from the first red point, add barlines.
                // Consider it as not omitted.
                if (!point.IsOmitted && nextOffset > currentOffset)
                    barlines.Add(currentOffset);

                // Until the calculated offset is higher than 2nd point,
                // calculate it again and add the point.
                currentOffset += barlineValue;
                while (currentOffset < nextOffset)
                {
                    barlines.Add(currentOffset);
                    currentOffset += barlineValue;
                }
            }

            decimal minimalFraction = 0.0000000001m;
            for (int i = 0; i < barlines.Count - 1; i++)
            {
                if (Math.Abs(barlines[i] - barlines[i + 1]) < minimalFraction && Math.Truncate(barlines[i + 1]) == barlines[i + 1])
                    barlines.RemoveAt(i--);
            }
        }

        // Get barlines as double. This is a function where the barlines returned from this list
        // may have rounding errors.
        public static void GetBarlines(List<TimingPoint> redPoints, List<HitObject> hitObjects, out List<double> barlines)
        {
            // Since these timing points are in order, use them in our advantage.
            TimingPoint point;

            barlines = new List<double>();

            // Now, calculate the same snappings and barlines using double
            // and calculate the rounding errors.
            for (int i = 0; i < redPoints.Count; i++)
            {
                point = redPoints[i];
                double nextOffset;
                if (i + 1 < redPoints.Count)
                    nextOffset = redPoints[i + 1].Offset;
                else
                    nextOffset = hitObjects[hitObjects.Count - 1].Offset;
                double currentOffset = point.Offset;
                double barlineValue = point.PointValue * point.Meter;

                // Starting from the first red point, add barlines.
                // Consider it as not omitted.
                if (!point.IsOmitted && nextOffset > currentOffset)
                    barlines.Add(currentOffset);

                // Until the calculated offset is higher than 2nd point,
                // calculate it again and add the point.
                currentOffset += barlineValue;
                while (currentOffset < nextOffset)
                {
                    barlines.Add(currentOffset);
                    currentOffset += barlineValue;
                }
            }

            double minimalFraction = 0.0000000000005d;
            for (int i = 0; i < barlines.Count - 1; i++)
            {
                if (Math.Abs(barlines[i] - barlines[i + 1]) < minimalFraction && Math.Abs(barlines[i + 1]) <= (double.Epsilon * 100))
                    barlines.RemoveAt(i--);
            }
        }

        // Get barlines as decimal. These won't have rounding errors since decimals are more precise.
        // This function also returns the dangerous barlines while having the actual barlines with
        // double precision.
        public static void GetBarlines(List<TimingPoint> redPoints, List<HitObject> hitObjects, out List<decimal> barlines, out List<decimal> dangerousBarlines)
        {
            GetBarlines(redPoints, hitObjects, out _, out barlines, out dangerousBarlines);
        }

        // Get barlines as decimal. These won't have rounding errors since decimals are more precise.
        // This function also returns the dangerous barlines while having the actual barlines with
        // double precision.
        public static void GetBarlines(List<TimingPoint> redPoints, List<HitObject> hitObjects, out List<double> barlines, out List<decimal> dangerousBarlines)
        {
            GetBarlines(redPoints, hitObjects, out barlines, out _, out dangerousBarlines);
        }

        // Get barlines as decimal. These won't have rounding errors since decimals are more precise.
        // This function also returns the dangerous barlines while having the actual barlines with
        // double and decimal precision.
        public static void GetBarlines(List<TimingPoint> redPoints, List<HitObject> hitObjects, out List<double> barlinesDouble, out List<decimal> barlinesDecimal, out List<decimal> dangerousBarlines)
        {
            GetBarlines(redPoints, hitObjects, out barlinesDouble);
            GetBarlines(redPoints, hitObjects, out barlinesDecimal);

            // After that, find the dangerous barlines. They are 
            // basically barlines which have rounding errors on them.
            // If there are 0, that means there can't be flying
            // barlines on this map.
            dangerousBarlines = new List<decimal>();
            for (int i = 0; i < barlinesDecimal.Count; i++)
            {
                double barlineDouble = barlinesDouble[i];
                decimal barlineDecimal = barlinesDecimal[i];

                if ((int)barlineDecimal > (int)barlineDouble)
                    dangerousBarlines.Add(barlineDecimal);
            }
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out IList<TimingPoint> points)
        {
            SortBeatmapElements(beatmap.TimingPoints);

            int startIndex = -1;
            int endIndex = -1;
            int i = 0;
            foreach (TimingPoint element in beatmap.TimingPoints)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                {
                    if (startIndex == -1)
                        startIndex = i;
                    endIndex = i;
                }
                i++;
            }

            points = VerifyUtils.createSafeSublist(beatmap.TimingPoints, startIndex, endIndex);
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out IList<HitObject> objects)
        {
            SortBeatmapElements(beatmap.HitObjects);

            int startIndex = -1;
            int endIndex = -1;
            int i = 0;
            foreach (HitObject element in beatmap.HitObjects)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                {
                    if (startIndex == -1)
                        startIndex = i;
                    endIndex = i;
                }
                i++;
            }

            objects = VerifyUtils.createSafeSublist(beatmap.HitObjects, startIndex, endIndex);
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out IList<TimingPoint> points, out IList<HitObject> objects)
        {
            SortBeatmapElements(beatmap.TimingPoints);
            SortBeatmapElements(beatmap.HitObjects);

            int startIndex = -1;
            int endIndex = -1;
            int i = 0;
            foreach (TimingPoint element in beatmap.TimingPoints)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                {
                    if (startIndex == -1)
                        startIndex = i;
                    endIndex = i;
                }
                i++;
            }
            points = VerifyUtils.createSafeSublist(beatmap.TimingPoints, startIndex, endIndex);

            startIndex = -1;
            endIndex = -1;
            i = 0;
            foreach (HitObject element in beatmap.HitObjects)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                {
                    if (startIndex == -1)
                        startIndex = i;
                    endIndex = i;
                }
                i++;
            }
            objects = VerifyUtils.createSafeSublist(beatmap.HitObjects, startIndex, endIndex);
        }

        public static void GetObjectsInBetween(Beatmap beatmap, double startOffset, double endOffset,
            out IList<Bookmark> bookmarks, out IList<TimingPoint> points, out IList<HitObject> objects)
        {
            SortBeatmapElements(beatmap.Bookmarks);
            SortBeatmapElements(beatmap.TimingPoints);
            SortBeatmapElements(beatmap.HitObjects);

            int startIndex = -1;
            int endIndex = -1;
            int i = 0;
            foreach (Bookmark element in beatmap.Bookmarks)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                {
                    if (startIndex == -1)
                        startIndex = i;
                    endIndex = i;
                }
                i++;
            }
            bookmarks = VerifyUtils.createSafeSublist(beatmap.Bookmarks, startIndex, endIndex);

            startIndex = -1;
            endIndex = -1;
            i = 0;
            foreach (TimingPoint element in beatmap.TimingPoints)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                {
                    if (startIndex == -1)
                        startIndex = i;
                    endIndex = i;
                }
                i++;
            }
            points = VerifyUtils.createSafeSublist(beatmap.TimingPoints, startIndex, endIndex);

            startIndex = -1;
            endIndex = -1;
            i = 0;
            foreach (HitObject element in beatmap.HitObjects)
            {
                if (VerifyUtils.verifyRange(startOffset, endOffset, element.Offset))
                {
                    if (startIndex == -1)
                        startIndex = i;
                    endIndex = i;
                }
                i++;
            }
            objects = VerifyUtils.createSafeSublist(beatmap.HitObjects, startIndex, endIndex);
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

        private static int GetClosestPointIndex(IList<TimingPoint> points, double offset)
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
            int mid;
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
            last = Math.Max(first, last);
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
                // Otherwise, the "mid" is already pointing the first object that is earlier
                // than the offset we seek. Just return that.
                return mid;
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
