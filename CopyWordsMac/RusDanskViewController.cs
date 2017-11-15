using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CopyWordsMac.Helpers;
using System.IO;
using CopyWordsMac.ViewModels;

namespace CopyWordsMac
{
    public partial class RusDanskViewController : AppKit.NSViewController
    {
        public const double ZoomStep = 0.3;

		private double _resizeFactor = 1.0;

        // Called when created from unmanaged code
        public RusDanskViewController(IntPtr handle) : base(handle)
        {
        }

        public ScannedImageViewModel ViewModel { get; set; }

        #region Event Handlers

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            UpdateControls();
        }

        partial void ButtonZoomInClicked(NSButton sender)
        {
            NSImage resizedImage = ZoomIn();
            ImageViewPage.Image = resizedImage;
        }

        partial void ButtonZoomOutClicked(NSButton sender)
        {
            NSImage resizedImage = ZoomOut();
            ImageViewPage.Image = resizedImage;
        }

        partial void ButtonNextClicked(AppKit.NSButton sender)
        {
            ViewModel.MoveForward();
            UpdateControls();
        }

        partial void ButtonPreviousClicked(AppKit.NSButton sender)
        {
            ViewModel.MoveBack();
            UpdateControls();
        }

        #endregion

        #region Private Methods

        private void UpdateControls()
        {
            NSImage resizedImage = ResizeImage(ViewModel.CurrentImage, _resizeFactor);
            ImageViewPage.Image = resizedImage;

            LabelImageFile.StringValue = ViewModel.Title;
            ButtonNext.Enabled = ViewModel.IsNextPageAvaliable;
            ButtonPrevious.Enabled = ViewModel.IsPreviousPageAvaliable;
        }

        private NSImage ZoomIn()
        {
            _resizeFactor += ZoomStep;
            return ResizeImage(ViewModel.CurrentImage, _resizeFactor);
        }

        private NSImage ZoomOut()
        {
            _resizeFactor -= ZoomStep;
            return ResizeImage(ViewModel.CurrentImage, _resizeFactor);
        }

        private NSImage ResizeImage(NSImage sourceImage, double resizeFactor)
        {
            var sourceSize = sourceImage.Size;

            float width = (float)(resizeFactor * sourceSize.Width);
            float height = (float)(resizeFactor * sourceSize.Height);

            var targetRect = new CoreGraphics.CGRect(0, 0, width, height);

            var newImage = new NSImage(new CoreGraphics.CGSize(width, height));

            newImage.LockFocus();
            sourceImage.DrawInRect(targetRect, CoreGraphics.CGRect.Empty, NSCompositingOperation.SourceOver, 1.0f);
            newImage.UnlockFocus();

            ConstraintImageViewWidth.Constant = width;
            ConstraintImageViewHeight.Constant = height;

            return newImage;
        }

        #endregion
    }
}
