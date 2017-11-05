using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CopyWordsMac.Helpers;
using System.IO;

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

        public string Word { get; set; }

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
            NSUserDefaults user = NSUserDefaults.StandardUserDefaults;
            string pathToDictionary = user.StringForKey(NSUserDefaultsKeys.DictionaryFolderPath) ?? string.Empty;

            string imageFile = Path.Combine(pathToDictionary, "0013.jpg");
            if (File.Exists(imageFile))
            {
                _currentImage = new NSImage(imageFile);
                ImageViewPage.Image = _currentImage;    
            }
            else
            {
                AlertManager.ShowWarningAlert("Cannot find dictionary files.", $"Cannot find files for Russisk-Dansk dictionary. Please select a path in Preferences dialog.");
            }
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
