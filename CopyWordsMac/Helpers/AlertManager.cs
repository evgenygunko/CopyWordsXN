using System;
using AppKit;

namespace CopyWordsMac.Helpers
{
    public static class AlertManager
    {
        public static void ShowWarningAlert(string messageText, string informativeText)
        {
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Warning,
                MessageText = messageText,
                InformativeText = informativeText,
            };
            alert.RunModal();
        }

        public static void ShowInfoAlert(string messageText, string informativeText)
        {
            var alert = new NSAlert()
            {
                AlertStyle = NSAlertStyle.Informational,
                MessageText = messageText,
                InformativeText = informativeText,
            };
            alert.RunModal();
        }
    }
}
