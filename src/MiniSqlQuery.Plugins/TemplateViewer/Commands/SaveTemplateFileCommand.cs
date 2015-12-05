using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Plugins.TemplateViewer.Commands
{
	[Obsolete("This class is an exact copy of SaveFileCommand which is in the main MiniSqlQuery project")]
	public class SaveTemplateFileCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="SaveTemplateFileCommand"/> class.</summary>
		public SaveTemplateFileCommand()
			: base("&Save File")
		{
			ShortcutKeys = Keys.Control | Keys.S;
			SmallImage = ImageResource.disk;
		}

		/// <summary>Gets a value indicating whether Enabled.</summary>
		public override bool Enabled
		{
			get
			{
				IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
				if (editor != null)
				{
					return editor.IsDirty;
				}

				return false;
			}
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
			if (editor != null)
			{
				if (editor.FileName == null)
				{
					CommandManager.GetCommandInstance<SaveTemplateFileAsCommand>().Execute();
				}
				else
				{
					editor.SaveFile();
				}
			}
		}
	}
}