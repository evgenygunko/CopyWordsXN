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
		AppKit.NSButton ButtonNext { get; set; }

		[Outlet]
		AppKit.NSButton ButtonPrevious { get; set; }

		[Outlet]
		AppKit.NSLayoutConstraint ConstraintImageViewHeight { get; set; }

		[Outlet]
		AppKit.NSLayoutConstraint ConstraintImageViewWidth { get; set; }

		[Outlet]
		AppKit.NSImageView ImageViewPage { get; set; }

		[Outlet]
		AppKit.NSTextField LabelImageFile { get; set; }

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
			if (ConstraintImageViewHeight != null) {
				ConstraintImageViewHeight.Dispose ();
				ConstraintImageViewHeight = null;
			}

			if (ConstraintImageViewWidth != null) {
				ConstraintImageViewWidth.Dispose ();
				ConstraintImageViewWidth = null;
			}

			if (ImageViewPage != null) {
				ImageViewPage.Dispose ();
				ImageViewPage = null;
			}

			if (LabelImageFile != null) {
				LabelImageFile.Dispose ();
				LabelImageFile = null;
			}

			if (ButtonPrevious != null) {
				ButtonPrevious.Dispose ();
				ButtonPrevious = null;
			}

			if (ButtonNext != null) {
				ButtonNext.Dispose ();
				ButtonNext = null;
			}
		}
	}
}
