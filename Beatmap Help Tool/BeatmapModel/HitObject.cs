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

            // If elements have 5 size, it is a normal hit object. Parse it as required.
            if (elements.Length == 6)
            {
                return new HitCircle(Convert.ToInt32(elements[0]),
                    Convert.ToInt32(elements[1]), Convert.ToDouble(elements[2]), 0d,
                    Convert.ToInt32(elements[3]), Convert.ToInt32(elements[4]),
                    elements[5]);
            }
            // If elements have the size of 11, it is a slider. Parse it as required.
            else if (elements.Length == 11)
            {
                // This requires some additional processing.
                double offset = Convert.ToDouble(elements[2]);
                TimingPoint point = SearchTools.GetClosestActiveInheritedPoint(beatmap.TimingPoints, offset);
                if (point != null)
                {
                    // We found the timing point, 
                }
                else
                {
                    MessageBoxUtils.showError("Slider cannot be parsed, there was no relative timing point found.");
                    return null;
                }
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

        HitSlider(int x, int y, double offset, double duration, int type, int hitsound, 
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

        HitSpinner(int x, int y, double offset, double duration, int type, int hitsound, string extras)
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
