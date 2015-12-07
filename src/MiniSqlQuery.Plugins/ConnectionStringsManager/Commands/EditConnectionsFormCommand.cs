#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core;
using MiniSqlQuery.Commands;

namespace MiniSqlQuery.Plugins.ConnectionStringsManager.Commands
{
	/// <summary>The edit connections form command.</summary>
	public class EditConnectionsFormCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="EditConnectionsFormCommand"/> class.</summary>
		public EditConnectionsFormCommand()
			: base("&Edit Connection Strings")
		{
			SmallImage = ImageResource.database_edit;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			DbConnectionsFormDialog.Show(HostWindow);
		}
	}
}