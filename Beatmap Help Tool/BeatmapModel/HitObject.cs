using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public abstract class HitObject : IOffset
    {
        private const int CIRCLE = 1;
        private const int SLIDER = 2;
        private const int SPINNER = 8;
        private const int MANIA_NOTE = 128;

        private double duration;
        private List<TimingPoint> timingPoints;
        private double offset;
        private double snap = 0;
        private bool requiresSnapDetection = true;

        public int X { get; set; }
        public int Y { get; set; }
        public int Type { get; set; }
        public int Hitsound { get; set; }
        public double Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
                requiresSnapDetection = true;
            }
        }
        public double Duration
        {
            get
            {
                if (this is HitCircle)
                    return 0;

                return duration;
            }

            set
            {
                if (!(this is HitCircle))
                    duration = value;
            }
        }
        public string Extras { get; set; }

        public double GetOffset()
        {
            return Offset;
        }

        public void SetOffset(double offset)
        {
            Offset = offset;
        }

        private HitObject SetTimingPoints(List<TimingPoint> points)
        {
            timingPoints = points;
            return this;
        }

        public double GetSnap()
        {
            if (requiresSnapDetection)
            {
                snap = SnapTools.getRelativeSnap(timingPoints, this);
                requiresSnapDetection = false;
            }
            return snap;
        }

        public static HitObject ParseLine(Beatmap beatmap, string line)
        {
            string[] elements = line.Trim().Split(',');
            int type = Convert.ToInt32(elements[3]);

            // If elements have 5 size, it is a normal hit object. Parse it as required.
            if (elements.Length == 6)
            {
                // Do a type check first.
                if ((type & CIRCLE) != 0)
                    return new HitCircle(Convert.ToInt32(elements[0]),
                        Convert.ToInt32(elements[1]), Convert.ToDouble(elements[2]), 0d,
                        Convert.ToInt32(elements[3]), Convert.ToInt32(elements[4]),
                        elements[5]).SetTimingPoints(beatmap.TimingPoints);
                else
                {
                    if ((type & MANIA_NOTE) != 0)
                    {
                        MessageBoxUtils.showError("Mania is not supported yet.");
                        return null;
                    }
                    else
                    {
                        MessageBoxUtils.showError("Type and element information does not match as a circle for line: " +
                            line + ", aborting note processing.");
                        return null;
                    }
                }
            }
            // If elements have the size of 11, it is a slider. Parse it as required.
            else if (elements.Length == 11)
            {
                // Always check the type first.
                if ((type & SLIDER) != 0)
                {
                    MessageBoxUtils.showError("Type and element information does not match as a slider for line: " +
                        line + ", aborting note processing.");
                    return null;
                }
                else
                {
                    // This requires some additional processing.
                    double offset = Convert.ToDouble(elements[2]);
                    TimingPoint point = SearchTools.GetClosestTimingPoint(beatmap.TimingPoints, offset);
                    if (point != null)
                    {
                        // We found the timing point, now it is time to construct the object.
                        double pixelLength = Convert.ToDouble(elements[7]);
                        double duration = pixelLength / (100.0 * beatmap.SliderMultiplier) * point.PointValue;
                        string[] hitsoundStrings = elements[8].Split('|');
                        List<int> edgeHitsounds = new List<int>();
                        foreach (string hitsound in hitsoundStrings)
                            edgeHitsounds.Add(Convert.ToInt32(hitsound));
                        return new HitSlider(Convert.ToInt32(elements[0]),
                            Convert.ToInt32(elements[1]), Convert.ToDouble(elements[2]),
                            duration, type, Convert.ToInt32(elements[4]),
                            elements[5], edgeHitsounds, elements[9] + "," + elements[10])
                            .SetTimingPoints(beatmap.TimingPoints);
                    }
                    else
                    {
                        MessageBoxUtils.showError("Slider cannot be parsed, there was no relative timing point found at offset " + 
                            offset.ToString() + ", aborting note processing.");
                        return null;
                    }
                }
            }
            else if (elements.Length == 7)
            {
                if ((type & SPINNER) != 0)
                {
                    double duration = Convert.ToDouble(elements[5]) - Convert.ToDouble(elements[2]);
                    return new HitSpinner(Convert.ToInt32(elements[0]), Convert.ToInt32(elements[1]),
                        Convert.ToDouble(elements[2]), duration, type, Convert.ToInt32(elements[4]),
                        elements[5])
                        .SetTimingPoints(beatmap.TimingPoints);
                }
                else
                {
                    MessageBoxUtils.showError("Type and element information does not match as a spinner for line: " +
                            line + ", aborting note processing.");
                    return null;
                }
            }
            else
            {
                MessageBoxUtils.showError("Unsupported element data found for line: " + line + ", aborting note " +
                    "processing.");
                return null;
            }
        }
    }

    class HitCircle : HitObject
    {
        public HitCircle(int x, int y, double offset, double duration, int type, int hitsound, string extras)
        {
            X = x;
            Y = y;
            Offset = offset;
            Duration = duration;
            Type = type;
            Hitsound = hitsound;
            Extras = extras;
        }
    }

    class HitSlider : HitObject
    {
        private string SliderInfo;
        private List<int> EdgeHitsounds;

        public HitSlider(int x, int y, double offset, double duration, int type, int hitsound, 
            string sliderInfo, List<int> edgeHitsounds, string extras)
        {
            X = x;
            Y = y;
            Offset = offset;
            Duration = duration;
            Type = type;
            Hitsound = hitsound;
            SliderInfo = sliderInfo;
            EdgeHitsounds = edgeHitsounds;
            // Extras here also includes sample set overrides of the slider (edgeAttributions).
            Extras = extras;
        }
    }

    class HitSpinner : HitObject
    { 
        public HitSpinner(int x, int y, double offset, double duration, int type, int hitsound, string extras)
        {
            X = x;
            Y = y;
            Offset = offset;
            Duration = duration;
            Type = type;
            Hitsound = hitsound;
            Extras = extras;
        }
    }
}
