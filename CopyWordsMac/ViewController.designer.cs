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
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSTextField txtLookUp { get; set; }

        [Action ("ButtonCopyDefinitionsClicked:")]
        partial void ButtonCopyDefinitionsClicked (AppKit.NSButton sender);

        [Action ("ButtonCopyEndingsClicked:")]
        partial void ButtonCopyEndingsClicked (AppKit.NSButton sender);

        [Action ("ButtonCopyExamplesClicked:")]
        partial void ButtonCopyExamplesClicked (AppKit.NSButton sender);

        [Action ("ButtonCopyPronunciationClicked:")]
        partial void ButtonCopyPronunciationClicked (AppKit.NSButton sender);

        [Action ("ButtonCopyTranslationClicked:")]
        partial void ButtonCopyTranslationClicked (AppKit.NSButton sender);

        [Action ("ButtonCopyWordClicked:")]
        partial void ButtonCopyWordClicked (AppKit.NSButton sender);

        [Action ("ButtonSearchClicked:")]
        partial void ButtonSearchClicked (AppKit.NSButton sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (txtLookUp != null) {
                txtLookUp.Dispose ();
                txtLookUp = null;
            }
        }
    }
}
