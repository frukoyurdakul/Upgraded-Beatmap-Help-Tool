using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public abstract class HitObject : BeatmapElement, IComparable
    {
        private const int CIRCLE = 1;
        private const int SLIDER = 2;
        private const int SPINNER = 8;
        private const int MANIA_NOTE = 128;

        private double duration;

        public int X { get; set; }
        public int Y { get; set; }
        public int Type { get; set; }
        public int Hitsound { get; set; }
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

        public HitObject(List<TimingPoint> points) : base(points) { }

        public static HitObject ParseLine(Beatmap beatmap, string line)
        {
            string[] elements = line.Trim().Split(',');
            int type = Convert.ToInt32(elements[3]);

            // If elements have 5 size, it is a normal hit object. Parse it as required.
            if ((type & CIRCLE) != 0)
            {
                // Do a type check first.
                if (elements.Length == 6)
                    return new HitCircle(beatmap.TimingPoints, Convert.ToInt32(elements[0]),
                        Convert.ToInt32(elements[1]), Convert.ToDouble(elements[2]), 0d,
                        Convert.ToInt32(elements[3]), Convert.ToInt32(elements[4]),
                        elements[5]);
                else
                {
                    MessageBoxUtils.showError("Type and element information does not match as a circle for line: " +
                            line + ", aborting note processing.");
                    return null;
                }
            }
            // If elements have the size of 11, it is a slider. Parse it as required.
            else if ((type & SLIDER) != 0)
            {
                // Always check the type first.
                if (elements.Length < 8)
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
                        string[] hitsoundStrings = elements.Length >= 9 ? elements[8].Split('|') : new string[0];
                        string extras = (elements.Length >= 10 ? elements[9] : "") + 
                            (elements.Length >= 11 ? "," + elements[10] : "");
                        List<int> edgeHitsounds = new List<int>();
                        for (int i = 0; i < hitsoundStrings.Length; i++)
                            edgeHitsounds.Add(Convert.ToInt32(hitsoundStrings[i]));
                        return new HitSlider(beatmap.TimingPoints, Convert.ToInt32(elements[0]),
                            Convert.ToInt32(elements[1]), Convert.ToDouble(elements[2]),
                            duration, type, Convert.ToInt32(elements[4]),
                            string.Join(",", elements[5], elements[6], elements[7]),
                            edgeHitsounds, extras);
                    }
                    else
                    {
                        MessageBoxUtils.showError("Slider cannot be parsed, there was no relative timing point found at offset " + 
                            offset.ToString() + ", aborting note processing.");
                        return null;
                    }
                }
            }
            else if ((type & SPINNER) != 0)
            {
                if (elements.Length == 7)
                {
                    double duration = Convert.ToDouble(elements[5]) - Convert.ToDouble(elements[2]);
                    return new HitSpinner(beatmap.TimingPoints, Convert.ToInt32(elements[0]), Convert.ToInt32(elements[1]),
                        Convert.ToDouble(elements[2]), duration, type, Convert.ToInt32(elements[4]),
                        elements[5]);
                }
                else
                {
                    MessageBoxUtils.showError("Type and element information does not match as a spinner for line: " +
                            line + ", aborting note processing.");
                    return null;
                }
            }
            else if ((type & MANIA_NOTE) != 0)
            {
                MessageBoxUtils.showError("Mania is not supported yet.");
                return null;
            }
            else
            {
                MessageBoxUtils.showError("Unsupported element data found for line: " + line + ", aborting note " +
                    "processing.");
                return null;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is HitObject)
            {
                HitObject hitObject = obj as HitObject;
                return (int) Offset - (int) hitObject.Offset;
            }
            else
            {
                throw new ArgumentException("Object is not a HitObject.");
            }
        }
    }

    class HitCircle : HitObject
    {
        public HitCircle(List<TimingPoint> points,
            int x, int y, double offset, double duration, int type, int hitsound, string extras) : base(points)
        {
            X = x;
            Y = y;
            Offset = offset;
            Duration = duration;
            Type = type;
            Hitsound = hitsound;
            Extras = extras;
        }

        override public string GetAsLine()
        {
            return string.Join(",", X, Y, Offset, Type, Hitsound, Extras);
        }
    }

    class HitSlider : HitObject
    {
        private readonly string SliderInfo;
        private readonly List<int> EdgeHitsounds;

        public HitSlider(List<TimingPoint> points, int x, int y, double offset, double duration, int type, int hitsound, 
            string sliderInfo, List<int> edgeHitsounds, string extras) : base(points)
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

        override public string GetAsLine()
        {
            return string.Join(",", X, Y, Offset, Type, Hitsound, SliderInfo, string.Join("|", EdgeHitsounds), Extras);
        }
    }

    class HitSpinner : HitObject
    { 
        public HitSpinner(List<TimingPoint> points, int x, int y, double offset, double duration, 
            int type, int hitsound, string extras) : base(points)
        {
            X = x;
            Y = y;
            Offset = offset;
            Duration = duration;
            Type = type;
            Hitsound = hitsound;
            Extras = extras;
        }

        override public string GetAsLine()
        {
            return string.Join(",", X, Y, Offset, Type, Hitsound, (Offset + Duration), Extras);
        }
    }
}
