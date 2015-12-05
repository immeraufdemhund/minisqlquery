﻿#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using System.IO;

namespace MiniSqlQuery.Plugins.DatabaseInspector.Commands
{
	/// <summary>The generate delete statement command.</summary>
	public class GenerateDeleteStatementCommand : GenerateStatementCommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="GenerateDeleteStatementCommand"/> class.</summary>
		public GenerateDeleteStatementCommand()
			: base("Generate Delete Statement")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IQueryEditor editor = ActiveFormAsSqlQueryEditor;
			string tableName = HostWindow.DatabaseInspector.RightClickedTableName;
			DbModelInstance model = HostWindow.DatabaseInspector.DbSchema;

			if (tableName != null && editor != null)
			{
				StringWriter sql = new StringWriter();
				SqlWriter.WriteDelete(sql, GetTableOrViewByName(model, tableName));
				editor.InsertText(sql.ToString());
			}
		}
	}
}