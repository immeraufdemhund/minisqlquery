#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using System.Windows.Forms;
using MiniSqlQuery.Core;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Commands
{
	/// <summary>The new file command.</summary>
	public class NewFileCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="NewFileCommand"/> class.</summary>
		public NewFileCommand()
			: base("New &File")
		{
			ShortcutKeys = Keys.Control | Keys.Alt | Keys.N;
			SmallImage = ImageResource.page;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			var newFileForm = Services.Resolve<NewFileForm>();

			if (newFileForm.ShowDialog() == DialogResult.OK)
			{
				var editor = Services.Resolve<IEditor>(newFileForm.FileEditorDescriptor.EditorKeyName);
				editor.FileName = null;
				HostWindow.DisplayDockedForm(editor as DockContent);
			}
		}
	}
}