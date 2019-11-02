using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public class TimingPoint : IComparable, IOffset
    {
        private double offset = 0, snap = 0;
        private bool requiresSnapDetection = true;
        private List<TimingPoint> timingPoints;

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

        // This can be either BPM or SV depending on
        // it's an actual point or inherited.
        public double PointValue { get; set; }

        public int Meter { get; set; }
        public int SampleSet { get; set; }
        public int SampleIndex { get; set; }
        public int Volume { get; set; }
        public bool IsInherited { get; set; }
        public bool IsKiaiOpen { get; set; }

        public TimingPoint(TimingPoint from)
        {
            Offset = from.Offset;
            PointValue = from.PointValue;
            Meter = from.Meter;
            SampleSet = from.SampleSet;
            SampleIndex = from.SampleIndex;
            Volume = from.Volume;
            IsInherited = from.IsInherited;
            IsKiaiOpen = from.IsKiaiOpen;
            timingPoints = from.timingPoints;
            offset = from.offset;
            requiresSnapDetection = from.requiresSnapDetection;
            snap = from.snap;
        }

        public TimingPoint(double offset, double pointValue, int meter, int sampleSet, 
            int sampleIndex, int volume, bool isInherited, bool isKiaiOpen)
        {
            Offset = offset;
            PointValue = pointValue;
            Meter = meter;
            SampleSet = sampleSet;
            SampleIndex = sampleIndex;
            Volume = volume;
            IsInherited = isInherited;
            IsKiaiOpen = isKiaiOpen;
        }

        private TimingPoint SetTimingPointsList(List<TimingPoint> points)
        {
            timingPoints = points;
            return this;
        }

        public static TimingPoint ParseLine(List<TimingPoint> points, string line)
        {
            string[] splitted = line.Trim().Split(',');
            if (splitted.Length == 8)
            {
                double pointValue = Convert.ToDouble(splitted[1]);
                return new TimingPoint(Convert.ToDouble(splitted[0]),
                    pointValue, Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[3]),
                    Convert.ToInt32(splitted[4]), Convert.ToInt32(splitted[5]), splitted[6] == "0",
                    splitted[7] == "0").SetTimingPointsList(points);
            }
            else
            {
                MessageBoxUtils.showError("A timing object always has to have 8 fields. Input: " + line.Trim());
                return null;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is TimingPoint)
            {
                TimingPoint point = obj as TimingPoint;
                if (point.Offset == Offset)
                    return IsInherited ? 0 : 1;
                else
                    return Convert.ToInt32(Offset - point.Offset);
            }
            else
            {
                throw new ArgumentException("Object is not a TimingPoint.");
            }
        }

        public double GetOffset()
        {
            return Offset;
        }

        public void SetOffset(double offset)
        {
            Offset = offset;
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
    }
}
