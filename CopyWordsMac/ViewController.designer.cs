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
		AppKit.NSTableView ActivityLog { get; set; }

		[Outlet]
		AppKit.NSButton ButtonOpslagsord { get; set; }

		[Outlet]
		AppKit.NSButton ButtonPlaySound { get; set; }

		[Outlet]
		AppKit.NSButton ButtonSaveSound { get; set; }

		[Outlet]
		AppKit.NSTextField LabelDefinitions { get; set; }

		[Outlet]
		AppKit.NSTextField LabelEndings { get; set; }

		[Outlet]
		AppKit.NSTextField LabelExamples { get; set; }

		[Outlet]
		AppKit.NSTextField LabelPronunciation { get; set; }

		[Outlet]
		AppKit.NSTextField LabelTranslation { get; set; }

		[Outlet]
		AppKit.NSTextField LabelWord { get; set; }

		[Outlet]
		AppKit.NSStackView StackViewOpslagsords { get; set; }

		[Outlet]
		AppKit.NSTextField txtLookUp { get; set; }

		[Action ("ButtonCopyDefinitionsClicked:")]
		partial void ButtonCopyDefinitionsClicked (AppKit.NSButton sender);

		[Action ("ButtonCopyEndingsClicked:")]
		partial void ButtonCopyEndingsClicked (AppKit.NSButton sender);

		[Action ("ButtonCopyExamplesClicked:")]
		partial void ButtonCopyExamplesClicked (AppKit.NSButton sender);

		[Action ("ButtonCopyPronunciationClicked:")]
		partial void ButtonCopyPronunciationClicked (AppKit.NSButton sender);

		[Action ("ButtonCopyWordClicked:")]
		partial void ButtonCopyWordClicked (AppKit.NSButton sender);

		[Action ("ButtonPlaySoundClicked:")]
		partial void ButtonPlaySoundClicked (AppKit.NSButton sender);

		[Action ("ButtonSaveSoundClicked:")]
		partial void ButtonSaveSoundClicked (AppKit.NSButton sender);

		[Action ("ButtonSearchClicked:")]
		partial void ButtonSearchClicked (AppKit.NSButton sender);

		[Action ("txtLookUpKeyDown:")]
		partial void txtLookUpKeyDown (AppKit.NSTextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityLog != null) {
				ActivityLog.Dispose ();
				ActivityLog = null;
			}

			if (ButtonPlaySound != null) {
				ButtonPlaySound.Dispose ();
				ButtonPlaySound = null;
			}

			if (ButtonSaveSound != null) {
				ButtonSaveSound.Dispose ();
				ButtonSaveSound = null;
			}

			if (LabelDefinitions != null) {
				LabelDefinitions.Dispose ();
				LabelDefinitions = null;
			}

			if (LabelEndings != null) {
				LabelEndings.Dispose ();
				LabelEndings = null;
			}

			if (LabelExamples != null) {
				LabelExamples.Dispose ();
				LabelExamples = null;
			}

			if (LabelPronunciation != null) {
				LabelPronunciation.Dispose ();
				LabelPronunciation = null;
			}

			if (LabelTranslation != null) {
				LabelTranslation.Dispose ();
				LabelTranslation = null;
			}

			if (LabelWord != null) {
				LabelWord.Dispose ();
				LabelWord = null;
			}

			if (txtLookUp != null) {
				txtLookUp.Dispose ();
				txtLookUp = null;
			}

			if (StackViewOpslagsords != null) {
				StackViewOpslagsords.Dispose ();
				StackViewOpslagsords = null;
			}

			if (ButtonOpslagsord != null) {
				ButtonOpslagsord.Dispose ();
				ButtonOpslagsord = null;
			}
		}
	}
}
