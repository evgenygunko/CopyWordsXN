using System;

using AppKit;
using Foundation;

namespace CopyWordsMac
{
    public partial class ViewController : NSViewController
    {
        private int numberOfTimesClicked = 0;

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

		partial void ButtonSearchClicked(AppKit.NSButton sender)
		{

			// Update counter and label
            //labelHelloWorld.StringValue = string.Format("The button has been clicked {0} time{1}.", ++numberOfTimesClicked, (numberOfTimesClicked < 2) ? "" : "s");
		}
    }
}
