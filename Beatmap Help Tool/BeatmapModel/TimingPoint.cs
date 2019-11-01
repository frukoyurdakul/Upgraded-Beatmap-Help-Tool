using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public class TimingPoint
    {
        private static readonly NumberFormatInfo numberFormatInfo = new NumberFormatInfo();

        static TimingPoint()
        {
            numberFormatInfo.NumberDecimalSeparator = ".";
        }

        public int Offset { get; set; }

        // This can be either BPM or SV depending on
        // it's an actual point or inherited.
        public double PointValue { get; set; }

        public int Meter { get; set; }
        public int SampleSet { get; set; }
        public int SampleIndex { get; set; }
        public int Volume { get; set; }
        public bool IsInherited { get; set; }
        public bool IsKiaiOpen { get; set; }

        private TimingPoint(int offset, double pointValue, int meter, int sampleSet, 
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

        public static TimingPoint ParseLine(string line)
        {
            string[] splitted = line.Trim().Split(',');
            if (splitted.Length == 8)
            {
                bool isInherited = splitted[6] == "1";
                double pointValue = isInherited ? (-100d / Convert.ToDouble(splitted[1])) :
                    (60000d / Convert.ToDouble(splitted[1]));
                return new TimingPoint(Convert.ToInt32(splitted[0]),
                    pointValue, Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[3]),
                    Convert.ToInt32(splitted[4]), Convert.ToInt32(splitted[5]), isInherited,
                    Convert.ToBoolean(splitted[7]));
            }
            else
                throw new ArgumentException("A timing object always has to have 8 fields. Input: " + line.Trim());
        }
    }
}
