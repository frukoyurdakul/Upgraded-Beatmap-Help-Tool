using Beatmap_Help_Tool.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Models
{
    public class EqualizeSvData
    {
        public const string KEY = "EqualizeSvData";

        public string StartTime = "";
        public string EndTime = "";
        public string TargetBPM = "";
        public string SvMultiplier = "";
        public bool EqualizeAll = true;
        public bool RelativeUse = true;
        public bool RememberOptions = true;

        public EqualizeSvData(string startTime, string endTime, string targetBpm, string svMultiplier, bool equalizeAll, bool relativeUse, bool rememberOptions)
        {
            StartTime = startTime;
            EndTime = endTime;
            TargetBPM = targetBpm;
            SvMultiplier = svMultiplier;
            EqualizeAll = equalizeAll;
            RelativeUse = relativeUse;
            RememberOptions = rememberOptions;
        }

        public void saveToPreferences()
        {
            SharedPreferences.edit().put(KEY, JsonConvert.SerializeObject(this)).apply();
        }
    }
}
