#region License

// Copyright 2005-2009 Paul Kohler (http://pksoftware.net/MiniSqlQuery/). All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://minisqlquery.codeplex.com/license

#endregion License

using MiniSqlQuery.Core;
using MiniSqlQuery.Core.DbModel;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace MiniSqlQuery.Tests.DbModel
{
    [TestFixture(Description = "Requires SQLCE DB", Category = "Functional")]// todo - build db model manually for unit test to remove db dependency
    public class DbModelTable_tests
    {
        private DbModelInstance _model;
        private string _connStr = @"data source=|DataDirectory|\sqlce-test.sdf";
        private string _providerName = "System.Data.SqlServerCe.3.5";

        [SetUp]
        public void TestSetup()
        {
            var service = DatabaseMetaDataService.Create(_providerName);// new SqlCeSchemaService { ProviderName = _providerName };
            _model = service.GetDbObjectModel(_connStr);
        }

        [Test]
        public void Can_find_tables_by_name()
        {
            var table = _model.FindTable("StaffUnit");
            Assert.That(table, Is.Not.Null);
        }

        [Test]
        public void Returns_null_if_cant_find_table_by_name()
        {
            var table = _model.FindTable("foo");
            Assert.That(table, Is.Null);
        }
    }
}