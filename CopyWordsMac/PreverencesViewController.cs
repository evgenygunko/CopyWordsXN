using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CopyWordsMac.Helpers;

namespace CopyWordsMac
{
    public partial class PreverencesViewController : AppKit.NSViewController
    {
        public PreverencesViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            NSUserDefaults user = NSUserDefaults.StandardUserDefaults;
            TextDictionaryFolderPath.StringValue = user.StringForKey(NSUserDefaultsKeys.DictionaryFolderPath) ?? string.Empty;

            TextDictionaryFolderPath.Changed += (s, e) => UpdateControls();
            UpdateControls();
        }

        partial void ButtonCancelClicked(AppKit.NSButton sender)
        {
            View.Window?.Close();
        }

        partial void ButtonOKClicked(AppKit.NSButton sender)
        {
            // save path to dictionary in user settings
            NSUserDefaults user = NSUserDefaults.StandardUserDefaults;
            user.SetString(TextDictionaryFolderPath.StringValue, NSUserDefaultsKeys.DictionaryFolderPath);

            View.Window?.Close();
        }

        partial void ButtonSelectFileClicked(AppKit.NSButton sender)
        {
            var dialog = new NSOpenPanel();

            dialog.Title = "Choose a folder with Russisk-Dansk dictionary files";
            dialog.ShowsResizeIndicator = true;
            dialog.ShowsHiddenFiles = false;
            dialog.CanChooseDirectories = true;
            dialog.CanCreateDirectories = true;
            dialog.AllowsMultipleSelection = false;

            if (dialog.RunModal() == (nint)(int)NSPanelButtonType.Ok)
            {
                NSUrl result = dialog.Url; // Pathname of the file

                if (result != null)
                {
                    TextDictionaryFolderPath.StringValue = result?.Path;
                }
            }

            UpdateControls();
        }

        private void UpdateControls()
        {
            ButtonOK.Enabled = !string.IsNullOrEmpty(TextDictionaryFolderPath.StringValue);
        }
    }
}
