using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class SnapTools
    {
        private const double BEAT_SNAP_DIVISOR = 48;

        public static double getRelativeSnap(List<TimingPoint> timingPoints, IOffset target)
        {
            List<TimingPoint> redPoints = new List<TimingPoint>();
            List<TimingPoint> excludedRedPoints = new List<TimingPoint>();

            // Extract all inherited points since they don't mean anything
            // while we are searching for the snap value.
            foreach (TimingPoint point in timingPoints)
                if (!point.IsInherited)
                {
                    redPoints.Add(point);
                    if (target != point)
                        excludedRedPoints.Add(point);
                }

            // Now, the BEAT_SNAP_DIVISOR divides a beat in 48 
            // equal snaps, which covers both 1/12 and 1/16 snaps.
            // Depending on the division, if the division is integer,
            // or if the division difference is below 0.1 or the actual snap
            // we target for is off smaller than 1 milliseconds which can be
            // caused by rounding errors in the end,
            // we can consider that note as snapped.
            if (redPoints.Count >= 1 && redPoints[0].GetOffset() == target.GetOffset())
                return 0;

            TimingPoint closestTimingPoint = SearchTools.GetClosestTimingPoint(excludedRedPoints, target.GetOffset());
            int zeroSnapPointIndex = SearchTools.GetClosestZeroSnapPointIndex(excludedRedPoints);
            if (closestTimingPoint != null)
            {
                TimingPoint timingPoint1, timingPoint2;
                IOffset itemInternal;
                double beatDuration;
                double finalSnap = 0d;
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
                    if (checkedActualTarget)
                        break;
                }

                // If we did not check the actual target here, that is because the 
                // object we seek is after the last timing point, which only requires
                // the computation of the final snap adding to the target and last point.
                if (!checkedActualTarget  && redPoints[redPointsCount - 1] == closestTimingPoint)
                {
                    TimingPoint point = redPoints[redPointsCount - 1];
                    finalSnap += getSnapInBetween(point, target, point.PointValue);
                }
                return finalSnap;
            }
            else
            {
                MessageBoxUtils.showError("Error while determining snap value for the object that is instance of " +
                    target.GetType() + "with the offset at " + target.GetOffset().ToString());
                return -1;
            }
        }

        private static double getSnapInBetween(IOffset target1, IOffset target2, double beatDuration)
        {
            double offsetDifference = target2.GetOffset() - target1.GetOffset();
            double snap = (offsetDifference / beatDuration) * BEAT_SNAP_DIVISOR;
            int snapInt = Convert.ToInt32(snap);
            double fluctuation = Math.Abs(snap - snapInt);
            double targetOffset = target1.GetOffset() + (snapInt / BEAT_SNAP_DIVISOR * beatDuration);
            if (fluctuation < 0.1d || Math.Abs(target2.GetOffset() - targetOffset) < 1)
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
                    "and offset " + target1.GetOffset());
                return -1;
            }
        }
    }
}
