using System;
using System.Collections.Generic;
using System.IO;
using AppKit;
using CopyWordsMac.Helpers;
using Foundation;

namespace CopyWordsMac.ViewModels
{
    public class ScannedImageViewModel
    {
        private string _word;
        private string _currentPage;
        private List<string> _allPages;

        private NSImage _currentImage;

        public ScannedImageViewModel(string word, List<string> allPages, string currentPage)
        {
            _word = word;
            _currentPage = currentPage;
            _allPages = allPages;

            SetCurrentImage();
        }

        #region Properties

        public bool IsPreviousPageAvaliable { get; set; }

        public bool IsNextPageAvaliable { get; set; }

        public NSImage CurrentImage
        {
            get { return _currentImage; }
        }

        public string Title { get; set; }

        #endregion

        #region Public Methods

        public void SetCurrentImage()
        {
            NSUserDefaults user = NSUserDefaults.StandardUserDefaults;
            string pathToDictionary = user.StringForKey(NSUserDefaultsKeys.DictionaryFolderPath);

            string imageFile = Path.Combine(pathToDictionary, _currentPage);
            _currentImage = new NSImage(imageFile);

            int currPageIndex = _allPages.IndexOf(_currentPage);
            IsPreviousPageAvaliable = currPageIndex > 0;
            IsNextPageAvaliable = currPageIndex < (_allPages.Count - 1);

            Title = $"Word: '{_word}', image file: '{_currentPage}'";
        }

        public void MoveForward()
        {
            int currPageIndex = _allPages.IndexOf(_currentPage);
            _currentPage = _allPages[currPageIndex + 1];

            SetCurrentImage();
        }

        public void MoveBack()
        {
            int currPageIndex = _allPages.IndexOf(_currentPage);
            _currentPage = _allPages[currPageIndex - 1];

            SetCurrentImage();
        }

        #endregion
    }
}
