using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using AppKit;
using CopyWordsMac.Models;
using CopyWordsMac.Parsers;

namespace CopyWordsMac.Commands
{
    public class LookUpWordCommand
    {
        public WordModel LookUpWord(string wordToLookUp)
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
            Stream ddoStream = DownloadPage(ddoUrl);
            if (ddoStream == null)
            {
                return null;
            }

            DDOPageParser ddoPageParser = new DDOPageParser();
            ddoPageParser.LoadStream(ddoStream);

            WordModel wordViewModel = new WordModel();
            wordViewModel.Word = ddoPageParser.ParseWord();
            wordViewModel.Endings = ddoPageParser.ParseEndings();
            wordViewModel.Pronunciation = ddoPageParser.ParsePronunciation();
            //wordViewModel.Sound = ddoPageParser.ParseSound();
            wordViewModel.Definitions = ddoPageParser.ParseDefinitions();
            wordViewModel.Examples = ddoPageParser.ParseExamples();

            // Download and parse a page from Slovar.dk
            string slovardkUrl = GetSlovardkUri(wordToLookUp);

            Stream slovardkStream = DownloadPage(slovardkUrl);
            SlovardkPageParser slovardkPageParser = new SlovardkPageParser();
            slovardkPageParser.LoadStream(slovardkStream);

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

        private static Stream DownloadPage(string url)
        {
            //Create a WebRequest to get the file
            HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(url);

            HttpWebResponse fileResp = null;

            try
            {
                //Create a response for this request
                fileResp = (HttpWebResponse)fileReq.GetResponse();
            }
            catch (WebException ex)
            {
                HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    var alert = new NSAlert()
                    {
                        AlertStyle = NSAlertStyle.Warning,
                        MessageText = "Cannot find word",
                        InformativeText = "Den Danske Ordbog server returned NotFound exception.",
                    };
                    alert.RunModal();

                    return null;
                }
            }

            if (fileReq.ContentLength > 0)
            {
                fileResp.ContentLength = fileReq.ContentLength;
            }

            //Get the Stream returned from the response
            Stream stream = fileResp.GetResponseStream();
            return stream;
        }
    }
}
