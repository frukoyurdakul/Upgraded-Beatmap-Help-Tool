using Beatmap_Help_Tool.BeatmapModel;
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
        public static bool AddSvChanges(Form form, Beatmap beatmap, int firstOffset, int lastOffset, 
            double firstSv, double lastSv, double targetBpm, 
            double gridSnap, int svOffset, int svIncreaseMode, int count, 
            double svIncreaseMultiplier, bool putPointsByNotes)
        {
            List<TimingPoint> actualPoints = beatmap.TimingPoints;
            List<HitObject> actualObjects = beatmap.HitObjects;

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
                points.Add(SearchUtils.GetClosestTimingPoint(actualPoints, firstOffset));

            // After this, if we still don't have a timing point, we cannot proceed.
            // A map has to contain at least one timing point before the declared offset.
            if (!SearchUtils.ContainsTimingPoint(points))
            {
                showErrorMessageInMainThread(form, "No timing points found to take reference from. Aborting.");
                return false;
            }

            // This is the BPM value, not the BPM itself, a.k.a it is the osu! representation
            // of a BPM value. For instance, this is 1000 if BPM is 120 in the map, where
            // a beat is in a total second.
            double currentBpmValue = SearchUtils.GetBpmValueInOffset(points, firstOffset);

            // Get the SV difference.
            double difference = lastSv - firstSv;
        
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

            // If the last offset is not defined,
            // calculate it with the grid snap value and count value.
            if (lastOffset < 0)
            {
                if (gridSnap == 0 || count == 0)
                {
                    showErrorMessageInMainThread(form, "End offset was not defined and grid snap and count values are\n also not defined, hence the end offset\ncould not be calculated. Aborting.");
                    return false;                        
                }
                lastOffset = SnapUtils.calculateEndOffset(beatmap, firstOffset, gridSnap, count);
            }

            // Now that we have set already in place, let's calculate the SVs and
            // add or edit them.

            // The temp value to use on additional SVs.
            double targetSv;

            // The current power that will result
            // in the final SV of the point. This should be
            // always 1 if the change is linear.
            double targetPower;

            return true;
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
