using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Dialect;
using NHibernate.Migrations.Fluent;
using NUnit.Framework;

namespace NHibernate.Test.Migrations
{
    [TestFixture]
    public class SurfaceFixture
    {
        [Test]
        public void CanSql()
        {
            var surface = new TestSurface();

            surface.Sql("Hello");

            var sql = surface.GetSqlStatements(new GenericDialect()).First();
            Assert.That(sql, Is.EqualTo("Hello"));

        }
    }
}
