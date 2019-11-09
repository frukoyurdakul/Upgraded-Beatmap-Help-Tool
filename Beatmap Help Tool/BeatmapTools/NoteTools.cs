using Beatmap_Help_Tool.BeatmapModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapTools
{
    public static class NoteTools
    {
        public static void setAllWhistlesToClaps(Beatmap beatmap)
        {
            List<HitObject> objects = beatmap.HitObjects;
            HitObject hitObject;
            for (int i = 0; i < objects.Count; i++)
            {
                hitObject = objects[i];
                hitObject.Hitsound &= ~HitObject.WHISTLE;
                hitObject.Hitsound |= HitObject.CLAP;
            }
        }
    }
}
