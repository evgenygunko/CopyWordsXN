using System;
using System.Threading.Tasks;
using AppKit;
using CopyWords.Parsers;
using CopyWords.Parsers.Models;
using CopyWordsMac.Helpers;
using CopyWordsMac.ViewModels;
using Foundation;

namespace CopyWordsMac
{
    public partial class ViewController : NSViewController
    {
        private WordViewModel _wordViewModel = new WordViewModel();

        private TranslationsDataSource log = new TranslationsDataSource();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.

            ActivityLog.DataSource = log;
            ActivityLog.Delegate = new TranslationsDelegate(log);

            UpdateControls();
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        #region Button clicked events

        async partial void txtLookUpKeyDown(AppKit.NSTextField sender)
        {
            await LookUpWordAsync();
        }

        async partial void ButtonSearchClicked(AppKit.NSButton sender)
        {
            await LookUpWordAsync();
        }

        partial void ButtonCopyWordClicked(AppKit.NSButton sender)
        {
            Clipboard.SetText(LabelWord.StringValue);
        }

        partial void ButtonCopyDefinitionsClicked(AppKit.NSButton sender)
        {
            Clipboard.SetText(LabelDefinitions.StringValue);
        }

        partial void ButtonCopyPronunciationClicked(AppKit.NSButton sender)
        {
            Clipboard.SetText(LabelPronunciation.StringValue);
        }

        partial void ButtonCopyEndingsClicked(AppKit.NSButton sender)
        {
            Clipboard.SetText(LabelEndings.StringValue);
        }

        partial void ButtonCopyExamplesClicked(AppKit.NSButton sender)
        {
            Clipboard.SetText(LabelExamples.StringValue);
        }

        async partial void ButtonPlaySoundClicked(AppKit.NSButton sender)
        {
            await AudioManager.PlaySoundAsync(_wordViewModel.Sound);
        }

        async partial void ButtonSaveSoundClicked(AppKit.NSButton sender)
        {
            await AudioManager.SaveSoundFileAsync(_wordViewModel.Sound, _wordViewModel.Word);
        }

        #endregion

        #region Private Methods

        private async Task LookUpWordAsync()
        {
            if (string.IsNullOrEmpty(txtLookUp.StringValue))
            {
                return;
            }

            string word = txtLookUp.StringValue;
            LookUpWord command = new LookUpWord();

            (bool isValid, string errorMessage) = command.CheckThatWordIsValid(word);
            if (!isValid)
            {
                ShowWarningAlert("Invalid search term", errorMessage);
                return;
            }

            WordViewModel wordViewModel = null;

            try
            {
                WordModel wordModel = await command.LookUpWordAsync(word);
                wordViewModel = WordViewModel.CreateFromModel(wordModel);

                log.Translations.Clear();
                foreach(RussianTranslation translation in wordViewModel.Translations)
                {
                    log.Translations.Add(new RussianTranslation(translation.DanishWord, translation.Translation));                
                }

                ActivityLog.ReloadData();

                if (wordModel == null)
                {
                    ShowInfoAlert("Cannot find word", $"Den Danske Ordbog doesn't have a page for '{word}'");
                }
            }
            catch (Exception ex)
            {
                ShowWarningAlert("Error occurred while searching word", ex.Message);
                return;
            }

            if (wordViewModel != null)
            {
                _wordViewModel = wordViewModel;
                UpdateControls();

                return;
            }

            ShowWarningAlert("Cannot find word", $"Cannot find word {word} in DDO.");
        }

        private void UpdateControls()
        {
            LabelWord.StringValue = _wordViewModel.Word;
            LabelDefinitions.StringValue = _wordViewModel.Definitions;
            LabelPronunciation.StringValue = _wordViewModel.Pronunciation;
            LabelEndings.StringValue = _wordViewModel.Endings;
            LabelExamples.StringValue = _wordViewModel.Examples;

            if (!string.IsNullOrEmpty(_wordViewModel.Sound))
            {
                ButtonPlaySound.Enabled = true;
                ButtonSaveSound.Enabled = true;
            }
            else
            {
                ButtonPlaySound.Enabled = false;
                ButtonSaveSound.Enabled = false;
            }
        }

        private void ShowWarningAlert(string messageText, string informativeText)
        {
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Warning,
                MessageText = messageText,
                InformativeText = informativeText,
            };
            alert.RunModal();
        }

        private void ShowInfoAlert(string messageText, string informativeText)
        {
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                MessageText = messageText,
                InformativeText = informativeText,
            };
            alert.RunModal();
        }
        #endregion
    }
}
