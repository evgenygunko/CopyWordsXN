using System;
using System.Collections.Generic;
using System.Linq;
using CopyWords.Parsers.Models;

namespace CopyWordsMac.ViewModels
{
    public class WordViewModel
    {
        public string Word { get; set; } = "<>";
        public string Endings { get; set; } = "<>";
        public string Pronunciation { get; set; } = "<>";
        public string Definitions { get; set; } = "<>";
        public string Sound { get; set; }
        public string Examples { get; set; } = "<>";
        public List<RussianTranslation> Translations = new List<RussianTranslation>();
        public List<VariationUrl> VariationUrls { get; set; }

        internal static WordViewModel CreateFromModel(WordModel wordModel)
        {
            WordViewModel wordViewModel = new WordViewModel();

            if (wordModel != null)
            {
                wordViewModel.Word = wordModel.Word;
                wordViewModel.Endings = wordModel.Endings;
                wordViewModel.Pronunciation = wordModel.Pronunciation;
                wordViewModel.Definitions = wordModel.Definitions;
                wordViewModel.Sound = wordModel.Sound;
                wordViewModel.Translations = wordModel.Translations;
                wordViewModel.VariationUrls = wordModel.VariationUrls;
            
                if (wordModel.Examples != null && wordModel.Examples.Count > 0)
                {
                    string delimeter = Environment.NewLine;
                    wordViewModel.Examples = wordModel.Examples.Aggregate((i, j) => i + delimeter + j);
                }
            }

            return wordViewModel;
        }
    }
}
