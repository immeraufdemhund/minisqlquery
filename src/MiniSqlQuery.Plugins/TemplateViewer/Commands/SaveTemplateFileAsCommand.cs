using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;
using System;
using System.Windows.Forms;

namespace MiniSqlQuery.Plugins.TemplateViewer.Commands
{
	[Obsolete("This class is an exact copy of SaveFileAsCommand from MiniSqlQuery project.")]
	internal class SaveTemplateFileAsCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="SaveTemplateFileAsCommand"/> class.</summary>
		public SaveTemplateFileAsCommand()
			: base("Save File &As...")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IEditor editor = HostWindow.Instance.ActiveMdiChild as IEditor;
			if (editor != null)
			{
				string oldFilename = editor.FileName;
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
				saveFileDialog.Filter = editor.FileFilter;

				if (saveFileDialog.ShowDialog(HostWindow.Instance) == DialogResult.OK)
				{
					// what if this filename covers an existing open window?
					string newFilename = saveFileDialog.FileName;
					editor.FileName = newFilename;
					editor.SaveFile();

					// register the new file and remove old if applicable
					var mostRecentFilesService = Services.Resolve<IMostRecentFilesService>();
					mostRecentFilesService.Register(newFilename);
					if (oldFilename != null && oldFilename.Equals(newFilename, StringComparison.InvariantCultureIgnoreCase))
					{
						mostRecentFilesService.Remove(oldFilename);
					}
				}
			}
		}
	}
}