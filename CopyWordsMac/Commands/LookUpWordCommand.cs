using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using AppKit;
using CopyWords.Parsers.Models;
using CopyWords.Parsers;
using System.Threading.Tasks;

namespace CopyWordsMac.Commands
{
    public class LookUpWordCommand
    {
        private HttpClient _httpClient = new HttpClient(); 

        public async Task<WordModel> LookUpWordAsync(string wordToLookUp)
        {
            if (string.IsNullOrEmpty(wordToLookUp))
            {
                throw new ArgumentException("LookUp text can't be null or empty.");
            }

            if (!CheckThatWordIsValid(wordToLookUp))
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Warning,
					MessageText = "Invalid search term",
                    InformativeText = "Search can only contain alphanumeric characters and spaces.",
                };
                alert.RunModal();

                return null;
            }

            string ddoUrl = $"http://ordnet.dk/ddo/ordbog?query={wordToLookUp}&search=S%C3%B8g";
      
            // Download and parse a page from DDO
            string ddoPageHtml = await DownloadPageAsync(ddoUrl);
            if (string.IsNullOrEmpty(ddoPageHtml))
            {
                return null;
            }

            DDOPageParser ddoPageParser = new DDOPageParser();
            ddoPageParser.LoadHtml(ddoPageHtml);

            WordModel wordViewModel = new WordModel();
            wordViewModel.Word = ddoPageParser.ParseWord();
            wordViewModel.Endings = ddoPageParser.ParseEndings();
            wordViewModel.Pronunciation = ddoPageParser.ParsePronunciation();
            //wordViewModel.Sound = ddoPageParser.ParseSound();
            wordViewModel.Definitions = ddoPageParser.ParseDefinitions();
            wordViewModel.Examples = ddoPageParser.ParseExamples();

            // Download and parse a page from Slovar.dk
            string slovardkUrl = GetSlovardkUri(wordToLookUp);

            string slovardkPageHtml = await DownloadPageAsync(slovardkUrl);
            SlovardkPageParser slovardkPageParser = new SlovardkPageParser();
            slovardkPageParser.LoadHtml(slovardkPageHtml);

            var translations = slovardkPageParser.ParseWord();
            wordViewModel.Translations = translations;

            return wordViewModel;
        }

        internal static bool CheckThatWordIsValid(string lookUp)
        {
            Regex regex = new Regex(@"^[\w ]+$");
            return regex.IsMatch(lookUp);
        }

        internal static string GetSlovardkUri(string wordToLookUp)
        {
            wordToLookUp = wordToLookUp.ToLower()
                .Replace("å", "'aa")
                .Replace("æ", "'ae")
                .Replace("ø", "'oe")
                .Replace(" ", "-");

            return $"http://www.slovar.dk/tdansk/{wordToLookUp}/?";
        }

        private async Task<string> DownloadPageAsync(string url)
        {
            string content = await _httpClient.GetStringAsync(url);
            return content;
        }
    }
}
