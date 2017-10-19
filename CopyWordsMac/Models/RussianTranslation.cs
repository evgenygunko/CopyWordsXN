using System;

namespace CopyWordsMac.Models
{
    public class RussianTranslation
    {
        public RussianTranslation(string danishWord, string translation)
        {
            DanishWord = danishWord;
            Translation = translation;
        }

        public string DanishWord { get; }

        public string Translation { get; }
    }
}
