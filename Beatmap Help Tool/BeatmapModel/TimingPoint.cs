using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using static Beatmap_Help_Tool.Utils.HtmlUtils;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public class TimingPoint : BeatmapElement, IComparable
    {
        private const int KIAI_BIT = 1;
        private const int OMIT_BIT = 8;

        // This can be either BPM or SV depending on
        // it's an actual point or inherited.
        public double PointValue { get; set; }

        public int Meter { get; set; }
        public int SampleSet { get; set; }
        public int SampleIndex { get; set; }
        public int Volume { get; set; }
        public bool IsInherited { get; set; }
        public bool IsKiaiOpen { get; set; }
        public bool IsOmitted { get; set; }

        public TimingPoint(TimingPoint from) : base(from.beatmap)
        {
            PointValue = from.PointValue;
            Meter = from.Meter;
            SampleSet = from.SampleSet;
            SampleIndex = from.SampleIndex;
            Volume = from.Volume;
            IsInherited = from.IsInherited;
            IsKiaiOpen = from.IsKiaiOpen;
            offset = from.offset;
            requiresSnapDetection = from.requiresSnapDetection;
            snap = from.snap;
        }

        public TimingPoint(Beatmap beatmap, double offset, double pointValue, int meter, int sampleSet, 
            int sampleIndex, int volume, bool isInherited, bool isKiaiOpen, bool isOmitted) : base(beatmap)
        {
            PointValue = pointValue;
            Meter = meter;
            SampleSet = sampleSet;
            SampleIndex = sampleIndex;
            Volume = volume;
            IsInherited = isInherited;
            IsKiaiOpen = isKiaiOpen;
            IsOmitted = isOmitted;

            // Offset is set last because the variables up above are used internally on the setter.
            Offset = offset;
        }

        public void setTo(TimingPoint from)
        {
            PointValue = from.PointValue;
            Meter = from.Meter;
            SampleSet = from.SampleSet;
            SampleIndex = from.SampleIndex;
            Volume = from.Volume;
            IsInherited = from.IsInherited;
            IsKiaiOpen = from.IsKiaiOpen;
            offset = from.offset;
            requiresSnapDetection = from.requiresSnapDetection;
            snap = from.snap;
        }

        public static TimingPoint ParseLine(Beatmap beatmap, string line)
        {
            string[] splitted = line.Trim().Split(',');
            if (splitted.Length == 8)
            {
                double pointValue = Convert.ToDouble(splitted[1]);
                int kiaiValue = Convert.ToInt32(splitted[7]);
                return new TimingPoint(beatmap, Convert.ToDouble(splitted[0]),
                    pointValue, Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[3]),
                    Convert.ToInt32(splitted[4]), Convert.ToInt32(splitted[5]), splitted[6] == "0",
                    ((kiaiValue & KIAI_BIT) == KIAI_BIT), ((kiaiValue & OMIT_BIT) == OMIT_BIT));
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

        public string getDisplayValueString()
        {
            if (IsInherited)
                return (-100d / PointValue).ToString("0.000") + "X";
            else
                return (60000 / PointValue).ToString();
        }

        public double getDisplayValue()
        {
            if (IsInherited)
            {
                decimal val = -100M / (decimal)PointValue;
                return (double)val;
            }
            else
            {
                decimal val = 60000M / (decimal)PointValue;
                return (double)val;
            }
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

        public bool ContentEquals(TimingPoint other)
        {
            if (other == null)
                return false;

            return offset == other.offset
                && PointValue == other.PointValue
                && Meter == other.Meter
                && IsInherited == other.IsInherited;
        }

        public override string GetSaveFormat()
        {
            int effects = IsKiaiOpen ? 0x00000001 : 0x00000000;
            effects |= IsOmitted ? 0x00000008 : 0x00000000;
            return string.Join(",", Offset, PointValue, Meter, SampleSet, SampleIndex, 
                Volume, (IsInherited ? 0 : 1).ToString(), effects.ToString());
        }

        public override int GetTypeInt()
        {
            return IsInherited ? 29 * 2 : 29 * 3;
        }
    }
}
