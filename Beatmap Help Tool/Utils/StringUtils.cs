namespace Beatmap_Help_Tool.Utils
{
    public static class StringUtils
    {
        public static int getIndexOfWithCount(string text, string searchText, int count)
        {
            if (count == 1)
                return text.IndexOf(searchText);

            int targetSize = searchText.Length;
            int countInternal = 0;
            string substring;
            for (int i = 0; i < text.Length - targetSize; i++)
            {
                substring = text.Substring(i, targetSize);
                if (substring == searchText && ++countInternal == count)
                    return i;
            }
            return -1;
        }

        public static int getStringCountInString(string text, string searchText)
        {
            if (text.Length < searchText.Length)
                return 0;

            int count = 0;
            int iterationCount = text.Length - searchText.Length;
            int searchLength = searchText.Length;
            for (int i = 0; i < iterationCount; i++)
            {
                if (text.Substring(i, searchLength) == searchText)
                    count++;
            }
            return count;
        }
    }
}
