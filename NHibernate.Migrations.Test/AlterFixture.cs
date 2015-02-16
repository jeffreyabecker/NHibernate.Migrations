using System.Linq;
using NHibernate.DdlGen.Model;
using NHibernate.Dialect;
using NHibernate.Migrations.Fluent;
using NHibernate.Migrations.Fluent.Builders;
using NUnit.Framework;

namespace NHibernate.Test.Migrations
{
    [TestFixture]
    public class AlterFixture
    {
        [Test]
        public void CanAddColumn()
        {
            var surface = new TestSurface();
            surface.Alter.Table(new DbName("Test"))
                   .AddColumn("Example", cb => cb.AnsiString(20));

            var sql = surface.GetSqlStatements(new GenericDialect()).First();
            Assert.That(sql, Is.EqualTo("alter table Test add column Example VARCHAR(20)"));


        }

        [Test]
        public void CanDropColumn()
        {
            var surface = new TestSurface();
            surface.Alter.Table("Test")
                .DropColumn("Example");

            var sql = surface.GetSqlStatements(new GenericDialect()).First();
            Assert.That(sql, Is.EqualTo("alter table Test drop column Example"));
        }


        [Test]
        public void CanAlterColumn()
        {
            var surface = new TestSurface();
            surface.Alter.Table("Test")
                .AlterColumn("Example", cb => cb.AnsiString(20));

            var sql = surface.GetSqlStatements(new MsSql2000Dialect()).First();
            Assert.That(sql, Is.EqualTo("alter table Test alter column Example VARCHAR(20) null"));
        }


        [Test]
        public void CanDropForeignKey()
        {
            var surface = new TestSurface();
            surface.Alter.Table("Test")
                   .DropForeignKey("FK_Bob");

            var sql = surface.GetSqlStatements(new GenericDialect()).First().Trim();
            Assert.That(sql, Is.EqualTo("alter table Test  drop constraint FK_Bob"));
        }

        [Test]
        public void CanDropIndex()
        {
            var surface = new TestSurface();
            surface.Alter.Table("Test")
                .DropIndex("IX_Larry", unique:false);

            var sql = surface.GetSqlStatements(new GenericDialect()).First().Trim();
            Assert.That(sql, Is.EqualTo("drop index Test.IX_Larry"));

        }


        [Test]
        public void CanDropUniqueIndex()
        {
            var surface = new TestSurface();
            surface.Alter.Table("Test")
                   .DropIndex("IX_Larry", unique:true);

            var sql = surface.GetSqlStatements(new GenericDialect()).First().Trim();
            Assert.That(sql, Is.EqualTo("alter table Test drop constraint IX_Larry"));

        }

        [Test]
        public void CanCreateForeignKey()
        {
            var surface = new TestSurface();
            string fkName = "FK_Underpants";
            var b1 = surface.Alter.Table("Test")
                .AddForeignKey("OtherId")
                .References(new DbName("OtherTable"));

            var sql = surface.GetSqlStatements(new GenericDialect()).First().Trim();
            Assert.That(sql, Is.EqualTo("alter table Test  add foreign key (OtherId) references OtherTable"));


        }

    }
}
