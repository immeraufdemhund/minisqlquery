#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core;

namespace MiniSqlQuery.Commands
{
	/// <summary>The show options form command.</summary>
	public class ShowOptionsFormCommand
		: CommandBase
	{
		/// <summary>Initializes a new instance of the <see cref="ShowOptionsFormCommand"/> class.</summary>
		public ShowOptionsFormCommand()
			: base("Options")
		{
			// ShortcutKeys = ?;
			SmallImage = ImageResource.cog;
		}

		/// <summary>Execute the command.</summary>
		public override void Execute()
		{
			using (OptionsForm optionsForm = Services.Resolve<OptionsForm>())
			{
				optionsForm.ShowDialog(HostWindow.Instance);
			}
		}
	}
}