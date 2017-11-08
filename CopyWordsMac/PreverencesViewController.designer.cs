// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CopyWordsMac
{
    [Register ("PreverencesViewController")]
    partial class PreverencesViewController
    {
        [Outlet]
        AppKit.NSButton ButtonOK { get; set; }

        [Outlet]
        AppKit.NSTextField TextAnkiMediaCollectionFolderPath { get; set; }

        [Outlet]
        AppKit.NSTextField TextDictionaryFolderPath { get; set; }

        [Action ("ButtonCancelClicked:")]
        partial void ButtonCancelClicked (AppKit.NSButton sender);

        [Action ("ButtonOKClicked:")]
        partial void ButtonOKClicked (AppKit.NSButton sender);

        [Action ("ButtonSelectAnkiMediaCollectionFolderClicked:")]
        partial void ButtonSelectAnkiMediaCollectionFolderClicked (AppKit.NSButton sender);

        [Action ("ButtonSelectFileClicked:")]
        partial void ButtonSelectFileClicked (AppKit.NSButton sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (ButtonOK != null) {
                ButtonOK.Dispose ();
                ButtonOK = null;
            }

            if (TextDictionaryFolderPath != null) {
                TextDictionaryFolderPath.Dispose ();
                TextDictionaryFolderPath = null;
            }

            if (TextAnkiMediaCollectionFolderPath != null) {
                TextAnkiMediaCollectionFolderPath.Dispose ();
                TextAnkiMediaCollectionFolderPath = null;
            }
        }
    }
}
