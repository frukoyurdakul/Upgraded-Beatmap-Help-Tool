using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public abstract class IHitObject
    {
        private const int CIRCLE = 1;
        private const int SLIDER = 2;
        private const int SPINNER = 8;
        private const int MANIA_NOTE = 128;
        
        public abstract int GetX();
        public abstract int GetY();
        public abstract double GetOffset();
        public abstract double GetDuration();
        public abstract int GetHitsound();
        public abstract int GetHitType();
        public abstract void SetDuration(double duration);
        public abstract void SetX(int x);
        public abstract void SetY(int y);
        public abstract void SetOffset(double offset);
        public abstract void SetHitsound(int hitsound);

        static IHitObject ParseLine(Beatmap beatmap, string line)
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
                        elements[5]);
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
                            elements[5], edgeHitsounds, elements[9] + "," + elements[10]);
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
                        elements[5]);
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

    class HitCircle : IHitObject
    {
        private int X, Y, Type, Hitsound;
        private double Offset, Duration;
        private string Extras;

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

        override public double GetDuration()
        {
            return Duration;
        }

        override public int GetHitsound()
        {
            return Hitsound;
        }

        override public double GetOffset()
        {
            return Offset;
        }

        override public int GetX()
        {
            return X;
        }

        override public int GetY()
        {
            return Y;
        }

        override public int GetHitType()
        {
            return Type;
        }

        override public void SetDuration(double duration)
        {
            // Hit circle does not have a duration.
        }

        override public void SetX(int x)
        {
            X = x;
        }

        override public void SetY(int y)
        {
            Y = y;
        }

        override public void SetOffset(double offset)
        {
            Offset = offset;
        }

        override public void SetHitsound(int hitsound)
        {
            Hitsound = hitsound;
        }
    }

    class HitSlider : IHitObject
    {
        private int X, Y, Type, Hitsound;
        private double Offset, Duration;
        private string SliderInfo, Extras;
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

        override public double GetDuration()
        {
            return Duration;
        }

        override public int GetHitsound()
        {
            return Hitsound;
        }

        override public double GetOffset()
        {
            return Offset;
        }

        override public int GetX()
        {
            return X;
        }

        override public int GetY()
        {
            return Y;
        }

        override public int GetHitType()
        {
            return Type;
        }

        override public void SetDuration(double duration)
        {
            // TODO Fill this up
            throw new NotImplementedException();
        }

        override public void SetX(int x)
        {
            X = x;
        }

        override public void SetY(int y)
        {
            Y = y;
        }

        override public void SetOffset(double offset)
        {
            Offset = offset;
        }

        override public void SetHitsound(int hitsound)
        {
            Hitsound = hitsound;
        }
    }

    class HitSpinner : IHitObject
    {
        private int X, Y, Type, Hitsound;
        private double Offset, Duration;
        private string Extras;

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

        override public double GetDuration()
        {
            return Duration;
        }

        override public int GetHitsound()
        {
            return Hitsound;
        }

        override public double GetOffset()
        {
            return Offset;
        }

        override public int GetX()
        {
            return X;
        }

        override public int GetY()
        {
            return Y;
        }

        override public int GetHitType()
        {
            return Type;
        }

        override public void SetDuration(double duration)
        {

        }

        override public void SetX(int x)
        {
            X = x;
        }

        override public void SetY(int y)
        {
            Y = y;
        }

        override public void SetOffset(double offset)
        {
            Offset = offset;
        }

        override public void SetHitsound(int hitsound)
        {
            Hitsound = hitsound;
        }
    }
}
