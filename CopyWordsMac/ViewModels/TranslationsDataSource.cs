using System;
using System.Collections.Generic;
using AppKit;
using CopyWords.Parsers.Models;

namespace CopyWordsMac.ViewModels
{
    public class TranslationsDataSource : NSTableViewDataSource
    {
        public TranslationsDataSource()
        { 
        }

        public List<RussianTranslation> Translations = new List<RussianTranslation>();

        public override nint GetRowCount(NSTableView tableView)
        {
            return Translations.Count;
        }
    }
}
