using Beatmap_Help_Tool.BeatmapTools;
using Beatmap_Help_Tool.Utils;
using System;
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

        /// <summary>
        /// Adds a snap value to the snap and closest snap 
        /// paramaters. Usually setOffset method is called
        /// after this.
        /// </summary>
        /// <param name="delta">the value between old snap and new snap</param>
        public void AddSnap(double delta)
        {
            snap += delta;
            closestSnap += delta;
        }

        /// <summary>
        /// Sets the offset but does not detect snaps. Usually used
        /// with resnapping tool.
        /// </summary>
        /// <param name="offset">the new offset</param>
        public void SetOffset(double offset)
        {
            this.offset = offset;
        }

        private void detectSnapsIfNecessary()
        {
            if (requiresSnapDetection)
            {
                double[] result = SnapUtils.getRelativeSnap(timingPoints, this);
                snap = result[0];
                closestSnap = result[1];
                requiresSnapDetection = false;
            }
        }

        public string GetOffsetWithLink()
        {
            return StringUtils.GetOffsetWithLink(offset);
        }

        public override string ToString()
        {
            return GetSaveFormat();
        }

        public abstract string GetSaveFormat();

        public abstract int GetTypeInt();

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return (obj as BeatmapElement).offset == offset;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 29 + offset.GetHashCode();
                return hash;
            }
        }
    }
}
