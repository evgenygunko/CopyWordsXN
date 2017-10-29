using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AppKit;
using AVFoundation;
using Foundation;

namespace CopyWordsMac.Helpers
{
    public class AudioManager
    {
        private static readonly float EffectsVolume = 1.0f;

        public static async Task PlaySoundAsync(string url)
        {
            // the test sound file is set as "bundle resource", which means xamarin
            // copies it to the root folder
            //NSUrl songURL = new NSUrl("mpthreetest.mp3");

            string tempFilePath;
            tempFilePath = System.IO.Path.GetTempFileName();

            bool isFileSaved = await SaveSoundFileFromUrlAsync(url, tempFilePath);

            if (isFileSaved)
            {
                NSUrl localURL = new NSUrl(tempFilePath);

                NSError err;
                AVAudioPlayer soundEffect = new AVAudioPlayer(localURL, "mp3", out err);
                soundEffect.Volume = EffectsVolume;
                soundEffect.FinishedPlaying += delegate
                {
                    soundEffect = null;
                };

                soundEffect.NumberOfLoops = 0;
                soundEffect.Play();
            }
        }

        public static async Task<bool> SaveSoundFileAsync(string url, string word)
        {
            // todo: assuming there is only one user in deck and his name is "User 1".
            // This needs to be moved into settings.
            string ankiFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Library/Application Support/Anki2/User 1/collection.media",
                $"{word}.mp3"
            );

            if (File.Exists(ankiFilePath))
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Informational,
                    MessageText = "The file already exists",
                    InformativeText = $"The file with name '{word}.mp3' already exists. Do you want to overwrite it?",
                };
                alert.AddButton("Ok");
                alert.AddButton("Cancel");
                var alertResult = alert.RunModal();

				//OK = 1000.
                if (alertResult != 1000)
                {
                    return false;
                }
            }

            bool result = await SaveSoundFileFromUrlAsync(url, ankiFilePath);

            // save text for Anki into clipboard
            Clipboard.SetText($"[sound:{word}.mp3]");

            return result;
        }

        private static async Task<bool> SaveSoundFileFromUrlAsync(string url, string fileName)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                using (Stream soundStream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        await soundStream.CopyToAsync(fs);
                    }
                }
            }
            else
            {
                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Warning,
                    MessageText = "Cannot save sound file",
                    InformativeText = "Server returned " + response.StatusCode,
                };
                alert.RunModal();

                return false;
            }

            return true;
        }
    }
}
