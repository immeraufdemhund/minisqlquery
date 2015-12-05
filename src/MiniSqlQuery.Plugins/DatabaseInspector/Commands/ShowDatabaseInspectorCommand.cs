#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core;
using MiniSqlQuery.Commands;
using WeifenLuo.WinFormsUI.Docking;

namespace MiniSqlQuery.Plugins.DatabaseInspector.Commands
{
	/// <summary>The show database inspector command.</summary>
	public class ShowDatabaseInspectorCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowDatabaseInspectorCommand"/> class.</summary>
		public ShowDatabaseInspectorCommand()
			: base("Show Database Inspector")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			DockContent databaseInspector = Services.Resolve<IDatabaseInspector>() as DockContent;
			if (databaseInspector != null)
			{
				HostWindow.ShowDatabaseInspector(databaseInspector as IDatabaseInspector, DockState.DockLeft);
				databaseInspector.Activate();
			}
		}
	}
}