using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent;
using NUnit.Framework;


namespace NHibernate.Test.Migrations
{
    [TestFixture]
    public class IndexBuilderFixture
    {
        [Test]
        public void CanCreateIndexOnOneColumn()
        {
            var surface = new TestSurface();
            surface.Create.Index("IX_RubberBabyBuggieBumpers")
                   .On(new DbName (null, "Silly", "Items"))
                   .Columns("Price");
            var op = (CreateIndexDdlOperation)(surface.GeneratedOperations.First());
            Assert.That(op.Model.Name, Is.EqualTo("IX_RubberBabyBuggieBumpers"));
            Assert.That(op.Model.TableName.Schema, Is.EqualTo("Silly"));
            Assert.That(op.Model.TableName.Name, Is.EqualTo("Items"));
            Assert.That(op.Model.Columns.First(), Is.EqualTo("Price"));
            Assert.That(op.Model.Unique, Is.False);

        }

        [Test]
        public void CanCreateUniqueIndexOnOneColumn()
        {
            var surface = new TestSurface();
            surface.Create.Index("IX_RubberBabyBuggieBumpers")
                   .On(new DbName(null, "Silly", "Items"))
                   .Columns("Name").Unique();
            var op = (CreateIndexDdlOperation)(surface.GeneratedOperations.First());
            Assert.That(op.Model.Name, Is.EqualTo("IX_RubberBabyBuggieBumpers"));
            Assert.That(op.Model.TableName.Schema, Is.EqualTo("Silly"));
            Assert.That(op.Model.TableName.Name, Is.EqualTo("Items"));
            Assert.That(op.Model.Columns.First(), Is.EqualTo("Name"));
            Assert.That(op.Model.Unique, Is.True);
        }


        [Test]
        public void CanCreateIndexOnMultipleColumns()
        {
            var surface = new TestSurface();
            surface.Create.Index("IX_RubberBabyBuggieBumpers")
                   .On(new DbName(null, "Silly", "Items"))
                   .Columns("Price", "Name");
            var op = (CreateIndexDdlOperation)(surface.GeneratedOperations.First());
            Assert.That(op.Model.Name, Is.EqualTo("IX_RubberBabyBuggieBumpers"));
            Assert.That(op.Model.TableName.Schema, Is.EqualTo("Silly"));
            Assert.That(op.Model.TableName.Name, Is.EqualTo("Items"));
            Assert.That(op.Model.Columns.First(), Is.EqualTo("Price"));
            Assert.That(op.Model.Columns.Skip(1).First(), Is.EqualTo("Name"));
            Assert.That(op.Model.Unique, Is.False);

        }

        [Test]
        public void CanCreateUniqueIndexOnMultipleColumns()
        {
            var surface = new TestSurface();
            surface.Create.Index("IX_RubberBabyBuggieBumpers")
                   .On(new DbName(null, "Silly", "Items"))
                   .Columns("Name","Price").Unique();
            var op = (CreateIndexDdlOperation)(surface.GeneratedOperations.First());
            Assert.That(op.Model.Name, Is.EqualTo("IX_RubberBabyBuggieBumpers"));
            Assert.That(op.Model.TableName.Schema, Is.EqualTo("Silly"));
            Assert.That(op.Model.TableName.Name, Is.EqualTo("Items"));
            Assert.That(op.Model.Columns.First(), Is.EqualTo("Name"));
            Assert.That(op.Model.Columns.Skip(1).First(), Is.EqualTo("Price"));
            Assert.That(op.Model.Unique, Is.True);


        }
    }
}
