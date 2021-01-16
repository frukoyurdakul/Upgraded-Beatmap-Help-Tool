using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.BeatmapModel
{
    public class Bookmark : BeatmapElement
    {
        public Bookmark(Beatmap beatmap, double offset) : base(beatmap)
        {
            Offset = offset;
        }

        public Bookmark(Bookmark from) : base(from.beatmap) { }

        public override string GetSaveFormat()
        {
            return Offset.ToString();
        }

        public override int GetTypeInt()
        {
            return 17;
        }
    }
}
