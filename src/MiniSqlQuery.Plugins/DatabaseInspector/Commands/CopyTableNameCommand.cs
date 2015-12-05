#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core.Commands;
using System.Windows.Forms;

namespace MiniSqlQuery.Plugins.DatabaseInspector.Commands
{
	/// <summary>The copy table name command.</summary>
	public class CopyTableNameCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="CopyTableNameCommand"/> class.</summary>
		public CopyTableNameCommand()
			: base("Copy table name")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			Clipboard.SetText(HostWindow.DatabaseInspector.RightClickedTableName);
		}
	}
}