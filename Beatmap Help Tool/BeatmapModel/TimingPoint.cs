using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public class TimingPoint : BeatmapElement, IComparable
    {
        private double offset = 0, snap = 0, closestSnap = 0;
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

        public TimingPoint(TimingPoint from) : base(from.timingPoints)
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

        public TimingPoint(List<TimingPoint> points, double offset, double pointValue, int meter, int sampleSet, 
            int sampleIndex, int volume, bool isInherited, bool isKiaiOpen) : base(points)
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

        public static TimingPoint ParseLine(List<TimingPoint> points, string line)
        {
            string[] splitted = line.Trim().Split(',');
            if (splitted.Length == 8)
            {
                double pointValue = Convert.ToDouble(splitted[1]);
                return new TimingPoint(points, Convert.ToDouble(splitted[0]),
                    pointValue, Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[3]),
                    Convert.ToInt32(splitted[4]), Convert.ToInt32(splitted[5]), splitted[6] == "0",
                    splitted[7] == "0");
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

        public override string ToString()
        {
            return GetAsLine();
        }

        public string getDisplayValue()
        {
            if (IsInherited)
                return (-100d / PointValue).ToString("0.000") + "X";
            else
                return (60000 / PointValue).ToString();
        }

        public string getDisplayOffset()
        {
            TimeSpan t = TimeSpan.FromMilliseconds(Offset);
            if (t.Hours > 0)
            {
                return Offset.ToString() + " (" + string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds) + ")";
            }
            else
            {
                return Offset.ToString() + " (" + string.Format("{0:D2}:{1:D2}:{2:D3}",
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds) + ")";
            }
        }

        public string getDisplayMeter()
        {
            return Meter + "/4";
        }

        public string getDisplayVolume()
        {
            return Volume + "%";
        }

        public bool getDisplayKiai()
        {
            return IsKiaiOpen;
        }

        public override string GetAsLine()
        {
            return string.Join(",", Offset, PointValue, Meter, SampleSet, SampleIndex, 
                Volume, (IsInherited ? 0 : 1).ToString(), (IsKiaiOpen ? 1 : 0).ToString());
        }
    }
}
