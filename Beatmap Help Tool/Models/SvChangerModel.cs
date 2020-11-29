using Beatmap_Help_Tool.Utils;
using Newtonsoft.Json;

namespace Beatmap_Help_Tool.SaveModel
{
    public class SvChangerModel
    {
        public const string KEY = "SvChangerData";

        public string FirstOffsetText = "";
        public string LastOffsetText = "";
        public string FirstSvText = "";
        public string LastSvText = "";
        public string TargetBpmText = "";
        public string GridSnapText = "";
        public string SvOffsetText = "";
        public int SvIncreaseMode = 0; // default index
        public string CountText = "";
        public string SvIncreaseMultiplierText = "";
        public bool PutPointsByNotes = true;
        public bool ActivateBetweenTimeMode = true;

        public SvChangerModel(string firstOffsetText, string lastOffsetText, 
            string firstSvText, string lastSvText, 
            string targetBpmText, string gridSnapText, 
            string svOffsetText, int svIncreaseMode, 
            string countText, string svIncreaseMultiplierText,
            bool putPointsByNotes, bool activateBetweenTimeMode)
        {
            FirstOffsetText = firstOffsetText;
            LastOffsetText = lastOffsetText;
            FirstSvText = firstSvText;
            LastSvText = lastSvText;
            TargetBpmText = targetBpmText;
            GridSnapText = gridSnapText;
            SvOffsetText = svOffsetText;
            SvIncreaseMode = svIncreaseMode;
            CountText = countText;
            SvIncreaseMultiplierText = svIncreaseMultiplierText;
            PutPointsByNotes = putPointsByNotes;
            ActivateBetweenTimeMode = activateBetweenTimeMode;
        }

        public void saveToPreferences()
        {
            SharedPreferences.edit().put(KEY, JsonConvert.SerializeObject(this)).apply();
        }
    }
}
