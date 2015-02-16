using System.Data;
using System.Linq;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Dialect;
using NHibernate.Migrations.Fluent;
using NUnit.Framework;


namespace NHibernate.Test.Migrations
{
    [TestFixture]
    public class TableBuilderFixture
    {

        [Test]
        public void CanCreateSequence()
        {
            var surface = new TestSurface();

            surface.Create.Sequence("dbo.Foo");
            var op = surface.GeneratedOperations.First() as CreateSequenceDdlOperation;
            Assert.That(op, Is.Not.Null);
            Assert.That(op.Model, Is.Not.Null);
            Assert.That(op.Model.Name.ToString(), Is.EqualTo("dbo.Foo"));
            Assert.That(op.Model.IncrementSize, Is.EqualTo(1));
            Assert.That(op.Model.InitialValue, Is.EqualTo(1));
        }

 
        [Test]
        public void CanCreateTableWithPrimaryKey()
        {
            var surface = new TestSurface();
            surface.Create.Table("Larry", c => new {Name = c.String(512, nullable: false)})
                .PrimaryKey(x => x.Name);

            var op = surface.GeneratedOperations.First() as CreateTableDdlOperation;
            Assert.That(op, Is.Not.Null);
            Assert.That(op.Model.Name.Name, Is.EqualTo("Larry"));
            Assert.That(op.Model.Columns.First().Name, Is.EqualTo("Name"));
            Assert.That(op.Model.Columns.First().SqlTypeCode.DbType, Is.EqualTo(DbType.String));
            Assert.That(op.Model.Columns.First().SqlTypeCode.Length, Is.EqualTo(512));
            Assert.That(op.Model.Columns.First().Nullable, Is.EqualTo(false));
            var sql = op.GetStatements(new GenericDialect()).First();
        }

        [Test]
        public void CanCreateTableWithCompositePrimaryKey()
        {
            var surface = new TestSurface();
            surface.Create.Table("Larry", c => new
            {
                Name = c.String(512, nullable: false),
                Birthdate = c.Date(nullable: false)
            })
                .PrimaryKey(x => new{x.Name, x.Birthdate});

            var op = surface.GeneratedOperations.First() as CreateTableDdlOperation;
            Assert.That(op.Model.PrimaryKey, Is.Not.Null);
            Assert.That(op.Model.PrimaryKey.Columns.Count, Is.EqualTo(2));
            Assert.That(op.Model.PrimaryKey.Columns.First().Name, Is.EqualTo("Name"));
           
        }


        [Test]
        public void CanCreateTableWithForeignKeys_WhenDialectRequiresInline()
        {
            var surface = new TestSurface();
            surface.Create.Table("This", c => new {Id = c.Int32(), ThatId = c.Int64()})
                   .PrimaryKey(x => x.Id, isIdentity: true)
                   .ForeignKey(x => x.ThatId, references: new DbName("ThatTable"), cascade: true, name: "FK_This_That");

            var allOperations = surface.GeneratedOperations;
            var createTableDdlOperation = allOperations.First() as CreateTableDdlOperation;
            Assert.That(createTableDdlOperation, Is.Not.Null);
            var createTableModel = createTableDdlOperation.Model;
            Assert.That(createTableModel.ForeignKeys.Count, Is.EqualTo(1));
            var fkModel = createTableModel.ForeignKeys.First();
            Assert.That(fkModel.CascadeDelete, Is.True);
            Assert.That(fkModel.Name,Is.EqualTo("FK_This_That"));
            Assert.That(fkModel.ReferencedTable.Name,Is.EqualTo("ThatTable"));
            Assert.That(fkModel.ForeignKeyColumns.First(), Is.EqualTo("ThatId"));
            

        }


        [Test]
        public void CanCreateTableWithForeignKeys_WhenDialectAllowsAlter()
        {
            var surface = new TestSurface();
            surface.Create.Table("This", c => new { Id = c.Int32(), ThatId = c.Int64() })
                   .PrimaryKey(x => x.Id, isIdentity: false)
                   .ForeignKey(x => x.ThatId, references: new DbName("ThatTable"), cascade: true, name: "FK_This_That");

            var op = surface.GeneratedOperations.ToList();

            Assert.That(op.Count, Is.EqualTo(2));
            var fkModel = ((CreateForeignKeyOperation)op[1]).Model;

            
            Assert.That(fkModel.CascadeDelete, Is.True);
            Assert.That(fkModel.Name, Is.EqualTo("FK_This_That"));
            Assert.That(fkModel.ReferencedTable.Name, Is.EqualTo("ThatTable"));
            Assert.That(fkModel.ForeignKeyColumns.First(), Is.EqualTo("ThatId"));

        }

        [Test]
        public void CanCommentOnTable()
        {
            var surface = new TestSurface();
            surface.Alter.Table("Test")
                .AddComment("Hello!");

            var sql = surface.GetSqlStatements(new Oracle8iDialect()).First();
            Assert.That(sql, Is.EqualTo("comment on table Test is 'Hello!'"));

        }

    }
}
