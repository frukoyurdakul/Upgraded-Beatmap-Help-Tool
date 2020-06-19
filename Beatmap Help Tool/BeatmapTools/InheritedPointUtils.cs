using Beatmap_Help_Tool.BeatmapModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class InheritedPointUtils
    {
        public static void AddSvChanges(Beatmap beatmap, int firstOffset, int lastOffset, 
            double firstSv, double lastSv, double targetBpm, 
            double gridSnap, int svOffset, int svIncreaseMode, int count, 
            double svIncreaseMultiplier, bool putPointsByNotes)
        {
            List<TimingPoint> actualPoints = beatmap.TimingPoints;
            List<HitObject> actualObjects = beatmap.HitObjects;


        }
    }
}
