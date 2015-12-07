using System;
using System.Collections.Specialized;
using MiniSqlQuery.Core;

namespace MiniSqlQuery.Plugins
{
	internal class MockApplicationSettings : Core.IApplicationSettings
	{
		public int CommandTimeout { get; set; }

		public System.Data.Common.DbConnection Connection { get; set; }

		public DbConnectionDefinition ConnectionDefinition { get; set; }

		public string DateTimeFormat { get; set; }

		public string DefaultConnectionDefinitionFilename { get; set; }

		public string DefaultFileFilter { get; set; }

		public bool EnableQueryBatching { get; set; }

		public bool IncludeReadOnlyColumnsInExport { get; set; }

		public bool LoadExternalPlugins { get; set; }

		public StringCollection MostRecentFiles { get; set; }

		public string NullText { get; set; }

		public string PlugInFileFilter { get; set; }

		public System.Data.Common.DbProviderFactory ProviderFactory { get; set; }

		public event EventHandler ConnectionDefinitionsChanged;
		public event EventHandler DatabaseConnectionReset;

		public void CloseConnection()
		{
			throw new NotImplementedException();
		}

		public DbConnectionDefinitionList GetConnectionDefinitions()
		{
			throw new NotImplementedException();
		}

		public System.Data.Common.DbConnection GetOpenConnection()
		{
			throw new NotImplementedException();
		}

		public int GetUntitledDocumentCounter()
		{
			throw new NotImplementedException();
		}

		public void ResetConnection()
		{
			throw new NotImplementedException();
		}

		public void SetConnectionDefinitions(DbConnectionDefinitionList definitionList)
		{
			throw new NotImplementedException();
		}
	}
}