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
        AppKit.NSTextField labelHelloWorld { get; set; }

        [Action ("buttonClickMeClicked:")]
        partial void buttonClickMeClicked (AppKit.NSButton sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (labelHelloWorld != null) {
                labelHelloWorld.Dispose ();
                labelHelloWorld = null;
            }
        }
    }
}
