using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace CopyWordsMac
{
    public partial class RusDanskViewController : AppKit.NSViewController
    {
        public const double ZoomStep = 0.3;

		private double _resizeFactor = 1.0;
        private NSImage _currentImage;

        // Called when created from unmanaged code
        public RusDanskViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            LoadImage();
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

        private void LoadImage()
        {
            _currentImage = new NSImage("/Users/evgeny/Documents/Dansk-Rissisk ordbog/DATA/0013.jpg");

            ImageViewPage.Image = _currentImage;
        }

        private NSImage ZoomIn()
        {
            _resizeFactor += ZoomStep;
            return ResizeImage(_currentImage, _resizeFactor);
        }

        private NSImage ZoomOut()
        {
            _resizeFactor -= ZoomStep;
            return ResizeImage(_currentImage, _resizeFactor);
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

            return newImage;
        }
    }
}
