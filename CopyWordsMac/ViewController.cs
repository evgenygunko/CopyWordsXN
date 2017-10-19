using System;

using AppKit;
using CopyWordsMac.Models;
using Foundation;

namespace CopyWordsMac
{
    public partial class ViewController : NSViewController
    {
        private WordModel _wordModel = new WordModel();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.

            LabelWord.StringValue = _wordModel.Word;
            LabelDefinitions.StringValue = _wordModel.Definitions;
            LabelTranslation.StringValue = _wordModel.Translations;
            LabelPronunciation.StringValue = _wordModel.Pronunciation;
            LabelEndings.StringValue = _wordModel.Endings;
            LabelExamples.StringValue = _wordModel.Examples;
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

        partial void ButtonSearchClicked(AppKit.NSButton sender)
        {
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                InformativeText = txtLookUp.StringValue,
                MessageText = "Will lookup this word",
            };
            alert.RunModal();

            // Update counter and label
            //labelHelloWorld.StringValue = string.Format("The button has been clicked {0} time{1}.", ++numberOfTimesClicked, (numberOfTimesClicked < 2) ? "" : "s");
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
    }
}
