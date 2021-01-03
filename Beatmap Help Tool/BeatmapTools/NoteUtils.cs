using Beatmap_Help_Tool.BeatmapModel;
using Beatmap_Help_Tool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class NoteUtils
    {
        public static void setAllWhistlesToClaps(Beatmap beatmap)
        {
            List<HitObject> objects = beatmap.HitObjects;
            HitObject hitObject;
            for (int i = 0; i < objects.Count; i++)
            {
                hitObject = objects[i];
                if (hitObject.isWhistle())
                {
                    hitObject.Hitsound &= ~HitObject.WHISTLE;
                    hitObject.Hitsound |= HitObject.CLAP;
                }
            }
        }

        public static void positionAllNotesForTaiko(Beatmap beatmap, int[] donCoordinates,
            int[] katCoordinates, int[] donFinishCoordinates, int[] katFinishCoordinates) 
        {
            beatmap.ApproachRate = 10;
            beatmap.CircleSize = 2;
            List<HitObject> objects = beatmap.HitObjects;
            HitObject hitObject;
            for (int i = 0; i < objects.Count; i++)
            {
                hitObject = objects[i];

                // Both claps and whistles are considered as kats,
                // if the note is a kat use kat or kat finisher one.
                if (hitObject.isWhistle() || hitObject.isClap())
                {
                    // Check if the note is a finisher or not.
                    if (hitObject.isFinish())
                    {
                        hitObject.X = katFinishCoordinates[0];
                        hitObject.Y = katFinishCoordinates[1];
                    }
                    else
                    {
                        // This is a regular kat.
                        hitObject.X = katCoordinates[0];
                        hitObject.Y = katCoordinates[1];
                    }
                }
                else
                {
                    // This is a don note. Check finisher.
                    if (hitObject.isFinish())
                    {
                        hitObject.X = donFinishCoordinates[0];
                        hitObject.Y = donFinishCoordinates[1];
                    }
                    else
                    {
                        // This is a regular kat.
                        hitObject.X = donCoordinates[0];
                        hitObject.Y = donCoordinates[1];
                    }
                }
            }
        }
    }
}
