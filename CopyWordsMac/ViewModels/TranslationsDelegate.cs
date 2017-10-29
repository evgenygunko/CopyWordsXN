using System;
using System.Drawing;
using AppKit;
using CopyWords.Parsers.Models;
using CopyWordsMac.Helpers;
using CoreGraphics;

namespace CopyWordsMac.ViewModels
{
    public class TranslationsDelegate : NSTableViewDelegate
    {
        private const string TextFieldCellIdentifier = "TextFieldCell";
        private const string ButtonCellIdentifier = "ButtonCell";

        private TranslationsDataSource DataSource;

        public TranslationsDelegate(TranslationsDataSource datasource)
        {
            this.DataSource = datasource;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            // This pattern allows you reuse existing views when they are no-longer in use.
            // If the returned view is null, you instance up a new view.
            // If a non-null view is returned, you modify it enough to reflect the new data.
            if (tableColumn.Identifier == "columnCopy")
            {
                NSButton buttonView = (NSButton)tableView.MakeView(ButtonCellIdentifier, this);
                if (buttonView == null)
                {
                    buttonView = new NSButton();
                    buttonView.Identifier = ButtonCellIdentifier;
                    buttonView.SetButtonType(NSButtonType.MomentaryPushIn);
                    buttonView.BezelStyle = NSBezelStyle.Rounded;

                    buttonView.Title = "kopire";
                    buttonView.Tag = row;

                    // Wireup events
                    buttonView.Activated += (sender, e) => {
                        var btn = sender as NSButton;
                        RussianTranslation translation = DataSource.Translations[(int)btn.Tag];
                        Clipboard.SetText(translation.Translation);
                    };
                }

                return buttonView;
            }
            else
            {
                NSTextField textFieldView = (NSTextField)tableView.MakeView(TextFieldCellIdentifier, this);
                if (textFieldView == null)
                {
                    textFieldView = new NSTextField();
                    textFieldView.Identifier = TextFieldCellIdentifier;
                    textFieldView.BackgroundColor = NSColor.Clear;
                    textFieldView.Bordered = false;
                    textFieldView.Selectable = false;
                    textFieldView.Editable = false;
                }

                // Set up view based on the column and row
                switch (tableColumn.Identifier)
                {
                    case "columnDansk":
                        textFieldView.StringValue = DataSource.Translations[(int)row].DanishWord;
                        break;

                    case "columnRussisk":
                        textFieldView.StringValue = DataSource.Translations[(int)row].Translation;
                        break;
                }

				return textFieldView;
            }
        }
    }
}
