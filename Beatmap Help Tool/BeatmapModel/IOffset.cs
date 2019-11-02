namespace Beatmap_Help_Tool.BeatmapModel
{
    public interface IOffset
    {
        double GetOffset();
        double GetSnap();
        void SetOffset(double offset);
    }
}
