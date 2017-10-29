using System;
using AppKit;

namespace CopyWordsMac.Helpers
{
    public static class Clipboard
    {
        public static void SetText(string text)
        {
            var pasteboard = NSPasteboard.GeneralPasteboard;

            pasteboard.ClearContents();
            pasteboard.SetStringForType(text, "NSStringPboardType");
        }
    }
}
