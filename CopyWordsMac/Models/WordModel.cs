using System;
using System.Collections.Generic;

namespace CopyWordsMac.Models
{
    public class WordModel
    {
        public string Word = "<>";
        public string Endings = "<>";
        public string Pronunciation = "<>";
        public string Definitions = "<>";
        public List<string> Examples = new List<string>();
        public List<RussianTranslation> Translations = new List<RussianTranslation>();
    }
}
