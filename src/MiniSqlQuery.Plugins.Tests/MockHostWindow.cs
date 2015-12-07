using MiniSqlQuery.Core;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Plugins
{
	class MockHostWindow : IHostWindow
	{
		public Form ActiveChildForm { get; set; }

		public IDatabaseInspector DatabaseInspector { get; set; }

		public Form Instance { get; set; }

		public ToolStrip ToolStrip { get; set; }

		public void AddPluginCommand<TCommand>() where TCommand : ICommand, new()
		{
			throw new NotImplementedException();
		}

		public void AddToolStripCommand<TCommand>(int? index) where TCommand : ICommand, new()
		{
			throw new NotImplementedException();
		}

		public void AddToolStripSeperator(int? index)
		{
			throw new NotImplementedException();
		}

		public void DisplayDockedForm(DockContent frm)
		{
			throw new NotImplementedException();
		}

		public DialogResult DisplayMessageBox(Form source, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			throw new NotImplementedException();
		}

		public DialogResult DisplaySimpleMessageBox(Form source, string text, string caption)
		{
			throw new NotImplementedException();
		}

		public ToolStripMenuItem GetMenuItem(string name)
		{
			throw new NotImplementedException();
		}

		public void SetArguments(string[] args)
		{
			throw new NotImplementedException();
		}

		public void SetPointerState(Cursor cursor)
		{
			throw new NotImplementedException();
		}

		public void SetResultCount(Form source, int? count)
		{
			throw new NotImplementedException();
		}

		public void SetStatus(Form source, string text)
		{
			throw new NotImplementedException();
		}

		public void ShowDatabaseInspector(IDatabaseInspector databaseInspector, DockState dockState)
		{
			throw new NotImplementedException();
		}

		public void ShowToolWindow(DockContent form, DockState dockState)
		{
			throw new NotImplementedException();
		}
	}
}
