using Beatmap_Help_Tool.BeatmapTools;
using System.Collections.Generic;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public abstract class BeatmapElement
    {
        protected double offset = 0, snap = 0, closestSnap = 0;
        protected bool requiresSnapDetection = true;
        protected List<TimingPoint> timingPoints;

        public BeatmapElement(List<TimingPoint> timingPoints)
        {
            this.timingPoints = timingPoints;
        }

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
                detectSnapsIfNecessary();
            }
        }

        public double GetSnap()
        {
            detectSnapsIfNecessary();
            return snap;
        }

        public double GetClosestSnap()
        {
            detectSnapsIfNecessary();
            return closestSnap;
        }

        private void detectSnapsIfNecessary()
        {
            if (requiresSnapDetection)
            {
                double[] result = SnapTools.getRelativeSnap(timingPoints, this);
                snap = result[0];
                closestSnap = result[1];
                requiresSnapDetection = false;
            }
        }

        public override string ToString()
        {
            return GetSaveFormat();
        }

        public abstract string GetSaveFormat();
    }
}
