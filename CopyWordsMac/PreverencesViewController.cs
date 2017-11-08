using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CopyWordsMac.Helpers;
using System.IO;

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

            TextAnkiMediaCollectionFolderPath.StringValue = user.StringForKey(NSUserDefaultsKeys.AnkiCollectionPath) ?? string.Empty;
            if (string.IsNullOrEmpty(TextAnkiMediaCollectionFolderPath.StringValue))
            {
                TextAnkiMediaCollectionFolderPath.StringValue = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "Library/Application Support/Anki2/User 1/collection.media");
            }

            TextDictionaryFolderPath.Changed += (s, e) => UpdateControls();
            TextAnkiMediaCollectionFolderPath.Changed += (s, e) => UpdateControls();
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
            user.SetString(TextAnkiMediaCollectionFolderPath.StringValue, NSUserDefaultsKeys.AnkiCollectionPath);

            View.Window?.Close();
        }

        partial void ButtonSelectFileClicked(AppKit.NSButton sender)
        {
            string folder = ShowChooseFolderDialog("Choose a folder with Russisk-Dansk dictionary files");

            if (folder != null)
            {
                TextDictionaryFolderPath.StringValue = folder;

                UpdateControls();
            }
        }

        partial void ButtonSelectAnkiMediaCollectionFolderClicked(AppKit.NSButton sender)
        {
            string folder = ShowChooseFolderDialog("Choose a folder with Anki media collection");

            if (folder != null)
            {
                TextAnkiMediaCollectionFolderPath.StringValue = folder;

                UpdateControls();
            }
        }

        private string ShowChooseFolderDialog(string title)
        {
            string folder = null;

            var dialog = new NSOpenPanel();

            dialog.Title = title;
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
                    folder = result?.Path;
                }
            }

            return folder;
        }

        private void UpdateControls()
        {
            ButtonOK.Enabled = !string.IsNullOrEmpty(TextDictionaryFolderPath.StringValue);
        }
    }
}
