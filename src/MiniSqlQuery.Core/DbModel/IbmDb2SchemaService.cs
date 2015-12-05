using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MiniSqlQuery.Core.DbModel
{
    internal class IbmDb2SchemaService : IDatabaseSchemaService
    {
        public string ProviderName { get; set; }

        public IbmDb2SchemaService(string providerName)
        {
            ProviderName = providerName;
        }
        public DbModelInstance GetDbObjectModel(string connectionString)
        {
            var model = new DbModelInstance
            {
                ConnectionString = connectionString,
                ProviderName = ProviderName,
            };

            using (var connection = DbProviderFactories.GetFactory(ProviderName).CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                //var sql = "select schemaname from syscat.schemata";
                //var table = connection.GetSchema("Columns");
                var table = connection.GetSchema("Tables");
                foreach (DataRow row in table.Rows)
                {
                    var tableModel = new DbModelTable
                    {
                        Schema = row[table.Columns["TABLE_SCHEMA"]].ToString(),
                        Name = row[table.Columns["TABLE_NAME"]].ToString(),
                        ObjectType = row[table.Columns["TABLE_TYPE"]].ToString(),

                    };
                    model.Add(tableModel);
                }
            }

            return model;
        }

        public Dictionary<string, DbModelType> GetDbTypes(DbConnection connection)
        {
            var types = connection.GetSchema("DataTypes");
            return new Dictionary<string, DbModelType>();
        }

        public string GetDescription()
        {
            return "";
        }
    }
}
