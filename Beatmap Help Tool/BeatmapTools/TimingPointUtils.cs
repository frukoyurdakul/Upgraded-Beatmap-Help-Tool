using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Beatmap_Help_Tool.Utils.HtmlUtils;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class TimingPointUtils
    {
        public static void addInconsistentTimingPoints(HtmlDisplayer htmlDisplayer, Dictionary<Beatmap, List<TimingPoint>> timingPointsPerBeatmap)
        {
            htmlDisplayer.addSection("Uninherited timing point inconsistencies detected.");
            KeyValuePair<Beatmap, List<TimingPoint>> previousPair = default(KeyValuePair<Beatmap, List<TimingPoint>>);
            foreach (KeyValuePair<Beatmap, List<TimingPoint>> pair in timingPointsPerBeatmap)
            {
                if (!previousPair.Equals(default(KeyValuePair<Beatmap, List<TimingPoint>>)))
                {
                    List<TimingPoint> first = previousPair.Value;
                    List<TimingPoint> second = pair.Value;

                    if (first.Count == second.Count)
                    {
                        for (int i = 0; i < first.Count; i++)
                        {
                            TimingPoint firstPoint = first[i];
                            TimingPoint secondPoint = second[i];
                            checkAndPrintContent(htmlDisplayer, previousPair, pair, firstPoint, secondPoint);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < first.Count; i++)
                        {
                            TimingPoint firstPoint = first[i];
                            bool isEqualOffsetPointFound = false;
                            for (int j = 0; j < second.Count; j++)
                            {
                                TimingPoint secondPoint = second[j];
                                if (secondPoint.Offset == firstPoint.Offset)
                                {
                                    isEqualOffsetPointFound = true;
                                    checkAndPrintContent(htmlDisplayer, previousPair, pair, firstPoint, secondPoint);
                                    break;
                                }
                            }
                            if (!isEqualOffsetPointFound)
                            {
                                htmlDisplayer.addWarning("Timing point with offset " + firstPoint.getDisplayOffset() + " not found in difficulty: " + pair.Key.DifficultyName);
                                htmlDisplayer.addLineBreak();
                            }
                        }
                    }
                }

                previousPair = pair;
            }
        }

        public static void changeBpmOfTimingPoint(Beatmap beatmap, HtmlDisplayer htmlDisplayer,
                                                  double offset, double newValue, bool shiftRestOfBeatmap,
                                                  bool saveBackups, string customPath)
        {
            if (saveBackups)
                beatmap.save(customPath + "//" + beatmap.FileName);

            // We need to extract the objects between the selected offset and the next timing point.
            List<TimingPoint> originalPoints = beatmap.TimingPoints;

            // Get exact and next timing points. Exact timing point cannot be null. If it is null, throw
            // a message and bail out.
            TimingPoint sourcePoint = SearchUtils.GetExactTimingPoint(originalPoints, offset);
            TimingPoint nextPoint = SearchUtils.GetClosestNextTimingPoint(originalPoints, sourcePoint);

            if (sourcePoint == null)
            {
                if (!htmlDisplayer.containsSections())
                    htmlDisplayer.addSection("Starting uninherited timing point not found in one or more difficulties.");
                else
                    htmlDisplayer.addLineBreak();
                htmlDisplayer.addSubsection("Beatmap difficulty: " + beatmap.DifficultyName);
                htmlDisplayer.addWarning(StringUtils.GetOffsetWithLink(offset) + " does not exist.");
                return;
            }

            // Get the objects we require.
            SearchUtils.GetObjectsInBetween(beatmap, offset, nextPoint != null ? nextPoint.Offset : double.MaxValue,
                out List<Bookmark> bookmarks, out List<TimingPoint> timingPoints, out List<HitObject> hitObjects);

            // Since the snaps are already calculated, it should be easy to calculate the next offsets
            // after changing the BPM.
        }

        private static void checkAndPrintContent(HtmlDisplayer htmlDisplayer, KeyValuePair<Beatmap, List<TimingPoint>> previousPair, KeyValuePair<Beatmap, List<TimingPoint>> pair, TimingPoint firstPoint, TimingPoint secondPoint)
        {
            if (!firstPoint.ContentEquals(secondPoint))
            {
                htmlDisplayer.addLine("Point at " + firstPoint.GetOffsetWithLink() + ":");
                htmlDisplayer.addLineWithBreak("Difficulty: " + previousPair.Key.DifficultyName);

                htmlDisplayer.addLine("Offset: " + firstPoint.getDisplayOffset(), firstPoint.Offset != secondPoint.Offset);
                htmlDisplayer.addLine("Point value: " + firstPoint.getDisplayValueString(), firstPoint.PointValue != secondPoint.PointValue);
                htmlDisplayer.addLine("Measure: " + firstPoint.getDisplayMeter(), firstPoint.Meter != secondPoint.Meter);
                htmlDisplayer.addLine("Inherited: " + firstPoint.IsInherited, firstPoint.IsInherited != secondPoint.IsInherited);

                htmlDisplayer.addLineBreak();

                htmlDisplayer.addLine("Difficulty: " + pair.Key.DifficultyName);
                htmlDisplayer.addLineWithBreak("Point at " + secondPoint.GetOffsetWithLink() + ":");

                htmlDisplayer.addLine("Offset: " + secondPoint.getDisplayOffset(), firstPoint.Offset != secondPoint.Offset);
                htmlDisplayer.addLine("Point value: " + secondPoint.getDisplayValueString(), firstPoint.PointValue != secondPoint.PointValue);
                htmlDisplayer.addLine("Measure: " + secondPoint.getDisplayMeter(), firstPoint.Meter != secondPoint.Meter);
                htmlDisplayer.addLine("Inherited: " + secondPoint.IsInherited, firstPoint.IsInherited != secondPoint.IsInherited);

                htmlDisplayer.addLineBreak();
            }
        }
    }
}
