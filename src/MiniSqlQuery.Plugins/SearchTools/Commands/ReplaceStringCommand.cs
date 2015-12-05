#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core;
using MiniSqlQuery.Core.Commands;

namespace MiniSqlQuery.Plugins.SearchTools.Commands
{
	/// <summary>The replace string command.</summary>
	public class ReplaceStringCommand : CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ReplaceStringCommand"/> class.</summary>
		public ReplaceStringCommand()
			: base("Replace String")
		{
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			IFindReplaceProvider editorFindProvider = HostWindow.ActiveChildForm as IFindReplaceProvider;

			if (editorFindProvider != null)
			{
				FindTextRequest req = null;
				int key = editorFindProvider.GetHashCode();

				// is there a request in the table for this window?
				if (SearchToolsCommon.FindReplaceTextRequests.ContainsKey(key))
				{
					req = SearchToolsCommon.FindReplaceTextRequests[key];
				}

				if (req != null)
				{
					// wrap around to start if at last pos
					if (req.Position != 0)
					{
						req.Position = editorFindProvider.CursorOffset;
					}

					if (editorFindProvider.ReplaceString(req.ReplaceValue, req.Position - req.SearchValue.Length, req.SearchValue.Length))
					{
						CommandManager.GetCommandInstance<FindNextStringCommand>().Execute();
					}
				}
			}
		}
	}
}