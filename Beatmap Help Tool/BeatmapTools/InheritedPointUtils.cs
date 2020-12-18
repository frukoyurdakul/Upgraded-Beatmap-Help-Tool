﻿using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class InheritedPointUtils
    {
        public static bool AddSvChanges(Form form, Beatmap beatmap, double firstOffset, double lastOffset, 
            double firstSv, double lastSv, double targetBpm, 
            double gridSnap, int svOffset, int svIncreaseMode, int count, 
            double svIncreaseMultiplier, bool putPointsByNotes)
        {
            List<TimingPoint> actualPoints = beatmap.TimingPoints;
            List<HitObject> actualObjects = beatmap.HitObjects;

            if (count > 0 && lastOffset <= firstOffset)
            {
                if (putPointsByNotes)
                {
                    // We need to determine the end offset here from notes
                    // and note count.
                    SnapUtils.getEndOffsetFromObjectsByCount(beatmap, firstOffset, count, out lastOffset);
                }
                else
                {
                    if (gridSnap == 0)
                    {
                        showErrorMessageInMainThread(form, "End offset was not defined and grid snap and count values are\n also not defined, hence the end offset\ncould not be calculated. Aborting.");
                        return false;
                    }
                    lastOffset = SnapUtils.calculateEndOffset(beatmap, firstOffset, gridSnap, count);
                }
            }
            else if (lastOffset <= firstOffset)
            {
                showErrorMessageInMainThread(form, "End offset could not be calculated,\nnecessary values are missing.");
                return false;
            }

            SearchUtils.GetObjectsInBetween(beatmap, firstOffset, lastOffset,
                out List<TimingPoint> points, out List<HitObject> objects);
    
            // If "putPointsByNotes" is selected and no objects are found, throw an error
            // and return false.
            if (putPointsByNotes && objects.Count == 0)
            {
                showErrorMessageInMainThread(form, "No hit objects found in the specified area. Aborting.");
                return false;
            }

            // We need at least 1 timing point to take reference from
            // the start of the list. Account that and add a timing
            // point if the first index is not a timing point already.
            if (!SearchUtils.IsFirstPointTimingPoint(points))
                points.Insert(0, SearchUtils.GetClosestTimingPoint(actualPoints, firstOffset));

            // After this, if we still don't have a timing point, we cannot proceed.
            // A map has to contain at least one timing point before or on the declared offset.
            if (!SearchUtils.ContainsTimingPoint(points))
            {
                showErrorMessageInMainThread(form, "No timing points found to take reference from. Aborting.");
                return false;
            }
        
            // The POW value. This is determined by
            // "svIncreaseMode" and "svIncreaseMultiplier"
            // where "svIncreaseMode" is 0 for linear, 1 for exponential
            // and 2 for logarithmic. If mode is 0, POW is always 1.
            // If mode is 1, the POW is the original value.
            // If mode is 2, the POW is divided as 1/POW.
            double pow;
            switch (svIncreaseMode)
            {
                case 0:
                    pow = 1;
                    break;
                case 1:
                    pow = svIncreaseMultiplier;
                    break;
                case 2:
                    pow = 1 / svIncreaseMultiplier;
                    break;
                default:
                    throw new ArgumentException("The sv increase mode has to be 0, 1 or 2, found " + svIncreaseMode + ".");
            }

            // This is a really corner case since we already prevent it from
            // happening in the SV Changer form itself, but it still is checked.
            if (pow == 0)
            {
                showErrorMessageInMainThread(form, "The sv increase mode and sv increase multiplier has\nresulted in the power value being 0. Aborting.");
                return false;
            }

            // If "put points by notes" is selected,
            // we need to calculate the target offset by 
            // the first note.
            if (putPointsByNotes)
                firstOffset = objects[0].Offset;

            // Get the start BPM value.
            double startBpmValue = SearchUtils.GetBpmValueInOffset(actualPoints, firstOffset);

            // If "targetBpm" is entered, we need to apply a ratio to the last SV.
            // Apply the logic here and change the last SV value depending on it.
            if (targetBpm != 0)
            {
                double ratio = targetBpm / SearchUtils.GetBpmInOffset(actualPoints, firstOffset);
                lastSv *= ratio;
            }

            // Now that we have set already in place, let's calculate the SVs and
            // add or edit them.

            // Get the total SV difference.
            double totalSvDifference = lastSv - firstSv;

            // Get the total offset difference.
            double totalOffsetDifference = lastOffset - firstOffset;

            // The temp value to use on additional SVs.
            double targetSv;

            // The temp value for the SV for osu! representation.
            double targetSvValue;

            // The current percentage that will result
            // in the final SV of the point. This should be
            // always equal to percentage change if the change is linear.
            double targetPowerValue;

            // Define a target offset object and 
            // calculate the target offset depending
            // on the passed parameters, a.k.a "putPointsByNoteSnaps"
            // or "gridSnap" and "count".
            double targetOffset = firstOffset;

            // Used in determining which hit object
            // we should use if "put points by notes"
            // is selected, as the target offset.
            int hitObjectIndex = 0;

            while (targetOffset <= lastOffset)
            {
                // Compute the actual target offset, where the offset is determined
                // but needs to be shifted depending on user input.
                double actualTargetOffset = targetOffset + svOffset;

                // This is the BPM value, not the BPM itself, a.k.a it is the osu! representation
                // of a BPM value. For instance, this is 1000 if BPM is 120 in the map, where
                // a beat is in a total second.
                double currentBpmValue = SearchUtils.GetBpmValueInOffset(points, targetOffset);

                // The ratio that we need to multiply while calculating the SV.
                double ratio = startBpmValue / currentBpmValue;

                // Get the closest and exact inherited points (if exists)
                // Closest point cannot be null (it will return the first timing point
                // in worst case), but exact point can be null.
                TimingPoint closestPoint = SearchUtils.GetClosestPoint(actualPoints, targetOffset, true);
                TimingPoint exactPoint = SearchUtils.GetExactInheritedPoint(actualPoints, targetOffset);

                // The copy point that we need to hold the attributes of.
                TimingPoint copy;

                // Determines if the object is already existing in the list.
                bool exists = false;

                // If exact point is not null, we need to edit that.
                if (exactPoint != null)
                {
                    copy = exactPoint;
                    exists = true;
                }
                else
                {
                    copy = new TimingPoint(closestPoint, actualPoints)
                    {
                        // Make sure the copy one is inherited.
                        IsInherited = true
                    };
                }

                // Get the current offset power value. This determines
                // how much the target SV will be as in "startSv + this value".
                targetPowerValue = MathUtils.calculateMultiplierFromPower(svIncreaseMultiplier,
                    firstOffset, lastOffset, targetOffset);

                // Calculate the target SV. Now, the value should be divided by -100 / target SV
                // to achieve the osu! representation of this value.
                // Ratio is the BPM ratio between the start and current offset. Should be 1
                // if the BPMs are the same.
                targetSv = MathUtils.calculateValueFromPercentage(firstSv, lastSv, targetPowerValue) * ratio;
                targetSvValue = -100d / targetSv;

                // At this point, we need to either add the point, or
                // replace the existing one. If the existing one toggles kiai,
                // and the actualTargetOffset is different from this point's offset,
                // we need to add a point and change the kiai one too instead. 
                // Otherwise, we just move the point.
                if (exists)
                {
                    if (SearchUtils.TogglesKiai(actualPoints, copy))
                    {
                        // If there is an offset change (a.k.a actualTargetOffset not
                        // equal to targetOffset) we need to add a point and change
                        // this one as well.
                        if (actualTargetOffset != targetOffset)
                        {
                            TimingPoint copy2 = new TimingPoint(copy, actualPoints)
                            {
                                Offset = actualTargetOffset,
                                IsKiaiOpen = !copy.IsKiaiOpen,
                                PointValue = targetSvValue
                            };
                            copy.PointValue = targetSvValue;
                            actualPoints.Insert(SearchUtils.GetAdditionIndex(actualPoints, copy2), copy2);
                        }
                        else
                        {
                            copy.PointValue = targetSvValue;
                            copy.Offset = actualTargetOffset;
                            SearchUtils.MarkChangeMade(actualPoints);
                        }
                    }
                    else
                    {
                        copy.PointValue = targetSvValue;
                        copy.Offset = actualTargetOffset;
                        SearchUtils.MarkChangeMade(actualPoints);
                    }
                }
                else
                {
                    copy.PointValue = targetSvValue;
                    copy.Offset = actualTargetOffset;
                    actualPoints.Insert(SearchUtils.GetAdditionIndex(actualPoints, copy), copy);
                }

                // At the bottom, calculate the next offset
                // and continue.
                if (putPointsByNotes)
                {
                    if (hitObjectIndex == objects.Count - 1)
                        break;
                    targetOffset = objects[++hitObjectIndex].Offset;
                }
                else
                    targetOffset += gridSnap / currentBpmValue;
            }

            // At the end, force sort the points
            // and return true.
            actualPoints.Sort();
            SearchUtils.MarkSorted(actualPoints);
            return true;
        }

        public static bool SnapInheritedPointsOnClosestTimingPoints(Form form, Beatmap beatmap,
            double firstOffset, double lastOffset)
        {
            SearchUtils.GetObjectsInBetween(beatmap, firstOffset, lastOffset,
                out List<TimingPoint> points);

            Dictionary<TimingPoint, double> newOffsets = new Dictionary<TimingPoint, double>();
            bool isHighRangeDetectedAndVerified = false;
            foreach (TimingPoint point in points)
            {
                if (point.IsInherited)
                {
                    TimingPoint closestPreviousPoint = SearchUtils.GetClosestTimingPoint(beatmap.TimingPoints, point.Offset);
                    TimingPoint closestNextPoint = SearchUtils.GetClosestNextTimingPoint(beatmap.TimingPoints, point);

                    double closestPreviousPointOffset = closestPreviousPoint != null ? closestPreviousPoint.Offset : 0;
                    double closestNextPointOffset = closestNextPoint != null ? closestNextPoint.Offset : 0;

                    double firstDifference = point.Offset - closestPreviousPointOffset;
                    double secondDifference = closestNextPointOffset - point.Offset;

                    if (!isHighRangeDetectedAndVerified && !VerifyUtils.verifyRangeAny(-400, 400, firstDifference, secondDifference))
                    {
                        if (MessageBoxUtils.showQuestionYesNo("Inherited points with more than 400 milliseconds gap between closest timing points detected. This might result in inherited points getting completely losing their purpose.".AddLines(2) + "Are you sure you want to continue?") == DialogResult.Yes)
                            isHighRangeDetectedAndVerified = true;
                        else
                            return false;
                    }

                    double targetOffset;
                    if (point.Offset - closestPreviousPointOffset < closestNextPointOffset - point.Offset)
                        targetOffset = closestPreviousPointOffset;
                    else
                        targetOffset = closestNextPointOffset;
                    newOffsets.Add(point, targetOffset);
                }
            }
            newOffsets.ForEach((key, value) =>
            {
                key.Offset = value;
            });
            newOffsets.Clear();
            return true;
        }

        public static void applyMultiplierToPoint(Dictionary<TimingPoint, double> originalInheritedPointValues, TimingPoint point, double multiplier, bool scaleWithExisting)
        {
            if (point.IsInherited)
            {
                double targetValue;
                if (scaleWithExisting)
                {
                    if (!originalInheritedPointValues.ContainsKey(point))
                        originalInheritedPointValues[point] = point.getDisplayValue();
                    targetValue = originalInheritedPointValues[point];
                }
                else
                    targetValue = 1;
                double pointValue = targetValue * multiplier;
                point.PointValue = -100d / pointValue;
            }
            else
                throw new ArgumentException("Passed parameter was not an inherited point: " + point.GetSaveFormat());
        }

        private static void showErrorMessageInMainThread(Form form, string message)
        {
            form.Invoke(new Action(() =>
            {
                MessageBoxUtils.showError(message);
            }));
        }
    }
}
