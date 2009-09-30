using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;

namespace MiniSqlQuery.Core.Commands
{
	public class ConvertTextToTitleCaseCommand
		: CommandBase
	{
		public ConvertTextToTitleCaseCommand()
			: base("Convert to 'Title Case' text")
		{
			ShortcutKeys = Keys.Control | Keys.Alt | Keys.U;
			//SmallImage = ImageResource.;
		}

		public override bool Enabled
		{
			get
			{
				return HostWindow.ActiveChildForm as IEditor != null;
			}
		}

		public override void Execute()
		{
			IEditor editor = ActiveFormAsEditor;
			if (Enabled && editor.SelectedText.Length > 0)
			{
				editor.InsertText(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(editor.SelectedText));
			}
		}
	}
}