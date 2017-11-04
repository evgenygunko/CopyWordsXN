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
    [Register ("RusDanskViewController")]
    partial class RusDanskViewController
    {
        [Outlet]
        AppKit.NSImageView ImageViewPage { get; set; }

        [Outlet]
        AppKit.NSTextField LabelImageFIle { get; set; }

        [Action ("ButtonNextClicked:")]
        partial void ButtonNextClicked (AppKit.NSButton sender);

        [Action ("ButtonPreviousClicked:")]
        partial void ButtonPreviousClicked (AppKit.NSButton sender);

        [Action ("ButtonZoomInClicked:")]
        partial void ButtonZoomInClicked (AppKit.NSButton sender);

        [Action ("ButtonZoomOutClicked:")]
        partial void ButtonZoomOutClicked (AppKit.NSButton sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (LabelImageFIle != null) {
                LabelImageFIle.Dispose ();
                LabelImageFIle = null;
            }

            if (ImageViewPage != null) {
                ImageViewPage.Dispose ();
                ImageViewPage = null;
            }
        }
    }
}
