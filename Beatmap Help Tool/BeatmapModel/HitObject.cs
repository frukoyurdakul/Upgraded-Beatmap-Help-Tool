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
        public abstract int GetOffset();
        public abstract int GetDuration();
        public abstract int GetHitsound();
        public abstract int GetHitType();
        public abstract void SetDuration(int duration);
        public abstract void SetX(int x);
        public abstract void SetY(int y);
        public abstract void SetOffset(int offset);
        public abstract void SetHitsound(int hitsound);

        static void ParseLine(string line)
        {

        }
    }

    class HitCircle : IHitObject
    {
        private int X, Y, Offset, Duration, Type, Hitsound;

        HitCircle()
        {

        }

        override public int GetDuration()
        {
            return Duration;
        }

        override public int GetHitsound()
        {
            return Hitsound;
        }

        override public int GetOffset()
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

        override public void SetDuration(int duration)
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

        override public void SetOffset(int offset)
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
        private int X, Y, Offset, Duration, Type, Hitsound;

        HitSlider()
        {

        }

        override public int GetDuration()
        {
            return Duration;
        }

        override public int GetHitsound()
        {
            return Hitsound;
        }

        override public int GetOffset()
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

        override public void SetDuration(int duration)
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

        override public void SetOffset(int offset)
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
        private int X, Y, Offset, Duration, Type, Hitsound;

        HitSpinner()
        {

        }

        override public int GetDuration()
        {
            return Duration;
        }

        override public int GetHitsound()
        {
            return Hitsound;
        }

        override public int GetOffset()
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

        override public void SetDuration(int duration)
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

        override public void SetOffset(int offset)
        {
            Offset = offset;
        }

        override public void SetHitsound(int hitsound)
        {
            Hitsound = hitsound;
        }
    }
}
