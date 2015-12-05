using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MiniSqlQuery.Core
{
    [TestFixture]
    public class DatabaseMetaDataServiceTests
    {
        [Test]
        public void CanListIbmDb2()
        {
            var service = DatabaseMetaDataService.Create("IBM.Data.DB2");
            var dbModel = service.GetDbObjectModel("Server=sbn01q-db202:50000;Database=infoturn;CurrentSchema=userid;");

            Assert.That(dbModel.Tables.Count(), Is.EqualTo(306));
            Assert.That(dbModel.Views.Count(), Is.EqualTo(280));
        }

        private void PrintObject<T>(T obj)
        {
            if (obj == null) Console.WriteLine("<NULL>");
            var objType = typeof(T);
            foreach (var prop in objType.GetProperties())
            {
                Console.WriteLine("{0}:{1}", prop.Name, prop.GetValue(obj, null) ?? "<NULL>");
            }
        }
    }
}
