using System;

using AppKit;
using CopyWords.Parsers;
using CopyWords.Parsers.Models;
using CopyWordsMac.ViewModels;
using Foundation;

namespace CopyWordsMac
{
    public partial class ViewController : NSViewController
    {
        private WordViewModel _wordViewModel = new WordViewModel();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.

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

        partial void txtLookUpKeyDown(AppKit.NSTextField sender)
        {
            LookUpWordAsync();
        }

        partial void ButtonSearchClicked(AppKit.NSButton sender)
        {
            LookUpWordAsync();
        }

        partial void ButtonCopyWordClicked(AppKit.NSButton sender)
        {
        }

        partial void ButtonCopyDefinitionsClicked(AppKit.NSButton sender)
        {
        }

        partial void ButtonCopyTranslationClicked(AppKit.NSButton sender)
        {
        }

        partial void ButtonCopyPronunciationClicked(AppKit.NSButton sender)
        {
        }

        partial void ButtonCopyEndingsClicked(AppKit.NSButton sender)
        {
        }

        partial void ButtonCopyExamplesClicked(AppKit.NSButton sender)
        {
        }

        #endregion

        #region Private Methods

        private async void LookUpWordAsync()
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
            LabelTranslation.StringValue = _wordViewModel.Translations;
            LabelPronunciation.StringValue = _wordViewModel.Pronunciation;
            LabelEndings.StringValue = _wordViewModel.Endings;
            LabelExamples.StringValue = _wordViewModel.Examples;
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

        #endregion
    }
}
