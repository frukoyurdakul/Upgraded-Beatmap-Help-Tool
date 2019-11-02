namespace Beatmap_Help_Tool.Utils
{
    public static class IndexUtils
    {
        public static int getIndexOfWithCount(string text, string searchString, int count)
        {
            if (count == 1)
                return text.IndexOf(searchString);

            int targetSize = searchString.Length;
            int countInternal = 0;
            string substring;
            for (int i = 0; i < text.Length - targetSize; i++)
            {
                substring = text.Substring(i, targetSize);
                if (substring == searchString)
                    countInternal++;
                if (countInternal == count)
                    return i;
            }
            return -1;
        }
    }
}
