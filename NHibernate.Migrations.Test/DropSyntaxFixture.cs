using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Dialect;
using NHibernate.Migrations.Fluent;
using NUnit.Framework;

namespace NHibernate.Test.Migrations
{
    [TestFixture]
    public class DropSyntaxFixture
    {
        [Test]
        public void CanDropTable_WithDbName()
        {
            var surface = new TestSurface();
            surface.Drop.Table(new DbName(null, "Silly","Things"));
            var op = (DropTableDdlOperation)(surface.GeneratedOperations.First());
            Assert.That(op.TableName.Schema, Is.EqualTo("Silly"));
            Assert.That(op.TableName.Name, Is.EqualTo("Things"));
        }

        [Test]
        public void CanDropTable_WithStringName()
        {
            var surface = new TestSurface();
            surface.Drop.Table("Silly.Things");
            var op = (DropTableDdlOperation)(surface.GeneratedOperations.First());
            Assert.That(op.TableName.Schema, Is.EqualTo("Silly"));
            Assert.That(op.TableName.Name, Is.EqualTo("Things"));
        }

        [Test]
        public void CanDropIndex()
        {
            var surface = new TestSurface();
            surface.Drop.Index("IX_Fooy", new DbName("silly.Things"));
            var op = (DropIndexDdlOperation)(surface.GeneratedOperations.First());

            Assert.That(op.Model.Name, Is.EqualTo("IX_Fooy"));
            Assert.That(op.Model.TableName.Schema, Is.EqualTo("silly"));
            Assert.That(op.Model.TableName.Name, Is.EqualTo("Things"));
            Assert.That(op.Model.Clustered, Is.False);
            Assert.That(op.Model.Unique, Is.False);
        }

        [Test]
        public void CanDropSequence()
        {
            var surface = new TestSurface();
            string sequenceName = "Larry";
            surface.Drop.Sequence(sequenceName);

            var sql = surface.GetSqlStatements(new Oracle8iDialect()).First();
            Assert.That(sql, Is.EqualTo("drop sequence Larry"));
        }
    }
}
