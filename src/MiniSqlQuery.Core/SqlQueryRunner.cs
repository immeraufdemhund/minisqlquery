#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace MiniSqlQuery.Core
{
	/// <summary>
	/// 	The sql query runner.
	/// </summary>
	public class SqlQueryRunner : QueryRunner
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SqlQueryRunner"/> class.
		/// </summary>
		/// <param name="factory">The provider factory.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="enableQueryBatching">The enable query batching.</param>
		/// <param name="commandTimeout"></param>
		public SqlQueryRunner(DbProviderFactory factory, string connectionString, bool enableQueryBatching, int commandTimeout)
			: base(factory, connectionString, enableQueryBatching, commandTimeout)
		{
		}

		/// <summary>
		/// 	The handle batch exception.
		/// </summary>
		/// <param name = "dbException">The db exception.</param>
		protected override void HandleBatchException(DbException dbException)
		{
			Exception = dbException;
			var exp = dbException as SqlException;
			if (exp != null)
			{
				foreach (SqlError error in exp.Errors)
				{
					Messages += string.Format("Error line {0}: {1}.{2}", error.LineNumber, error.Message, Environment.NewLine);
				}
			}
		}

		/// <summary>
		/// 	The subscribe to messages.
		/// </summary>
		/// <param name = "connection">The connection.</param>
		protected override void SubscribeToMessages(DbConnection connection)
		{
			var conn = connection as SqlConnection;
			if (conn != null)
			{
				conn.InfoMessage += ConnectionInfoMessage;
			}
			base.UnsubscribeFromMessages(connection);
		}

		/// <summary>
		/// 	The unsubscribe from messages.
		/// </summary>
		/// <param name = "connection">The connection.</param>
		protected override void UnsubscribeFromMessages(DbConnection connection)
		{
			var conn = connection as SqlConnection;
			if (conn != null)
			{
				conn.InfoMessage -= ConnectionInfoMessage;
			}
			base.UnsubscribeFromMessages(connection);
		}

		/// <summary>
		/// 	The connection information message collection method.
		/// </summary>
		/// <param name = "sender">The sender.</param>
		/// <param name = "e">The events for the message.</param>
		private void ConnectionInfoMessage(object sender, SqlInfoMessageEventArgs e)
		{
			Messages += e.Message + Environment.NewLine;
		}
	}
}