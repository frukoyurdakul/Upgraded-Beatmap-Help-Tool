using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public static class MathUtils
    {
        public static double calculateMultiplierFromPower(double power, double start, double end, double value)
        {
            return Math.Pow(calculatePercentage(start, end, value), power);
        }

        // This returns a value in between 0 and 1.
        public static double calculatePercentage(double start, double end, double value)
        {
            // Prevent divide by 0.
            if (end == start)
                return value == start ? 1 : 0;

            return (value - start) / (end - start);
        }

        // This returns a value regarding to the percentage and the difference.
        // Example, 100 --> 150, 0.5 returns 125 for this.
        public static double calculateValueFromPercentage(double start, double end, double percentage)
        {
            return start + ((end - start) * percentage);
        }
    }
}
