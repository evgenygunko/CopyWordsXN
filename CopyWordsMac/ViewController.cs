using System;

using AppKit;
using Foundation;

namespace CopyWordsMac
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			// Do any additional setup after loading the view.

			// Set the initial value for the label
            //labelHelloWorld.StringValue = "Button has not been clicked yet.";
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
