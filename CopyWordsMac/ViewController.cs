using System;
using System.Diagnostics;
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

        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            // Take action based on Segue ID
            switch (segue.Identifier)
            {
                case "OpenRusDanskDictionary":
                    var command = new LookupInDRDictionary();
                    var viewModel = command.CreateViewModel(txtLookUp.StringValue);

                    if (viewModel != null)
                    {
                        var controller = segue.DestinationController as RusDanskViewController;
                        controller.ViewModel = viewModel;
                    }
                    break;

                default:
                    break;
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
                AlertManager.ShowWarningAlert("Invalid search term", errorMessage);
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
                    AlertManager.ShowInfoAlert("Cannot find word", $"Den Danske Ordbog doesn't have a page for '{word}'");
                }
            }
            catch (Exception ex)
            {
                AlertManager.ShowWarningAlert("Error occurred while searching word", ex.Message);
                return;
            }

            if (wordViewModel != null)
            {
                _wordViewModel = wordViewModel;
                UpdateControls();

                return;
            }

            AlertManager.ShowWarningAlert("Cannot find word", $"Cannot find word {word} in DDO.");
        }

        private async Task GetWordVariationAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                AlertManager.ShowWarningAlert("URL is null or empty", "Cannot find a URL to lookup this word");
                return;
            }

            LookUpWord command = new LookUpWord();

            WordViewModel wordViewModel = null;

            try
            {
                WordModel wordModel = await command.GetWordVariationAsync(url);
                wordViewModel = WordViewModel.CreateFromModel(wordModel);

                log.Translations.Clear();
                foreach (RussianTranslation translation in wordViewModel.Translations)
                {
                    log.Translations.Add(new RussianTranslation(translation.DanishWord, translation.Translation));
                }

                ActivityLog.ReloadData();

                if (wordModel == null)
                {
                    AlertManager.ShowInfoAlert("Cannot find word", $"Den Danske Ordbog doesn't have a page for url '{url}'");
                }
            }
            catch (Exception ex)
            {
                AlertManager.ShowWarningAlert($"Error occurred while trying to load a page for url '{url}'", ex.Message);
                return;
            }

            if (wordViewModel != null)
            {
                _wordViewModel = wordViewModel;
                UpdateControls();

                return;
            }

            AlertManager.ShowWarningAlert("Cannot find word", $"Cannot find a page with url '{url}' in DDO.");
        }

        private void UpdateControls()
        {
            LabelWord.StringValue = _wordViewModel.Word;

            for (int i = StackViewOpslagsords.Views.Length - 1; i >= 0; i--)
            {
                StackViewOpslagsords.RemoveView(StackViewOpslagsords.Views[i]);
            }

            for (int i = 0; i < _wordViewModel.VariationUrls?.Count; i++)
            {
                var button = new NSButton();
                button.SetButtonType(NSButtonType.OnOff);
                button.BezelStyle = NSBezelStyle.RegularSquare;
                button.Title = _wordViewModel.VariationUrls[i].Word;

                if (txtLookUp.StringValue.Equals(_wordViewModel.VariationUrls[i].Word, StringComparison.InvariantCultureIgnoreCase))
                {
                    button.State = NSCellStateValue.On;
                }

                button.Tag = i;
                button.Activated += async delegate {
                    await VariantButtonClicked(button);
                };

                StackViewOpslagsords.AddView(button, NSStackViewGravity.Trailing);
            }

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

        #endregion

        private async Task VariantButtonClicked(NSObject sender)
        {
            var button = sender as NSButton;

            int variantIndex = int.Parse(button.Tag.ToString());
            VariationUrl variationUrl = _wordViewModel.VariationUrls[variantIndex];
            Debug.WriteLine($"Button with a variant clicked, word: {variationUrl.Word} url = {variationUrl.URL}");

            txtLookUp.StringValue = variationUrl.Word;

            await GetWordVariationAsync(variationUrl.URL);
        }
    }
}
