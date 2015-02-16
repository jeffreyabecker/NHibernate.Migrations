using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using NHibernate.Cfg;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Generation;
using NUnit.Framework;

namespace NHibernate.Migrations.Test
{
    [TestFixture]
    class CanBuildClass
    {
        [Test]
        public void CanBuildClassFromStuff()
        {
            var b = new Builder2<CSharpCodeProvider>();
            var code = b.GenerateMigration(new MigrationGenerationArguments
            {
                Cfg = new Configuration(),
                CodeNamespace = "ExampleNamespace",
                Context = "Default",
                Name = "ExampleMigration",
                Version = "12345",
                Operations =new IDdlOperation[]
                {
                    new EnableForeignKeyConstratintDdlOperation(),
                    new DisableForeignKeyConstraintDdlOperation(), 
                    new DropTableDdlOperation(new DbName("dbo.Hello")), 
                    new AddTableCommentsDdlOperation( new TableCommentsModel{ TableName = new DbName(null,null,"ExampleTable"), Comment = "Hi", Columns = new List<ColumnCommentModel>
                    {
                        new ColumnCommentModel{Comment = "Hi", ColumnName="ExampleColumn"}
                    }})
                }
            });
            Console.WriteLine(code);
        }

    }
}
