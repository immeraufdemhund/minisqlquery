﻿#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core;
using System.Collections.Generic;

namespace MiniSqlQuery.Plugins.SearchTools
{
	/// <summary>The search tools common.</summary>
	public class SearchToolsCommon
	{
		/// <summary>The _find text requests.</summary>
		private static readonly Dictionary<int, FindTextRequest> _findTextRequests = new Dictionary<int, FindTextRequest>();

		/// <summary>Gets FindReplaceTextRequests.</summary>
		public static Dictionary<int, FindTextRequest> FindReplaceTextRequests
		{
			get { return _findTextRequests; }
		}
	}
}