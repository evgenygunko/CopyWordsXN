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
        public string Sound { get; set; } = "<>";
        public string Examples { get; set; } = "<>";
        public string Translations { get; set; } = "<>";

        internal static WordViewModel CreateFromModel(WordModel wordModel)
        {
            WordViewModel wordViewModel = new WordViewModel()
            {
                Word = wordModel.Word,
                Endings = wordModel.Endings,
                Pronunciation = wordModel.Pronunciation,
                Definitions = wordModel.Definitions,
                Sound = wordModel.Sound
            };

            string delimeter = Environment.NewLine;

            if (wordModel.Examples != null && wordModel.Examples.Count > 0)
            {
                wordViewModel.Examples = wordModel.Examples.Aggregate((i, j) => i + delimeter + j);
            }

            return wordViewModel;
        }
    }
}
